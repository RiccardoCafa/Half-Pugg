namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class match1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RequestedGroups", "CreateAt");
            DropColumn("dbo.RequestedGroups", "AlteredAt");
            DropColumn("dbo.RequestedHalls", "CreateAt");
            DropColumn("dbo.RequestedHalls", "AlteredAt");
            DropColumn("dbo.RequestedMatches", "CreateAt");
            DropColumn("dbo.RequestedMatches", "AlteredAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestedMatches", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestedMatches", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestedHalls", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestedHalls", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestedGroups", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RequestedGroups", "CreateAt", c => c.DateTime(nullable: false));
        }
    }
}
