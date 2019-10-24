namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GamertoPlayer : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Gamers", newName: "Players");
            RenameColumn(table: "dbo.Groups", name: "Gamer_ID", newName: "Player_ID");
            RenameColumn(table: "dbo.Halls", name: "Gamer_ID", newName: "Player_ID");
            RenameIndex(table: "dbo.Groups", name: "IX_Gamer_ID", newName: "IX_Player_ID");
            RenameIndex(table: "dbo.Halls", name: "IX_Gamer_ID", newName: "IX_Player_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Halls", name: "IX_Player_ID", newName: "IX_Gamer_ID");
            RenameIndex(table: "dbo.Groups", name: "IX_Player_ID", newName: "IX_Gamer_ID");
            RenameColumn(table: "dbo.Halls", name: "Player_ID", newName: "Gamer_ID");
            RenameColumn(table: "dbo.Groups", name: "Player_ID", newName: "Gamer_ID");
            RenameTable(name: "dbo.Players", newName: "Gamers");
        }
    }
}
