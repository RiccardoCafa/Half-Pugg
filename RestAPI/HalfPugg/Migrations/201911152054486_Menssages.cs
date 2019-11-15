namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Menssages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerGroups", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlayerGroups", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlayerHalls", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.PlayerHalls", "AlteredAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlayerHalls", "AlteredAt");
            DropColumn("dbo.PlayerHalls", "CreateAt");
            DropColumn("dbo.PlayerGroups", "AlteredAt");
            DropColumn("dbo.PlayerGroups", "CreateAt");
        }
    }
}
