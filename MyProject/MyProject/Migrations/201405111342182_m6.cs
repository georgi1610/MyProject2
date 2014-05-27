namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transports", "ArrivalAddress_AddressId", c => c.Int());
            CreateIndex("dbo.Transports", "ArrivalAddress_AddressId");
            AddForeignKey("dbo.Transports", "ArrivalAddress_AddressId", "dbo.Addresses", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transports", "ArrivalAddress_AddressId", "dbo.Addresses");
            DropIndex("dbo.Transports", new[] { "ArrivalAddress_AddressId" });
            DropColumn("dbo.Transports", "ArrivalAddress_AddressId");
        }
    }
}
