namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "ArrivalTransport_TransportId", c => c.Int());
            AddColumn("dbo.Addresses", "DepartureTransport_TransportId", c => c.Int());
            CreateIndex("dbo.Addresses", "ArrivalTransport_TransportId");
            CreateIndex("dbo.Addresses", "DepartureTransport_TransportId");
            AddForeignKey("dbo.Addresses", "ArrivalTransport_TransportId", "dbo.Transports", "TransportId");
            AddForeignKey("dbo.Addresses", "DepartureTransport_TransportId", "dbo.Transports", "TransportId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "DepartureTransport_TransportId", "dbo.Transports");
            DropForeignKey("dbo.Addresses", "ArrivalTransport_TransportId", "dbo.Transports");
            DropIndex("dbo.Addresses", new[] { "DepartureTransport_TransportId" });
            DropIndex("dbo.Addresses", new[] { "ArrivalTransport_TransportId" });
            DropColumn("dbo.Addresses", "DepartureTransport_TransportId");
            DropColumn("dbo.Addresses", "ArrivalTransport_TransportId");
        }
    }
}
