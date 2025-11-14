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
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDb _appDb;
        public CommentRepository(AppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<Comment> CreatComment(Comment comment)
        {
            await _appDb.comments.AddAsync(comment);
            await _appDb.SaveChangesAsync();
            return comment;
        }
    }
}
