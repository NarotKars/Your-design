using AutoMapper;
using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using WEB_API.Controllers;
using WEB_API.Models;
using Xunit;
using System.Linq;
using WEB_API;

namespace Test
{
    public class CustomersTest
    {
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.AddProfile<AutoMapperProfile>();
        });
        [Fact]
        public void CustomersTestGetCustomersPersonalInformation()
        {
           /* CustomersController companiesController = new CustomersController(new Customers(), new Mapper(config));
            var result = companiesController.GetCustomersPersonalInformation(1).Value;
            Assert.NotNull(result.Name);
            Assert.NotNull(result.PhoneNumber);
            Assert.NotEqual(0, result.UserId);
            Assert.NotNull(result.Email);*/
        }
        [Fact]
        public void CompaniesTestPostCustomer()
        {
            /*CustomersController customersController = new CustomersController(new Customers(), new Mapper(config));
            APICustomersPersonalInformation personalInformation = new APICustomersPersonalInformation();
            personalInformation.Name = "Test";
            personalInformation.Password = "Test";
            personalInformation.Email = "test@yourdesign.com";
            personalInformation.PhoneNumber = "+37412345698";
            APIAddress address = new APIAddress();
            address.City = "Test";
            address.Street = "Test";
            address.Number = "Test";
            personalInformation.Address = address;
            long result = customersController.PostCustomer(personalInformation).Value;
            Assert.True(result != 0);*/
        }
    }
}
