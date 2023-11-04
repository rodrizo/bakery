------------------------------------------------------------------------------------------------------------------
SELECT *
FROM Sucursales


INSERT INTO Sucursales VALUES ((SELECT MAX(SucursalId)+1 FROM Sucursales), 'Terminal', 
'Zona 1 Lote 10', '502 10231234', 'Lionel Messi', '6am - 4pm', 1)
------------------------------------------------------------------------------------------------------------------
SELECT *
FROM Ingredientes


INSERT INTO Ingredientes VALUES (1, 'Harina de Fuerza', 'Proveedor ', 10.5, SYSDATE,1)

INSERT INTO Ingredientes VALUES ((SELECT MAX(IngredienteId)+1 FROM Ingredientes), 
'Huevo', 'Los Pollos Hermanos', 35, SYSDATE,1)
------------------------------------------------------------------------------------------------------------------
SELECT *
FROM Panes
SELECT *
FROM Recetas


INSERT INTO Panes VALUES (1, 'Pan Francés', '1.5', 'Pan francés', '1.5' ,1)

------------------------------------------------------------------------------------------------------------------
SELECT *
FROM Recetas

INSERT INTO Recetas VALUES (1, 'Receta para pan fracés', 1, 1)






