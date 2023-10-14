using Oracle.ManagedDataAccess.Client;

namespace Panaderia.Context
{
    public interface IPanaderiaDbContext
    {
        OracleCommand GetCommand();
        OracleConnection GetConn();
    }
}
