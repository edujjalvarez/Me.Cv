using Me.Cv.Api;
using Me.Cv.Application;
using Me.Cv.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Environment, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseApiServices(builder.Configuration);

app.Run();
