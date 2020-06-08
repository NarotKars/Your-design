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
using Microsoft.AspNetCore.Mvc;

namespace Test
{
    public class CategoriesTest
    {
        MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.AddProfile<AutoMapperProfile>();
        });
        [Fact]
        public void GetCategoriesTest()
        {
            CategoriesController categoriesController = new CategoriesController(new Categories(), new Mapper(config));
            var result = categoriesController.GetCategories().Value;

            Assert.NotEmpty(result);
            Assert.NotNull(result.Select(item => item.Name));
            Assert.True(result.All(item => item.Id != 0));
            Assert.NotNull(result.Select(item => item.Id));
        }
    }
}
