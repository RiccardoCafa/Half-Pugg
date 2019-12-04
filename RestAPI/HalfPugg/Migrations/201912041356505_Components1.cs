namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Components1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "TotalComponentes", c => c.Single(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "TotalComponentes");
        }
    }
}
