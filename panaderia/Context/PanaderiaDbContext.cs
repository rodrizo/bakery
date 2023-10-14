using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Panaderia.Context
{
    public class PanaderiaDbContext : IPanaderiaDbContext
    {
        private IConfiguration _config;
        private OracleConnection _connection;
        private OracleCommand _cmd;
        private string _connectionString;

        public PanaderiaDbContext(IConfiguration configuration)
        {
            _config = configuration;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public OracleConnection GetConn()
        {
            _connection = new OracleConnection(_connectionString);
            return _connection;
        }

        public OracleCommand GetCommand()
        {
            _cmd = _connection.CreateCommand();
            return _cmd;
        }
    }
}
