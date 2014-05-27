using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MyProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int? EmployeeId { get; set; }
        public Employee MyEmployee { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("MyConn")//DefaultConnection")
        {
        }

        public DbSet<Employee> MyEmployee { get; set; }
        public DbSet<Country> MyCountry { get; set; }
        public DbSet<Address> MyAddress { get; set; }
        public DbSet<Status> MyStatus { get; set; }
        public DbSet<Delegation> MyDelegation { get; set; }
        public DbSet<Transport> MyTransport { get; set; }
        public DbSet<Request> MyRequest { get; set; }

        public DbSet<Allowance> Allowances { get; set; }

        public DbSet<TransportCompany> TransportCompanies { get; set; }


    }
}