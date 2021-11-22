using RabbitMQ.Client;
using System;

namespace Demo.EventBus.Service.Contracts
{
    public interface IConnectionProvider : IDisposable
    {
        IConnection GetConnection();
    }
}
