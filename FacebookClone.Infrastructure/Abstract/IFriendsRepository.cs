using FacebookClone.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public interface IFriendsRepository
    {
        public Task<FriendRequest> SendFriendRequest(FriendRequest friendRequest);
        public Task<Friendship> AcceptFriend(Friendship friendRequest);
        public Task<FriendRequest> GetFriendRequest(string senderId, string reciverId);
        public Task RemoveFriendShip(Friendship friendship);
        Task<Friendship> GetFriendShip(string userId, string friendId);
        Task UpdateFriendRequest(FriendRequest friendRequest);
    }
}
