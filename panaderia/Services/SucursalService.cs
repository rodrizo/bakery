using Oracle.ManagedDataAccess.Client;
using Panaderia.Context;
using PanaderiaModels.Entities;
using System.Data;

namespace Panaderia.Services
{
    public interface ISucursalService
    {
        List<Sucursal> ObtenerSucursales();
        string CrearSucursal(Sucursal model);
        string EditarSucursal(int id, Sucursal model);
    }

    public class SucursalService : ISucursalService
    {
        private readonly IPanaderiaDbContext _dbContext;

        public SucursalService(IPanaderiaDbContext dbContext) => this._dbContext = dbContext;

        #region Obtener Sucursales
        //Método para obtener listado de sucursales
        public List<Sucursal> ObtenerSucursales()
        {
            List<Sucursal> sucursales = new List<Sucursal>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT SucursalId, Nombre, Direccion, NumeroTelefono, GerenteSucursal, HorarioOperacion, IsActive FROM Sucursales WHERE IsActive = 1";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            sucursales.Add(new Sucursal()
                            {
                                SucursalId = Convert.ToInt32(reader["SucursalId"]),
                                Nombre = reader["Nombre"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                NumeroTelefono = reader["NumeroTelefono"].ToString(),
                                GerenteSucursal = reader["GerenteSucursal"].ToString(),
                                HorarioOperacion = reader["HorarioOperacion"].ToString(),
                                IsActive = reader["IsActive"].ToString()
                            });
                        }
                        reader.Dispose();
                        return sucursales;
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

        #region Agregar Sucursal
        //Método para agregar una sucursal
        public string CrearSucursal(Sucursal model)
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

                        cmd.CommandText = "pkgMain.crud_sucursal";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_SucursalId", OracleDbType.Varchar2).Value = null;
                        cmd.Parameters.Add("p_Nombre", OracleDbType.Varchar2).Value = model.Nombre;
                        cmd.Parameters.Add("p_Direccion", OracleDbType.Varchar2).Value = model.Direccion;
                        cmd.Parameters.Add("p_NumeroTelefono", OracleDbType.Varchar2).Value = model.NumeroTelefono;
                        cmd.Parameters.Add("p_GerenteSucursal", OracleDbType.Varchar2).Value = model.GerenteSucursal;
                        cmd.Parameters.Add("p_HorarioOperacion", OracleDbType.Varchar2).Value = model.HorarioOperacion;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = null;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "La sucursal fue creado con éxito.";
                        } else if (salida.Equals("2"))
                        {
                            return "La sucursal fue editado con éxito.";
                        } else if(salida.Equals("3"))
                        {
                            return "La sucursal fue eliminado con éxito.";
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

        #region Editar Sucursal
        //Método para editar una sucursal
        public string EditarSucursal(int id, Sucursal model)
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

                        cmd.CommandText = "pkgMain.crud_sucursal";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_SucursalId", OracleDbType.Varchar2).Value = model.SucursalId;
                        cmd.Parameters.Add("p_Nombre", OracleDbType.Varchar2).Value = model.Nombre;
                        cmd.Parameters.Add("p_Direccion", OracleDbType.Varchar2).Value = model.Direccion;
                        cmd.Parameters.Add("p_NumeroTelefono", OracleDbType.Varchar2).Value = model.NumeroTelefono;
                        cmd.Parameters.Add("p_GerenteSucursal", OracleDbType.Varchar2).Value = model.GerenteSucursal;
                        cmd.Parameters.Add("p_HorarioOperacion", OracleDbType.Varchar2).Value = model.HorarioOperacion;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = model.IsActive;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "La sucursal fue creado con éxito.";
                        }
                        else if (salida.Equals("2"))
                        {
                            return "La sucursal fue editado con éxito.";
                        }
                        else if (salida.Equals("3"))
                        {
                            return "La sucursal fue eliminado con éxito.";
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
