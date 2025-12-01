using FacebookClone.Data.Entities;
<<<<<<< HEAD
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Infrastructure.Context;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
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
    public class LikeService : ILikeSerivce
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IHttpContextAccessor _context;
        private readonly IPostRepository _postRepository;
        private readonly UserManager<User> _userManager;
        public LikeService(ILikeRepository likeRepository,IHttpContextAccessor httpContextAccessor,IPostRepository postRepository,
            UserManager<User> userManager)
        {
            _likeRepository = likeRepository;
            _context = httpContextAccessor;
            _postRepository = postRepository;
            _userManager = userManager;
        }

        public Task<int> GetLikesCount(int postId)
        {
            return _likeRepository.GetLikesCount(postId);
        }

        public Task<string> RemoveLike(int id)
        {
            var userId = _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");
            var existingLike =  _likeRepository.GetUserLike(userId, id);
            if (existingLike == null)
                throw new Exception("Like not found");
            return _likeRepository.RemoveLike(existingLike.Result);
        }

        public async Task<string> SetLike(LikeDto like)
        {
            var userId = _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");
           
            var post =  await _postRepository.GetPostById(like.postId);
            if (post == null)
                throw new Exception("the post not found");
            var existingLike = await _likeRepository.GetUserLike(userId, like.postId);
            if (existingLike != null)
            {
                var removeLike = _likeRepository.RemoveLike(existingLike);
                post.LikeCount -= 1;
               await _postRepository.Update(post);
                return "Like removed successfully";
            }
            var setLike = new Like
            {
                UserId=userId,
                PostId=like.postId,
                CreatedAt=DateTime.Now,
            };
            var res = await _likeRepository.SetLike(setLike);
            post.LikeCount += 1;
            await _postRepository.Update(post);
            return res;
        }
=======
using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class LikeService : ILikeSerivce
{
    private readonly ILikeRepository _likeRepository;
    private readonly IHttpContextAccessor _context;
    private readonly IPostRepository _postRepository;

    public LikeService(
        ILikeRepository likeRepository,
        IHttpContextAccessor httpContextAccessor,
        IPostRepository postRepository)
    {
        _likeRepository = likeRepository;
        _context = httpContextAccessor;
        _postRepository = postRepository;
    }

    public async Task<string> SetLike(LikeDto like)
    {
        var userId = _context.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new Exception("User not authenticated");

        var post = await _postRepository.GetPostById(like.postId);
        if (post == null)
            throw new Exception("Post not found");

        var existingLike = await _likeRepository.GetUserLike(userId, like.postId);

        if (existingLike != null)
        {
            await _likeRepository.RemoveLike(existingLike);

            post.LikeCount -= 1;
            await _postRepository.Update(post);

            return "Like removed successfully";
        }

        var newLike = new Like
        {
            UserId = userId,
            PostId = like.postId,
            CreatedAt = DateTime.UtcNow
        };

        var res = await _likeRepository.SetLike(newLike);

        post.LikeCount += 1;
        await _postRepository.Update(post);

        return res;
>>>>>>> Comment
    }
}
