namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transports", "ArrivalAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Transports", "DepartureAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "ArrivalTransport_TransportId", "dbo.Transports");
            DropForeignKey("dbo.Addresses", "DepartureTransport_TransportId", "dbo.Transports");
            DropIndex("dbo.Transports", new[] { "ArrivalAddress_AddressId" });
            DropIndex("dbo.Transports", new[] { "DepartureAddress_AddressId" });
            DropIndex("dbo.Addresses", new[] { "ArrivalTransport_TransportId" });
            DropIndex("dbo.Addresses", new[] { "DepartureTransport_TransportId" });
            AddColumn("dbo.Transports", "DepartureAddress", c => c.String(nullable: false));
            AddColumn("dbo.Transports", "ArrivalAddress", c => c.String(nullable: false));
            DropColumn("dbo.Addresses", "ArrivalTransport_TransportId");
            DropColumn("dbo.Addresses", "DepartureTransport_TransportId");
            DropColumn("dbo.Transports", "ArrivalAddress_AddressId");
            DropColumn("dbo.Transports", "DepartureAddress_AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transports", "DepartureAddress_AddressId", c => c.Int());
            AddColumn("dbo.Transports", "ArrivalAddress_AddressId", c => c.Int());
            AddColumn("dbo.Addresses", "DepartureTransport_TransportId", c => c.Int());
            AddColumn("dbo.Addresses", "ArrivalTransport_TransportId", c => c.Int());
            DropColumn("dbo.Transports", "ArrivalAddress");
            DropColumn("dbo.Transports", "DepartureAddress");
            CreateIndex("dbo.Addresses", "DepartureTransport_TransportId");
            CreateIndex("dbo.Addresses", "ArrivalTransport_TransportId");
            CreateIndex("dbo.Transports", "DepartureAddress_AddressId");
            CreateIndex("dbo.Transports", "ArrivalAddress_AddressId");
            AddForeignKey("dbo.Addresses", "DepartureTransport_TransportId", "dbo.Transports", "TransportId");
            AddForeignKey("dbo.Addresses", "ArrivalTransport_TransportId", "dbo.Transports", "TransportId");
            AddForeignKey("dbo.Transports", "DepartureAddress_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Transports", "ArrivalAddress_AddressId", "dbo.Addresses", "AddressId");
        }
    }
}
