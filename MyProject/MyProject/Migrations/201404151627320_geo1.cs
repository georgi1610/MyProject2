namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class geo1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EMail", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EMail");
        }
    }
}
