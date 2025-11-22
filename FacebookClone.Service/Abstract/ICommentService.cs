using FacebookClone.Data.Entities;
<<<<<<< HEAD
using FacebookClone.Infrastructure.Context;
=======
>>>>>>> 13a9533776d69d7cb7fd77eb476a062271fde758
using FacebookClone.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Abstract
{
    public interface ICommentService
    {
        public Task<CommentDto> CreatComment(CreateCommentDto comment);
<<<<<<< HEAD
        public Task<CommentDto> GetCommentById(int id);
        public Task<string> RemoveComment(int id);

        public Task<Comment?> GetUserComment(string userId, int postId);

        public Task<IEnumerable<CommentDto>> GetPostComments(int postId);



=======
>>>>>>> 13a9533776d69d7cb7fd77eb476a062271fde758
    }
}
