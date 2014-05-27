namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class x1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transports", "PlaneTicketPath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transports", "PlaneTicketPath", c => c.String());
        }
    }
}
