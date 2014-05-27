namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transports", "DepartureAddress_AddressId", c => c.Int());
            CreateIndex("dbo.Transports", "DepartureAddress_AddressId");
            AddForeignKey("dbo.Transports", "DepartureAddress_AddressId", "dbo.Addresses", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transports", "DepartureAddress_AddressId", "dbo.Addresses");
            DropIndex("dbo.Transports", new[] { "DepartureAddress_AddressId" });
            DropColumn("dbo.Transports", "DepartureAddress_AddressId");
        }
    }
}
