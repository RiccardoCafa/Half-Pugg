namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playerController_idInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerGames", "IdGame_ID_Game", "dbo.Games");
            DropForeignKey("dbo.PlayerGames", "IdGamer_ID", "dbo.Gamers");
            DropIndex("dbo.PlayerGames", new[] { "IdGame_ID_Game" });
            DropIndex("dbo.PlayerGames", new[] { "IdGamer_ID" });
            RenameColumn(table: "dbo.PlayerGames", name: "IdGame_ID_Game", newName: "Game_ID_Game");
            RenameColumn(table: "dbo.PlayerGames", name: "IdGamer_ID", newName: "Gamer_ID");
            AddColumn("dbo.PlayerGames", "IDGamer", c => c.Int(nullable: false));
            AddColumn("dbo.PlayerGames", "IDGame", c => c.Int(nullable: false));
            AlterColumn("dbo.PlayerGames", "Game_ID_Game", c => c.Int());
            AlterColumn("dbo.PlayerGames", "Gamer_ID", c => c.Int());
            CreateIndex("dbo.PlayerGames", "Game_ID_Game");
            CreateIndex("dbo.PlayerGames", "Gamer_ID");
            AddForeignKey("dbo.PlayerGames", "Game_ID_Game", "dbo.Games", "ID_Game");
            AddForeignKey("dbo.PlayerGames", "Gamer_ID", "dbo.Gamers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerGames", "Gamer_ID", "dbo.Gamers");
            DropForeignKey("dbo.PlayerGames", "Game_ID_Game", "dbo.Games");
            DropIndex("dbo.PlayerGames", new[] { "Gamer_ID" });
            DropIndex("dbo.PlayerGames", new[] { "Game_ID_Game" });
            AlterColumn("dbo.PlayerGames", "Gamer_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.PlayerGames", "Game_ID_Game", c => c.Int(nullable: false));
            DropColumn("dbo.PlayerGames", "IDGame");
            DropColumn("dbo.PlayerGames", "IDGamer");
            RenameColumn(table: "dbo.PlayerGames", name: "Gamer_ID", newName: "IdGamer_ID");
            RenameColumn(table: "dbo.PlayerGames", name: "Game_ID_Game", newName: "IdGame_ID_Game");
            CreateIndex("dbo.PlayerGames", "IdGamer_ID");
            CreateIndex("dbo.PlayerGames", "IdGame_ID_Game");
            AddForeignKey("dbo.PlayerGames", "IdGamer_ID", "dbo.Gamers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PlayerGames", "IdGame_ID_Game", "dbo.Games", "ID_Game", cascadeDelete: true);
        }
    }
}
