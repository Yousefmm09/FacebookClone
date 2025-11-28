using FacebookClone.Core.Feature.Posts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Queries.Models
{
    public class GetPosByIdQuery:IRequest<PostDto>
    {
        public int PostId { get; set; }
    }
}
