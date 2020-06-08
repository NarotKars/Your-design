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
    public class ProductsTest
    {
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.AddProfile<AutoMapperProfile>();
        });
        [Fact]
        public void ProductsTestGetProducts()
        {
            ProductsController productsController = new ProductsController(new Products(), new Mapper(config));
            var result = productsController.GetProducts().Value;
            Assert.NotEmpty(result);
            Assert.True(result.All(item => item.Id != 0));
            Assert.NotNull(result.Select(item => item.CompanyName));
            Assert.True(result.All(item => item.SellingPrice != 0));
            Assert.NotNull(result.Select(item => item.Photo));
        }
        
        [Fact]
        public void ProductsTestGetProductById()
        {
            /*ProductsController productsController = new ProductsController(new Products(), new Mapper(config));
            var result = productsController.GetProductById(74).Value;
            Assert.NotEmpty(result);
            Assert.True(result.All(item => item.Id != 0));
            Assert.NotNull(result.Select(item => item.CompanyName));
            Assert.True(result.All(item => item.SellingPrice != 0));
            Assert.NotNull(result.Select(item => item.Photo));*/

        }
    }
}
