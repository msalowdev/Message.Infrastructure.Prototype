using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Prototype
{
    public interface IMessageBus
    {
        void Subscribe<TMessageType>(Action<TMessageType> handler);
        void Unsubscribe<TMessageType>(Action<TMessageType> handler);
        void Publish(IMessage message);
    }
}
