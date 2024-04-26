-------------------------------------------------------14-09-2022------------------------------------------------------------------------------------

alter PROCEDURE [dbo].[sp_DtoPacientesAtencionesActivas_1]    
(  
  
 @PISO AS SMALLINT,  
 @NIVEL_MAQUINA AS INT  
  
)      
  
AS        
  
BEGIN        
  
 SET ROWCOUNT  1000        
  
IF(@NIVEL_MAQUINA=0)  
BEGIN  
  SELECT CodigoHabitacion = a.HAB_CODIGO,        
  
   NumeroHabitacion = h.hab_Numero,        
  
   Cedula = p.PAC_IDENTIFICACION,        
  
   NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),        
  
   HistoriaClincia = p.PAC_HISTORIA_CLINICA,        
  
   NumeroAtencion = a.ATE_NUMERO_ATENCION,        
  
   Atencion = a.ATE_CODIGO,        
  
   Sexo = p.PAC_GENERO,        
  
   Aseguradora = cc.CAT_NOMBRE,        
  
   FechaIngreso = a.ATE_FECHA_INGRESO,        
  
   MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),        
  
   TipoTratamiento = tt.TIA_DESCRIPCION,        
  
   DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,  
  
   DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),   
      CONVERT(DATE,GETDATE()) ))     
              
   , FechaNacimiento = PAC_FECHA_NACIMIENTO  
   , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )  
   , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO   
      WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO)  
  
  
  
   FROM ATENCIONES a        
  
     JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO        
  
     LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO        
  
     LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO        
  
     LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO        
  
     LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO        
  
     LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO   
  
       
  
   WHERE   
   
    A.ESC_CODIGO=1-- and a.TIP_CODIGO in (1,2,3)    
      
  
  ORDER BY h.hab_Numero asc        
  
    
  
END  
  
IF(@PISO=0)  
BEGIN  
  SELECT CodigoHabitacion = a.HAB_CODIGO,        
  
   NumeroHabitacion = h.hab_Numero,        
  
   Cedula = p.PAC_IDENTIFICACION,        
  
   NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),        
  
   HistoriaClincia = p.PAC_HISTORIA_CLINICA,        
  
   NumeroAtencion = a.ATE_NUMERO_ATENCION,        
  
   Atencion = a.ATE_CODIGO,        
  
   Sexo = p.PAC_GENERO,        
  
   Aseguradora = cc.CAT_NOMBRE,        
  
   FechaIngreso = a.ATE_FECHA_INGRESO,        
  
   MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),        
  
   TipoTratamiento = tt.TIA_DESCRIPCION,        
  
   DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,  
  
   DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),   
      CONVERT(DATE,GETDATE()) ))     
              
   , FechaNacimiento = PAC_FECHA_NACIMIENTO  
   , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )  
   , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO   
      WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO)  
  
  
  
   FROM ATENCIONES a        
  
     JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO        
  
     LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO        
  
     LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO        
  
     LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO        
  
     LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO        
  
     LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO   
  
     LEFT JOIN NIVEL_PISO np ON np.NIV_CODIGO=h.NIV_CODIGO  
  
   WHERE   
   
    A.ESC_CODIGO=1    
    and    NP.NIV_CODIGO = @NIVEL_MAQUINA  
  
  ORDER BY h.hab_Numero asc        
  
    
  
