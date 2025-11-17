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
        public Task<FriendRequestDto> SendFriendRequest(FriendRequestDto friendRequest);
        public Task<FriendshipDto> AcceptFriend(FriendshipDto friendRequest);
        public Task<string> RemoveFriendShip(string friendId);

    }
}
