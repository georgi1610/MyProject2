namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixthMigration1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "Address_AddressId", c => c.Int());
            CreateIndex("dbo.Requests", "Address_AddressId");
            AddForeignKey("dbo.Requests", "Address_AddressId", "dbo.Addresses", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "Address_AddressId", "dbo.Addresses");
            DropIndex("dbo.Requests", new[] { "Address_AddressId" });
            DropColumn("dbo.Requests", "Address_AddressId");
        }
    }
}
