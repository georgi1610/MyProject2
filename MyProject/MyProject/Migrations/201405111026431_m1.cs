namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransportCompanies",
                c => new
                    {
                        TransportCompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TransportCompanyId);
            
            AddColumn("dbo.Transports", "DepartureDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transports", "ArrivalDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Transports", "ArrivalAddress_AddressId", c => c.Int());
            AddColumn("dbo.Transports", "DepartureAddress_AddressId", c => c.Int());
            AddColumn("dbo.Transports", "DriverId_EmployeeId", c => c.Int());
            AddColumn("dbo.Transports", "TransportCompanyId_TransportCompanyId", c => c.Int());
            CreateIndex("dbo.Transports", "ArrivalAddress_AddressId");
            CreateIndex("dbo.Transports", "DepartureAddress_AddressId");
            CreateIndex("dbo.Transports", "DriverId_EmployeeId");
            CreateIndex("dbo.Transports", "TransportCompanyId_TransportCompanyId");
            AddForeignKey("dbo.Transports", "ArrivalAddress_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Transports", "DepartureAddress_AddressId", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Transports", "DriverId_EmployeeId", "dbo.Employees", "EmployeeId");
            AddForeignKey("dbo.Transports", "TransportCompanyId_TransportCompanyId", "dbo.TransportCompanies", "TransportCompanyId");
            DropColumn("dbo.Transports", "TransportCompany");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transports", "TransportCompany", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.Transports", "TransportCompanyId_TransportCompanyId", "dbo.TransportCompanies");
            DropForeignKey("dbo.Transports", "DriverId_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Transports", "DepartureAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Transports", "ArrivalAddress_AddressId", "dbo.Addresses");
            DropIndex("dbo.Transports", new[] { "TransportCompanyId_TransportCompanyId" });
            DropIndex("dbo.Transports", new[] { "DriverId_EmployeeId" });
            DropIndex("dbo.Transports", new[] { "DepartureAddress_AddressId" });
            DropIndex("dbo.Transports", new[] { "ArrivalAddress_AddressId" });
            DropColumn("dbo.Transports", "TransportCompanyId_TransportCompanyId");
            DropColumn("dbo.Transports", "DriverId_EmployeeId");
            DropColumn("dbo.Transports", "DepartureAddress_AddressId");
            DropColumn("dbo.Transports", "ArrivalAddress_AddressId");
            DropColumn("dbo.Transports", "ArrivalDateTime");
            DropColumn("dbo.Transports", "DepartureDateTime");
            DropTable("dbo.TransportCompanies");
        }
    }
}
