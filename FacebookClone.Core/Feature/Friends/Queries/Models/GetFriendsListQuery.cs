using FacebookClone.Service.Dto;
using MediatR;

namespace FacebookClone.Core.Feature.Friends.Queries.Models
{
    public record GetFriendsListQuery(string UserId) 
        : IRequest<List<UserDto>>;
}
