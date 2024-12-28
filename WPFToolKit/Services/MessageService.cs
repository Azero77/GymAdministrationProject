using WPFToolKit.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFToolKit.Services
{
    public class MessageService
    {
        public MessageStore MessageStore { get; }

        public MessageService(MessageStore messageStore)
        {
            MessageStore = messageStore;
        }

        public void SetMessage(string message, MessageType messageType)
        {
            MessageStore.CurrentMessage = message;
            MessageStore.CurrentMessageType = messageType;

        }
    }
}
