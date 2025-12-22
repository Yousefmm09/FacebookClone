using System;

namespace FacebookClone.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string? ReceiverId { get; set; } // null for group messages
        public string? GroupId { get; set; } // optional simple group id
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; }
        public bool IsGroup { get; set; }
    }
}
