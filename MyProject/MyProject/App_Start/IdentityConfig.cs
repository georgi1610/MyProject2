using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyProject.App_Start
{
//    public class MyDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    public class MyDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
       /* protected void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasRequired(d => d.FirstName).WithMany().WillCascadeOnDelete(true);
        }*/

        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            context.Database.ExecuteSqlCommand("ALTER TABLE AspNetUsers ADD CONSTRAINT constr UNIQUE(EmployeeId)");
    
            base.Seed(context);
        }

        private void InitializeIdentityForEF(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var myinfo = new MyUserInfo() { FirstName = "Pranav", LastName = "Rastogi" };
            string name = "Admin";
            string password = "123456";
            //string test = "test";
            
            //Create Role Test and User Test
            //RoleManager.Create(new IdentityRole(test));
            //UserManager.Create(new ApplicationUser() { UserName = test });

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }
            if (!RoleManager.RoleExists("Employee"))
            {
                var roleresult = RoleManager.Create(new IdentityRole("Employee"));
            }


            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = name;
            //user.HomeTown = "Seattle";
            //user.MyUserInfo = myinfo;
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
        }
    }
}