using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Data.Entities
{
    public class FriendRequest
    {
        public int Id { get; set; }
        [ForeignKey("SenderId")]
        public string SenderId { get; set; }
        [ForeignKey("ReceiverId")]
        public string ReceiverId { get; set; }    

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;

        public User Sender { get; set; }
        public User Receiver { get; set; }

        public enum FriendRequestStatus
        {
           Pending,
           Accepted,
           Rejected
        }
}
}
