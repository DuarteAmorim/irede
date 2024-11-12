using irede.shared.Extensions;
using irede.shared.Notifications;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace irede.infra.Database
{
    /// <summary>
    /// factory pattern
    /// </summary>
    public class DapperContext : Notifiable, IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private bool _disposed = false;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MySqlConnection");

        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
            }


            _disposed = true;
        }
    }
}



