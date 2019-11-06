namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Procedure4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters");
            DropIndex("dbo.RequestedMatches", new[] { "IdFilters" });
            AlterColumn("dbo.RequestedMatches", "RequestedTime", c => c.DateTime());
            AlterColumn("dbo.RequestedMatches", "ComfirmedTime", c => c.DateTime());
            AlterColumn("dbo.RequestedMatches", "IdFilters", c => c.Int());
            CreateIndex("dbo.RequestedMatches", "IdFilters");
            AddForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters", "ID_Filter");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters");
            DropIndex("dbo.RequestedMatches", new[] { "IdFilters" });
            AlterColumn("dbo.RequestedMatches", "IdFilters", c => c.Int(nullable: false));
            AlterColumn("dbo.RequestedMatches", "ComfirmedTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RequestedMatches", "RequestedTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.RequestedMatches", "IdFilters");
            AddForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters", "ID_Filter", cascadeDelete: true);
        }
    }
}
