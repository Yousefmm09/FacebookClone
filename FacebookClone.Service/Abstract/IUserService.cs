using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Abstract
{
    public interface IUserService
    {
        public Task<List<Friendship>> GetFriends(string userId);
        public Task<ProfileUserDto> GetProfile();
        public Task<string> UpdateProfile( User profileData);

        public Task<string> DeleteUser();
    }
}
