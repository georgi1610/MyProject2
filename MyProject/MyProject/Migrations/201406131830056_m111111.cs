namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m111111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Headquarter_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Headquarter_Id");
        }
    }
}
