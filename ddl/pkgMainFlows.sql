
CREATE OR REPLACE PACKAGE pkgMainFlows AS 
    PROCEDURE agregar_item_receta (p_Id in NUMBER, p_RecetaId in NUMBER, p_IngredienteId in NUMBER, 
    p_Descripcion in VARCHAR2, p_Cantidad in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2);

    PROCEDURE agregar_pedido (p_PedidoId in NUMBER, p_FechaPedido in DATE, p_Ruta in VARCHAR2, p_Estado in VARCHAR2, 
    p_Comentarios in VARCHAR2, p_SucursalId in NUMBER, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
    PROCEDURE agregar_pedido_item (p_Id in NUMBER, p_PanId in NUMBER, p_PedidoId in NUMBER, p_Cantidad in NUMBER, 
    p_Comentarios in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
END pkgMainFlows;


CREATE OR REPLACE PACKAGE BODY pkgMainFlows AS
    
    PROCEDURE agregar_item_receta (p_Id in NUMBER, p_RecetaId in NUMBER, p_IngredienteId in NUMBER, 
    p_Descripcion in VARCHAR2, p_Cantidad in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2)
    IS
    
    BEGIN
    
        --Si p_Id no es nulo, hay dos opciones -> Update o Delete
        IF (p_Id IS NOT NULL) THEN
            --Si IsActive = NULL + PanId <> NULL -> Update sencillo
            IF (p_IsActive IS NULL) THEN
                UPDATE RecetaIngrediente rg
                SET rg.IngredienteId = p_IngredienteId, rg.Descripcion = p_Descripcion, rg.Cantidad = p_Cantidad
                WHERE rg.Id = p_Id;
                COMMIT;
                p_salida:='2'; -- Código para determinar updates
                --Si IsActive <> NULL -> Delete
            ELSIF (p_IsActive IS NOT NULL) THEN
                UPDATE RecetaIngrediente rg
                SET rg.IsActive = '0'
                 WHERE rg.Id = p_Id;
                    p_salida:='3'; --Código para determinar deletes
                COMMIT;
            END IF;
        ELSIF (p_Id IS NULL) THEN
            INSERT INTO RecetaIngrediente VALUES (p_RecetaId,p_IngredienteId,p_Descripcion,p_Cantidad,1,(SELECT MAX(Id)+1 FROM RecetaIngrediente));
                p_salida:='1';  --Código para determinar inserts
            COMMIT;
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='0'; --Código para determinar errores
        ROLLBACK;
    END agregar_item_receta;
    
    PROCEDURE agregar_pedido (p_PedidoId in NUMBER, p_FechaPedido in DATE, p_Ruta in VARCHAR2, p_Estado in VARCHAR2, 
    p_Comentarios in VARCHAR2, p_SucursalId in NUMBER, p_IsActive in CHAR, p_salida OUT VARCHAR2)
    IS
    BEGIN
        --Si p_PedidoId no es nulo, hay dos opciones -> Update o Delete
        IF (p_PedidoId IS NOT NULL) THEN
            --Si IsActive = NULL + PanId <> NULL -> Update sencillo
            IF (p_IsActive IS NULL) THEN
                UPDATE Pedidos p
                SET p.PedidoId = p_PedidoId, p.FechaPedido = p_FechaPedido, p.Ruta = p_Ruta, 
                p.Estado = p_Estado, p.Comentarios = p_Comentarios
                WHERE p.SucursalId = p_SucursalId;
                COMMIT;
                p_salida:='2'; -- Código para determinar updates
                --Si IsActive <> NULL -> Delete
            ELSIF (p_IsActive IS NOT NULL) THEN
                UPDATE Pedidos p
                SET p.IsActive = '0'
                WHERE p.PedidoId = p_PedidoId;
                    p_salida:='3'; --Código para determinar deletes
                COMMIT;
            END IF;
        ELSIF (p_PedidoId IS NULL) THEN
            INSERT INTO Pedidos VALUES ((SELECT MAX(PedidoId)+1 FROM Pedidos),p_FechaPedido,
            p_Ruta,p_Estado, p_Comentarios, 1, p_SucursalId);
                p_salida:='1';  --Código para determinar inserts
            COMMIT;
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='0'; --Código para determinar errores
        ROLLBACK;
    END agregar_pedido;
    
    --missing this one to apply the logic
    PROCEDURE agregar_pedido_item (p_Id in NUMBER, p_PanId in NUMBER, p_PedidoId in NUMBER, p_Cantidad in NUMBER, 
    p_Comentarios in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2)
    IS
    BEGIN
        --Si p_PedidoId no es nulo, hay dos opciones -> Update o Delete
        IF (p_Id IS NOT NULL) THEN
            --Si IsActive = NULL + PanId <> NULL -> Update sencillo
            IF (p_IsActive IS NULL) THEN
                UPDATE PedidoPan pp
                SET pp.PanId = p_PanId, pp.PedidoId = p_PedidoId, pp.Cantidad = p_Cantidad, 
                pp.Comentarios = p_Comentarios
                WHERE pp.Id = p_Id;
                COMMIT;
                p_salida:='2'; -- Código para determinar updates
                --Si IsActive <> NULL -> Delete
            ELSIF (p_IsActive IS NOT NULL) THEN
                UPDATE PedidoPan pp
                SET pp.IsActive = '0'
                WHERE pp.Id = p_Id;
                    p_salida:='3'; --Código para determinar deletes
                COMMIT;
            END IF;
        ELSIF (p_Id IS NULL) THEN
            INSERT INTO PedidoPan VALUES (p_PanId, p_PedidoId, p_Cantidad, p_Comentarios, 1, 
            (SELECT MAX(Id)+1 FROM PedidoPan));
                p_salida:='1';  --Código para determinar inserts
            COMMIT;
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='0'; --Código para determinar errores
        ROLLBACK;   
    END agregar_pedido_item;
    
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


VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMainFlows.agregar_pedido_item(1, 1, 5, '5 panes de tipo 1', :p_salida);
END;
/
*/