using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Users.Command.Models
{
    public  class UserRegisterModel:IRequest<string>
    {
        public string UserName {  get; set; }= string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Bio {  get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile Image { get; set; }

    }
}
