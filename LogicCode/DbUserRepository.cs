using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagerWpf.LogicCode
{
    public class DbUserRepository : IUserRepository
    {
        private string? _connectionString;
        private List<User> _users = [];
        public DbUserRepository(string path)
        {
            _connectionString = path;
            _users = GetUsers();
        }

        //public List<User> Users { get { return _users; } }

        public User? FindUserById(string id)
        {
            return _users.FirstOrDefault(u =>
            u.UserId == id);
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
                    reader["PasswordHash"].ToString(),
                    new BankAccount(reader["UserId"].ToString() ?? "Not Found", Convert.ToDecimal(reader["Balance"]))));
            }
            return users;
        }
        public bool ValidatePassword(string userId, string password)
        {
            string query = "SELECT PasswordHash FROM UserDetails WHERE UserId = @userId";

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using (SqliteCommand cmd = new(query, connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                var storedHash = cmd.ExecuteScalar()?.ToString();

                if (string.IsNullOrEmpty(storedHash))
                    return false;

                return PasswordHasher.VerifyPassword(password, storedHash);

            }
        }

        public User? ValidateUser(string userName, string password)
        {
            User? user = FindUserByUsername(userName);

            if (ValidatePassword(user.UserId, password))
                return user;
            else
                return null;
        }
        public User? FindUserByUsername(string userName)
        {
            return _users.FirstOrDefault(a => a.UserName == userName);
        }
    }
}
