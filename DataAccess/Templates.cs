using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class Templates
    {
        public long InsertTemplate(Template template)
        {
            string sql = "InsertImage";
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        command.Parameters.Add("@photo", SqlDbType.Binary).Value = template.Photo;
                        command.Parameters.Add("@text", SqlDbType.VarChar).Value = template.Text;
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = template.Status;
                        SqlParameter newImageId = command.CreateParameter();
                        newImageId.ParameterName = "@id";
                        newImageId.Direction = System.Data.ParameterDirection.Output;
                        newImageId.DbType = System.Data.DbType.Int64;
                        command.Parameters.Add(newImageId);
                        command.ExecuteNonQuery();
                        return (long)newImageId.Value;
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

        public List<Template> GetImages(string status1 = null, string status2=null, int id=0)
        {
            List<Template> templates = new List<Template>();
            string sql = "GetImages";
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (status1 == null)
                            command.Parameters.Add("@categoryId", SqlDbType.Int).Value = id;
                        else
                        {
                            command.Parameters.Add("@status1", SqlDbType.VarChar).Value = status1;
                            command.Parameters.Add("@status2", SqlDbType.VarChar).Value = status2;
                        }
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int colText = dataReader.GetOrdinal("Text");
                                int colStatus = dataReader.GetOrdinal("Status");
                                int colId = dataReader.GetOrdinal("Id");
                                while (dataReader.Read())
                                {
                                    templates.Add(new Template
                                    {
                                        Photo = (byte[])(dataReader["Photo"]),
                                        Text = dataReader.GetString(colText),
                                        Status = dataReader.GetString(colStatus),
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
            return templates;
        }

        public int UpdateImage(Template template)
        {
            string sql = "UpdateImage";
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.Add("@id", SqlDbType.Int).Value = template.Id;
                        command.Parameters.Add("@photo", SqlDbType.Binary).Value = template.Photo;
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = template.Status;
                        command.Parameters.Add("@retValue", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;
                        command.ExecuteNonQuery();
                        return (int)command.Parameters["@retValue"].Value;
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


        public void UpdateStatus(Template template)
        {
            string sql = "UpdateStatus";
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        command.Parameters.Add("@id", SqlDbType.BigInt).Value = template.Id;
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = template.Status;
                        command.ExecuteNonQuery();
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

        public List<Template> GetImagesByCompany(long companyId)
        {
            List<Template> templates = new List<Template>();
            string sql = "GetImagesByCompany";
            try
            {
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@companyId", SqlDbType.BigInt).Value = companyId;
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                int colText = dataReader.GetOrdinal("Text");
                                int colStatus = dataReader.GetOrdinal("Status");
                                int colId = dataReader.GetOrdinal("Id");
                                while (dataReader.Read())
                                {
                                    templates.Add(new Template
                                    {
                                        Photo = (byte[])(dataReader["Photo"]),
                                        Text = dataReader.GetString(colText),
                                        Status = dataReader.GetString(colStatus),
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
            return templates;
        }
    }
}
