using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Pomodoros.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Pomodoros.Infrastructure.Persistance.Query;
using Pomodoros.Application.Contracts.Persistance.Query;
using Pomodoros.Application.Contracts.Persistance.Command;
using Pomodoros.Infrastructure.Persistance.Command;
using Pomodoros.Application.Contracts.Context;
using Pomodoros.Infrastructure.Context;
using SharedKernal.Core.Interfaces.AppSettings;
using SharedKernal.Infrastructure.Persistance.AppSettings;
using SharedKernal;

namespace Pomodoros.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

           // services.AddCustomCache(Configuration);
            services.AddCustomMapper(Configuration);
            services.AddCustomAuth(Configuration);
            services.AddCustomMediatr();
            services.AddDependencies();
            services.AddControllers();
            services.AddSharedKernalDependencies();
            services.AddLocalizationGrpcDependencies(Configuration);
            services.AddCustomSwagger(Configuration);
            services.AddCustomMessagingQueue(Configuration);
            services.AddCustomConfiguration(Configuration);
            services.AddSupportedCultureServices();


            ContainerManager.Container = services.BuildServiceProvider();





            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pomodoros.API", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pomodoros.API v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
