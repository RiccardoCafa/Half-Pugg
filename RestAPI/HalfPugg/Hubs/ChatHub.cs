using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HalfPugg
{
    public class ChatHub : Hub
    {
        
        public void Send(string name, string message)
        {
            // envia a mensagem para os clientes
            Clients.All.SendMessage(name, message);
        }
    }
}