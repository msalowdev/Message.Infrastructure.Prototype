using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Domain.Prototype.DomainEvents;

namespace Message.Console.Prototype.DomainHandlers
{
    public class AccountCreatedHandler : IDomainEventHandler<AccountCreated>
    {
        public void Handle(AccountCreated domainEvent)
        {
            System.Console.
        }
    }
}
