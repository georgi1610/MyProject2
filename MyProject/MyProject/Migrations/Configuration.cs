namespace MyProject.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MyProject.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//false;
            ContextKey = "MyProject.Models.ApplicationDbContext";
        }

        protected override void Seed(MyProject.Models.ApplicationDbContext context)
        {
         //   if (System.Diagnostics.Debugger.IsAttached == false)
         //       System.Diagnostics.Debugger.Launch();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
/*            context.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[Addresses] 
DROP CONSTRAINT [FK_dbo.Addresses_dbo.Countries_CountryId]");
            context.Database.ExecuteSqlCommand(@"ALTER TABLE [dbo].[Addresses]  
                        WITH CHECK ADD  CONSTRAINT [FK_dbo.Addresses_dbo.Countries_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
ON UPDATE CASCADE
ON DELETE CASCADE");*/
/*
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Carson",   LastName = "Smith", HireDate = DateTime.Now.AddHours(4), 
                               PersonalID = "12345667891242", EMail = "testcolina3@gmail.com", Department = "IT",
                               Position = "Developer", Headquarter = "Best IT", EMailPassword = "parola123"},
                new Employee { FirstName = "Carl",   LastName = "Smith", HireDate = DateTime.Now.AddHours(4), 
                               PersonalID = "12345667891242", EMail = "testcolina3@gmail.com", Department = "IT",
                               Position = "Developer", Headquarter = "Best IT", EMailPassword = "parola123"},
                new Employee { FirstName = "John",   LastName = "Smith", HireDate = DateTime.Now.AddHours(4), 
                               PersonalID = "12345667891242", EMail = "testcolina3@gmail.com", Department = "IT",
                               Position = "Developer", Headquarter = "Best IT", EMailPassword = "parola123"},
                new Employee { FirstName = "Alexander",   LastName = "Smith", HireDate = DateTime.Now.AddHours(4), 
                               PersonalID = "12345667891242", EMail = "testcolina3@gmail.com", Department = "IT",
                               Position = "Developer", Headquarter = "Best IT", EMailPassword = "parola123"},
                new Employee { FirstName = "Dan",   LastName = "Smith", HireDate = DateTime.Now.AddHours(4), 
                               PersonalID = "12345667891242", EMail = "testcolina3@gmail.com", Department = "Human Resources",
                               Position = "Accountant", Headquarter = "Best IT", EMailPassword = "parola123"},
                new Employee { FirstName = "Bob",   LastName = "Smith", HireDate = DateTime.Now.AddHours(4), 
                               PersonalID = "12345667891242", EMail = "testcolina3@gmail.com", Department = "Transport",
                               Position = "Driver", Headquarter = "Best IT", EMailPassword = "parola123"}
                
            };
            employees[1].SuperiorEmployee = employees[0];
            employees[2].SuperiorEmployee = employees[0];
            employees[3].SuperiorEmployee = employees[0];
            employees[4].SuperiorEmployee = employees[0];
            employees[5].SuperiorEmployee = employees[0];
            employees.ForEach(s => context.MyEmployee.Add(s));
            context.SaveChanges();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { MyEmployee = employees[0], UserName = employees[0].FullName},
                new ApplicationUser { MyEmployee = employees[1], UserName = employees[1].FullName},
                new ApplicationUser { MyEmployee = employees[2], UserName = employees[2].FullName},
                new ApplicationUser { MyEmployee = employees[3], UserName = employees[3].FullName},
                new ApplicationUser { MyEmployee = employees[4], UserName = employees[4].FullName},
                new ApplicationUser { MyEmployee = employees[5], UserName = employees[5].FullName}
            };
            string password = "password";
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var res = IdentityResult.Success;
            foreach (ApplicationUser a in users)
            {
                res = UserManager.Create(a, password);//creeaza username si parola
                res = UserManager.AddToRole(a.Id, "Employee");//atribuie rolul employee
            }
            context.SaveChanges();

            var statuses = new List<Status>
            {
                new Status { StatusName = "New"},
                new Status { StatusName = "Approved"},
                new Status { StatusName = "Denied"},
                new Status { StatusName = "Closed"}
                
            };
            statuses.ForEach(s => context.MyStatus.Add(s));
            context.SaveChanges();

            var delegTypes = new List<Delegation>
            {
                new Delegation { DelegationType = "Business Meeting"},
                new Delegation { DelegationType = "Training"},
                new Delegation { DelegationType = "Conference"}
               
            };
            delegTypes.ForEach(s => context.MyDelegation.Add(s));
            context.SaveChanges();

            var countries = new List<Country>
            {
                new Country { CountryName = "Romania"},
                new Country { CountryName = "Germany"},
                new Country { CountryName = "France"}
               
            };
            countries.ForEach(s => context.MyCountry.Add(s));
            context.SaveChanges();

            var addresses = new List<Address>
            {
                new Address { CompanyName="Best IT 1", StreetName="mystr1",Number=10, Country = countries[0]},
                new Address { CompanyName="Best IT 2", StreetName="mystr2",Number=20, Country = countries[1]},
                new Address { CompanyName="Best IT 3", StreetName="mystr3",Number=30, Country = countries[2]}
               
            };
            addresses.ForEach(s => context.MyAddress.Add(s));
            context.SaveChanges();

            var allowances = new List<Allowance>
            {
                new Allowance { Type="intern", Amount=40, Currency="RON"},
                new Allowance { Type="intern", Amount=50, Currency="RON"},
                new Allowance { Type="extern", Amount=100, Currency="EUR"}
            };
            allowances.ForEach(s => context.Allowances.Add(s));
            context.SaveChanges();

            var transportComps = new List<TransportCompany>
            {
                new TransportCompany { CompanyName = "Drive Taxi"},
                new TransportCompany { CompanyName = "Drive Com"},
                new TransportCompany { CompanyName = "Best Car"}

            };
            transportComps.ForEach(s => context.TransportCompanies.Add(s));
            context.SaveChanges();

            var transports = new List<Transport>
            {
                new Transport { TransportComp = transportComps[0], DepartureDateTime = DateTime.Now, DepartureTime = DateTime.Now,
                                DepartureAddress = "dep1", ArrivalAddress = "arr1", ArrivalDateTime = DateTime.Now.AddHours(3), ArrivalTime = DateTime.Now.AddHours(3),
                                Driver = employees[5]},
                new Transport { TransportComp = transportComps[1], DepartureDateTime = DateTime.Now, DepartureTime = DateTime.Now,
                                DepartureAddress = "dep2", ArrivalAddress = "arr2", ArrivalDateTime = DateTime.Now.AddHours(3), ArrivalTime = DateTime.Now.AddHours(3),
                                Driver = employees[5]},
                new Transport { TransportComp = transportComps[2], DepartureDateTime = DateTime.Now, DepartureTime = DateTime.Now,
                                DepartureAddress = "dep3", ArrivalAddress = "arr3", ArrivalDateTime = DateTime.Now.AddHours(3), ArrivalTime = DateTime.Now.AddHours(3),
                                Driver = employees[5]}

            };
            transports.ForEach(s => context.MyTransport.Add(s));
            context.SaveChanges();

            var requests = new List<Request>
            {
                new Request {Applicant = employees[1], Approver = employees[0], HREmployee = employees[4], Allowance = allowances[0],
                             Delegation = delegTypes[0], DepartureAddress = addresses[0], ReturnAddress = addresses[2],
                             DepartureDate = DateTime.Now.AddHours(3), ReturnDate = DateTime.Now.AddHours(10), StandIn = employees[0],
                             Description = "my request description", Status = statuses[0], Transport = transports[0]},
                new Request {Applicant = employees[2], Approver = employees[0], HREmployee = employees[4], Allowance = allowances[1],
                             Delegation = delegTypes[1], DepartureAddress = addresses[0], ReturnAddress = addresses[2],
                             DepartureDate = DateTime.Now.AddHours(3), ReturnDate = DateTime.Now.AddHours(10), StandIn = employees[0],
                             Description = "my request description", Status = statuses[0], Transport = transports[1]},
                new Request {Applicant = employees[3], Approver = employees[0], HREmployee = employees[4], Allowance = allowances[2],
                             Delegation = delegTypes[2], DepartureAddress = addresses[0], ReturnAddress = addresses[2],
                             DepartureDate = DateTime.Now.AddHours(3), ReturnDate = DateTime.Now.AddHours(10), StandIn = employees[0],
                             Description = "my request description", Status = statuses[0], Transport = transports[2]},

            };
            requests.ForEach(s => context.MyRequest.Add(s));
  *///          context.SaveChanges();

        }
    }
}
