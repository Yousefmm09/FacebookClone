using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public interface IRefreshTokenRepository
    {
        public Task<UserRefreshToken> RefreshToken(UserRefreshToken userRefreshToken);
        public IQueryable<UserRefreshToken> GetTableNoTracking();

    }
}
