
using Localization.Application.Contracts.Persistance;
using Localization.Application.Contracts.Services;
using Localization.Integration.Persistance;
using Localization.Integration.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pomodoros.Application.Contracts.Context;
using Pomodoros.Application.Contracts.Persistance.Command;
using Pomodoros.Application.Contracts.Persistance.Query;
using Pomodoros.Application.Models;
using Pomodoros.Infrastructure.Context;
using Pomodoros.Infrastructure.Persistance.Command;
using Pomodoros.Infrastructure.Persistance.Query;
using SharedKernal.Core.Interfaces.AppSettings;
using SharedKernal.GrpcServices;
using SharedKernal.Infrastructure.Persistance.AppSettings;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Localization.Grpc.Protos;
using SharedKernal.CacheService;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Pomodoros.API
{
    public static class InjectionExtension
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



        public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
        {
            var domain = Assembly.Load(new AssemblyName("Pomodoros.Application"));
            services.AddMediatR(typeof(Startup).Assembly, domain);
            return services;
        }
        public static IServiceCollection AddCustomMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = Assembly.Load(new AssemblyName("Pomodoros.Application"));
            services.AddAutoMapper(typeof(Startup).Assembly, domain);
            return services;
        }
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {

            services.AddScoped<ICommandPomodorosRepository, CommandPomodorosRepository>();
            services.AddScoped<IQueryPomodorosRepository, QueryPomodorosRepository>();
            services.AddScoped<IPomodoroContext, PomodoroContext>();
            services.AddSingleton<IAppSettingsQueryRepository, AppSettingsQueryRepository>();
            services.AddSingleton<ICachedAppSettingServices, CachedAppSettingsService>();
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


        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pomodoros.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme()
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

            });



            return services;
        }

      

        public static IServiceCollection AddCustomCache(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = configuration.GetValue<string>("CacheDbSettings:ConnectionString");
            //});
            return services;
        }
       
        public static IServiceCollection AddCustomMessagingQueue(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMassTransit(config =>
            //{
            //    config.AddConsumer<UpdateDueDateEventConsumer>();

            //    config.UsingRabbitMq((ctx, cfg) =>
            //    {
            //        cfg.Host(configuration["EventBusSettings:HostAddress"]);

            //        cfg.ReceiveEndpoint(EventBusConstants.DueDateUpdateQueue, c =>
            //        {
            //            c.ConfigureConsumer<UpdateDueDateEventConsumer>(ctx);
            //        });

            //    });
                

            //});
            return services;

        }
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NoSqlDataBaseSettings>(configuration.GetSection("NoSqlDatabaseSettings"));
            
            services.Configure<JWTTokenSettings>(configuration.GetSection("JWTTokenSettings"));


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
                    options.DefaultRequestCulture = new RequestCulture("en");
                    options.SupportedCultures = cultures;
                    options.SupportedUICultures = cultures;
                });
        }

        public static IServiceCollection AddHealthMonitoring(this IServiceCollection services,
          IConfiguration configuration)
        {
            
               services.AddHealthChecks()
                    .AddMongoDb(configuration["NoSqlDatabaseSettings:ConnectionString"]
                    , "MongoDb Health", HealthStatus.Degraded)
                    .AddSqlServer(configuration["DatabaseSettings:AppSettingsDBConnection"],
                    name: "SQL Server",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { "db", "sql", "database" });

            return services;
        }

    }

}
