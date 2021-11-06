namespace PlantInventory.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedArchiveFunction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batch", "IsArchived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Herb", "IsArchived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Move", "IsArchived", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stage", "IsArchived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stage", "IsArchived");
            DropColumn("dbo.Move", "IsArchived");
            DropColumn("dbo.Herb", "IsArchived");
            DropColumn("dbo.Batch", "IsArchived");
        }
    }
}
