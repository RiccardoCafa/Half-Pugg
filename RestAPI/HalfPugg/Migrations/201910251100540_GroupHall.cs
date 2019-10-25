namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupHall : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Players", "Group_ID_Group", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Player_ID", "dbo.Players");
            DropIndex("dbo.Players", new[] { "Group_ID_Group" });
            DropIndex("dbo.Groups", new[] { "IdAdmin" });
            DropIndex("dbo.Groups", new[] { "Player_ID" });
            DropColumn("dbo.Groups", "IdAdmin");
            RenameColumn(table: "dbo.Groups", name: "Player_ID", newName: "IdAdmin");
            AlterColumn("dbo.Groups", "IdAdmin", c => c.Int(nullable: false));
            CreateIndex("dbo.Groups", "IdAdmin");
            AddForeignKey("dbo.Groups", "IdAdmin", "dbo.Players", "ID", cascadeDelete: true);
            DropColumn("dbo.Players", "Group_ID_Group");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "Group_ID_Group", c => c.Int());
            DropForeignKey("dbo.Groups", "IdAdmin", "dbo.Players");
            DropIndex("dbo.Groups", new[] { "IdAdmin" });
            AlterColumn("dbo.Groups", "IdAdmin", c => c.Int());
            RenameColumn(table: "dbo.Groups", name: "IdAdmin", newName: "Player_ID");
            AddColumn("dbo.Groups", "IdAdmin", c => c.Int(nullable: false));
            CreateIndex("dbo.Groups", "Player_ID");
            CreateIndex("dbo.Groups", "IdAdmin");
            CreateIndex("dbo.Players", "Group_ID_Group");
            AddForeignKey("dbo.Groups", "Player_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.Players", "Group_ID_Group", "dbo.Groups", "ID_Group");
        }
    }
}
