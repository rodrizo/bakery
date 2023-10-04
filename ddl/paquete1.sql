
CREATE OR REPLACE PACKAGE PAQUETE1 AS 

    PROCEDURE agregar_pan (Nombre in VARCHAR2, PrecioUnitario in VARCHAR2, 
    Descripcion in VARCHAR2, TiempoPreparacion in VARCHAR2, p_salida OUT VARCHAR2);
    
END PAQUETE1;


CREATE OR REPLACE PACKAGE BODY PAQUETE1 AS

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
    
    
END PAQUETE1;

/*
VARIABLE p_salida VARCHAR2;

BEGIN
  
  PAQUETE1.agregar_pan('Cubos', '2', 'Cubos descripcion', '2', :p_salida);
END;
/
*/