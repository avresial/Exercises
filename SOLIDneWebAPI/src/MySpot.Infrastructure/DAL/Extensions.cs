using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Decorators;
using MySpot.Infrastructure.DAL.Repositories;
using MySpot.Infrastructure.Logging.Decorators;

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
            services.AddScoped<IUserRepository, PostgresUserRepository>();
            services.AddScoped<IUnitOfWork, PostgresUnitOfWork>();

            services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return services;
        }


    }
}
