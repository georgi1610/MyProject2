namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class geo2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "Transport_TransportId", "dbo.Transports");
            DropIndex("dbo.Requests", new[] { "Transport_TransportId" });
            RenameColumn(table: "dbo.Requests", name: "Transport_TransportId", newName: "TransportId");
            AlterColumn("dbo.Requests", "TransportId", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "TransportId");
            AddForeignKey("dbo.Requests", "TransportId", "dbo.Transports", "TransportId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "TransportId", "dbo.Transports");
            DropIndex("dbo.Requests", new[] { "TransportId" });
            AlterColumn("dbo.Requests", "TransportId", c => c.Int());
            RenameColumn(table: "dbo.Requests", name: "TransportId", newName: "Transport_TransportId");
            CreateIndex("dbo.Requests", "Transport_TransportId");
            AddForeignKey("dbo.Requests", "Transport_TransportId", "dbo.Transports", "TransportId");
        }
    }
}
