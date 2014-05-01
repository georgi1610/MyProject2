using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject.Controllers;
using System.Web.Mvc;
using MyProject.Models;

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
            emp.FirstName = "Irina";
            emp.LastName = "Ban";
            emp.PersonalID = "2881214123456";
            emp.HireDate = DateTime.Now;
            emp.Position = "position";
            emp.Department = "dep";
            emp.EMail = "irina.mail";
            emp.EMailPassword = "pass";

            empController.Create(emp, 1, 2);

            // Act
//            bool isValid = ...

            //Assert
//            Assert.IsFalse(isValid);

        }
    }
}
