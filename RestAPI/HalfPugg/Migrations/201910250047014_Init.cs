namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classification_Gamer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Classification_Player",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
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
                "dbo.ClassificationPlayers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer = c.Int(nullable: false),
                        IdClassification = c.Int(nullable: false),
                        Points = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classification_Gamer", t => t.IdClassification, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .Index(t => t.IdPlayer)
                .Index(t => t.IdClassification);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 70),
                        Nickname = c.String(nullable: false, maxLength: 50),
                        HashPassword = c.String(nullable: false, maxLength: 100),
                        Bio = c.String(maxLength: 300),
                        Email = c.String(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        ImagePath = c.String(maxLength: 100),
                        ID_Branch = c.Int(nullable: false),
                        Slogan = c.String(),
                        Genre = c.String(maxLength: 100),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Group_ID_Group = c.Int(),
                        Hall_ID_Hall = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.Group_ID_Group)
                .ForeignKey("dbo.Halls", t => t.Hall_ID_Hall)
                .Index(t => t.Nickname, unique: true)
                .Index(t => t.Group_ID_Group)
                .Index(t => t.Hall_ID_Hall);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID_Group = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        IdGame = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        IdAdmin = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Filter_ID_Filter = c.Int(),
                        Player_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Group)
                .ForeignKey("dbo.Players", t => t.IdAdmin, cascadeDelete: true)
                .ForeignKey("dbo.Filters", t => t.Filter_ID_Filter)
                .ForeignKey("dbo.Games", t => t.IdGame, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_ID)
                .Index(t => t.IdGame)
                .Index(t => t.IdAdmin)
                .Index(t => t.Filter_ID_Filter)
                .Index(t => t.Player_ID);
            
            CreateTable(
                "dbo.MessageGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        Send_Time = c.DateTime(nullable: false),
                        View_Time = c.DateTime(nullable: false),
                        ID_User = c.Int(nullable: false),
                        ID_Recipient = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.ID_Recipient, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.ID_User, cascadeDelete: true)
                .Index(t => t.ID_User)
                .Index(t => t.ID_Recipient);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID_Game = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Description = c.String(nullable: false, maxLength: 70),
                        EndPoint = c.String(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Game);
            
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
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        ID_Hall = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 70),
                        IdGame = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        IdAdmin = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Player_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Hall)
                .ForeignKey("dbo.Players", t => t.IdAdmin, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.IdGame, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.Player_ID)
                .Index(t => t.IdGame)
                .Index(t => t.IdAdmin)
                .Index(t => t.Player_ID);
            
            CreateTable(
                "dbo.MessageHalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 400),
                        Send_Time = c.DateTime(nullable: false),
                        View_Time = c.DateTime(nullable: false),
                        ID_User = c.Int(nullable: false),
                        ID_Recipient = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Halls", t => t.ID_Recipient, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.ID_User, cascadeDelete: true)
                .Index(t => t.ID_User)
                .Index(t => t.ID_Recipient);
            
            CreateTable(
                "dbo.HashTags",
                c => new
                    {
                        ID_Matter = c.Int(nullable: false, identity: true),
                        Hashtag = c.String(nullable: false, maxLength: 70),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Matter);
            
            CreateTable(
                "dbo.GameInGames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdClassification = c.Int(nullable: false),
                        IdPlayerGame = c.Int(nullable: false),
                        Points = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classification_Gamer", t => t.IdClassification, cascadeDelete: true)
                .ForeignKey("dbo.PlayerGames", t => t.IdPlayerGame, cascadeDelete: true)
                .Index(t => t.IdClassification)
                .Index(t => t.IdPlayerGame);
            
            CreateTable(
                "dbo.PlayerGames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 300),
                        IDGame = c.Int(nullable: false),
                        IDGamer = c.Int(nullable: false),
                        IdAPI = c.String(nullable: false),
                        Weight = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.IDGame, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IDGamer, cascadeDelete: true)
                .Index(t => t.IDGame)
                .Index(t => t.IDGamer)
                .Index(t => t.IdAPI, unique: true);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.ID_Filter, cascadeDelete: true)
                .Index(t => t.ID_Filter);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer1 = c.Int(nullable: false),
                        IdPlayer2 = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Weight = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Player1_ID = c.Int(),
                        Player2_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Players", t => t.Player1_ID)
                .ForeignKey("dbo.Players", t => t.Player2_ID)
                .Index(t => t.Player1_ID)
                .Index(t => t.Player2_ID);
            
            CreateTable(
                "dbo.MessagePlayers",
                c => new
                    {
                        ID_Message = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 400),
                        ID_User = c.Int(nullable: false),
                        ID_Recipient = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Message);
            
            CreateTable(
                "dbo.Numbereds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        ID_Filter_ID_Filter = c.Int(nullable: false),
                        IDFilter_ID_Filter = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.ID_Filter_ID_Filter, cascadeDelete: true)
                .ForeignKey("dbo.Filters", t => t.IDFilter_ID_Filter)
                .Index(t => t.ID_Filter_ID_Filter)
                .Index(t => t.IDFilter_ID_Filter);
            
            CreateTable(
                "dbo.PlayerHashtags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdHash = c.Int(nullable: false),
                        IdPlayer = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HashTags", t => t.IdHash, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .Index(t => t.IdHash)
                .Index(t => t.IdPlayer);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.ID_Filter, cascadeDelete: true)
                .Index(t => t.ID_Filter);
            
            CreateTable(
                "dbo.RequestedGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer = c.Int(nullable: false),
                        IdSala = c.Int(nullable: false),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        IdFilters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.IdFilters, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.IdSala, cascadeDelete: true)
                .Index(t => t.IdPlayer)
                .Index(t => t.IdSala)
                .Index(t => t.IdFilters);
            
            CreateTable(
                "dbo.RequestedHalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer = c.Int(nullable: false),
                        IdSala = c.Int(nullable: false),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        IdFilters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.IdFilters, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.IdSala, cascadeDelete: true)
                .Index(t => t.IdPlayer)
                .Index(t => t.IdSala)
                .Index(t => t.IdFilters);
            
            CreateTable(
                "dbo.RequestedMatches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer = c.Int(nullable: false),
                        IdPlayer2 = c.Int(nullable: false),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        IdFilters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.IdFilters, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.IdPlayer2, cascadeDelete: true)
                .Index(t => t.IdPlayer)
                .Index(t => t.IdPlayer2)
                .Index(t => t.IdFilters);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        ID_Template = c.Int(nullable: false, identity: true),
                        IdGame = c.Int(nullable: false),
                        Path = c.String(nullable: false, maxLength: 100),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Game_ID_Game = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Template)
                .ForeignKey("dbo.Games", t => t.Game_ID_Game)
                .Index(t => t.Game_ID_Game);
            
            CreateTable(
                "dbo.FilterGames",
                c => new
                    {
                        Filter_ID_Filter = c.Int(nullable: false),
                        Game_ID_Game = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Filter_ID_Filter, t.Game_ID_Game })
                .ForeignKey("dbo.Filters", t => t.Filter_ID_Filter, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_ID_Game, cascadeDelete: true)
                .Index(t => t.Filter_ID_Filter)
                .Index(t => t.Game_ID_Game);
            
            CreateTable(
                "dbo.HallFilters",
                c => new
                    {
                        Hall_ID_Hall = c.Int(nullable: false),
                        Filter_ID_Filter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hall_ID_Hall, t.Filter_ID_Filter })
                .ForeignKey("dbo.Halls", t => t.Hall_ID_Hall, cascadeDelete: true)
                .ForeignKey("dbo.Filters", t => t.Filter_ID_Filter, cascadeDelete: true)
                .Index(t => t.Hall_ID_Hall)
                .Index(t => t.Filter_ID_Filter);
            
            CreateTable(
                "dbo.HashTagGames",
                c => new
                    {
                        HashTag_ID_Matter = c.Int(nullable: false),
                        Game_ID_Game = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HashTag_ID_Matter, t.Game_ID_Game })
                .ForeignKey("dbo.HashTags", t => t.HashTag_ID_Matter, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_ID_Game, cascadeDelete: true)
                .Index(t => t.HashTag_ID_Matter)
                .Index(t => t.Game_ID_Game);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Templates", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.RequestedMatches", "IdPlayer2", "dbo.Groups");
            DropForeignKey("dbo.RequestedMatches", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.RequestedHalls", "IdSala", "dbo.Groups");
            DropForeignKey("dbo.RequestedHalls", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.RequestedHalls", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.RequestedGroups", "IdSala", "dbo.Groups");
            DropForeignKey("dbo.RequestedGroups", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.RequestedGroups", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.Ranges", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.PlayerHashtags", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.PlayerHashtags", "IdHash", "dbo.HashTags");
            DropForeignKey("dbo.Numbereds", "IDFilter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Numbereds", "ID_Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Matches", "Player2_ID", "dbo.Players");
            DropForeignKey("dbo.Matches", "Player1_ID", "dbo.Players");
            DropForeignKey("dbo.Labels", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.GameInGames", "IdPlayerGame", "dbo.PlayerGames");
            DropForeignKey("dbo.PlayerGames", "IDGamer", "dbo.Players");
            DropForeignKey("dbo.PlayerGames", "IDGame", "dbo.Games");
            DropForeignKey("dbo.GameInGames", "IdClassification", "dbo.Classification_Gamer");
            DropForeignKey("dbo.ClassificationPlayers", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.Halls", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.Groups", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.Groups", "IdGame", "dbo.Games");
            DropForeignKey("dbo.HashTagGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.HashTagGames", "HashTag_ID_Matter", "dbo.HashTags");
            DropForeignKey("dbo.Halls", "IdGame", "dbo.Games");
            DropForeignKey("dbo.HallFilters", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.HallFilters", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.Players", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.MessageHalls", "ID_User", "dbo.Players");
            DropForeignKey("dbo.MessageHalls", "ID_Recipient", "dbo.Halls");
            DropForeignKey("dbo.Halls", "IdAdmin", "dbo.Players");
            DropForeignKey("dbo.Groups", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.FilterGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.FilterGames", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Players", "Group_ID_Group", "dbo.Groups");
            DropForeignKey("dbo.MessageGroups", "ID_User", "dbo.Players");
            DropForeignKey("dbo.MessageGroups", "ID_Recipient", "dbo.Groups");
            DropForeignKey("dbo.Groups", "IdAdmin", "dbo.Players");
            DropForeignKey("dbo.ClassificationPlayers", "IdClassification", "dbo.Classification_Gamer");
            DropIndex("dbo.HashTagGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.HashTagGames", new[] { "HashTag_ID_Matter" });
            DropIndex("dbo.HallFilters", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.HallFilters", new[] { "Hall_ID_Hall" });
            DropIndex("dbo.FilterGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.FilterGames", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.Templates", new[] { "Game_ID_Game" });
            DropIndex("dbo.RequestedMatches", new[] { "IdFilters" });
            DropIndex("dbo.RequestedMatches", new[] { "IdPlayer2" });
            DropIndex("dbo.RequestedMatches", new[] { "IdPlayer" });
            DropIndex("dbo.RequestedHalls", new[] { "IdFilters" });
            DropIndex("dbo.RequestedHalls", new[] { "IdSala" });
            DropIndex("dbo.RequestedHalls", new[] { "IdPlayer" });
            DropIndex("dbo.RequestedGroups", new[] { "IdFilters" });
            DropIndex("dbo.RequestedGroups", new[] { "IdSala" });
            DropIndex("dbo.RequestedGroups", new[] { "IdPlayer" });
            DropIndex("dbo.Ranges", new[] { "ID_Filter" });
            DropIndex("dbo.PlayerHashtags", new[] { "IdPlayer" });
            DropIndex("dbo.PlayerHashtags", new[] { "IdHash" });
            DropIndex("dbo.Numbereds", new[] { "IDFilter_ID_Filter" });
            DropIndex("dbo.Numbereds", new[] { "ID_Filter_ID_Filter" });
            DropIndex("dbo.Matches", new[] { "Player2_ID" });
            DropIndex("dbo.Matches", new[] { "Player1_ID" });
            DropIndex("dbo.Labels", new[] { "ID_Filter" });
            DropIndex("dbo.PlayerGames", new[] { "IdAPI" });
            DropIndex("dbo.PlayerGames", new[] { "IDGamer" });
            DropIndex("dbo.PlayerGames", new[] { "IDGame" });
            DropIndex("dbo.GameInGames", new[] { "IdPlayerGame" });
            DropIndex("dbo.GameInGames", new[] { "IdClassification" });
            DropIndex("dbo.MessageHalls", new[] { "ID_Recipient" });
            DropIndex("dbo.MessageHalls", new[] { "ID_User" });
            DropIndex("dbo.Halls", new[] { "Player_ID" });
            DropIndex("dbo.Halls", new[] { "IdAdmin" });
            DropIndex("dbo.Halls", new[] { "IdGame" });
            DropIndex("dbo.MessageGroups", new[] { "ID_Recipient" });
            DropIndex("dbo.MessageGroups", new[] { "ID_User" });
            DropIndex("dbo.Groups", new[] { "Player_ID" });
            DropIndex("dbo.Groups", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.Groups", new[] { "IdAdmin" });
            DropIndex("dbo.Groups", new[] { "IdGame" });
            DropIndex("dbo.Players", new[] { "Hall_ID_Hall" });
            DropIndex("dbo.Players", new[] { "Group_ID_Group" });
            DropIndex("dbo.Players", new[] { "Nickname" });
            DropIndex("dbo.ClassificationPlayers", new[] { "IdClassification" });
            DropIndex("dbo.ClassificationPlayers", new[] { "IdPlayer" });
            DropTable("dbo.HashTagGames");
            DropTable("dbo.HallFilters");
            DropTable("dbo.FilterGames");
            DropTable("dbo.Templates");
            DropTable("dbo.RequestedMatches");
            DropTable("dbo.RequestedHalls");
            DropTable("dbo.RequestedGroups");
            DropTable("dbo.Ranges");
            DropTable("dbo.PlayerHashtags");
            DropTable("dbo.Numbereds");
            DropTable("dbo.MessagePlayers");
            DropTable("dbo.Matches");
            DropTable("dbo.Labels");
            DropTable("dbo.PlayerGames");
            DropTable("dbo.GameInGames");
            DropTable("dbo.HashTags");
            DropTable("dbo.MessageHalls");
            DropTable("dbo.Halls");
            DropTable("dbo.Filters");
            DropTable("dbo.Games");
            DropTable("dbo.MessageGroups");
            DropTable("dbo.Groups");
            DropTable("dbo.Players");
            DropTable("dbo.ClassificationPlayers");
            DropTable("dbo.Classification_Match");
            DropTable("dbo.Classification_Player");
            DropTable("dbo.Classification_Gamer");
        }
    }
}
