using FacebookClone.Data.Entities.Identity;
using FacebookClone.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Abstract
{
    public interface IAuthenticationsService
    {
        public Task<AuthMessage> CreatRefreshToken(string OldAccessToken, string RefreshToekn);
        public Task<AuthMessage> CreateAccessTokenAsync(User user);
        public Task<AuthMessage> ConfirmEmail(string userId,string token);
    }
}
