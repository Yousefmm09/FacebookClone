using FacebookClone.Core.Feature.Post.Queries.Models;
using FacebookClone.Infrastructure.Context;
using FacebookClone.Service.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Feature.Post.Queries.Handlers
{
    public class SearchPostsHandler
     : IRequestHandler<SearchPostsQuery, List<SearchPostDto>>
    {
        private readonly AppDb _context;

        public SearchPostsHandler(AppDb context)
        {
            _context = context;
        }

        public async Task<List<SearchPostDto>> Handle(SearchPostsQuery request, CancellationToken ct)
        {
            var skip = (request.PageNumber - 1) * request.PageSize;

            return await _context.Posts
                .AsNoTracking()
                .Where(p => p.Content.Contains(request.Text))
                .OrderByDescending(p => p.CreatedAt)
                .Skip(skip)
                .Take(request.PageSize)
                .Select(p => new SearchPostDto
                {
                    Content = p.Content,
                    LikeCount = p.LikeCount,
                    CommentCount = p.CommentCount

                })
                .ToListAsync(ct);
        }
    }

}
