using FacebookClone.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure.Abstract
{
    public interface IPostRepository
    {
        public Task<Post> CreatPostAsync(Post post);
        public Task<string> DeletePost(int postId);
        public Task<string> UpdatePost(Post post, int postId);
        public Task<Post> GetPostById(int postId);
    }
}
