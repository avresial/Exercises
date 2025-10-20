
using Confab.Modules.Conferences.Api;
using Confab.Shared.Infrastructure;
namespace Confab.Bootstrapper;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddInfrastructure();
        builder.Services.AddOpenApi();
        builder.Services.AddConferences();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        if (app.Environment.IsDevelopment())
            app.MapOpenApi();


        app.UseInfrastructure();
        app.MapGet("/", (context) => context.Response.WriteAsync("Confab Api!"));
        app.Run();
    }
}
