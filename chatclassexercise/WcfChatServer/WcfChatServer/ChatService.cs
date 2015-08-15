using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfChatServer
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single,
        ConcurrencyMode=ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {
        private List<string> users = new List<string>();
        private Dictionary<string, IChatServiceCallback> callbacks =
            new Dictionary<string, IChatServiceCallback>();

        public void GetMessageFromClient(string message, List<string> clients)
        {
            Thread newThread=new Thread(delegate(){
                SendMessageToClients(message, clients);
            });
            newThread.Start();
        }

        private void SendMessageToClients(string message, List<string> clients)
        {
            foreach (string client in clients)
            {
                callbacks[client].SendMessageToClient(message);
            }
        }

        public void NewClientConnected(string userName)
        {
            if (users.Contains(userName))
            {
                UserExistsFault fault = new UserExistsFault();
                fault.Message = "User " + userName + " already exists";
                throw new FaultException<UserExistsFault>(fault);
            }

            try
            {
                IChatServiceCallback callback =
                    OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();
                callbacks.Add(userName, callback);
                users.Add(userName);
                UpdateUserListAtAllClients();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        private void UpdateUserListAtAllClients()
        {
            Thread updateThread = new Thread(new ThreadStart(UpdateClientAboutUsers));
            updateThread.Start();
        }

        private void UpdateClientAboutUsers()
        {
            foreach (var item in callbacks.Values)
            {
                item.UpdateClientsList(users);
            }
        }

        public void ClientDisconnected(string userName)
        {
            users.Remove(userName);
            callbacks.Remove(userName);
            UpdateUserListAtAllClients();
        }
    }
}
