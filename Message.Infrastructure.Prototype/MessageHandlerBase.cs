using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Message.Infrastructure.Prototype
{
    public abstract class MessageHandlerBase<TMessageType> : IMessageHandler<TMessageType> where TMessageType : IMessage
    {

        public void Handle(TMessageType message)
        {
            HandleMessageBegin(message);
            HandleMessage(message);
            HandleMessageEnd(message);
        }

        protected abstract void HandleMessageBegin(TMessageType message);
        protected abstract void HandleMessage(TMessageType message);
        protected abstract void HandleMessageEnd(TMessageType message);
    }
}
