using System;
using System.Collections.Generic;

namespace Message.Domain.Prototype.DomainEvents
{
    public static class DomainEventsHandler
    {
        [ThreadStatic]
        private static Dictionary<Type, List<Delegate>> _eventHandlers;

        public static void Register<T>(Action<T> callback)
        {
            var eventType = typeof (T);

            if (_eventHandlers == null)
                _eventHandlers = new Dictionary<Type, List<Delegate>>();

            if (_eventHandlers.ContainsKey(eventType))
            {
                var handlers = _eventHandlers[eventType];

                handlers.Add(callback);
            }
            else
            {
                _eventHandlers[eventType] = new List<Delegate> { callback };
            }
        }

        public static void Register(Type domainEventType, Delegate callback)
        {

        }

        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            var eventType = typeof (T);

            if (_eventHandlers == null || !_eventHandlers.ContainsKey(eventType))
                return;

            var handlers = _eventHandlers[eventType];

            foreach (var handler in handlers)
            {
                handler.DynamicInvoke(domainEvent);
            }
        }

        public static void ClearHandlers()
        {
            _eventHandlers = null;
        }
    }
}
