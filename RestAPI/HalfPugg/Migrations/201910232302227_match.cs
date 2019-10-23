namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class match : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RequestedMatches", name: "Sala_ID", newName: "Player2_ID");
            RenameIndex(table: "dbo.RequestedMatches", name: "IX_Sala_ID", newName: "IX_Player2_ID");
            AddColumn("dbo.RequestedMatches", "IdPlayer2", c => c.Int(nullable: false));
            DropColumn("dbo.RequestedMatches", "IdSala");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestedMatches", "IdSala", c => c.Int(nullable: false));
            DropColumn("dbo.RequestedMatches", "IdPlayer2");
            RenameIndex(table: "dbo.RequestedMatches", name: "IX_Player2_ID", newName: "IX_Sala_ID");
            RenameColumn(table: "dbo.RequestedMatches", name: "Player2_ID", newName: "Sala_ID");
        }
    }
}
