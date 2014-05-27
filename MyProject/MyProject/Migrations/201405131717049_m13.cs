namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m13 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transports", "DepartureAddressId");
            DropColumn("dbo.Transports", "ArrivalAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transports", "ArrivalAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Transports", "DepartureAddressId", c => c.Int(nullable: false));
        }
    }
}
