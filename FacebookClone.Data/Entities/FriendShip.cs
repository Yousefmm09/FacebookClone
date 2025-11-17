using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities
{
    
        public class Friendship
        {
            public int Id { get; set; }

            [ForeignKey("User")]
            public string UserId { get; set; }
            public User User { get; set; }

            [ForeignKey("Friend")]
            public string FriendId { get; set; }
            public User Friend { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
}
