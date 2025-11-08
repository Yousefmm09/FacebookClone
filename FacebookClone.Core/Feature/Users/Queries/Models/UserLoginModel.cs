using FacebookClone.Service.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Queries.Models
{
    public class UserLoginModel:IRequest<AuthMessage>
    {
        public string Email {  get; set; }
        public string Password { get; set; }
        public  class Message
        {
           public string Statue { get; set; }
             public  string AccessToken {  get; set; }
            public string RefreshToken {  get; set; }

        }
    }
}
