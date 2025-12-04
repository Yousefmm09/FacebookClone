using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Implementations
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDb _appDb;
        private readonly UserManager<User> _userManager;
        public PostRepository(AppDb appDb,UserManager<User> userManager)
        {
            _appDb=appDb;
            _userManager=userManager;
        }
        public async Task<Post> CreatPostAsync(Post post)
        {
            await _appDb.Posts.AddAsync(post);
            await _appDb.SaveChangesAsync();
            return post;

        }
        public async Task<string> DeletePost(int postId)
        {
            var post =  _appDb.Posts.Include(l=>l.Likes)
                .Include(x=>x.Comments).FirstOrDefault(x=>x.Id==postId);
            if (post != null)
            {
                _appDb.Likes.RemoveRange(post.Likes);
                _appDb.comments.RemoveRange(post.Comments);
                _appDb.Remove(post);
                await _appDb.SaveChangesAsync();
                return "the post delete successfully";
            }
            return "not found post";
        }

        public async Task<Post> GetPostById(int postId)
        {
            var post = await _appDb.Posts.FindAsync(postId);
            return post;
        }
        public async Task<int> GetCountPostbyUser(string userId)
        {
            var user = await _appDb.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("not found user");
            var CountPosts= _appDb.Posts.Where(x=>x.UserId==userId).Count();

            return CountPosts;
        }

        public async Task<int> LikeCount(string userId,int postId)
        {
            var count =   _appDb.Likes.Count(x => x.UserId == userId && x.PostId == postId);
            return count;
        }

        public async Task Update(Post post)
        {
            _appDb.Posts.Update(post);
            await _appDb.SaveChangesAsync();
        }

        public async Task<string> UpdatePost(Post post,int postId)
        {
            var postInDb = await _appDb.Posts.FindAsync(postId);
            if (postInDb == null)
            {
                return "Post Not Found";
            }
            var res = _appDb.Update(post);
            await _appDb.SaveChangesAsync();
            return "Post Updated Successfully";
        }
    }
}
