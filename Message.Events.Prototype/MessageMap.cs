using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Events.Prototype
{
    public static class MessageMap
    {
        public static IDictionary<string, Type> GetMessageMap()
        {
            return new Dictionary<string, Type>()
            {
                {"billing.account.create",typeof (CreateAccount)}
            };
        }
    }
}
