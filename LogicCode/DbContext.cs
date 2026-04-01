using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagerWpf.LogicCode
{
    public class DbContext : IDisposable
    {
        private readonly SqliteConnection _connection;

        public DbContext(string connectionString)
        {
            _connection = new(connectionString);
            _connection.Open();
        }

        public SqliteConnection Connection => _connection;
        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
