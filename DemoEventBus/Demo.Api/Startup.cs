using Demo.Api.Extensions;
using Demo.EventBus.Service.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace Demo.Api
{
    public class Startup
    {

        public readonly IConfiguration _configuration;
        public readonly Serilog.ILogger _logger;


        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
              

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwagger();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHealthChecks();

            //services.AddEventBus(_configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory log)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            log.AddSerilog();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            if (_configuration.GetValue<bool>("Swagger.Settings:UIOn"))
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Event Bus API v1");
                    c.OAuthAppName("Demo Event Bus API - Swagger");
                });
            }


        }
    }
}
