using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Domain.Prototype.DomainEvents
{
    public class AccountCreated : IDomainEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
