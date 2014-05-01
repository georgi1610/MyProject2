namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifthMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Requests", new[] { "Employee_EmployeeId" });
            DropColumn("dbo.Addresses", "RequestId");
            DropColumn("dbo.Requests", "Employee_EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "Employee_EmployeeId", c => c.Int());
            AddColumn("dbo.Addresses", "RequestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Requests", "Employee_EmployeeId");
            AddForeignKey("dbo.Requests", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
