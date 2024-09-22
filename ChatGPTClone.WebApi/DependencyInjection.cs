using ChatGPTClone.Application.Common.Interfaces;
using ChatGPTClone.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;

namespace ChatGPTClone.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services.AddMemoryCache();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowCredentials()
                           .SetIsOriginAllowed((host) => true)
                           .AllowAnyHeader(); 
                });
        });

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

        services.AddSwaggerGen(setupAction =>
        {

            setupAction.SwaggerDoc(
                "v1",
                new OpenApiInfo()
                {
                    Title = "ChatGPT Clone Web API",
                    Version = "1",
                    Description = "Through this API you can access MextFullStackSaaS App's details",
                    Contact = new OpenApiContact()
                    {
                        Email = "alper.tunga@yazilim.academy",
                        Name = "Alper Tunga",
                        Url = new Uri("https://yazilim.academy/")
                    },
                    
                    License = new OpenApiLicense()
                    {
                        Name = "© 2024 Yazılım Academy Tüm Hakları Saklıdır",
                        Url = new Uri("https://yazilim.academy/")
                    }
                });

            setupAction.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = $"Input your Bearer token in this format - Bearer token to access this API",
            });

            setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        }, new List<string>()
                    },
                });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var secretKey = configuration["JwtSettings:SecretKey"];

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException("JwtSettings:SecretKey is not set.");

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}