using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]

        public Post Post { get; set; } = null!;
        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public User user { get; set; } = null!;

        public string Content { get; set; } = null!;
        public int? ParentCommentId { get; set; }
        [ForeignKey("ParentCommentId")]

        public Comment? ParentComment { get; set; }

        public ICollection<Comment>? Replies { get; set; } = new List<Comment>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
