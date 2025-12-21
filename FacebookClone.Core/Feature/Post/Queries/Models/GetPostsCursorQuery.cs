using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Queries.Models
{
    public record GetPostsCursorQuery(int LastId = 0, int PageSize = 5)
    : IRequest<List<PostCursorDto>>;

}
