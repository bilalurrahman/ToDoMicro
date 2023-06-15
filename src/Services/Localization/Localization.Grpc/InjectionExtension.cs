using Localization.Application.Contracts.Persistance;
using Localization.Application.Contracts.Services;
using Localization.Integration.Persistance;
using Localization.Integration.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Localization.Grpc
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
        {
            //var domain = Assembly.Load(new AssemblyName("Authentication.Application"));
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizationCacheServices, LocalizationCacheService>();
            services.AddScoped<ILocalizationQueryRepository, LocalizationQueryRepository>();

            return services;
        }
    }
}
