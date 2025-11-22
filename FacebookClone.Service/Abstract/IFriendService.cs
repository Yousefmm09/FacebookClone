using FacebookClone.Data.Entities;
using FacebookClone.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Abstract
{
    public interface IFriendService
    {
        Task<FriendRequestDto> SendFriendRequest(FriendRequestDto friendRequest);
        Task<FriendshipDto> AcceptFriend(FriendshipDto friendRequest);
        Task RemoveFriendShip(string userId, string friendId);
        Task RemoveBothFriendShips(string userId, string friendId);
        Task RemoveFriendRequests(string userId);
        Task<List<string>> getAllFriends(string userId);
    }

}
