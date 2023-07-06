using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SharedKernal;
using SharedKernal.Middlewares.ExceptionHandlers;

namespace Tasks.API
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

            services.AddCustomCache(Configuration);
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            app.AddGlobalExceptionHandler();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasks.API v1"));

            app.UseRouting();
            app.UseSerilogRequestLogging();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
