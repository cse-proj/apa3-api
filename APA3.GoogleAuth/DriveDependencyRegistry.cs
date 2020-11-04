using APA3.GoogleAuth.Services;
using Microsoft.Extensions.DependencyInjection;

namespace APA3.Drive
{
    public static class DriveDependencyRegistry
    {
        public static IServiceCollection AddGoogleAuthService(this IServiceCollection services)
        {
            services.AddSingleton<GoogleServiceAccountOauthService>();
                
            return services;
        }
    }
}