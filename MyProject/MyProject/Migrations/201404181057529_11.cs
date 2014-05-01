namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "TransportId", "dbo.Transports");
            DropIndex("dbo.Requests", new[] { "TransportId" });
            AlterColumn("dbo.Requests", "TransportId", c => c.Int());
            CreateIndex("dbo.Requests", "TransportId");
            AddForeignKey("dbo.Requests", "TransportId", "dbo.Transports", "TransportId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "TransportId", "dbo.Transports");
            DropIndex("dbo.Requests", new[] { "TransportId" });
            AlterColumn("dbo.Requests", "TransportId", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "TransportId");
            AddForeignKey("dbo.Requests", "TransportId", "dbo.Transports", "TransportId", cascadeDelete: true);
        }
    }
}
