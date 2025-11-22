using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FacebookClone.Service.Implementations
{
    public class FriendService : IFriendService
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public FriendService(IFriendsRepository friendsRepository, IHttpContextAccessor httpContext, UserManager<User> userManager)
        {
            _friendsRepository = friendsRepository;
            _contextAccessor = httpContext;
            _userManager = userManager;
        }

        private string GetCurrentUserId()
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");
            return userId;
        }

        public async Task<FriendshipDto> AcceptFriend(FriendshipDto friendRequest)
        {
            var currentUser = GetCurrentUserId();
            var senderId = friendRequest.UserId;

            var request = await _friendsRepository.GetFriendRequest(senderId, currentUser);

            if (request == null)
                throw new Exception("Friend request not found");

            if (request.Status == FriendRequest.FriendRequestStatus.Accepted)
                throw new Exception("Friend request already accepted");

            if (request.Status == FriendRequest.FriendRequestStatus.Rejected)
                throw new Exception("Friend request was rejected before and can't be accepted");

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

            return new FriendshipDto
            {
                UserId = senderId,
                FriendId = currentUser,
                CreatedAt = DateTime.UtcNow
            };
        }

        


        public async Task<FriendRequestDto> SendFriendRequest(FriendRequestDto friendRequest)
        {
            var senderId = GetCurrentUserId();
            var receiverId = friendRequest.ReceiverId;

            if (string.IsNullOrEmpty(receiverId))
                throw new Exception("Receiver not found");

            if (receiverId == senderId)
                throw new Exception("You cannot send a request to yourself");
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

            var receiverUser = await _userManager.FindByIdAsync(receiverId);
            return new FriendRequestDto
            {
                ReceiverId = receiverId,
                ReceiverUserName = receiverUser?.UserName ?? "Unknown",
                SentAt = newRequest.SentAt,
                Status = FriendRequestDto.FriendRequestStatus.Pending
            };
        }

        public  Task RemoveFriendRequests(string userId)
        {
            return   _friendsRepository.RemoveFriendRequests(userId);
        }

        public async Task RemoveFriendShip(string userId, string friendId)
        {
         
            var friendship1 = await _friendsRepository.GetFriendShip(userId, friendId);
            if (friendship1 != null)
                await _friendsRepository.DeleteFriendshipEntity(friendship1);

            
            var friendship2 = await _friendsRepository.GetFriendShip(friendId, userId);
            if (friendship2 != null)
                await _friendsRepository.DeleteFriendshipEntity(friendship2);
        }

        public async Task RemoveBothFriendShips(string userId, string friendId)
{
    // friendship 1
    await _friendsRepository.RemoveFriendShip(userId, friendId);

    // friendship 2
    await _friendsRepository.RemoveFriendShip(friendId, userId);
}

        public Task<List<string>> getAllFriends(string userId)
        {
            return _friendsRepository.getAllFriends(userId);
        }
    }
}