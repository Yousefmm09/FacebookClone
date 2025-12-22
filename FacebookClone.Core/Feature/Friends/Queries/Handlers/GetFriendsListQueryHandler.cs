using FacebookClone.Core.Feature.Friends.Queries.Models;
using FacebookClone.Infrastructure.Context;
using FacebookClone.Service.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FacebookClone.Core.Feature.Friends.Queries.Handlers
{
    public class GetFriendsListQueryHandler 
        : IRequestHandler<GetFriendsListQuery, List<UserDto>>
    {
        private readonly AppDb _context;

        public GetFriendsListQueryHandler(AppDb context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(GetFriendsListQuery request, CancellationToken ct)
        {
            // Get all friendships where the user is either UserId or FriendId
            var friends = await _context.friendShips
                .Where(f => f.UserId == request.UserId || f.FriendId == request.UserId)
                .Select(f => f.UserId == request.UserId ? f.Friend : f.User)
                .Select(u => new UserDto
                {
                    UserId = u.Id,
                    UserName = u.UserName ?? string.Empty,
                    Email = u.Email ?? string.Empty,
                    Bio = u.Bio ?? string.Empty,
                    ProfileImageUrl = u.ProfilePictureUrl ?? string.Empty
                })
                .ToListAsync(ct);

            return friends;
        }
    }
}
