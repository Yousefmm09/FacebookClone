using FacebookClone.Service.Abstract;
using FacebookClone.Service.Implementations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure
{
    public static class ModuleInfrastructureService
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationsService, AuthenticationsService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ILikeSerivce, LikeService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IAdminService, AdminService>();

            return services;
        }
    }
}
