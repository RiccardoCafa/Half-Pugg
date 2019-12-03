namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refatoracao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilterGames", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.FilterGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.Groups", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Halls", "IdAdmin", "dbo.Players");
            DropForeignKey("dbo.HallFilters", "Hall_ID", "dbo.Halls");
            DropForeignKey("dbo.HallFilters", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Halls", "IdGame", "dbo.Games");
            DropForeignKey("dbo.PlayerHalls", "IdHall", "dbo.Halls");
            DropForeignKey("dbo.PlayerHalls", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.Labels", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.MessageHalls", "ID_Relation", "dbo.PlayerHalls");
            DropForeignKey("dbo.Numbereds", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.PlayerRequestedGroups", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.Ranges", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.RequestedGroups", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.RequestedHalls", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.RequestedHalls", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.RequestedHalls", "IdSala", "dbo.Halls");
            DropForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters");
            DropIndex("dbo.Groups", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.Halls", new[] { "IdGame" });
            DropIndex("dbo.Halls", new[] { "IdAdmin" });
            DropIndex("dbo.PlayerHalls", new[] { "IdPlayer" });
            DropIndex("dbo.PlayerHalls", new[] { "IdHall" });
            DropIndex("dbo.Labels", new[] { "ID_Filter" });
            DropIndex("dbo.MessageHalls", new[] { "ID_Relation" });
            DropIndex("dbo.Numbereds", new[] { "ID_Filter" });
            DropIndex("dbo.PlayerRequestedGroups", new[] { "IdFilters" });
            DropIndex("dbo.Ranges", new[] { "ID_Filter" });
            DropIndex("dbo.RequestedGroups", new[] { "IdFilters" });
            DropIndex("dbo.RequestedHalls", new[] { "IdPlayer" });
            DropIndex("dbo.RequestedHalls", new[] { "IdSala" });
            DropIndex("dbo.RequestedHalls", new[] { "IdFilters" });
            DropIndex("dbo.RequestedMatches", new[] { "IdFilters" });
            DropIndex("dbo.FilterGames", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.FilterGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.HallFilters", new[] { "Hall_ID" });
            DropIndex("dbo.HallFilters", new[] { "Filter_ID_Filter" });
            AddColumn("dbo.Players", "MPoints", c => c.String());
            AddColumn("dbo.MessageGroups", "IdGroup", c => c.Int(nullable: true));
            CreateIndex("dbo.MessageGroups", "IdGroup");
            AddForeignKey("dbo.MessageGroups", "IdGroup", "dbo.Groups", "ID", cascadeDelete: true);
            DropColumn("dbo.Groups", "Filter_ID_Filter");
            DropColumn("dbo.PlayerRequestedGroups", "IdFilters");
            DropColumn("dbo.RequestedGroups", "IdFilters");
            DropColumn("dbo.RequestedMatches", "IdFilters");
            DropTable("dbo.Filters");
            DropTable("dbo.Halls");
            DropTable("dbo.PlayerHalls");
            DropTable("dbo.Classification_Match");
            DropTable("dbo.Labels");
            DropTable("dbo.MessageHalls");
            DropTable("dbo.Numbereds");
            DropTable("dbo.Ranges");
            DropTable("dbo.RequestedHalls");
            DropTable("dbo.FilterGames");
            DropTable("dbo.HallFilters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HallFilters",
                c => new
                    {
                        Hall_ID = c.Int(nullable: false),
                        Filter_ID_Filter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hall_ID, t.Filter_ID_Filter });
            
            CreateTable(
                "dbo.FilterGames",
                c => new
                    {
                        Filter_ID_Filter = c.Int(nullable: false),
                        Game_ID_Game = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Filter_ID_Filter, t.Game_ID_Game });
            
            CreateTable(
                "dbo.RequestedHalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer = c.Int(nullable: false),
                        IdSala = c.Int(nullable: false),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1),
                        IdFilters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ranges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ID_Filter = c.Int(nullable: false),
                        Max = c.Single(nullable: false),
                        Min = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Numbereds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ID_Filter = c.Int(nullable: false),
                        Number = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MessageHalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        Send_Time = c.DateTime(nullable: false),
                        ID_Relation = c.Int(nullable: false),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Labels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ID_Filter = c.Int(nullable: false),
                        NameLabel = c.String(nullable: false, maxLength: 50),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Classification_Match",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PlayerHalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer = c.Int(nullable: false),
                        IdHall = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Capacity = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        IdGame = c.Int(nullable: false),
                        IdAdmin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Filters",
                c => new
                    {
                        ID_Filter = c.Int(nullable: false, identity: true),
                        NameFilter = c.String(nullable: false, maxLength: 100),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Filter);
            
            AddColumn("dbo.RequestedMatches", "IdFilters", c => c.Int());
            AddColumn("dbo.RequestedGroups", "IdFilters", c => c.Int(nullable: false));
            AddColumn("dbo.PlayerRequestedGroups", "IdFilters", c => c.Int(nullable: false));
            AddColumn("dbo.Groups", "Filter_ID_Filter", c => c.Int());
            DropForeignKey("dbo.MessageGroups", "IdGroup", "dbo.Groups");
            DropIndex("dbo.MessageGroups", new[] { "IdGroup" });
            DropColumn("dbo.MessageGroups", "IdGroup");
            DropColumn("dbo.Players", "MPoints");
            CreateIndex("dbo.HallFilters", "Filter_ID_Filter");
            CreateIndex("dbo.HallFilters", "Hall_ID");
            CreateIndex("dbo.FilterGames", "Game_ID_Game");
            CreateIndex("dbo.FilterGames", "Filter_ID_Filter");
            CreateIndex("dbo.RequestedMatches", "IdFilters");
            CreateIndex("dbo.RequestedHalls", "IdFilters");
            CreateIndex("dbo.RequestedHalls", "IdSala");
            CreateIndex("dbo.RequestedHalls", "IdPlayer");
            CreateIndex("dbo.RequestedGroups", "IdFilters");
            CreateIndex("dbo.Ranges", "ID_Filter");
            CreateIndex("dbo.PlayerRequestedGroups", "IdFilters");
            CreateIndex("dbo.Numbereds", "ID_Filter");
            CreateIndex("dbo.MessageHalls", "ID_Relation");
            CreateIndex("dbo.Labels", "ID_Filter");
            CreateIndex("dbo.PlayerHalls", "IdHall");
            CreateIndex("dbo.PlayerHalls", "IdPlayer");
            CreateIndex("dbo.Halls", "IdAdmin");
            CreateIndex("dbo.Halls", "IdGame");
            CreateIndex("dbo.Groups", "Filter_ID_Filter");
            AddForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters", "ID_Filter");
            AddForeignKey("dbo.RequestedHalls", "IdSala", "dbo.Halls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RequestedHalls", "IdPlayer", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RequestedHalls", "IdFilters", "dbo.Filters", "ID_Filter", cascadeDelete: true);
            AddForeignKey("dbo.RequestedGroups", "IdFilters", "dbo.Filters", "ID_Filter", cascadeDelete: true);
            AddForeignKey("dbo.Ranges", "ID_Filter", "dbo.Filters", "ID_Filter", cascadeDelete: true);
            AddForeignKey("dbo.PlayerRequestedGroups", "IdFilters", "dbo.Filters", "ID_Filter", cascadeDelete: true);
            AddForeignKey("dbo.Numbereds", "ID_Filter", "dbo.Filters", "ID_Filter", cascadeDelete: true);
            AddForeignKey("dbo.MessageHalls", "ID_Relation", "dbo.PlayerHalls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Labels", "ID_Filter", "dbo.Filters", "ID_Filter", cascadeDelete: true);
            AddForeignKey("dbo.PlayerHalls", "IdPlayer", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PlayerHalls", "IdHall", "dbo.Halls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Halls", "IdGame", "dbo.Games", "ID_Game", cascadeDelete: true);
            AddForeignKey("dbo.HallFilters", "Filter_ID_Filter", "dbo.Filters", "ID_Filter", cascadeDelete: true);
            AddForeignKey("dbo.HallFilters", "Hall_ID", "dbo.Halls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Halls", "IdAdmin", "dbo.Players", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Groups", "Filter_ID_Filter", "dbo.Filters", "ID_Filter");
            AddForeignKey("dbo.FilterGames", "Game_ID_Game", "dbo.Games", "ID_Game", cascadeDelete: true);
            AddForeignKey("dbo.FilterGames", "Filter_ID_Filter", "dbo.Filters", "ID_Filter", cascadeDelete: true);
        }
    }
}
