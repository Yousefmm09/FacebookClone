using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Data.Entities;
using FacebookClone.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Abstract
{
    public interface IPostService
    {
        public Task<PostDto> CreatPostAsync(PostDto postDto);
        public Task<string> UpdatePost(PostDto postDto,int postId);
        public Task<string> DeletePost(int postId);
        public Task<PostDto> GetPostById(int postId);
        public Task<PostShareDto> SharePost(int postid);
    }
}
