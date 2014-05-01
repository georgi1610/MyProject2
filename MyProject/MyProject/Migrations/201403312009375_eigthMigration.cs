namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eigthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.Requests", new[] { "Address_AddressId" });
            DropColumn("dbo.Requests", "Address_AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "Address_AddressId", c => c.Int());
            CreateIndex("dbo.Requests", "Address_AddressId");
            AddForeignKey("dbo.Requests", "Address_AddressId", "dbo.Addresses", "AddressId");
        }
    }
}
