using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public interface IAdmin
    {
        public Task<List<User>> GetAllUser(int PageSize,int PageNumber);
        public Task<int> GetTotalUsersCountAsync();
        public Task<bool> BannedUser(string userId, string BannedReason);
        public Task<bool> UnbannedUser(string userId);
        public Task<User> GetUserDetails(string userId);
    }
}
