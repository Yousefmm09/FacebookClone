using FacebookClone.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacebookClone.Data.Entities
{
    public class OtpEmail
    {
        [Required]
        public int Id { get; set; }
        public required string UserId { get; set; }
        [ForeignKey("UserId")]
        public required User User { get; set; }
        public string OtpHash { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Attempts { get; set; } = 0;
        [Required]
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(5);
        [Required]
        public bool IsUsed { get; set; } = false;

    }
}