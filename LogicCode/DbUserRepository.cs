using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagerWpf.LogicCode
{
    public class DbUserRepository
    {
        private string? _connectionString;
        public DbUserRepository()
        {
            _connectionString = "Data Source=userSql.db";
        }

        public List<User> GetUsers()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var cmd = new SqliteCommand("SELECT * FROM UserDetails", connection);
            using var reader = cmd.ExecuteReader();

            var users = new List<User>();
            while (reader.Read())
            {
                users.Add(new User(reader["UserId"].ToString(),
                    reader["UserName"].ToString(),
                    reader["Name"].ToString(),
                    reader["HashPassword"].ToString(),
                    new BankAccount(reader["UserId"].ToString() ?? "Not Found", Convert.ToDecimal(reader["Balance"]))));
            }
            return users;
        }
        public bool ValidatePassword(string userId, string password)
        {
            string query = "SELECT PaswordHash FROM UserDetails WHERE UserId = @userId";

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using (SqliteCommand cmd = new(query, connection))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                var storedHash = cmd.ExecuteScalar()?.ToString();

                if (string.IsNullOrEmpty(storedHash))
                    return false;

                return PasswordHasher.VerifyPassword(password, storedHash);

            }
        }
    }
}
