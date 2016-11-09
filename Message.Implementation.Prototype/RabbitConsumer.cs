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
        private readonly IMessageDispatcher _messageDispatcher;

        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;




        public RabbitConsumer(IMessageConsumerConfig consumerConfig, IMessageFactory messageFactory, IMessageDispatcher messageDispatcher)
        {
            if (consumerConfig == null)
                throw new ArgumentNullException(nameof(consumerConfig));
            if (messageFactory == null)
                throw new ArgumentNullException(nameof(messageFactory));
            if (messageDispatcher == null)
                throw new ArgumentNullException(nameof(messageDispatcher));
            _consumerConfig = consumerConfig;
            _messageFactory = messageFactory;
            _messageDispatcher = messageDispatcher;
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
            _model = _connection.CreateModel();


            //Bind to queue
            _model.BasicQos(0, 1, false);

            var name = _model.QueueDeclare(_consumerConfig.QueueName, _consumerConfig.IsDurable);
            _model.QueueBind(_consumerConfig.QueueName, _consumerConfig.ExchangeName, "billing.account.create");


            EventingBasicConsumer consumer = new EventingBasicConsumer(_model);

            _model.BasicConsume(_consumerConfig.QueueName, false, consumer);

            consumer.Received += Consumer_Received;

        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            //Get message body
            var messageBody = Encoding.Default.GetString(e.Body);
            var routingKey = e.RoutingKey;

            var message = _messageFactory.FromEvent(routingKey, messageBody);

            if (message == null)
                logger.Error($"failed to convert message. Routing Key [{e.RoutingKey}], Message Body [{messageBody}]");
            else
                _messageDispatcher.Dispatch(message);
            _model.BasicAck(e.DeliveryTag, false);
        }

        public IMessage GetNextMessage()
        {
            throw new NotImplementedException();
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
