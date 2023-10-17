
CREATE OR REPLACE PACKAGE pkgMain AS 

    PROCEDURE crud_pan (p_PanId in NUMBER, p_Nombre in VARCHAR2, p_PrecioUnitario in VARCHAR2, 
    p_Descripcion in VARCHAR2, p_TiempoPreparacion in VARCHAR2, p_IsActive in CHAR, p_salida OUT VARCHAR2);
    
    PROCEDURE agregar_ingrediente (p_Nombre in VARCHAR2, p_Proveedor in VARCHAR2, 
    p_CostoUnitario in NUMBER, p_FechaCompra in DATE, p_salida OUT VARCHAR2);
    
    PROCEDURE agregar_sucursal (p_Nombre in VARCHAR2, p_Direccion in VARCHAR2, 
    p_NumeroTelefono in VARCHAR2, p_GerenteSucursal in VARCHAR2, p_HorarioOperacion in VARCHAR2, p_salida OUT VARCHAR2);
    
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
                p_salida:='2'; -- C�digo para determinar updates
                --Si IsActive <> NULL -> Delete
            ELSIF (p_IsActive IS NOT NULL) THEN
                UPDATE Panes
                SET IsActive = '0'
                WHERE PanId = p_PanId;
                    p_salida:='3'; --C�digo para determinar deletes
                COMMIT;
            END IF;
        ELSIF (p_PanId IS NULL) THEN
            INSERT INTO Panes VALUES ((SELECT MAX(PanId)+1 FROM Panes),p_Nombre,p_PrecioUnitario, p_Descripcion, p_TiempoPreparacion, 1);
            COMMIT;
           
            --Creando receta inmediatamente para luego solo a�adir detalles
            INSERT INTO Recetas VALUES ((SELECT MAX(RecetaId)+1 FROM Recetas),NULL, 1, (SELECT MAX(PanId) FROM Panes));
                p_salida:='1';  --C�digo para determinar inserts
            COMMIT;
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='0'; --C�digo para determinar errores
        ROLLBACK;
    END crud_pan;
    
    
    PROCEDURE agregar_ingrediente (p_Nombre in VARCHAR2, p_Proveedor in VARCHAR2, 
    p_CostoUnitario in NUMBER, p_FechaCompra in DATE, p_salida OUT VARCHAR2)
    IS
    BEGIN
        INSERT INTO Ingredientes VALUES ((SELECT MAX(IngredienteId)+1 FROM Ingredientes)
        ,p_Nombre,p_Proveedor, p_CostoUnitario, p_FechaCompra, 1);
        p_salida:='1';  
        COMMIT;
        
       EXCEPTION
       WHEN OTHERS THEN
        p_salida:='0';
       ROLLBACK;
    END agregar_ingrediente;
    
    PROCEDURE agregar_sucursal (p_Nombre in VARCHAR2, p_Direccion in VARCHAR2, 
    p_NumeroTelefono in VARCHAR2, p_GerenteSucursal in VARCHAR2, p_HorarioOperacion in VARCHAR2, p_salida OUT VARCHAR2)
    IS
    BEGIN
        INSERT INTO Sucursales VALUES ((SELECT MAX(SucursalId)+1 FROM Sucursales)
        ,p_Nombre,p_Direccion, p_NumeroTelefono, p_GerenteSucursal, p_HorarioOperacion, 1);
        p_salida:='1';  
        COMMIT;
        
       EXCEPTION
       WHEN OTHERS THEN
        p_salida:='0';
       ROLLBACK;
    END agregar_sucursal;
    
END pkgMain;

/*
SET SERVEROUTPUT ON;
VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMain.crud_pan(13, 'Pud�n', '7', 'Pudin ok', '2', NULL,:p_salida);
 
END;
/


VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMain.agregar_ingrediente('Agua', 'Agua Salvavidas', 16, SYSDATE, :p_salida);
END;
/


VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMain.agregar_sucursal('Nueva Terminal', 'Main St 888', '505 123123123', 'Fidelina Perez', '8am - 7pm', :p_salida);
END;
/
*/