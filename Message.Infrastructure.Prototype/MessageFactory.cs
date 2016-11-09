using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NLog;

namespace Message.Infrastructure.Prototype
{
    public class MessageFactory : IMessageFactory
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IDictionary<string, Type> _messageMap;

        public MessageFactory(IDictionary<string, Type> messageMap)
        {
            if (messageMap == null)
                throw new ArgumentNullException(nameof(messageMap));
            _messageMap = messageMap;
        }

        public IMessage FromEvent(string eventKey, string body)
        {
            IMessage message = null;
            var type = _messageMap[eventKey];
            if (type == null)
                logger.Error(
                    $"event key [{eventKey}] not found in message map. Message map count [{_messageMap.Count}]. Message body: [{body}])");
            else if (!typeof (IMessage).IsAssignableFrom(type))
                logger.Error($"type found for event key [{eventKey}] does not implement IMessage");
            else
                message = (IMessage) JsonConvert.DeserializeObject(body, type);

            return message;
        }
    }
}
