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

        public async Task<Comment> GetCommentById(int id)
        {
            var comment=   _appDb.comments.Where(x=>x.Id==id).FirstOrDefault();
                return comment;
                
        }

        public  Task<IEnumerable<Comment>> GetPostComments(int postId)
        {
            var comments=  _appDb.comments.Where(x=>x.PostId==postId).AsEnumerable();
            return Task.FromResult(comments);
        }

        public  Task<Comment?> GetUserComment(string userId, int postId)
        {
            var comment=   _appDb.comments.FirstOrDefault(x=>x.UserId==userId && x.PostId==postId);
            return Task.FromResult(comment);
        }

<<<<<<< HEAD
        public Task<string> RemoveComment(Comment comment)
        {
            var commentpost =  _appDb.comments.Remove(comment);
            return Task.FromResult("the comment is removed");
        }

      
=======
        public async Task<string> RemoveComment(Comment comment)
        {
            var commentpost = _appDb.comments.Remove(comment);
            await _appDb.SaveChangesAsync();
            return "the comment is removed";
        }

        public Task<Comment> EditComment(int id, Comment comment)
        {
            var existingComment = _appDb.comments.FirstOrDefault(x => x.Id == id);
            if (existingComment != null)
            {
                _appDb.Update(existingComment);
                _appDb.SaveChanges();
            }
            return Task.FromResult(existingComment);
        }

        public Task<Comment?> UserComment(string userId)
        {
            var userComment = _appDb.comments.FirstOrDefault(x => x.UserId == userId);
            return Task.FromResult(userComment);
        }

>>>>>>> Comment
    }
}
