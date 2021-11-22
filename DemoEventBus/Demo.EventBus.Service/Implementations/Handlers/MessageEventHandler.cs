using Demo.EventBus.Service.Models.Message;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.EventBus.Service.Implementations.Handlers
{
    public class MessageEventHandler : INotificationHandler<MessageRequest> 
    {
        private readonly ILogger _logger;

        public MessageEventHandler(ILogger logger)
        {
            _logger = logger;
        }

    
        public Task Handle(MessageRequest notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler recebendo a mensagem de notificação...");
            // aqui poderia fazer algum processamento da mensagem 
            if (notification != null)
            {
                _logger.LogInformation("Id da mensagem: " + notification.Id);
                _logger.LogInformation("Descrição da mensagem: " + notification.Descripetion);
            }

           return Task.FromResult(true);
       }
    }
}
