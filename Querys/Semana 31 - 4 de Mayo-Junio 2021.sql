---TODOS


-- ALTER TABLE HC_INTERCONSULTA
-- ADD HIN_FECHACREACION DATETIME NOT NULL DEFAULT GETDATE()


USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_QuirofanoPacienteProcedimiento]    Script Date: 07/06/2021 15:08:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_QuirofanoPacienteProcedimiento]
@orden int,
@cie_codigo bigint,
@codpro varchar(13),
@cantidad int,
@paciente int,
@atencion int,
@usada int,
@usuario varchar(100)
AS
DECLARE @fecha DATETIME
SET @fecha = GETDATE(); 
	INSERT INTO QUIROFANO_PROCE_PRODU VALUES(@orden, @cie_codigo, @codpro, @cantidad, @paciente, @atencion, @fecha, 0, @usuario, 0,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
go

USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_QuirofanoNumero]    Script Date: 07/06/2021 16:39:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_QuirofanoNumero]
@atencion int,
@codigo int
AS
SELECT COUNT(QPP_CODIGO) AS Valor FROM QUIROFANO_PROCE_PRODU WHERE ATE_CODIGO = @atencion AND PCI_CODIGO = @codigo




--FIN TODOS



--SOLO ALIANZA




--FIN ALIANZA



--SOLO PASTEUR


--FIN PASTEUR




-- USE [His3000]
-- GO

-- /****** Object:  Table [dbo].[PACIENTES_JIRE]    Script Date: 01/06/2021 09:26:09 ******/
-- SET ANSI_NULLS ON
-- GO

-- SET QUOTED_IDENTIFIER ON
-- GO

-- CREATE TABLE [dbo].[PACIENTES_JIRE](
	-- [cedula] [varchar](20) NULL,
	-- [hc] [varchar](10) NULL,
	-- [apellido1] [varchar](20) NULL,
	-- [apellido2] [varchar](20) NULL,
	-- [nombre1] [varchar](20) NULL,
	-- [nombre2] [varchar](20) NULL,
	-- [direccion] [varchar](200) NULL,
	-- [fechaNacimiento] [date] NULL,
	-- [telefono] [varchar](20) NULL,
	-- [email] [varchar](100) NULL,
	-- [codigo] [varchar](20) NULL
-- ) ON [PRIMARY]
-- GO


