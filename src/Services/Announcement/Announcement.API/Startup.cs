using Annoucement.Infrastructure.Integration;
using Announcement.Application.Contracts.Integration;
using Announcement.Application.Models;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.OpenApi.Models;
using System.Reflection;

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

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile("Resources/todo-app-fcm.json"),
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Announcement.API", Version = "v1" });
            });

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            var domain = Assembly.Load(new AssemblyName("Announcement.Application"));
            services.AddMediatR(typeof(Startup).Assembly, domain);

            services.AddTransient<IEmailIntegration, EmailIntegration>();


            services.AddScoped<IFireBaseIntegration, FireBaseIntegration>();
            


            services.AddHealthChecks();

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
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
