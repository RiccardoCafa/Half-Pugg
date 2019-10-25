namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HallPlayer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "IdAdmin", "dbo.Players");
            DropForeignKey("dbo.Halls", "IdAdmin", "dbo.Players");
            AddColumn("dbo.Players", "Group_ID_Group", c => c.Int());
            AddColumn("dbo.Players", "Hall_ID_Hall", c => c.Int());
            AddColumn("dbo.Groups", "Player_ID", c => c.Int());
            AddColumn("dbo.Halls", "Player_ID", c => c.Int());
            CreateIndex("dbo.Players", "Group_ID_Group");
            CreateIndex("dbo.Players", "Hall_ID_Hall");
            CreateIndex("dbo.Groups", "Player_ID");
            CreateIndex("dbo.Halls", "Player_ID");
            AddForeignKey("dbo.Players", "Group_ID_Group", "dbo.Groups", "ID_Group");
            AddForeignKey("dbo.Players", "Hall_ID_Hall", "dbo.Halls", "ID_Hall");
            AddForeignKey("dbo.Groups", "Player_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.Halls", "Player_ID", "dbo.Players", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Halls", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.Groups", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.Players", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.Players", "Group_ID_Group", "dbo.Groups");
            DropIndex("dbo.Halls", new[] { "Player_ID" });
            DropIndex("dbo.Groups", new[] { "Player_ID" });
            DropIndex("dbo.Players", new[] { "Hall_ID_Hall" });
            DropIndex("dbo.Players", new[] { "Group_ID_Group" });
            DropColumn("dbo.Halls", "Player_ID");
            DropColumn("dbo.Groups", "Player_ID");
            DropColumn("dbo.Players", "Hall_ID_Hall");
            DropColumn("dbo.Players", "Group_ID_Group");
            AddForeignKey("dbo.Halls", "IdAdmin", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Groups", "IdAdmin", "dbo.Players", "ID", cascadeDelete: true);
        }
    }
}
