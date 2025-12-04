using FacebookClone.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacebookClone.Service.Dto
{
    public class FriendshipDto
    {

        public string UserId { get; set; }
        
        public string FriendId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}