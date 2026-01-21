using Application;
using Infrastructure;
using Presentation;
using Presentation.Hubs;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseMiddlewares();
app.UseCors();
app.MapControllers();
app.MapHubs();

app.Run();