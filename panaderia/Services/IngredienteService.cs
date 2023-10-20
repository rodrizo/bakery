using Oracle.ManagedDataAccess.Client;
using Panaderia.Context;
using PanaderiaModels.Entities;
using System.Data;

namespace Panaderia.Services
{
    public interface IServiceIngrediente
    {
        List<Ingrediente> ObtenerIngredientes();
        string CrearIngrediente(Ingrediente model);
        string EditarIngrediente(int id, Ingrediente model);
    }

    public class IngredienteService : IServiceIngrediente
    {
        private readonly IPanaderiaDbContext _dbContext;

        public IngredienteService(IPanaderiaDbContext dbContext) => this._dbContext = dbContext;

        #region Obtener Ingredientes
        //Método para obtener listado de ingredientes
        public List<Ingrediente> ObtenerIngredientes()
        {
            List<Ingrediente> ingredientes = new List<Ingrediente>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT IngredienteId, Nombre, Proveedor, CostoUnitario, FechaCompra, IsActive FROM Ingredientes WHERE IsActive = '1'";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ingredientes.Add(new Ingrediente()
                            {
                                IngredienteId = Convert.ToInt32(reader["IngredienteId"]),
                                Nombre = reader["Nombre"].ToString(),
                               Proveedor = reader["Proveedor"].ToString(),
                                CostoUnitario = Convert.ToDouble(reader["CostoUnitario"].ToString()),
                                FechaCompra = Convert.ToDateTime(reader["FechaCompra"].ToString()),
                                IsActive = reader["IsActive"].ToString()
                            });
                        }
                        reader.Dispose();
                        return ingredientes;
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

        #region Agregar Ingrediente
        //Método para agregar un Ingrediente
        public string CrearIngrediente(Ingrediente model)
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

                        cmd.CommandText = "pkgMain.crud_ingrediente";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_IngredienteId", OracleDbType.Int32).Value = null;
                        cmd.Parameters.Add("p_Nombre", OracleDbType.Varchar2).Value = model.Nombre;
                        cmd.Parameters.Add("p_Proveedor", OracleDbType.Varchar2).Value = model.Proveedor;
                        cmd.Parameters.Add("p_CostoUnitario", OracleDbType.Double).Value = model.CostoUnitario;
                        cmd.Parameters.Add("p_FechaCompra", OracleDbType.Date).Value = model.FechaCompra;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = null;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El ingrediente fue creado con éxito.";
                        } else if (salida.Equals("2"))
                        {
                            return "El ingrediente fue editado con éxito.";
                        } else if(salida.Equals("3"))
                        {
                            return "El ingrediente fue eliminado con éxito.";
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

        #region Editar Ingrediente
        //Método para editar un ingrediente
        public string EditarIngrediente(int id, Ingrediente model)
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

                        cmd.CommandText = "pkgMain.crud_ingrediente";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_IngredienteId", OracleDbType.Int32).Value = model.IngredienteId;
                        cmd.Parameters.Add("p_Nombre", OracleDbType.Varchar2).Value = model.Nombre;
                        cmd.Parameters.Add("p_Proveedor", OracleDbType.Varchar2).Value = model.Proveedor;
                        cmd.Parameters.Add("p_CostoUnitario", OracleDbType.Double).Value = model.CostoUnitario;
                        cmd.Parameters.Add("p_FechaCompra", OracleDbType.Date).Value = model.FechaCompra;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = model.IsActive;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El ingrediente fue creado con éxito.";
                        }
                        else if (salida.Equals("2"))
                        {
                            return "El ingrediente fue editado con éxito.";
                        }
                        else if (salida.Equals("3"))
                        {
                            return "El ingrediente fue eliminado con éxito.";
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
