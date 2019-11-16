namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menssagem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MessageHalls", "View_Time");
            DropColumn("dbo.MessageHalls", "Status");
            DropColumn("dbo.MessageHalls", "CreateAt");
            DropColumn("dbo.MessageHalls", "AlteredAt");
            DropColumn("dbo.MessagePlayers", "AlteredAt");
            DropColumn("dbo.MessageGroups", "View_Time");
            DropColumn("dbo.MessageGroups", "Status");
            DropColumn("dbo.MessageGroups", "CreateAt");
            DropColumn("dbo.MessageGroups", "AlteredAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageGroups", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageGroups", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageGroups", "Status", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.MessageGroups", "View_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessagePlayers", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageHalls", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageHalls", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageHalls", "Status", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.MessageHalls", "View_Time", c => c.DateTime(nullable: false));
        }
    }
}
