namespace PlantInventory.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBatchService : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Batch", "UserId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Batch", "UserId");
        }
    }
}
