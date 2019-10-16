namespace HalfPugg.Migrations
{
    using System.Data.Entity.Migrations;
    using HalfPugg.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<HalfPuggContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HalfPuggContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
