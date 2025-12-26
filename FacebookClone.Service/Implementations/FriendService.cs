using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using FacebookClone.Api.Hubs;
using FacebookClone.Infrastructure.Context;

namespace FacebookClone.Service.Implementations
{
    public class FriendService : IFriendService
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly AppDb _db;
        private readonly ILogger<FriendService> _logger;

        public FriendService(
            IFriendsRepository friendsRepository, 
            IHttpContextAccessor httpContext, 
            UserManager<User> userManager,
            IHubContext<NotificationHub> notificationHub, 
            AppDb db,
            ILogger<FriendService> logger)
        {
            _friendsRepository = friendsRepository;
            _contextAccessor = httpContext;
            _userManager = userManager;
            _notificationHub = notificationHub;
            _db = db;
            _logger = logger;
        }

        private string GetCurrentUserId()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Attempt to access friend service without authentication");
                throw new UnauthorizedAccessException("User not authenticated");
            }
            return userId;
        }

        public async Task<FriendshipDto> AcceptFriend(FriendshipDto friendRequest)
        {
            var currentUser = GetCurrentUserId();
            var senderId = friendRequest.UserId;

            _logger.LogInformation("Attempting to accept friend request. SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, currentUser);

            var request = await _friendsRepository.GetFriendRequest(senderId, currentUser);

            if (request == null)
            {
                _logger.LogWarning("Friend request not found. SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, currentUser);
                throw new KeyNotFoundException("Friend request not found");
            }

            if (request.Status == FriendRequest.FriendRequestStatus.Accepted)
            {
                _logger.LogWarning("Friend request already accepted. SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, currentUser);
                throw new InvalidOperationException("Friend request already accepted");
            }

            if (request.Status == FriendRequest.FriendRequestStatus.Rejected)
            {
                _logger.LogWarning("Attempt to accept rejected friend request. SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, currentUser);
                throw new InvalidOperationException("Friend request was rejected before and can't be accepted");
            }

            request.Status = FriendRequest.FriendRequestStatus.Accepted;
            await _friendsRepository.UpdateFriendRequest(request);

            var friendship1 = new Friendship
            {
                UserId = senderId,
                FriendId = currentUser,
                CreatedAt = DateTime.UtcNow,
            };

            var friendship2 = new Friendship
            {
                UserId = currentUser,
                FriendId = senderId,
                CreatedAt = DateTime.UtcNow,
            };

            await _friendsRepository.AcceptFriend(friendship1);
            await _friendsRepository.AcceptFriend(friendship2);

            _logger.LogInformation("Friend request accepted successfully. SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, currentUser);

           // notify both users
           //await NotifyUser(senderId, "FriendAccepted", "Friend request accepted", $"{_userManager.FindByIdAsync(currentUser).Result?.UserName} accepted your request", currentUser);
           //await NotifyUser(currentUser, "FriendAccepted", "Friend request accepted", $"You are now friends with {_userManager.FindByIdAsync(senderId).Result?.UserName}", senderId);

            return new FriendshipDto
            {
                UserId = senderId,
                FriendId = currentUser,
                CreatedAt = DateTime.UtcNow
            };
        }

        public async Task<string> RemoveFriendShip(string friendId)
        {
            var userId = GetCurrentUserId();

            if (string.IsNullOrEmpty(friendId))
                throw new Exception("Friend ID is required");

            if (userId == friendId)
                throw new Exception("You cannot unfriend yourself");

            var friendship1 = await _friendsRepository.GetFriendShip(userId, friendId);

            if (friendship1 == null)
                throw new Exception("You are not friends");

            var friendship2 = await _friendsRepository.GetFriendShip(friendId, userId);

            if (friendship1 != null)
                await _friendsRepository.RemoveFriendShip(friendship1);

            if (friendship2 != null && friendship2.Id != friendship1.Id)
                await _friendsRepository.RemoveFriendShip(friendship2);

            return "Unfriend done successfully";
        }

        public async Task<FriendRequestDto> SendFriendRequest(FriendRequestDto friendRequest)
        {
            var senderId = GetCurrentUserId();
            var receiverId = friendRequest.ReceiverId;

            _logger.LogInformation("Attempting to send friend request. SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, receiverId);

            if (string.IsNullOrEmpty(receiverId))
            {
                _logger.LogWarning("Receiver ID is null or empty");
                throw new ArgumentException("Receiver not found");
            }

            if (receiverId == senderId)
            {
                _logger.LogWarning("User attempting to send friend request to themselves. UserId: {UserId}", senderId);
                throw new InvalidOperationException("You cannot send a request to yourself");
            }
            // Check reverse request (Receiver had sent before)
            var reverseRequest = await _friendsRepository.GetFriendRequest(receiverId, senderId);

            if (reverseRequest != null)
            {
                switch (reverseRequest.Status)
                {
                    case FriendRequest.FriendRequestStatus.Pending:
                        throw new Exception("This user already sent you a friend request. Accept it instead.");

                    case FriendRequest.FriendRequestStatus.Accepted:
                        var friendship = await _friendsRepository.GetFriendShip(senderId, receiverId);
                        if (friendship != null)
                            throw new Exception("You are already friends");

                        // Reverse request was accepted but friendship deleted → re-send
                        reverseRequest.Status = FriendRequest.FriendRequestStatus.Pending;
                        reverseRequest.SentAt = DateTime.UtcNow;
                        await _friendsRepository.UpdateFriendRequest(reverseRequest);

                        var userX = await _userManager.FindByIdAsync(receiverId);
                        return new FriendRequestDto
                        {
                            ReceiverId = receiverId,
                            ReceiverUserName = userX?.UserName ?? "Unknown",
                            SentAt = reverseRequest.SentAt,
                            Status = FriendRequestDto.FriendRequestStatus.Pending
                        };

                    case FriendRequest.FriendRequestStatus.Rejected:
                        reverseRequest.Status = FriendRequest.FriendRequestStatus.Pending;
                        reverseRequest.SentAt = DateTime.UtcNow;
                        await _friendsRepository.UpdateFriendRequest(reverseRequest);

                        var userY = await _userManager.FindByIdAsync(receiverId);
                        return new FriendRequestDto
                        {
                            ReceiverId = receiverId,
                            ReceiverUserName = userY?.UserName ?? "Unknown",
                            SentAt = reverseRequest.SentAt,
                            Status = FriendRequestDto.FriendRequestStatus.Pending
                        };
                }
            }
            var existingRequest = await _friendsRepository.GetFriendRequest(senderId, receiverId);

            if (existingRequest != null)
            {
                switch (existingRequest.Status)
                {
                    case FriendRequest.FriendRequestStatus.Pending:
                        throw new Exception("Friend request already sent and pending");

                    case FriendRequest.FriendRequestStatus.Accepted:
                        {
                            var friendship = await _friendsRepository.GetFriendShip(senderId, receiverId);

                            if (friendship != null)
                                throw new Exception("You are already friends");

                            // check is accepted and the  not found friendship (unfriend together)
                            existingRequest.Status = FriendRequest.FriendRequestStatus.Pending;
                            existingRequest.SentAt = DateTime.UtcNow;
                            await _friendsRepository.UpdateFriendRequest(existingRequest);

                            var receiverUserA = await _userManager.FindByIdAsync(receiverId);
                            return new FriendRequestDto
                            {
                                ReceiverId = receiverId,
                                ReceiverUserName = receiverUserA?.UserName ?? "Unknown",
                                SentAt = existingRequest.SentAt,
                                Status = FriendRequestDto.FriendRequestStatus.Pending
                            };
                        }

                    case FriendRequest.FriendRequestStatus.Rejected:
                        existingRequest.Status = FriendRequest.FriendRequestStatus.Pending;
                        existingRequest.SentAt = DateTime.UtcNow;
                        await _friendsRepository.UpdateFriendRequest(existingRequest);

                        var receiverUser1 = await _userManager.FindByIdAsync(receiverId);
                        return new FriendRequestDto
                        {
                            ReceiverId = receiverId,
                            ReceiverUserName = receiverUser1?.UserName ?? "Unknown",
                            SentAt = existingRequest.SentAt,
                            Status = FriendRequestDto.FriendRequestStatus.Pending
                        };
                }
            }

            var existingFriendship = await _friendsRepository.GetFriendShip(senderId, receiverId);
            if (existingFriendship != null)
                throw new Exception("You are already friends");

            var newRequest = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                SentAt = DateTime.UtcNow,
                Status = FriendRequest.FriendRequestStatus.Pending,
            };
            await _friendsRepository.SendFriendRequest(newRequest);

            _logger.LogInformation("Friend request sent successfully. SenderId: {SenderId}, ReceiverId: {ReceiverId}", senderId, receiverId);

           // notify receiver
           //await _notificationService.Notify(receiverId, "FriendRequest", "New friend request", $"{_userManager.FindByIdAsync(senderId).Result?.UserName} sent you a friend request", senderId);

            var receiverUser = await _userManager.FindByIdAsync(receiverId);
            return new FriendRequestDto
            {
                ReceiverId = receiverId,
                ReceiverUserName = receiverUser?.UserName ?? "Unknown",
                SentAt = newRequest.SentAt,
                Status = FriendRequestDto.FriendRequestStatus.Pending
            };
        }
    }
}