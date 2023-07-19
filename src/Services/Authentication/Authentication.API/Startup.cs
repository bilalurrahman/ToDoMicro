

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.OpenApi.Models;
using SharedKernal;
using SharedKernal.GrpcServices;
using SharedKernal.Middlewares.ExceptionHandlers;
using Serilog;
using SharedKernal.Middlewares.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace Authentication.API
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

            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });
            services.AddHttpContextAccessor();
            services.AddCustomAuth(Configuration);
            services.AddCustomFluentValidation(Configuration);
            services.AddCustomMediatr(Configuration);
            services.AddCustomConfiguration(Configuration);
            services.AddDependencyInjection(Configuration);
            services.AddSharedKernalDependencies();
            services.AddLocalizationGrpcDependencies(Configuration);
            services.AddCustomMapper();
            services.AddSupportedCultureServices();

            services.AddHealthMonitoring(Configuration);




            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication.API", Version = "v1" });

            });
            ContainerManager.Container = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors("AllowOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication.API v1"));
            }
            //  app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.AddGlobalExceptionHandler();
            

            app.UseHttpsRedirection();

            app.UseRouting();
           

            app.UseSerilogRequestLogging(
                options=>
                {
                    options.EnrichDiagnosticContext = Enricher.HttpRequestEnricher;
                });

            app.UseAuthentication();
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
