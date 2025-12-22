using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Dto
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? PostCount { get; set; }
        public int? FrinedCount { get; set; }
        public DateTime CreatedAt {  get; set; }
        public string Bio{ get; set;}
        public  string? ProfileImageUrl { get; set; }
    }
}
