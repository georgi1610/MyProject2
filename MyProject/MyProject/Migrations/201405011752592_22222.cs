namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22222 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Delegations", "DelegationType", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Addresses", "CompanyName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Addresses", "StreetName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Addresses", "PostalCode", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Status", "StatusName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Transports", "TransportCompany", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transports", "TransportCompany", c => c.String(nullable: false));
            AlterColumn("dbo.Status", "StatusName", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "StreetName", c => c.String(nullable: false));
            AlterColumn("dbo.Addresses", "CompanyName", c => c.String(nullable: false));
            AlterColumn("dbo.Delegations", "DelegationType", c => c.String(nullable: false));
        }
    }
}
