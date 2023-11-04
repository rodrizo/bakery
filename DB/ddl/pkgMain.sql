
CREATE OR REPLACE PACKAGE pkgMain AS 

    PROCEDURE crud_pan (p_PanId in NUMBER, p_Nombre in VARCHAR2, p_PrecioUnitario in VARCHAR2, 
    p_Descripcion in VARCHAR2, p_TiempoPreparacion in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
    PROCEDURE crud_ingrediente (p_IngredienteId in NUMBER, p_Nombre in VARCHAR2, p_Proveedor in VARCHAR2, 
    p_CostoUnitario in NUMBER, p_FechaCompra in DATE, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
    PROCEDURE crud_sucursal (p_SucursalId in NUMBER, p_Nombre in VARCHAR2, p_Direccion in VARCHAR2, 
    p_NumeroTelefono in VARCHAR2, p_GerenteSucursal in VARCHAR2, p_HorarioOperacion in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
END pkgMain;

    
CREATE OR REPLACE PACKAGE BODY pkgMain AS

    PROCEDURE crud_pan        (p_PanId in NUMBER, p_Nombre in VARCHAR2, p_PrecioUnitario in VARCHAR2, 
    p_Descripcion in VARCHAR2, p_TiempoPreparacion in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2
    ) IS
    BEGIN
        --Si PanId no es nulo, hay dos opciones -> Update o Delete
        IF (p_PanId IS NOT NULL) THEN
            --Si IsActive = NULL + PanId <> NULL -> Update sencillo
            IF (p_IsActive IS NULL) THEN
                UPDATE Panes p
                SET p.Nombre = p_Nombre, p.PrecioUnitario = p_PrecioUnitario, p.Descripcion = p_Descripcion,
                p.TiempoPreparacion = p_TiempoPreparacion
                WHERE p.PanId = p_PanId;
                COMMIT;
                p_salida:='2'; -- Código para determinar updates
                --Si IsActive <> NULL -> Delete
            ELSIF (p_IsActive IS NOT NULL) THEN
                UPDATE Panes
                SET IsActive = '0'
                WHERE PanId = p_PanId;
                    p_salida:='3'; --Código para determinar deletes
                COMMIT;
            END IF;
        ELSIF (p_PanId IS NULL) THEN
            INSERT INTO Panes VALUES ((SELECT MAX(PanId)+1 FROM Panes),p_Nombre,p_PrecioUnitario, p_Descripcion, p_TiempoPreparacion, 1);
            COMMIT;
           
            --Creando receta inmediatamente para luego solo añadir detalles
            INSERT INTO Recetas VALUES ((SELECT MAX(RecetaId)+1 FROM Recetas),NULL, 1, (SELECT MAX(PanId) FROM Panes));
            COMMIT;
            
            --Creando stock automáticamente para el nuevo pan que acaba de crearse
            INSERT INTO Stock VALUES((SELECT MAX(StockId)+1 FROM Stock), (SELECT MAX(PanId)+1 FROM Panes), 500, NULL, SYSDATE, SYSDATE, 1);
                p_salida:='1';  --Código para determinar inserts
            COMMIT;
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='0'; --Código para determinar errores
        ROLLBACK;
    END crud_pan;
    
    
    PROCEDURE crud_ingrediente (p_IngredienteId in NUMBER, p_Nombre in VARCHAR2, p_Proveedor in VARCHAR2, 
    p_CostoUnitario in NUMBER, p_FechaCompra in DATE, p_IsActive in CHAR, p_salida OUT VARCHAR2)
    IS
    BEGIN
        --Si IngredienteId no es nulo, hay dos opciones -> Update o Delete
        IF (p_IngredienteId IS NOT NULL) THEN
            --Si IsActive = NULL + IngredienteId <> NULL -> Update sencillo
            IF (p_IsActive IS NULL) THEN
                UPDATE Ingredientes i
                    SET i.Nombre = p_Nombre, i.Proveedor = p_Proveedor, i.CostoUnitario = p_CostoUnitario,
                    i.FechaCompra = p_FechaCompra
                    WHERE i.IngredienteId = p_IngredienteId;
                    COMMIT;
                    p_salida:='2'; -- Código para determinar updates
                    --Si IsActive <> NULL -> Delete
                    ELSIF (p_IsActive IS NOT NULL) THEN
                UPDATE Ingredientes
                SET IsActive = '0'
                WHERE IngredienteId = p_IngredienteId;
                    p_salida:='3'; --Código para determinar deletes
                COMMIT;
            END IF;
        ELSIF (p_IngredienteId IS NULL) THEN
            INSERT INTO Ingredientes VALUES ((SELECT MAX(IngredienteId)+1 FROM Ingredientes)
            ,p_Nombre,p_Proveedor, p_CostoUnitario, p_FechaCompra, 1);
            p_salida:='1';  
            COMMIT;
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='0'; --Código para determinar errores
        ROLLBACK;
    END crud_ingrediente;
    
    PROCEDURE crud_sucursal (p_SucursalId in NUMBER, p_Nombre in VARCHAR2, p_Direccion in VARCHAR2, 
    p_NumeroTelefono in VARCHAR2, p_GerenteSucursal in VARCHAR2, p_HorarioOperacion in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2)
    IS
    BEGIN
        --Si SucursalId no es nulo, hay dos opciones -> Update o Delete
        IF (p_SucursalId IS NOT NULL) THEN
            --Si IsActive = NULL + SucursalId <> NULL -> Update sencillo
            IF (p_IsActive IS NULL) THEN
                UPDATE Sucursales s
                    SET s.Nombre = p_Nombre, s.Direccion = p_Direccion, s.NumeroTelefono = p_NumeroTelefono,
                    s.GerenteSucursal = p_GerenteSucursal, s.HorarioOperacion = p_HorarioOperacion
                    WHERE s.SucursalId = p_SucursalId;
                    COMMIT;
                    p_salida:='2'; -- Código para determinar updates
                    --Si IsActive <> NULL -> Delete
                    ELSIF (p_IsActive IS NOT NULL) THEN
                UPDATE Sucursales
                SET IsActive = '0'
                WHERE SucursalId = p_SucursalId;
                    p_salida:='3'; --Código para determinar deletes
                COMMIT;
            END IF;
        ELSIF (p_SucursalId IS NULL) THEN
            INSERT INTO Sucursales VALUES ((SELECT MAX(SucursalId)+1 FROM Sucursales)
            ,p_Nombre,p_Direccion, p_NumeroTelefono, p_GerenteSucursal, p_HorarioOperacion, 1);
            p_salida:='1';  
            COMMIT;
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='0'; --Código para determinar errores
        ROLLBACK;
        /*
        
        */  
       
    END crud_sucursal;
    
END pkgMain;

/*
SET SERVEROUTPUT ON;
VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMain.crud_pan(13, 'Pudín', '7', 'Pudin ok', '2', NULL,:p_salida);
 
END;
/


VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMain.crud_ingrediente(11, 'Agua', 'Agua Salvavidas 2 TEST', 16, SYSDATE, 1, :p_salida);
END;
/


VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMain.crud_sucursal(7, 'Nueva Terminal TEST', 'Main St 888', '505 12345678', 'Fidelina Perez', '8am - 7pm', 0, :p_salida);
END;
/


*/