using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IOrderDetails
    {
        List<Product> GetOrdersDetailsByOrderId(long orderId);
        long InsertProductsInOrders(OrderDetail orderDetails);
        void UpdateStatusOfProductsInOrders(OrderDetail orderDetail);
    }
}
