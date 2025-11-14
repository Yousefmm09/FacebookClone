using FacebookClone.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public  interface ICommentRepository
    {
        public Task<Comment> CreatComment(Comment comment);
        Task<Comment?> GetUserComment(string userId, int postId);
    }
}
