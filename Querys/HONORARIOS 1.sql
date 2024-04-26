ALTER PROCEDURE sp_HonorarioCxC_EC
@hom_codigo bigint,
@forpag float,
@fechatran datetime
AS
BEGIN
--VARIABLES
DECLARE @codigo_s float
SET @codigo_s = (SELECT codclisic FROM Sic3000..Forma_Pago WHERE forpag = @forpag AND claspag NOT IN (2,5))
DECLARE @claspag nvarchar(3)
SET @claspag = (SELECT claspag FROM Sic3000..Forma_Pago WHERE forpag = @forpag AND claspag NOT IN (2,5))
DECLARE @fecha DATETIME
SET @fecha = @fechatran
DECLARE @fecha1 nvarchar(8)
SET @fecha1 = DATENAME(year, @fechatran) + CONVERT(VARCHAR(2),MONTH(@fechatran)) + DATENAME(day, @fechatran)
DECLARE @idEC smallint
SET @idEC = (SELECT MAX(id) + 1 FROM Sic3000..EstadoCuentas)
DECLARE @fecha2 nvarchar(10)
SET @fecha2 = DATENAME(DAY, @fechatran) + '/' +  CONVERT(VARCHAR(2),MONTH(@fechatran)) + '/' + DATENAME(YEAR, @fechatran)
DECLARE @ate_numero nvarchar(5)
DECLARE @paciente nvarchar(100)
DECLARE @hc nvarchar(10)
DECLARE @caja nvarchar(3)
DECLARE @valor float
DECLARE @ate_codigo BIGINT
SET @ate_codigo = (SELECT ATE_CODIGO FROM His3000..HONORARIOS_MEDICOS WHERE HOM_CODIGO = @hom_codigo)
SET @ate_numero = (SELECT ATE_NUMERO_ATENCION FROM His3000..ATENCIONES WHERE ATE_CODIGO = @ate_codigo)
SET @paciente = (SELECT PAC_APELLIDO_PATERNO + ' ' + PAC_APELLIDO_MATERNO + ' ' + PAC_NOMBRE1 + ' ' + PAC_NOMBRE2 
FROM His3000..PACIENTES WHERE PAC_CODIGO = (SELECT PAC_CODIGO FROM His3000..ATENCIONES WHERE ATE_CODIGO = @ate_codigo))
SET @hc = (SELECT PAC_HISTORIA_CLINICA FROM His3000..PACIENTES WHERE PAC_CODIGO = (SELECT PAC_CODIGO FROM His3000..ATENCIONES WHERE ATE_CODIGO = @ate_codigo))
SET @caja = (SELECT caja FROM His3000..HONORARIOS_MEDICOS_DATOSADICIONALES WHERE HOM_CODIGO = @hom_codigo)
SET @valor = (SELECT (ISNULL(HOM_VALOR_NETO,0)-(ISNULL(VALOR,0)+ISNULL(HOM_RECORTE,0)+ ISNULL(HOM_VALOR_CANCELADO,0))) FROM His3000..HONORARIOS_MEDICOS WHERE HOM_CODIGO = @hom_codigo)
DECLARE @numfac nvarchar(15)
SET @numfac = CONVERT(nvarchar, @hom_codigo)

IF(@codigo_s >= 0)
BEGIN

	INSERT INTO Sic3000..EstadoCuentas VALUES(@idEC, @numfac, 'HONORARIOS', 'H', @numfac,
	Convert(nvarchar(10), @codigo_s), @fecha2, 'HONORARIO No. ' + @numfac + ' PACIENTE: ' + @paciente + ' ATENCION: ' + @ate_numero + ' HISTORIA CLINICA: ' + @hc,
	@valor, 0, @valor, @fecha1, Convert(nvarchar(10), @forpag), @claspag, @caja, @fechatran)

	INSERT INTO Sic3000..CxC VALUES(Convert(nvarchar(10),@codigo_s), Convert(nvarchar(20), @hom_codigo), 
	@fecha, 'H', Convert(nvarchar(20),@valor), '0', Convert(nvarchar(20), @valor), @fecha1, @fecha, NULL, @fecha, 
	Convert(nvarchar(10),@forpag), Convert(nvarchar(10), @claspag), @fecha1, 1, null)
END
END
GO


--insert into Sic3000..EstadoCuentas values (7481, '5859', 'HONORARIOS', 'H', '5859', '100028', '18/11/2021', 'HONORORARIO Nº 5859 PACIENTE: EDGAR RAMOS HC: 98907 ATENCION: 4567',
--250, 0, 250, '20211118', '123', '4', '203', @fechatran)



EXEC His3000.dbo.sp_HonorarioCxC_EC 5859, 123

SELECT * FROM Sic3000..CxC WHERE numdoc = '5859'
select * from Sic3000..EstadoCuentas WHERE numfac = '5859'


SP_HELPTEXT sp_ReporteHonorarios

CREATE PROCEDURE sp_ReporteHonorarios  
@ate_codigo bigint,  
@codigo_pedido bigint  
AS  
SELECT  dbo.CUENTAS_PACIENTES.MED_CODIGO,   
CONCAT(dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_APELLIDO_MATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1, ' ',dbo.MEDICOS.MED_NOMBRE2) AS MEDICO,  
dbo.CUENTAS_PACIENTES.CUE_FECHA AS FECHA, dbo.CUENTAS_PACIENTES.CUE_VALOR AS VALOR,  
dbo.CUENTAS_PACIENTES.NumVale AS FACT_MEDICO,(SELECT    TOP(1) dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE   
FROM dbo.ATENCIONES a INNER JOIN dbo.ATENCION_DETALLE_CATEGORIAS ON a.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO   
INNER JOIN dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO  
WHERE a.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO) as SEGURO  
FROM dbo.CUENTAS_PACIENTES   
INNER JOIN dbo.PRODUCTO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO   
INNER JOIN dbo.MEDICOS ON dbo.CUENTAS_PACIENTES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO   
INNER JOIN dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO  
WHERE(dbo.PRODUCTO.PRO_DESCRIPCION LIKE '%HONORARIO%')  
AND dbo.ATENCIONES.ATE_CODIGO = @ate_codigo AND CUENTAS_PACIENTES.Codigo_Pedido = @codigo_pedido  