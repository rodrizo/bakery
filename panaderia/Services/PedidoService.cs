using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using Panaderia.Context;
using PanaderiaModels.Entities;
using System.Data;

namespace Panaderia.Services
{
    public interface IPedidoService
    {
        List<object> ObtenerPedidos(int sucursalId);
        Pedido ObtenerPedidoById(int pedidoId);
        List<object> ObtenerItemsPedido(int pedidoId);
        string CrearPedido(int sucursalId, Pedido model);
        string CrearPedidoItem(int pedidoId, PedidoPan model);
        string EditarPedido(int id, Pedido model);
        string EditarPedidoItem(int id, PedidoPan model);
    }

    public class PedidoService : IPedidoService
    {
        private readonly IPanaderiaDbContext _dbContext;

        public PedidoService(IPanaderiaDbContext dbContext) => this._dbContext = dbContext;

        #region Obtener Pedidos
        //Método para obtener listado de pedidos
        public List<object> ObtenerPedidos(int sucursalId)
        {
            List<object> pedidos = new List<object>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT p.PedidoId, p.FechaPedido, p.Ruta, p.Estado, p.Comentarios, p.IsActive, s.Nombre AS Sucursal " +
                            "FROM Pedidos p " +
                            "INNER JOIN Sucursales s ON s.SucursalId = p.SucursalId " +
                            "AND s.IsActive = 1 " +
                            $"AND p.SucursalId = NVL({sucursalId}, p.SucursalId)" +
                            " WHERE p.IsActive = 1";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var pedido = new
                            {
                                PedidoId = Convert.ToInt32(reader["PedidoId"]),
                                FechaPedido = Convert.ToDateTime(reader["FechaPedido"]),
                                Ruta = reader["Ruta"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                Comentarios = reader["Comentarios"].ToString(),
                                IsActive = reader["IsActive"].ToString(),
                                Sucursal = reader["Sucursal"].ToString(),
                            };
                            pedidos.Add(pedido);
                        }
                        reader.Dispose();
                        return pedidos;
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

        #region Obtener Pedido por Id
        //Método para obtener un pedido por id
        public Pedido ObtenerPedidoById(int pedidoId)
        {
            Pedido pedido = new Pedido();
            //Pedido pedidos = new List<object>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT p.PedidoId, p.FechaPedido, p.Ruta, p.Estado, p.Comentarios, p.IsActive, p.SucursalId" +
                            " FROM Pedidos p " +
                            $" WHERE p.PedidoId = NVL({pedidoId}, p.PedidoId)" +
                            " AND p.IsActive = 1";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            pedido = new Pedido()
                            {
                                PedidoId = Convert.ToInt32(reader["PedidoId"]),
                                FechaPedido = Convert.ToDateTime(reader["FechaPedido"]),
                                Ruta = reader["Ruta"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                Comentarios = reader["Comentarios"].ToString(),
                                IsActive = reader["IsActive"].ToString(),
                                SucursalId = Convert.ToInt32(reader["SucursalId"])
                            };
                        }
                        reader.Dispose();
                        return pedido;
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

        #region Obtener Items de Pedido
        //Método para obtener items de un pedido
        public List<object> ObtenerItemsPedido(int pedidoId)
        {
            List<object> items = new List<object>();

            using (OracleConnection con = _dbContext.GetConn())
            {
                using (OracleCommand cmd = _dbContext.GetCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "SELECT pp.Id AS ItemId, ps.Nombre AS Pan, ps.PrecioUnitario, s.Nombre AS Sucursal, pp.Cantidad, pp.Comentarios, pp.IsActive, " +
                            "CASE WHEN (to_char(FechaPedido, 'hh24:mi:ss')) BETWEEN '00:00:01' AND '11:59:59' THEN pp.Cantidad ELSE NULL END AS Tomorrow, '' AS Resto," +
                            "CASE WHEN (to_char(FechaPedido, 'hh24:mi:ss')) BETWEEN '12:00:00' AND '23:59:59' THEN pp.Cantidad ELSE NULL END AS Tarde " +
                            "FROM PedidoPan pp " +
                            "INNER JOIN Pedidos p ON p.PedidoId = pp.PedidoId " +
                            "INNER JOIN Panes ps ON ps.PanId = pp.PanId " +
                            "INNER JOIN Sucursales s ON s.SucursalId = p.SucursalId " +
                            "    AND s.IsActive = 1 " +
                            "WHERE pp.IsActive = 1 " +
                            $"AND pp.PedidoId = NVL({pedidoId}, pp.PedidoId)";

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var item = new
                            {
                                Id = Convert.ToInt32(reader["ItemId"]),
                                Pan = reader["Pan"].ToString(),
                                PrecioUnitario = reader["PrecioUnitario"].ToString(),
                                Sucursal = reader["Sucursal"].ToString(),
                                Cantidad = reader["Cantidad"].ToString(),
                                Comentarios = reader["Comentarios"].ToString(),
                                IsActive = reader["IsActive"].ToString(),
                                Tomorrow = reader["Tomorrow"].ToString(),
                                Resto = reader["Resto"].ToString(),
                                Tarde = reader["Tarde"].ToString()
                            };
                            items.Add(item);
                        }
                        reader.Dispose();
                        return items;
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

        #region Agregar Pedido
        //Método para agregar un Pedido
        public string CrearPedido(int sucursalId, Pedido model)
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

                        cmd.CommandText = "pkgMainFlows.crud_pedido";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_PedidoId", OracleDbType.Int32).Value = null;
                        cmd.Parameters.Add("p_FechaPedido", OracleDbType.Date).Value = model.FechaPedido;
                        cmd.Parameters.Add("p_Ruta", OracleDbType.Varchar2).Value = model.Ruta;
                        cmd.Parameters.Add("p_Estado", OracleDbType.Varchar2).Value = model.Estado;
                        cmd.Parameters.Add("p_Comentarios", OracleDbType.Varchar2).Value = model.Comentarios;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = null;
                        cmd.Parameters.Add("p_SucursalId", OracleDbType.Int32).Value = sucursalId;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El pedido fue creado con éxito.";
                        } else if (salida.Equals("2"))
                        {
                            return "El pedido fue editado con éxito.";
                        } else if(salida.Equals("3"))
                        {
                            return "El pedido fue eliminado con éxito.";
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

        #region Crear Item de un pedido
        //Método para crear un item de un pedido
        public string CrearPedidoItem(int pedidoId, PedidoPan model)
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

                        cmd.CommandText = "pkgMainFlows.crud_pedido_item";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_Id", OracleDbType.Int32).Value = null;
                        cmd.Parameters.Add("p_PanId", OracleDbType.Int32).Value = model.PanId;
                        cmd.Parameters.Add("p_PedidoId", OracleDbType.Int32).Value = pedidoId;
                        cmd.Parameters.Add("p_Cantidad", OracleDbType.Int32).Value = model.Cantidad;
                        cmd.Parameters.Add("p_Comentarios", OracleDbType.Varchar2).Value = model.Comentarios;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = null;
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
                        else if (Convert.ToInt32(salida) >= 10000)
                        {
                            return $"Solo hay {(Convert.ToInt32(salida) - 10000).ToString().Trim    ()} unidades de este pan. Por favor ingrese una cantidad menor o igual a    {(Convert.ToInt32(salida) - 10000).ToString().Trim()}.";
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

        #region Editar un pedido
        //Método para editar un pedido
        public string EditarPedido(int id, Pedido model)
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

                        cmd.CommandText = "pkgMainFlows.crud_pedido";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_PedidoId", OracleDbType.Int32).Value = id;
                        cmd.Parameters.Add("p_FechaPedido", OracleDbType.Date).Value = model.FechaPedido;
                        cmd.Parameters.Add("p_Ruta", OracleDbType.Varchar2).Value = model.Ruta;
                        cmd.Parameters.Add("p_Estado", OracleDbType.Varchar2).Value = model.Estado;
                        cmd.Parameters.Add("p_Comentarios", OracleDbType.Varchar2).Value = model.Comentarios;
                        cmd.Parameters.Add("p_IsActive", OracleDbType.Char).Value = model.IsActive;
                        cmd.Parameters.Add("p_SucursalId", OracleDbType.Int32).Value = model.SucursalId;
                        cmd.Parameters.Add("p_salida", OracleDbType.Varchar2, 2000).Value = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        salida = cmd.Parameters["p_salida"].Value.ToString();

                        if (salida.Equals("1"))
                        {
                            return "El pedido fue creado con éxito.";
                        }
                        else if (salida.Equals("2"))
                        {
                            return "El pedido fue editado con éxito.";
                        }
                        else if (salida.Equals("3"))
                        {
                            return "El pedido fue eliminado con éxito.";
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

        #region Editar item de un pedido
        //Método para editar item de un pedido
        public string EditarPedidoItem(int id, PedidoPan model)
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

                        cmd.CommandText = "pkgMainFlows.crud_pedido_item";

                        //Execute the command and use DataReader to display the data
                        //OracleDataReader reader = cmd.ExecuteReader();
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("p_Id", OracleDbType.Int32).Value = id;
                        cmd.Parameters.Add("p_PanId", OracleDbType.Int32).Value = model.PanId;
                        cmd.Parameters.Add("p_PedidoId", OracleDbType.Int32).Value = model.PedidoId;
                        cmd.Parameters.Add("p_Cantidad", OracleDbType.Int32).Value = model.Cantidad;
                        cmd.Parameters.Add("p_Comentarios", OracleDbType.Varchar2).Value = model.Comentarios;
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
