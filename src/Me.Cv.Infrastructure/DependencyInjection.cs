using Me.Cv.Domain.Interfaces;
using Me.Cv.Infrastructure.Emails;
using Me.Cv.Infrastructure.ReCaptchas;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Me.Cv.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        //if (emailSettings != null)
        //{
        //    services.AddSingleton(emailSettings);
        //}
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailSettings>>().Value);

        //var reCaptchaSettings = configuration.GetSection("ReCaptchaSettings").Get<ReCaptchaSettings>();
        //if (reCaptchaSettings != null)
        //{
        //    services.AddSingleton(reCaptchaSettings);
        //}
        services.Configure<ReCaptchaSettings>(configuration.GetSection("ReCaptchaSettings"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<ReCaptchaSettings>>().Value);

        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IReCaptchaService, ReCaptchaService>();

        return services;
    }
}
