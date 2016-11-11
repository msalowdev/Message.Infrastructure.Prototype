using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Message.Domain.Prototype.DomainEvents;
using Message.Infrastructure.Prototype;

namespace Message.Console.Prototype.MessageHandlers
{
    public abstract class DomainMessageHandlerBase<TMessageType> :  MessageHandlerBase<TMessageType> where TMessageType : IMessage
    {
        protected Dictionary<Type, List<Delegate>> Handlers;

        protected DomainMessageHandlerBase()
        {
            Handlers = new Dictionary<Type, List<Delegate>>();
        }
        public void RegisterDomainEventsHandler<TDomainEvent>(Action<TDomainEvent> handler)
        {
            var domainEventType = typeof(TDomainEvent);

            if (Handlers.ContainsKey(domainEventType))
            {
                var handlers = Handlers[domainEventType];
                handlers.Add(handler);
            }
            else
            {
                Handlers[domainEventType] = new List<Delegate> { handler };
            }

        }

        protected override void HandleMessageBegin(TMessageType message)
        {
            var messageType = message.GetType();

            if (!Handlers.ContainsKey(messageType)) return;

            var handlers = Handlers[messageType];

            foreach (var handler in handlers)
            {
                DomainEventsHandler.Register(messageType, handler);
            }
        }
        protected abstract override void HandleMessage(TMessageType message);
        protected override void HandleMessageEnd(TMessageType message)
        {
            DomainEventsHandler.ClearHandlers();
        }
    }
}
