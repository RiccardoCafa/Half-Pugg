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
                        Points = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Classification_ID = c.Int(nullable: false),
                        Player_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classification_Gamer", t => t.Classification_ID, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.Player_ID, cascadeDelete: true)
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
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        ID_Recipient_ID_Group = c.Int(nullable: false),
                        ID_User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.ID_Recipient_ID_Group, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.ID_User_ID, cascadeDelete: true)
                .Index(t => t.ID_Recipient_ID_Group)
                .Index(t => t.ID_User_ID);
            
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
                        Capacity = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Admin_ID = c.Int(nullable: false),
                        game_ID_Game = c.Int(nullable: false),
                        Gamer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID_Hall)
                .ForeignKey("dbo.Gamers", t => t.Admin_ID, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.game_ID_Game, cascadeDelete: true)
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
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        ID_Recipient_ID_Hall = c.Int(nullable: false),
                        ID_User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Halls", t => t.ID_Recipient_ID_Hall, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.ID_User_ID, cascadeDelete: true)
                .Index(t => t.ID_Recipient_ID_Hall)
                .Index(t => t.ID_User_ID);
            
            CreateTable(
                "dbo.GameInGames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Points = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Classification_ID = c.Int(nullable: false),
                        IdGame_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classification_Gamer", t => t.Classification_ID, cascadeDelete: true)
                .ForeignKey("dbo.PlayerGames", t => t.IdGame_ID, cascadeDelete: true)
                .Index(t => t.Classification_ID)
                .Index(t => t.IdGame_ID);
            
            CreateTable(
                "dbo.PlayerGames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 300),
                        IdAPI = c.String(nullable: false),
                        Weight = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        IdGame_ID_Game = c.Int(nullable: false),
                        IdGamer_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Games", t => t.IdGame_ID_Game, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.IdGamer_ID, cascadeDelete: true)
                .Index(t => t.IdAPI, unique: true)
                .Index(t => t.IdGame_ID_Game)
                .Index(t => t.IdGamer_ID);
            
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
                        NameLabel = c.String(nullable: false, maxLength: 50),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        ID_Filter_ID_Filter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.ID_Filter_ID_Filter, cascadeDelete: true)
                .Index(t => t.ID_Filter_ID_Filter);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Weight = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Player1_ID = c.Int(nullable: false),
                        Player2_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Gamers", t => t.Player1_ID, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.Player2_ID, cascadeDelete: true)
                .Index(t => t.Player1_ID)
                .Index(t => t.Player2_ID);
            
            CreateTable(
                "dbo.MessageGamers",
                c => new
                    {
                        ID_Message = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 400),
                        Send_Time = c.DateTime(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        ID_Recipient_ID = c.Int(nullable: false),
                        ID_User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Message)
                .ForeignKey("dbo.Gamers", t => t.ID_Recipient_ID, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.ID_User_ID, cascadeDelete: true)
                .Index(t => t.ID_Recipient_ID)
                .Index(t => t.ID_User_ID);
            
            CreateTable(
                "dbo.Numbereds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        ID_Filter_ID_Filter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.ID_Filter_ID_Filter, cascadeDelete: true)
                .Index(t => t.ID_Filter_ID_Filter);
            
            CreateTable(
                "dbo.PlayerHashtags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Weight = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Hash_ID_Matter = c.Int(nullable: false),
                        Player2_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HashTags", t => t.Hash_ID_Matter, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.Player2_ID, cascadeDelete: true)
                .Index(t => t.Hash_ID_Matter)
                .Index(t => t.Player2_ID);
            
            CreateTable(
                "dbo.Ranges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Max = c.Single(nullable: false),
                        Min = c.Single(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        ID_Filter_ID_Filter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.ID_Filter_ID_Filter, cascadeDelete: true)
                .Index(t => t.ID_Filter_ID_Filter);
            
            CreateTable(
                "dbo.RequestedGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Filters_ID_Filter = c.Int(),
                        Player_ID = c.Int(nullable: false),
                        Sala_ID_Group = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.Filters_ID_Filter)
                .ForeignKey("dbo.Gamers", t => t.Player_ID, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Sala_ID_Group, cascadeDelete: true)
                .Index(t => t.Filters_ID_Filter)
                .Index(t => t.Player_ID)
                .Index(t => t.Sala_ID_Group);
            
            CreateTable(
                "dbo.RequestedHalls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Filters_ID_Filter = c.Int(),
                        Player_ID = c.Int(nullable: false),
                        Sala_ID_Hall = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.Filters_ID_Filter)
                .ForeignKey("dbo.Gamers", t => t.Player_ID, cascadeDelete: true)
                .ForeignKey("dbo.Halls", t => t.Sala_ID_Hall, cascadeDelete: true)
                .Index(t => t.Filters_ID_Filter)
                .Index(t => t.Player_ID)
                .Index(t => t.Sala_ID_Hall);
            
            CreateTable(
                "dbo.RequestedMatches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequestedTime = c.DateTime(nullable: false),
                        ComfirmedTime = c.DateTime(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        Filters_ID_Filter = c.Int(),
                        Player_ID = c.Int(nullable: false),
                        Sala_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filters", t => t.Filters_ID_Filter)
                .ForeignKey("dbo.Gamers", t => t.Player_ID, cascadeDelete: true)
                .ForeignKey("dbo.Gamers", t => t.Sala_ID, cascadeDelete: true)
                .Index(t => t.Filters_ID_Filter)
                .Index(t => t.Player_ID)
                .Index(t => t.Sala_ID);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        ID_Template = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false, maxLength: 100),
                        CreateAt = c.DateTime(nullable: false),
                        AlteredAt = c.DateTime(nullable: false),
                        game_ID_Game = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Template)
                .ForeignKey("dbo.Games", t => t.game_ID_Game, cascadeDelete: true)
                .Index(t => t.game_ID_Game);
            
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
            DropForeignKey("dbo.Templates", "game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.RequestedMatches", "Sala_ID", "dbo.Gamers");
            DropForeignKey("dbo.RequestedMatches", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.RequestedMatches", "Filters_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.RequestedHalls", "Sala_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.RequestedHalls", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.RequestedHalls", "Filters_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.RequestedGroups", "Sala_ID_Group", "dbo.Groups");
            DropForeignKey("dbo.RequestedGroups", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.RequestedGroups", "Filters_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Ranges", "ID_Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.PlayerHashtags", "Player2_ID", "dbo.Gamers");
            DropForeignKey("dbo.PlayerHashtags", "Hash_ID_Matter", "dbo.HashTags");
            DropForeignKey("dbo.Numbereds", "ID_Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.MessageGamers", "ID_User_ID", "dbo.Gamers");
            DropForeignKey("dbo.MessageGamers", "ID_Recipient_ID", "dbo.Gamers");
            DropForeignKey("dbo.Matches", "Player2_ID", "dbo.Gamers");
            DropForeignKey("dbo.Matches", "Player1_ID", "dbo.Gamers");
            DropForeignKey("dbo.Labels", "ID_Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Games", "HashTag_ID_Matter", "dbo.HashTags");
            DropForeignKey("dbo.Gamers", "HashTag_ID_Matter", "dbo.HashTags");
            DropForeignKey("dbo.GameInGames", "IdGame_ID", "dbo.PlayerGames");
            DropForeignKey("dbo.PlayerGames", "IdGamer_ID", "dbo.Gamers");
            DropForeignKey("dbo.PlayerGames", "IdGame_ID_Game", "dbo.Games");
            DropForeignKey("dbo.GameInGames", "Classification_ID", "dbo.Classification_Gamer");
            DropForeignKey("dbo.ClassificationPlayers", "Player_ID", "dbo.Gamers");
            DropForeignKey("dbo.Halls", "Gamer_ID", "dbo.Gamers");
            DropForeignKey("dbo.Groups", "Gamer_ID", "dbo.Gamers");
            DropForeignKey("dbo.Groups", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.Halls", "game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.HallFilters", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.HallFilters", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.Gamers", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.MessageHalls", "ID_User_ID", "dbo.Gamers");
            DropForeignKey("dbo.MessageHalls", "ID_Recipient_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.Halls", "Admin_ID", "dbo.Gamers");
            DropForeignKey("dbo.Groups", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.FilterGames", "Game_ID_Game", "dbo.Games");
            DropForeignKey("dbo.FilterGames", "Filter_ID_Filter", "dbo.Filters");
            DropForeignKey("dbo.Gamers", "Group_ID_Group", "dbo.Groups");
            DropForeignKey("dbo.MessageGroups", "ID_User_ID", "dbo.Gamers");
            DropForeignKey("dbo.MessageGroups", "ID_Recipient_ID_Group", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Admin_ID", "dbo.Gamers");
            DropForeignKey("dbo.ClassificationPlayers", "Classification_ID", "dbo.Classification_Gamer");
            DropIndex("dbo.HallFilters", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.HallFilters", new[] { "Hall_ID_Hall" });
            DropIndex("dbo.FilterGames", new[] { "Game_ID_Game" });
            DropIndex("dbo.FilterGames", new[] { "Filter_ID_Filter" });
            DropIndex("dbo.Templates", new[] { "game_ID_Game" });
            DropIndex("dbo.RequestedMatches", new[] { "Sala_ID" });
            DropIndex("dbo.RequestedMatches", new[] { "Player_ID" });
            DropIndex("dbo.RequestedMatches", new[] { "Filters_ID_Filter" });
            DropIndex("dbo.RequestedHalls", new[] { "Sala_ID_Hall" });
            DropIndex("dbo.RequestedHalls", new[] { "Player_ID" });
            DropIndex("dbo.RequestedHalls", new[] { "Filters_ID_Filter" });
            DropIndex("dbo.RequestedGroups", new[] { "Sala_ID_Group" });
            DropIndex("dbo.RequestedGroups", new[] { "Player_ID" });
            DropIndex("dbo.RequestedGroups", new[] { "Filters_ID_Filter" });
            DropIndex("dbo.Ranges", new[] { "ID_Filter_ID_Filter" });
            DropIndex("dbo.PlayerHashtags", new[] { "Player2_ID" });
            DropIndex("dbo.PlayerHashtags", new[] { "Hash_ID_Matter" });
            DropIndex("dbo.Numbereds", new[] { "ID_Filter_ID_Filter" });
            DropIndex("dbo.MessageGamers", new[] { "ID_User_ID" });
            DropIndex("dbo.MessageGamers", new[] { "ID_Recipient_ID" });
            DropIndex("dbo.Matches", new[] { "Player2_ID" });
            DropIndex("dbo.Matches", new[] { "Player1_ID" });
            DropIndex("dbo.Labels", new[] { "ID_Filter_ID_Filter" });
            DropIndex("dbo.PlayerGames", new[] { "IdGamer_ID" });
            DropIndex("dbo.PlayerGames", new[] { "IdGame_ID_Game" });
            DropIndex("dbo.PlayerGames", new[] { "IdAPI" });
            DropIndex("dbo.GameInGames", new[] { "IdGame_ID" });
            DropIndex("dbo.GameInGames", new[] { "Classification_ID" });
            DropIndex("dbo.MessageHalls", new[] { "ID_User_ID" });
            DropIndex("dbo.MessageHalls", new[] { "ID_Recipient_ID_Hall" });
            DropIndex("dbo.Halls", new[] { "Gamer_ID" });
            DropIndex("dbo.Halls", new[] { "game_ID_Game" });
            DropIndex("dbo.Halls", new[] { "Admin_ID" });
            DropIndex("dbo.Games", new[] { "HashTag_ID_Matter" });
            DropIndex("dbo.MessageGroups", new[] { "ID_User_ID" });
            DropIndex("dbo.MessageGroups", new[] { "ID_Recipient_ID_Group" });
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
