using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Events.Prototype;
using Message.Infrastructure.Prototype;

namespace Message.Console.Prototype
{
    public class DeleteAccountHandler : IMessageHandler<AccountDelete>
    {
        public ICollection<IMessage> PendingMessages { get; }
        public void Handle(AccountDelete message)
        {
            System.Console.WriteLine("test");
        }

        public void SendPendingMessages(string correlationId)
        {
            throw new NotImplementedException();
        }
    }
}
