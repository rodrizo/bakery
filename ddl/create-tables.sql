-- Generated by Oracle SQL Developer Data Modeler 23.1.0.087.0806
--   at:        2023-09-19 00:41:40 EDT
--   site:      Oracle Database 21c
--   type:      Oracle Database 21c



DROP TABLE Ingrediente CASCADE CONSTRAINTS;

DROP TABLE Pan CASCADE CONSTRAINTS;

DROP TABLE PanReceta CASCADE CONSTRAINTS;

DROP TABLE Pedido CASCADE CONSTRAINTS;

DROP TABLE PedidoPan CASCADE CONSTRAINTS;

DROP TABLE Receta CASCADE CONSTRAINTS;

DROP TABLE RecetaIngrediente CASCADE CONSTRAINTS;

DROP TABLE ucursal CASCADE CONSTRAINTS;

-- predefined type, no DDL - MDSYS.SDO_GEOMETRY

-- predefined type, no DDL - XMLTYPE

CREATE TABLE Ingrediente (
    IngredienteId NUMBER(4) NOT NULL,
    Nombre        VARCHAR2(250 CHAR),
    Proveedor     VARCHAR2(250 CHAR),
    CostoUnitario NUMBER(4),
    FechaCompra   DATE,
    IsActive      CHAR(1)
);

ALTER TABLE ingrediente ADD CONSTRAINT ingrediente_pk PRIMARY KEY ( ingredienteid );

CREATE TABLE Pan (
    PanId             NUMBER(4) NOT NULL,
    Nombre            VARCHAR2(75),
    PrecioUnitario    NUMBER(5),
    Descripcion       VARCHAR2(1250),
    TiempoPreparacion NUMBER(2),
    IsActive          CHAR(1)
);

COMMENT ON COLUMN pan.panid IS
    'Id del tipo de pan a producir';

COMMENT ON COLUMN pan.nombre IS
    'Nombre del tipo de pan';

ALTER TABLE pan ADD CONSTRAINT pan_pk PRIMARY KEY ( panid );

CREATE TABLE PanReceta (
    PanId       NUMBER(4) NOT NULL,
    RecetaId NUMBER(4) NOT NULL
);

ALTER TABLE PanReceta ADD CONSTRAINT pan_receta_pk PRIMARY KEY ( PanId,
                                                                  RecetaId );

CREATE TABLE Pedido (
    PedidoId            NUMBER(4) NOT NULL,
    FechaPedido         DATE,
    Cantidad            NUMBER(4),
    Ruta                VARCHAR2(75 CHAR),
    Estado              VARCHAR2(250 CHAR),
    Comentarios         VARCHAR2(250 CHAR),
    IsActive            CHAR(1),
    SucursalId NUMBER(4) NOT NULL
);

ALTER TABLE Pedido ADD CONSTRAINT pedido_pk PRIMARY KEY ( PedidoId,
                                                          Sucursalid );

CREATE TABLE PedidoPan (
    PanId         NUMBER(4) NOT NULL,
    PedidoId   NUMBER(4) NOT NULL,
    SucursalId NUMBER(4) NOT NULL
);

ALTER TABLE PedidoPan
    ADD CONSTRAINT pedido_pan_pk PRIMARY KEY ( PanId,
                                               PedidoId,
                                               SucursalId );

CREATE TABLE Receta (
    RecetaId    NUMBER(4) NOT NULL,
    Cantidad    NUMBER(4),
    Descripcion VARCHAR2(250 CHAR),
    IsActive    CHAR(1)
);

ALTER TABLE Receta ADD CONSTRAINT receta_pk PRIMARY KEY ( RecetaId );

CREATE TABLE RecetaIngrediente (
    RecetaId           NUMBER(4) NOT NULL,
    IngredienteId NUMBER(4) NOT NULL
);

ALTER TABLE RecetaIngrediente ADD CONSTRAINT receta_ingrediente_pk PRIMARY KEY ( RecetaId,
                                                                                  IngredienteId );

CREATE TABLE Sucursal (
    SucursalId       NUMBER(4) NOT NULL,
    Nombre           VARCHAR2(250 CHAR),
    Direccion        VARCHAR2(750 CHAR),
    NumeroTelefono   VARCHAR2(30 CHAR),
    GerenteSucursal  VARCHAR2(75 CHAR),
    HorariOoperacion VARCHAR2(175 CHAR),
    IsActive         CHAR(1)
);

ALTER TABLE Sucursal ADD CONSTRAINT sucursal_pk PRIMARY KEY ( SucursalId );

ALTER TABLE PanReceta
    ADD CONSTRAINT pan_receta_pan_fk FOREIGN KEY ( PanId )
        REFERENCES Pan ( PanId );

ALTER TABLE PanReceta
    ADD CONSTRAINT pan_receta_receta_fk FOREIGN KEY ( RecetaId )
        REFERENCES receta ( RecetaId );

ALTER TABLE PedidoPan
    ADD CONSTRAINT pedido_pan_pan_fk FOREIGN KEY ( PanId )
        REFERENCES Pan ( PanId );

ALTER TABLE PedidoPan
    ADD CONSTRAINT pedido_pan_pedido_fk FOREIGN KEY ( PedidoId,
                                                      SucursalId )
        REFERENCES Pedido ( PedidoId,
                            SucursalId );

ALTER TABLE RecetaIngrediente
    ADD CONSTRAINT receta_ingrediente_ingrediente_fk FOREIGN KEY ( IngredienteId )
        REFERENCES Ingrediente ( IngredienteId );

ALTER TABLE RecetaIngrediente
    ADD CONSTRAINT receta_ingrediente_receta_fk FOREIGN KEY ( RecetaId )
        REFERENCES Receta ( RecetaId );

ALTER TABLE Pedido
    ADD CONSTRAINT SucursalPedido FOREIGN KEY ( SucursalId )
        REFERENCES Sucursal ( SucursalId );



-- Oracle SQL Developer Data Modeler Summary Report: 
-- 
-- CREATE TABLE                             8
-- CREATE INDEX                             0
-- ALTER TABLE                             15
-- CREATE VIEW                              0
-- ALTER VIEW                               0
-- CREATE PACKAGE                           0
-- CREATE PACKAGE BODY                      0
-- CREATE PROCEDURE                         0
-- CREATE FUNCTION                          0
-- CREATE TRIGGER                           0
-- ALTER TRIGGER                            0
-- CREATE COLLECTION TYPE                   0
-- CREATE STRUCTURED TYPE                   0
-- CREATE STRUCTURED TYPE BODY              0
-- CREATE CLUSTER                           0
-- CREATE CONTEXT                           0
-- CREATE DATABASE                          0
-- CREATE DIMENSION                         0
-- CREATE DIRECTORY                         0
-- CREATE DISK GROUP                        0
-- CREATE ROLE                              0
-- CREATE ROLLBACK SEGMENT                  0
-- CREATE SEQUENCE                          0
-- CREATE MATERIALIZED VIEW                 0
-- CREATE MATERIALIZED VIEW LOG             0
-- CREATE SYNONYM                           0
-- CREATE TABLESPACE                        0
-- CREATE USER                              0
-- 
-- DROP TABLESPACE                          0
-- DROP DATABASE                            0
-- 
-- REDACTION POLICY                         0
-- 
-- ORDS DROP SCHEMA                         0
-- ORDS ENABLE SCHEMA                       0
-- ORDS ENABLE OBJECT                       0
-- 
-- ERRORS                                   0
-- WARNINGS                                 0

