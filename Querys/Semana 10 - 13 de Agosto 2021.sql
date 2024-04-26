
---ESTO NO VA EN LA ACTUALIZACION
-- USE [Sic3000]
-- GO

-- /****** Object:  View [dbo].[HonorariosMedico]    Script Date: 10/08/2021 10:51:14 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO

-- ALTER VIEW [dbo].[HonorariosMedico]
-- AS
-- SELECT HM.HOM_CODIGO AS CODIGO, A.ATE_CODIGO, P.PAC_HISTORIA_CLINICA, 
-- P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,
-- convert(nvarchar, hm.HOM_CODIGO) as CODIGO2
-- FROM His3000..HONORARIOS_MEDICOS HM
-- INNER JOIN His3000..ATENCIONES A ON HM.ATE_CODIGO = A.ATE_CODIGO
-- INNER JOIN His3000..PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
-- GO
--FIN DE ESTO NO VA


-- ALTER PROCEDURE [dbo].[sp_GuardaFechasEvolucionMedica]    
-- (    
 -- @nuevaEvolucion INT,    
 -- @fechaInicio DATETIME,    
 -- @fechaFin DATETIME,    
 -- @evdescripcion varchar(200) ,  
 -- @docs varchar(100)  
-- )    
    
-- AS BEGIN    
    
  -- UPDATE HC_EVOLUCION_DETALLE SET FECHA_INICIO=@fechaInicio, FECHA_FIN=@fechaFin    
 -- WHERE EVD_CODIGO=@nuevaEvolucion    
    
  -- UPDATE HC_EVOLUCION SET NOM_USUARIO = @docs where EVO_CODIGO = (select EVO_CODIGO from HC_EVOLUCION_DETALLE where EVD_CODIGO = @nuevaEvolucion)  
-- END 


CREATE PROCEDURE sp_AsientoAgrupado
@ate_codigo bigint,
@facturaMedico nvarchar(20)
AS
SELECT * FROM HONORARIOS_MEDICOS HM 
INNER JOIN HONORARIOS_MEDICOS_DATOSADICIONALES HMD ON HM.HOM_CODIGO = HMD.HOM_CODIGO
WHERE HMD.GENERADO_ASIENTO <> 1 AND ATE_CODIGO = @ate_codigo
AND HOM_FACTURA_MEDICO = @facturaMedico
GO



UPDATE EstadoCuentas SET fecha2 = CONVERT(date, fecha) where fecha2 is null




--crear en el cgcontabilidad
ALTER TABLE CgContabilidad
ADD HOM_CODIGO BIGINT


ALTER TABLE CgContabilidad
ADD codusu float 
--fin cg contabilidad



CREATE PROCEDURE sp_NumeroAtencion    
@EVO_CODIGO BIGINT    
    
AS    
BEGIN    
  
 SELECT COUNT(*) + 1 FROM ATENCIONES WHERE PAC_CODIGO=@EVO_CODIGO     
END



--SCRIPTS DE PABLO
-- CREATE PROCEDURE sp_VerificaEstadoMedico
-- @MedCodigo int
-- AS
-- BEGIN
	
	-- SELECT TIM_CODIGO FROM MEDICOS WHERE MED_CODIGO=@MedCodigo

-- END



ALTER VIEW ECHONORARIOPACIENTES
as
SELECT  EC.fecha, EC.numfac, HM.PACIENTE, EC.obs, FP.forpag, EC.iddoc
, FP.despag, C.codclas, C.desclas, EC.saldo, EC.debe, EC.haber, EC.fecha2, P.PAC_IDENTIFICACION
FROM EstadoCuentas EC
INNER JOIN HonorariosMedico HM ON EC.numfac = CONVERT(NVARCHAR,HM.CODIGO)
INNER JOIN Clasificacion C ON EC.claspag = C.codclas
INNER JOIN Forma_Pago FP ON EC.forpag = FP.forpag
INNER JOIN His3000..PACIENTES P ON HM.PAC_HISTORIA_CLINICA = P.PAC_HISTORIA_CLINICA
WHERE P.PAC_IDENTIFICACION = '1700313032'
go




alter VIEW EstadoCuentaPaciente
AS
SELECT EC.fecha, EC.numfac, FDA.Nombres, EC.obs, FP.forpag, EC.iddoc
, FP.despag, C.codclas, C.desclas, EC.saldo, EC.debe, EC.haber, EC.fecha2, FDA.RucCedula
FROM EstadoCuentas EC
LEFT JOIN FacturaDatosAdicionales FDA ON EC.numfac = FDA.Numdoc
--INNER JOIN Cliente CL ON FDA.RucCedula = CL.ruccli
INNER JOIN Clasificacion C ON EC.claspag = C.codclas
INNER JOIN Forma_Pago FP ON EC.forpag = FP.forpag