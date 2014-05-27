namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transports", "ArrivalAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Transports", "DepartureAddress_AddressId", "dbo.Addresses");
            DropIndex("dbo.Transports", new[] { "ArrivalAddress_AddressId" });
            DropIndex("dbo.Transports", new[] { "DepartureAddress_AddressId" });
            DropColumn("dbo.Transports", "ArrivalAddress_AddressId");
            DropColumn("dbo.Transports", "DepartureAddress_AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transports", "DepartureAddress_AddressId", c => c.Int());
            AddColumn("dbo.Transports", "ArrivalAddress_AddressId", c => c.Int());
            CreateIndex("dbo.Transports", "DepartureAddress_AddressId");
            CreateIndex("dbo.Transports", "ArrivalAddress_AddressId");
            AddForeignKey("dbo.Transports", "DepartureAddress_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Transports", "ArrivalAddress_AddressId", "dbo.Addresses", "AddressId");
        }
    }
}
