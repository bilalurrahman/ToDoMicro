using Microsoft.Extensions.DependencyInjection;
using SharedKernal.Core.Interfaces.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tawakkalna.Integration.RestClient;

namespace EventBus.Job
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
          services.AddScoped<IRestClient, RestClient>();            
          return services;
        }
    }
}
