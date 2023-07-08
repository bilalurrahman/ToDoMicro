using EventBus.Consumer.Annoucement;
using EventsBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal.Common.HttpContextHelper;
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
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextHelper, HttpContextHelper>();
            services.AddScoped<IRestClient, RestClient>();            
          return services;
        }
        public static IServiceCollection AddMessageQueues(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<NewTaskEmailCreationEventConsumer>();
                config.AddConsumer<DueDateNotificationEventConsumer>();
                config.AddConsumer<ReminderDateNotificationEventConsumer>();
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
                });

            });
            return services;
        }
    }
}
