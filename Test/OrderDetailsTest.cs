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
    public class OrderDetailsTest
    {
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.AddProfile<AutoMapperProfile>();
        });
        [Fact]
         public void OrderDetailsTestGetOrderDetailsByOrderId()
        {
            OrderDetailsController orderDetailsController = new OrderDetailsController(new OrderDetails(), new Mapper(config));
            var result = orderDetailsController.GetOrderDetailsByOrderId(151).Value;
            Assert.NotEmpty(result);
            Assert.True(result.All(item => item.Id != 0));
        } 
    }
}
