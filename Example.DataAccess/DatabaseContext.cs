
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

using Example.DataAccess.Infrastructure;

namespace Example.DataAccess
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IConfiguration _configuration;

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public DatabaseContext(IConfiguration configuration)
        {
            // The connection string should be provided in the configuration.
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    // Create and open Connection if it doesn't exist yet.
                    _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    _connection.Open();
                }

                return _connection;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    // Begin Transaction if it doesn't exist yet.
                    _transaction = Connection.BeginTransaction();
                }

                return _transaction;
            }
        }

        // Make sure that _transaction and _connection are disposed.
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
