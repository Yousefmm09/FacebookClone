using FacebookClone.Data.Entities;
using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Comments.Command.Models
{
    public class CreatCommentCommand:IRequest<CommentDto>
    {
        public int PostId { get; set; }
        public string Content { get; set; } = null!;
        public int? ParentCommentId { get; set; }
    }
}
