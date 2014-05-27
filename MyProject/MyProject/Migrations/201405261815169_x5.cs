namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EndDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EndDate");
        }
    }
}
