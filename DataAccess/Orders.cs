using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Orders : IOrders
    {
        readonly DateTime dateTime = new DateTime();
        public List<Order> GetOrders(DateTime orderDate = new DateTime(), long customerId = 0)
        {
            List<Order> orders = new List<Order>();
            string sql = "GetOrders";
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
                        if (orderDate != dateTime)
                            command.Parameters.Add("@date", System.Data.SqlDbType.DateTime).Value = orderDate;
                        else if (customerId != 0)
                            command.Parameters.Add("@customerId", System.Data.SqlDbType.BigInt).Value = customerId;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int colId = dataReader.GetOrdinal("Id");
                                int colDate = dataReader.GetOrdinal("OrderDate");
                                int colCity = dataReader.GetOrdinal("City");
                                int colStreet = dataReader.GetOrdinal("street");
                                int colNumber = dataReader.GetOrdinal("number");
                                int colAmount = dataReader.GetOrdinal("Amount");
                                int colStatus = dataReader.GetOrdinal("Status");
                                while (dataReader.Read())
                                {
                                    Address address = new Address();
                                    address.City = dataReader.GetString(colCity);
                                    address.Street = dataReader.GetString(colStreet);
                                    address.Number = dataReader.GetString(colNumber);
                                    orders.Add(new Order
                                    {
                                        Id = dataReader.GetInt64(colId),
                                        Address = address,
                                        Date = dataReader.GetDateTime(colDate),
                                        Amount = dataReader.GetDecimal(colAmount),
                                        Status = dataReader.GetString(colStatus),
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

        

        public List<OrderDetailsCompany> GetOrdersDetailsByCompany(long companyId)
        {
            List<OrderDetailsCompany> productsInOrders = new List<OrderDetailsCompany>();
            string sql = "GetOrderDetailsByCompany";
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

                        command.Parameters.Add("@companyId", System.Data.SqlDbType.BigInt).Value = companyId;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int colId = dataReader.GetOrdinal("Id");
                                int colStatus = dataReader.GetOrdinal("Status");
                                int colName = dataReader.GetOrdinal("Name");
                                int colPhoneNumber = dataReader.GetOrdinal("PhoneNumber");
                                while (dataReader.Read())
                                {
                                    productsInOrders.Add(new OrderDetailsCompany
                                    {
                                        Id = dataReader.GetInt64(colId),
                                        Status = dataReader.GetString(colStatus),
                                        Photo = (byte[])(dataReader["Photo"]),
                                        Name = dataReader.GetString(colName),
                                        PhoneNumber = dataReader.GetString(colPhoneNumber)
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
            return productsInOrders;
        }

        public long InsertOrder(Order order)
        {
            string sql = "InsertOrder";
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
                        command.Parameters.Add("@orderDate", System.Data.SqlDbType.DateTime).Value = order.Date;
                        command.Parameters.Add("@amount", System.Data.SqlDbType.Decimal).Value = order.Amount;
                        command.Parameters.Add("@status", System.Data.SqlDbType.VarChar).Value = order.Status;
                        command.Parameters.Add("@customerId", System.Data.SqlDbType.BigInt).Value = order.CustomerId;
                        SqlParameter newOrderId = command.CreateParameter();
                        newOrderId.ParameterName = "@id";
                        newOrderId.Direction = System.Data.ParameterDirection.Output;
                        newOrderId.DbType = System.Data.DbType.Int64;
                        command.Parameters.Add(newOrderId);

                        command.ExecuteNonQuery();
                        return (long)newOrderId.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    if (ex.Errors[i].Number == 547)
                        throw new Exception("A customer with this id doesn't exist");
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

        

        public void UpdateOrderStatus(Order order)
        {
            string sql = "UpdateOrderStatus";
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

                        command.Parameters.Add("@orderId", System.Data.SqlDbType.BigInt).Value = order.Id;
                        command.Parameters.Add("@customerId", System.Data.SqlDbType.BigInt).Value = order.CustomerId;
                        command.Parameters.Add("@city", System.Data.SqlDbType.VarChar).Value = order.Address.City;
                        command.Parameters.Add("@street", System.Data.SqlDbType.VarChar).Value = order.Address.Street;
                        command.Parameters.Add("@number", System.Data.SqlDbType.VarChar).Value = order.Address.Number;
                        command.Parameters.Add("@status", System.Data.SqlDbType.VarChar).Value = order.Status;
                        command.Parameters.Add("@phoneNumber", System.Data.SqlDbType.VarChar).Value = order.PhoneNumber;

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
    }
}
