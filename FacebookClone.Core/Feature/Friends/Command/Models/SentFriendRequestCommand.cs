using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FacebookClone.Data.Entities.FriendRequest;

namespace FacebookClone.Core.Feature.Friends.Command.Models
{
    public class SentFriendRequestCommand:IRequest<FriendRequestDto>
    {
        public string ReceiverId { get; set; }
        public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;
    }
}
