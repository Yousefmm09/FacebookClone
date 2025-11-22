using FacebookClone.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Dto
{
    public class ProfileUserDto
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }
        public int Likes { get; set; }
        public int Friends { get; set; }
        public List<UserPostDto> Posts { get; set; }
    }
}
