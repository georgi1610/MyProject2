namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transports", "DepartureTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transports", "ArrivalTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transports", "ArrivalTime");
            DropColumn("dbo.Transports", "DepartureTime");
        }
    }
}
