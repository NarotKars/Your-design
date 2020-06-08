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
    public class OrderDetailsController : ControllerBase
    {
        private IOrderDetails orderDetails { get; set; }
        private readonly IMapper mapper;
        private readonly OrderDetails orderDetail = new OrderDetails();
        public OrderDetailsController(IOrderDetails orderDetails, IMapper mapper)
        {
            this.orderDetails = orderDetails;
            this.mapper = mapper;
        }
        [HttpGet("{orderId:long}")]
        public ActionResult<IEnumerable<APIProductDetails>> GetOrderDetailsByOrderId(long orderId)
        {
            List<APIProductDetails> products = new List<APIProductDetails>();
            try
            {
                products = mapper.Map<List<APIProductDetails>>(orderDetail.GetOrdersDetailsByOrderId(orderId));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return products;
        }

        [HttpPost("SetProductInCustomersCurrentOrder")]
        public ActionResult<long> PostProductInCustomersCurrentOrder(APIOrderDetails orderDetails)
        {
            long newProductInOrderId;
            try
            {
                newProductInOrderId=orderDetail.InsertProductsInOrders(mapper.Map<OrderDetail>(orderDetails));
            }
            catch(Exception ex)
            {
                if(ex.Message.StartsWith("Either")) return BadRequest(ex.Message);
                else return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (newProductInOrderId == 0) return BadRequest("An error occurred");
            return Created("", "The product is successfully added into the customer's current order");
        }
        [HttpPut("ProductsInOrders/{id}")]
        public ActionResult UpdateStatusOfProductsInOrders(APIUpdateStatusOfProductInOrder orderDetails)
        {
            try
            {
                orderDetail.UpdateStatusOfProductsInOrders(mapper.Map<OrderDetail>(orderDetails));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
                //return BadRequest(ex.Message);
            }
            return Created("", "The status is successfully changed");
        }

        [HttpDelete("ProductsInOrders/{id:long}")]
        public ActionResult DeleteProductInOrder(long id)
        {
            try
            {
                orderDetail.DeleteProductInOrder(id);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Created("", "The product is deleted");
        }
    }
}