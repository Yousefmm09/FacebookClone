using FacebookClone.Core.Feature.Friends.Queries.Models;
using FacebookClone.Infrastructure.Context;
using FacebookClone.Service.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static FacebookClone.Data.Entities.FriendRequest;

namespace FacebookClone.Core.Feature.Friends.Queries.Handlers
{
    public class GetPendingFriendRequestsQueryHandler 
        : IRequestHandler<GetPendingFriendRequestsQuery, List<FriendRequestDto>>
    {
        private readonly AppDb _context;

        public GetPendingFriendRequestsQueryHandler(AppDb context)
        {
            _context = context;
        }

        public async Task<List<FriendRequestDto>> Handle(GetPendingFriendRequestsQuery request, CancellationToken ct)
        {
            // Get all pending friend requests where the user is the receiver
            var friendRequests = await _context.friendRequests
                .Where(fr => fr.ReceiverId == request.UserId && 
                            fr.Status == FriendRequestStatus.Pending)
                .Include(fr => fr.Sender)
                .Select(fr => new FriendRequestDto
                {
                    Id = fr.Id,
                    SenderId = fr.SenderId,
                    ReceiverId = fr.ReceiverId,
                    SenderUserName = fr.Sender.UserName ?? string.Empty,
                    SenderEmail = fr.Sender.Email ?? string.Empty,
                    SenderProfileImageUrl = fr.Sender.ProfilePictureUrl ?? string.Empty,
                    SentAt = fr.SentAt,
                    Status = (FriendRequestDto.FriendRequestStatus)fr.Status
                })
                .OrderByDescending(fr => fr.SentAt)
                .ToListAsync(ct);

            return friendRequests;
        }
    }
}
