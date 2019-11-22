using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HalfPugg.Hubs;
using HalfPugg.Models;
using Microsoft.AspNet.SignalR;

namespace HalfPugg
{
    public class ChatHub : Hub
    {

        ConnectionManager api = new ConnectionManager();

        public ChatHub()
        {
            Console.WriteLine($"Chat inited {api != null}");
        }

        public async Task ConnectToAPI(int UserID)
        {
            Player player =  await api.ConnectUser(UserID, Context.ConnectionId);
          
            //Adciona esta ConnectionID aos grupos do usuario correspondente
            foreach(var g in player.Groups)
            {
              await Groups.Add(Context.ConnectionId, "group_"+g.ID.ToString());
            }

            //Adciona esta ConnectionID aos halls do usuario correspondente
            foreach (var h in player.Halls.Where(x=>x.Hall.Active))
            {
                await Groups.Add(Context.ConnectionId, "hall_" + h.ID.ToString());
            }
        }

        public async Task SendMessageGroup(string Message,int UserID, int GroupID)
        {
            Group g = await api.GetGroupAsync(GroupID);
            if (g == null) return;
            
            Player p = await api.GetPlayerAsync(UserID);
            if (p == null) return;

            MessageGroup mg = new MessageGroup()
            {
                Content = Message,
                ID_Destination = GroupID,
                ID_Sender = UserID,
                Status = MessageStatus.None,
                Send_Time = DateTime.Now,
                Destination =  g,
                Sender =  p
            };
            g.Messages.Add(mg);
           await Clients.Group("group_" + GroupID).ReceiveMessageGroup(mg);
           await api.db.SaveChangesAsync();
        }
        public async Task SendMessageHall(string Message,int UserID, int HallID)
        {
            Hall h = await api.GetHallAsync(HallID);
            if (h == null || !h.Active) return;
            
            Player p = await api.GetPlayerAsync(UserID);
            if (p == null) return;

            MessageHall mh = new MessageHall()
            {
                Content = Message,
                ID_Destination = HallID,
                ID_Sender = UserID,
                Status = MessageStatus.None,
                Send_Time = DateTime.Now,
                Destination = h,
                Sender = p
            };

            h.Messages.Add(mh);
            await Clients.Group("hall_" + HallID).ReceiveMessageHall(mh);
            await api.db.SaveChangesAsync();
        }

        public async Task<bool> JoinInGroup(int UserID, int GroupID)
        {

           
            Task<Group> tg =  api.GetGroupAsync(GroupID);
            Task<Player> tp = api.GetPlayerAsync(UserID);

            Group g = tg.Result;
            Player p = tp.Result;
            
            if (p == null || g == null) return false;
            PlayerGroup obj = (PlayerGroup)api.db.PlayerGroups.ToList().Where(x => x.IdGroup == g.ID && x.IdPlayer == p.ID );
            if (!g.Integrants.Contains(obj))
            {
                p.Groups.Add(obj);
                g.Integrants.Add(obj);
                await api.db.SaveChangesAsync();
            }

            await Groups.Add(Context.ConnectionId, "group_" + GroupID);
            Clients.Group("group_" + GroupID).ReceiveAlert($"{p.Name} joined");
            return true;
        }
        public async Task<bool> JoinInHall(int UserID, int HallID)
        {
            Task<Hall> th = api.GetHallAsync(HallID);
            Task<Player> tp = api.GetPlayerAsync(UserID);

            Hall h = th.Result;
            Player p = tp.Result;

            if (p == null || h ==null || !h.Active) return false;

            PlayerHall obj = (PlayerHall)api.db.PlayerHalls.ToList().Where(x => x.IdHall == h.ID && x.IdPlayer == p.ID);
            if (!h.Integrants.Contains(obj))
            {
                p.Halls.Add(obj);
                h.Integrants.Add(obj);
                await api.db.SaveChangesAsync();
            }

            await Groups.Add(Context.ConnectionId, "group_" + HallID);
            Clients.Group("group_" + HallID).ReceiveAlert($"{p.Name} joined");
            return true;
        }


        public async Task<bool> ExitFromGroup(int UserID, int GroupID)
        {
            Task<Group> tg = api.GetGroupAsync(GroupID);
            Task<Player> tp = api.GetPlayerAsync(UserID);

            Group g = tg.Result;
            Player p = tp.Result;

            if (p == null || g == null) return false;

            PlayerGroup obj = (PlayerGroup)api.db.PlayerGroups.ToList().Where(x => x.IdGroup == g.ID && x.IdPlayer == p.ID);
            if (g.Integrants.Contains(obj))
            {
                p.Groups.Remove(obj);
                g.Integrants.Remove(obj);
                await api.db.SaveChangesAsync();
            }

            await Groups.Add(Context.ConnectionId, "group_" + GroupID);
            Clients.Group("group_" + GroupID).ReceiveAlert($"{p.Name} exited");
            return true;
        }
        public async Task<bool> ExitFromHall(int UserID, int HallID)
        {
            Task<Hall> th = api.GetHallAsync(HallID);
            Task<Player> tp = api.GetPlayerAsync(UserID);

            Hall h = th.Result;
            Player p = tp.Result;

            if (p == null || h == null || !h.Active) return false;
            PlayerHall obj = (PlayerHall)api.db.PlayerHalls.ToList().Where(x => x.IdHall == h.ID && x.IdPlayer == p.ID);
            if (h.Integrants.Contains(obj))
            {
                p.Halls.Remove(obj);
                h.Integrants.Remove(obj);
                await api.db.SaveChangesAsync();
            }

            await Groups.Remove(Context.ConnectionId, "group_" + HallID);
            Clients.Group("group_" + HallID).ReceiveAlert($"{p.Name} exited");
            return true;
        }

        public override Task OnReconnected()
        {
             api.db.ChatConnections.Find(Context.ConnectionId).Connected = true;
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            api.Desconnect(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

    }
}