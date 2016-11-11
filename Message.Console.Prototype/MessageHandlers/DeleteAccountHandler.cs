using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Console.Prototype.MessageHandlers;
using Message.Events.Prototype;
using Message.Infrastructure.Prototype;

namespace Message.Console.Prototype
{
    public class DeleteAccountHandler : DomainMessageHandlerBase<AccountDelete>
    {
        protected override void HandleMessage(AccountDelete message)
        {
            throw new NotImplementedException();
        }
    }
}
