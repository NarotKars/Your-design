using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class OrderDetails : IOrderDetails
    {
        public List<Product> GetOrdersDetailsByOrderId(long orderId)
        {
            List<Product> orders = new List<Product>();
            string sql = "GetOrderDetailsByOrderId";
            try
            {
                ConnectionManager.GetConnectionString();
                //ConnectionManager.GetWindowsAuthenticationConnectionString();
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        connection.Open();

                        command.Parameters.Add("@orderId", System.Data.SqlDbType.BigInt).Value = orderId;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int colId = dataReader.GetOrdinal("Id");
                                int colSellingPrice = dataReader.GetOrdinal("SellingPrice");
                                int colCompanyName = dataReader.GetOrdinal("Name");
                                int colProductInOrderId = dataReader.GetOrdinal("productInOrderId");
                                while (dataReader.Read())
                                {
                                    orders.Add(new Product
                                    {
                                        Id = dataReader.GetInt64(colId),
                                        SellingPrice = dataReader.GetDecimal(colSellingPrice),
                                        CompanyName = dataReader.GetString(colCompanyName),
                                        Photo = (byte[])(dataReader["Photo"]),
                                        ProductInOrderId = dataReader.GetInt64(colProductInOrderId)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                   "Message: " + ex.Errors[i].Message + "\n" +
                   "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                   "Source: " + ex.Errors[i].Source + "\n" +
                   "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                throw new Exception(errorMessages.ToString());
            }
            return orders;
        }

        public long InsertProductsInOrders(OrderDetail orderDetails)
        {
            string sql = "InsertProductsInOrders";
            try
            {
                ConnectionManager.GetConnectionString();
                //ConnectionManager.GetWindowsAuthenticationConnectionString();
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@customerId", System.Data.SqlDbType.BigInt).Value = orderDetails.CustomerId;
                        command.Parameters.Add("@productId", System.Data.SqlDbType.BigInt).Value = orderDetails.ProductId;
                        SqlParameter newProductInOrderId = command.CreateParameter();
                        newProductInOrderId.ParameterName = "@id";
                        newProductInOrderId.Direction = System.Data.ParameterDirection.Output;
                        newProductInOrderId.DbType = System.Data.DbType.Int64;
                        command.Parameters.Add(newProductInOrderId);

                        command.ExecuteNonQuery();
                        return (long)newProductInOrderId.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    if (ex.Errors[i].Number == 547)
                        throw new Exception("Either customer or product with this id doesn't exist");
                    else
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                       "Message: " + ex.Errors[i].Message + "\n" +
                       "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                       "Source: " + ex.Errors[i].Source + "\n" +
                       "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                }
                throw new Exception(errorMessages.ToString());
            }
        }
        public void UpdateStatusOfProductsInOrders(OrderDetail orderDetail)
        {
            string sql = "UpdateStatusOfProductsInOrders";
            try
            {
                ConnectionManager.GetConnectionString();
                //ConnectionManager.GetWindowsAuthenticationConnectionString();
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.Add("@id", SqlDbType.BigInt).Value = orderDetail.Id;
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = orderDetail.Status;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                   "Message: " + ex.Errors[i].Message + "\n" +
                   "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                   "Source: " + ex.Errors[i].Source + "\n" +
                   "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                throw new Exception(errorMessages.ToString());
            }
        }

        public long DeleteProductInOrder(long id)
        {
            string sql = "DeleteProductInOrder";
            try
            {
                ConnectionManager.GetConnectionString();
                //ConnectionManager.GetWindowsAuthenticationConnectionString();
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
                        command.ExecuteNonQuery();
                        return 10;
                    }
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                   "Message: " + ex.Errors[i].Message + "\n" +
                   "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                   "Source: " + ex.Errors[i].Source + "\n" +
                   "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                throw new Exception(errorMessages.ToString());
            }
        }
    }
}
