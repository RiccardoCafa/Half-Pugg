using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HalfPugg.Models
{
    public class HalfPuggContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public HalfPuggContext() : base("name=HalfPuggContext")
        {
        }

        public System.Data.Entity.DbSet<HalfPugg.Models.Gamer> Gamers { get; set; }

        public System.Data.Entity.DbSet<HalfPugg.Models.Game> Games { get; set; }

        public System.Data.Entity.DbSet<HalfPugg.Models.Classification_Gamer> Classification_Gamer { get; set; }

        public System.Data.Entity.DbSet<HalfPugg.Models.Classification_User> Classification_User { get; set; }

        public System.Data.Entity.DbSet<HalfPugg.Models.Game_Topic> Game_Topic { get; set; }

        public System.Data.Entity.DbSet<HalfPugg.Models.Match> Matches { get; set; }

        public System.Data.Entity.DbSet<HalfPugg.Models.Matter> Matters { get; set; }

        public System.Data.Entity.DbSet<HalfPugg.Models.User_Game> User_Game { get; set; }

        public System.Data.Entity.DbSet<HalfPugg.Models.Topic> Topics { get; set; }
    }
}
