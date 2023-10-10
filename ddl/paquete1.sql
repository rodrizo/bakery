
CREATE OR REPLACE PACKAGE pkgMain AS 

    PROCEDURE agregar_pan (Nombre in VARCHAR2, PrecioUnitario in VARCHAR2, 
    Descripcion in VARCHAR2, TiempoPreparacion in VARCHAR2, p_salida OUT VARCHAR2);
    
    
    PROCEDURE agregar_ingrediente (Nombre in VARCHAR2, Proveedor in VARCHAR2, 
    CostoUnitario in NUMBER, FechaCompra in DATE, p_salida OUT VARCHAR2);
    
    PROCEDURE agregar_sucursal (Nombre in VARCHAR2, Direccion in VARCHAR2, 
    NumeroTelefono in VARCHAR2, GerenteSucursal in VARCHAR2, HorarioOperacion in VARCHAR2, p_salida OUT VARCHAR2);
    
END pkgMain;


CREATE OR REPLACE PACKAGE BODY pkgMain AS

    PROCEDURE agregar_pan        (Nombre in VARCHAR2, PrecioUnitario in VARCHAR2, 
    Descripcion in VARCHAR2, TiempoPreparacion in VARCHAR2, p_salida OUT VARCHAR2
    ) IS
    BEGIN
       INSERT INTO Panes VALUES ((SELECT MAX(PanId)+1 FROM Panes),Nombre,PrecioUnitario, Descripcion, TiempoPreparacion, 1);
       COMMIT;
       
       --Creando receta inmediatamente para luego solo añadir detalles
       INSERT INTO Recetas VALUES ((SELECT MAX(RecetaId)+1 FROM Recetas),NULL, 1, (SELECT MAX(PanId) FROM Panes));
        p_salida:='Nueva Receta creada con exito';  
       COMMIT;
       
       EXCEPTION
       WHEN OTHERS THEN
        p_salida:='No se pudo ingresar el nuevo pan';
       ROLLBACK;
    END agregar_pan;
    
    
    PROCEDURE agregar_ingrediente (Nombre in VARCHAR2, Proveedor in VARCHAR2, 
    CostoUnitario in NUMBER, FechaCompra in DATE, p_salida OUT VARCHAR2)
    IS
    BEGIN
        INSERT INTO Ingredientes VALUES ((SELECT MAX(IngredienteId)+1 FROM Ingredientes)
        ,Nombre,Proveedor, CostoUnitario, FechaCompra, 1);
        COMMIT;
        
       EXCEPTION
       WHEN OTHERS THEN
        p_salida:='No se pudo ingresar el nuevo ingrediente';
       ROLLBACK;
    END agregar_ingrediente;
    
    PROCEDURE agregar_sucursal (Nombre in VARCHAR2, Direccion in VARCHAR2, 
    NumeroTelefono in VARCHAR2, GerenteSucursal in VARCHAR2, HorarioOperacion in VARCHAR2, p_salida OUT VARCHAR2)
    IS
    BEGIN
        INSERT INTO Sucursales VALUES ((SELECT MAX(SucursalId)+1 FROM Sucursales)
        ,Nombre,Direccion, NumeroTelefono, GerenteSucursal, HorarioOperacion, 1);
        COMMIT;
        
       EXCEPTION
       WHEN OTHERS THEN
        p_salida:='No se pudo ingresar la nueva sucursal';
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