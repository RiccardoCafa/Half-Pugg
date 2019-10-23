namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchStatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.PlayerGames", "Gamer_ID", "dbo.Gamers");
            DropIndex("dbo.PlayerGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.PlayerGames", new[] { "Gamer_ID" });
            RenameColumn(table: "dbo.PlayerGames", name: "Game_ID_Game", newName: "IdGame_ID_Game");
            RenameColumn(table: "dbo.PlayerGames", name: "Gamer_ID", newName: "IdGamer_ID");
            AddColumn("dbo.Matches", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PlayerGames", "IdGame_ID_Game", c => c.Int(nullable: false));
            AlterColumn("dbo.PlayerGames", "IdGamer_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.PlayerGames", "IdGame_ID_Game");
            CreateIndex("dbo.PlayerGames", "IdGamer_ID");
            AddForeignKey("dbo.PlayerGames", "IdGame_ID_Game", "dbo.Games", "ID_Game", cascadeDelete: true);
            AddForeignKey("dbo.PlayerGames", "IdGamer_ID", "dbo.Gamers", "ID", cascadeDelete: true);
            DropColumn("dbo.PlayerGames", "IDGamer");
            DropColumn("dbo.PlayerGames", "IDGame");
            DropColumn("dbo.MessageGamers", "Send_Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageGamers", "Send_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlayerGames", "IDGame", c => c.Int(nullable: false));
            AddColumn("dbo.PlayerGames", "IDGamer", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlayerGames", "IdGamer_ID", "dbo.Gamers");
            DropForeignKey("dbo.PlayerGames", "IdGame_ID_Game", "dbo.Games");
            DropIndex("dbo.PlayerGames", new[] { "IdGamer_ID" });
            DropIndex("dbo.PlayerGames", new[] { "IdGame_ID_Game" });
            AlterColumn("dbo.PlayerGames", "IdGamer_ID", c => c.Int());
            AlterColumn("dbo.PlayerGames", "IdGame_ID_Game", c => c.Int());
            DropColumn("dbo.Matches", "Status");
            RenameColumn(table: "dbo.PlayerGames", name: "IdGamer_ID", newName: "Gamer_ID");
            RenameColumn(table: "dbo.PlayerGames", name: "IdGame_ID_Game", newName: "Game_ID_Game");
            CreateIndex("dbo.PlayerGames", "Gamer_ID");
            CreateIndex("dbo.PlayerGames", "Game_ID_Game");
            AddForeignKey("dbo.PlayerGames", "Gamer_ID", "dbo.Gamers", "ID");
            AddForeignKey("dbo.PlayerGames", "Game_ID_Game", "dbo.Games", "ID_Game");
        }
    }
}
