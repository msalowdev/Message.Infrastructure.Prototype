using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Events.Prototype;
using Message.Implementation.Prototype;
using Message.Infrastructure.Prototype;
using Newtonsoft.Json;

namespace Message.Console.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new RabbitConsumerConfig()
            {
                ExchangeName = "Billing",
                QueueName = "PrototypeQueue",
                HostName = "192.168.1.17",
                UserName = "demouser",
                Password = "test123"
            };

            var factory = new MessageFactory(MessageMap.GetMessageMap());

            var dispatcher = new MessageDispatcher(null);

            dispatcher.Register<CreateAccount>((i) =>
            {
                System.Console.WriteLine(i.AccountName);
            });

            var consumer = new RabbitConsumer(config, factory, dispatcher);

            string value = string.Empty;

            while (value != "exit")
            {
                value = System.Console.ReadLine();
            }

            consumer.Dispose();
        }
    }
}
