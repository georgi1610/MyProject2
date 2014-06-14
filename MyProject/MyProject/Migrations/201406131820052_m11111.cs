namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11111 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Headquarter_AddressId", "dbo.Addresses");
            DropIndex("dbo.Employees", new[] { "Headquarter_AddressId" });
            AlterColumn("dbo.Employees", "Headquarter_AddressId", c => c.Int());
            CreateIndex("dbo.Employees", "Headquarter_AddressId");
            AddForeignKey("dbo.Employees", "Headquarter_AddressId", "dbo.Addresses", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Headquarter_AddressId", "dbo.Addresses");
            DropIndex("dbo.Employees", new[] { "Headquarter_AddressId" });
            AlterColumn("dbo.Employees", "Headquarter_AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employees", "Headquarter_AddressId");
            AddForeignKey("dbo.Employees", "Headquarter_AddressId", "dbo.Addresses", "AddressId", cascadeDelete: true);
        }
    }
}
