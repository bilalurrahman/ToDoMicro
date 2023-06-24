using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
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
            services.AddScoped<ITaskJob, TasksJob>();
            services.AddScoped<ITasksContext, TasksContext>();
            services.AddScoped<ITasksQueryRepository, TasksQueryRepository>();
            services.Configure<NoSqlDataBaseSettings>(Configuration.GetSection("NoSqlDatabaseSettings"));
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("TasksJobConnection")));
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            RecurringJob.AddOrUpdate<ITaskJob>("DueDateCheck",x => x.DueDateCheck(),Cron.Minutely);
        }
        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context) => true;
        }
    }
}
