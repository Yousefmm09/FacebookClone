using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Friends.Command.Models
{
    public class RemoveFriendCommand:IRequest<string>
    {
        public string FriendId { get; set; }=string.Empty;
    }
}
