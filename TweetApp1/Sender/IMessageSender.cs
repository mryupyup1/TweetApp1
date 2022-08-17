using Rachis.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp1.Sender
{
    public interface IMessageSender
    {
        void Publish(string message);

    }
}
