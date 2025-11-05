using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities.Identity
{
   public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshTokenSecret { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Expired { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User user { get; set; }
    }
}
