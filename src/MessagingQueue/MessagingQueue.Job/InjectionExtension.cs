using EventBus.Consumer.Annoucement;
using EventBus.Consumer.Tasks;
using EventsBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal.Common.HttpContextHelper;
using SharedKernal.Core.Interfaces.RestClient;
using System.Reflection;
using HealthChecks.UI.Client;
using AutoMapper;
using Tawakkalna.Integration.RestClient;
using RabbitMQ.Client;
using System;

namespace EventBus.Job
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextHelper, HttpContextHelper>();
            services.AddScoped<IRestClient, RestClient>();            
          return services;
        }
        public static IServiceCollection AddCustomMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var domain = Assembly.Load(new AssemblyName("EventBus.Core"));
            services.AddAutoMapper(typeof(Startup).Assembly, domain);
            return services;
        }
        public static IServiceCollection AddMessageQueues(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<NewTaskEmailCreationEventConsumer>();
                config.AddConsumer<DueDateNotificationEventConsumer>();
                config.AddConsumer<ReminderDateNotificationEventConsumer>();
                config.AddConsumer<UpdateDueDateEventConsumer>();
                config.AddConsumer<UpdateReminderDateEventConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                    
                    cfg.ReceiveEndpoint(EventBusConstants.NewTaskEmailCreationQueue, c =>
                    {
                        c.ConfigureConsumer<NewTaskEmailCreationEventConsumer>(ctx);
                    });

                    cfg.ReceiveEndpoint(EventBusConstants.DueDateNotificationQueue, c =>
                    {
                        c.ConfigureConsumer<DueDateNotificationEventConsumer>(ctx);
                    });

                    cfg.ReceiveEndpoint(EventBusConstants.ReminderDateNotificationQueue, c =>
                    {
                        c.ConfigureConsumer<ReminderDateNotificationEventConsumer>(ctx);
                    });
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
            //services.AddMassTransitHostedService();
            return services;
        }



        public static IServiceCollection AddHealthMonitoring(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connStr = configuration["EventBusSettings:HostAddress"].ToString();
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(connStr),
                AutomaticRecoveryEnabled = true
            };
            var connection = factory.CreateConnection();
            services.AddSingleton(connection)
                .AddHealthChecks()
               .AddRabbitMQ();
            return services;
        }
    }
}
