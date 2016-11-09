using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Events.Prototype;
using Message.Implementation.Prototype;
using Message.Infrastructure.Prototype;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;

namespace Message.Console.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new RabbitConsumerConfig()
            {
                ExchangeBindings = new List<ExchangeBinding>()
                {
                    new ExchangeBinding
                    {
                        RoutingKey = "billing.account.create",
                        ExchangeName = "Billing"
                        
                    },
                    new ExchangeBinding
                    {
                        RoutingKey = "billing.account.delete",
                        ExchangeName = "Billing"

                    },
                    new ExchangeBinding
                    {
                        RoutingKey = "policy.term.created",
                        ExchangeName = "Policy"
                    }
                },
                QueueName = "PrototypeQueue",
                HostName = "10.0.0.190",
                UserName = "DemoUser",
                Password = "test123",
                Port = 5672
            };

            var account = new CreateAccount
            {
                AccountName = "Account Name",
                Amount = 10.20m,
                CorrelationId = "1234"
            };

            var deleteAccount = new AccountDelete()
            {
                CorrelationId = "1234",
                AccountId = 2
            };

            var stringValue = JsonConvert.SerializeObject(deleteAccount);

            var factory = new MessageFactory(MessageMap.GetMessageMap());

            UnityContainer container = new UnityContainer();

            container.RegisterType<IMessageHandler<CreateAccount>, CreateAccountHandler>();

            var handler = new CreateAccountHandler();
            var badHandler = new DeleteAccountHandler();
            var dispatcher = new MessageDispatcher();

            dispatcher.RegisterHandler(handler);
            dispatcher.RegisterHandler(badHandler);

            dispatcher.Register<CreateAccount>((i) =>
            {
                System.Console.WriteLine($"From Action account Name {i.AccountName}");
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
