namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refatoracao3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "MPoints", c => c.Single(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "MPoints");
        }
    }
}
