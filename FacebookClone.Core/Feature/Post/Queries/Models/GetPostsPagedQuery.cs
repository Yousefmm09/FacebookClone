using FacebookClone.Core.Feature.Posts.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Queries.Models
{
    public record GetPostsPagedQuery(int PageNumber = 1, int PageSize = 10)
     : IRequest<List<PostDto>>;
}
