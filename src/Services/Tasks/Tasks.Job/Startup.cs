using Hangfire;
using Hangfire.Dashboard;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedKernal.Common.HttpContextHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Tasks.Application.BackgroundJobs.TasksJobs;
using Tasks.Application.Contracts;
using Tasks.Application.Contracts.Context;
using Tasks.Application.Models;
using Tasks.Infrastructure.Context;
using Tasks.Infrastructure.Persistance;

namespace Tasks.Job
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            var domain = Assembly.Load(new AssemblyName("Tasks.Application"));
            services.AddAutoMapper(typeof(Startup).Assembly, domain);
            services.AddMediatR(typeof(Startup).Assembly, domain);

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                });
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("CacheDbSettings:ConnectionString");
            });


            services.AddScoped<IHttpContextHelper, HttpContextHelper>();
            services.AddScoped<ITaskJob, TasksJob>();
            services.AddScoped<ITasksContext, TasksContext>();
            services.AddScoped<ITasksQueryRepository, TasksQueryRepository>();
            services.AddScoped<ITasksCommandsRepository, TasksCommandRepository>();
            services.Configure<NoSqlDataBaseSettings>(Configuration.GetSection("NoSqlDatabaseSettings"));
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("TasksJobConnection")));
            services.AddHangfireServer();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            ,IRecurringJobManager recurringJobManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var options = new DashboardOptions()
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            };
            app.UseHangfireDashboard("/hangfire", options);
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Tasks Job");
                });
            });

            RecurringJob.AddOrUpdate<ITaskJob>("DueDateCheck",x => x.DueDateCheck(),Cron.MinuteInterval(15));
            RecurringJob.AddOrUpdate<ITaskJob>("ReminderDateCheck",x => x.ReminderCheck(),Cron.MinuteInterval(15));
        }
        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context) => true;
        }
    }
}
