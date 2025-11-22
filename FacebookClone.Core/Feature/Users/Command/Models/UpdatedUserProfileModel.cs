using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Command.Models
{
    public class UpdatedUserProfileModel:IRequest<string>
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
