using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MySpot.Infrastructure.DAL
{
    internal static class Extensions
    {
        public static IServiceCollection AddPostgress(this IServiceCollection services)
        {
            const string connectionString = "Host=localhost;Database=Myspot;Username=postgres;Password=";

            services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(connectionString));

            return services;
        }
    }
}
