--------------------------------------------------05-09-2022----------------------------------------------------------------------------------
CREATE TABLE ARE_ASIGNADA(
AS_ID INT IDENTITY NOT NULL,
AS_DESCRIPCION VARCHAR (100) NULL
)
go

INSERT INTO ARE_ASIGNADA
VALUES ('TODAS')
GO


CREATE PROCEDURE sp_ActualizaKardexSicBrigada      
@numdoc nvarchar(20),  
@usuario int  
AS        
UPDATE Sic3000..Kardex SET codlocal = 62 WHERE numdoc = @numdoc and codusu = @usuario
go

CREATE PROCEDURE [dbo].[sp_CertificadoMedicoPacienteTodos]      
AS      
BEGIN      
SELECT P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_IDENTIFICACION AS Identificacion,      
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,      
A.ATE_FECHA_INGRESO AS 'F. Ingreso', A.ATE_CODIGO, A.ATE_FECHA_ALTA, M.MED_EMAIL, M.MED_RUC,      
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico      
FROM PACIENTES P       
INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO      
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO      
INNER JOIN USUARIOS U ON M.ID_USUARIO = U.ID_USUARIO      
ORDER BY A.ATE_FECHA_INGRESO DESC      
END
go

CREATE PROCEDURE [dbo].[sp_CertificadoMedicoPacienteBrigada]      
AS      
BEGIN      
SELECT P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_IDENTIFICACION AS Identificacion,      
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,      
A.ATE_FECHA_INGRESO AS 'F. Ingreso', A.ATE_CODIGO, A.ATE_FECHA_ALTA, M.MED_EMAIL, M.MED_RUC,      
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico      
FROM PACIENTES P       
INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO      
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO      
INNER JOIN USUARIOS U ON M.ID_USUARIO = U.ID_USUARIO      
WHERE A.TIP_CODIGO = 12  
ORDER BY A.ATE_FECHA_INGRESO DESC      
END
go
--------------------------------------------------05-09-2022----------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[sp_QuirofanoGastro]  
AS  
SELECT M.MED_CODIGO AS CODIGO, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,  
EM.ESP_NOMBRE AS ESPECIALIDAD  
FROM MEDICOS M   
INNER JOIN ESPECIALIDADES_MEDICAS EM ON M.ESP_CODIGO = EM.ESP_CODIGO  
WHERE --EM.ESP_CODIGO = 3 or  
EM.ESP_CODIGO = 121
ORDER BY M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 ASC  
go 

CREATE PROCEDURE [dbo].[sp_QuirofanoPatologo]  
AS  
SELECT M.MED_CODIGO AS CODIGO, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,  
EM.ESP_NOMBRE AS ESPECIALIDAD  
FROM MEDICOS M   
INNER JOIN ESPECIALIDADES_MEDICAS EM ON M.ESP_CODIGO = EM.ESP_CODIGO  
WHERE --EM.ESP_CODIGO = 3 or  
EM.ESP_CODIGO = 146
ORDER BY M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 ASC  
go
--------------------------------------------------06-09-2022----------------------------------------------------------------------------------
alter PROCEDURE sp_CrearMedicos    
@med_codigo int,    
@esp_codigo int,    
@med_nombre1 nvarchar(100),    
@med_nombre2 nvarchar(100),    
@med_apellido1 nvarchar(100),    
@med_apellido2 nvarchar(100),    
@fechanacimiento datetime,    
@med_direccion nvarchar(500),    
@med_direccionC nvarchar(160),    
@med_ruc nvarchar(16),    
@med_email nvarchar(80),    
@med_genero char(1),
@med_cuenta_contable nvarchar(10),
@telefono_casa nvarchar(16),    
@telefono_consu nvarchar(16),    
@celular nvarchar(16),    
@transferencia bit,    
@tim_codigo int,    
@tih_codigo int ,  
@ret_codigo int
AS
declare @codigo_med nvarchar (15)
set @codigo_med = (select codigo_c from Cg3000..Cgcodcon where campo4 = @med_ruc)
IF EXISTS (select codigo_c from Cg3000..Cgcodcon where campo4 = @med_ruc)
begin
INSERT INTO MEDICOS VALUES(@med_codigo, @ret_codigo, 10, @esp_codigo, 1, 1, @tim_codigo,@tih_codigo, @codigo_med, GETDATE(), GETDATE(), @med_nombre1,     
@med_nombre2, @med_apellido1, @med_apellido2, @fechanacimiento, @med_direccion, @med_direccionC, @med_ruc,     
@med_email, @med_genero, NULL, 'C', @med_cuenta_contable, @telefono_casa, @telefono_consu, @celular, '000000', NULL, NULL, NULL, @transferencia, 0,    
1, null, null)
end 
else 
begin
INSERT INTO MEDICOS VALUES(@med_codigo, @ret_codigo, 10, @esp_codigo, 1, 1, @tim_codigo,@tih_codigo, NULL, GETDATE(), GETDATE(), @med_nombre1,     
@med_nombre2, @med_apellido1, @med_apellido2, @fechanacimiento, @med_direccion, @med_direccionC, @med_ruc,     
@med_email, @med_genero, NULL, 'C', @med_cuenta_contable, @telefono_casa, @telefono_consu, @celular, '000000', NULL, NULL, NULL, @transferencia, 0,    
1, null, null)
end
go
--------------------------------------------------09-09-2022----------------------------------------------------------------------------------
CREATE TABLE CERTIFICADO_MEDICO_IESS
(
CMI_CODIGO int IDENTITY(1,1) NOT NULL,
ATE_CODIGO int NOT NULL,
MED_CODIGO int NOT NULL,
CMI_INSTITUCION_LABORAL nvarchar(300) NULL,
CMI_FECHA datetime NOT NULL,
CMI_DIAS_REPOSO int NULL,
CMI_ACTIVIDAD_LABORAL nvarchar(500) NULL,
CMI_CONTINGENCIA nvarchar(500) NULL,
CMI_CONFIRMADO nvarchar(100) NULL,
CMI_TRATAMIENTO nvarchar (500),
CMI_FECHA_CIRUGIA datetime NULL,
CMI_DESCRIPCION_SINTOMAS nvarchar(500) NULL,
CMI_NOTA nvarchar(500) NULL,
CMI_TIPO_INGRESO int NULL,
CMI_ESTADO bit NULL,
CMI_ENFERMEDAD BIT NULL,
CMI_SINTOMAS BIT NULL,
CMI_REPOSO BIT NULL,
CMI_AISLAMIENTO BIT NULL,
CMI_TELETRABAJO BIT NULL,
DIRECCION_PACIENTE varchar(500) NULL,
TELEFONO_PACIENTE varchar(500) NULL,
CMI_ANULADO varchar (150) null,
CMI_FECHA_ALTA datetime NOT NULL
)
go

