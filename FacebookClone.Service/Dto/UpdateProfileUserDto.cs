using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Dto
{
    public class UpdateProfileUserDto
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }
    }
}
