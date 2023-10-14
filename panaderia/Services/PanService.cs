using Oracle.ManagedDataAccess.Client;
using Panaderia.Context;
using PanaderiaModels.Entities;

namespace Panaderia.Services
{
    public interface IPanService
    {
        List<Pan> ObtenerPanes();
    }

    public class PanService : IPanService
    {
        private IPanaderiaDbContext _dbContext;

        public PanService(IPanaderiaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Pan> ObtenerPanes()
        {
            List<Pan> panes = new List<Pan>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT PanId, Nombre, PrecioUnitario, Descripcion, TiempoPreparacion, IsActive FROM Panes";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            panes.Add(new Pan()
                            {
                                Id = Convert.ToInt32(reader["PanId"]),
                                Nombre = reader["Nombre"].ToString(),
                                PrecioUnitario = reader["PrecioUnitario"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                TiempoPreparacion = reader["TiempoPreparacion"].ToString(),
                                IsActive = reader["IsActive"].ToString()
                            });
                        }
                        reader.Dispose();
                        return panes;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
