using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>
    {
        //private User result;

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            string sql = "InsertUser";
            ConnectionManager.GetConnectionString();
            using (SqlConnection connection = ConnectionManager.CreateConnection())
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add("@email", System.Data.SqlDbType.VarChar).Value = user.Email;
                    command.Parameters.Add("@passwordHash", System.Data.SqlDbType.VarChar).Value = user.PasswordHash;
                    command.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = user.Name;
                    command.Parameters.Add("@rank", System.Data.SqlDbType.VarChar).Value = user.Rank;
                    command.Parameters.Add("@normalizedUserName", System.Data.SqlDbType.VarChar).Value = user.NormalizedUserName;
                    SqlParameter newUserId = command.CreateParameter();
                    newUserId.ParameterName = "@id";
                    newUserId.Direction = System.Data.ParameterDirection.Output;
                    newUserId.DbType = System.Data.DbType.Int64;
                    command.Parameters.Add(newUserId);

                    command.ExecuteNonQuery();
                    user.Id=(long)newUserId.Value;
                }
            }
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            User result = new User();
            string sql = "GetUserById";
            ConnectionManager.GetConnectionString();
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
                                result.Id = dataReader.GetInt64(dataReader.GetOrdinal("Id"));
                                result.Email = dataReader.GetString(dataReader.GetOrdinal("Email"));
                                result.Name = dataReader.GetString(dataReader.GetOrdinal("Name"));
                                result.PhoneNumber = dataReader.GetString(dataReader.GetOrdinal("PhoneNum"));
                                result.NormalizedUserName = dataReader.GetString(dataReader.GetOrdinal("NormalizedUserName"));
                                Address address = new Address();
                                address.City = dataReader.GetString(dataReader.GetOrdinal("City"));
                                address.Street = dataReader.GetString(dataReader.GetOrdinal("Street"));
                                address.Number = dataReader.GetString(dataReader.GetOrdinal("Number"));
                                result.Address = address;
                                result.IsValid = dataReader.GetString(dataReader.GetOrdinal("IsValid"));
                            }
                        }
                    }
                }
            }
            return Task.FromResult(result);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            User user = new User();
            string sql = "GetUserByName";
            ConnectionManager.GetConnectionString();
            using (SqlConnection connection = ConnectionManager.CreateConnection())
            using(SqlCommand command = new SqlCommand(sql,connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                
                command.Parameters.Add("@normalizedUserName", System.Data.SqlDbType.VarChar).Value = normalizedUserName;
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {
                            user.Id = dataReader.GetInt64(dataReader.GetOrdinal("Id"));
                            user.Email = dataReader.GetString(dataReader.GetOrdinal("Email"));
                            user.Name = dataReader.GetString(dataReader.GetOrdinal("Name"));
                            user.PhoneNumber = dataReader.GetString(dataReader.GetOrdinal("PhoneNumber"));
                            user.PasswordHash = dataReader.GetString(dataReader.GetOrdinal("PasswordHash"));
                            user.Rank = dataReader.GetString(dataReader.GetOrdinal("Rank"));
                            Address address = new Address();
                            address.City = dataReader.GetString(dataReader.GetOrdinal("City"));
                            address.Street = dataReader.GetString(dataReader.GetOrdinal("Street"));
                            address.Number = dataReader.GetString(dataReader.GetOrdinal("Number"));
                            user.Address = address;
                            user.IsValid = dataReader.GetString(dataReader.GetOrdinal("IsValid"));
                        }
                    }
                }
            }
            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Name);
        }

        public Task<string> GetUserRankAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Rank);
        }
        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != "");
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Name = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
