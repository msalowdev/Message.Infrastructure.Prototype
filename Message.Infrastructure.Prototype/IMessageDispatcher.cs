

using System;
using System.Collections.Generic;

namespace Message.Infrastructure.Prototype
{
    public interface IMessageDispatcher
    {
        void Register<T>(Action<T> handler) where T : IMessage;
        void Dispatch<T>(T message) where T : IMessage;
    }
}
