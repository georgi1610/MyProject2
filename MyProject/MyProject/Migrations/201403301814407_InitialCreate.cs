namespace MyProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false),
                        StreetName = c.String(nullable: false),
                        Number = c.Short(nullable: false),
                        PostalCode = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        CountryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Delegations",
                c => new
                    {
                        DelegationId = c.Int(nullable: false, identity: true),
                        DelegationType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DelegationId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        SubmitDate = c.DateTime(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        DelegationId = c.Int(nullable: false),
                        DepartureAddressId = c.Int(nullable: false),
                        ReturnAddressId = c.Int(nullable: false),
                        ApplicantId = c.Int(nullable: false),
                        ApproverId = c.Int(nullable: false),
                        Allowance_AllowanceId = c.Int(),
                        Employee_EmployeeId = c.Int(),
                        Applicant_EmployeeId = c.Int(),
                        Approver_EmployeeId = c.Int(),
                        DepartureAddress_AddressId = c.Int(),
                        HREmployee_EmployeeId = c.Int(),
                        ReturnAddress_AddressId = c.Int(),
                        StandIn_EmployeeId = c.Int(),
                        Transport_TransportId = c.Int(),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Allowances", t => t.Allowance_AllowanceId)
                .ForeignKey("dbo.Employees", t => t.Employee_EmployeeId)
                .ForeignKey("dbo.Employees", t => t.Applicant_EmployeeId)
                .ForeignKey("dbo.Employees", t => t.Approver_EmployeeId)
                .ForeignKey("dbo.Delegations", t => t.DelegationId, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.DepartureAddress_AddressId)
                .ForeignKey("dbo.Employees", t => t.HREmployee_EmployeeId)
                .ForeignKey("dbo.Addresses", t => t.ReturnAddress_AddressId)
                .ForeignKey("dbo.Employees", t => t.StandIn_EmployeeId)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Transports", t => t.Transport_TransportId)
                .Index(t => t.Allowance_AllowanceId)
                .Index(t => t.Employee_EmployeeId)
                .Index(t => t.Applicant_EmployeeId)
                .Index(t => t.Approver_EmployeeId)
                .Index(t => t.DelegationId)
                .Index(t => t.DepartureAddress_AddressId)
                .Index(t => t.HREmployee_EmployeeId)
                .Index(t => t.ReturnAddress_AddressId)
                .Index(t => t.StandIn_EmployeeId)
                .Index(t => t.StatusId)
                .Index(t => t.Transport_TransportId);
            
            CreateTable(
                "dbo.Allowances",
                c => new
                    {
                        AllowanceId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Amount = c.Int(nullable: false),
                        Currency = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AllowanceId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        PersonalID = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                        Position = c.String(nullable: false, maxLength: 50),
                        Department = c.String(nullable: false, maxLength: 50),
                        SuperiorEmployee_EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Employees", t => t.SuperiorEmployee_EmployeeId)
                .Index(t => t.SuperiorEmployee_EmployeeId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        StatusName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "dbo.Transports",
                c => new
                    {
                        TransportId = c.Int(nullable: false, identity: true),
                        TransportCompany = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TransportId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        EmployeeId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "Transport_TransportId", "dbo.Transports");
            DropForeignKey("dbo.Requests", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Requests", "StandIn_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "ReturnAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Requests", "HREmployee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "DepartureAddress_AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Requests", "DelegationId", "dbo.Delegations");
            DropForeignKey("dbo.Requests", "Approver_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "Applicant_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "SuperiorEmployee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "Employee_EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "Allowance_AllowanceId", "dbo.Allowances");
            DropForeignKey("dbo.Addresses", "CountryId", "dbo.Countries");
            DropIndex("dbo.AspNetUsers", new[] { "EmployeeId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Requests", new[] { "Transport_TransportId" });
            DropIndex("dbo.Requests", new[] { "StatusId" });
            DropIndex("dbo.Requests", new[] { "StandIn_EmployeeId" });
            DropIndex("dbo.Requests", new[] { "ReturnAddress_AddressId" });
            DropIndex("dbo.Requests", new[] { "HREmployee_EmployeeId" });
            DropIndex("dbo.Requests", new[] { "DepartureAddress_AddressId" });
            DropIndex("dbo.Requests", new[] { "DelegationId" });
            DropIndex("dbo.Requests", new[] { "Approver_EmployeeId" });
            DropIndex("dbo.Requests", new[] { "Applicant_EmployeeId" });
            DropIndex("dbo.Employees", new[] { "SuperiorEmployee_EmployeeId" });
            DropIndex("dbo.Requests", new[] { "Employee_EmployeeId" });
            DropIndex("dbo.Requests", new[] { "Allowance_AllowanceId" });
            DropIndex("dbo.Addresses", new[] { "CountryId" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Transports");
            DropTable("dbo.Status");
            DropTable("dbo.Employees");
            DropTable("dbo.Allowances");
            DropTable("dbo.Requests");
            DropTable("dbo.Delegations");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
        }
    }
}
