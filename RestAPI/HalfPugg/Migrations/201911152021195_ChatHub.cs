namespace HalfPugg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChatHub : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FilterHalls", newName: "HallFilters");
            RenameTable(name: "dbo.GameFilters", newName: "FilterGames");
            DropForeignKey("dbo.ClassificationPlayers", "IdJudgePlayer", "dbo.Players");
            DropForeignKey("dbo.FilterHalls", "Hall_ID_Hall", "dbo.Halls");
            DropForeignKey("dbo.MessageHalls", "ID_Recipient", "dbo.Halls");
            DropForeignKey("dbo.PlayerHalls", "IdHall", "dbo.Halls");
            DropForeignKey("dbo.RequestedHalls", "IdSala", "dbo.Halls");
            DropForeignKey("dbo.MessageGroups", "ID_Recipient", "dbo.Groups");
            DropForeignKey("dbo.PlayerGroups", "IdGroup", "dbo.Groups");
            DropForeignKey("dbo.RequestedGroups", "IdGroup", "dbo.Groups");
            DropIndex("dbo.ClassificationPlayers", new[] { "IdJudgePlayer" });
            RenameColumn(table: "dbo.MessageHalls", name: "ID_User", newName: "ID_Sender");
            RenameColumn(table: "dbo.HallFilters", name: "Hall_ID_Hall", newName: "Hall_ID");
            RenameColumn(table: "dbo.MessageHalls", name: "ID_Recipient", newName: "ID_Destination");
            RenameColumn(table: "dbo.MessagePlayers", name: "ID_Recipient", newName: "ID_Destination");
            RenameColumn(table: "dbo.MessagePlayers", name: "ID_User", newName: "ID_Sender");
            RenameColumn(table: "dbo.MessageGroups", name: "ID_Recipient", newName: "ID_Destination");
            RenameColumn(table: "dbo.MessageGroups", name: "ID_User", newName: "ID_Sender");
            RenameIndex(table: "dbo.MessageHalls", name: "IX_ID_User", newName: "IX_ID_Sender");
            RenameIndex(table: "dbo.MessageHalls", name: "IX_ID_Recipient", newName: "IX_ID_Destination");
            RenameIndex(table: "dbo.MessageGroups", name: "IX_ID_User", newName: "IX_ID_Sender");
            RenameIndex(table: "dbo.MessageGroups", name: "IX_ID_Recipient", newName: "IX_ID_Destination");
            RenameIndex(table: "dbo.MessagePlayers", name: "IX_ID_User", newName: "IX_ID_Sender");
            RenameIndex(table: "dbo.MessagePlayers", name: "IX_ID_Recipient", newName: "IX_ID_Destination");
            RenameIndex(table: "dbo.HallFilters", name: "IX_Hall_ID_Hall", newName: "IX_Hall_ID");
            DropPrimaryKey("dbo.Halls");
            DropPrimaryKey("dbo.Groups");
            DropPrimaryKey("dbo.MessagePlayers");
            DropPrimaryKey("dbo.HallFilters");
            DropPrimaryKey("dbo.FilterGames");
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
            
            AddColumn("dbo.Players", "Hall_ID", c => c.Int());
            AddColumn("dbo.Players", "Group_ID", c => c.Int());
            AddColumn("dbo.MessageHalls", "View_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageHalls", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageHalls", "Status", c => c.Short(nullable: false));
            AddColumn("dbo.Halls", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Halls", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Halls", "Player_ID", c => c.Int());
            AddColumn("dbo.Groups", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Groups", "Player_ID", c => c.Int());
            AddColumn("dbo.MessagePlayers", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.MessagePlayers", "Send_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessagePlayers", "View_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessagePlayers", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageGroups", "View_Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageGroups", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageGroups", "Status", c => c.Short(nullable: false));
            AlterColumn("dbo.MessageHalls", "Content", c => c.String(maxLength: 500));
            AlterColumn("dbo.Groups", "Name", c => c.String(maxLength: 70));
            AlterColumn("dbo.MessagePlayers", "Content", c => c.String(maxLength: 500));
            AlterColumn("dbo.MessagePlayers", "Status", c => c.Short(nullable: false));
            AddPrimaryKey("dbo.Halls", "ID");
            AddPrimaryKey("dbo.Groups", "ID");
            AddPrimaryKey("dbo.MessagePlayers", "ID");
            AddPrimaryKey("dbo.HallFilters", new[] { "Hall_ID", "Filter_ID_Filter" });
            AddPrimaryKey("dbo.FilterGames", new[] { "Filter_ID_Filter", "Game_ID_Game" });
            CreateIndex("dbo.Players", "Hall_ID");
            CreateIndex("dbo.Players", "Group_ID");
            CreateIndex("dbo.Groups", "Player_ID");
            CreateIndex("dbo.Halls", "Player_ID");
            AddForeignKey("dbo.Players", "Hall_ID", "dbo.Halls", "ID");
            AddForeignKey("dbo.Players", "Group_ID", "dbo.Groups", "ID");
            AddForeignKey("dbo.Groups", "Player_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.Halls", "Player_ID", "dbo.Players", "ID");
            AddForeignKey("dbo.HallFilters", "Hall_ID", "dbo.Halls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MessageHalls", "ID_Destination", "dbo.Halls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PlayerHalls", "IdHall", "dbo.Halls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RequestedHalls", "IdSala", "dbo.Halls", "ID", cascadeDelete: true);
            AddForeignKey("dbo.MessageGroups", "ID_Destination", "dbo.Groups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PlayerGroups", "IdGroup", "dbo.Groups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RequestedGroups", "IdGroup", "dbo.Groups", "ID", cascadeDelete: true);
            DropColumn("dbo.ClassificationPlayers", "IdJudgePlayer");
            DropColumn("dbo.Halls", "ID_Hall");
            DropColumn("dbo.Halls", "AlteredAt");
            DropColumn("dbo.Groups", "ID_Group");
            DropColumn("dbo.Groups", "AlteredAt");
            DropColumn("dbo.MessagePlayers", "ID_Message");
            DropColumn("dbo.MessagePlayers", "CreateAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessagePlayers", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessagePlayers", "ID_Message", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Groups", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Groups", "ID_Group", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Halls", "AlteredAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Halls", "ID_Hall", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ClassificationPlayers", "IdJudgePlayer", c => c.Int(nullable: false));
            DropForeignKey("dbo.RequestedGroups", "IdGroup", "dbo.Groups");
            DropForeignKey("dbo.PlayerGroups", "IdGroup", "dbo.Groups");
            DropForeignKey("dbo.MessageGroups", "ID_Destination", "dbo.Groups");
            DropForeignKey("dbo.RequestedHalls", "IdSala", "dbo.Halls");
            DropForeignKey("dbo.PlayerHalls", "IdHall", "dbo.Halls");
            DropForeignKey("dbo.MessageHalls", "ID_Destination", "dbo.Halls");
            DropForeignKey("dbo.HallFilters", "Hall_ID", "dbo.Halls");
            DropForeignKey("dbo.Halls", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.Groups", "Player_ID", "dbo.Players");
            DropForeignKey("dbo.Players", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.Players", "Hall_ID", "dbo.Halls");
            DropForeignKey("dbo.ChatConnections", "Player_ID", "dbo.Players");
            DropIndex("dbo.Halls", new[] { "Player_ID" });
            DropIndex("dbo.Groups", new[] { "Player_ID" });
            DropIndex("dbo.Players", new[] { "Group_ID" });
            DropIndex("dbo.Players", new[] { "Hall_ID" });
            DropIndex("dbo.ChatConnections", new[] { "Player_ID" });
            DropPrimaryKey("dbo.FilterGames");
            DropPrimaryKey("dbo.HallFilters");
            DropPrimaryKey("dbo.MessagePlayers");
            DropPrimaryKey("dbo.Groups");
            DropPrimaryKey("dbo.Halls");
            AlterColumn("dbo.MessagePlayers", "Status", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.MessagePlayers", "Content", c => c.String(nullable: false, maxLength: 400));
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.MessageHalls", "Content", c => c.String(nullable: false, maxLength: 400));
            DropColumn("dbo.MessageGroups", "Status");
            DropColumn("dbo.MessageGroups", "AlteredAt");
            DropColumn("dbo.MessageGroups", "View_Time");
            DropColumn("dbo.MessagePlayers", "AlteredAt");
            DropColumn("dbo.MessagePlayers", "View_Time");
            DropColumn("dbo.MessagePlayers", "Send_Time");
            DropColumn("dbo.MessagePlayers", "ID");
            DropColumn("dbo.Groups", "Player_ID");
            DropColumn("dbo.Groups", "ID");
            DropColumn("dbo.Halls", "Player_ID");
            DropColumn("dbo.Halls", "Active");
            DropColumn("dbo.Halls", "ID");
            DropColumn("dbo.MessageHalls", "Status");
            DropColumn("dbo.MessageHalls", "AlteredAt");
            DropColumn("dbo.MessageHalls", "View_Time");
            DropColumn("dbo.Players", "Group_ID");
            DropColumn("dbo.Players", "Hall_ID");
            DropTable("dbo.ChatConnections");
            AddPrimaryKey("dbo.FilterGames", new[] { "Game_ID_Game", "Filter_ID_Filter" });
            AddPrimaryKey("dbo.HallFilters", new[] { "Filter_ID_Filter", "Hall_ID_Hall" });
            AddPrimaryKey("dbo.MessagePlayers", "ID_Message");
            AddPrimaryKey("dbo.Groups", "ID_Group");
            AddPrimaryKey("dbo.Halls", "ID_Hall");
            RenameIndex(table: "dbo.HallFilters", name: "IX_Hall_ID", newName: "IX_Hall_ID_Hall");
            RenameIndex(table: "dbo.MessagePlayers", name: "IX_ID_Destination", newName: "IX_ID_Recipient");
            RenameIndex(table: "dbo.MessagePlayers", name: "IX_ID_Sender", newName: "IX_ID_User");
            RenameIndex(table: "dbo.MessageGroups", name: "IX_ID_Destination", newName: "IX_ID_Recipient");
            RenameIndex(table: "dbo.MessageGroups", name: "IX_ID_Sender", newName: "IX_ID_User");
            RenameIndex(table: "dbo.MessageHalls", name: "IX_ID_Destination", newName: "IX_ID_Recipient");
            RenameIndex(table: "dbo.MessageHalls", name: "IX_ID_Sender", newName: "IX_ID_User");
            RenameColumn(table: "dbo.MessageGroups", name: "ID_Sender", newName: "ID_User");
            RenameColumn(table: "dbo.MessageGroups", name: "ID_Destination", newName: "ID_Recipient");
            RenameColumn(table: "dbo.MessagePlayers", name: "ID_Sender", newName: "ID_User");
            RenameColumn(table: "dbo.MessagePlayers", name: "ID_Destination", newName: "ID_Recipient");
            RenameColumn(table: "dbo.MessageHalls", name: "ID_Destination", newName: "ID_Recipient");
            RenameColumn(table: "dbo.HallFilters", name: "Hall_ID", newName: "Hall_ID_Hall");
            RenameColumn(table: "dbo.MessageHalls", name: "ID_Sender", newName: "ID_User");
            CreateIndex("dbo.ClassificationPlayers", "IdJudgePlayer");
            AddForeignKey("dbo.RequestedGroups", "IdGroup", "dbo.Groups", "ID_Group", cascadeDelete: true);
            AddForeignKey("dbo.PlayerGroups", "IdGroup", "dbo.Groups", "ID_Group", cascadeDelete: true);
            AddForeignKey("dbo.MessageGroups", "ID_Recipient", "dbo.Groups", "ID_Group", cascadeDelete: true);
            AddForeignKey("dbo.RequestedHalls", "IdSala", "dbo.Halls", "ID_Hall", cascadeDelete: true);
            AddForeignKey("dbo.PlayerHalls", "IdHall", "dbo.Halls", "ID_Hall", cascadeDelete: true);
            AddForeignKey("dbo.MessageHalls", "ID_Recipient", "dbo.Halls", "ID_Hall", cascadeDelete: true);
            AddForeignKey("dbo.FilterHalls", "Hall_ID_Hall", "dbo.Halls", "ID_Hall", cascadeDelete: true);
            AddForeignKey("dbo.ClassificationPlayers", "IdJudgePlayer", "dbo.Players", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.FilterGames", newName: "GameFilters");
            RenameTable(name: "dbo.HallFilters", newName: "FilterHalls");
        }
    }
}
