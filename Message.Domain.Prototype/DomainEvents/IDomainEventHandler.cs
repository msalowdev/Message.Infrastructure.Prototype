using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Message.Domain.Prototype.DomainEvents
{
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent: IDomainEvent
    {
        void Handle(TDomainEvent domainEvent);
    }
}
