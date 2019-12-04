namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Components : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Groups", "TotalComponentes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "TotalComponentes", c => c.String());
        }
    }
}
