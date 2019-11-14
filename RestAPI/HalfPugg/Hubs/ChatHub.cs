using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace HalfPugg
{
    public class ChatHub : Hub
    {

        public override Task OnConnected()
        {
            
            return base.OnConnected();
        }

        public async Task JoinGroup(string name,string groupID)
        {
            await Groups.Add(Context.ConnectionId, groupID);
            Clients.Group(groupID).SendMessage(name, "joined to group!");
        }

        public Task LeaveGroup(string groupID)
        {
             return Groups.Remove(Context.ConnectionId, groupID);
        }

        public void ReceiveMessageGroup(string name, string message, string groupID)
        {
            Clients.Group(groupID).SendMessage(name, message);
        }

        public void ReceiveMessage(string name, string message)
        {
            // envia a mensagem para os clientes
            Clients.All.SendMessage(name, message);
        }
    }
}