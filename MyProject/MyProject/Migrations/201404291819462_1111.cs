namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EMailPassword", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EMailPassword");
        }
    }
}
