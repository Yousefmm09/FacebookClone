using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Like.Queries.Models
{
    public class GetCountLikesQuery:IRequest<int>
    {
        public int PostId { get; set; }
    }
}
