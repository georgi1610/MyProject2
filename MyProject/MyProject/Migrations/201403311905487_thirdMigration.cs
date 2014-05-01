namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thirdMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "DepartureAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "ReturnAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "Address_AddressId", c => c.Int());
            CreateIndex("dbo.Requests", "Address_AddressId");
            AddForeignKey("dbo.Requests", "Address_AddressId", "dbo.Addresses", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.Requests", new[] { "Address_AddressId" });
            DropColumn("dbo.Requests", "Address_AddressId");
            DropColumn("dbo.Requests", "ReturnAddressId");
            DropColumn("dbo.Requests", "DepartureAddressId");
        }
    }
}
