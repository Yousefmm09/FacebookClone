using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; } = null!;

        public string? Content { get; set; }

        public int? ParentPostId { get; set; }
        public Post? ParentPost { get; set; }  // Self Reference

        public string Privacy { get; set; } = "Public"; // Public, Friends, OnlyMe
        public int LikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int ShareCount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<PostMedia> Media { get; set; } = new List<PostMedia>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
