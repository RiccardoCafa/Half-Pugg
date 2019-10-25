namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Player : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "Player1_ID", "dbo.Players");
            DropForeignKey("dbo.Matches", "Player2_ID", "dbo.Players");
            DropIndex("dbo.Matches", new[] { "Player1_ID" });
            DropIndex("dbo.Matches", new[] { "Player2_ID" });
            DropColumn("dbo.Matches", "IdPlayer1");
            DropColumn("dbo.Matches", "IdPlayer2");
            RenameColumn(table: "dbo.Matches", name: "Player1_ID", newName: "IdPlayer1");
            RenameColumn(table: "dbo.Matches", name: "Player2_ID", newName: "IdPlayer2");
            RenameColumn(table: "dbo.RequestedGroups", name: "IdSala", newName: "IdGroup");
            RenameIndex(table: "dbo.RequestedGroups", name: "IX_IdSala", newName: "IX_IdGroup");
            AlterColumn("dbo.Matches", "IdPlayer1", c => c.Int(nullable: false));
            AlterColumn("dbo.Matches", "IdPlayer2", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "IdPlayer1");
            CreateIndex("dbo.Matches", "IdPlayer2");
            CreateIndex("dbo.MessagePlayers", "ID_User");
            CreateIndex("dbo.MessagePlayers", "ID_Recipient");
            AddForeignKey("dbo.MessagePlayers", "ID_Recipient", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MessagePlayers", "ID_User", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Matches", "IdPlayer1", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Matches", "IdPlayer2", "dbo.Players", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "IdPlayer2", "dbo.Players");
            DropForeignKey("dbo.Matches", "IdPlayer1", "dbo.Players");
            DropForeignKey("dbo.MessagePlayers", "ID_User", "dbo.Players");
            DropForeignKey("dbo.MessagePlayers", "ID_Recipient", "dbo.Players");
            DropIndex("dbo.MessagePlayers", new[] { "ID_Recipient" });
            DropIndex("dbo.MessagePlayers", new[] { "ID_User" });
            DropIndex("dbo.Matches", new[] { "IdPlayer2" });
            DropIndex("dbo.Matches", new[] { "IdPlayer1" });
            AlterColumn("dbo.Matches", "IdPlayer2", c => c.Int());
            AlterColumn("dbo.Matches", "IdPlayer1", c => c.Int());
            RenameIndex(table: "dbo.RequestedGroups", name: "IX_IdGroup", newName: "IX_IdSala");
            RenameColumn(table: "dbo.RequestedGroups", name: "IdGroup", newName: "IdSala");
            RenameColumn(table: "dbo.Matches", name: "IdPlayer2", newName: "Player2_ID");
            RenameColumn(table: "dbo.Matches", name: "IdPlayer1", newName: "Player1_ID");
            AddColumn("dbo.Matches", "IdPlayer2", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "IdPlayer1", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "Player2_ID");
            CreateIndex("dbo.Matches", "Player1_ID");
            AddForeignKey("dbo.Matches", "Player2_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.Matches", "Player1_ID", "dbo.Players", "ID");
        }
    }
}
