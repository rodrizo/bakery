

/*OBTENER ITEMS DE UN PEDIDO EN ESPECÍFICO*/

SELECT p.PedidoId, pp.Id AS ItemId, ps.PanId, ps.Nombre AS Pan, ps.PrecioUnitario, s.Nombre AS Sucursal, pp.Cantidad, pp.Comentarios, pp.IsActive, 
CASE WHEN (to_char(FechaPedido, 'hh24:mi:ss')) BETWEEN '00:00:01' AND '11:59:59' THEN pp.Cantidad ELSE NULL END AS Tomorrow, '' AS Resto, 
CASE WHEN (to_char(FechaPedido, 'hh24:mi:ss')) BETWEEN '12:00:00' AND '23:59:59' THEN pp.Cantidad ELSE NULL END AS Tarde 
FROM PedidoPan pp 
INNER JOIN Pedidos p ON p.PedidoId = pp.PedidoId 
INNER JOIN Panes ps ON ps.PanId = pp.PanId 
INNER JOIN Sucursales s ON s.SucursalId = p.SucursalId 
    AND s.IsActive = 1 
WHERE pp.IsActive = 1 
AND pp.PedidoId = NVL(NULL, pp.PedidoId) --Insertar ID de pedido acá
ORDER BY p.PedidoId

/*OBTENER UN PEDIDO POR ID*/
SELECT p.PedidoId, p.FechaPedido, p.Ruta, p.Estado, p.Comentarios, p.IsActive, p.SucursalId
FROM Pedidos p 
WHERE p.PedidoId = NVL(p.pedidoId, p.PedidoId) --Insertar ID de pedido acá
AND p.IsActive = 1


/*OBTENER ITEMS DE UNA RECETA*/

SELECT r.RecetaId, ri.Id AS ItemId, p.Nombre, ig.Nombre AS Ingrediente, ri.Descripcion, ri.Cantidad, ri.IsActive 
FROM Recetas r 
INNER JOIN Panes p ON p.PanId = r.PanId 
AND p.IsActive = 1 
INNER JOIN RecetaIngrediente ri ON ri.RecetaId = r.RecetaId 
AND ri.IsActive = 1 
INNER JOIN Ingredientes ig ON ig.IngredienteId = ri.IngredienteId 
AND ig.IsActive = 1 
WHERE r.IsActive = 1
AND r.RecetaId = NVL(r.RecetaId, r.RecetaId)
ORDER BY r.RecetaId ASC

/*OBTENER STOCK*/

SELECT st.StockId AS Id, p.Nombre AS Pan, st.Cantidad, st.Notas, st.FechaCreacion, st.FechaModificacion 
FROM Stock st 
INNER JOIN Panes p ON p.PanId = st.PanId 
AND p.IsActive = 1 
WHERE st.IsActive = 1 

/*OBTENER BITÁCORA DE STOCK*/
SELECT st.StockId, p.Nombre, st.Cantidad, st.Notas, st.FechaCreacion, st.FechaModificacion, sk.Cantidad AS CantidadActual
FROM StockActivity st
INNER JOIN Stock sk ON sk.StockId = st.StockId
INNER JOIN Panes p ON p.PanId = sk.PanId
ORDER BY 1