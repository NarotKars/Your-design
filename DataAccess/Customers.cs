using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class Customers : ICustomers
    {
        public PersonalInformation GetCustomersPersonalInfromation(long userId)
        {
            PersonalInformation personalInformation = new PersonalInformation();
            string sql = "GetCusomersPersonalInformation";
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

                        command.Parameters.Add("@userId", System.Data.SqlDbType.BigInt).Value = userId;
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {

                                while (dataReader.Read())
                                {
                                    personalInformation.UserId = dataReader.GetInt64(dataReader.GetOrdinal("Id"));
                                    personalInformation.Email = dataReader.GetString(dataReader.GetOrdinal("Email"));
                                    personalInformation.Name = dataReader.GetString(dataReader.GetOrdinal("Name"));
                                    personalInformation.PhoneNumber = dataReader.GetString(dataReader.GetOrdinal("PhoneNum"));
                                    Address address = new Address();
                                    address.City = dataReader.GetString(dataReader.GetOrdinal("City"));
                                    address.Street = dataReader.GetString(dataReader.GetOrdinal("Street"));
                                    address.Number = dataReader.GetString(dataReader.GetOrdinal("Number"));
                                    personalInformation.Address = address;
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
            return personalInformation;
        }


        public long InsertCustomer(PersonalInformation personalInformation)
        {
            string sql = "InsertCustomer";
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

                        command.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = personalInformation.Email;
                        command.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = personalInformation.Password;
                        command.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = personalInformation.Name;
                        command.Parameters.Add("@city", System.Data.SqlDbType.VarChar).Value = personalInformation.Address.City;
                        command.Parameters.Add("@street", System.Data.SqlDbType.VarChar).Value = personalInformation.Address.Street;
                        command.Parameters.Add("@number", System.Data.SqlDbType.VarChar).Value = personalInformation.Address.Number;
                        command.Parameters.Add("@phoneNum", System.Data.SqlDbType.VarChar).Value = personalInformation.PhoneNumber;

                        SqlParameter newCustomerId = command.CreateParameter();
                        newCustomerId.ParameterName = "@id";
                        newCustomerId.Direction = System.Data.ParameterDirection.Output;
                        newCustomerId.DbType = System.Data.DbType.Int64;
                        command.Parameters.Add(newCustomerId);

                        command.ExecuteNonQuery();
                        return (long)newCustomerId.Value;
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

        public void UpdateCustomersPersonalInformation(PersonalInformation personalInformation)
        {
            string sql = "UpdateCustomersPersonalInformation";
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

                        command.Parameters.Add("@id", System.Data.SqlDbType.BigInt).Value = personalInformation.UserId;
                        command.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = personalInformation.Name;
                        command.Parameters.Add("@phoneNum", System.Data.SqlDbType.VarChar).Value = personalInformation.PhoneNumber;
                        command.Parameters.Add("@city", System.Data.SqlDbType.VarChar).Value = personalInformation.Address.City;
                        command.Parameters.Add("@street", System.Data.SqlDbType.VarChar).Value = personalInformation.Address.Street;
                        command.Parameters.Add("@number", System.Data.SqlDbType.VarChar).Value = personalInformation.Address.Number;

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
