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
        public Task<JwtToken> CreatRefreshToken(string OldAccessToken, string RefreshToekn);
        public Task<JwtToken> CreateAccessTokenAsync(User user);
    }
}
