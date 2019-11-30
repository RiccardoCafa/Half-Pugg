namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatConnection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChatConnections", "Player_ID", "dbo.Players");
            DropIndex("dbo.ChatConnections", new[] { "Player_ID" });
            RenameColumn(table: "dbo.ChatConnections", name: "Player_ID", newName: "IdPlayer");
            AddColumn("dbo.ChatConnections", "CreateAt", c => c.DateTime());
            AddColumn("dbo.Groups", "SouceImg", c => c.String());
            AddColumn("dbo.Groups", "TotalComponentes", c => c.String());
            AlterColumn("dbo.ChatConnections", "IdPlayer", c => c.Int(nullable: false));
            CreateIndex("dbo.ChatConnections", "IdPlayer");
            AddForeignKey("dbo.ChatConnections", "IdPlayer", "dbo.Players", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatConnections", "IdPlayer", "dbo.Players");
            DropIndex("dbo.ChatConnections", new[] { "IdPlayer" });
            AlterColumn("dbo.ChatConnections", "IdPlayer", c => c.Int());
            DropColumn("dbo.Groups", "TotalComponentes");
            DropColumn("dbo.Groups", "SouceImg");
            DropColumn("dbo.ChatConnections", "CreateAt");
            RenameColumn(table: "dbo.ChatConnections", name: "IdPlayer", newName: "Player_ID");
            CreateIndex("dbo.ChatConnections", "Player_ID");
            AddForeignKey("dbo.ChatConnections", "Player_ID", "dbo.Players", "ID");
        }
    }
}
