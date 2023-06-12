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


        public static IServiceCollection AddCustomMediatr(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = Assembly.Load(new AssemblyName("Authentication.Application"));
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly, domain);

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
            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
            services.AddTransient<IUserCommandRepository, UserCommandRepository>();

            return services;
        }
    }
}
