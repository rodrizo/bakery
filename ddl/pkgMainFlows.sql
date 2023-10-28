CREATE OR REPLACE PACKAGE pkgMainFlows AS 
    PROCEDURE crud_item_receta (p_Id in NUMBER, p_RecetaId in NUMBER, p_IngredienteId in NUMBER, 
    p_Descripcion in VARCHAR2, p_Cantidad in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2);

    PROCEDURE crud_pedido (p_PedidoId in NUMBER, p_FechaPedido in DATE, p_Ruta in VARCHAR2, p_Estado in VARCHAR2, 
    p_Comentarios in VARCHAR2, p_SucursalId in NUMBER, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
    PROCEDURE crud_pedido_item (p_Id in NUMBER, p_PanId in NUMBER, p_PedidoId in NUMBER, p_Cantidad in NUMBER, 
    p_Comentarios in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
    PROCEDURE crud_stock (p_StockId in NUMBER, p_PanId in NUMBER, p_Cantidad in NUMBER, 
    p_Notas in VARCHAR2, p_FechaCreacion in DATE, p_FechaModificacion in DATE, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
END pkgMainFlows;


CREATE OR REPLACE PACKAGE BODY pkgMainFlows AS
    
    PROCEDURE crud_item_receta (p_Id in NUMBER, p_RecetaId in NUMBER, p_IngredienteId in NUMBER, 
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
    END crud_item_receta;
    
    PROCEDURE crud_pedido (p_PedidoId in NUMBER, p_FechaPedido in DATE, p_Ruta in VARCHAR2, p_Estado in VARCHAR2, 
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
                WHERE p.PedidoId = p_PedidoId;
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
    END crud_pedido;
    
    --missing this one to apply the logic
    PROCEDURE crud_pedido_item (p_Id in NUMBER, p_PanId in NUMBER, p_PedidoId in NUMBER, p_Cantidad in NUMBER, 
    p_Comentarios in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2)
    IS
    BEGIN
        --Si p_PedidoId no es nulo, hay dos opciones -> Update o Delete
        IF (p_Id IS NOT NULL) THEN
                --Si IsActive = NULL + PanId <> NULL -> Update sencillo
                IF (p_IsActive IS NULL) THEN
                    UPDATE PedidoPan pp
                    SET --pp.PanId = p_PanId,-- pp.Cantidad = p_Cantidad, 
                    pp.Comentarios = p_Comentarios
                    WHERE pp.Id = p_Id;
                    COMMIT;
                    p_salida:='2'; -- Código para determinar updates
                --Si IsActive <> NULL -> Delete
                ELSIF (p_IsActive IS NOT NULL) THEN
                    --Cuando se elimine un item del pedido, se sumará nuevamente la cantidad al stock de ese pan
                    UPDATE Stock st
                    SET st.Cantidad = (SELECT (Cantidad + st.Cantidad) FROM PedidoPan pp WHERE pp.Id = p_Id AND pp.IsActive = 1)
                    WHERE st.PanId = p_PanId;
                    
                    COMMIT;
                    
                    DELETE PedidoPan
                    WHERE Id = p_Id;
                        p_salida:='3'; --Código para determinar deletes
                    COMMIT;
                END IF;
        ELSIF (p_Id IS NULL) THEN
            DECLARE stockFuturo NUMBER; --Flag para validar inventario del pan
            BEGIN
                SELECT (Cantidad - p_Cantidad) INTO stockFuturo FROM Stock WHERE PanId = p_PanId AND IsActive = 1;
                --Si el stock quedará en 0 o arriba de cero, podemos añadir el item al pedido
                IF stockFuturo >= 0 THEN
                    --Creando nuevo item del pedido
                    INSERT INTO PedidoPan VALUES (p_PanId, p_PedidoId, p_Cantidad, p_Comentarios, 1, 
                    (SELECT MAX(Id)+1 FROM PedidoPan));
                    
                    --Actualizando stock
                    UPDATE Stock
                    SET Cantidad = Cantidad - p_Cantidad
                    WHERE PanId = p_PanId
                    AND IsActive = 1;
                    
                    p_salida:='1';  --Código para determinar inserts
                    
                    COMMIT;
                ELSE
                    DECLARE salidaStock CHAR(100);
                    BEGIN
                        SELECT TO_CHAR(Cantidad) INTO salidaStock FROM Stock WHERE PanId = p_PanId AND IsActive = 1;
                        --p_salida;
                        p_salida:= salidaStock+10000; --Código para determinar error de inventario
                        --ROLLBACK;   
                    END;
                END IF;
                
                EXCEPTION
                WHEN OTHERS THEN
                    p_salida:='0'; --Código para determinar errores
                ROLLBACK;   
            END;
        END IF;
    END crud_pedido_item;
    
    PROCEDURE crud_stock (p_StockId in NUMBER, p_PanId in NUMBER, p_Cantidad in NUMBER, 
    p_Notas in VARCHAR2, p_FechaCreacion in DATE, p_FechaModificacion in DATE, p_IsActive in CHAR, p_salida OUT VARCHAR2)
    IS
    BEGIN
        --Insertando datos en bitácora antes de ser modificados
        INSERT INTO StockActivity 
        SELECT (SELECT MAX(Id)+1 FROM StockActivity), st.StockId, st.PanId, st.Cantidad, st.Notas, st.FechaCreacion, st.FechaModificacion
        FROM Stock st
        WHERE st.StockId = p_StockId;
        
        COMMIT;
        
        --Editando stock
        UPDATE Stock
        SET Cantidad = p_Cantidad, Notas = p_Notas, FechaModificacion = SYSDATE
        WHERE StockId = p_StockId;
        
        COMMIT;
        
        p_salida:='2'; -- Código para determinar updates
    
    END crud_stock;
    
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



VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMainFlows.crud_pedido_item(null, 1, 1, 5500, '10 panes franceces', '1', :p_salida);
  
END;
/


*/