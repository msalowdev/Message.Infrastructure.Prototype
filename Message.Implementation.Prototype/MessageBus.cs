using System;
using System.Collections.Generic;
using Message.Infrastructure.Prototype;

namespace Message.Implementation.Prototype
{
    public sealed class MessageBus : IMessageBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers;

        public MessageBus()
        {
            _subscribers = new Dictionary<Type, List<Delegate>>();
        }

        public void Subscribe<TMessageType>(Action<TMessageType> handler)
        {
            var handlerType = typeof (TMessageType);
            if (_subscribers.ContainsKey(handlerType))
            {
                var handlers = _subscribers[handlerType];

                handlers.Add(handler);
            }
            else
            {
                _subscribers[handlerType] = new List<Delegate> {handler};
            }
        }

        public void Unsubscribe<TMessageType>(Action<TMessageType> handler)
        {
            var handlerType = typeof(TMessageType);

            if (_subscribers.ContainsKey(handlerType))
            {
                var handlers = _subscribers[handlerType];

                handlers.Remove(handler);
            }
        }

        public void Publish(IMessage message)
        {
            var messageType = message.GetType();

            if (_subscribers.ContainsKey(messageType))
            {
                var handlers = _subscribers[messageType];
                foreach (var handler in handlers)
                {
                    handler.DynamicInvoke(message);
                }
            }
        }
    }
}
