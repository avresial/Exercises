using Confab.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
//[assembly: InternalsVisibleTo("Confab.Services.Tickets.Core")]
[assembly: InternalsVisibleTo("Confab.Shared.Tests")]

namespace Confab.Shared.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var disabledModules = new List<string>();
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                {
                    continue;
                }

                if (!bool.Parse(value))
                {
                    disabledModules.Add(key.Split(":")[0]);
                }
            }
        }

        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                    manager.ApplicationParts.Remove(part);

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this WebApplication app)
    {
        //app.UseCors(CorsPolicy);
        //app.UseErrorHandling();
        //app.UseSwagger();
        //app.UseReDoc(reDoc =>
        //{
        //    reDoc.RoutePrefix = "docs";
        //    reDoc.SpecUrl("/swagger/v1/swagger.json");
        //    reDoc.DocumentTitle = "Confab API";
        //});
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

}
