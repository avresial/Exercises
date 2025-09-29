using Microsoft.Extensions.Options;
using MySpot.Api;
using MySpot.Application;
using MySpot.Infrastructure;
using MySpot.Infrastructure.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCore()
                .AddInfrastructure(builder.Configuration)
                .AddApplication()
                .AddControllers();

builder.UseSerilog();


var app = builder.Build();

app.UseInfrastructure();
app.MapGet("api", (IOptions<AppOptions> options) => Results.Ok(options.Value.Name));
app.UseUsersApi();
app.Run();