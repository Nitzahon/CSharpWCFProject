using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatServer
{
    [ServiceContract(CallbackContract=typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void GetMessageFromClient(string message, List<string> clients);

        [FaultContract(typeof(UserExistsFault))]
        [OperationContract]
        void NewClientConnected(string userName);

        [OperationContract(IsOneWay = true)]
        void ClientDisconnected(string userName);
    }

    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay=true)]
        void SendMessageToClient(string message);

        [OperationContract(IsOneWay=true)]
        void UpdateClientsList(List<string> clients);
    }
}
