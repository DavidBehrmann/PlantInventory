namespace PlantInventory.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReachedMVP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batch", "ArchiveComment", c => c.String());
            AddColumn("dbo.Herb", "ArchiveComment", c => c.String());
            AddColumn("dbo.Move", "ModifiedUTC", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Move", "ArchiveComment", c => c.String());
            AddColumn("dbo.Stage", "ArchiveComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stage", "ArchiveComment");
            DropColumn("dbo.Move", "ArchiveComment");
            DropColumn("dbo.Move", "ModifiedUTC");
            DropColumn("dbo.Herb", "ArchiveComment");
            DropColumn("dbo.Batch", "ArchiveComment");
        }
    }
}
