using System.Collections.Generic;

namespace Message.Infrastructure.Prototype
{
    public interface IMessageHandler
    {
       
    }

    public interface IMessageHandler<TMessage> : IMessageHandler where TMessage : IMessage
    {
        ICollection<IMessage> PendingMessages { get; }
        void Handle(TMessage message);
        //todo: Figure out how to send messages that does not involve calling from the outside
        void SendPendingMessages(string correlationId);
    }

}
