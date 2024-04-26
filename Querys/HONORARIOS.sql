CREATE PROCEDURE sp_OcuparControlADS
AS
DECLARE @fechaControl nvarchar(10)
SET @fechaControl = (CONVERT(VARCHAR(2),MONTH(GETDATE()))) + '/' + DATENAME(YEAR, GETDATE())
PRINT @fechaControl
SELECT numdoc_zv FROM Cg3000..Cgzonval WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD' AND ocupado = 0

IF((SELECT numdoc_zv FROM Cg3000..Cgzonval WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD' AND ocupado = 0) > 0)
BEGIN
	UPDATE Cg3000..Cgzonval SET ocupado = 1 WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD'
END
GO
------------------------------------------------

alter PROCEDURE sp_HonorarioCgDetmae
@tipdoc nvarchar(2),
@numdoc float,
@linea smallint,
@año nvarchar(4),
@fechatran datetime,
@codzona int,
@codloc float,
@codcue_cp nvarchar(1),
@cuenta_pc nvarchar(6),
@subcta_pc nvarchar(3),
@codpre_pc nvarchar(10),
@codigo_c float,
@nocomp nvarchar(20),
@beneficiario nvarchar(255),
@debe float,
@haber float,
@comentario nvarchar(255),
@movbanc nvarchar(5)
AS
BEGIN
DECLARE @fecha NVARCHAR(10)
SET @fecha = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)

INSERT INTO Cg3000..Cgdetmae VALUES(@tipdoc, @numdoc, @linea, @año, @fechatran, @codzona, @codloc, @codcue_cp, @cuenta_pc,
@subcta_pc, @codpre_pc, @codigo_c, @nocomp, 0, @beneficiario, @debe, @haber, @comentario, @movbanc, @fechatran, @fecha,
@fecha, NULL, 0, 0, 'N', 'N', NULL, 0, 'N', NULL, 0, '02', '01', '0', @fecha, '0', 0, 0, 0, 0, NULL, 'N', 0, 0, @fechatran,
NULL, 0, 0, NULL)
END
GO
---------------------------------------------------------------------------------
alter PROCEDURE sp_HonorariosCgCabMae
@usuario float,
@numdoc float,
@observacion nvarchar(255),
@valor float,
@beneficiario nvarchar(255),
@hom_codigo bigint,
@fechatran datetime
AS
BEGIN
DECLARE @fecha NVARCHAR(10)
SET @fecha = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)
INSERT INTO Cg3000..Cgcabmae VALUES (@usuario, 1, 'AD', @numdoc, DATENAME(year, @fechatran), @fechatran, @fechatran,
@observacion, @valor, @valor, @fecha, @fecha, @beneficiario, NULL, 0, 'N', NULL, 0, 'NINGUNO', 'NINGUNO', 'NINGUNO', @hom_codigo)
END
GO
--------------------------------------------------------------------------------

ALTER PROCEDURE sp_HonorarioCxp
@codigo_c float,
@numasi float,
@usuario float,
@nocomp nvarchar(100),
@valor float,
@numlinea int,
@forpag int,
@despag nvarchar(50),
@hom_codigo bigint,
@fechatran datetime
AS
BEGIN
DECLARE @fecha nvarchar(50)
SET @fecha = DATENAME(year,@fechatran) + '-' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' + DATENAME(day,@fechatran)
INSERT INTO Cg3000..CgCuentasXPagar VALUES(@codigo_c, 1, 'AD', @numasi, @fecha, @fecha, @usuario, @nocomp, @nocomp,
@fecha, '210101-005', 0, @valor, 'A', @fecha, @usuario, @numlinea, 'N', NULL, NULL, NULL, NULL, 0, 0, @forpag, 1, @despag, 0, 0,@hom_codigo)
END
GO
----------------------------------------------------------------

CREATE PROCEDURE sp_LiberarControlADS
AS
DECLARE @fechaControl nvarchar(10)
SET @fechaControl = (CONVERT(VARCHAR(2),MONTH(GETDATE()))) + '/' + DATENAME(YEAR, GETDATE())
UPDATE Cg3000..Cgzonval SET ocupado = 0, numdoc_zv = numdoc_zv + 1 WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD'
GO
--------------------------------------------------------------

CREATE PROCEDURE sp_HonorarioLiberado
@hom_codigo bigint
AS
UPDATE His3000..HONORARIOS_MEDICOS_DATOSADICIONALES SET GENERADO_ASIENTO = 0 WHERE HOM_CODIGO = @hom_codigo
GO

--------------------------------------------------------------------

alter PROCEDURE sp_HonorarioImpresionAsiento
@hom_codigo BIGINT,
@parametro int
AS
if(@parametro = 0)
BEGIN
	SELECT codrespon, c.beneficiario, c.fechatran, c.observacion,
	d.numdoc, d.codpre_pc AS CODIGO, p.nomcue_pc, d.codigo_c, d.nocomp, d.debe, d.haber FROM Cg3000..Cgcabmae c
	inner join Cg3000..Cgdetmae d on c.numdoc = d.numdoc
	inner join Cg3000..Cgplacue p on d.codpre_pc = p.codpre_pc
	WHERE HOM_CODIGO = @hom_codigo AND d.tipdoc = 'AD'
END
ELSE
BEGIN
DECLARE @numasi float
	SET @numasi = (SELECT numdoc FROM Cg3000..Cgcabmae WHERE HOM_CODIGO = @hom_codigo)
	SELECT d.numdoc, d.codpre_pc AS CODIGO, p.nomcue_pc, d.codigo_c, d.nocomp, d.debe, d.haber FROM Cg3000..Cgdetmae d
	inner join Cg3000..Cgplacue p on d.codpre_pc = p.codpre_pc
	WHERE numdoc = @numasi AND tipdoc = 'AD'
END
GO


ALTER PROCEDURE sp_Medico
AS
SELECT M.MED_CODIGO AS CODIGO, CASE WHEN (ISNULL((M.MED_CODIGO_LIBRO), 0)) = 0 THEN 'NO' ELSE 'SI' END AS DEPURADO, 
CASE WHEN (M.MED_ESTADO) = 0 THEN 'NO' ELSE 'SI' END AS ACTIVO,
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,
EM.ESP_NOMBRE AS ESPECIALIDAD, M.MED_RUC AS 'RUC/CEDULA', M.MED_EMAIL AS EMAIL, M.MED_TELEFONO_CELULAR AS CELULAR,
TM.TIM_NOMBRE AS 'TIPO MEDICO', M.MED_DIRECCION_CONSULTORIO AS OBSERVACION, M.MED_CON_TRANSFERENCIA, M.MED_DIRECCION AS 'DIRECCION'
FROM MEDICOS M
INNER JOIN ESPECIALIDADES_MEDICAS EM ON M.ESP_CODIGO = EM.ESP_CODIGO
INNER JOIN TIPO_MEDICO TM ON M.TIM_CODIGO = TM.TIM_CODIGO
ORDER BY M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 ASC
GO
SELECT MED_CODIGO_LIBRO FROM MEDICOS
