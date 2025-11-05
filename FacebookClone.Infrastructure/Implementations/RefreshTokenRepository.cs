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
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDb _appDb;
        public RefreshTokenRepository(AppDb appDb)
        {
            _appDb = appDb;
        }
        public  IQueryable <UserRefreshToken> GetTableNoTracking()
        {
            return  _appDb.userRefreshToken.AsNoTracking();
        }

        public async Task<UserRefreshToken> RefreshToken(UserRefreshToken userRefreshToken)
        {
          await   _appDb.userRefreshToken.AddAsync(userRefreshToken);
            await _appDb.SaveChangesAsync();
            return userRefreshToken;
        }
    }
}
