using FacebookClone.Data.Entities;
using FacebookClone.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Abstract
{
    public interface ILikeSerivce
    {
        public Task<string> SetLike(LikeDto like);

        public Task<string> RemoveLike(int id);
        public Task<int> GetLikesCount(int postId);
    }
}
