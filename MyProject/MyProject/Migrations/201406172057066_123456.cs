namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123456 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "Motivation", c => c.String(maxLength: 50));
            AlterColumn("dbo.Requests", "Description", c => c.String(maxLength: 50));
          //  AlterColumn("dbo.Transports", "DepartureAddress", c => c.String(nullable: false, maxLength: 50));
         //   AlterColumn("dbo.Transports", "ArrivalAddress", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Transports", "PlaneTicketPath", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transports", "PlaneTicketPath", c => c.String());
           // AlterColumn("dbo.Transports", "ArrivalAddress", c => c.String(nullable: false));
            //AlterColumn("dbo.Transports", "DepartureAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "Description", c => c.String());
            AlterColumn("dbo.Requests", "Motivation", c => c.String());
        }
    }
}
