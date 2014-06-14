namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11111111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "EmployeeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "EmployeeId");
        }
    }
}
