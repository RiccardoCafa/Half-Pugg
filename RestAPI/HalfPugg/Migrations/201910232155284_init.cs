namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                "dbo.Classification_Game",
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
                        Classification_ID = c.Int(),
                        Player_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classification_Gamer", t => t.Classification_ID)
                .ForeignKey("dbo.Gamers", t => t.Player_ID)
                .Index(t => t.Classification_ID)
                .Index(t => t.Player_ID);
            
            CreateTable(
                "dbo.Gamers",
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
                        Genre = c.String(maxLength: 100),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Group_ID_Group = c.Int(),
                        Hall_ID_Hall = c.Int(),
                        HashTag_ID_Matter = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.Group_ID_Group)
                .ForeignKey("dbo.Halls", t => t.Hall_ID_Hall)
                .ForeignKey("dbo.HashTags", t => t.HashTag_ID_Matter)
                .Index(t => t.Nickname, unique: true)
                .Index(t => t.Group_ID_Group)
                .Index(t => t.Hall_ID_Hall)
                .Index(t => t.HashTag_ID_Matter);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID_Group = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Capacity = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Admin_ID = c.Int(nullable: false),
                        Filter_ID_Filter = c.Int(),
                        Game_ID_Game = c.Int(nullable: false),
                        Gamer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Group)
                .ForeignKey("dbo.Gamers", t => t.Admin_ID, cascadeDelete: true)
                .ForeignKey("dbo.Filters", t => t.Filter_ID_Filter)
                .ForeignKey("dbo.Games", t => t.Game_ID_Game, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.Gamer_ID)
                .Index(t => t.Admin_ID)
                .Index(t => t.Filter_ID_Filter)
                .Index(t => t.Game_ID_Game)
                .Index(t => t.Gamer_ID);
            
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
                        Recipient_ID_Group = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.Recipient_ID_Group)
                .ForeignKey("dbo.Gamers", t => t.User_ID)
                .Index(t => t.Recipient_ID_Group)
                .Index(t => t.User_ID);
            
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
                        HashTag_ID_Matter = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Game)
                .ForeignKey("dbo.HashTags", t => t.HashTag_ID_Matter)
                .Index(t => t.HashTag_ID_Matter);
            
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
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Admin_ID = c.Int(nullable: false),
                        game_ID_Game = c.Int(),
                        Gamer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Hall)
                .ForeignKey("dbo.Gamers", t => t.Admin_ID, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.game_ID_Game)
                .ForeignKey("dbo.Gamers", t => t.Gamer_ID)
                .Index(t => t.Admin_ID)
                .Index(t => t.game_ID_Game)
                .Index(t => t.Gamer_ID);
            
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
                        Recipient_ID_Hall = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Halls", t => t.Recipient_ID_Hall)
                .ForeignKey("dbo.Gamers", t => t.User_ID)
                .Index(t => t.Recipient_ID_Hall)
                .Index(t => t.User_ID);
            
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
                        Classification_ID = c.Int(),
                        PlayerGame_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classification_Gamer", t => t.Classification_ID)
                .ForeignKey("dbo.PlayerGames", t => t.PlayerGame_ID)
                .Index(t => t.Classification_ID)
                .Index(t => t.PlayerGame_ID);
            
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
                        Game_ID_Game = c.Int(),
                        Gamer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.Game_ID_Game)
                .ForeignKey("dbo.Gamers", t => t.Gamer_ID)
                .Index(t => t.IdAPI, unique: true)
                .Index(t => t.Game_ID_Game)
                .Index(t => t.Gamer_ID);
            
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
                .ForeignKey("dbo.Gamers", t => t.Player1_ID)
                .ForeignKey("dbo.Gamers", t => t.Player2_ID)
                .Index(t => t.Player1_ID)
                .Index(t => t.Player2_ID);
            
            CreateTable(
                "dbo.MessageGamers",
                c => new
                    {
                        ID_Message = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 400),
                        ID_User = c.Int(nullable: false),
                        ID_Recipient = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Recipient_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Message)
                .ForeignKey("dbo.Gamers", t => t.Recipient_ID)
                .ForeignKey("dbo.Gamers", t => t.User_ID)
                .Index(t => t.Recipient_ID)
                .Index(t => t.User_ID);
            
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
                        Hash_ID_Matter = c.Int(),
                        Player_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HashTags", t => t.Hash_ID_Matter)
                .ForeignKey("dbo.Gamers", t => t.Player_ID)
                .Index(t => t.Hash_ID_Matter)
                .Index(t => t.Player_ID);
            
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
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Filters_ID_Filter = c.Int(),
                        Player_ID = c.Int(),
                        Sala_ID_Group = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.Filters_ID_Filter)
                .ForeignKey("dbo.Gamers", t => t.Player_ID)
                .ForeignKey("dbo.Groups", t => t.Sala_ID_Group)
                .Index(t => t.Filters_ID_Filter)
                .Index(t => t.Player_ID)
                .Index(t => t.Sala_ID_Group);
            
            CreateTable(
                "dbo.RequestedHalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer = c.Int(nullable: false),
                        IdSala = c.Int(nullable: false),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Filters_ID_Filter = c.Int(),
                        Player_ID = c.Int(),
                        Sala_ID_Hall = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.Filters_ID_Filter)
                .ForeignKey("dbo.Gamers", t => t.Player_ID)
                .ForeignKey("dbo.Halls", t => t.Sala_ID_Hall)
                .Index(t => t.Filters_ID_Filter)
                .Index(t => t.Player_ID)
                .Index(t => t.Sala_ID_Hall);
            
            CreateTable(
                "dbo.RequestedMatches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdPlayer = c.Int(nullable: false),
                        IdSala = c.Int(nullable: false),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Filters_ID_Filter = c.Int(),
                        Player_ID = c.Int(),
                        Sala_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.Filters_ID_Filter)
                .ForeignKey("dbo.Gamers", t => t.Player_ID)
                .ForeignKey("dbo.Gamers", t => t.Sala_ID)
                .Index(t => t.Filters_ID_Filter)
                .Index(t => t.Player_ID)
                .Index(t => t.Sala_ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Templates", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.RequestedMatches", "Sala_ID", "dbo.Gamers");
            DropForeignKey("dbo.RequestedMatches", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.RequestedMatches", "Filters_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.RequestedHalls", "Sala_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.RequestedHalls", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.RequestedHalls", "Filters_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.RequestedGroups", "Sala_ID_Group", "dbo.Groups");
            DropForeignKey("dbo.RequestedGroups", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.RequestedGroups", "Filters_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Ranges", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.PlayerHashtags", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.PlayerHashtags", "Hash_ID_Matter", "dbo.HashTags");
            DropForeignKey("dbo.Numbereds", "IDFilter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Numbereds", "ID_Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.MessageGamers", "User_ID", "dbo.Gamers");
            DropForeignKey("dbo.MessageGamers", "Recipient_ID", "dbo.Gamers");
            DropForeignKey("dbo.Matches", "Player2_ID", "dbo.Gamers");
            DropForeignKey("dbo.Matches", "Player1_ID", "dbo.Gamers");
            DropForeignKey("dbo.Labels", "ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Games", "HashTag_ID_Matter", "dbo.HashTags");
            DropForeignKey("dbo.Gamers", "HashTag_ID_Matter", "dbo.HashTags");
            DropForeignKey("dbo.GameInGames", "PlayerGame_ID", "dbo.PlayerGames");
            DropForeignKey("dbo.PlayerGames", "Gamer_ID", "dbo.Gamers");
            DropForeignKey("dbo.PlayerGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.GameInGames", "Classification_ID", "dbo.Classification_Gamer");
            DropForeignKey("dbo.ClassificationPlayers", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.Halls", "Gamer_ID", "dbo.Gamers");
            DropForeignKey("dbo.Groups", "Gamer_ID", "dbo.Gamers");
            DropForeignKey("dbo.Groups", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.Halls", "game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.HallFilters", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.HallFilters", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.Gamers", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.MessageHalls", "User_ID", "dbo.Gamers");
            DropForeignKey("dbo.MessageHalls", "Recipient_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.Halls", "Admin_ID", "dbo.Gamers");
            DropForeignKey("dbo.Groups", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.FilterGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.FilterGames", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Gamers", "Group_ID_Group", "dbo.Groups");
            DropForeignKey("dbo.MessageGroups", "User_ID", "dbo.Gamers");
            DropForeignKey("dbo.MessageGroups", "Recipient_ID_Group", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Admin_ID", "dbo.Gamers");
            DropForeignKey("dbo.ClassificationPlayers", "Classification_ID", "dbo.Classification_Gamer");
            DropIndex("dbo.HallFilters", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.HallFilters", new[] { "Hall_ID_Hall" });
            DropIndex("dbo.FilterGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.FilterGames", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.Templates", new[] { "Game_ID_Game" });
            DropIndex("dbo.RequestedMatches", new[] { "Sala_ID" });
            DropIndex("dbo.RequestedMatches", new[] { "Player_ID" });
            DropIndex("dbo.RequestedMatches", new[] { "Filters_ID_Filter" });
            DropIndex("dbo.RequestedHalls", new[] { "Sala_ID_Hall" });
            DropIndex("dbo.RequestedHalls", new[] { "Player_ID" });
            DropIndex("dbo.RequestedHalls", new[] { "Filters_ID_Filter" });
            DropIndex("dbo.RequestedGroups", new[] { "Sala_ID_Group" });
            DropIndex("dbo.RequestedGroups", new[] { "Player_ID" });
            DropIndex("dbo.RequestedGroups", new[] { "Filters_ID_Filter" });
            DropIndex("dbo.Ranges", new[] { "ID_Filter" });
            DropIndex("dbo.PlayerHashtags", new[] { "Player_ID" });
            DropIndex("dbo.PlayerHashtags", new[] { "Hash_ID_Matter" });
            DropIndex("dbo.Numbereds", new[] { "IDFilter_ID_Filter" });
            DropIndex("dbo.Numbereds", new[] { "ID_Filter_ID_Filter" });
            DropIndex("dbo.MessageGamers", new[] { "User_ID" });
            DropIndex("dbo.MessageGamers", new[] { "Recipient_ID" });
            DropIndex("dbo.Matches", new[] { "Player2_ID" });
            DropIndex("dbo.Matches", new[] { "Player1_ID" });
            DropIndex("dbo.Labels", new[] { "ID_Filter" });
            DropIndex("dbo.PlayerGames", new[] { "Gamer_ID" });
            DropIndex("dbo.PlayerGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.PlayerGames", new[] { "IdAPI" });
            DropIndex("dbo.GameInGames", new[] { "PlayerGame_ID" });
            DropIndex("dbo.GameInGames", new[] { "Classification_ID" });
            DropIndex("dbo.MessageHalls", new[] { "User_ID" });
            DropIndex("dbo.MessageHalls", new[] { "Recipient_ID_Hall" });
            DropIndex("dbo.Halls", new[] { "Gamer_ID" });
            DropIndex("dbo.Halls", new[] { "game_ID_Game" });
            DropIndex("dbo.Halls", new[] { "Admin_ID" });
            DropIndex("dbo.Games", new[] { "HashTag_ID_Matter" });
            DropIndex("dbo.MessageGroups", new[] { "User_ID" });
            DropIndex("dbo.MessageGroups", new[] { "Recipient_ID_Group" });
            DropIndex("dbo.Groups", new[] { "Gamer_ID" });
            DropIndex("dbo.Groups", new[] { "Game_ID_Game" });
            DropIndex("dbo.Groups", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.Groups", new[] { "Admin_ID" });
            DropIndex("dbo.Gamers", new[] { "HashTag_ID_Matter" });
            DropIndex("dbo.Gamers", new[] { "Hall_ID_Hall" });
            DropIndex("dbo.Gamers", new[] { "Group_ID_Group" });
            DropIndex("dbo.Gamers", new[] { "Nickname" });
            DropIndex("dbo.ClassificationPlayers", new[] { "Player_ID" });
            DropIndex("dbo.ClassificationPlayers", new[] { "Classification_ID" });
            DropTable("dbo.HallFilters");
            DropTable("dbo.FilterGames");
            DropTable("dbo.Templates");
            DropTable("dbo.RequestedMatches");
            DropTable("dbo.RequestedHalls");
            DropTable("dbo.RequestedGroups");
            DropTable("dbo.Ranges");
            DropTable("dbo.PlayerHashtags");
            DropTable("dbo.Numbereds");
            DropTable("dbo.MessageGamers");
            DropTable("dbo.Matches");
            DropTable("dbo.Labels");
            DropTable("dbo.HashTags");
            DropTable("dbo.PlayerGames");
            DropTable("dbo.GameInGames");
            DropTable("dbo.MessageHalls");
            DropTable("dbo.Halls");
            DropTable("dbo.Filters");
            DropTable("dbo.Games");
            DropTable("dbo.MessageGroups");
            DropTable("dbo.Groups");
            DropTable("dbo.Gamers");
            DropTable("dbo.ClassificationPlayers");
            DropTable("dbo.Classification_Match");
            DropTable("dbo.Classification_Game");
            DropTable("dbo.Classification_Gamer");
        }
    }
}
