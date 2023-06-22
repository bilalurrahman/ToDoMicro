using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Tasks.Application.Contracts;
using Tasks.Application.Contracts.Context;
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
            services.AddScoped<ITasksCommandsRepository, TasksCommandRepository>();
            services.AddScoped<ITasksQueryRepository, TasksQueryRepository>();
            services.AddScoped<ITasksContext, TasksContext>();

            return services;
        }

        public static IServiceCollection AddSharedKernalDependencies(this IServiceCollection services)
        {
            return services;
        }
        public static IServiceCollection AddLocalizationGrpcDependencies(this IServiceCollection services)
        {
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


        public static IServiceCollection AddCustomCache(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });
            return services;
        }
    }

}