CREATE TABLE CERTIFICADO_MEDICO_DETALLE_IESS
(
CMDI_CODIGO int IDENTITY(1,1) NOT NULL,
CMI_CODIGO int NULL,
CIE_CODIGO varchar(7) NULL
)
go

ALTER TABLE CERTIFICADO_MEDICO_IESS 
add CMI_PROCEDIMIENTO NVARCHAR(250)
go

CREATE PROCEDURE sp_Certificado_DetalleIESS
@CIE_CODIGO varchar(7)  
AS  
DECLARE @codigo_certificado INT  
SET @codigo_certificado = (SELECT MAX(CMI_CODIGO) FROM CERTIFICADO_MEDICO_IESS)  
INSERT INTO CERTIFICADO_MEDICO_DETALLE_IESS VALUES (@codigo_certificado, @cie_codigo)
go
--------------------------------------------------09-09-2022----------------------------------------------------------------------------------

create PROCEDURE sp_CertificadoIESS_Mostrar              
@ate_codigo int              
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
WHERE CM.ATE_CODIGO = @ate_codigo              
ORDER BY CM.CMI_FECHA DESC
go
--------------------------------------------------09-09-2022----------------------------------------------------------------------------------
   
ALTER procedure sp_CertificadosMedicos              
@fechainicio datetime,              
@fechafin datetime,          
@estado bit          
AS              
BEGIN            
select CM.CER_CODIGO AS 'NRO CERTIFICADO', CM.CER_FECHA AS 'FECHA CERTIFICADO',  A.ATE_CODIGO AS 'NRO ATENCION',               
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,              
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,              
CER_DIAS_REPOSO AS 'DIAS REPOSO', CER_OBSERVACION AS OBSERVACION,'CM' as 'TIPO CERTIFICADO'
from CERTIFICADO_MEDICO CM              
--INNER JOIN CERTIFICADO_MEDICO_DETALLE CMD ON CM.CER_CODIGO = CMD.CER_CODIGO              
INNER JOIN MEDICOS M ON CM.MED_CODIGO = M.MED_CODIGO              
INNER JOIN ATENCIONES A ON CM.ATE_CODIGO = A.ATE_CODIGO              
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO              
where CM.CER_FECHA BETWEEN @fechainicio AND @fechafin AND  CER_ESTADO=@estado            
UNION    
select CM.CMI_CODIGO AS 'NRO CERTIFICADO', CM.CMI_FECHA AS 'FECHA CERTIFICADO',  A.ATE_CODIGO AS 'NRO ATENCION',    
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,    
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,     
CMI_DIAS_REPOSO AS 'DIAS REPOSO', CMI_DESCRIPCION_SINTOMAS AS OBSERVACION,'CME' as 'TIPO CERTIFICADO'      
from CERTIFICADO_MEDICO_IESS CM    
inner join MEDICOS M ON CM.MED_CODIGO = M.MED_CODIGO    
INNER JOIN ATENCIONES A ON CM.ATE_CODIGO = A.ATE_CODIGO    
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO    
where CM.CMI_FECHA BETWEEN @fechainicio AND @fechafin AND CMI_ESTADO=@estado        
END
go
--------------------------------------------------09-09-2022----------------------------------------------------------------------------------
create PROCEDURE sp_Certificado_IESS    
@ATE_CODIGO int,    
@MED_CODIGO int,    
@CMI_INSTITUCION_LABORAL nvarchar(300),    
@CMI_FECHA datetime,    
@CMI_DIAS_REPOSO int,    
@CMI_ACTIVIDAD_LABORAL nvarchar(500),    
@CMI_CONTINGENCIA nvarchar(500),    
@CMI_CONFIRMADO nvarchar(100),
@CMI_TRATAMIENTO NVARCHAR (100),
@CMI_FECHA_CIRUGIA datetime,    
@CMI_DESCRIPCION_SINTOMAS nvarchar(500),    
@CMI_NOTA nvarchar(500),    
@CMI_TIPO_INGRESO int,    
@CMI_ESTADO bit,    
@CMI_ENFERMEDAD BIT,    
@CMI_SINTOMAS BIT,    
@CMI_REPOSO BIT,    
@CMI_AISLAMIENTO BIT,    
@CMI_TELETRABAJO BIT,    
@DIRECCION_PACIENTE varchar(500),    
@TELEFONO_PACIENTE varchar(500),    
@CMI_ANULADO varchar (150),    
@CMI_FECHA_ALTA datetime,
@CMI_PROCEDIMIENTO NVARCHAR(250)
AS        
INSERT INTO CERTIFICADO_MEDICO_IESS(ATE_CODIGO,MED_CODIGO,CMI_INSTITUCION_LABORAL,CMI_FECHA,CMI_DIAS_REPOSO,CMI_ACTIVIDAD_LABORAL,CMI_CONTINGENCIA,CMI_CONFIRMADO,CMI_TRATAMIENTO
,CMI_FECHA_CIRUGIA,CMI_DESCRIPCION_SINTOMAS,CMI_NOTA,CMI_TIPO_INGRESO,CMI_ESTADO,CMI_ENFERMEDAD,CMI_SINTOMAS,CMI_REPOSO,CMI_AISLAMIENTO,CMI_TELETRABAJO,DIRECCION_PACIENTE,
TELEFONO_PACIENTE,CMI_ANULADO,CMI_FECHA_ALTA,CMI_PROCEDIMIENTO)     
    
