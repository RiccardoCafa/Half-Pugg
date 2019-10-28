namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Sex", c => c.String(nullable: false, maxLength: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Sex");
        }
    }
}
