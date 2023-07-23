using Authentication.Application.Behaviours;
using Authentication.Application.Contracts.Persistance;
using Authentication.Common.Helpers.JWTHelper;
using Authentication.Infrastructure.Persistance;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;
using FluentValidation;
using SharedKernal.GrpcServices;
using Localization.Application.Contracts.Persistance;
using Localization.Application.Contracts.Services;
using Localization.Grpc.Protos;
using Localization.Integration.Persistance;
using Localization.Integration.Services;
using Authentication.Application.Models;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SharedKernal.Common.HttpContextHelper;
using Microsoft.AspNetCore.Http;

namespace Authentication.API
{
    public static class InjectionExt
    {
        public static IServiceCollection AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JWTTokenSettings:SecretKey"))),
                     ValidateLifetime = true,
                     ValidateAudience = false,
                     ValidateIssuer = false,
                     ClockSkew = TimeSpan.Zero
                 };
             });

            return services;
        }



        public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddValidatorsFromAssembly(typeof(Application.Features.Login.LoginRequest).GetTypeInfo().Assembly);
            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddCustomMediatr(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = Assembly.Load(new AssemblyName("Authentication.Application"));
            services.AddMediatR(typeof(Startup).Assembly, domain);

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTTokenSettings>(configuration.GetSection("JWTTokenSettings"));
            services.Configure<GrpcSettings>(configuration.GetSection("GrpcSettings"));
            return services;
        }
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<IHttpContextHelper, HttpContextHelper>();
            services.AddScoped<IJWTCreateToken, JWTCreateToken>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            services.AddScoped<IDeviceQueryRepository, DeviceQueryRepository>();
            services.AddScoped<IUserCommandRepository, UserCommandRepository>();
            


            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }

        public static IServiceCollection AddSharedKernalDependencies(this IServiceCollection services)
        {
            services.AddScoped<LocalizationGrpcServices>();
            return services;
        }
        public static IServiceCollection AddLocalizationGrpcDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddGrpcClient<LocalizationProtoService.LocalizationProtoServiceClient>
                 (o => o.Address = new Uri(configuration["GrpcSettings:LocalizationUrl"]));

            services.AddSingleton<ILocalizationCacheServices, LocalizationCacheService>();
            services.AddSingleton<ILocalizationQueryRepository, LocalizationQueryRepository>();


            return services;
        }

        public static IServiceCollection AddCustomMapper(this IServiceCollection services)
        {
            var domain = Assembly.Load(new AssemblyName("Authentication.Application"));
            services.AddAutoMapper(typeof(Startup).Assembly, domain);
            return services;
        }


        public static void AddSupportedCultureServices(this IServiceCollection services)
        {
            var cultures = new List<CultureInfo>
            {
                new CultureInfo("ar-SA"),
                new CultureInfo("en")
            };
            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    options.DefaultRequestCulture = new RequestCulture("en", "ar-SA");
                    options.SupportedCultures = cultures;
                    options.SupportedUICultures = cultures;
                });
        }

        public static IServiceCollection AddHealthMonitoring(this IServiceCollection services, 
            IConfiguration configuration)
        {
            string grpcServerUrl = "http://host.docker.internal:5701";
                //(configuration["GrpcSettings:LocalizationUrl"]);

            services.AddSingleton<GrpcHealthCheck>(new GrpcHealthCheck(grpcServerUrl));
            services.AddHealthChecks()
               .AddSqlServer(configuration["DatabaseSettings:UserDBQueryConnection"],
                name: "SQL Server",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "db", "sql", "database" })
             .AddCheck<SeqHealthCheck>("Seq")
             .AddCheck<GrpcHealthCheck>("Grpc Localization");

            return services;
        }

    }
}
