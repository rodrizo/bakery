using Oracle.ManagedDataAccess.Client;
using Panaderia.Context;
using PanaderiaModels.Entities;
using System.Data;

namespace Panaderia.Services
{
    public interface IStockService
    {
        List<object> ObtenerStock();
        string EditarStock(int id, Stock model);
        //string CrearPan(Stock model);
    }

    public class StockService : IStockService
    {
        private readonly IPanaderiaDbContext _dbContext;

        public StockService(IPanaderiaDbContext dbContext) => this._dbContext = dbContext;

        #region Obtener stock
        //Método para obtener stock
        public List<object> ObtenerStock()
        {
            List<object> stocks = new List<object>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT st.StockId AS Id, p.Nombre AS Pan, st.Cantidad, st.Notas, st.FechaCreacion, st.FechaModificacion " +
                        "FROM Stock st " +
                        "INNER JOIN Panes p ON p.PanId = st.PanId " +
                        "AND p.IsActive = 1 " +
                        "WHERE st.IsActive = 1 ";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var item = new
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Pan = reader["Pan"].ToString(),
                                Cantidad = Convert.ToDouble(reader["Cantidad"]),
                                Notas = reader["Notas"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"])
                            };
                            stocks.Add(item);
                        }
                        reader.Dispose();
                        return stocks;
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

        #region Editar Stock
        //Método para editar un stock
        public string EditarStock(int id, Stock model)
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

                        cmd.CommandText = "pkgMainFlows.crud_stock";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_StockId", OracleDbType.Int32).Value = id;
                        cmd.Parameters.Add("p_PanId", OracleDbType.Int32).Value = null;
                        cmd.Parameters.Add("p_Cantidad", OracleDbType.Varchar2).Value = model.Cantidad;
                        cmd.Parameters.Add("p_Notas", OracleDbType.Varchar2).Value = model.Notas;
                        cmd.Parameters.Add("p_FechaCreacion", OracleDbType.Date).Value = model.FechaCreacion;
                        cmd.Parameters.Add("p_FechaModificacion", OracleDbType.Date).Value = DateTime.Now;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = model.IsActive;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El stock fue creado con éxito.";
                        }
                        else if (salida.Equals("2"))
                        {
                            return "El stock fue editado con éxito.";
                        }
                        else if (salida.Equals("3"))
                        {
                            return "El stock fue eliminado con éxito.";
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
