namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transports", "DriverId_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Transports", new[] { "DriverId_EmployeeId" });
            AddColumn("dbo.Transports", "DriverId", c => c.Int(nullable: false));
            AddColumn("dbo.Transports", "Driver_EmployeeId", c => c.Int());
            CreateIndex("dbo.Transports", "Driver_EmployeeId");
            AddForeignKey("dbo.Transports", "Driver_EmployeeId", "dbo.Employees", "EmployeeId");
            DropColumn("dbo.Transports", "DriverId_EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transports", "DriverId_EmployeeId", c => c.Int());
            DropForeignKey("dbo.Transports", "Driver_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Transports", new[] { "Driver_EmployeeId" });
            DropColumn("dbo.Transports", "Driver_EmployeeId");
            DropColumn("dbo.Transports", "DriverId");
            CreateIndex("dbo.Transports", "DriverId_EmployeeId");
            AddForeignKey("dbo.Transports", "DriverId_EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
