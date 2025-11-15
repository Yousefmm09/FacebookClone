using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManage;
        private readonly IHttpContextAccessor _contextAccessor;
        public PostService(IPostRepository postRepository,UserManager<User> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _postRepository=postRepository;
            _userManage=userManager;
            _contextAccessor=httpContextAccessor;
        }
        public async Task<PostDto> CreatPostAsync(PostDto postDto)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");
            var newPost = new Post
            {
                UserId = userId,
                Content = postDto.Content,
                CommentCount=postDto.CommentCount,
                ParentPostId=postDto.ParentPostId,
                Privacy=postDto.Privacy,
                CreatedAt=postDto.CreatedAt,
            };
            await _postRepository.CreatPostAsync(newPost);
            return new PostDto
            {
                PostId=newPost.Id,
                UserId = newPost.UserId,
                UserName = _userManage.Users.FirstOrDefault(u => u.Id == userId)?.UserName ?? "Unknown",
                Content = newPost.Content,
                ParentPostId = newPost.ParentPostId,
                Privacy = newPost.Privacy,
                LikeCount = newPost.LikeCount,
                CommentCount = newPost.CommentCount,
                ShareCount = newPost.ShareCount,
                CreatedAt = newPost.CreatedAt
            };
        }

        public async Task<string> DeletePost(int postId)
        {
            var user =_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId =   _userManage.Users.FirstOrDefault(x => x.Id == user);
            if (userId != null)
            {
                var UserPost = await _postRepository.DeletePost(postId);
                return "the post delete successfully";
            }
            throw new Exception("User not authenticated");
        }

        public async Task<PostDto> GetPostById(int postId)
        {
            var post = await _postRepository.GetPostById(postId);
            if (post == null)
                throw new Exception("Post not found");
            var showPost = new PostDto
            {
                Content = post.Content,
                Privacy = post.Privacy,
            };
            return showPost;
        }

        public async Task<string> UpdatePost(PostDto postDto,int postId)
        {
            var user = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(user == null)
            throw new Exception("the user not authoraized");
            var getPost= await _postRepository.GetPostById(postId);
            if(getPost.UserId!=user)
                throw new Exception("You are not allowed to edit this post");
            if (getPost != null)
            {
              getPost.Content = postDto.Content;
                getPost.Privacy = postDto.Privacy;
                getPost.UpdatedAt = DateTime.Now;
                await _postRepository.UpdatePost(getPost, postId);
                return "Post updated successfully";
            }
            throw new Exception("the post not updated");
        }
    }
}
