using HalfPugg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HalfPugg.Hubs
{
    public class ConnectionManager
    {
       public HalfPuggContext db = new HalfPuggContext();

        public async Task<Player> ConnectUser(int UserID, string ConnectionId)
        {
            Player player = await db.Gamers.FindAsync(UserID);
            if (player != null)
            {
                if (player.ChatConnections == null) player.ChatConnections = new List<ChatConnection>();
                player.ChatConnections.Add(new ChatConnection { Connected = true, ConnectionID = ConnectionId });
                await db.SaveChangesAsync();
            }
            return player;
        }

        public async Task Desconnect(string ConnectionId)
        {
           ChatConnection con= await db.ChatConnections.FindAsync(ConnectionId);
            con.Connected = false;
            await db.SaveChangesAsync();
        }

        public async Task<bool> UserOnline(int UserID)
        {
            Player p = await db.Gamers.FindAsync(UserID);
            return p.ChatConnections.Where(x => x.Connected).Count() > 0;
        }

        public async Task<Player> GetPlayerAsync(int UserID)
        {
            return await db.Gamers.FindAsync(UserID);
        }
        public async Task<Group> GetGroupAsync(int GroupID)
        {
            return await db.Groups.FindAsync(GroupID);
        }
        public async Task<Hall> GetHallAsync(int HallID)
        {
            return await db.Halls.FindAsync(HallID);
        }
    }
}