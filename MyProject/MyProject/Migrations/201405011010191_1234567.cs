namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1234567 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "DepartureAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Requests", "ReturnAddress_AddressId", "dbo.Addresses");
            DropIndex("dbo.Requests", new[] { "DepartureAddress_AddressId" });
            DropIndex("dbo.Requests", new[] { "ReturnAddress_AddressId" });
            AlterColumn("dbo.Requests", "DepartureAddress_AddressId", c => c.Int(nullable: false));
            AlterColumn("dbo.Requests", "ReturnAddress_AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "DepartureAddress_AddressId");
            CreateIndex("dbo.Requests", "ReturnAddress_AddressId");
            AddForeignKey("dbo.Requests", "DepartureAddress_AddressId", "dbo.Addresses", "AddressId", cascadeDelete: false);
            AddForeignKey("dbo.Requests", "ReturnAddress_AddressId", "dbo.Addresses", "AddressId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "ReturnAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Requests", "DepartureAddress_AddressId", "dbo.Addresses");
            DropIndex("dbo.Requests", new[] { "ReturnAddress_AddressId" });
            DropIndex("dbo.Requests", new[] { "DepartureAddress_AddressId" });
            AlterColumn("dbo.Requests", "ReturnAddress_AddressId", c => c.Int());
            AlterColumn("dbo.Requests", "DepartureAddress_AddressId", c => c.Int());
            CreateIndex("dbo.Requests", "ReturnAddress_AddressId");
            CreateIndex("dbo.Requests", "DepartureAddress_AddressId");
            AddForeignKey("dbo.Requests", "ReturnAddress_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Requests", "DepartureAddress_AddressId", "dbo.Addresses", "AddressId");
        }
    }
}