-- USE [His3000]
-- GO
-- /****** Object:  StoredProcedure [dbo].[sp_QuirofanoCuentaPacientes]    Script Date: 03/06/2021 20:49:23 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- create PROCEDURE [dbo].[sp_QuirofanoCuentaPacientes]
-- @ate_codigo bigint,
-- @codpro varchar(15),
-- @cue_detalle varchar(500),
-- @cue_valor float,
-- @cue_cantidad float,
-- @cue_total float,
-- @cue_iva float,
-- @rub_codigo int,
-- @id_usuario int,
-- @codigo_pedido bigint,
-- @costo float
-- AS
-- INSERT INTO CUENTAS_PACIENTES VALUES((SELECT MAX(CUE_CODIGO) + 1 FROM CUENTAS_PACIENTES), @ate_codigo, 
-- GETDATE(), @codpro, @cue_detalle, @cue_valor, @cue_cantidad, @cue_total, @cue_iva, 1, '0', @rub_codigo,
-- 1,@id_usuario, 0, @codpro, NULL, 'PEDIDO GENERADO POR QUIROFANO', 0, NULL, @codigo_pedido, NULL, @costo, NULL, 
-- 'N', 0, 0, 0, GETDATE())




-- USE [His3000]
-- GO
-- /****** Object:  StoredProcedure [dbo].[sp_QuirofanoAgregarPedidoProducto]    Script Date: 03/06/2021 21:47:57 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- ALTER PROCEDURE [dbo].[sp_QuirofanoAgregarPedidoProducto]
-- @codpro varchar(15),
-- @prodesc varchar(500),
-- @cantidad float,
-- @valor float,
-- @total float,
-- @ped_codigo int, 
-- @iva float
-- AS
-- DECLARE @detalle int
-- SET @detalle = (SELECT MAX(PDD_CODIGO) FROM PEDIDOS_DETALLE)
-- INSERT INTO PEDIDOS_DETALLE VALUES(@detalle + 1, @ped_codigo, @codpro, @prodesc, @cantidad, @valor, @iva, @total
-- ,1, 0, NULL, NULL, NULL, NULL, @codpro)



-- USE [His3000]
-- GO
-- /****** Object:  StoredProcedure [dbo].[sp_QuirofanoMostrarProcedimientoPaciente]    Script Date: 06/06/2021 17:38:11 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- ALTER PROCEDURE [dbo].[sp_QuirofanoMostrarProcedimientoPaciente]
-- @cie_codigo bigint,
-- @atencion int
-- AS
-- IF EXISTS (SELECT * FROM QUIROFANO_PROCE_PRODU WHERE PCI_CODIGO = @cie_codigo AND ATE_CODIGO = @atencion AND QPP_ORDEN IS NOT NULL)
-- BEGIN
	-- SELECT QPP.CODPRO AS Codigo, P.despro AS Producto, QP.QP_GRUPO AS Grupo,
	-- B.existe AS Stock, QPP.QPP_CANTIDAD AS 'Cant. Original', 
	-- QPP.QPP_CANT_ADICIONAL AS 'Cant. Adicional', QPP.QPP_CANTIDAD_ORIGINAL AS 'Cant. Devolución',
	-- QPP.QPP_FECHA AS Fecha, QPP.QPP_ORDEN, QPP_USUARIO as Usuario, p.preven
	-- FROM QUIROFANO_PROCE_PRODU QPP
	-- INNER JOIN QUIROFANO_PRODUCTOS QP ON QPP.CODPRO = QP.CODPRO
	-- INNER JOIN Sic3000.dbo.Producto P ON QP.CODPRO = P.codpro
	-- INNER JOIN Sic3000.dbo.Bodega B ON P.codpro = B.codpro
	-- WHERE QPP.PCI_CODIGO = @cie_codigo AND QPP.ATE_CODIGO = @atencion AND B.codbod = 12
-- END
-- ELSE
-- BEGIN
	-- SELECT QPP.CODPRO AS Codigo, P.despro AS Producto, QP.QP_GRUPO AS Grupo,
	-- B.existe AS Stock, QPP.QPP_CANTIDAD AS 'Cant. Original', 
	-- QPP.QPP_CANT_ADICIONAL AS 'Cant. Adicional', QPP.QPP_CANTIDAD_ORIGINAL AS 'Cant. Devolución',
	-- QPP.QPP_FECHA AS Fecha, QPP.QPP_ORDEN, QPP_USUARIO as Usuario, P.preven
	-- FROM QUIROFANO_PROCE_PRODU QPP
	-- INNER JOIN QUIROFANO_PRODUCTOS QP ON QPP.CODPRO = QP.CODPRO
	-- INNER JOIN Sic3000.dbo.Producto P ON QP.CODPRO = P.codpro
	-- INNER JOIN Sic3000.dbo.Bodega B ON P.codpro = B.codpro
	-- WHERE QPP.PCI_CODIGO = @cie_codigo AND QPP.ATE_CODIGO IS NULL AND B.codbod = 12
-- END






-- USE [His3000]
-- GO
-- /****** Object:  StoredProcedure [dbo].[sp_QuirofanoBodega]    Script Date: 06/06/2021 17:08:38 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- ALTER PROCEDURE [dbo].[sp_QuirofanoBodega]
-- @codpro nvarchar(20),
-- @existe float
-- AS
-- UPDATE Sic3000.dbo.Bodega SET existe = existe - @existe WHERE codpro = @codpro AND codbod = 12

-- USE [His3000]
-- GO
-- /****** Object:  StoredProcedure [dbo].[sp_QuirofanoRegistro]    Script Date: 06/06/2021 19:40:57 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- ALTER PROCEDURE [dbo].[sp_QuirofanoRegistro]
-- @ate_codigo int,
-- @pac_codigo int,
-- @cie_codigo bigint
-- AS
-- SELECT hab_Codigo, 

-- (SELECT M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 
-- FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_CIRUJANO
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2),


-- (SELECT M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 
-- FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_AYUDANTE
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2),

-- (SELECT M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 
-- FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_AYUDANTIA
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2)

-- , QPP.TA_CODIGO, QPP_RECUPERACION,

-- (SELECT M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 
-- FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_ANESTESIOLOGO
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2), 

-- QPP_HORAINICIO, 

-- (SELECT U.APELLIDOS + ' ' + U.NOMBRES FROM USUARIOS U
-- INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON U.ID_USUARIO = QPP.QPP_CIRCULANTE
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY U.APELLIDOS + ' ' + U.NOMBRES)

-- , (SELECT U.APELLIDOS + ' ' + U.NOMBRES FROM USUARIOS U
-- INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON U.ID_USUARIO = QPP.QPP_INSTRUMENTISTA
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY U.APELLIDOS + ' ' + U.NOMBRES),

-- (SELECT M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 
-- FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_PATOLOGO
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2), 

-- QPP_ATENCION, C.PCI_DESCRIPCION, QPP_FECHA, QPP_HORAFIN, QPP_DURACION, TA.TA_DESCRIPCION,

-- (SELECT qpp.QPP_CIRUJANO FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_CIRUJANO
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY QPP_CIRUJANO)AS CODCIRUJANO,

-- (SELECT QPP_AYUDANTE FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_AYUDANTE
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY QPP_AYUDANTE)AS CODAYUDANTE,

-- (SELECT QPP_AYUDANTIA FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_AYUDANTIA
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY QPP_AYUDANTIA)AS CODAYUDANTIA,

-- (SELECT QPP_ANESTESIOLOGO FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_ANESTESIOLOGO
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY QPP_ANESTESIOLOGO)AS CODANESTESIOLOGO,


-- (SELECT QPP_CIRCULANTE FROM USUARIOS U
-- INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON U.ID_USUARIO = QPP.QPP_CIRCULANTE
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY QPP_CIRCULANTE) AS CODCIRCULANTE,

-- (SELECT QPP_INSTRUMENTISTA FROM USUARIOS U
-- INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON U.ID_USUARIO = QPP.QPP_INSTRUMENTISTA
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY QPP_INSTRUMENTISTA) AS CODINSTRUMENTISTA,

-- (SELECT QPP_PATOLOGO FROM MEDICOS M INNER JOIN QUIROFANO_PROCE_PRODU  QPP ON M.MED_CODIGO = QPP.QPP_PATOLOGO
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY QPP_PATOLOGO)  AS CODPATOLOGO

-- FROM QUIROFANO_PROCE_PRODU QPP 
-- INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO
-- INNER JOIN TIPO_ANESTESIA TA ON QPP.TA_CODIGO = TA.TA_CODIGO
-- WHERE PAC_CODIGO = @pac_codigo AND ATE_CODIGO = @ate_codigo AND QPP.PCI_CODIGO = @cie_codigo
-- GROUP BY hab_Codigo, QPP.TA_CODIGO, QPP_RECUPERACION, QPP_ATENCION, C.PCI_DESCRIPCION, QPP_HORAINICIO, QPP_FECHA,
-- QPP_HORAFIN, QPP_DURACION, TA.TA_DESCRIPCION




-- --NO ME DEJA CREAR EL PROCEDURE POR QUE EL CAMPO IDKARDEX ES IDENTIFIED
-- CREATE PROCEDURE sp_ControlInventarioQuirofano
-- @codpro varchar(15),
-- @numdoc varchar(20),
-- @tipdoc varchar(5),
-- @id float,
-- @egreso nvarchar(100),
-- @codusu nvarchar(10),
-- @costo float,
-- @costoTotal float,
-- @fechaaux nvarchar(15),
-- @codprv float,
-- @venta float, 
-- @codsec float,
-- @coddep float,
-- @codsub float,
-- @coddiv float,
-- @hc bigint,
-- @ate_codigo bigint
-- AS
-- INSERT INTO Sic3000..Kardex VALUES(@codpro, GETDATE(), @numdoc, @tipdoc, '12', @id, '0', @egreso, 0, 'PEDIDO HIS',
-- @codusu, @costo, @costoTotal, @costo, @fechaaux, @codprv, @venta, 0, @codsec, @coddep, @codsub, @coddiv, @fechaaux, 
-- @costoTotal, NULL, @hc, @ate_codigo, NULL, NULL, NULL, NULL, NULL)
-- GO
