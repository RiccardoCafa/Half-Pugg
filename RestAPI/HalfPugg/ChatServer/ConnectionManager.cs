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
        private HalfPuggContext db = new HalfPuggContext();

        public async Task<Player> ConnectUser(int UserID, string ConnectionId)
        {


            Player player = await db.Gamers.FindAsync(UserID);

            if (player != null)
            {
                if (db.ChatConnections.Find(ConnectionId) != null) return player;

                if (player.ChatConnections == null)
                {
                    player.ChatConnections = new List<ChatConnection>();
                }

                player.ChatConnections.Add(new ChatConnection { Connected = true, ConnectionID = ConnectionId });
                await db.SaveChangesAsync();
            }
            return player;
        }

        //public async Task<Player> GetUserConnected(string ConnectionID)
        //{
        //  var ci=  db.ChatConnections.Where(x => x.ConnectionID == ConnectionID).FirstOrDefault();
            
        //}

        public async Task Desconnect(string ConnectionId)
        {
            ChatConnection con = await db.ChatConnections.FindAsync(ConnectionId);
            con.Connected = false;
            await db.SaveChangesAsync();
        }

        public async Task<bool> UserOnline(int UserID)
        {
            Player p = await db.Gamers.FindAsync(UserID);
            return p.ChatConnections.Where(x => x.Connected).Count() > 0;
        }


    }
}