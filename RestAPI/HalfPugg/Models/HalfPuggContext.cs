using HalfPugg.Hubs;
using System.Data.Entity;

namespace HalfPugg.Models
{
    public class HalfPuggContext : DbContext
    {
        public HalfPuggContext() : base("Half-Pugg"){ }
               
        public DbSet<Player> Gamers { get; set; }
        public DbSet<PlayerGroup> PlayerGroups { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Classification_Gamer> Classification_Gamers { get; set; }
        public DbSet<ClassificationPlayer> Classification_Players { get; set; }
        public DbSet<PlayerHashtag> PlayerHashtags { get; set; }
        public DbSet<PlayerGame> PlayerGames { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Classification_Player> Classification_Games { get; set; }
        public DbSet<GameInGame> GamerInGame { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
        public DbSet<MessagePlayer> MessageGamers { get; set; }
        public DbSet<MessageGroup> MessageGroups { get; set; }
        public DbSet<RequestedGroup> RequestedGroups { get; set; }
        public DbSet<RequestedMatch> RequestedMatchs { get; set; }
        public DbSet<ChatConnection> ChatConnections { get; set; }
        public DbSet<PlayerRequestedGroup> PlayerRequestedGroup { get; set; }

    }
}
