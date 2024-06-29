using Microsoft.OpenApi.Models;

namespace Me.Cv.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CustomCorsPolicy", policy =>
            {
                var allowedOriginsDictionary = new Dictionary<string, string>();
                configuration.GetSection("AllowedOrigins").Bind(allowedOriginsDictionary);
                var allowedOrigins = allowedOriginsDictionary.Values.ToArray();
                if (!environment.IsDevelopment())
                {
                    allowedOrigins = allowedOrigins.Where(ao => !ao.Contains("localhost")).ToArray();
                }
                policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
            });
        });
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CV API", Version = configuration["Version:Code"] });
        });
        services.AddExceptionHandler<CustomExceptionHandler>();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app, IConfiguration configuration)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var dotPrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                options.SwaggerEndpoint($"{dotPrefix}/swagger/v1/swagger.json", $"CV API {configuration["Version:Code"]} ({configuration["Version:Date"]})");
            });
        }

        app.UseExceptionHandler(options => { });

        app.UseHttpsRedirection();

        app.UseCors("CustomCorsPolicy");

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
