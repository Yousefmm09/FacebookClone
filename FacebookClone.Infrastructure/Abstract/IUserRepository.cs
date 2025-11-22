using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public interface IUserRepository
    {
        public Task<List<Friendship>> GetFriends(string userId);
        public Task<User> GetProfile(string userId);
        public Task<string> UpdateProfile(string userId, User profileData);
        public Task Update(User user);
        public int GetLikeCount(string userId);
        public int GetFriendCount(string userId);

        public Task<string> DeleteUser(string userId);
    }
}
