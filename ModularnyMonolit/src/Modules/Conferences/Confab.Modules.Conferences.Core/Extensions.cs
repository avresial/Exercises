using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        //services.AddPostgres<ConferencesDbContext>();
        services.AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>();
        services.AddSingleton<IHostRepository, InMemoryHostRepository>();
        //services.AddScoped<IHostRepository, HostRepository>();
        services.AddScoped<IHostService, HostService>();

        services.AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();
        services.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>();
        //services.AddScoped<IConferenceRepository, ConferenceRepository>();
        services.AddScoped<IConferenceService, ConferenceService>();

        return services;
    }
}
