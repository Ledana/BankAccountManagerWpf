using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagerWpf.LogicCode
{
    public class DatabaseSeeder
    {
        public static void SeedUsers(SqliteConnection connection)
        {
//            string createTableQuery = @"
//CREATE TABLE IF NOT EXISTS UserDetails (
//    Name NVARCHAR(25) NOT NULL,
//    UserName NVARCHAR(25) NOT NULL,
//    UserId INTEGER,
//    PasswordHash TEXT NOT NULL,
//    Balance DECIMAL
//);";

//            using (var cmd = new SqliteCommand(createTableQuery, connection))
//            {
//                cmd.ExecuteNonQuery();
//            }
            string insertQuery = "INSERT INTO UserDetails (Name, UserName, UserId, PasswordHash, Balance) VALUES(@name, @userName, @userId, @passwordHash, @balance)";

            var testUsers = new List<(string Name, string UserName, string UserId, string Password, decimal Balance)>
            {
                ("Bob Dylan", "Dylan6790", "123456790", "Bob123", 0m),
                ("Amelia Aster", "Aster6791", "123456791", "Amelia123", 150m),
                ("Vivian Scott", "Scott6792", "123456792", "Vivian123", 80m),
                ("Luiza Griffin", "Griffin6793", "123456793", "Luiza123", 0m),
                ("Franceska DeMarti", "DeMarti6794", "123456794", "Francesca123", 0m),
                ("Lory Marti", "Marti6795", "123456795", "Lory123", 700m),
                ("Laila Martini", "Martini6796", "123456796", "Laila123", 250m),
                ("Violet Vi", "Vi6797", "123456797", "Violet123", 0m)
            };

            foreach (var (Name, UserName, UserId, Password, Balance) in testUsers)
            {
                using (var cmd = new SqliteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@name", Name);
                    cmd.Parameters.AddWithValue("@userName", UserName);
                    cmd.Parameters.AddWithValue("@userId", UserId);
                    cmd.Parameters.AddWithValue("@passwordHash", PasswordHasher.HashPassword(Password));
                    cmd.Parameters.AddWithValue("@balance", Balance);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
