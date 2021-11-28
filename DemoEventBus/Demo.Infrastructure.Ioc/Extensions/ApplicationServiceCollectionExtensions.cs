using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using Demo.Infrastructure.Ioc.Handlers;
using Demo.Mock.Booking;
using Demo.Api.Repository.Contracts.Booking;
using Demo.Api.Repository.Implementations.Booking;
using Demo.Infrastructure.Ioc.Models;

namespace Demo.Infrastructure.Ioc.Extensions
{
    public static class ApplicationServiceCollectionExtensions
    {
        private static IConfiguration _configuration;
        private static DemoApiSettings _demoApiSettings = new DemoApiSettings();
        private static MockSettings _mockSettings = new MockSettings();

        public static void AddCustomizedServices(this IServiceCollection services, IConfiguration configuration)
        {

            _configuration = configuration;            
            services.AddServices();
            services.AddRepositories();           
            services.AddMocks();
            services.AddPolices();
            services.AddRefit();

        }

        private static void AddPolices(this IServiceCollection services)
        {
            // adicionar a injecção de dependencia dos serviços
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError().OrResult(m => m.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

            services.AddRefitClient<IBookingProviderRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(_demoApiSettings.BookingApi))
                .AddPolicyHandler(retryPolicy);
        }


        private static void AddServices(this IServiceCollection services)
        {
            // adicionar a injecção de dependencia dos serviços
            // services.AddScoped<Interface service, classe service>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            // adicionar a injecção de dependencia dos serviços
            services.AddScoped<IBookingRepository, BookingRepository>();
        }

        private static void AddMocks(this IServiceCollection services)
        {
            // adicionar a injecção de dependencia dos serviços
            services.AddTransient(provider => BookingMock.GenerateProviderRepository());
        }

        private static void AddRefit(this IServiceCollection services)
        {
            RefitSettings settings = new RefitSettings {
                ContentSerializer = new NewtonsoftJsonContentSerializer(
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
            };

            Console.WriteLine("Url api: " + _demoApiSettings.BookingApi);

            HttpClient apiHttpClient = new HttpClient(new BookingApiHttpClientHandler(settings))
            {
                BaseAddress = new Uri(_demoApiSettings.BookingApi)
            };

            if (!_mockSettings.Booking)
            {
                services.AddSingleton(provider => RestService.For<IBookingProviderRepository>(apiHttpClient));
            }
        }

    }
}
