using FacebookClone.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FacebookClone.Api.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private readonly AppDb _db;
        public NotificationHub(AppDb db)
        {
            _db = db;
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

        // Optional: mark notifications as read
        public async Task MarkAsRead(int[] ids)
        {
            var list = _db.Notifications.Where(n => ids.Contains(n.Id));
            foreach (var n in list)
            {
                n.IsRead = true;
                n.ReadAt = DateTime.UtcNow;
            }
            await _db.SaveChangesAsync();
        }
    }
}
