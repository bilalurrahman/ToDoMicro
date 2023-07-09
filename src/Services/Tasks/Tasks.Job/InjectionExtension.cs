using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernal.Common.HttpContextHelper;
using System.Reflection;
using Tasks.Application.BackgroundJobs.TasksJobs;
using Tasks.Application.Contracts;
using Tasks.Application.Contracts.Context;
using Tasks.Application.Models;
using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Persistance;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Hangfire.Dashboard;

namespace Tasks.Job
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
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpContextHelper, HttpContextHelper>();
            services.AddScoped<ITaskJob, TasksJob>();
            services.AddScoped<ITasksContext, TasksContext>();
            services.AddScoped<ITasksQueryRepository, TasksQueryRepository>();
            services.AddScoped<ITasksCommandsRepository, TasksCommandRepository>();
            return services;
        }

        

        public static IServiceCollection AddCustomCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheDbSettings:ConnectionString");
            });
            return services;
        }
        public static IServiceCollection AddCustomMapper(this IServiceCollection services)
        {
            var domain = Assembly.Load(new AssemblyName("Tasks.Application"));
            services.AddAutoMapper(typeof(Startup).Assembly, domain);
            return services;
        }
        public static IServiceCollection AddCustomMessagingQueue(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {              
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);                    
                });                

            });
            return services;

        }
        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<NoSqlDataBaseSettings>(configuration.GetSection("NoSqlDatabaseSettings"));
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("TasksJobConnection")));
            return services;
        }

        public static IApplicationBuilder HangfireConfigure(this IApplicationBuilder app)
        {
            var options = new DashboardOptions()
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            };
            app.UseHangfireDashboard("/hangfire", options);
            TasksRecurringJobs();

            return app;
        }

        private static void TasksRecurringJobs()
        {
            RecurringJob.AddOrUpdate<ITaskJob>("DueDateCheck", x => x.DueDateCheck(), Cron.MinuteInterval(15));//call from the appsettings.
            RecurringJob.AddOrUpdate<ITaskJob>("ReminderDateCheck", x => x.ReminderCheck(), Cron.MinuteInterval(15));
        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context) => true;
        }


    }

}
