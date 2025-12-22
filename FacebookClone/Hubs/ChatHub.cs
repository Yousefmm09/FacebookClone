using FacebookClone.Data.Entities;
using FacebookClone.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Linq;

namespace FacebookClone.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly AppDb _db;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public ChatHub(AppDb db, IHubContext<NotificationHub> notificationHub)
        {
            _db = db;
            _notificationHub = notificationHub;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");
            }
            await base.OnConnectedAsync();
        }

        public async Task JoinGroup(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"group:{groupId}");
        }

        public async Task LeaveGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"group:{groupId}");
        }

        public async Task SendDirectMessage(string toUserId, string content)
        {
            var fromUserId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(fromUserId) || string.IsNullOrWhiteSpace(toUserId) || string.IsNullOrWhiteSpace(content))
                return;

            var msg = new Message
            {
                SenderId = fromUserId,
                ReceiverId = toUserId,
                Content = content,
                IsGroup = false,
                CreatedAt = DateTime.UtcNow
            };
            _db.Messages.Add(msg);
            await _db.SaveChangesAsync();

            // Optional: also send bell notification for new message
            var notification = new Notifications
            {
                UserId = toUserId,
                Type = "Message",
                Title = "New message",
                Message = content,
                RelatedId = fromUserId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };
            _db.Notifications.Add(notification);
            await _db.SaveChangesAsync();
            await _notificationHub.Clients.Group($"user:{toUserId}").SendAsync("Notify", new {
                id = notification.Id,
                type = notification.Type,
                title = notification.Title,
                message = notification.Message,
                relatedId = notification.RelatedId,
                createdAt = notification.CreatedAt,
                isRead = notification.IsRead
            });

            await Clients.Group($"user:{toUserId}").SendAsync("ReceiveDirectMessage", new
            {
                id = msg.Id,
                senderId = msg.SenderId,
                receiverId = msg.ReceiverId,
                content = msg.Content,
                createdAt = msg.CreatedAt
            });
        }

        public async Task SendGroupMessage(string groupId, string content)
        {
            var fromUserId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(fromUserId) || string.IsNullOrWhiteSpace(groupId) || string.IsNullOrWhiteSpace(content))
                return;

            var msg = new Message
            {
                SenderId = fromUserId,
                GroupId = groupId,
                Content = content,
                IsGroup = true,
                CreatedAt = DateTime.UtcNow
            };
            _db.Messages.Add(msg);
            await _db.SaveChangesAsync();

            await Clients.Group($"group:{groupId}").SendAsync("ReceiveGroupMessage", new
            {
                id = msg.Id,
                senderId = msg.SenderId,
                groupId = msg.GroupId,
                content = msg.Content,
                createdAt = msg.CreatedAt
            });
        }
    }
}
