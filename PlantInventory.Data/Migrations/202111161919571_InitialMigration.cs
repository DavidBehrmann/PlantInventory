namespace PlantInventory.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Batch",
                c => new
                    {
                        BatchId = c.Int(nullable: false, identity: true),
                        HerbId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        TotalPotCount = c.Int(nullable: false),
                        DateReceived = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        IsArchived = c.Boolean(nullable: false),
                        ArchiveComment = c.String(),
                    })
                .PrimaryKey(t => t.BatchId)
                .ForeignKey("dbo.Herb", t => t.HerbId, cascadeDelete: true)
                .Index(t => t.HerbId);
            
            CreateTable(
                "dbo.Herb",
                c => new
                    {
                        HerbId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        HerbName = c.String(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        ArchiveComment = c.String(),
                    })
                .PrimaryKey(t => t.HerbId);
            
            CreateTable(
                "dbo.Move",
                c => new
                    {
                        MoveId = c.Int(nullable: false, identity: true),
                        BatchId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        MoveFrom = c.Int(nullable: false),
                        MoveTo = c.Int(nullable: false),
                        NumberOfPotsMoved = c.Int(nullable: false),
                        Comment = c.String(),
                        DateMoved = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        IsArchived = c.Boolean(nullable: false),
                        ArchiveComment = c.String(),
                    })
                .PrimaryKey(t => t.MoveId)
                .ForeignKey("dbo.Batch", t => t.BatchId, cascadeDelete: true)
                .Index(t => t.BatchId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Stage",
                c => new
                    {
                        StageId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        BatchId = c.Int(nullable: false),
                        CountGrowRoom = c.Int(nullable: false),
                        CountPacking = c.Int(nullable: false),
                        CountFreshCut = c.Int(nullable: false),
                        CountDump = c.Int(nullable: false),
                        IsArchived = c.Boolean(nullable: false),
                        ArchiveComment = c.String(),
                    })
                .PrimaryKey(t => t.StageId)
                .ForeignKey("dbo.Batch", t => t.BatchId, cascadeDelete: true)
                .Index(t => t.BatchId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.Stage", "BatchId", "dbo.Batch");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Move", "BatchId", "dbo.Batch");
            DropForeignKey("dbo.Batch", "HerbId", "dbo.Herb");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Stage", new[] { "BatchId" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Move", new[] { "BatchId" });
            DropIndex("dbo.Batch", new[] { "HerbId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Stage");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Move");
            DropTable("dbo.Herb");
            DropTable("dbo.Batch");
        }
    }
}
