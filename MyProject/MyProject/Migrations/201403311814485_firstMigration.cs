namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "ApplicantId");
            DropColumn("dbo.Requests", "ApproverId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "ApproverId", c => c.Int(nullable: false));
            AddColumn("dbo.Requests", "ApplicantId", c => c.Int(nullable: false));
        }
    }
}
