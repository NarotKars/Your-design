using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProducts products { get; set; }
        private readonly IMapper mapper;
        private Products allAboutProducts = new Products();
        public ProductsController(IProducts products, IMapper mapper)
        {
            this.products = products;
            this.mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<APIProduct>> GetProducts()
        {
            List<APIProduct> prods = new List<APIProduct>();
            try
            {
                prods = mapper.Map<List<APIProduct>>(allAboutProducts.GetProducts());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return prods;
        }

        [HttpGet("{id:long}")]
        public ActionResult<APIProduct> GetProductById(long id)
        {
            APIProduct prod = new APIProduct();
            try
            {
                prod = mapper.Map<APIProduct>(allAboutProducts.GetProductById(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return prod;
        }
    }
}