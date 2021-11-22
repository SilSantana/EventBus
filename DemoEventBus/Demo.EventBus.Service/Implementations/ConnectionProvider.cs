using Demo.EventBus.Service.Contracts;
using RabbitMQ.Client;
using System;

namespace Demo.EventBus.Service.Implementations
{
    public class ConnectionProvider : IConnectionProvider
    {

        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private bool _disposed;


        public ConnectionProvider(string username, string password, string virtualHost, string host)
        {
            _factory = new ConnectionFactory
            {
                UserName = username,
                Password = password,
                VirtualHost = virtualHost,
                HostName = host,
                AutomaticRecoveryEnabled = true,
                RequestedHeartbeat = TimeSpan.FromSeconds(30)
            };

            try
            {
                _connection = _factory.CreateConnection();
            }
            catch (Exception ex)
            {
                _connection = null;
                Console.WriteLine("Could not connected in RabbitMq error: " + ex.Message, ex);
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _connection?.Close();

            _disposed = true;
        }

        public IConnection GetConnection()
        {
            return _connection;
        }
    }
}
