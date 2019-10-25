namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.Halls", "Player_ID", "dbo.Players");
            DropIndex("dbo.Players", new[] { "Hall_ID_Hall" });
            DropIndex("dbo.Halls", new[] { "Player_ID" });
            DropColumn("dbo.Players", "Hall_ID_Hall");
            DropColumn("dbo.Halls", "Player_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Halls", "Player_ID", c => c.Int());
            AddColumn("dbo.Players", "Hall_ID_Hall", c => c.Int());
            CreateIndex("dbo.Halls", "Player_ID");
            CreateIndex("dbo.Players", "Hall_ID_Hall");
            AddForeignKey("dbo.Halls", "Player_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.Players", "Hall_ID_Hall", "dbo.Halls", "ID_Hall");
        }
    }
}
