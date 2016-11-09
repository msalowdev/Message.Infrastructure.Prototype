using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Infrastructure.Prototype;

namespace Message.Events.Prototype
{
    public class AccountDelete : IMessage
    {
        public string CorrelationId { get; set; }

        public int AccountId { get; set; }
    }
}
