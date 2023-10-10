
CREATE OR REPLACE PACKAGE pkgMainFlows AS 

    PROCEDURE agregar_item_receta (RecetaId in NUMBER, IngredienteId in NUMBER, 
    Descripcion in VARCHAR2, Cantidad in VARCHAR2, p_salida OUT VARCHAR2);

    PROCEDURE agregar_pedido (FechaPedido in DATE, Cantidad in NUMBER, 
    Ruta in VARCHAR2, Estado in VARCHAR2, Comentarios in VARCHAR2, SucursalId in NUMBER, p_salida OUT VARCHAR2);
    
END pkgMainFlows;


CREATE OR REPLACE PACKAGE BODY pkgMainFlows AS
    
    PROCEDURE agregar_item_receta (RecetaId in NUMBER, IngredienteId in NUMBER, 
    Descripcion in VARCHAR2, Cantidad in VARCHAR2, p_salida OUT VARCHAR2)
    IS
    
    BEGIN
        INSERT INTO RecetaIngrediente VALUES (RecetaId,IngredienteId,Descripcion,Cantidad,1);
        COMMIT;
        
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='No se pudo ingresar el nuevo ingrediente a la receta';
        ROLLBACK;
    END agregar_item_receta;
    
    
    PROCEDURE agregar_pedido (FechaPedido in DATE, Cantidad in NUMBER, 
    Ruta in VARCHAR2, Estado in VARCHAR2, Comentarios in VARCHAR2, SucursalId in NUMBER, p_salida OUT VARCHAR2)
    IS
    BEGIN
        INSERT INTO Pedidos VALUES ((SELECT MAX(PedidoId)+1 FROM Pedidos),FechaPedido,Cantidad,
        Ruta,Estado, Comentarios, SucursalId, 1);
        COMMIT;
        
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='No se pudo ingresar el nuevo pedido';
        ROLLBACK;
    END agregar_pedido;
END pkgMainFlows;

/*

VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMainFlows.agregar_item_receta(1, 2, 'Echar 3 tazas de harina suave', '3', :p_salida);
END;
/

SELECT *
FROM Pedidos

VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMainFlows.agregar_pedido(1, 2, 'Echar 3 tazas de harina suave', '3', :p_salida);
END;
/
*/