using EaseTrail.WebApp.Services;
using EaseTrail.WebApp.Interfaces;

namespace EaseTrail.WebApp
{
    public static class SettingScope
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUtilsContext, UtilsContext>();

            services.AddScoped<IUserContext, UserContext>();
            services.AddScoped<IWorkSpaceContext, WorkSpaceContext>();

            return services;
        }
    }
}
