-- CREATE PROCEDURE sp_OcuparControlADS  
-- @fechaAsiento datetime  
-- AS    
-- --DECLARE @fechaAsiento datetime  
-- --set @fechaAsiento = '27-12-2021'  
-- DECLARE @fechaControl nvarchar(10)    
-- DECLARE @MES NVARCHAR(2)  
-- SET @MES = (CONVERT(VARCHAR(2),MONTH(@fechaAsiento)))  
-- if (len(@MES) = 1)  
-- BEGIN   
 -- SET @fechaControl = '0' + (CONVERT(VARCHAR(2),MONTH(@fechaAsiento))) + '/' + DATENAME(YEAR, @fechaAsiento)    
-- END  
-- ELSE  
-- BEGIN  
 -- SET @fechaControl = (CONVERT(VARCHAR(2),MONTH(@fechaAsiento))) + '/' + DATENAME(YEAR, @fechaAsiento)    
-- END  
-- PRINT @fechaControl    
-- SELECT numdoc_zv FROM Cg3000..Cgzonval WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD' AND ocupado = 0    
    
-- IF((SELECT numdoc_zv FROM Cg3000..Cgzonval WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD' AND ocupado = 0) > 0)    
-- BEGIN    
 -- UPDATE Cg3000..Cgzonval SET ocupado = 1 WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD'    
-- END   
-- ------------------------------------------------------------------

-- alter PROCEDURE sp_LiberarControlADS     
-- @fechaAsiento datetime  
-- AS    
-- --DECLARE @fechaAsiento datetime  
-- --set @fechaAsiento = '27-12-2021'  
-- DECLARE @fechaControl nvarchar(10)    
-- DECLARE @MES NVARCHAR(2)  
-- SET @MES = (CONVERT(VARCHAR(2),MONTH(@fechaAsiento)))  
-- IF(LEN(@MES) = 1)  
-- BEGIN  
 -- SET @fechaControl = '0' + (CONVERT(VARCHAR(2),MONTH(@fechaAsiento))) + '/' + DATENAME(YEAR, @fechaAsiento)    
-- END  
-- ELSE  
-- BEGIN  
 -- SET @fechaControl = (CONVERT(VARCHAR(2),MONTH(@fechaAsiento))) + '/' + DATENAME(YEAR, @fechaAsiento)    
-- END  
-- UPDATE Cg3000..Cgzonval SET ocupado = 0, numdoc_zv = numdoc_zv + 1 WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD'   

-- -------------------------------------------------------------------

-- ALTER PROCEDURE sp_HonorariosCgCabMae  
-- @usuario float,  
-- @numdoc float,  
-- @observacion nvarchar(255),  
-- @valor float,  
-- @beneficiario nvarchar(255),  
-- @hom_codigo bigint,  
-- @fechatran datetime  
-- AS  
-- BEGIN  
-- DECLARE @fecha NVARCHAR(10)  
-- DECLARE @MES NVARCHAR(2)
-- DECLARE @DIA NVARCHAR(2)
-- SET @MES = CONVERT(VARCHAR(2),MONTH(@fechatran))
-- SET @DIA = DATENAME(day, @fechatran)
-- IF(LEN(@MES) = 1)
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha = DATENAME(year, @fechatran) + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '0' + DATENAME(day, @fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha = DATENAME(year, @fechatran) + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)
	-- END
-- END
-- ELSE
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '0' + DATENAME(day, @fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)
	-- END
-- END
-- INSERT INTO Cg3000..Cgcabmae VALUES (@usuario, 1, 'AD', @numdoc, DATENAME(year, @fechatran), @fechatran, @fechatran,  
-- @observacion, @valor, @valor, @fecha, @fecha, @beneficiario, NULL, 0, 'N', NULL, 0, 'NINGUNO', 'NINGUNO', 'NINGUNO', @hom_codigo)  
-- END  

-- ----------------------------------------------------------------------------------------------------------

-- ALTER PROCEDURE sp_HonorarioCgDetmae  
-- @tipdoc nvarchar(2),  
-- @numdoc float,  
-- @linea smallint,  
-- @año nvarchar(4),  
-- @fechatran datetime,  
-- @codzona int,  
-- @codloc float,  
-- @codcue_cp nvarchar(1),  
-- @cuenta_pc nvarchar(6),  
-- @subcta_pc nvarchar(3),  
-- @codpre_pc nvarchar(10),  
-- @codigo_c float,  
-- @nocomp nvarchar(20),  
-- @beneficiario nvarchar(255),  
-- @debe float,  
-- @haber float,  
-- @comentario nvarchar(255),  
-- @movbanc nvarchar(5)  
-- AS  
-- BEGIN  
-- DECLARE @fecha NVARCHAR(10) 
-- DECLARE @MES NVARCHAR(2)
-- DECLARE @DIA NVARCHAR(2)
-- SET @MES = CONVERT(VARCHAR(2),MONTH(@fechatran))
-- SET @DIA = DATENAME(day, @fechatran)
-- IF(LEN(@MES) = 1)
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha = DATENAME(year, @fechatran) + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '0' + DATENAME(day, @fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha = DATENAME(year, @fechatran) + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)
	-- END
-- END
-- ELSE
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '0' + DATENAME(day, @fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)
	-- END
-- END 
-- INSERT INTO Cg3000..Cgdetmae VALUES(@tipdoc, @numdoc, @linea, @año, @fechatran, @codzona, @codloc, @codcue_cp, @cuenta_pc,  
-- @subcta_pc, @codpre_pc, @codigo_c, @nocomp, 0, @beneficiario, @debe, @haber, @comentario, @movbanc, @fechatran, @fecha,  
-- @fecha, NULL, 0, 0, 'N', 'N', NULL, 0, 'N', NULL, 0, '02', '01', '0', @fecha, '0', 0, 0, 0, 0, NULL, 'N', 0, 0, @fechatran,  
-- NULL, 0, 0, NULL)  
-- END  

-- -----------------------------------------------------------------------------------------------------------------------------
-- ALTER PROCEDURE sp_HonorarioCxp  
-- @codigo_c float,  
-- @numasi float,  
-- @usuario float,  
-- @nocomp nvarchar(100),  
-- @valor float,  
-- @numlinea int,  
-- @forpag int,  
-- @despag nvarchar(50),  
-- @hom_codigo bigint,  
-- @fechatran datetime  
-- AS  
-- BEGIN  
-- DECLARE @fecha nvarchar(50)  
-- DECLARE @MES NVARCHAR(2)
-- DECLARE @DIA NVARCHAR(2)
-- SET @MES = CONVERT(VARCHAR(2),MONTH(@fechatran))
-- SET @DIA = DATENAME(day, @fechatran)
-- IF(LEN(@MES) = 1)
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha = DATENAME(year,@fechatran) + '-' + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' + '0' +DATENAME(day,@fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha = DATENAME(year,@fechatran) + '-' + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' +DATENAME(day,@fechatran)
	-- END
-- END
-- ELSE
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha = DATENAME(year,@fechatran) + '-' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' + '0' +DATENAME(day,@fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha = DATENAME(year,@fechatran) + '-' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' +DATENAME(day,@fechatran)
	-- END
-- END
-- INSERT INTO Cg3000..CgCuentasXPagar VALUES(@codigo_c, 1, 'AD', @numasi, @fecha, @fecha, @usuario, @nocomp, @nocomp,  
-- @fecha, '210101-005', 0, @valor, 'A', @fecha, @usuario, @numlinea, 'N', NULL, NULL, NULL, NULL, 0, 0, @forpag, 1, @despag, 0, 0,@hom_codigo)  
-- END  

-- ---------------------------------------------------------------------------------------------

-- ALTER PROCEDURE sp_HonorarioCxC_EC  
-- @hom_codigo bigint,  
-- @forpag float,  
-- @fechatran datetime  
-- AS  
-- BEGIN  
-- --VARIABLES  
-- DECLARE @codigo_s float  
-- SET @codigo_s = (SELECT codclisic FROM Sic3000..Forma_Pago WHERE forpag = @forpag AND claspag NOT IN (2,5))  
-- DECLARE @claspag nvarchar(3)  
-- SET @claspag = (SELECT claspag FROM Sic3000..Forma_Pago WHERE forpag = @forpag AND claspag NOT IN (2,5))  
-- DECLARE @fecha DATETIME 
-- DECLARE @MES NVARCHAR(2)
-- DECLARE @DIA NVARCHAR(2)
-- SET @MES = CONVERT(VARCHAR(2),MONTH(@fechatran))
-- SET @DIA = DATENAME(day, @fechatran)
-- SET @fecha = @fechatran  
-- DECLARE @fecha1 nvarchar(8)
-- IF(LEN(@MES) = 1)
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha1 = DATENAME(year, @fechatran) + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '0' + DATENAME(day, @fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha1 = DATENAME(year, @fechatran) + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)
	-- END
-- END
-- ELSE
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha1 = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '0' + DATENAME(day, @fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha1 = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)
	-- END
-- END 
-- DECLARE @idEC smallint  
-- SET @idEC = (SELECT MAX(id) + 1 FROM Sic3000..EstadoCuentas)  
-- DECLARE @fecha2 nvarchar(10)
-- IF(LEN(@MES) = 1)
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha2 = DATENAME(year, @fechatran) + '/0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '/0' + DATENAME(day, @fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha2 = DATENAME(year, @fechatran) + '/0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '/' + DATENAME(day, @fechatran)
	-- END
-- END
-- ELSE
-- BEGIN
	-- IF(LEN(@DIA) = 1)
	-- BEGIN
		-- SET @fecha2 = DATENAME(year, @fechatran) + '/' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '/0' + DATENAME(day, @fechatran)
	-- END
	-- ELSE
	-- BEGIN
		-- SET @fecha2 = DATENAME(year, @fechatran) + '/' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '/' + DATENAME(day, @fechatran)
	-- END
-- END 
-- DECLARE @ate_numero nvarchar(5)  
-- DECLARE @paciente nvarchar(100)  
-- DECLARE @hc nvarchar(10)  
-- DECLARE @caja nvarchar(3)  
-- DECLARE @valor float  
-- DECLARE @ate_codigo BIGINT  
-- SET @ate_codigo = (SELECT ATE_CODIGO FROM His3000..HONORARIOS_MEDICOS WHERE HOM_CODIGO = @hom_codigo)  
-- SET @ate_numero = (SELECT ATE_NUMERO_ATENCION FROM His3000..ATENCIONES WHERE ATE_CODIGO = @ate_codigo)  
-- SET @paciente = (SELECT PAC_APELLIDO_PATERNO + ' ' + PAC_APELLIDO_MATERNO + ' ' + PAC_NOMBRE1 + ' ' + PAC_NOMBRE2   
-- FROM His3000..PACIENTES WHERE PAC_CODIGO = (SELECT PAC_CODIGO FROM His3000..ATENCIONES WHERE ATE_CODIGO = @ate_codigo))  
-- SET @hc = (SELECT PAC_HISTORIA_CLINICA FROM His3000..PACIENTES WHERE PAC_CODIGO = (SELECT PAC_CODIGO FROM His3000..ATENCIONES WHERE ATE_CODIGO = @ate_codigo))  
-- SET @caja = (SELECT caja FROM His3000..HONORARIOS_MEDICOS_DATOSADICIONALES WHERE HOM_CODIGO = @hom_codigo)  
-- SET @valor = (SELECT (ISNULL(HOM_VALOR_NETO,0)-(ISNULL(VALOR,0)+ISNULL(HOM_RECORTE,0)+ ISNULL(HOM_VALOR_CANCELADO,0))) FROM His3000..HONORARIOS_MEDICOS WHERE HOM_CODIGO = @hom_codigo)  
-- DECLARE @numfac nvarchar(15)  
-- SET @numfac = CONVERT(nvarchar, @hom_codigo)  
  
-- IF(@codigo_s >= 0)  
-- BEGIN  
  
 -- INSERT INTO Sic3000..EstadoCuentas VALUES(@idEC, @numfac, 'HONORARIOS', 'H', @numfac,  
 -- Convert(nvarchar(10), @codigo_s), @fecha2, 'HONORARIO No. ' + @numfac + ' PACIENTE: ' + @paciente + ' ATENCION: ' + @ate_numero + ' HISTORIA CLINICA: ' + @hc,  
 -- @valor, 0, @valor, @fecha1, Convert(nvarchar(10), @forpag), @claspag, @caja, @fechatran)  
  
 -- INSERT INTO Sic3000..CxC VALUES(Convert(nvarchar(10),@codigo_s), Convert(nvarchar(20), @hom_codigo),   
 -- @fecha, 'H', Convert(nvarchar(20),@valor), '0', Convert(nvarchar(20), @valor), @fecha1, @fecha, NULL, @fecha,   
 -- Convert(nvarchar(10),@forpag), Convert(nvarchar(10), @claspag), @fecha1, 1, null)  
-- END  
-- END  
-- ----------------------------------------------------------------------------
 -- ALTER PROCEDURE sp_HonorariosAnulacionTotal
  -- @hom_codigo bigint,
  -- @usuario int
  -- AS
  -- DECLARE @NUMASI FLOAT
  -- SET @NUMASI = (SELECT numdoc FROM Cg3000..Cgcabmae WHERE HOM_CODIGO = @hom_codigo AND tipdoc = 'AD') 
  -- DECLARE @CODCLI BIGINT
  -- SET @CODCLI = (SELECT TOP 1 codigo_c FROM Cg3000..Cgdetmae WHERE numdoc = @NUMASI  AND tipdoc = 'AD')
  -- DECLARE @VALOR FLOAT
  -- SET @VALOR = (SELECT totdebe FROM Cg3000..Cgcabmae WHERE HOM_CODIGO = @hom_codigo AND tipdoc = 'AD' )
  -- DELETE FROM Cg3000..Cgdetmae WHERE numdoc = @NUMASI AND tipdoc = 'AD'
  -- DELETE FROM Cg3000..Cgcabmae WHERE numdoc = @NUMASI AND tipdoc = 'AD'
  -- UPDATE Cg3000..CgCuentasXPagar SET estado = 'E' WHERE numasi = @NUMASI AND tipasi = 'AD'
  -- DELETE FROM Sic3000..EstadoCuentas WHERE numfac = CONVERT(NVARCHAR, @hom_codigo) AND iddoc = 'H'
  -- DELETE FROM Sic3000..CxC WHERE numdoc = CONVERT(NVARCHAR, @hom_codigo) AND tipo = 'H'

  -- UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET GENERADO_ASIENTO = 0 WHERE HOM_CODIGO = @hom_codigo
  -- INSERT INTO SERIES3000_AUDITORIA..CG_AUDTORIA VALUES
  -- ('AD', CONVERT(BIGINT, @NUMASI), @CODCLI, @usuario, @VALOR, @VALOR, '', '', @hom_codigo, 'ANULADO DESDE EL HIS3000', GETDATE())
  -- GO
  -- ----------------------------------------------------------------
  -- ALTER PROCEDURE sp_HonorarioValidarAnulacion  
-- @hom_codigo bigint,  
-- @codcli nvarchar(10),  
-- @nocomp nvarchar(20)  
-- AS  
-- DECLARE @usuario FLOAT  
-- SET @usuario = (SELECT usuarioing FROM Cg3000..CgCuentasXPagar WHERE nocomp = @nocomp AND codigo_c = @codcli and estadopago <> 'P' and estado = 'A')  
  
  
-- SELECT saldo  AS VALOR, 'TIPO' = 'CXC', 'USUARIO' = @usuario FROM Sic3000..CxC WHERE numdoc = CONVERT(nvarchar(10), @hom_codigo) --SI ES CERO ESTA PAGADO Y NO PUEDE ANULAR  
-- UNION  
-- SELECT (SUM(debe) - SUM(haber)) AS VALOR, 'TIPO' = 'EC',  'USUARIO' = @usuario FROM Sic3000..EstadoCuentas WHERE numfac = CONVERT(nvarchar(10), @hom_codigo)  
-- UNION  
-- SELECT isnull((SUM(debe) - SUM(haber)), 0) AS VALOR, 'TIPO' = 'CXP',  'USUARIO' = @usuario  FROM Cg3000..CgCuentasXPagar WHERE nocomp = @nocomp AND codigo_c = @codcli and estadopago <> 'P' AND estado = 'A' 
-- -------------------------------------------------------------------------------------------------------------
-- ALTER PROCEDURE sp_AuditoriaCg
-- @beneficiario nvarchar(255),
-- @usuario int,
-- @observacion nvarchar(500),
-- @hom_codigo bigint
-- AS
  -- DECLARE @NUMASI FLOAT
  -- SET @NUMASI = (SELECT numdoc FROM Cg3000..Cgcabmae WHERE HOM_CODIGO = @hom_codigo AND tipdoc = 'AD') 
  -- DECLARE @CODCLI BIGINT
  -- SET @CODCLI = (SELECT TOP 1 codigo_c FROM Cg3000..Cgdetmae WHERE numdoc = @NUMASI  AND tipdoc = 'AD')
  -- DECLARE @VALOR FLOAT
  -- SET @VALOR = (SELECT totdebe FROM Cg3000..Cgcabmae WHERE HOM_CODIGO = @hom_codigo AND tipdoc = 'AD' )
  -- INSERT INTO SERIES3000_AUDITORIA..CG_AUDTORIA VALUES
  -- ('AD', CONVERT(BIGINT, @NUMASI), @CODCLI, @usuario, @VALOR, @VALOR, @observacion, @beneficiario, @hom_codigo,
  -- 'GENERA AD EN EL HIS3000', GETDATE())
  -- GO
  
  -- -------------------------------------------------------------------------------------
  -- USE [Sic3000]
-- GO

-- /****** Object:  View [dbo].[vista_VERSOBRANTES]    Script Date: 04/01/2022 9:47:38 ******/
-- SET ANSI_NULLS ON
-- GO

-- SET QUOTED_IDENTIFIER ON
-- GO



-- ALTER VIEW [dbo].[vista_VERSOBRANTES]
-- AS
-- SELECT CXC.numdoc AS 'No_DOCUMENTO', C.nomcli AS 'CLIENTE', 
-- ISNULL(ISNULL((HM.PACIENTE), (A.nombre)), (FDA.Nombres)) AS PACIENTE, 
-- tipo AS TIPO,  cast(debe as float) AS DEBE, 
-- cast(haber as float) AS HABER, ROUND((cast(debe as float)- cast(haber as float)),2) AS SALDO, CXC.fecha AS FECHA,
-- F.despag AS 'FORMA_PAGO', CL.desclas AS CLASIFICACION, F.forpag, CXC.fila, CXC.codcli
-- FROM CxC cxc
-- INNER JOIN Cliente C ON cxc.codcli = C.codcli
-- INNER JOIN Forma_Pago F ON cxc.forpag = F.forpag
-- INNER JOIN Clasificacion CL ON cxc.claspag = CL.codclas
-- LEFT JOIN HonorariosMedico HM ON CXC.numdoc = CONVERT(nvarchar, CODIGO) AND tipo = 'H'
-- LEFT JOIN Anticipo A ON CXC.numdoc = A.numrec AND CXC.tipo = 'A'
-- LEFT JOIN FacturaDatosAdicionales FDA ON CXC.numdoc = FDA.Numdoc
-- GO
-----------------------------------------------------------------------------------------------------
USE [Sic3000]
GO

/****** Object:  Table [dbo].[Ajustes]    Script Date: 07/01/2022 10:55:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ajustes](
	[numpag] [nvarchar](10) NOT NULL,
	[tipo] [nvarchar](1) NOT NULL,
	[codcli] [nvarchar](10) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[numdoc] [nvarchar](13) NOT NULL,
	[pago] [float] NULL,
	[obs] [nvarchar](1) NULL,
	[pagado] [nvarchar](20) NULL,
	[reten_iva] [bit] NOT NULL,
	[valor_ri] [nvarchar](20) NULL,
	[reten_fuente] [bit] NOT NULL,
	[valor_rf] [nvarchar](20) NULL,
	[pagos] [ntext] NULL,
	[fecha1] [nvarchar](8) NULL,
	[codresp] [float] NULL,
	[consec] [nchar](10) NULL,
	[caja] [nchar](10) NULL,
	[forpag] [varchar](10) NULL,
	[numliquidacion] [bigint] NULL,
	[asicon] [nvarchar](15) NULL,
	[comentario] [nvarchar](250) NULL,
	[Id_Ajustes] [int] IDENTITY(1,1) NOT NULL,
	[fila_cxc] [float] NULL,
	[Arqueada] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Ajustes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Ajustes] ADD  DEFAULT ((0.00)) FOR [fila_cxc]
GO


-------------------------------------------------------------------------------------
USE [Sic3000]
GO

/****** Object:  Table [dbo].[DetalleAjustes]    Script Date: 07/01/2022 10:56:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DetalleAjustes](
	[numpag] [nvarchar](10) NOT NULL,
	[forpag] [nvarchar](10) NOT NULL,
	[id] [nvarchar](3) NOT NULL,
	[despag] [nvarchar](255) NULL,
	[monto] [float] NULL,
	[auxiliarcont] [float] NULL,
	[facturaprov] [nvarchar](50) NULL,
	[autorizacion] [nvarchar](50) NULL,
	[fechaautoriza] [nvarchar](50) NULL,
	[banco] [nvarchar](100) NULL,
	[numCT] [nvarchar](50) NULL,
	[propietario] [nvarchar](150) NULL,
	[Id_Ajustes] [int] NULL,
	[Observacion] [nvarchar](max) NULL,
	[LoteRecap] [nvarchar](50) NULL,
	[NroDocumento] [nvarchar](50) NULL,
	[FechaEmision] [date] NULL,
 CONSTRAINT [PK_DetalleAjustes] PRIMARY KEY CLUSTERED 
(
	[numpag] ASC,
	[forpag] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[DetalleAjustes] ADD  DEFAULT ((0)) FOR [auxiliarcont]
GO

ALTER TABLE [dbo].[DetalleAjustes]  WITH CHECK ADD  CONSTRAINT [FK_Ajustes_DetalleAjuste] FOREIGN KEY([Id_Ajustes])
REFERENCES [dbo].[Ajustes] ([Id_Ajustes])
GO

ALTER TABLE [dbo].[DetalleAjustes] CHECK CONSTRAINT [FK_Ajustes_DetalleAjuste]
GO

---------------------------------------------------------------------------------





