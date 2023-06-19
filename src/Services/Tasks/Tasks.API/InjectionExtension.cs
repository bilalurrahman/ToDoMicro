﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tasks.Application.Contracts;
using Tasks.Infrastructure.Persistance;

namespace Tasks.API
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddCustomMediatr(this IServiceCollection services)
        {
            var domain = Assembly.Load(new AssemblyName("Tasks.Application"));
            services.AddMediatR(typeof(Startup).Assembly, domain);
            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITasksCommandsRepository, TasksCommandRepository>();
           
            return services;
        }
    }
}
