namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassificationPlayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassificationPlayers", "IdJudgePlayer", c => c.Int(nullable: false));
            CreateIndex("dbo.ClassificationPlayers", "IdJudgePlayer");
            AddForeignKey("dbo.ClassificationPlayers", "IdJudgePlayer", "dbo.Players", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassificationPlayers", "IdJudgePlayer", "dbo.Players");
            DropIndex("dbo.ClassificationPlayers", new[] { "IdJudgePlayer" });
            DropColumn("dbo.ClassificationPlayers", "IdJudgePlayer");
        }
    }
}
