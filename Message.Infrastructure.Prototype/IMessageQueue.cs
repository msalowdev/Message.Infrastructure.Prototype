using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Infrastructure.Prototype
{
    public interface IMessageQueue
    {
        void Start();
        void Stop();
    }
}
