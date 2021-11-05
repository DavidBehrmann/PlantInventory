namespace PlantInventory.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBatchService1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Batch", new[] { "HerbID" });
            AddColumn("dbo.Batch", "ModifiedUTC", c => c.DateTimeOffset(nullable: false, precision: 7));
            CreateIndex("dbo.Batch", "HerbId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Batch", new[] { "HerbId" });
            DropColumn("dbo.Batch", "ModifiedUTC");
            CreateIndex("dbo.Batch", "HerbID");
        }
    }
}
