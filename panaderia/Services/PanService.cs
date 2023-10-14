using Oracle.ManagedDataAccess.Client;
using Panaderia.Context;
using PanaderiaModels.Entities;
using System.Data;

namespace Panaderia.Services
{
    public interface IPanService
    {
        List<Pan> ObtenerPanes();
        string CrearPan(Pan model);
    }

    public class PanService : IPanService
    {
        private IPanaderiaDbContext _dbContext;

        public PanService(IPanaderiaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region ObtenerPanes
        //Método para obtener listado de panes
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
        #endregion

        #region AgregarPan
        //Método para agregar un pan
        public string CrearPan(Pan model)
        {
            string salida;

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "pkgMain.agregar_pan";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("Nombre", OracleDbType.Varchar2).Value = model.Nombre;
                        cmd.Parameters.Add("PrecioUnitario", OracleDbType.Varchar2).Value = model.PrecioUnitario;
                        cmd.Parameters.Add("Descripcion", OracleDbType.Varchar2).Value = model.Descripcion;
                        cmd.Parameters.Add("TiempoPreparacion", OracleDbType.Varchar2).Value = model.TiempoPreparacion;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        return (salida.Equals("1")) ? "El pan fue creado con éxito." : "No se pudo crear el pan.";
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
        #endregion
    }
}
