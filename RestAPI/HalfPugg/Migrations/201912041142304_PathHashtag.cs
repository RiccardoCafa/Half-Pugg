namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PathHashtag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HashTags", "PathImg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HashTags", "PathImg");
        }
    }
}
