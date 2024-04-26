--------------------------------------------------27-09-2022----------------------------------------------------------------------------------  
alter PROCEDURE sp_CertificadoIESS_Mostrar                
@ate_codigo int,
@CMI_CODIGO int
AS                
SELECT TOP 1 P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' +P.PAC_NOMBRE2 AS PACIENTE,        
P.PAC_IDENTIFICACION, P.PAC_HISTORIA_CLINICA, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA,CM.CMI_CODIGO,        
CM.CMI_DESCRIPCION_SINTOMAS,CM.CMI_DIAS_REPOSO,CM.CMI_CONFIRMADO,CM.CMI_INSTITUCION_LABORAL,CM.CMI_ACTIVIDAD_LABORAL,CM.DIRECCION_PACIENTE,CM.TELEFONO_PACIENTE,        
CM.CMI_ENFERMEDAD,CM.CMI_SINTOMAS,CM.CMI_REPOSO,CM.CMI_AISLAMIENTO,CM.CMI_TELETRABAJO,CM.CMI_NOTA,        
 M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,                
M.MED_RUC, M.MED_EMAIL,(SELECT top 1 EMP_NOMBRE FROM EMPRESA), (SELECT top 1 EMP_DIRECCION FROM EMPRESA),                
(SELECT top 1 EMP_TELEFONO FROM EMPRESA), isnull(CM.DIRECCION_PACIENTE, PD.DAP_DIRECCION_DOMICILIO), isnull(CM.TELEFONO_PACIENTE, PD.DAP_TELEFONO2),        
CM.CMI_CONTINGENCIA,CM.CMI_FECHA_ALTA,CM.ATE_CODIGO,CM.CMI_TIPO_INGRESO,CM.CMI_TRATAMIENTO,CM.CMI_PROCEDIMIENTO       
FROM CERTIFICADO_MEDICO_IESS CM                
INNER JOIN ATENCIONES A ON CM.ATE_CODIGO = A.ATE_CODIGO                
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO                
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO             
INNER JOIN PACIENTES_DATOS_ADICIONALES PD ON P.PAC_CODIGO = PD.PAC_CODIGO           
WHERE CM.ATE_CODIGO = @ate_codigo and cm.CMI_CODIGO = @CMI_CODIGO     
ORDER BY CM.CMI_FECHA DESC  
go

--------------------------------------------------28-09-2022----------------------------------------------------------------------------------  
CREATE procedure [dbo].[sp_DatosCuentaAgrupaFactura] (@p_CodigoAtencion as int)                    
as    
begin    
select    
PAC_HISTORIA_CLINICA as HISTORIA,                    
atenciones.ATE_NUMERO_ATENCION AS ATENCION  ,                    
PACIENTES.PAC_APELLIDO_PATERNO + ' ' + PACIENTES.PAC_APELLIDO_MATERNO + ' ' +PACIENTES.PAC_NOMBRE1 + ' ' +PACIENTES.PAC_NOMBRE2 as DATOS,                    
ATENCIONES.ATE_FECHA_INGRESO AS INGRESO,                    
ATENCIONES.ATE_FECHA_ALTA AS ALTA,                    
RUB_GRUPO AS GRUPO,                    
CUENTAS_PACIENTES.CUE_FECHA AS fECHA_INGRESO,                    
CUENTAS_PACIENTES.CUE_DETALLE AS DESCRIPCION,                    
CUENTAS_PACIENTES.CUE_CANTIDAD AS CANTIDAD,                    
CUENTAS_PACIENTES.CUE_VALOR_UNITARIO AS [VALOR_UNITARIO],    
CUENTAS_PACIENTES.CUE_IVA as IVA,    
(CUENTAS_PACIENTES.CUE_VALOR + CUENTAS_PACIENTES.CUE_IVA) as TOTAL,                
(SELECT M.MED_APELLIDO_PATERNO+' '+M.MED_APELLIDO_MATERNO+' '+M.MED_NOMBRE1+' '+M.MED_NOMBRE2 FROM MEDICOS M WHERE M.MED_CODIGO=CUENTAS_PACIENTES.MED_CODIGO) AS MEDICO     
,CUENTAS_PACIENTES.Descuento AS DESCUENTO    
 from CUENTAS_PACIENTES ,RUBROS,ATENCIONES,PACIENTES    
 where CUENTAS_PACIENTES.ATE_CODIGO in (select ate_codigo from AGRUPACION_CUENTAS where ate_codigo_madre = @p_CodigoAtencion)  
 and RUBROS.RUB_CODIGO=CUENTAS_PACIENTES.RUB_CODIGO    
 and CUENTAS_PACIENTES.CUE_ESTADO=1    
 and CUENTAS_PACIENTES.ATE_CODIGO=ATENCIONES.ATE_CODIGO                    
 and PACIENTES.PAC_CODIGO=ATENCIONES.PAC_CODIGO                  
 and isnull(CUENTAS_PACIENTES.CUE_CANTIDAD,0)>0 /*para que no aparezcan items con 0 David Mantilla*/   
 order by atenciones.ATE_NUMERO_ATENCION,RUB_GRUPO,CUENTAS_PACIENTES.CUE_FECHA asc      
end