using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfChatClient.ServiceReference1;

namespace WcfChatClient
{
    public class ClientCallback: IChatServiceCallback
    {
        public delegate void UpdateTextBoxDelegate(string s);
        public event UpdateTextBoxDelegate addMessageToTextbox;
        public void SendMessageToClient(string message)
        {
            addMessageToTextbox(message);
        }

        public delegate void UpdateListDelegate(List<string> users);
        public event UpdateListDelegate updateUsersList;
        public void UpdateClientsList(List<string> clients)
        {
            updateUsersList(clients);
        }
    }
}