END  
  
  
IF(@PISO=1)  
BEGIN  
  
  SELECT CodigoHabitacion = a.HAB_CODIGO,        
  
   NumeroHabitacion = h.hab_Numero,        
  
   Cedula = p.PAC_IDENTIFICACION,        
  
   NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),        
  
   HistoriaClincia = p.PAC_HISTORIA_CLINICA,        
  
   NumeroAtencion = a.ATE_NUMERO_ATENCION,        
  
   Atencion = a.ATE_CODIGO,        
  
   Sexo = p.PAC_GENERO,        
  
   Aseguradora = cc.CAT_NOMBRE,        
  
   FechaIngreso = a.ATE_FECHA_INGRESO,        
  
   MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),        
  
   TipoTratamiento = tt.TIA_DESCRIPCION,        
  
   DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,  
  
   DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),   
      CONVERT(DATE,GETDATE()) ))     
  
         , FechaNacimiento = PAC_FECHA_NACIMIENTO  
   , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )  
   , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO   
      WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO)  
  
   FROM ATENCIONES a        
  
     JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO        
  
     LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO        
  
     LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO        
  
     LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO        
  
     LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO        
  
     LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO  
       
     LEFT JOIN NIVEL_PISO np ON np.NIV_CODIGO=h.NIV_CODIGO      
       
  
   WHERE   
   
    A.ESC_CODIGO=1    
    and  H.NIV_CODIGO=NP.NIV_CODIGO  
    AND NP.NIV_CODIGO = @NIVEL_MAQUINA  
         
  
  ORDER BY h.hab_Numero asc        
  
 END  
  
 IF(@PISO=2)  
 BEGIN  
  
  SELECT CodigoHabitacion = a.HAB_CODIGO,        
  
   NumeroHabitacion = h.hab_Numero,        
  
   Cedula = p.PAC_IDENTIFICACION,        
  
   NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),        
  
   HistoriaClincia = p.PAC_HISTORIA_CLINICA,        
  
   NumeroAtencion = a.ATE_NUMERO_ATENCION,        
  
   Atencion = a.ATE_CODIGO,        
  
   Sexo = p.PAC_GENERO,        
  
   Aseguradora = cc.CAT_NOMBRE,        
  
   FechaIngreso = a.ATE_FECHA_INGRESO,        
  
   MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),        
  
   TipoTratamiento = tt.TIA_DESCRIPCION,        
  
   DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,  
  
   DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),   
      CONVERT(DATE,GETDATE()) ))     
  
         , FechaNacimiento = PAC_FECHA_NACIMIENTO  
   , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )  
   , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO   
      WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO)  
  
  
   FROM ATENCIONES a        
  
     JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO        
  
     LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO        
  
     LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO        
  
     LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO        
  
     LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO        
  
     LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO, NIVEL_PISO NP        
  
   WHERE   
   
    A.ESC_CODIGO=1    
    and  H.NIV_CODIGO=NP.NIV_CODIGO  
    AND NP.NIV_CODIGO = @NIVEL_MAQUINA  
     
  
  ORDER BY h.hab_Numero asc        
  
 END  
  
 IF(@PISO=3)  
 BEGIN  
  
  SELECT CodigoHabitacion = a.HAB_CODIGO,        
  
   NumeroHabitacion = h.hab_Numero,        
  
   Cedula = p.PAC_IDENTIFICACION,        
  
   NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),        
  
   HistoriaClincia = p.PAC_HISTORIA_CLINICA,        
  
   NumeroAtencion = a.ATE_NUMERO_ATENCION,        
  
   Atencion = a.ATE_CODIGO,        
  
   Sexo = p.PAC_GENERO,        
  
   Aseguradora = cc.CAT_NOMBRE,        
  
   FechaIngreso = a.ATE_FECHA_INGRESO,        
  
   MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),        
  
   TipoTratamiento = tt.TIA_DESCRIPCION,        
  
   DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,  
  
   DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),   
      CONVERT(DATE,GETDATE()) ))     
  
   , FechaNacimiento = PAC_FECHA_NACIMIENTO  
   , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )  
   , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO   
      WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO)  
  
   FROM ATENCIONES a        
  
     JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO        
  
     LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO        
  
     LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO        
  
     LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO        
  
     LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO        
  
     LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO, NIVEL_PISO NP        
  
   WHERE   
   
    A.ESC_CODIGO=1    
    and  H.NIV_CODIGO=NP.NIV_CODIGO  
    AND NP.NIV_CODIGO IN (7,8,9,13)  
  
     
  
  ORDER BY h.hab_Numero asc        
  
 END  
  set rowcount 0        
END

