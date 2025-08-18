using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Policies;
using MySpot.Core.Services;

namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IReservationPolicy, RegularEmployeeReservationPolicy>()
                    .AddSingleton<IReservationPolicy, ManagerEmployeeReservationPolicy>()
                    .AddSingleton<IReservationPolicy, BossEmployeeReservationPolicy>()
                    .AddSingleton<IParkingReservationService, ParkingReservationService>();



            return services;
        }
    }
}
