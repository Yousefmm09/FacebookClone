using FacebookClone.Core.Feature.Friends.Queries.Models;
using FacebookClone.Infrastructure.Context;
using FacebookClone.Service.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FacebookClone.Core.Feature.Friends.Queries.Handlers
{
    public class SearchFriendsQueryHandler 
        : IRequestHandler<SearchFriendsQuery, List<UserDto>>
    {
        private readonly AppDb _context;

        public SearchFriendsQueryHandler(AppDb context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(SearchFriendsQuery request, CancellationToken ct)
        {
            var searchTermLower = request.SearchTerm.ToLower();

            var users = await _context.Users
                .Where(u => u.Id != request.UserId && 
                           (u.UserName.ToLower().Contains(searchTermLower) ||
                            u.Email.ToLower().Contains(searchTermLower)))
                .Select(u => new UserDto
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Bio = u.Bio,
                    ProfileImageUrl = u.ProfilePictureUrl
                })
                .Take(20) // Limit to 20 results
                .ToListAsync(ct);

            return users;
        }
    }
}
