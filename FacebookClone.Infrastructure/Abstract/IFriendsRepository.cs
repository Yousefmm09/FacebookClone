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
        Task<string> RemoveFriendShip(string userId, string friendId);
        public Task DeleteFriendshipEntity(Friendship friendship);
        public Task RemoveFriendRequests(string userId);
        Task<Friendship> GetFriendShip(string userId, string friendId);
        Task UpdateFriendRequest(FriendRequest friendRequest);
        // get friendship by user ids
        public Task<List<string>> getAllFriends(string userId);
    }
}
