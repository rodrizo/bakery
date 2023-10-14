
CREATE OR REPLACE PACKAGE pkgMain AS 

    PROCEDURE agregar_pan (PanId in NUMBER, Nombre in VARCHAR2, PrecioUnitario in VARCHAR2, 
    Descripcion in VARCHAR2, TiempoPreparacion in VARCHAR2, IsActive in CHAR, p_salida OUT VARCHAR2);
    
    
    PROCEDURE agregar_ingrediente (Nombre in VARCHAR2, Proveedor in VARCHAR2, 
    CostoUnitario in NUMBER, FechaCompra in DATE, p_salida OUT VARCHAR2);
    
    PROCEDURE agregar_sucursal (Nombre in VARCHAR2, Direccion in VARCHAR2, 
    NumeroTelefono in VARCHAR2, GerenteSucursal in VARCHAR2, HorarioOperacion in VARCHAR2, p_salida OUT VARCHAR2);
    
END pkgMain;


CREATE OR REPLACE PACKAGE BODY pkgMain AS

    PROCEDURE agregar_pan        (PanId in NUMBER, Nombre in VARCHAR2, PrecioUnitario in VARCHAR2, 
    Descripcion in VARCHAR2, TiempoPreparacion in VARCHAR2, IsActive in CHAR, p_salida OUT VARCHAR2
    ) IS
    BEGIN
        --Si PanId no es nulo, hay dos opciones -> Update o Delete
        IF (PanId IS NOT NULL) THEN
            --Si IsActive = NULL + PanId <> NULL -> Update sencillo
            IF (IsActive IS NULL) THEN
                UPDATE Panes
                SET Nombre = Nombre, PrecioUnitario = PrecioUnitario, Descripcion = Descripcion,
                TiempoPreparacion = TiempoPreparacion
                WHERE PanId = PanId;
                    p_salida:='2'; -- Código para determinar updates
                COMMIT;
                --Si IsActive <> NULL -> Delete
            ELSIF (IsActive IS NOT NULL) THEN
                UPDATE Panes
                SET IsActive = '0'
                WHERE PanId = PanId;
                    p_salida:='3'; --Código para determinar deletes
                COMMIT;
            END IF;
        ELSIF (PanId IS NULL) THEN
            INSERT INTO Panes VALUES ((SELECT MAX(PanId)+1 FROM Panes),Nombre,PrecioUnitario, Descripcion, TiempoPreparacion, 1);
            COMMIT;
           
            --Creando receta inmediatamente para luego solo añadir detalles
            INSERT INTO Recetas VALUES ((SELECT MAX(RecetaId)+1 FROM Recetas),NULL, 1, (SELECT MAX(PanId) FROM Panes));
                p_salida:='1';  --Código para determinar inserts
            COMMIT;
        END IF;
        
        EXCEPTION
        WHEN OTHERS THEN
            p_salida:='0'; --Código para determinar errores
        ROLLBACK;
    END agregar_pan;
    
    
    PROCEDURE agregar_ingrediente (Nombre in VARCHAR2, Proveedor in VARCHAR2, 
    CostoUnitario in NUMBER, FechaCompra in DATE, p_salida OUT VARCHAR2)
    IS
    BEGIN
        INSERT INTO Ingredientes VALUES ((SELECT MAX(IngredienteId)+1 FROM Ingredientes)
        ,Nombre,Proveedor, CostoUnitario, FechaCompra, 1);
        p_salida:='1';  
        COMMIT;
        
       EXCEPTION
       WHEN OTHERS THEN
        p_salida:='0';
       ROLLBACK;
    END agregar_ingrediente;
    
    PROCEDURE agregar_sucursal (Nombre in VARCHAR2, Direccion in VARCHAR2, 
    NumeroTelefono in VARCHAR2, GerenteSucursal in VARCHAR2, HorarioOperacion in VARCHAR2, p_salida OUT VARCHAR2)
    IS
    BEGIN
        INSERT INTO Sucursales VALUES ((SELECT MAX(SucursalId)+1 FROM Sucursales)
        ,Nombre,Direccion, NumeroTelefono, GerenteSucursal, HorarioOperacion, 1);
        p_salida:='1';  
        COMMIT;
        
       EXCEPTION
       WHEN OTHERS THEN
        p_salida:='0';
       ROLLBACK;
    END agregar_sucursal;
    
END pkgMain;

/*
VARIABLE p_salida VARCHAR2;

BEGIN
  
  pkgMain.agregar_pan('Alemán', '3.5', 'Descripción del pan alemán', '2', :p_salida);

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