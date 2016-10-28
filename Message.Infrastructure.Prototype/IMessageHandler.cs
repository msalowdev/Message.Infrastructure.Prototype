

using System;
using System.Collections;
using System.Collections.Generic;

namespace Message.Infrastructure.Prototype
{
    public interface IMessageHandler<TMessage> where TMessage : IMessage
    {
        ICollection<IMessage> PendingMessages { get; }
        void Handle(TMessage message);
        void SendPendingMessages(string correlationId);
    }
}
