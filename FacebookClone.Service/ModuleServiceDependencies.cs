using FacebookClone.Service.Abstract;
using FacebookClone.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddTransient<IFriendService, FriendService>();
            services.AddTransient<IAdminService, AdminService>();

            return services;
        }
    }
}
