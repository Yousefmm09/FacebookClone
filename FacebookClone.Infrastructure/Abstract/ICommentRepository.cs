using FacebookClone.Data.Entities;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Diagnostics.Contracts;
=======
>>>>>>> 13a9533776d69d7cb7fd77eb476a062271fde758
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public  interface ICommentRepository
    {
        public Task<Comment> CreatComment(Comment comment);
        Task<Comment?> GetUserComment(string userId, int postId);
<<<<<<< HEAD

        public Task<Comment> GetCommentById(int id); 
        public Task<IEnumerable<Comment>> GetPostComments(int postId);
        public Task<string> RemoveComment(Comment comment);
=======
>>>>>>> 13a9533776d69d7cb7fd77eb476a062271fde758
    }
}
