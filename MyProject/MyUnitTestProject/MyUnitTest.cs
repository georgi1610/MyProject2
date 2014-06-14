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
        public void TestAspNetUserOk()
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

        [TestMethod]
        public void TestAspNetUserFailed()
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

            var user = new ApplicationUser();
            user.MyEmployee = emp;
            //user.EmployeeId = emp.EmployeeId;

            empController.ModelState.Clear();

            var validationContext = new ValidationContext(user, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(user, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var name in validationResult.MemberNames)
                    empController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsFalse(ismodelvalid);
        }

        [TestMethod]
        public void TestEmployeeFailed()
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
            emp.EMail = "irina@de";
            emp.EMailPassword = "password";

           
            empController.ModelState.Clear();

            var validationContext = new ValidationContext(emp, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(emp, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var name in validationResult.MemberNames)
                    empController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsFalse(ismodelvalid);
        }

        [TestMethod]
        public void TestStatusOk()
        {

            var statusController = new StatusController();

            var status = new Status();
            status.StatusId = 1;
            status.StatusName = "New";
            
            statusController.ModelState.Clear();

            var validationContext = new ValidationContext(status, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(status, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var name in validationResult.MemberNames)
                    statusController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsTrue(ismodelvalid);
        }

        [TestMethod]
        public void TestStatusFailed()
        {

            var statusController = new StatusController();

            var status = new Status();
            status.StatusId = 1;
            status.StatusName = "Ne";

            statusController.ModelState.Clear();

            var validationContext = new ValidationContext(status, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(status, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var name in validationResult.MemberNames)
                    statusController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsFalse(ismodelvalid);
        }

        [TestMethod]
        public void TestAddressOk()
        {
            var addressController = new AddressController();

            var country = new Country();
            country.CountryId = 1;
            country.CountryName = "Romania";

            var address = new Address();
            address.AddressId = 1;
            address.CompanyName = "company name";
            address.Country = country;
            address.StreetName = "street";
            address.Number = 10;
            address.PostalCode = "123456";
 
            addressController.ModelState.Clear();

            var validationContext = new ValidationContext(address, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(address, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var name in validationResult.MemberNames)
                    addressController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsTrue(ismodelvalid);
        }

        [TestMethod]
        public void TestTransportOk()
        {
            var transportController = new TransportController();

            var country = new Country();
            country.CountryId = 1;
            country.CountryName = "Romania";

            var address = new Address();
            address.AddressId = 1;
            address.CompanyName = "company name";
            address.Country = country;
            address.StreetName = "street";
            address.Number = 10;
            address.PostalCode = "123456";

            var transportCompany = new TransportCompany();
            transportCompany.TransportCompanyId = 1;
            transportCompany.CompanyName = "transport company";

            var emp = new Employee();
            emp.EmployeeId = 1;
            emp.FirstName = "Dan";
            emp.LastName = "Toma";
            emp.PersonalID = "1881214123456";
            emp.HireDate = DateTime.Now;
            emp.Position = "driver";
            emp.Department = "transport";
            emp.EMail = "dan.toma@yahoo.de";
            emp.EMailPassword = "password";

            var transport = new Transport();
            transport.TransportId = 1;
            transport.TransportComp = transportCompany;
            transport.DepartureAddress = "departure address";
            transport.DepartureDateTime = DateTime.Now.AddDays(1);
            transport.DepartureTime = DateTime.Now.AddDays(1);
            transport.ArrivalAddress = "arrival address";
            transport.ArrivalDateTime = DateTime.Now.AddDays(10);
            transport.ArrivalTime = DateTime.Now.AddDays(10);
            transport.Driver = emp;

            transportController.ModelState.Clear();

            var validationContext = new ValidationContext(transport, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(transport, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var name in validationResult.MemberNames)
                    transportController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsTrue(ismodelvalid);
        }

        [TestMethod]
        public void TestRequestOk()
        {
            var requestController = new RequestController();

            var country = new Country();
            country.CountryId = 1;
            country.CountryName = "Romania";

            var addressDep = new Address();
            addressDep.AddressId = 1;
            addressDep.CompanyName = "company name dep";
            addressDep.Country = country;
            addressDep.StreetName = "street dep";
            addressDep.Number = 10;
            addressDep.PostalCode = "123456";

            var addressRet = new Address();
            addressRet.AddressId = 1;
            addressRet.CompanyName = "company name ret";
            addressRet.Country = country;
            addressRet.StreetName = "street ret";
            addressRet.Number = 10;
            addressRet.PostalCode = "123456";

            var empApplicant = new Employee();
            empApplicant.EmployeeId = 1;
            empApplicant.FirstName = "Dan";
            empApplicant.LastName = "Toma";
            empApplicant.PersonalID = "1881214123456";
            empApplicant.HireDate = DateTime.Now;
            empApplicant.Position = "developer";
            empApplicant.Department = "IT";
            empApplicant.EMail = "dan.toma@yahoo.de";
            empApplicant.EMailPassword = "password";

            var request = new Request();
            request.RequestId = 1;
            request.DepartureAddress = addressDep;
            request.ReturnAddress = addressRet;
            request.Status = new Status() { StatusId = 1, StatusName = "New"};
            request.SubmitDate = DateTime.Now;
            request.DepartureDate = DateTime.Now.AddDays(10);
            request.ReturnDate = DateTime.Now.AddDays(13);
            request.Applicant = empApplicant;
            request.Delegation = new Delegation() { DelegationId = 1, DelegationType = "Business meeting"};
            request.Description = "description";
            
            requestController.ModelState.Clear();

            var validationContext = new ValidationContext(request, null, null);
            var validationResults = new List<ValidationResult>();
            var ismodelvalid = Validator.TryValidateObject(request, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                foreach (var name in validationResult.MemberNames)
                    requestController.ModelState.AddModelError(name, validationResult.ErrorMessage);
            }
            Assert.IsTrue(ismodelvalid);
        }

    }
}

        