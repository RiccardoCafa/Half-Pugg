using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HalfPugg
{
    public class ChatHub : Hub
    {
        
        public void ReceiveMessage(string name, string message)
        {
            // envia a mensagem para os clientes
            Clients.All.SendMessage(name+$" [{DateTime.Now.Hour}:{DateTime.Now.Minute}]", message);
        }
    }
}