GO
---------------------------------------------------------------09.15.2022------------------------------------------------------------------------------------------------------------
ALTER PROCEDURE sp_Certificado_IESS
@CMI_CODIGO int,
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
INSERT INTO CERTIFICADO_MEDICO_IESS(CMI_CODIGO,ATE_CODIGO,MED_CODIGO,CMI_INSTITUCION_LABORAL,CMI_FECHA,CMI_DIAS_REPOSO,CMI_ACTIVIDAD_LABORAL,CMI_CONTINGENCIA,CMI_CONFIRMADO,CMI_TRATAMIENTO
,CMI_FECHA_CIRUGIA,CMI_DESCRIPCION_SINTOMAS,CMI_NOTA,CMI_TIPO_INGRESO,CMI_ESTADO,CMI_ENFERMEDAD,CMI_SINTOMAS,CMI_REPOSO,CMI_AISLAMIENTO,CMI_TELETRABAJO,DIRECCION_PACIENTE,
TELEFONO_PACIENTE,CMI_ANULADO,CMI_FECHA_ALTA,CMI_PROCEDIMIENTO)     
    
VALUES (@CMI_CODIGO,@ATE_CODIGO,@MED_CODIGO,@CMI_INSTITUCION_LABORAL,@CMI_FECHA,@CMI_DIAS_REPOSO,@CMI_ACTIVIDAD_LABORAL,@CMI_CONTINGENCIA,@CMI_CONFIRMADO,@CMI_TRATAMIENTO
,@CMI_FECHA_CIRUGIA,@CMI_DESCRIPCION_SINTOMAS,@CMI_NOTA,@CMI_TIPO_INGRESO,@CMI_ESTADO,@CMI_ENFERMEDAD,@CMI_SINTOMAS,@CMI_REPOSO,@CMI_AISLAMIENTO,@CMI_TELETRABAJO
,@DIRECCION_PACIENTE,@TELEFONO_PACIENTE,@CMI_ANULADO,@CMI_FECHA_ALTA,@CMI_PROCEDIMIENTO)

GO
---------------------------------------------------------------09.15.2022------------------------------------------------------------------------------------------------------------
alter PROCEDURE [dbo].[sp_Certificado_InsertarPaciente]
@cer_codigo int,
@ate_codigo int,    
@med_codigo int,    
@observacion varchar(4000),    
@reposo int,    
@actividad nvarchar(1000),    
@contingencia nvarchar(500),    
@tratamiento nvarchar(1000),    
@procedimiento nvarchar(1000),    
@ingreso int,  
@fechaCirugia datetime  
AS    
INSERT INTO CERTIFICADO_MEDICO(CER_CODIGO,ATE_CODIGO, MED_CODIGO, CER_OBSERVACION, CER_FECHA,     
CER_DIAS_REPOSO, CER_ACTIVIDAD_LABORAL, CER_CONTINGENCIA, CER_TRATAMIENTO, CER_FECHA_CIRUGIA, CER_PROCEDIMIENTO,    
CER_TIPO_INGRESO)    
VALUES (@cer_codigo,@ate_codigo, @med_codigo, @observacion, GETDATE(), @reposo,     
@actividad, @contingencia, @tratamiento, @fechaCirugia, @procedimiento, @ingreso)

GO
---------------------------------------------------------------09.15.2022------------------------------------------------------------------------------------------------------------
create table CAMBIO_ESTADO_ATENCIONES(
CEA_CODIGO INT IDENTITY,
ATE_CODIGO INT,
ESC_CODIGO INT,
ID_USUARIO INT,
CEA_FECHA DATETIME
)

GO
---------------------------------------------------------------09.15.2022------------------------------------------------------------------------------------------------------------

create PROCEDURE sp_grabarAutitoriaAtenciones
@ATE_CODIGO INT,
@ESC_CODIGO INT,
@ID_USUARIO INT
AS
INSERT INTO CAMBIO_ESTADO_ATENCIONES (ATE_CODIGO,ESC_CODIGO,ID_USUARIO,CEA_FECHA)
VALUES (@ATE_CODIGO ,@ESC_CODIGO,@ID_USUARIO,getdate())