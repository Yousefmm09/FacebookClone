using FacebookClone.Core.Feature.Posts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Posts.Command.Models
{
    public class CreatPostCommand:IRequest<PostDto>
    {
        public string? Content { get; set; }
        public int? ParentPostId { get; set; }
        public string Privacy { get; set; } = "Public";
    }
}
