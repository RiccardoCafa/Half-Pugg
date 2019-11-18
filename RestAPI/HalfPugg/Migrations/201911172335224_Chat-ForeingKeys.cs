namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatForeingKeys : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "IdGroups", c => c.Int(nullable: true));
            AddColumn("dbo.Players", "IdHalls", c => c.Int(nullable: true));
            AddColumn("dbo.Groups", "IdIntegrants", c => c.Int(nullable: true));
            AddColumn("dbo.Groups", "IdMessages", c => c.Int(nullable: true));
            AddColumn("dbo.Halls", "IdIntegrants", c => c.Int(nullable: true));
            AddColumn("dbo.Halls", "IdFilters", c => c.Int(nullable: true));
            AddColumn("dbo.Halls", "IdMessages", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Halls", "IdMessages");
            DropColumn("dbo.Halls", "IdFilters");
            DropColumn("dbo.Halls", "IdIntegrants");
            DropColumn("dbo.Groups", "IdMessages");
            DropColumn("dbo.Groups", "IdIntegrants");
            DropColumn("dbo.Players", "IdHalls");
            DropColumn("dbo.Players", "IdGroups");
        }

       
    }
}
