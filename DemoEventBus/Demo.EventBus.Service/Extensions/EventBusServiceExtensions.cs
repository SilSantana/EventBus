using Demo.EventBus.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Demo.EventBus.Service.Contracts;
using Demo.EventBus.Service.Implementations;
using System.Reflection;
using Demo.EventBus.Service.Implementations.Handlers;
using Demo.EventBus.Service.Models.Message;
using Demo.EntitiesDto.Resources.Constants;
using MediatR;

namespace Demo.EventBus.Service.Extensions
{
    public static class EventBusServiceExtensions
    {
        private static IConfiguration _configuration;

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;
            var host = _configuration["RabbitMQ:Host"];
            var virtualHost = _configuration["RabbitMQ:VirtualHost"];
            var username = _configuration["RabbitMQ:Username"];
            var password = _configuration["RabbitMQ:Password"];

            //services.AddSingleton<IConnectionProvider>(new ConnectionProvider(username, password, virtualHost, host));

            //services.AddHostedService(provider => new MessageEventSubscriber<MessageRequest>(
            //    provider.GetService<IConnectionProvider>(),
            //    EventBusConstants.EXCHANGE,
            //    EventBusConstants.BOOKING_QUEUE_NAME,
            //    EventBusConstants.ROUTING_KEY,
            //    ExchangeType.Topic,
            //    provider,
            //    provider.GetService<ILogger<MessageEventSubscriber<MessageRequest>>>()
            //    ));


            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;

        }

    }
}
