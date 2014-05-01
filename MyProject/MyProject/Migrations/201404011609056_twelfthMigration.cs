namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twelfthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "StandInId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Requests", "StandInId");
        }
    }
}
