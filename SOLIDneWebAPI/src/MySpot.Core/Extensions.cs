﻿using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;

namespace MySpot.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services) 
        {

            return services;
        }
    }
}
