namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Message : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageHalls", "ID_Destination", "dbo.Halls");
            DropForeignKey("dbo.MessageHalls", "ID_Sender", "dbo.Players");
            DropForeignKey("dbo.MessageGroups", "ID_Destination", "dbo.Groups");
            DropForeignKey("dbo.MessageGroups", "ID_Sender", "dbo.Players");
            DropIndex("dbo.MessageHalls", new[] { "ID_Sender" });
            DropIndex("dbo.MessageHalls", new[] { "ID_Destination" });
            DropIndex("dbo.MessageGroups", new[] { "ID_Sender" });
            DropIndex("dbo.MessageGroups", new[] { "ID_Destination" });
            AddColumn("dbo.MessageHalls", "ID_Relation", c => c.Int(nullable: false));
            AddColumn("dbo.MessageGroups", "ID_Relation", c => c.Int(nullable: false));
            CreateIndex("dbo.MessageGroups", "ID_Relation");
            CreateIndex("dbo.MessageHalls", "ID_Relation");
            AddForeignKey("dbo.MessageGroups", "ID_Relation", "dbo.PlayerGroups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MessageHalls", "ID_Relation", "dbo.PlayerHalls", "ID", cascadeDelete: true);
            DropColumn("dbo.MessageHalls", "ID_Sender");
            DropColumn("dbo.MessageHalls", "ID_Destination");
            DropColumn("dbo.MessageGroups", "ID_Sender");
            DropColumn("dbo.MessageGroups", "ID_Destination");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageGroups", "ID_Destination", c => c.Int(nullable: false));
            AddColumn("dbo.MessageGroups", "ID_Sender", c => c.Int(nullable: false));
            AddColumn("dbo.MessageHalls", "ID_Destination", c => c.Int(nullable: false));
            AddColumn("dbo.MessageHalls", "ID_Sender", c => c.Int(nullable: false));
            DropForeignKey("dbo.MessageHalls", "ID_Relation", "dbo.PlayerHalls");
            DropForeignKey("dbo.MessageGroups", "ID_Relation", "dbo.PlayerGroups");
            DropIndex("dbo.MessageHalls", new[] { "ID_Relation" });
            DropIndex("dbo.MessageGroups", new[] { "ID_Relation" });
            DropColumn("dbo.MessageGroups", "ID_Relation");
            DropColumn("dbo.MessageHalls", "ID_Relation");
            CreateIndex("dbo.MessageGroups", "ID_Destination");
            CreateIndex("dbo.MessageGroups", "ID_Sender");
            CreateIndex("dbo.MessageHalls", "ID_Destination");
            CreateIndex("dbo.MessageHalls", "ID_Sender");
            AddForeignKey("dbo.MessageGroups", "ID_Sender", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MessageGroups", "ID_Destination", "dbo.Groups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MessageHalls", "ID_Sender", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MessageHalls", "ID_Destination", "dbo.Halls", "ID", cascadeDelete: true);
        }
    }
}
