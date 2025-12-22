using FacebookClone.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace FacebookClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")] 
    public class NotificationsController : ControllerBase
    {
        private readonly AppDb _db;
        public NotificationsController(AppDb db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotifications([FromQuery] int take = 50)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var list = await _db.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(Math.Clamp(take, 1, 200))
                .Select(n => new {
                    n.Id,
                    n.Type,
                    n.Title,
                    n.Message,
                    n.RelatedId,
                    n.CreatedAt,
                    n.IsRead
                })
                .ToListAsync();

            return Ok(list);
        }

        public class MarkReadRequest { public int[] Ids { get; set; } = Array.Empty<int>(); }

        [HttpPost("mark-read")]
        public async Task<IActionResult> MarkRead([FromBody] MarkReadRequest req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();
            if (req == null || req.Ids.Length == 0) return BadRequest("No ids provided");

            var items = await _db.Notifications
                .Where(n => n.UserId == userId && req.Ids.Contains(n.Id))
                .ToListAsync();

            foreach (var n in items)
            {
                n.IsRead = true;
                n.ReadAt = DateTime.UtcNow;
            }
            await _db.SaveChangesAsync();
            return Ok(new { updated = items.Count });
        }

        [HttpPost("mark-all-read")]
        public async Task<IActionResult> MarkAllRead()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var items = await _db.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var n in items)
            {
                n.IsRead = true;
                n.ReadAt = DateTime.UtcNow;
            }
            await _db.SaveChangesAsync();
            return Ok(new { updated = items.Count });
        }
    }
}
