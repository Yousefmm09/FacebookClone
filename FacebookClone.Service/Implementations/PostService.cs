using FacebookClone.Core.Feature.Posts.DTOs;
using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
        private readonly ILikeRepository _likeRepository;
        private readonly ILogger<PostService> _logger;

        public PostService(
            IPostRepository postRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            ILikeRepository likeRepository,
            ILogger<PostService> logger)
        {
            _postRepository=postRepository;
            _userManage=userManager;
            _contextAccessor=httpContextAccessor;
            _likeRepository=likeRepository;
            _logger=logger;
        }
        public async Task<PostDto> CreatPostAsync(PostDto postDto)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("Attempt to create post without authentication");
                throw new UnauthorizedAccessException("User not authenticated");
            }

            _logger.LogInformation("Creating new post for user: {UserId}", userId);
            var newPost = new Post
            {
                UserId = userId,
                Content = postDto.Content,
                CommentCount=postDto.CommentCount,
                ParentPostId=postDto.ParentPostId,
                Privacy=postDto.Privacy,
                CreatedAt=DateTime.UtcNow,
            };
            await _postRepository.CreatPostAsync(newPost);
            var likcount = await _postRepository.LikeCount(userId,newPost.Id);
            _logger.LogInformation("Post created successfully. PostId: {PostId}, UserId: {UserId}", newPost.Id, userId);
            return new PostDto
            {
                PostId=newPost.Id,
                UserId = newPost.UserId,
                UserName = _userManage.Users.FirstOrDefault(u => u.Id == userId)?.UserName ?? "Unknown",
                Content = newPost.Content,
                ParentPostId = newPost.ParentPostId,
                Privacy = newPost.Privacy,
                LikeCount = likcount,
                CommentCount = newPost.CommentCount,
                ShareCount = newPost.ShareCount,
                CreatedAt = newPost.CreatedAt
            };
        }

        public async Task<string> DeletePost(int postId)
        {
            var user =_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogInformation("Attempting to delete post. PostId: {PostId}, UserId: {UserId}", postId, user);
            var post= await _postRepository.GetPostById(postId);
            var userId =   _userManage.Users.FirstOrDefault(x => x.Id == user);
            if (userId != null)
            {
                var UserPost = await _postRepository.DeletePost(postId);
                _logger.LogInformation("Post deleted successfully. PostId: {PostId}, UserId: {UserId}", postId, user);
                return "the post delete successfully";
            }
            _logger.LogWarning("Attempt to delete post without authentication. PostId: {PostId}", postId);
            throw new UnauthorizedAccessException("User not authenticated");
        }

        public async Task<PostDto> GetPostById(int postId)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _logger.LogDebug("Fetching post by ID. PostId: {PostId}, UserId: {UserId}", postId, userId);
            var post = await _postRepository.GetPostById(postId);
            if (post == null)
            {
                _logger.LogWarning("Post not found. PostId: {PostId}", postId);
                throw new KeyNotFoundException($"Post with ID {postId} not found");
            }
            var likcount = await _postRepository.LikeCount(userId,postId);
            var showPost = new PostDto
            {
                Content = post.Content,
                Privacy = post.Privacy,
                LikeCount = likcount,
            };
            _logger.LogDebug("Post retrieved successfully. PostId: {PostId}", postId);
            return showPost;
        }

        public async Task<PostShareDto> SharePost(int PostId)
        {
            var user = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (user == null)
            {
                _logger.LogWarning("Attempt to share post without authentication. PostId: {PostId}", PostId);
                throw new UnauthorizedAccessException("User not authorized");
            }

            _logger.LogInformation("Attempting to share post. PostId: {PostId}, UserId: {UserId}", PostId, user);
            var getPost = await _postRepository.GetPostById(PostId);
            if (getPost == null)
            {
                _logger.LogWarning("Post not found for sharing. PostId: {PostId}", PostId);
                throw new KeyNotFoundException("Post not found");
            }

            var existingShare = await _postRepository.GetPostShare(PostId, user);
            if (existingShare != null)
            {
                _logger.LogWarning("User already shared this post. PostId: {PostId}, UserId: {UserId}", PostId, user);
                throw new InvalidOperationException("You have already shared this post");
            }

            var share = new PostsShare
            {
                UserId = user,
                PostId = getPost.Id,
                CreatedAt = DateTime.Now,
            };
           
            getPost.ShareCount += 1;
            await _postRepository.SharePost(share);
            _logger.LogInformation("Post shared successfully. PostId: {PostId}, UserId: {UserId}", PostId, user);
            return new PostShareDto
            {
                Id = share.Id,
                PostId = getPost.Id,
                UserId = share.UserId,
                CreatedAt = share.CreatedAt,
            };
        }

        public async Task<string> UpdatePost(PostDto postDto,int postId)
        {
            var user = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(user == null)
            {
                _logger.LogWarning("Attempt to update post without authentication. PostId: {PostId}", postId);
                throw new UnauthorizedAccessException("User not authorized");
            }

            _logger.LogInformation("Attempting to update post. PostId: {PostId}, UserId: {UserId}", postId, user);
            var getPost= await _postRepository.GetPostById(postId);
            
            if(getPost == null)
            {
                _logger.LogWarning("Post not found for update. PostId: {PostId}", postId);
                throw new KeyNotFoundException("Post not found");
            }

            if(getPost.UserId!=user)
            {
                _logger.LogWarning("User not authorized to edit post. PostId: {PostId}, UserId: {UserId}, PostOwnerId: {PostOwnerId}", 
                    postId, user, getPost.UserId);
                throw new UnauthorizedAccessException("You are not allowed to edit this post");
            }

            getPost.Content = postDto.Content;
            getPost.Privacy = postDto.Privacy;
            getPost.UpdatedAt = DateTime.Now;
            await _postRepository.UpdatePost(getPost, postId);
            _logger.LogInformation("Post updated successfully. PostId: {PostId}, UserId: {UserId}", postId, user);
            return "Post updated successfully";
        }
    }
}
