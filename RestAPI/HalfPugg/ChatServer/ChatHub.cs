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
        HalfPuggContext db = new HalfPuggContext();


        /// <summary>
        /// associa a ConnectionID atual ao ID do usuario no Half, é necessário que seja feita esta
        /// conexão antes de tudo pois o mesmo usuario pode estar usando o chat por dois dispositivos diferentes
        /// e cada um gera uma ConnectionID propria sendo assim necessário mapear-las para o usuario correspondente
        /// </summary>
        /// <param name="UserID"> ID do usuario a ser conectado</param>
        /// <returns></returns>
        public async Task ConnectToAPI(int UserID)
        {
            Player player = await api.ConnectUser(UserID, Context.ConnectionId);

            if (player == null) {
                await Clients.All.receiveAlert($"User: {UserID} not finded");
                return;
            }

            player.Groups = db.PlayerGroups.Where(x => x.IdPlayer == UserID).ToList();
            player.Halls = db.PlayerHalls.Where(x => x.IdPlayer == UserID).ToList();

            await Clients.Caller.receiveAlert($"{player.Name} connected a API");
            //Adciona esta ConnectionID aos grupos do usuario correspondente
            foreach (var g in player.Groups)
            {
                string gid = "group_" + g.IdGroup;
                await Groups.Add(Context.ConnectionId, gid);
                await Clients.Caller.receiveAlert($"{player.Name} connected to {g.Group.Name}");
            }

            //Adciona esta ConnectionID aos halls do usuario correspondente
            foreach (var h in player.Halls.Where(x => x.Hall.Active))
            {
                string hid = "hall_" + h.IdHall;
                await Groups.Add(Context.ConnectionId, hid);
                await Clients.Caller.receiveAlert($"{player.Name} connected to {h.Hall.Name}");
            }

          
        }

        /// <summary>
        /// Envia uma mensagem para um grupo, caso o grupo ou o usuario não existam no banco será chamada
        /// a função receiveAlert
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="UserID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public async Task<bool> SendMessageGroup(string Message, int UserID, int GroupID)
        {
            PlayerGroup pg = db.PlayerGroups.Where(x => x.IdPlayer == UserID && x.IdGroup == GroupID).FirstOrDefault();

            if (pg == null)
            {
                await Clients.Caller.receiveAlert($"User: {UserID} or Group: {GroupID} not finded");
                return false;
            }

            MessageGroup mg = new MessageGroup()
            {
                Content = Message,
             
                Status = MessageStatus.None,
                Send_Time = DateTime.Now,
                ID_Relation = pg.ID,
                PlayerGroup = pg
            };

            db.MessageGroups.Add(mg);
            await Clients.Group("group_" + GroupID).receiveMessageGroup(mg);
            await Clients.Caller.receiveAlert($"Message sent from {pg.Player.Name} to {pg.Group.Name}");
            await db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SendMessageHall(string Message, int UserID, int HallID)
        {
            PlayerHall ph = db.PlayerHalls.Where(x => x.IdPlayer == UserID && x.IdHall == HallID).FirstOrDefault();

            if (ph == null)
            {
                Clients.Caller.receiveAlert($"User: {UserID} or Hall: {HallID} not finded");
               
                return false;
            }

            MessageHall mh = new MessageHall { 
            Content = Message,
            PlayerHall = ph,
            ID_Relation = ph.ID,
            Send_Time = DateTime.Now,
            Status = MessageStatus.None
            };

            await Clients.Group("hall_" + HallID).receiveMessageHall(mh);
            await Clients.Caller.receiveAlert($"Message sent from {ph.Player.Name} to {ph.Hall.Name}");
            await db.SaveChangesAsync();

            return true;
        }
        public async Task<bool> SendMessagePlayer(string Message, int UserID, int ReceiverID)
        {
            Player sender = await db.Gamers.FindAsync(UserID);
            Player receiver = await db.Gamers.FindAsync(ReceiverID);

            if (sender == null || receiver == null)
            {
                Clients.Caller.receiveAlert($"Sender: {UserID} or Receiver: {ReceiverID} not finded");
                return false;
            }

            bool receiverOnlide = await api.UserOnline(ReceiverID);

            MessagePlayer mp = new MessagePlayer
            {
                Content = Message,
                Destination = receiver,
                ID_Destination = ReceiverID,
                ID_Sender = UserID,
                Sender = sender,
                Send_Time = DateTime.Now,
                Status = MessageStatus.NoReceived
            };

            db.MessageGamers.Add(mp);
            foreach (var item in db.ChatConnections./*Where(x=>x.Player_ID == Receiver_ID && x.Connected).*/ToArray())
            {
                await Clients.Client(item.ConnectionID).receiveMessageHall(mp);
            }

            await Clients.Caller.receiveAlert($"Message sent from {sender.Name} to {receiver.Name}");
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> JoinInGroup(int UserID, int GroupID)
        {

            PlayerGroup pg = db.PlayerGroups.Where(x => x.IdPlayer == UserID && x.IdGroup == GroupID).FirstOrDefault();
            if (pg == null)
            {
                Group g = await db.Groups.FindAsync(GroupID);
                Player p = await db.Gamers.FindAsync(UserID);


                if (p == null || g == null) {
                    await Clients.Caller.receiveAlert($"User: {UserID} or Group: {GroupID} not finded");
                    return false;
                }

                PlayerGroup npg = new PlayerGroup { Group = g, IdGroup = GroupID, Player = p, IdPlayer = UserID };
                db.PlayerGroups.Add(npg);
                await db.SaveChangesAsync();
                foreach (var c in p.ChatConnections)
                {
                    await Groups.Add(c.ConnectionID, "group_" + GroupID);
                }
                await Clients.Group("group_" + GroupID).receiveGroupAlert($"{p.Name} joined");
                await Clients.Caller.receiveAlert($"{pg.Player.Name} joined into {pg.Group.Name}");
      
            }
            else
            {
                await Clients.Caller.receiveAlert($"{pg.Player.Name} already joined into {pg.Group.Name}");
            }
           
            return true;
        }
        public async Task<bool> JoinInHall(int UserID, int HallID)
        {

            PlayerHall ph = db.PlayerHalls.Where(x => x.IdPlayer == UserID && x.IdHall == HallID).FirstOrDefault();

            if(ph == null)
            {
                Hall h = await db.Halls.FindAsync(HallID);
                Player p = await db.Gamers.FindAsync(UserID);

                if (p == null || h == null || !h.Active)
                {
                    await Clients.Caller.receiveAlert($"User: {UserID} or Hall: {HallID} not finded");
                    return false;
                }
                var nph = new PlayerHall { Hall = h, IdHall = h.ID, Player = p, IdPlayer = p.ID };
                db.PlayerHalls.Add(nph);
                await db.SaveChangesAsync();

                foreach (var c in p.ChatConnections)
                {
                    await Groups.Add(c.ConnectionID, "hall_" + HallID);
                }
             
                await Clients.Group("hall_" + HallID).receiveHallAlert($"{p.Name} joined");
                await Clients.Caller.receiveAlert($"{ph.Player.Name} joined into {ph.Hall.Name}");
            }
            else
            {
                await Clients.Caller.receiveAlert($"{ph.Player.Name} already joined into {ph.Hall.Name}");
            }

            return true;
        }

        public async Task<bool> ExitFromGroup(int UserID, int GroupID)
        {
            Player p = await db.Gamers.FindAsync(UserID);
            Group g = await db.Groups.FindAsync(GroupID);
            PlayerGroup pg = db.PlayerGroups.Where(x => x.IdGroup == GroupID && x.IdPlayer == UserID).FirstOrDefault();

            if (pg == null) 
            {
                await Clients.Caller.receiveAlert($"User: {UserID} or Group: {GroupID} not finded");
                return false; 
            }
            

            db.PlayerGroups.Remove(pg);
            await db.SaveChangesAsync();
           
            foreach(var c in p.ChatConnections)
            {
                await Groups.Remove(c.ConnectionID, "group_" + GroupID);
            }
           
            await Clients.Caller.receiveAlert($"{pg.Player.Name} leaved {pg.Group.Name}");
            await Clients.Group("group_" + GroupID).ReceiveGroupAlert($"{p.Name} exited");
        
            return true;
        }
        public async Task<bool> ExitFromHall(int UserID, int HallID)
        {
            Player p = await db.Gamers.FindAsync(UserID);
            Hall h = await db.Halls.FindAsync(HallID);
            PlayerHall ph = db.PlayerHalls.Where(x => x.IdHall == HallID && x.IdPlayer == UserID).FirstOrDefault();

            if (ph == null) return false;

            await Clients.Caller.receiveAlert($"{ph.Player.Name} leaved {ph.Hall.Name}");          
            await Clients.Group("hall_" + HallID).ReceiveHallAlert($"{p.Name} exited");
          
            foreach (var c in p.ChatConnections)
            {
                await Groups.Remove(c.ConnectionID, "hall_" + HallID);
            }
          

            db.PlayerHalls.Remove(ph);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<Group> GetGroup(int GroupID)
        {
            return await db.Groups.FindAsync(GroupID);
        }
        public async Task<Hall> GetHall(int HallID)
        {
            return await db.Halls.FindAsync(HallID);
        }

        public override Task OnReconnected()
        {
            var con = db.ChatConnections.Find(Context.ConnectionId);
            if (con == null) return null;
            Clients.Caller.receiveAlert($"{Context.ConnectionId} reconected");
            con.Connected = true;
            db.SaveChanges();
            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            api.Desconnect(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

    }
}