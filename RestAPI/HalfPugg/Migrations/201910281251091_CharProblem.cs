namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CharProblem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Numbereds", "IDFilter_ID_Filter", "dbo.Filters");
            DropIndex("dbo.Numbereds", new[] { "IDFilter_ID_Filter" });
            RenameColumn(table: "dbo.Numbereds", name: "ID_Filter_ID_Filter", newName: "ID_Filter");
            RenameIndex(table: "dbo.Numbereds", name: "IX_ID_Filter_ID_Filter", newName: "IX_ID_Filter");
            AddColumn("dbo.Players", "Type", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.Players", "Sex", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.MessageHalls", "Status", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.MessagePlayers", "Status", c => c.String(nullable: false, maxLength: 1));
            AddColumn("dbo.MessageGroups", "Status", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.RequestedGroups", "Status", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.RequestedHalls", "Status", c => c.String(nullable: false, maxLength: 1));
            DropColumn("dbo.Numbereds", "IDFilter_ID_Filter");
            DropColumn("dbo.RequestedMatches", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestedMatches", "Status", c => c.String(nullable: false));
            AddColumn("dbo.Numbereds", "IDFilter_ID_Filter", c => c.Int());
            AlterColumn("dbo.RequestedHalls", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.RequestedGroups", "Status", c => c.String(nullable: false));
            DropColumn("dbo.MessageGroups", "Status");
            DropColumn("dbo.MessagePlayers", "Status");
            DropColumn("dbo.MessageHalls", "Status");
            DropColumn("dbo.Players", "Sex");
            DropColumn("dbo.Players", "Type");
            RenameIndex(table: "dbo.Numbereds", name: "IX_ID_Filter", newName: "IX_ID_Filter_ID_Filter");
            RenameColumn(table: "dbo.Numbereds", name: "ID_Filter", newName: "ID_Filter_ID_Filter");
            CreateIndex("dbo.Numbereds", "IDFilter_ID_Filter");
            AddForeignKey("dbo.Numbereds", "IDFilter_ID_Filter", "dbo.Filters", "ID_Filter");
        }
    }
}
