using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IProducts
    {
        List<Product> GetProducts();
        Product GetProductById(long productId);
    }
}
