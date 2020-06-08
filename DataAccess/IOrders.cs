using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IOrders
    {
        List<Order> GetOrders(DateTime orderDate = new DateTime(), long customerId = 0);
        List<OrderDetailsCompany> GetOrdersDetailsByCompany(long companyId);
        long InsertOrder(Order order);
        void UpdateOrderStatus(Order order);
    }
}
