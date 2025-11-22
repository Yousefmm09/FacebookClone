using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Dto
{
    public class UserPostDto
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LikeCount { get; set; }
    }
}
