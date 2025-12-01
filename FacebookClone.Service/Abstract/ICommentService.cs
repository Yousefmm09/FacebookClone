using FacebookClone.Data.Entities;


using FacebookClone.Infrastructure.Context;
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
        public Task<CommentDto> GetCommentById(int id);
        public Task<string> RemoveComment(int id);

        public Task<Comment?> GetUserComment(string userId, int postId);

        public Task<IEnumerable<CommentDto>> GetPostComments(int postId);
    }
}
