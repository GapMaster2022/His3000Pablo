ALTER PROCEDURE sp_HonorarioValidarAnulacion
@hom_codigo bigint,
@codcli nvarchar(10),
@nocomp nvarchar(20)
AS
DECLARE @usuario FLOAT
SET @usuario = (SELECT usuarioing FROM Cg3000..CgCuentasXPagar WHERE nocomp = @nocomp AND codigo_c = @codcli and estadopago <> 'P')


SELECT saldo  AS VALOR, 'TIPO' = 'CXC', 'USUARIO' = @usuario FROM Sic3000..CxC WHERE numdoc = CONVERT(nvarchar(10), @hom_codigo) --SI ES CERO ESTA PAGADO Y NO PUEDE ANULAR
UNION
SELECT (SUM(debe) - SUM(haber)) AS VALOR, 'TIPO' = 'EC',  'USUARIO' = @usuario FROM Sic3000..EstadoCuentas WHERE numfac = CONVERT(nvarchar(10), @hom_codigo)
UNION
SELECT isnull((SUM(debe) - SUM(haber)), 0) AS VALOR, 'TIPO' = 'CXP',  'USUARIO' = @usuario  FROM Cg3000..CgCuentasXPagar WHERE nocomp = @nocomp AND codigo_c = @codcli and estadopago <> 'P'
GO

------------------------------------------------------------
USE [His3000]
GO

/****** Object:  View [dbo].[DESPACHOS]    Script Date: 07/12/2021 16:01:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[DESPACHOS]
AS
SELECT ISNULL((CD.CD_ESTADO), 'false') AS 'despachados',  
             P.PED_CODIGO AS 'CODIGO PEDIDO', COUNT(PD.PRO_DESCRIPCION) AS CANTIDAD,  
             P.PED_FECHA AS FECHA, CONVERT(varchar, PED_FECHA, 8) AS HORA,  
             PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2 AS PACIENTE,
			 NP.NIV_CODIGO, NP.NIV_NOMBRE AS PISO, H.hab_Codigo, H.hab_Numero AS 'HAB',
             PA.PAC_HISTORIA_CLINICA AS 'HIST. CLINICA', A.ATE_NUMERO_ATENCION AS 'N? ATENCION',  
             U.APELLIDOS + ' ' + U.NOMBRES AS 'PEDIDO POR',  
             ISNULL((U1.APELLIDOS + ' ' + U1.NOMBRES), '') AS 'DESPACHADO POR', ISNULL((CD.CD_FECHA), '') AS 'FECHA DESPACHO',  
             ISNULL((CONVERT(varchar, CD.CD_FECHA, 8)), '00:00:00') AS 'HORA DESPACHO', ISNULL((CD.CD_OBSERVACION), '') AS OBSERVACION  
             FROM PEDIDOS P  
             INNER JOIN PEDIDOS_DETALLE PD ON PD.PED_CODIGO = P.PED_CODIGO  
			 INNER JOIN Sic3000..Producto SP ON PD.PRO_CODIGO = SP.codpro AND SP.clasprod <> 'S'
             INNER JOIN ATENCIONES A ON P.ATE_CODIGO = A.ATE_CODIGO  
             INNER JOIN PACIENTES PA ON A.PAC_CODIGO = PA.PAC_CODIGO  
             INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo  
             LEFT JOIN CONTROL_DESPACHO CD ON P.PED_CODIGO = CD.PED_CODIGO  
             INNER JOIN NIVEL_PISO NP ON NP.NIV_CODIGO = H.NIV_CODIGO  
             INNER JOIN USUARIOS U ON P.ID_USUARIO = U.ID_USUARIO  
             LEFT JOIN USUARIOS U1 ON CD.ID_USUARIO = U1.ID_USUARIO
			 GROUP BY P.PED_CODIGO, CD.CD_ESTADO, P.PED_FECHA,  
             PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2,  
             PA.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION, U.APELLIDOS + ' ' + U.NOMBRES,  
             U1.APELLIDOS + ' ' + U1.NOMBRES, CD.CD_FECHA, CD.CD_OBSERVACION, NP.NIV_CODIGO,
			 NP.NIV_NOMBRE, H.hab_Codigo, H.hab_Numero 
GO

-------------------------------------------------------------------
ALTER PROCEDURE sp_Pedidos_InsertDespacho  
@ped_codigo bigint,  
@id_usuario smallint,  
@observacion nvarchar(500),  
@despachado int, 
@fecha datetime

AS  
if(@despachado = 0)  
BEGIN  
 INSERT INTO CONTROL_DESPACHO(ID_USUARIO, PED_CODIGO, CD_OBSERVACION, CD_FECHA, CD_ESTADO)   
 VALUES (@id_usuario, @ped_codigo, @observacion, @fecha, 1)  
END  
ELSE  
BEGIN  
 INSERT INTO CONTROL_DESPACHO(ID_USUARIO, PED_CODIGO, CD_OBSERVACION, CD_FECHA, CD_ESTADO)   
 VALUES (@id_usuario, @ped_codigo, @observacion, @fecha,  0)  
END  
