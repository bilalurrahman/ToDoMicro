using Annoucement.Infrastructure.Integration;
using Announcement.Application.Contracts.Integration;
using Announcement.Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using MassTransit;
using Announcement.Application.Features.MessageConsumer;
using EventsBus.Messages.Common;

namespace Announcement.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Announcement.API", Version = "v1" });
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailIntegration, EmailIntegration>();

            //Add Queue Here for masstransit

            services.AddMassTransit(config =>
            {
                config.AddConsumer<NewTaskEmailCreationEventConsumer>();
                config.AddConsumer<DueDateNotificationEventConsumer>();
                config.AddConsumer<ReminderDateNotificationEventConsumer>();
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);

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

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Announcement.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
