using System;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ConnectionManager
    {
        private static string ConnectionString { get; set; }

        public static void InitializeConnectionString(string userName, string password)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            /*builder.DataSource = "LAPTOP-74OJ765K\\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "YourDesignDB";*/
            builder.DataSource = "SQL5047.site4now.net";
            builder.InitialCatalog = "DB_A57D7B_yourDesignDB";
            builder.UserID = userName;//DB_A57D7B_yourDesignDB_admin
            builder.Password = password;//rememberIt3
            ConnectionString = builder.ConnectionString;
        }
        public static void CheckConnection()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    throw new Exception("Incorrect username or password. Please try again");
                }
            }
        }

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public static void GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "SQL5047.site4now.net";
            builder.InitialCatalog = "DB_A57D7B_yourDesignDB";
            builder.UserID = "DB_A57D7B_yourDesignDB_admin";
            builder.Password = "rememberIt3";
            ConnectionString = builder.ConnectionString;
        }
        public static void GetWindowsAuthenticationConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "LAPTOP-74OJ765K\\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "YourDesignDB";
            ConnectionString = builder.ConnectionString;
        }

    }
}
