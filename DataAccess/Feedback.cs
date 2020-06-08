using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DataAccess.Models;
using System.Data;

namespace DataAccess
{
    public class Feedback : IFeedback
    {
        public List<FeedbackModel> GetFeedback()
        {
            List<FeedbackModel> feedback = new List<FeedbackModel>();
            string sql = "GetFeedback";
            try
            {
                ConnectionManager.GetConnectionString();
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
                                int colName = dataReader.GetOrdinal("Name");
                                int colText = dataReader.GetOrdinal("Text");
                                while (dataReader.Read())
                                {
                                    feedback.Add(new FeedbackModel
                                    {
                                        Name = dataReader.GetString(colName),
                                        Feedback = dataReader.GetString(colText)
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
            return feedback;
        }
        public long InsertFeedback(FeedbackModel feedback)
        {
            string sql = "InsertFeedback";
            try
            {
                //ConnectionManager.GetWindowsAuthenticationConnectionString();
                ConnectionManager.GetConnectionString();
                using (SqlConnection connection = ConnectionManager.CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();

                        command.Parameters.Add("@customerId", SqlDbType.BigInt).Value = feedback.CustomerId;
                        command.Parameters.Add("@text", SqlDbType.VarChar).Value = feedback.Feedback;
                        SqlParameter newFeedbackId = command.CreateParameter();
                        newFeedbackId.ParameterName = "@id";
                        newFeedbackId.Direction = System.Data.ParameterDirection.Output;
                        newFeedbackId.DbType = System.Data.DbType.Int64;
                        command.Parameters.Add(newFeedbackId);

                        command.ExecuteNonQuery();
                        return (long)newFeedbackId.Value;

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
