using FacebookClone.Service.Dto;
using MediatR;

namespace FacebookClone.Core.Feature.Friends.Queries.Models
{
    public record GetMyFriendsQuery(string UserId) 
        : IRequest<List<FriendshipDto>>;
}
