CREATE PROCEDURE [dbo].[sp_GastroHabitacion]  
AS  
SELECT H.hab_Codigo AS CODIGO, H.hab_Numero AS HABITACION from  HABITACIONES H  
INNER JOIN NIVEL_PISO N ON H.NIV_CODIGO = N.NIV_CODIGO  
WHERE N.NIV_CODIGO =30  
ORDER BY H.hab_Codigo ASC  

-----------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_QuirofanoAnestesia]  
AS  
SELECT TA_CODIGO AS CODIGO, TA_DESCRIPCION AS DESCRIPCION FROM TIPO_ANESTESIA  ORDER BY 2 ASC
-------------------------------------------------------------------------------------------
--solo activar en la falconi 1 ES ACTIVO Y 0 ES DESACTIVADO
SELECT * FROM PARAMETROS_DETALLE ORDER BY 1 ASC

INSERT INTO PARAMETROS_DETALLE VALUES(41, 25, 'FECHA FACTURA MANUAL', NULL, 'PUEDEN EDITAR FECHA DE FACTURA', 0); --solo activar en la falconi  

--solo activar en la falconi
--------------------------------------------------------------------------------------------

CREATE PROCEDURE sp_ParametroFechaFactura
AS
SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 41
GO

--------------------------------------------------------------------------------------------

----SOLO ACTUALIZAR EN LA PASTEUR----------------------------------------------------
alter PROCEDURE [dbo].[sp_RecuperaPacienteConsultaExterna]  
(  
 @Ate_Codigo as bigint  
)  
AS  

BEGIN   
DECLARE @date datetime, @tmpdate datetime, @years int, @months int, @days int
declare @edad nvarchar(30)
SET @date = (select PAC_FECHA_NACIMIENTO from PACIENTES p 
				inner join ATENCIONES a on p.PAC_CODIGO = a.PAC_CODIGO
				where a.ATE_CODIGO = @Ate_Codigo)

SET @tmpdate = @date
SET @years = floor(
(cast(convert(varchar(8),getdate(),112) as int)-
cast(convert(varchar(8),@date,112) as int) ) / 10000)
SET @tmpdate = DATEADD(yy, @years, @tmpdate)
SET @months = DATEDIFF(m, @tmpdate, GETDATE()) - CASE WHEN DAY(@date) > DAY(GETDATE()) THEN 1 ELSE 0 END
SET @tmpdate = DATEADD(m, @months, @tmpdate)
SET @days = DATEDIFF(d, @tmpdate, GETDATE())
set @edad = cast(@years as nvarchar) + ' A, ' + cast(@months as nvarchar) + ' M. ' --+ cast(@days as nvarchar) + ' dias.'

 select (p.PAC_NOMBRE1+' '+p.PAC_NOMBRE2) as Nombre, (p.PAC_APELLIDO_PATERNO+' '+p.PAC_APELLIDO_MATERNO) as Apellido, 
-- floor((cast(convert(varchar(8),getdate(),112) as int)-
--cast(convert(varchar(8),p.PAC_FECHA_NACIMIENTO,112) as int) ) / 10000) as Edad, 
'Edad' = @edad,
p.PAC_GENERO Genero,   
 concat(m.MED_NOMBRE1,' ',m.MED_NOMBRE2,' ',m.MED_APELLIDO_PATERNO,' ',m.MED_APELLIDO_MATERNO) as Medico,  
 p.PAC_OBSERVACIONES Observacion, a.ATE_DIAGNOSTICO_INICIAL Diagnostico  
 from ATENCIONES a, PACIENTES p, MEDICOS m  
  
 where a.PAC_CODIGO=p.PAC_CODIGO and a.MED_CODIGO=m.MED_CODIGO and  
 a.ATE_CODIGO=@Ate_Codigo
END  

----SOLO SE ACTUALIZA EN LA PASTEUR--------------------------------------------


-------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[sp_oper_recuperar_tarifario] (  
 @codAseguradora INT 
)  
as  
BEGIN   
DECLARE @l_cod_tarifario SMALLINT  
DECLARE @l_resultadoS VARCHAR(20)    
    
 SET @l_cod_tarifario= (SELECT TOP 1  TARIFARIOS.TAR_CODIGO  FROM ASEGURADORAS_EMPRESAS 
INNER JOIN CONVENIOS_TARIFARIOS ON ASEGURADORAS_EMPRESAS.ASE_CODIGO = CONVENIOS_TARIFARIOS.ASE_CODIGO 
INNER JOIN ESPECIALIDADES_TARIFARIOS ON CONVENIOS_TARIFARIOS.EST_CODIGO = ESPECIALIDADES_TARIFARIOS.EST_CODIGO 
INNER JOIN TARIFARIOS ON ESPECIALIDADES_TARIFARIOS.TAR_CODIGO = TARIFARIOS.TAR_CODIGO  
WHERE     ASEGURADORAS_EMPRESAS.ASE_CODIGO = @codAseguradora)  
   
 IF @l_cod_tarifario IS NULL   
 BEGIN  
  SET @l_resultados = '1'  
  GOTO fin  
 END  
  
  SET @l_resultadoS =@l_cod_tarifario       
        
fin:  
 SELECT 'resultado' = @l_resultadoS  
END


------------------------------------------------------------------------------------------------------------
ALTER TABLE AGENDAMIENTO
ADD MED_CODIGO BIGINT
-------------------------------------------------------------------------------------------------------------


alter PROCEDURE sp_GrabaAgendaConsultaExterna    
(    
 @dtpFechaCita AS DATE,    
 @cmbEspecialidades AS VARCHAR(50),    
 @lblMedico AS VARCHAR(100),    
 @lblMailMed AS VARCHAR(100),    
 @cmbConsultorios AS VARCHAR(100),    
 @cmbHora AS VARCHAR(15),    
 @txtMotivo AS VARCHAR(1000),    
 @txtNotas AS VARCHAR(1000),    
 @txtIdentificacion AS VARCHAR(20),  
 @medicoCelular nvarchar(10),
 @med_codigo bigint
)    
AS    
BEGIN    
    
 DECLARE @IDPACIENTE AS BIGINT    
 SET @IDPACIENTE=(SELECT ID_AGENDAMIENTO FROM AGENDA_PACIENTE WHERE Identificacion=@txtIdentificacion)    
 INSERT INTO AGENDAMIENTO VALUES     
 (@IDPACIENTE,@cmbEspecialidades,@lblMedico,@lblMailMed,@cmbConsultorios,@dtpFechaCita,@cmbHora,@txtMotivo,@txtNotas, @medicoCelular, @med_codigo)    
    
END  