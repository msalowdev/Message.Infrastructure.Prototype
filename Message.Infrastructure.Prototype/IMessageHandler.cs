using System;
using System.Collections.Generic;

namespace Message.Infrastructure.Prototype
{
    
    public interface IMessageHandler<TMessage>
    {
        void Handle(TMessage message);
    }

}
