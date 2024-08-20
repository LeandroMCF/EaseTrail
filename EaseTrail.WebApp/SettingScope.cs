using EaseTrail.WebApp.Services;
using EaseTrail.WebApp.Interfaces;

namespace EaseTrail.WebApp
{
    public static class SettingScope
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }
    }
}
