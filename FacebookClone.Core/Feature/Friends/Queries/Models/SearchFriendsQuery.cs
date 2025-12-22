using FacebookClone.Service.Dto;
using MediatR;

namespace FacebookClone.Core.Feature.Friends.Queries.Models
{
    public record SearchFriendsQuery(string SearchTerm, string UserId) 
        : IRequest<List<UserDto>>;
}
