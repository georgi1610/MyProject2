namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13Migration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Requests", "StandInId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "StandInId", c => c.Int(nullable: false));
        }
    }
}
