using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities
{
    public class PostsShare
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]

        public Post Post { get; set; } = null!;
        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; } = null!;

    }
}
