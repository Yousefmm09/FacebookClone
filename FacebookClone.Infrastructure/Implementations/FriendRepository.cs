using FacebookClone.Data.Entities;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Implementations
{
    public class FriendRepository : IFriendsRepository
    {
        private readonly AppDb _appDb;

        public FriendRepository(AppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task<Friendship> AcceptFriend(Friendship friendRequest)
        {
            await _appDb.friendShips.AddAsync(friendRequest);
            await _appDb.SaveChangesAsync();
            return friendRequest;
        }

        public async Task<FriendRequest> SendFriendRequest(FriendRequest friendRequest)
        {
            await _appDb.friendRequests.AddAsync(friendRequest);
            await _appDb.SaveChangesAsync();
            return friendRequest;
        }
        public async Task<FriendRequest> GetFriendRequest(string senderId, string receiverId)
        {
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId))
                return null;

            return await _appDb.friendRequests
                .FirstOrDefaultAsync(x =>
                    x.SenderId == senderId && x.ReceiverId == receiverId);
        }

        public async Task UpdateFriendRequest(FriendRequest friendRequest)
        {
            _appDb.friendRequests.Update(friendRequest);
            await _appDb.SaveChangesAsync();
        }

        public async Task RemoveFriendShip(Friendship friendship)
        {
            _appDb.friendShips.Remove(friendship);
            await _appDb.SaveChangesAsync();
        }
        public async Task<Friendship> GetFriendShip(string userId, string friendId)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(friendId))
                return null;

            return await _appDb.friendShips
                .FirstOrDefaultAsync(x =>
                    (x.UserId == userId && x.FriendId == friendId) ||
                    (x.UserId == friendId && x.FriendId == userId));
        }
    }
}