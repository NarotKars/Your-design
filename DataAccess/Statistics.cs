using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DataAccess.Models;

namespace DataAccess
{
    public class Statistics
    {
        public Dictionary<string, int> GetCountOfOrdersOfEveryCompany()
        {
            Dictionary<string, int> countOfOrders = new Dictionary<string, int>();
            string sql = "GetCountOfOrdersOfEveryCompany";
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
                                int colName = dataReader.GetOrdinal("Name");
                                int colCount = dataReader.GetOrdinal("countOfOrders");
                                while (dataReader.Read())
                                {
                                    countOfOrders.Add(dataReader.GetString(colName), dataReader.GetInt32(colCount));
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
            return countOfOrders;
        }

        public decimal GetProfit()
        {
            string sql = "GetProfitOfEveryOrder";
            decimal profit = 0;
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
                                int colOrderProfit = dataReader.GetOrdinal("OrderProfit");
                                while (dataReader.Read())
                                {
                                    profit += dataReader.GetDecimal(colOrderProfit);
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
            return profit;
        }
    }
}
