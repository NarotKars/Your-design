using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class Products : IProducts
    {
        public long InsertProduct(Product product)
        {
            string sql = "InsertProduct";
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        command.Parameters.Add("@imageId", SqlDbType.BigInt).Value = product.ImageId;
                        command.Parameters.Add("@companyId", SqlDbType.Int).Value = product.CompanyId;
                        command.Parameters.Add("@categoryId", SqlDbType.Int).Value = product.CategoryId;
                        command.Parameters.Add("@buyingPrice", SqlDbType.Decimal).Value = product.BuyingPrice;
                        command.Parameters.Add("@sellingPrice", SqlDbType.Decimal).Value = product.SellingPrice;
                        SqlParameter newProductId = command.CreateParameter();
                        newProductId.ParameterName = "@id";
                        newProductId.Direction = System.Data.ParameterDirection.Output;
                        newProductId.DbType = System.Data.DbType.Int64;
                        command.Parameters.Add(newProductId);
                        command.ExecuteNonQuery();
                        return (long)newProductId.Value;
                    }
                }
            }
            catch(SqlException ex)
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
        public List<Product> GetProductDetails(long imgId)
        {
            List<Product> products = new List<Product>();
            string sql = "GetProductDetails";
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@imageId", SqlDbType.BigInt).Value = imgId;
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int colName = dataReader.GetOrdinal("Name");
                                int colBuyingPrice = dataReader.GetOrdinal("BuyingPrice");
                                int colSellingPrice = dataReader.GetOrdinal("SellingPrice");
                                while (dataReader.Read())
                                {
                                    products.Add(new Product
                                    {
                                        CompanyName = dataReader.GetString(colName),
                                        BuyingPrice = dataReader.GetDecimal(colBuyingPrice),
                                        SellingPrice = dataReader.GetDecimal(colSellingPrice)
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
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
            return products;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            string sql = "GetProducts";
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
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int colId = dataReader.GetOrdinal("Id");
                                int colCompanyName = dataReader.GetOrdinal("CompanyName");
                                int colSellingPrice = dataReader.GetOrdinal("SellingPrice");
                                int colStatus = dataReader.GetOrdinal("Status");
                                int colCategory = dataReader.GetOrdinal("CategoryName");
                                int colBuyingPrice = dataReader.GetOrdinal("BuyingPrice");
                                int colCategoryId = dataReader.GetOrdinal("CategoryId");
                                int colCompanyId = dataReader.GetOrdinal("CompanyId");
                                while (dataReader.Read())
                                {
                                    products.Add(new Product
                                    {
                                        Id = dataReader.GetInt64(colId),
                                        CompanyName = dataReader.GetString(colCompanyName),
                                        SellingPrice = dataReader.GetDecimal(colSellingPrice),
                                        Photo = (byte[])(dataReader["Photo"]),
                                        Status = dataReader.GetString(colStatus),
                                        CategoryName = dataReader.GetString(colCategory),
                                        BuyingPrice = dataReader.GetDecimal(colBuyingPrice),
                                        CompanyId = dataReader.GetInt64(colCompanyId),
                                        CategoryId=dataReader.GetInt32(colCategoryId)
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
            return products;
        }

        public Product GetProductById(long productId)
        {
            Product product = new Product();
            string sql = "GetProducts";
            try
            {
                ConnectionManager.GetConnectionString();
                //ConnectionManager.GetWindowsAuthenticationConnectionString();
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@productId", SqlDbType.BigInt).Value = productId;
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    product.Id = dataReader.GetInt64(dataReader.GetOrdinal("Id"));
                                    product.CompanyName = dataReader.GetString(dataReader.GetOrdinal("CompanyName"));
                                    product.SellingPrice = dataReader.GetDecimal(dataReader.GetOrdinal("SellingPrice"));
                                    product.Photo = (byte[])(dataReader["Photo"]);
                                    product.Status = dataReader.GetString(dataReader.GetOrdinal("Status"));
                                    product.BuyingPrice = dataReader.GetDecimal(dataReader.GetOrdinal("BuyingPrice"));
                                    product.CategoryId = dataReader.GetInt32(dataReader.GetOrdinal("CategoryId"));
                                    product.CompanyId = dataReader.GetInt64(dataReader.GetOrdinal("CompanyId"));
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
            return product;
        }


        public long InsertCustomerDesign(Product product)
        {
            string sql = "InsertCustomerDesign";
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        command.Parameters.Add("@photo", SqlDbType.Binary).Value = product.Photo;
                        command.Parameters.Add("@categoryId", SqlDbType.Int).Value = product.CategoryId;
                        command.Parameters.Add("@companyId", SqlDbType.BigInt).Value = product.CompanyId;
                        command.Parameters.Add("@buyingPrice", SqlDbType.Decimal).Value = product.BuyingPrice;
                        command.Parameters.Add("@sellingPrice", SqlDbType.Decimal).Value = product.SellingPrice;
                        SqlParameter newProductId = command.CreateParameter();
                        newProductId.ParameterName = "@productId";
                        newProductId.Direction = System.Data.ParameterDirection.Output;
                        newProductId.DbType = System.Data.DbType.Int64;
                        command.Parameters.Add(newProductId);
                        command.ExecuteNonQuery();
                        return (long)newProductId.Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    if (ex.Errors[i].Number == 547)
                        throw new Exception("Either company and/or category with this id doesn't exist");
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
    }
}
