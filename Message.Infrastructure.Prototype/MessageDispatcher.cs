using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using NLog;

namespace Message.Infrastructure.Prototype
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private List<Delegate> actions;
        private List<Tuple<Type, IMessageHandler>>  messageHandlerMap;

        public void Register<T>(Action<T> handler) where T : IMessage
        {
            if (actions == null)
                actions = new List<Delegate>();
            actions.Add(handler);
        }

        public void RegisterHandler<T>(IMessageHandler<T> handler) where T : IMessage
        {
            var messageType = typeof (T);
            if (messageHandlerMap == null)
                messageHandlerMap = new List<Tuple<Type, IMessageHandler>>();


            messageHandlerMap.Add(new Tuple<Type, IMessageHandler>(messageType, handler));
        }

        public void Dispatch<T>(T message) where T : IMessage
        {
            var messageType = message.GetType();

            if (messageHandlerMap != null)
            {
                foreach (var messageHandlerPair in messageHandlerMap.Where(i => i.Item1 == messageType))
                {
                    var handler = messageHandlerPair.Item2;

                    var otherTestType = typeof (IMessageHandler<>).MakeGenericType(messageType);
                    if (otherTestType.IsInstanceOfType(handler))
                    {
                        var handleMethod = otherTestType.GetMethod("Handle");

                        handleMethod?.Invoke(handler, new object[] {message});
                    }
                }
            }

            if (actions != null)
            {
                
                foreach (var additionalAction in actions)
                {
                    var parametersTypes =
                    additionalAction.Method.GetParameters();

                    //Assumption: Because the actions passed in only allow for one parameter
                    //The parameter list should only contain one and we will use that type to determine
                    //if this is the method to run.
                    if (parametersTypes.Length == 1)
                    {
                        var actionType = parametersTypes[0].ParameterType;

                        if (messageType == actionType)
                        {
                            additionalAction.DynamicInvoke(message);
                        }
                    }
                    else
                    {
                        logger.Warn($"Action provide that does not match expectations for a message handler.");
                    }
                }
            }
        }
    }
}
