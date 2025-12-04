using FacebookClone.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public interface ILikeRepository
    {
        public Task<string> SetLike(Like like);
        Task<Like?> GetUserLike(string userId,int postId);
         Task<string> RemoveLike(Like like);
    }
}
