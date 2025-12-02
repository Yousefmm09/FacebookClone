using FacebookClone.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Core.Middleware
{
    public class BannedUserCheck
    {
        private readonly RequestDelegate _delegate;
        private readonly IHttpContextAccessor _contextAccessor;
        public BannedUserCheck(RequestDelegate requestDelegate, IHttpContextAccessor contextAccessor)
        {
            _delegate = requestDelegate;
            _contextAccessor = contextAccessor;
        }
        public async Task Invoke(HttpContext context,AppDb db)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user=await db.Users.FindAsync(userId);
                if (user.IsBanned==true)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("You are banned");
                    return;
                }
            }
            await _delegate(context);
        }
    }
}
