﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repositories;

namespace MySpot.Infrastructure.DAL
{
    internal static class Extensions
    {
        public static IServiceCollection AddPostgress(this IServiceCollection services)
        {
            const string connectionString = "Host=localhost;Database=Myspot;Username=postgres;Password=";

            services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(connectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();

            return services;
        }
    }
}
