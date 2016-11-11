using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Infrastructure.Prototype;
using NLog;
using NLog.Fluent;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Message.Implementation.Prototype
{
    public class RabbitConsumer : IMessageConsumer, IDisposable
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IMessageConsumerConfig _consumerConfig;
        private readonly IMessageFactory _messageFactory;
        //private readonly IMessageDispatcher _messageDispatcher;
        private readonly IMessageBus _messageBus;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;




        public RabbitConsumer(IMessageConsumerConfig consumerConfig, IMessageFactory messageFactory, IMessageBus messageBus)//, IMessageDispatcher messageDispatcher)
        {
            if (consumerConfig == null)
                throw new ArgumentNullException(nameof(consumerConfig));
            if (messageFactory == null)
                throw new ArgumentNullException(nameof(messageFactory));
            if (messageBus == null)
                throw new ArgumentNullException(nameof(messageBus));
            _consumerConfig = consumerConfig;
            _messageFactory = messageFactory;
            _messageBus = messageBus;
            InitConsumer();
        }

        private void InitConsumer()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = _consumerConfig.HostName,
                UserName = _consumerConfig.UserName,
                Password = _consumerConfig.Password,
                Port = _consumerConfig.Port
            };

            if (!string.IsNullOrEmpty(_consumerConfig.VirtualHost))
            {
                _connectionFactory.VirtualHost = _consumerConfig.VirtualHost;
            }

            _connection = _connectionFactory.CreateConnection();
            ConfigureModel();
            ConfigureBindings();

            EventingBasicConsumer consumer = new EventingBasicConsumer(_model);

            _model.BasicConsume(_consumerConfig.QueueName, false, consumer);

            consumer.Received += Consumer_Received;

        }

        private void ConfigureModel()
        {
            _model = _connection.CreateModel();
            _model.BasicQos(0, 1, false);
            _model.QueueDeclare(_consumerConfig.QueueName, _consumerConfig.IsDurable);
        }

        private void ConfigureBindings()
        {
            foreach (var exchangeBinding in _consumerConfig.ExchangeBindings)
            {
                _model.QueueBind(_consumerConfig.QueueName, exchangeBinding.ExchangeName, exchangeBinding.RoutingKey);
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
           
            var messageBody = Encoding.Default.GetString(e.Body);
            var routingKey = e.RoutingKey;

            var message = _messageFactory.FromEvent(routingKey, messageBody);

            if (message == null)
                logger.Error($"failed to convert message. Routing Key [{e.RoutingKey}], Message Body [{messageBody}]");
            else
                _messageBus.Publish(message);
            _model.BasicAck(e.DeliveryTag, false);
        }

        public void Dispose()
        {
            _model?.Dispose();
            _connection?.Dispose();

            _connectionFactory = null;
            GC.SuppressFinalize(this);
        }
    }
}
