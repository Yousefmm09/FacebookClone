using FacebookClone.Data.Entities;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Implementations
{
    public class LikeRepository : ILikeRepository
    {
        private readonly AppDb _appDb;
        public LikeRepository(AppDb appDb)
        {
            _appDb=appDb;
        }

        public async Task<Like?> GetUserLike(string userId, int postId)
        {
            var user =   _appDb.Likes.FirstOrDefault(x => x.UserId == userId && x.PostId == postId);
            return user;
        }

        public async Task RemoveLike(Like like)
        {
            var likes= _appDb.Likes.Remove(like);
           await _appDb.SaveChangesAsync();
        }

        public async Task<string> SetLike(Like like)
        {
            var existingLike= _appDb.Likes.FirstOrDefault(x=>x.UserId==like.UserId&& x.PostId==like.PostId);
            if (existingLike != null)
                return "Already liked";

            await _appDb.Likes.AddAsync(like);
            await _appDb.SaveChangesAsync();
            return "the like is added";
        }
    }
}
