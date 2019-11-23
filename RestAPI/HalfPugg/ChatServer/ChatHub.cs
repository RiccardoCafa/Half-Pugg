﻿using System;
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

        public async Task ConnectToAPI(int UserID)
        {
            Player player = await api.ConnectUser(UserID, Context.ConnectionId);
            if (player == null) return;

            //Adciona esta ConnectionID aos grupos do usuario correspondente
            foreach (var g in player.Groups)
            {
                await Groups.Add(Context.ConnectionId, "group_" + g.ID.ToString());
            }

            //Adciona esta ConnectionID aos halls do usuario correspondente
            foreach (var h in player.Halls.Where(x => x.Hall.Active))
            {
                await Groups.Add(Context.ConnectionId, "hall_" + h.ID.ToString());
            }

            Clients.All.receiveAlert($"{UserID} conectado a API");
        }

        public async Task SendMessageGroup(string Message, int UserID, int GroupID)
        {
            PlayerGroup pg = db.PlayerGroups.Where(x => x.IdPlayer == UserID && x.IdGroup == GroupID).FirstOrDefault();

            if (pg == null) Clients.Caller.ReceiveAlert("User or group not finded");

            MessageGroup mg = new MessageGroup()
            {
                Content = Message,
             
                Status = MessageStatus.None,
                Send_Time = DateTime.Now,
                ID_Relation = pg.ID,
                PlayerGroup = pg
            };
            db.MessageGroups.Add(mg);
            await Clients.Group("group_" + GroupID).ReceiveMessageGroup(mg);
            await db.SaveChangesAsync();
        }
        public async Task SendMessageHall(string Message, int UserID, int HallID)
        {
            PlayerHall ph = db.PlayerHalls.Where(x => x.IdPlayer == UserID && x.IdHall == HallID).FirstOrDefault();

            if (ph == null) Clients.Caller.ReceiveAlert("User or hall not finded");

            MessageHall mh = new MessageHall()
            {
                Content = Message,
            
                Status = MessageStatus.None,
                Send_Time = DateTime.Now,
                ID_Relation = ph.ID,
                PlayerHall = ph

            };

            db.MessageHalls.Add(mh);
            await Clients.Group("hall_" + HallID).ReceiveMessageHall(mh);
            await db.SaveChangesAsync();
        }

        public async Task<bool> JoinInGroup(int UserID, int GroupID)
        {


            Group g = await db.Groups.FindAsync(GroupID);
            Player p = await db.Gamers.FindAsync(UserID);

            if (p == null || g == null) return false;

            PlayerGroup pg = new PlayerGroup { Group = g, IdGroup = GroupID, Player = p, IdPlayer = UserID };
            db.PlayerGroups.Add(pg);
            await db.SaveChangesAsync();


            await Groups.Add(Context.ConnectionId, "group_" + GroupID);
            Clients.Caller.joinedInGroup(g);
            Clients.Group("group_" + GroupID).receiveAlert($"{p.Name} joined");
            return true;
        }
        public async Task<bool> JoinInHall(int UserID, int HallID)
        {
            Hall h = await db.Halls.FindAsync(HallID);
            Player p = await db.Gamers.FindAsync(UserID);

            if (p == null || h == null || !h.Active) return false;

            var ph = new PlayerHall { Hall = h, IdHall = h.ID, Player = p, IdPlayer = p.ID };
            db.PlayerHalls.Add(ph);
            await db.SaveChangesAsync();


            await Groups.Add(Context.ConnectionId, "hall_" + HallID);
            Clients.Caller.joinedInHall(h);
            Clients.Group("hall_" + HallID).ReceiveAlert($"{p.Name} joined");
            return true;
        }

        public async Task<bool> ExitFromGroup(int UserID, int GroupID)
        {
            Player p = await db.Gamers.FindAsync(UserID);
            Group g = await db.Groups.FindAsync(GroupID);
            PlayerGroup pg = db.PlayerGroups.Where(x => x.IdGroup == GroupID && x.IdPlayer == UserID).FirstOrDefault();

            if (pg == null) return false;

            db.PlayerGroups.Remove(pg);
            await db.SaveChangesAsync();


            await Groups.Remove(Context.ConnectionId, "group_" + GroupID);
            Clients.Caller.leavedGroup(g);
            Clients.Group("group_" + GroupID).ReceiveAlert($"{p.Name} exited");
            return true;
        }
        public async Task<bool> ExitFromHall(int UserID, int HallID)
        {
            Player p = await db.Gamers.FindAsync(UserID);
            Hall h = await db.Halls.FindAsync(HallID);
            PlayerHall ph = db.PlayerHalls.Where(x => x.IdHall == HallID && x.IdPlayer == UserID).FirstOrDefault();

            if (ph == null) return false;

            db.PlayerHalls.Remove(ph);
            await db.SaveChangesAsync();


            await Groups.Remove(Context.ConnectionId, "hall_" + HallID);
            Clients.Caller.leavedHall(h);
            Clients.Group("hall_" + HallID).ReceiveAlert($"{p.Name} exited");
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