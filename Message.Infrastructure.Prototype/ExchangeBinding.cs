using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Prototype
{
    public class ExchangeBinding
    {
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
    }
}
