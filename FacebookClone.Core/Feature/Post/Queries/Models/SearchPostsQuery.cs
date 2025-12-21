using FacebookClone.Service.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Queries.Models
{
    public record SearchPostsQuery(
    string Text,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<List<SearchPostDto>>;

}
