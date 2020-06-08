using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess
{
    public class Companies : ICompanies
    {
        public List<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            string sql = "GetCompanies";
            try
            {
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
                                int colName = dataReader.GetOrdinal("Name");
                                while (dataReader.Read())
                                {
                                    companies.Add(new Company
                                    {
                                        Name = dataReader.GetString(colName),
                                        Id = dataReader.GetInt64(colId)
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
            return companies;
        }

        public PersonalInformation GetCompaniesPersonalInfromation(long companyId)
        {
            PersonalInformation personalInformation = new PersonalInformation();
            string sql = "GetCompaniesPersonalInformation";
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
                                while (dataReader.Read())
                                {
                                    personalInformation.UserId = dataReader.GetInt64(dataReader.GetOrdinal("Id"));
                                    personalInformation.Email = dataReader.GetString(dataReader.GetOrdinal("Email"));
                                    personalInformation.Name = dataReader.GetString(dataReader.GetOrdinal("Name"));
                                    personalInformation.PhoneNumber = dataReader.GetString(dataReader.GetOrdinal("PhoneNum"));
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

        public long InsertCompany(PersonalInformation personalInformation)
        {
            string sql = "InsertCompany";
            try
            {
                ConnectionManager.GetConnectionString();
                //ConnectionManager.GetWindowsAuthenticationConnectionString();
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@email", SqlDbType.VarChar).Value = personalInformation.Email;
                        command.Parameters.Add("@phoneNum", SqlDbType.VarChar).Value = personalInformation.PhoneNumber;
                        command.Parameters.Add("@name", SqlDbType.VarChar).Value = personalInformation.Name;
                        command.Parameters.Add("@password", SqlDbType.VarChar).Value = personalInformation.Password;
                        SqlParameter newCompanyId = command.CreateParameter();
                        newCompanyId.ParameterName = "@id";
                        newCompanyId.Direction = System.Data.ParameterDirection.Output;
                        newCompanyId.DbType = System.Data.DbType.Int64;
                        command.Parameters.Add(newCompanyId);
                        command.ExecuteNonQuery();

                        return (long)newCompanyId.Value;
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


        public void UpdateCompanysPersonalInformation(PersonalInformation personalInformation)
        {
            string sql = "UpdateCompanysPersonalInformation";
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

        public void DeleteCompany(long id)
        {
            string sql = "DeleteCompany";
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