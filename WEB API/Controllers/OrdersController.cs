using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper mapper;
        private IOrders orders { get; set; }
        private readonly Orders allAboutOrders = new Orders();
        public OrdersController(IOrders orders, IMapper mapper)
        {
            this.orders = orders;
            this.mapper = mapper;
        }
        [HttpGet("{customerId:long}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<APIOrder>> GetOrdersByCustomerId(long customerId)
        {
            List<APIOrder> customerOrders = new List<APIOrder>();
            try
            {
                customerOrders = mapper.Map<List<APIOrder>>(allAboutOrders.GetOrders(new DateTime(), customerId));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return customerOrders;
        }

        [HttpGet("Company/{companyId:long}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<ApiOrderDetailsCompany>> GetOrderDetailsByCompany(long companyId)
        {
            List<ApiOrderDetailsCompany> companyOrders = new List<ApiOrderDetailsCompany>();
            try
            {
                companyOrders = mapper.Map<List<ApiOrderDetailsCompany>>(allAboutOrders.GetOrdersDetailsByCompany(companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return companyOrders; 
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult PostOrder(APIPostOrder order)
        {
            OrderDetails orderDetails = new OrderDetails();
            long orderId;
            try
            {
                orderId= allAboutOrders.InsertOrder(mapper.Map<Order>(order));
            }
            catch(Exception ex)
            {
                if (ex.Message.StartsWith("A customer")) return BadRequest(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (orderId == 0) return BadRequest("An error occurred");
            if (order.ProductId == 0)
            {
                Products products = new Products();
                long productId;
                try
                {
                    productId = products.InsertCustomerDesign(mapper.Map<Product>(order));
                }
                catch(Exception ex)
                {
                    if (ex.Message.StartsWith("Either")) return BadRequest(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                APIOrderDetails orderDetail = new APIOrderDetails();
                orderDetail.CustomerId = order.CustomerId;
                orderDetail.ProductId = productId;
                long newProductInOrderId;
                try
                {
                    newProductInOrderId=orderDetails.InsertProductsInOrders(mapper.Map<OrderDetail>(orderDetail));
                }
                catch(Exception ex)
                {
                    if (ex.Message.StartsWith("Either")) return BadRequest(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                if (newProductInOrderId == 0) return BadRequest("An error occurred");
            }
            else
            {
                /*APIOrderDetails orderDetails = new APIOrderDetails();
                orderDetails.CustomerId = order.CustomerId;
                orderDetails.ProductId = order.ProductId;*/
                long newProductInOrderId;
                try
                {
                    newProductInOrderId=orderDetails.InsertProductsInOrders(mapper.Map<OrderDetail>(order));
                }
                catch(Exception ex)
                {
                    if (ex.Message.StartsWith("Either")) return BadRequest(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                if (newProductInOrderId == 0) return BadRequest("An error occurred");
            }
            return Created("", "The order is created successfully with one product in it");
        }

        

        [HttpPost("SetDesignInOrder")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult PostDesignInOrders(APIPostOrder order)
        {
            Products products = new Products();
            OrderDetails orderDetails = new OrderDetails();
            long productId;
            try
            {
                productId = products.InsertCustomerDesign(mapper.Map<Product>(order));
            }
            catch(Exception ex)
            {
                if (ex.Message.StartsWith("Either")) return BadRequest(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (productId == 0) return BadRequest("An error occurred");
            APIOrderDetails orderDetail = new APIOrderDetails();
            orderDetail.CustomerId = order.CustomerId;
            orderDetail.ProductId = productId;
            long newProductInOrderId;
            try
            {
                newProductInOrderId=orderDetails.InsertProductsInOrders(mapper.Map<OrderDetail>(orderDetail));
            }
            catch(Exception ex)
            {
                if (ex.Message.StartsWith("Either")) return BadRequest(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (productId == 0) return BadRequest("An error occurred");
            return Created("", "The customer design is successfully added to the products and also in customer's current order");
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult UpdateOrderStatusAndAddress(APIUpdateOrderStatus order)
        {
            try
            {
                allAboutOrders.UpdateOrderStatus(mapper.Map<Order>(order));
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Created("", "The status of order is successfully updated");
        }
    }
}