namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transports", "DepartureAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.Transports", "ArrivalAddressId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transports", "ArrivalAddressId");
            DropColumn("dbo.Transports", "DepartureAddressId");
        }
    }
}
