namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Procedure3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestedMatches", "Filters_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.RequestedMatches", "Player1_ID", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "Player2_ID", "dbo.Players");
            DropIndex("dbo.RequestedMatches", new[] { "Filters_ID_Filter" });
            DropIndex("dbo.RequestedMatches", new[] { "Player1_ID" });
            DropIndex("dbo.RequestedMatches", new[] { "Player2_ID" });
            DropColumn("dbo.RequestedMatches", "IdFilters");
            DropColumn("dbo.RequestedMatches", "IdPlayer2");
            RenameColumn(table: "dbo.RequestedMatches", name: "Filters_ID_Filter", newName: "IdFilters");
            RenameColumn(table: "dbo.RequestedMatches", name: "Player1_ID", newName: "IdPlayer1");
            RenameColumn(table: "dbo.RequestedMatches", name: "Player2_ID", newName: "IdPlayer2");
            AlterColumn("dbo.RequestedMatches", "IdFilters", c => c.Int(nullable: false));
            AlterColumn("dbo.RequestedMatches", "IdPlayer1", c => c.Int(nullable: false));
            AlterColumn("dbo.RequestedMatches", "IdPlayer2", c => c.Int(nullable: false));
            CreateIndex("dbo.RequestedMatches", "IdPlayer1");
            CreateIndex("dbo.RequestedMatches", "IdPlayer2");
            CreateIndex("dbo.RequestedMatches", "IdFilters");
            AddForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters", "ID_Filter", cascadeDelete: true);
            AddForeignKey("dbo.RequestedMatches", "IdPlayer1", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RequestedMatches", "IdPlayer2", "dbo.Players", "ID", cascadeDelete: true);
            DropColumn("dbo.RequestedMatches", "IdPlayer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestedMatches", "IdPlayer", c => c.Int(nullable: false));
            DropForeignKey("dbo.RequestedMatches", "IdPlayer2", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "IdPlayer1", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters");
            DropIndex("dbo.RequestedMatches", new[] { "IdFilters" });
            DropIndex("dbo.RequestedMatches", new[] { "IdPlayer2" });
            DropIndex("dbo.RequestedMatches", new[] { "IdPlayer1" });
            AlterColumn("dbo.RequestedMatches", "IdPlayer2", c => c.Int());
            AlterColumn("dbo.RequestedMatches", "IdPlayer1", c => c.Int());
            AlterColumn("dbo.RequestedMatches", "IdFilters", c => c.Int());
            RenameColumn(table: "dbo.RequestedMatches", name: "IdPlayer2", newName: "Player2_ID");
            RenameColumn(table: "dbo.RequestedMatches", name: "IdPlayer1", newName: "Player1_ID");
            RenameColumn(table: "dbo.RequestedMatches", name: "IdFilters", newName: "Filters_ID_Filter");
            AddColumn("dbo.RequestedMatches", "IdPlayer2", c => c.Int(nullable: false));
            AddColumn("dbo.RequestedMatches", "IdFilters", c => c.Int(nullable: false));
            CreateIndex("dbo.RequestedMatches", "Player2_ID");
            CreateIndex("dbo.RequestedMatches", "Player1_ID");
            CreateIndex("dbo.RequestedMatches", "Filters_ID_Filter");
            AddForeignKey("dbo.RequestedMatches", "Player2_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.RequestedMatches", "Player1_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.RequestedMatches", "Filters_ID_Filter", "dbo.Filters", "ID_Filter");
        }
    }
}
