using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject.Controllers;
using System.Web.Mvc;
using MyProject.Models;
using MyProject.Validation;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MyUnitTestProject
{
    [TestClass]
    public class MyUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {

            var empController = new EmployeeController();
            
            var emp = new Employee();
            emp.EmployeeId = 1;
            emp.FirstName = "Irina";
            emp.LastName = "Ban";
            emp.PersonalID = "2881214123456";
            emp.HireDate = DateTime.Now;
            emp.Position = "position";
            emp.Department = "dep";
            emp.EMail = "irina.mail@yahoo.de";
            emp.EMailPassword = "password";
            /*
            empController.ModelState.Clear();
           
            var validationContext = new ValidationContext(emp, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(emp, validationContext, validationResults,true);
            foreach (var validationResult in validationResults)
            {
                foreach(var name in validationResult.MemberNames)
                    empController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsTrue(ismodelvalid);
            */
            var user = new ApplicationUser();
            user.MyEmployee = emp;
            user.EmployeeId = emp.EmployeeId;
            
            empController.ModelState.Clear();

            var validationContext = new ValidationContext(user, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(user, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var name in validationResult.MemberNames)
                    empController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsTrue(ismodelvalid);


        }
    }
}
