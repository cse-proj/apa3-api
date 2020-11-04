using APA3.Drive.Mappers;
using APA3.Drive.Services;
using Microsoft.Extensions.DependencyInjection;

namespace APA3.Drive
{
    public static class DriveDependencyRegistry
    {
        public static IServiceCollection AddDriveService(this IServiceCollection services)
        {
            services.AddSingleton<FileMapper>()
                .AddSingleton<FolderMapper>()
                .AddSingleton<DriveMapper>()
                .AddSingleton<GoogleDriveService>();
                
            return services;
        }
    }
}