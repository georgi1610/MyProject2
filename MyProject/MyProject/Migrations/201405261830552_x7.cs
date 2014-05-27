namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "EndDate", c => c.DateTime(nullable: false));
        }
    }
}
