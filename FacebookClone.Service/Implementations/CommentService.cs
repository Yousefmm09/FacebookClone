using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using FacebookClone.Infrastructure.Abstract;
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
using Microsoft.AspNetCore.SignalR;
using FacebookClone.Api.Hubs;
using FacebookClone.Infrastructure.Context;

namespace FacebookClone.Service.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly AppDb _db;
        public CommentService(ICommentRepository commentRepository,IHttpContextAccessor httpContextAccessor
            ,IPostRepository postRepository,UserManager<User> userManager,
            IHubContext<NotificationHub> notificationHub, AppDb db)
        {
            _commentRepository = commentRepository;
            _contextAccessor = httpContextAccessor;
            _postRepository = postRepository;
            _userManager = userManager;
            _notificationHub = notificationHub;
            _db = db;
        }
        public async Task<CommentDto> CreatComment(CreateCommentDto commentDto)
        {
            var userId=_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");
            var post=  await _postRepository.GetPostById(commentDto.PostId);
            if(post == null)
                throw new Exception("Not found post");
            var comment = new Comment()
            {
                UserId= userId,
                Content=commentDto.Content,
                ParentCommentId=commentDto.ParentCommentId,
                CreatedAt= DateTime.Now,
                PostId= commentDto.PostId,
                //Replies= commentDto.Replies,
            };
            post.CommentCount += 1;
           await _commentRepository.CreatComment(comment);

            // Notify post owner if different user
            if (!string.IsNullOrEmpty(post.UserId) && post.UserId != userId)
            {
                var notification = new Notifications
                {
                    UserId = post.UserId,
                    Type = "Comment",
                    Title = "New comment",
                    Message = "Someone commented on your post",
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
            return new CommentDto
            {
                Content=comment.Content,
               CreatedAt=comment.CreatedAt,
               PostId= comment.PostId,
               //Replies= comment.Replies,
            };
        }

        public async Task<string> EditComment(int commentId, CommentDto comment)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");

            var userComment = await _commentRepository.UserComment(userId);
            if (userComment == null)
                return $"not found comment by  {_userManager.Users.FirstOrDefault(x => x.Id == userId)?.UserName}";

            var commentUser = await _commentRepository.GetCommentUser(commentId);
            if (commentUser!=null&&commentUser.UserId==userId)
            {
                commentUser.Content = comment.Content;
                commentUser.CreatedAt = DateTime.Now;
               await _commentRepository.EditComment(commentId,commentUser);
                return "Comment updated successfully";
            }
            throw new Exception("The comment not updated");
        
        }

        public async Task<CommentDto> GetCommentById(int id)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");

            var comment= await _commentRepository.GetCommentUser(id);
            if (comment == null)
                throw new Exception("the comment not found");
            return new CommentDto
            {
                Content=comment.Content,
               CreatedAt=comment.CreatedAt,
               //Replies= comment.Replies,
            };
        }

        public async Task<IEnumerable<CommentDto>> GetPostComments(int postId)
        {
            var comments = await _commentRepository.GetPostComments(postId);
            if (comments == null)
                throw new Exception("the comment not found");
            return comments.Select(comment => new CommentDto
            {
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                //Replies= comment.Replies,
            });
        }

        public async Task<Comment?> GetUserComment(string userId, int postId)
        {
            var comment= _commentRepository.GetUserComment(userId, postId);
            return await comment;
        }

        public async Task<string> RemoveComment(int id)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User not authenticated");
            //check user is make this comment
            var userComment=await _commentRepository.UserComment(userId);
            if (userComment == null)
                return $"not found comment by  {_userManager.Users.FirstOrDefault(x => x.Id == userId)?.UserName}";
            var comment = await _commentRepository.GetCommentUser(id);
            if (comment == null)
                throw new Exception("Comment not found");

            await _commentRepository.RemoveComment(comment);
            return "Comment removed successfully";
        }
    }
}
