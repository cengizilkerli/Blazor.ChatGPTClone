using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.WebApi.Services;
using System.Globalization;

namespace ChatGPTClone.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUserServices, CurrentUserManager>();

        services.AddSingleton<IEnvironmentService, EnvironmentManager>(sp => new EnvironmentManager(environment.WebRootPath));

        services.AddLocalization(option => option.ResourcesPath = "Resources");

        services.Configure<RequestLocalizationOptions>(option => 
        {
            var defaultCulture = new CultureInfo("tr-TR");

            var supportedCultures = new List<CultureInfo> 
            {
                defaultCulture,
                new("en-GB")
            };

            option.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(defaultCulture);
            option.SupportedCultures = supportedCultures;
            option.SupportedUICultures = supportedCultures;
            option.ApplyCurrentCultureToResponseHeaders = true;
        });

        return services;
    }
}