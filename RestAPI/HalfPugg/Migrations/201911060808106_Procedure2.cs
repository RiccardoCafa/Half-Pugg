namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Procedure2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.RequestedMatches", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "IdPlayer2", "dbo.Players");
            DropIndex("dbo.RequestedMatches", new[] { "IdPlayer" });
            DropIndex("dbo.RequestedMatches", new[] { "IdPlayer2" });
            DropIndex("dbo.RequestedMatches", new[] { "IdFilters" });
            AddColumn("dbo.RequestedMatches", "Filters_ID_Filter", c => c.Int());
            AddColumn("dbo.RequestedMatches", "Player1_ID", c => c.Int());
            AddColumn("dbo.RequestedMatches", "Player2_ID", c => c.Int());
            CreateIndex("dbo.RequestedMatches", "Filters_ID_Filter");
            CreateIndex("dbo.RequestedMatches", "Player1_ID");
            CreateIndex("dbo.RequestedMatches", "Player2_ID");
            AddForeignKey("dbo.RequestedMatches", "Filters_ID_Filter", "dbo.Filters", "ID_Filter");
            AddForeignKey("dbo.RequestedMatches", "Player1_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.RequestedMatches", "Player2_ID", "dbo.Players", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestedMatches", "Player2_ID", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "Player1_ID", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "Filters_ID_Filter", "dbo.Filters");
            DropIndex("dbo.RequestedMatches", new[] { "Player2_ID" });
            DropIndex("dbo.RequestedMatches", new[] { "Player1_ID" });
            DropIndex("dbo.RequestedMatches", new[] { "Filters_ID_Filter" });
            DropColumn("dbo.RequestedMatches", "Player2_ID");
            DropColumn("dbo.RequestedMatches", "Player1_ID");
            DropColumn("dbo.RequestedMatches", "Filters_ID_Filter");
            CreateIndex("dbo.RequestedMatches", "IdFilters");
            CreateIndex("dbo.RequestedMatches", "IdPlayer2");
            CreateIndex("dbo.RequestedMatches", "IdPlayer");
            AddForeignKey("dbo.RequestedMatches", "IdPlayer2", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RequestedMatches", "IdPlayer", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters", "ID_Filter", cascadeDelete: true);
        }
    }
}
