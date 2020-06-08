using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess;
using AutoMapper;
using WEB_API.Models;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private ICategories Categories { get; set; }
        public CategoriesController(ICategories Categories, IMapper mapper)
        {
            this.Categories = Categories;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<APICategory>> GetCategories()
        {
            Categories categories = new Categories();
            List<APICategory> category = new List<APICategory>();
            try
            {
                category=mapper.Map<List<APICategory>>(categories.GetCategories());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return category;
        }
    }
}