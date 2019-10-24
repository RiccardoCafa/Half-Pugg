namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Slogan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gamers", "Slogan", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gamers", "Slogan");
        }
    }
}
