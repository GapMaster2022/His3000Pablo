ALTER PROCEDURE sp_Certificado_Mostrar      
@ate_codigo int      
AS      
SELECT TOP 1 P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' +P.PAC_NOMBRE2 AS PACIENTE,      
P.PAC_IDENTIFICACION, P.PAC_HISTORIA_CLINICA, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA,       
CM.CER_DIAS_REPOSO, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,      
M.MED_RUC, M.MED_EMAIL, CM.CER_CODIGO, CM.CER_OBSERVACION, (SELECT top 1 EMP_NOMBRE FROM EMPRESA), (SELECT top 1 EMP_DIRECCION FROM EMPRESA),      
(SELECT top 1 EMP_TELEFONO FROM EMPRESA), isnull(CM.DIRECCION_PACIENTE, PD.DAP_DIRECCION_DOMICILIO), isnull(CM.TELEFONO_PACIENTE, PD.DAP_TELEFONO2)
FROM CERTIFICADO_MEDICO CM      
INNER JOIN ATENCIONES A ON CM.ATE_CODIGO = A.ATE_CODIGO      
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO      
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO   
INNER JOIN PACIENTES_DATOS_ADICIONALES PD ON P.PAC_CODIGO = PD.PAC_CODIGO 
WHERE CM.ATE_CODIGO = @ate_codigo      
ORDER BY CM.CER_FECHA DESC 

--------------------------------------------------------------

USE [Sic3000]
GO

/****** Object:  View [dbo].[EstadoCuentaPaciente]    Script Date: 14/03/2022 15:06:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[EstadoCuentaPaciente]
AS
SELECT EC.fecha, EC.numfac, EC.numdoc, FDA.Nombres, EC.obs, FP.forpag, EC.iddoc
, FP.despag, C.codclas, C.desclas, EC.saldo, EC.debe, EC.haber, EC.fecha2, FDA.RucCedula, EC.fecha1
FROM EstadoCuentas EC
LEFT JOIN FacturaDatosAdicionales FDA ON EC.numfac = FDA.Numdoc
--INNER JOIN Cliente CL ON FDA.RucCedula = CL.ruccli
INNER JOIN Clasificacion C ON EC.claspag = C.codclas
INNER JOIN Forma_Pago FP ON EC.forpag = FP.forpag
GO


------------------------------------------------------------------
USE [Sic3000]
GO

/****** Object:  View [dbo].[ECHONORARIOPACIENTES]    Script Date: 14/03/2022 15:24:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[ECHONORARIOPACIENTES]
as
SELECT  EC.fecha, EC.numfac, EC.numdoc, HM.PACIENTE, EC.obs, FP.forpag, EC.iddoc
, FP.despag, C.codclas, C.desclas, EC.saldo, EC.debe, EC.haber, EC.fecha2, P.PAC_IDENTIFICACION, EC.fecha1
FROM EstadoCuentas EC
INNER JOIN HonorariosMedico HM ON EC.numfac = CONVERT(NVARCHAR,HM.CODIGO)
INNER JOIN Clasificacion C ON EC.claspag = C.codclas
INNER JOIN Forma_Pago FP ON EC.forpag = FP.forpag
INNER JOIN His3000..PACIENTES P ON HM.PAC_HISTORIA_CLINICA = P.PAC_HISTORIA_CLINICA
GO

------------------------------------------------------------------------------

--SCRIPT PARA VALIDAR ERRORES DENTRO LA TABLA DE CXC  NO ES NECESARTIO EJECUTAR
SELECT numdoc, debe, TRY_CAST(haber AS FLOAT)AS RESULT FROM CxC
WHERE TRY_CAST(haber AS FLOAT) IS NULL

------------------------------------------------------------------------------------


