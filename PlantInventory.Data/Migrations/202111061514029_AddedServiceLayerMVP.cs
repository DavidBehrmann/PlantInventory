namespace PlantInventory.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedServiceLayerMVP : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Move", new[] { "BatchID" });
            DropIndex("dbo.Stage", new[] { "BatchID" });
            AddColumn("dbo.Move", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Stage", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Move", "BatchId");
            CreateIndex("dbo.Stage", "BatchId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stage", new[] { "BatchId" });
            DropIndex("dbo.Move", new[] { "BatchId" });
            DropColumn("dbo.Stage", "UserId");
            DropColumn("dbo.Move", "UserId");
            CreateIndex("dbo.Stage", "BatchID");
            CreateIndex("dbo.Move", "BatchID");
        }
    }
}
