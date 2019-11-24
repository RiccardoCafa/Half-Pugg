namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatConnections",
                c => new
                    {
                        ConnectionID = c.String(nullable: false, maxLength: 128),
                        Connected = c.Boolean(nullable: false),
                        Player_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ConnectionID)
                .ForeignKey("dbo.Players", t => t.Player_ID)
                .Index(t => t.Player_ID);
            
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
                        IdJudgePlayer = c.Int(nullable: false),
                        IdClassification = c.Int(nullable: false),
                        Points = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classification_Gamer", t => t.IdClassification, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdJudgePlayer, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .Index(t => t.IdPlayer)
                .Index(t => t.IdJudgePlayer)
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
                        ImagePath = c.String(maxLength: 500),
                        Type = c.String(nullable: false, maxLength: 1),
                        ID_Branch = c.String(),
                        Slogan = c.String(),
                        Sex = c.String(nullable: false, maxLength: 1),
                        Genre = c.String(maxLength: 100),
                        CreateAt = c.DateTime(),
                        AlteredAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Nickname, unique: true);
            
            CreateTable(
                "dbo.PlayerGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdGroup = c.Int(nullable: false),
                        IdPlayer = c.Int(nullable: false),
                        CreateAt = c.DateTime(),
                        AlteredAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.IdGroup, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .Index(t => t.IdGroup)
                .Index(t => t.IdPlayer);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Capacity = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        IdGame = c.Int(nullable: false),
                        IdAdmin = c.Int(nullable: false),
                        Filter_ID_Filter = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Players", t => t.IdAdmin, cascadeDelete: true)
                .ForeignKey("dbo.Filters", t => t.Filter_ID_Filter)
                .ForeignKey("dbo.Games", t => t.IdGame, cascadeDelete: true)
                .Index(t => t.IdGame)
                .Index(t => t.IdAdmin)
                .Index(t => t.Filter_ID_Filter);
            
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
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Capacity = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        IdGame = c.Int(nullable: false),
                        IdAdmin = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Players", t => t.IdAdmin, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.IdGame, cascadeDelete: true)
                .Index(t => t.IdGame)
                .Index(t => t.IdAdmin);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Halls", t => t.IdHall, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .Index(t => t.IdPlayer)
                .Index(t => t.IdHall);
            
            CreateTable(
                "dbo.MessageHalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        Send_Time = c.DateTime(nullable: false),
                        ID_Sender = c.Int(nullable: false),
                        ID_Destination = c.Int(nullable: false),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Halls", t => t.ID_Destination, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.ID_Sender, cascadeDelete: true)
                .Index(t => t.ID_Sender)
                .Index(t => t.ID_Destination);
            
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
                "dbo.MessageGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        Send_Time = c.DateTime(nullable: false),
                        ID_Sender = c.Int(nullable: false),
                        ID_Destination = c.Int(nullable: false),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.ID_Destination, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.ID_Sender, cascadeDelete: true)
                .Index(t => t.ID_Sender)
                .Index(t => t.ID_Destination);
            
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
                .ForeignKey("dbo.Classification_Player", t => t.IdClassification, cascadeDelete: true)
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Players", t => t.IdPlayer1, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer2, cascadeDelete: true)
                .Index(t => t.IdPlayer1)
                .Index(t => t.IdPlayer2);
            
            CreateTable(
                "dbo.MessagePlayers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 500),
                        Send_Time = c.DateTime(nullable: false),
                        ID_Sender = c.Int(nullable: false),
                        ID_Destination = c.Int(nullable: false),
                        Status = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Players", t => t.ID_Destination, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.ID_Sender, cascadeDelete: true)
                .Index(t => t.ID_Sender)
                .Index(t => t.ID_Destination);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.ID_Filter, cascadeDelete: true)
                .Index(t => t.ID_Filter);
            
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
                "dbo.PlayerRequestedGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayerRequest = c.Int(nullable: false),
                        IdGroup = c.Int(nullable: false),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1),
                        IdFilters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.IdFilters, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.IdGroup, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayerRequest, cascadeDelete: true)
                .Index(t => t.IdPlayerRequest)
                .Index(t => t.IdGroup)
                .Index(t => t.IdFilters);
            
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
                        IdGroup = c.Int(nullable: false),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 1),
                        IdFilters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.IdFilters, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.IdGroup, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .Index(t => t.IdPlayer)
                .Index(t => t.IdGroup)
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
                        Status = c.String(nullable: false, maxLength: 1),
                        IdFilters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.IdFilters, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer, cascadeDelete: true)
                .ForeignKey("dbo.Halls", t => t.IdSala, cascadeDelete: true)
                .Index(t => t.IdPlayer)
                .Index(t => t.IdSala)
                .Index(t => t.IdFilters);
            
            CreateTable(
                "dbo.RequestedMatches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer1 = c.Int(nullable: false),
                        IdPlayer2 = c.Int(nullable: false),
                        RequestedTime = c.DateTime(),
                        ComfirmedTime = c.DateTime(),
                        Status = c.String(nullable: false, maxLength: 1),
                        IdFilters = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.IdFilters)
                .ForeignKey("dbo.Players", t => t.IdPlayer1, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.IdPlayer2, cascadeDelete: true)
                .Index(t => t.IdPlayer1)
                .Index(t => t.IdPlayer2)
                .Index(t => t.IdFilters);
            
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
                        Hall_ID = c.Int(nullable: false),
                        Filter_ID_Filter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hall_ID, t.Filter_ID_Filter })
                .ForeignKey("dbo.Halls", t => t.Hall_ID, cascadeDelete: true)
                .ForeignKey("dbo.Filters", t => t.Filter_ID_Filter, cascadeDelete: true)
                .Index(t => t.Hall_ID)
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
            DropForeignKey("dbo.RequestedMatches", "IdPlayer2", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "IdPlayer1", "dbo.Players");
            DropForeignKey("dbo.RequestedMatches", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.RequestedHalls", "IdSala", "dbo.Halls");
            DropForeignKey("dbo.RequestedHalls", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.RequestedHalls", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.RequestedGroups", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.RequestedGroups", "IdGroup", "dbo.Groups");
            DropForeignKey("dbo.RequestedGroups", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.Ranges", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.PlayerRequestedGroups", "IdPlayerRequest", "dbo.Players");
            DropForeignKey("dbo.PlayerRequestedGroups", "IdGroup", "dbo.Groups");
            DropForeignKey("dbo.PlayerRequestedGroups", "IdFilters", "dbo.Filters");
            DropForeignKey("dbo.PlayerHashtags", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.PlayerHashtags", "IdHash", "dbo.HashTags");
            DropForeignKey("dbo.Numbereds", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.MessagePlayers", "ID_Sender", "dbo.Players");
            DropForeignKey("dbo.MessagePlayers", "ID_Destination", "dbo.Players");
            DropForeignKey("dbo.Matches", "IdPlayer2", "dbo.Players");
            DropForeignKey("dbo.Matches", "IdPlayer1", "dbo.Players");
            DropForeignKey("dbo.Labels", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.GameInGames", "IdPlayerGame", "dbo.PlayerGames");
            DropForeignKey("dbo.PlayerGames", "IDGamer", "dbo.Players");
            DropForeignKey("dbo.PlayerGames", "IDGame", "dbo.Games");
            DropForeignKey("dbo.GameInGames", "IdClassification", "dbo.Classification_Player");
            DropForeignKey("dbo.ClassificationPlayers", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.ClassificationPlayers", "IdJudgePlayer", "dbo.Players");
            DropForeignKey("dbo.PlayerGroups", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.PlayerGroups", "IdGroup", "dbo.Groups");
            DropForeignKey("dbo.MessageGroups", "ID_Sender", "dbo.Players");
            DropForeignKey("dbo.MessageGroups", "ID_Destination", "dbo.Groups");
            DropForeignKey("dbo.Groups", "IdGame", "dbo.Games");
            DropForeignKey("dbo.HashTagGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.HashTagGames", "HashTag_ID_Matter", "dbo.HashTags");
            DropForeignKey("dbo.MessageHalls", "ID_Sender", "dbo.Players");
            DropForeignKey("dbo.MessageHalls", "ID_Destination", "dbo.Halls");
            DropForeignKey("dbo.PlayerHalls", "IdPlayer", "dbo.Players");
            DropForeignKey("dbo.PlayerHalls", "IdHall", "dbo.Halls");
            DropForeignKey("dbo.Halls", "IdGame", "dbo.Games");
            DropForeignKey("dbo.HallFilters", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.HallFilters", "Hall_ID", "dbo.Halls");
            DropForeignKey("dbo.Halls", "IdAdmin", "dbo.Players");
            DropForeignKey("dbo.Groups", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.FilterGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.FilterGames", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Groups", "IdAdmin", "dbo.Players");
            DropForeignKey("dbo.ChatConnections", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.ClassificationPlayers", "IdClassification", "dbo.Classification_Gamer");
            DropIndex("dbo.HashTagGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.HashTagGames", new[] { "HashTag_ID_Matter" });
            DropIndex("dbo.HallFilters", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.HallFilters", new[] { "Hall_ID" });
            DropIndex("dbo.FilterGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.FilterGames", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.RequestedMatches", new[] { "IdFilters" });
            DropIndex("dbo.RequestedMatches", new[] { "IdPlayer2" });
            DropIndex("dbo.RequestedMatches", new[] { "IdPlayer1" });
            DropIndex("dbo.RequestedHalls", new[] { "IdFilters" });
            DropIndex("dbo.RequestedHalls", new[] { "IdSala" });
            DropIndex("dbo.RequestedHalls", new[] { "IdPlayer" });
            DropIndex("dbo.RequestedGroups", new[] { "IdFilters" });
            DropIndex("dbo.RequestedGroups", new[] { "IdGroup" });
            DropIndex("dbo.RequestedGroups", new[] { "IdPlayer" });
            DropIndex("dbo.Ranges", new[] { "ID_Filter" });
            DropIndex("dbo.PlayerRequestedGroups", new[] { "IdFilters" });
            DropIndex("dbo.PlayerRequestedGroups", new[] { "IdGroup" });
            DropIndex("dbo.PlayerRequestedGroups", new[] { "IdPlayerRequest" });
            DropIndex("dbo.PlayerHashtags", new[] { "IdPlayer" });
            DropIndex("dbo.PlayerHashtags", new[] { "IdHash" });
            DropIndex("dbo.Numbereds", new[] { "ID_Filter" });
            DropIndex("dbo.MessagePlayers", new[] { "ID_Destination" });
            DropIndex("dbo.MessagePlayers", new[] { "ID_Sender" });
            DropIndex("dbo.Matches", new[] { "IdPlayer2" });
            DropIndex("dbo.Matches", new[] { "IdPlayer1" });
            DropIndex("dbo.Labels", new[] { "ID_Filter" });
            DropIndex("dbo.PlayerGames", new[] { "IdAPI" });
            DropIndex("dbo.PlayerGames", new[] { "IDGamer" });
            DropIndex("dbo.PlayerGames", new[] { "IDGame" });
            DropIndex("dbo.GameInGames", new[] { "IdPlayerGame" });
            DropIndex("dbo.GameInGames", new[] { "IdClassification" });
            DropIndex("dbo.MessageGroups", new[] { "ID_Destination" });
            DropIndex("dbo.MessageGroups", new[] { "ID_Sender" });
            DropIndex("dbo.MessageHalls", new[] { "ID_Destination" });
            DropIndex("dbo.MessageHalls", new[] { "ID_Sender" });
            DropIndex("dbo.PlayerHalls", new[] { "IdHall" });
            DropIndex("dbo.PlayerHalls", new[] { "IdPlayer" });
            DropIndex("dbo.Halls", new[] { "IdAdmin" });
            DropIndex("dbo.Halls", new[] { "IdGame" });
            DropIndex("dbo.Groups", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.Groups", new[] { "IdAdmin" });
            DropIndex("dbo.Groups", new[] { "IdGame" });
            DropIndex("dbo.PlayerGroups", new[] { "IdPlayer" });
            DropIndex("dbo.PlayerGroups", new[] { "IdGroup" });
            DropIndex("dbo.Players", new[] { "Nickname" });
            DropIndex("dbo.ClassificationPlayers", new[] { "IdClassification" });
            DropIndex("dbo.ClassificationPlayers", new[] { "IdJudgePlayer" });
            DropIndex("dbo.ClassificationPlayers", new[] { "IdPlayer" });
            DropIndex("dbo.ChatConnections", new[] { "Player_ID" });
            DropTable("dbo.HashTagGames");
            DropTable("dbo.HallFilters");
            DropTable("dbo.FilterGames");
            DropTable("dbo.RequestedMatches");
            DropTable("dbo.RequestedHalls");
            DropTable("dbo.RequestedGroups");
            DropTable("dbo.Ranges");
            DropTable("dbo.PlayerRequestedGroups");
            DropTable("dbo.PlayerHashtags");
            DropTable("dbo.Numbereds");
            DropTable("dbo.MessagePlayers");
            DropTable("dbo.Matches");
            DropTable("dbo.Labels");
            DropTable("dbo.PlayerGames");
            DropTable("dbo.GameInGames");
            DropTable("dbo.MessageGroups");
            DropTable("dbo.HashTags");
            DropTable("dbo.MessageHalls");
            DropTable("dbo.PlayerHalls");
            DropTable("dbo.Halls");
            DropTable("dbo.Filters");
            DropTable("dbo.Games");
            DropTable("dbo.Groups");
            DropTable("dbo.PlayerGroups");
            DropTable("dbo.Players");
            DropTable("dbo.ClassificationPlayers");
            DropTable("dbo.Classification_Match");
            DropTable("dbo.Classification_Player");
            DropTable("dbo.Classification_Gamer");
            DropTable("dbo.ChatConnections");
        }
    }
}
