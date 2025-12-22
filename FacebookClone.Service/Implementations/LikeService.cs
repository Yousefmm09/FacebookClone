using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Service.Abstract;
using FacebookClone.Service.Dto;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using FacebookClone.Api.Hubs;
using FacebookClone.Infrastructure.Context;
using FacebookClone.Data.Entities;

public class LikeService : ILikeSerivce
{
    private readonly ILikeRepository _likeRepository;
    private readonly IHttpContextAccessor _context;
    private readonly IPostRepository _postRepository;
    private readonly IHubContext<NotificationHub> _notificationHub;
    private readonly AppDb _db;

    public LikeService(
        ILikeRepository likeRepository,
        IHttpContextAccessor httpContextAccessor,
        IPostRepository postRepository,
        IHubContext<NotificationHub> notificationHub,
        AppDb db)
    {
        _likeRepository = likeRepository;
        _context = httpContextAccessor;
        _postRepository = postRepository;
        _notificationHub = notificationHub;
        _db = db;
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

        // send notification to post owner
        if (!string.IsNullOrEmpty(post.UserId) && post.UserId != userId)
        {
            var notification = new Notifications
            {
                UserId = post.UserId,
                Type = "Like",
                Title = "New like",
                Message = "Someone liked your post",
                RelatedId = post.Id.ToString(),
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };
            _db.Notifications.Add(notification);
            await _db.SaveChangesAsync();
            await _notificationHub.Clients.Group($"user:{post.UserId}").SendAsync("Notify", new {
                id = notification.Id,
                type = notification.Type,
                title = notification.Title,
                message = notification.Message,
                relatedId = notification.RelatedId,
                createdAt = notification.CreatedAt,
                isRead = notification.IsRead
            });
        }

        return res;
    }
}
