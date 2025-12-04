using FacebookClone.Infrastructure.Abstract;
using FacebookClone.Infrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookClone.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection GetServices(this IServiceCollection services)
        {
            services.AddTransient<IRefreshTokenRepository,RefreshTokenRepository>();
            services.AddTransient<IPostRepository,PostRepository>();
            services.AddTransient<ILikeRepository,LikeRepository>();
            services.AddTransient<ICommentRepository,CommentRepository>();
            services.AddTransient<IFriendsRepository,FriendRepository>();
            services.AddTransient<IAdmin,AdminRepository>();
            return services;
        }
    }
}
