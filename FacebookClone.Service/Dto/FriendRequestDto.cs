using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Dto
{
    public class FriendRequestDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
       public string ReceiverUserName {  get; set; }

        public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;

        public enum FriendRequestStatus
        {
            Pending,
            Accepted,
            Rejected
        }
    }
}
