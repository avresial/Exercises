using Confab.Shared.Abstractions.Time;
using Confab.Shared.Infrastructure.Api;
using Confab.Shared.Infrastructure.Exceptions;
using Confab.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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

        //services.AddCors(cors =>
        //{
        //    cors.AddPolicy(CorsPolicy, x =>
        //    {
        //        x.WithOrigins("*")
        //            .WithMethods("POST", "PUT", "DELETE")
        //            .WithHeaders("Content-Type", "Authorization");
        //    });
        //});
        //services.AddSwaggerGen(swagger =>
        //{
        //    swagger.CustomSchemaIds(x => x.FullName);
        //    swagger.SwaggerDoc("v1", new OpenApiInfo
        //    {
        //        Title = "Confab API",
        //        Version = "v1"
        //    });
        //});

        services.AddMemoryCache();
        //services.AddSingleton<IRequestStorage, RequestStorage>();
        //services.AddSingleton<IContextFactory, ContextFactory>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddTransient(sp => sp.GetRequiredService<IContextFactory>().Create());
        //services.AddModuleInfo(modules);
        //services.AddModuleRequests(assemblies);
        //services.AddAuth(modules);
        services.AddErrorHandling();
        //services.AddCommands(assemblies);
        //services.AddQueries(assemblies);
        //services.AddEvents(assemblies);
        //services.AddDomainEvents(assemblies);
        //services.AddMessaging();
        //services.AddPostgres();
        //services.AddTransactionalDecorators();
        services.AddSingleton<IClock, UtcClock>();
        //services.AddHostedService<AppInitializer>();
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
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        //services
        //    .AddConvey()
        //    .AddRabbitMq()
        //    .Build();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this WebApplication app)
    {
        //app.UseCors(CorsPolicy);
        app.UseErrorHandling();
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
