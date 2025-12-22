using System;

namespace FacebookClone.Data.Entities
{
    public class Notifications
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // target user
        public string Type { get; set; } = string.Empty; // Like, Comment, FriendRequest, Message
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? RelatedId { get; set; } // e.g., postId/commentId/userId
        public string? MetadataJson { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; }
    }
}
