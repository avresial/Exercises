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

app.Run();