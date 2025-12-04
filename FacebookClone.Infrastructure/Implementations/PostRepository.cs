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
            var post = await _appDb.Posts.FindAsync(postId);
            if (post != null)
            {
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

        public async Task<PostsShare> SharePost(PostsShare posts)
        {
           
            var res = await _appDb.postsShares.AddAsync(posts);
            await _appDb.SaveChangesAsync();
            return res.Entity;
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
        public async Task<PostsShare?> GetPostShare(int postId, string userId)
        {
            return await _appDb.postsShares
                .FirstOrDefaultAsync(s => s.PostId == postId && s.UserId == userId);
        }
    }
}
