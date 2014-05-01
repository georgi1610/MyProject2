namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "RequestId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "RequestId");
        }
    }
}
