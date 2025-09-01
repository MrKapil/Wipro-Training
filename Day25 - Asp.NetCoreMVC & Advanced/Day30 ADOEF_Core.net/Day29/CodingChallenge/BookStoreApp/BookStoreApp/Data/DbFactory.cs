using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BookStoreApp.Data
{
    public class DbFactory
    {
        private readonly string _connectionString;

        public DbFactory(IConfiguration configuration)
        {
            // Accept either key name to avoid mismatches
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? configuration.GetConnectionString("BookStoreDb")
                ?? throw new InvalidOperationException(
                    "No connection string named 'DefaultConnection' or 'BookStoreDb' found in appsettings.json -> ConnectionStrings.");
        }

        public SqlConnection CreateConnection()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new InvalidOperationException("Connection string is empty.");
            return new SqlConnection(_connectionString);
        }
    }
}