VALUES (@ATE_CODIGO,@MED_CODIGO,@CMI_INSTITUCION_LABORAL,@CMI_FECHA,@CMI_DIAS_REPOSO,@CMI_ACTIVIDAD_LABORAL,@CMI_CONTINGENCIA,@CMI_CONFIRMADO,@CMI_TRATAMIENTO
,@CMI_FECHA_CIRUGIA,@CMI_DESCRIPCION_SINTOMAS,@CMI_NOTA,@CMI_TIPO_INGRESO,@CMI_ESTADO,@CMI_ENFERMEDAD,@CMI_SINTOMAS,@CMI_REPOSO,@CMI_AISLAMIENTO,@CMI_TELETRABAJO
,@DIRECCION_PACIENTE,@TELEFONO_PACIENTE,@CMI_ANULADO,@CMI_FECHA_ALTA,@CMI_PROCEDIMIENTO)
go

CREATE PROCEDURE sp_CertificadoIESS_Mostrar_Detalle  
@CMI_CODIGO bigint  
AS  
SELECT CMD.CIE_CODIGO, C.CIE_DESCRIPCION   
FROM CERTIFICADO_MEDICO_DETALLE_IESS CMD  
INNER JOIN CIE10 C ON CMD.CIE_CODIGO = C.CIE_CODIGO  
WHERE CMI_CODIGO = @CMI_CODIGO  
go
--------------------------------------------------09-09-2022----------------------------------------------------------------------------------
alter procedure sp_CertificadosXmedico          
@fechainicio datetime,          
@fechafin datetime,      
@idMedico int,    
@estado bit    
AS          
BEGIN        
select CM.CER_CODIGO AS 'NRO CERTIFICADO', CM.CER_FECHA AS 'FECHA CERTIFICADO',  A.ATE_NUMERO_ATENCION AS 'NRO ATENCION',           
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,          
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,          
CER_DIAS_REPOSO AS 'DIAS REPOSO', CER_OBSERVACION AS OBSERVACION,'CM' as 'TIPO CERTIFICADO'             
from CERTIFICADO_MEDICO CM          
--INNER JOIN CERTIFICADO_MEDICO_DETALLE CMD ON CM.CER_CODIGO = CMD.CER_CODIGO          
INNER JOIN MEDICOS M ON CM.MED_CODIGO = M.MED_CODIGO          
INNER JOIN ATENCIONES A ON CM.ATE_CODIGO = A.ATE_CODIGO          
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO          
where CM.CER_FECHA BETWEEN @fechainicio AND @fechafin AND  CER_ESTADO=1  and cm.MED_CODIGO=@idMedico and CM.CER_ESTADO=@estado 

END 