namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchChar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestedMatches", "Status", c => c.String(nullable: false, maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequestedMatches", "Status");
        }
    }
}
