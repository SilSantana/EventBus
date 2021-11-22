using Demo.EventBus.Service.Contracts;
using Demo.EventBus.Service.Models.Message;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.EventBus.Service.Implementations.Handlers
{
    public class MessageEventSubscriber<T> : BackgroundService, IDisposable where T  : MessageHandlerModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MessageEventSubscriber<T>> _logger;
        private bool _disposed;
        private readonly IModel _model;
        private readonly string _queue;


        public MessageEventSubscriber(
            IConnectionProvider connectionProvider,
            string exchange,
            string queue,
            string routingKey,
            string exchangeType,
            IServiceProvider serviceProvider,
            ILogger<MessageEventSubscriber<T>> logger,
            ushort prefectchSize = 10
            )
        {

            try
            {
                var exchange1 = exchange;
                _queue = queue;
                _serviceProvider = serviceProvider;
                _logger = logger;
                _model = connectionProvider.GetConnection().CreateModel();
                _model.ExchangeDeclare(exchange1, exchangeType, durable: true);
                _model.QueueDeclare(
                    _queue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                _model.QueueBind(_queue, exchange1, routingKey);
                _model.BasicQos(0, prefectchSize, false);

                _logger.LogInformation("Connected successfully in RabbitMQ started at: {time} ", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error to connect in RabbitMQ: " + ex.Message, ex);
                _model = null;
            }

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                stoppingToken.Register(() => _logger.LogInformation("Subscriber stopped at {time}", DateTimeOffset.Now));
                if(_model != null)
                {
                    var consumer = new EventingBasicConsumer(_model);
                    consumer.Received += (sender, e) =>
                    {
                        var body = e.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var messageObj = JsonConvert.DeserializeObject<T>(message);

                        _ = CallBack(messageObj, e).ContinueWith(task =>
                        {
                            if (!task.IsCompletedSuccessfully)                            
                                _logger.LogError(task.Exception, "Message {deliveryTag} handling error for {routingKey} at {time}", e.DeliveryTag, e.RoutingKey, DateTimeOffset.Now);
                           

                            _model.BasicAck(e.DeliveryTag, false);
                            return;
                        }, stoppingToken);
                    };

                    _model.BasicConsume(_queue, false, consumer);
                }            
            }, stoppingToken);
        }


        private async Task CallBack(T message, BasicDeliverEventArgs headers)
        {
            _logger.LogInformation("New message {deliveryTag} received  for {routingKey} at: {time} ", headers.DeliveryTag, headers.RoutingKey, DateTimeOffset.Now);

            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            if (mediator != null) await mediator.Publish(message);
        }

        public override void Dispose()
        {
            if (_disposed)
                return;

            _model?.Close();
            _disposed = true;
            base.Dispose(); 
        }

    }
}
