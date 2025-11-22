using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
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
    public class UserRepository : IUserRepository
    {
        private readonly AppDb _appDb;

        public UserRepository(AppDb appDb)
        {
            _appDb = appDb;
        }

        public async Task<string> DeleteUser(string userId)
        {
            var user = await _appDb.Users.FindAsync(userId);
            if (user != null)
            {
                _appDb.Users.Remove(user);
                await _appDb.SaveChangesAsync();
                return "The user deleted successfully";
            }
            return "Not found user";
        }

        public async Task<List<Friendship>> GetFriends(string userId)
        {
            return  _appDb.friendShips.Where(x=>x.UserId==userId||x.FriendId==userId).
                Include(x=>x.User).
                Include(x=>x.Friend)
                .AsSplitQuery()
                .AsNoTracking().
                ToList();
        }

        public async Task<User> GetProfile(string userId)
        {
            var user = await _appDb.Users.Include(p=>p.Posts).FirstOrDefaultAsync(x=>x.Id==userId);

            return user;
        }

        public async Task Update(User user)
        {
           _appDb.Users.Update(user);
          await  _appDb.SaveChangesAsync();
        }

        public async Task<string> UpdateProfile(string userId, User profileData)
        {
            var user = await _appDb.Users.FirstOrDefaultAsync(x=>x.Id==userId);
            if (user != null)
            {
               
                user.Bio = profileData.Bio;
                user.UserName=profileData.UserName;
                user.Email = profileData.Email;
                user.PhoneNumber=profileData.PhoneNumber;
                user.ProfilePictureUrl=profileData.ProfilePictureUrl;
                _appDb.Users.Update(user);
                _appDb.SaveChanges();
                return "Profile updated successfully";
            }
            return "User not found";
        }

        public  int  GetFriendCount(string userId)
        {
            return   _appDb.friendShips.Where(x=>x.UserId==userId).Count();
        }
        public  int  GetLikeCount(string userId)
        {
            return   _appDb.Likes.Where(x => x.UserId == userId).Count();
        }
    }
}
