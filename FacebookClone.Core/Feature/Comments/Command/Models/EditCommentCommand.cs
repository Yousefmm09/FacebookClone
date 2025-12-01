using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Comments.Command.Models
{
    public class EditCommentCommand:IRequest<string>
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
