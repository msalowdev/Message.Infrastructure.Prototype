using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Message.Infrastructure.Prototype
{
    public class MessageDispatcher : IMessageDispatcher
    {

        private List<Delegate> actions;
        private readonly IUnityContainer _container;

        public MessageDispatcher(IUnityContainer container)
        {
            _container = container;
        }

        public void Register<T>(Action<T> handler) where T : IMessage
        {
            if (actions == null)
                actions = new List<Delegate>();
            actions.Add(handler);
        }


        public void Dispatch<T>(T message) where T : IMessage
        {
            if (_container != null)
            {
                var handlers = _container.ResolveAll<IMessageHandler<T>>();

                foreach (var messageHandler in handlers)
                {
                    messageHandler.Handle(message);
                }
            }

            if (actions != null)
            {
                foreach (var additionalAction in actions)
                {
                    //why doesn't this work?
                    var testType = typeof (Action<T>);

                    var  action = additionalAction as Action<T>;
                    action?.Invoke(message);
                }
            }
        }
    }
}
