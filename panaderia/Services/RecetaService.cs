using Oracle.ManagedDataAccess.Client;
using Panaderia.Context;
using PanaderiaModels.Entities;
using System.Data;

namespace Panaderia.Services
{
    public interface IRecetaService
    {
        List<object> ObtenerItemsRecetas();
        /*
        string CrearItemReceta(ItemReceta model);
        string EditarItemReceta(int id, ItemReceta model);*/
    }

    public class RecetaService : IRecetaService
    {
        private IPanaderiaDbContext _dbContext;

        public RecetaService(IPanaderiaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Obtener Recetas y sus items
        //Método para obtener listado de sucursales
        public List<object> ObtenerItemsRecetas()
        {
            List<object> recetas = new List<object>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT r.RecetaId, ri.Id AS ItemId, p.Nombre, ig.Nombre AS Ingrediente, ri.Descripcion, ri.Cantidad, ri.IsActive FROM Recetas r INNER JOIN Panes p ON p.PanId = r.PanId AND p.IsActive = 1 INNER JOIN RecetaIngrediente ri ON ri.RecetaId = r.RecetaId AND ri.IsActive = 1 INNER JOIN Ingredientes ig ON ig.IngredienteId = ri.IngredienteId AND ig.IsActive = 1 WHERE r.IsActive = 1";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var receta = new
                            {
                                RecetaId = Convert.ToInt32(reader["RecetaId"]),
                                ItemId = Convert.ToInt32(reader["ItemId"]),
                                Nombre = reader["Nombre"].ToString(),
                                Ingrediente = reader["Ingrediente"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Cantidad = reader["Cantidad"].ToString(),
                                IsActive = reader["IsActive"].ToString()
                            };
                            recetas.Add(receta);
                        }
                        reader.Dispose();
                        return recetas;
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
        /*
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
        */
    }
}
