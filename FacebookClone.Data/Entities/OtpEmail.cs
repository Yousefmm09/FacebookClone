using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities
{
    public class OtpEmail
    {
        [Required]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string OtpHash { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Attempts { get; set; } = 0;
        [Required]
        public DateTime ExpiresAt { get; set; }= DateTime.UtcNow.AddMinutes(5);
        [Required]
        public bool IsUsed { get; set; } = false;

    }
}