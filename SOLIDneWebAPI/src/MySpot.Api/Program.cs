using MySpot.Application;
using MySpot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCore()
                .AddInfrastructure(builder.Configuration)
                .AddApplication()
                .AddControllers();

var app = builder.Build();
app.MapControllers();


app.Run();