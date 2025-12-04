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
    public class AdminRepository : IAdmin
    {
        private readonly AppDb _appDb;
        public AdminRepository(AppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<List<User>> GetAllUser(int PageSize, int PageNumber)
        {
            return await _appDb.Users
        .AsNoTracking()
        .OrderBy(u => u.Id)
        .Skip((PageNumber - 1) * PageSize)
        .Take(PageSize)
        .ToListAsync();
        }
        public async Task<int> GetTotalUsersCountAsync()
        {
            return await _appDb.Users.CountAsync();
        }

        public async Task<bool> BannedUser(string userId,string BannedReason)
        {
            var user= await _appDb.Users.FindAsync( userId);
            if (user == null)
                throw new Exception("Not Found User");
            user.IsBanned = true;
            user.BanReason = BannedReason;
            user.BannedAt=DateTime.Now;
            await _appDb.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UnbannedUser(string userId)
        {
            var user = await _appDb.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("Not Found User");
            user.IsBanned = false;
            user.BannedAt = DateTime.Now;
            await _appDb.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserDetails(string userId)
        {
            var user =await _appDb.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("Not Found User");
            return user;
        }
    }
}
