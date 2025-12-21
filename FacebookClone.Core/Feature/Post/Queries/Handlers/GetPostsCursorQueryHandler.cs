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
    public class GetPostsCursorHandler
    : IRequestHandler<GetPostsCursorQuery, List<PostCursorDto>>
    {
        private readonly AppDb _context;

        public GetPostsCursorHandler(AppDb context)
        {
            _context = context;
        }

        public async Task<List<PostCursorDto>> Handle(GetPostsCursorQuery request, CancellationToken ct)
        {
            return await _context.Posts
                .AsNoTracking()
                .Where(p => p.Id > request.LastId)
                .OrderBy(p => p.Id)
                .Take(request.PageSize)
                .Select(p => new PostCursorDto
                {
                    Id = p.Id,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    LikeCount = p.Likes.Count,
                    CommentCount = p.Comments.Count
                })
                .ToListAsync(ct);
        }
    }

}
