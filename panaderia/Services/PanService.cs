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
        string EditarPan(int id, Pan model);
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

                        cmd.CommandText = "SELECT PanId, Nombre, PrecioUnitario, Descripcion, TiempoPreparacion, IsActive FROM Panes WHERE IsActive = '1'";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            panes.Add(new Pan()
                            {
                                PanId = Convert.ToInt32(reader["PanId"]),
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

                        cmd.CommandText = "pkgMain.crud_pan";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_PanId", OracleDbType.Int32).Value = null;
                        cmd.Parameters.Add("p_Nombre", OracleDbType.Varchar2).Value = model.Nombre;
                        cmd.Parameters.Add("p_PrecioUnitario", OracleDbType.Varchar2).Value = model.PrecioUnitario;
                        cmd.Parameters.Add("p_Descripcion", OracleDbType.Varchar2).Value = model.Descripcion;
                        cmd.Parameters.Add("p_TiempoPreparacion", OracleDbType.Varchar2).Value = model.TiempoPreparacion;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = null;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El pan fue creado con éxito.";
                        } else if (salida.Equals("2"))
                        {
                            return "El pan fue editado con éxito.";
                        } else if(salida.Equals("3"))
                        {
                            return "El pan fue eliminado con éxito.";
                        }
                        else
                        {
                            return "Error";
                        }
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

        #region Editarpan
        //Método para editar un pan
        public string EditarPan(int id, Pan model)
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

                        cmd.CommandText = "pkgMain.crud_pan";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_PanId", OracleDbType.Int32).Value = model.PanId;
                        cmd.Parameters.Add("p_Nombre", OracleDbType.Varchar2).Value = model.Nombre;
                        cmd.Parameters.Add("p_PrecioUnitario", OracleDbType.Varchar2).Value = model.PrecioUnitario;
                        cmd.Parameters.Add("p_Descripcion", OracleDbType.Varchar2).Value = model.Descripcion;
                        cmd.Parameters.Add("p_TiempoPreparacion", OracleDbType.Varchar2).Value = model.TiempoPreparacion;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = model.IsActive;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El pan fue creado con éxito.";
                        }
                        else if (salida.Equals("2"))
                        {
                            return "El pan fue editado con éxito.";
                        }
                        else if (salida.Equals("3"))
                        {
                            return "El pan fue eliminado con éxito.";
                        }
                        else
                        {
                            return "Error";
                        }
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
