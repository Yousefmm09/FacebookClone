using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Posts.DTOs
{
    public class PostDto
    {
        public int PostId {  get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? Content { get; set; }
        public int? ParentPostId { get; set; } // reference to parent, not object

        public string Privacy { get; set; } = "Public"; // Public, Friends, OnlyMe
        public int LikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int ShareCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string UserName { get;  set; }
    }
}
