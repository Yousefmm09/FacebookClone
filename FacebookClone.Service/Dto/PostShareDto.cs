using FacebookClone.Data.Entities;
using FacebookClone.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Service.Dto
{
    public class PostShareDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }

        public string UserId { get; set; }


    }
}
