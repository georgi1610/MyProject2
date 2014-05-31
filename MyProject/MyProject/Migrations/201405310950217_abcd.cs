namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Headquarter_AddressId", c => c.Int());
            CreateIndex("dbo.Employees", "Headquarter_AddressId");
            AddForeignKey("dbo.Employees", "Headquarter_AddressId", "dbo.Addresses", "AddressId");
            DropColumn("dbo.Employees", "Headquarter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Headquarter", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Employees", "Headquarter_AddressId", "dbo.Addresses");
            DropIndex("dbo.Employees", new[] { "Headquarter_AddressId" });
            DropColumn("dbo.Employees", "Headquarter_AddressId");
        }
    }
}
