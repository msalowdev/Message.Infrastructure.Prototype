using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Events.Prototype;
using Message.Infrastructure.Prototype;

namespace Message.Console.Prototype
{
    public class CreateAccountHandler : IMessageHandler<CreateAccount>
    {
        public ICollection<IMessage> PendingMessages { get; }
        public void Handle(CreateAccount message)
        {
            System.Console.WriteLine($"Account Name from inside handler: {message.AccountName}");
        }

        public void SendPendingMessages(string correlationId)
        {
            throw new NotImplementedException();
        }
    }
}
