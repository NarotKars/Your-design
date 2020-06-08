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
    public class OrdersTest
    {
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.AddProfile<AutoMapperProfile>();
        });
        [Fact]
        public void GetOrdersTest()
        {
            OrdersController ordersController = new OrdersController(new Orders(), new Mapper(config));
            var result = ordersController.GetOrdersByCustomerId(13).Value;
            Assert.NotEmpty(result);
        }
    }
}
