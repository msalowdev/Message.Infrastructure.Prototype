using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Infrastructure.Prototype;

namespace Message.Implementation.Prototype
{
    public class RabbitConsumerConfig : IMessageConsumerConfig
    {
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
        public List<ExchangeBinding> ExchangeBindings { get; set; }
        public bool IsDurable { get; set; }
        public int Port { get; set; }

        public RabbitConsumerConfig()
        {
            ExchangeBindings = new List<ExchangeBinding>();
        }

    }

}
