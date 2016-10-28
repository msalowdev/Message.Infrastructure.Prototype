using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Infrastructure.Prototype;

namespace Message.Implementation.Prototype
{
    public class RabbitConsumer: IMessageConsumer
    {
        public IMessage GetNextMessage()
        {
            throw new NotImplementedException();
        }
    }
}
