namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "DepartureAddressId");
            DropColumn("dbo.Requests", "ReturnAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "ReturnAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "DepartureAddressId", c => c.Int(nullable: false));
        }
    }
}
