namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eleventhMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "DepartureAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "ReturnAddressId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "ReturnAddressId");
            DropColumn("dbo.Requests", "DepartureAddressId");
        }
    }
}
