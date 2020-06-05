namespace newdip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxx : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Addrees = c.String(),
                        Description = c.String(),
                        Site = c.String(),
                        TimeTable = c.String(),
                    })
                .PrimaryKey(t => t.BuildingId);
            
            CreateTable(
                "dbo.Floors",
                c => new
                    {
                        FloorId = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        BuildingId = c.Int(),
                    })
                .PrimaryKey(t => t.FloorId)
                .ForeignKey("dbo.Buildings", t => t.BuildingId)
                .Index(t => t.BuildingId);
            
            CreateTable(
                "dbo.PointMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        IsWaypoint = c.Boolean(nullable: false),
                        FloorId = c.Int(),
                        RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Floors", t => t.FloorId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.FloorId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.EdgeMs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Weight = c.Double(nullable: false),
                        PointFromId = c.Int(nullable: false),
                        PointToId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PointMs", t => t.PointToId)
                .ForeignKey("dbo.PointMs", t => t.PointFromId)
                .Index(t => t.PointFromId)
                .Index(t => t.PointToId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        FloorId = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        Timetable = c.String(),
                        Phone = c.String(),
                        Site = c.String(),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Floors", t => t.FloorId)
                .Index(t => t.FloorId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.String(),
                        RoomName = c.String(),
                        Building = c.String(),
                        ClientName = c.String(),
                        RoomId = c.Int(),
                        ClientId = c.Int(),
                        isPublic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.RoomId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FavoriteRooms",
                c => new
                    {
                        FavoriteRoomId = c.Int(nullable: false, identity: true),
                        Building = c.String(),
                        Name = c.String(),
                        Details = c.String(),
                        ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.FavoriteRoomId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        LastName = c.String(),
                        Status = c.String(),
                        Details = c.String(),
                        Email = c.String(),
                        Site = c.String(),
                        Phone = c.String(),
                        RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Workers", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.PointMs", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Notes", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Notes", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.FavoriteRooms", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Rooms", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.PointMs", "FloorId", "dbo.Floors");
            DropForeignKey("dbo.EdgeMs", "PointFromId", "dbo.PointMs");
            DropForeignKey("dbo.EdgeMs", "PointToId", "dbo.PointMs");
            DropForeignKey("dbo.Floors", "BuildingId", "dbo.Buildings");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Workers", new[] { "RoomId" });
            DropIndex("dbo.FavoriteRooms", new[] { "ClientId" });
            DropIndex("dbo.Notes", new[] { "ClientId" });
            DropIndex("dbo.Notes", new[] { "RoomId" });
            DropIndex("dbo.Rooms", new[] { "FloorId" });
            DropIndex("dbo.EdgeMs", new[] { "PointToId" });
            DropIndex("dbo.EdgeMs", new[] { "PointFromId" });
            DropIndex("dbo.PointMs", new[] { "RoomId" });
            DropIndex("dbo.PointMs", new[] { "FloorId" });
            DropIndex("dbo.Floors", new[] { "BuildingId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Workers");
            DropTable("dbo.FavoriteRooms");
            DropTable("dbo.Clients");
            DropTable("dbo.Notes");
            DropTable("dbo.Rooms");
            DropTable("dbo.EdgeMs");
            DropTable("dbo.PointMs");
            DropTable("dbo.Floors");
            DropTable("dbo.Buildings");
        }
    }
}
