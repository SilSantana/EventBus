using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Demo.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Demo Event Bus Booking API",
                        Version = "v1",
                        Description = "Demo Event Bus Booking API",
                        Contact = new OpenApiContact()
                        {
                            Name = "SilSantana",
                            Email = "https://github.com/SilSantana"
                        }
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }

    }
}
