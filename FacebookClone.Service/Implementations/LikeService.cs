using FacebookClone.Data.Entities;
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
    }
}
