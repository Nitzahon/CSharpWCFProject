using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GameService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GameService : IGameService
    {
        public void DoWork()
        {
        }
    }
}
