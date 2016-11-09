using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Prototype
{
    public interface IMessageConsumerConfig
    {
        string HostName { get; set; }
        string VirtualHost { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string QueueName { get; set; }
        string ExchangeName { get; set; }
        bool IsDurable { get; set; }
        int Port { get; set; }


    }
}
