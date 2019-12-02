using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace ChatHubServer.chatHub
{


    public class ChatHub:Hub
    {

        public Dictionary<int, HashSet<string>> userConnections = new Dictionary<int, HashSet<string>>();

        public void Connect(int userID)
        {
            if (userConnections.ContainsKey(userID))
            {
                userConnections[userID].Add(Context.ConnectionId);
            }
            else
            {
                userConnections.Add(userID, new HashSet<string>() { Context.ConnectionId});
            }
        }

        public async Task JoinGroup(string groupID,int userID)
        {
            if (!userConnections.ContainsKey(userID)) Connect(userID);
            foreach(var v in userConnections[userID])
            {
                Console.WriteLine(v);
                await Groups.AddToGroupAsync(v, groupID);
            }
            
            await Clients.Groups(groupID).SendAsync("joinedGroup", groupID);
        }

        public async Task LeaveGroup(string groupID, int userID)
        {
            foreach (var v in userConnections[userID])
            {
                await Groups.RemoveFromGroupAsync(v, groupID);
            }
          
            await Clients.Groups(groupID).SendAsync("leavedGroup", groupID);
        }

        public async Task SendMessage(string message,int userID, string groupID)
        {
            await Clients.Group(groupID).SendAsync("receiveMessage",message, userID);
        }

    }
}