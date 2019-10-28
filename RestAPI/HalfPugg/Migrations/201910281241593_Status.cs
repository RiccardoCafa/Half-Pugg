namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequestedGroups", "Status", c => c.String(nullable: false));
            AddColumn("dbo.RequestedHalls", "Status", c => c.String(nullable: false));
            AddColumn("dbo.RequestedMatches", "Status", c => c.String(nullable: false));
            DropColumn("dbo.Players", "Sex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "Sex", c => c.String(nullable: false, maxLength: 1));
            DropColumn("dbo.RequestedMatches", "Status");
            DropColumn("dbo.RequestedHalls", "Status");
            DropColumn("dbo.RequestedGroups", "Status");
        }
    }
}
