namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PathLen1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Players", "ImagePath", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Players", "ImagePath", c => c.String(maxLength: 1000));
        }
    }
}
