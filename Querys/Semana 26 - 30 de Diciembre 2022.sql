----------------------------------------------------------------2022-12-27 11:19-----------------------------------------------------------------------------------
ALTER PROCEDURE sp_CertificadoPresentacion
@fechainicio datetime,                
@fechafin datetime,            
@estado bit    
as
select  hc as 'HISTORIAL CLINICO',CP.ate_codigo AS 'NRO ATENCION',id as 'NRO CERTIFICADO', apellido1+' '+apellido2+' '+nombre1+' '+nombre2 AS 'PACIENTE',
M.MED_APELLIDO_PATERNO+' '+MED_APELLIDO_MATERNO+' '+MED_NOMBRE1+' '+MED_NOMBRE2 as MEDICO,fechaAlta AS 'FECHA DE ALTA',cp.medico as 'MED_CODIGO','CA' as 'TIPO CERTIFICADO' 
from CERTIFICADO_PRESENTACION CP INNER JOIN MEDICOS M ON CP.medico = M.MED_CODIGO
where CP.fechaAlta BETWEEN @fechainicio AND @fechafin AND  CP.estado=@estado    
GO
----------------------------------------------------------------2022-12-27 12:17-----------------------------------------------------------------------------------

alter procedure sp_CertificadoPresentacionXmedico            
@fechainicio datetime,            
@fechafin datetime,        
@idMedico int,      
@estado bit      
AS       
select  hc as 'HISTORIAL CLINICO',CP.ate_codigo AS 'NRO ATENCION',id as 'NRO CERTIFICADO', apellido1+' '+apellido2+' '+nombre1+' '+nombre2 AS 'PACIENTE',
M.MED_APELLIDO_PATERNO+' '+MED_APELLIDO_MATERNO+' '+MED_NOMBRE1+' '+MED_NOMBRE2 as MEDICO,fechaAlta AS 'FECHA DE ALTA',CP.medico as 'MED_CODIGO','CA' as 'TIPO CERTIFICADO' 
from CERTIFICADO_PRESENTACION CP INNER JOIN MEDICOS M ON CP.medico = M.MED_CODIGO
where CP.fechaAlta BETWEEN @fechainicio AND @fechafin AND  CP.estado=@estado and cp.medico = @idMedico