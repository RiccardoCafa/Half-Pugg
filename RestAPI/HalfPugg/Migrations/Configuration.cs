namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HalfPugg.Models.HalfPuggContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HalfPugg.Models.HalfPuggContext context)
        {
            
        }
    }
}
