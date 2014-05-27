namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EndDate");
        }
    }
}
