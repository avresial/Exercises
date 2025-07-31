using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Services;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Time;

namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("App");
            services.Configure<AppOptions>(section);

            services
                .AddSingleton<IClock, Clock>()
                .AddPostgress(configuration)
                .AddHostedService<DatabaseInitializer>()
                ;

            return services;
        }
    }
}
