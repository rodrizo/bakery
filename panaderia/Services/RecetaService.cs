using Oracle.ManagedDataAccess.Client;
using Panaderia.Context;
using PanaderiaModels.Entities;
using System.Data;

namespace Panaderia.Services
{
    public interface IRecetaService
    {
        List<object> ObtenerRecetas();
        List<object> ObtenerItemsRecetas(int id);
        string CrearItemReceta(int id, ItemReceta model);
        string EditarItemReceta(int id, ItemReceta model);
    }

    public class RecetaService : IRecetaService
    {
        private readonly IPanaderiaDbContext _dbContext;

        public RecetaService(IPanaderiaDbContext dbContext) => this._dbContext = dbContext;

        #region Obtener Receta
        //Método para obtener listado de sucursales
        public List<object> ObtenerRecetas()
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

                        cmd.CommandText = "SELECT r.RecetaId, p.Nombre AS Pan, r.Descripcion, r.IsActive FROM Recetas r INNER JOIN Panes p ON p.PanId = r.Panid " +
                            "AND p.IsActive = 1" +
                            "WHERE r.IsActive = 1";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var receta = new
                            {
                                RecetaId = Convert.ToInt32(reader["RecetaId"]),
                                Pan = reader["Pan"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                IsActive = reader["IsActive"].ToString(),
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

        #region Obtener Receta y sus items
        //Método para obtener listado de sucursales
        public List<object> ObtenerItemsRecetas(int id)
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

                        cmd.CommandText = "SELECT r.RecetaId, ri.Id AS ItemId, p.Nombre, ig.Nombre AS Ingrediente, ri.Descripcion, ri.Cantidad, ri.IsActive " +
                            "FROM Recetas r " +
                            "INNER JOIN Panes p ON p.PanId = r.PanId " +
                            "AND p.IsActive = 1 " +
                            "INNER JOIN RecetaIngrediente ri ON ri.RecetaId = r.RecetaId " +
                            "AND ri.IsActive = 1 " +
                            "INNER JOIN Ingredientes ig ON ig.IngredienteId = ri.IngredienteId " +
                            "AND ig.IsActive = 1 " +
                            "WHERE r.IsActive = 1" +
                            $"AND r.RecetaId = NVL({id}, r.RecetaId)" +
                            $"ORDER BY ri.Id ASC";

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

        #region Agregar Item de una Receta
        //Método para agregar un Item de una Receta
        public string CrearItemReceta(int id, ItemReceta model)
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

                        cmd.CommandText = "pkgMainFlows.crud_item_receta";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_Id", OracleDbType.Int32).Value = null;
                        cmd.Parameters.Add("p_RecetaId", OracleDbType.Int32).Value = model.RecetaId;
                        cmd.Parameters.Add("p_IngredienteId", OracleDbType.Int32).Value = model.IngredienteId;
                        cmd.Parameters.Add("p_Descripcion", OracleDbType.Varchar2).Value = model.Descripcion;
                        cmd.Parameters.Add("p_Cantidad", OracleDbType.Varchar2).Value = model.Cantidad;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = null;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El item fue creado con éxito.";
                        } else if (salida.Equals("2"))
                        {
                            return "El item fue editado con éxito.";
                        } else if(salida.Equals("3"))
                        {
                            return "El item fue eliminado con éxito.";
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

        #region Editar Item de una Receta
        //Método para editar un item de una receta
        public string EditarItemReceta(int id, ItemReceta model)
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

                        cmd.CommandText = "pkgMainFlows.crud_item_receta";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_Id", OracleDbType.Int32).Value = model.Id;
                        cmd.Parameters.Add("p_RecetaId", OracleDbType.Int32).Value = model.RecetaId;
                        cmd.Parameters.Add("p_IngredienteId", OracleDbType.Int32).Value = model.IngredienteId;
                        cmd.Parameters.Add("p_Descripcion", OracleDbType.Varchar2).Value = model.Descripcion;
                        cmd.Parameters.Add("p_Cantidad", OracleDbType.Varchar2).Value = model.Cantidad;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = model.IsActive;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El item fue creado con éxito.";
                        }
                        else if (salida.Equals("2"))
                        {
                            return "El item fue editado con éxito.";
                        }
                        else if (salida.Equals("3"))
                        {
                            return "El item fue eliminado con éxito.";
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
