using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Comments.Queries.Models
{
    public class GetCommentByIdModel:IRequest<CommentDto>
    {
        public int Id { get; set; }
    }
}
