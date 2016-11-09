using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Infrastructure.Prototype;

namespace Message.Events.Prototype
{
    public class CreateAccount : IMessage 
    {
        public string CorrelationId { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
    }
}
