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
            return services;
        }
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJWTCreateToken, JWTCreateToken>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            services.AddScoped<IUserCommandRepository, UserCommandRepository>();


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));



            //services.AddScoped<ILocalizationGrpcServices, LocalizationGrpcServices>();

            return services;
        }
    }
}
