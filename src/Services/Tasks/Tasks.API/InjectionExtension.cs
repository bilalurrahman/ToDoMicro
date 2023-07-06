using EventsBus.Messages.Common;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedKernal.Common.HttpContextHelper;
using SharedKernal.GrpcServices;
using Localization.Application.Contracts.Persistance;
using Localization.Application.Contracts.Services;
using Localization.Grpc.Protos;
using Localization.Integration.Persistance;
using Localization.Integration.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Tasks.Application.BackgroundJobs.TasksJobs;
using Tasks.Application.Contracts;
using Tasks.Application.Contracts.Context;
using Tasks.Application.MessageConsumer;
using Tasks.Application.Models;
using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Persistance;

namespace Tasks.API
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
            var domain = Assembly.Load(new AssemblyName("Tasks.Application"));
            services.AddMediatR(typeof(Startup).Assembly, domain);
            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {

            services.AddScoped<IHttpContextHelper, HttpContextHelper>();
            services.AddScoped<ITasksCommandsRepository, TasksCommandRepository>();
            services.AddScoped<ITasksQueryRepository, TasksQueryRepository>();
            services.AddScoped<ITasksContext, TasksContext>();
            services.AddScoped<ITaskJob, TasksJob>();
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasks.API", Version = "v1" });
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

        public static void UseSwaggerMiddelware(this IApplicationBuilder app, string version)
        {
            app.UseSwagger(setupAction =>
            {
                //resolve missing version at swagger.json issue 
                setupAction.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks.API v1");
                setupAction.DocumentTitle = "Tasks Api";
                setupAction.DocExpansion(DocExpansion.List);
                setupAction.RoutePrefix = "swagger/tasks";
            });
        }


        public static IServiceCollection AddCustomCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheDbSettings:ConnectionString");
            });
            return services;
        }
        public static IServiceCollection AddCustomMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = Assembly.Load(new AssemblyName("Tasks.Application"));
            services.AddAutoMapper(typeof(Startup).Assembly, domain);
            return services;
        }
        public static IServiceCollection AddCustomMessagingQueue(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<UpdateDueDateEventConsumer>();
                config.AddConsumer<UpdateReminderDateEventConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.DueDateUpdateQueue, c =>
                    {
                        c.ConfigureConsumer<UpdateDueDateEventConsumer>(ctx);
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.ReminderDateUpdateQueue, c =>
                    {
                        c.ConfigureConsumer<UpdateReminderDateEventConsumer>(ctx);
                    });

                });
                

            });
            return services;

        }
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NoSqlDataBaseSettings>(configuration.GetSection("NoSqlDatabaseSettings"));
            services.Configure<EventBusSettings>(configuration.GetSection("EventBusSettings"));
            services.Configure<CacheDbSettings>(configuration.GetSection("CacheDbSettings"));
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
    }

}
