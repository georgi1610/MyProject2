namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1234 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Requests", name: "Allowance_AllowanceId", newName: "AllowanceId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Requests", name: "AllowanceId", newName: "Allowance_AllowanceId");
        }
    }
}
