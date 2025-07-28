using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL
{
    internal static class Extensions
    {
        private static string _sectionName = "Postgress";
        public static IServiceCollection AddPostgress(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<PostgressOptions>(_sectionName);

            services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(options.ConnectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var section = configuration.GetSection(sectionName);

            if (section == null) throw new ArgumentException($"Configuration section '{sectionName}' not found.");

            var options = new T();
            section.Bind(options);
            return options;
        }
    }
}
