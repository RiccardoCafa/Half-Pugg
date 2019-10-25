using System.Data.Entity;

namespace HalfPugg.Models
{
    public class HalfPuggContext : DbContext
    {
        public HalfPuggContext() : base("Half-Pugg"){ }
               
        public DbSet<Player> Gamers { get; set; }
        public DbSet<PlayerGroup> PlayerGroup { get; set; }
        public DbSet<PlayerHall> PlayerHall { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Classification_Gamer> Classification_Gamers { get; set; }
        public DbSet<ClassificationPlayer> Classification_Players { get; set; }
        public DbSet<PlayerHashtag> PlayerHashtags { get; set; }
        public DbSet<PlayerGame> PlayerGames { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Classification_Player> Classification_Games { get; set; }
        public DbSet<GameInGame> GamerInGame { get; set; }
        public DbSet<Classification_Match> Classification_Matchs { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<HashTag> HashTags { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<MessagePlayer> MessageGamers { get; set; }
        public DbSet<MessageGroup> MessageGroups { get; set; }
        public DbSet<MessageHall> MessageHalls { get; set; }
        public DbSet<Numbered> Numbereds { get; set; }
        public DbSet<Range> Ranges { get; set; }
        public DbSet<RequestedGroup> RequestedGroups { get; set; }
        public DbSet<RequestedHall> RequestedHalls { get; set; }
        public DbSet<RequestedMatch> RequestedMatchs { get; set; }
        public DbSet<Template> Templates { get; set; }               

    }
}
