using Confab.Modules.Conferences.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Modules.Conferences.Api;

internal static class Extensions
{
    public static IServiceCollection AddConferences(this IServiceCollection services)
    {
        //services.AddPostgres<ConferencesDbContext>();
        //services.AddSingleton<IHostDeletionPolicy, HostDeletionPolicy>();
        // services.AddSingleton<IHostRepository, InMemoryHostRepository>();
        //services.AddScoped<IHostRepository, HostRepository>();
        //services.AddScoped<IHostService, HostService>();

        //services.AddSingleton<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();
        // services.AddSingleton<IConferenceRepository, InMemoryConferenceRepository>();
        //services.AddScoped<IConferenceRepository, ConferenceRepository>();
        //services.AddScoped<IConferenceService, ConferenceService>();
        services.AddCore();
        return services;
    }
}
