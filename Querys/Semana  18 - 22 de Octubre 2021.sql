CREATE TABLE LOGOS_EMPRESA(
LEM_CODIGO BIGINT IDENTITY(1,1) NOT NULL,
LEM_RUTA NVARCHAR(250),
LEM_TIPO NVARCHAR(250))


CREATE PROCEDURE RutasLogo
@tipo nvarchar(250)
AS
SELECT LEM_RUTA FROM LOGOS_EMPRESA WHERE LEM_TIPO = @tipo
GO



----------------------------------------------------------
CREATE TABLE SINTOMAS_CONSULTA(
SC_CODIGO BIGINT IDENTITY(1,1) NOT NULL,
SC_DESCRIPCION NVARCHAR(500))

ALTER PROCEDURE sp_VerSintomas
AS
SELECT SC_CODIGO AS CODIGO, SC_DESCRIPCION AS SINTOMAS FROM SINTOMAS_CONSULTA ORDER BY 2 ASC
GO
----------------------------------------------------
USE [His3000]
GO
SET IDENTITY_INSERT [dbo].[SINTOMAS_CONSULTA] ON 

INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (1, N'DOLOR DE GARGANTA')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (2, N'SÍNTOMAS CATARRALES')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (3, N'DOLOR DE OÍDOS')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (4, N'TOS')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (5, N'DESHABITUACIÓN TABÁQUICA')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (6, N'BRONQUITIS AGUDA')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (7, N'NEUMONÍA')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (8, N'ASMA')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (9, N'DOLOR TORÁCICO')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (10, N'PALPITACIONES')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (11, N'DOLOR DE ESTÓMAGO Y ACIDEZ')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (12, N'DIARREA')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (13, N'ESTREÑIMIENTO')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (14, N'DOLOR DE CABEZA')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (15, N'MAREO Y VÉRTIGO')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (16, N'DOLOR CERVICAL')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (17, N'DOLOR LUMBAR')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (18, N'DEPRESIÓN')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (19, N'ANSIEDAD')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (20, N'INSOMNIO')
INSERT [dbo].[SINTOMAS_CONSULTA] ([SC_CODIGO], [SC_DESCRIPCION]) VALUES (21, N'ASTENIA O FIEBRE')
SET IDENTITY_INSERT [dbo].[SINTOMAS_CONSULTA] OFF
GO
------------------------------------------------------------------------------------------------------------



CREATE procedure [dbo].[sp_DatosAtencionSimplificadaConsultaExterna]                    
                         (                  
                         -- PERMITE INGRESAR LOS DATOS MAS IMPORTANTES EN UNA ATENCION QUE ES DE PROCEDIMIENTO. / GIOVANNY TAPIA / 26/10/2012                  
                         --usados para guardar datos de paciente    
                         @PAC_CODIGO as int,      
                         @ID_USUSARIO as smallint,                
                         @DireccionPaciente as varchar(255),                   
                         @TelefonoPaciente as varchar(9),                 
                         @CelularPaciente as varchar(10),                
                         @Nuevo as bit           
                         --para crear atencion    
                              ,@ATE_CODIGO as int,                    
                              @ATE_NUMERO_ATENCION as nchar(20),                    
                              @ATE_FECHA as datetime,                    
                              @ATE_NUMERO_CONTROL as varchar(10),                    
                              @ATE_FACTURA_PACIENTE as varchar(20),                    
                              @ATE_FACTURA_FECHA as date,                    
                              @ATE_FECHA_INGRESO as datetime,                    
                              @ATE_FECHA_ALTA as datetime,                    
                              @ATE_REFERIDO as bit,                    
                              @ATE_REFERIDO_DE as varchar(100),                    
                              @ATE_EDAD_PACIENTE as smallint,                    
                              @ATE_ACOMPANANTE_NOMBRE as varchar(100),                    
                              @ATE_ACOMPANANTE_CEDULA as varchar(10),                    
                              @ATE_ACOMPANANTE_PARENTESCO as varchar(50),                    
                              @ATE_ACOMPANANTE_TELEFONO as nchar(20),                    
                              @ATE_ACOMPANANTE_DIRECCION as varchar(100),                    
                              @ATE_ACOMPANANTE_CIUDAD as varchar(50),                    
                              @ATE_GARANTE_NOMBRE as varchar(100),                    
                              @ATE_GARANTE_CEDULA as varchar(10),                    
                              @ATE_GARANTE_PARENTESCO as varchar(50),                    
                              @ATE_GARANTE_MONTO_GARANTIA as numeric(9),                    
                              @ATE_GARANTE_TELEFONO as nchar(20),                    
                              @ATE_GARANTE_DIRECCION as varchar(100),                    
                              @ATE_GARANTE_CIUDAD as varchar(20),                    
                              @ATE_DIAGNOSTICO_INICIAL as varchar(255),                    
                              @ATE_DIAGNOSTICO_FINAL as varchar(255),                    
                              @ATE_OBSERVACIONES as varchar(255),                    
                              @ATE_ESTADO as bit,                    
                              @ATE_FACTURA_NOMBRE as varchar(30),                    
                              @ATE_DIRECTORIO as varchar(250),                    
                              @DAP_CODIGO as int,                    
                              @HAB_CODIGO as smallint,                    
                              @CAJ_CODIGO as smallint,                    
                              @TIA_CODIGO as smallint,                    
                              @TIR_CODIGO as smallint,                    
                              @AFL_CODIGO as smallint,                    
                              @MED_CODIGO as int,                    
                              @TIP_CODIGO as smallint,                    
                              @TIF_CODIGO as smallint,                    
                     @TIF_OBSERVACION as varchar(255),                    
  @ATE_NUMERO_ADMISION as int,                    
                              @ATE_EN_QUIROFANO as bit,                    
                              @FOR_PAGO as int,                    
                              @ATE_QUIEN_ENTREGA_PAC as varchar(80),                    
                              @ATE_CIERRE_HC as bit,                    
                              @ATE_FEC_ING_HABITACION as datetime,                    
                              @ESC_CODIGO as int,                    
                              @CUE_ESTADO as varchar(20),                    
                              @TipoAtencion as varchar(128)                         
                         )                    
                                            
                         as                    
                         begin                    
                         /* Variables Locales*/          
                              declare @SecuencialNumeroAtencion as BigInt                    
                              declare @SecuencialCodigoAtencion as BigInt                    
                              declare @NumeroAtencion as int                               
                         /************************************************************/       
                         /* NUMERO ATENCION */         
                              select @SecuencialNumeroAtencion = MAX(CAST(ATE_NUMERO_ATENCION AS BIGINT) ) + 1 from ATENCIONES   --Tomo el numero de control    
                              update NUMERO_CONTROL set NUMCON = (SELECT MAX(CAST(@ATE_NUMERO_ATENCION AS BIGINT) ) + 1 from ATENCIONES) where CODCON=8  --ACTUALIZA # DE CONTROL           
                         /* CODIGO ATENCION */     --ES EL MISMO NUMERO DE ATENCION QUE ESTA EN NUMERO DE CONTROL, PARA QUE OTRA CONSULTA??    
                              select @SecuencialCodigoAtencion= (isnull(MAX(ATENCIONES.ATE_CODIGO),0)+1) from [his3000]..ATENCIONES   -- GENERA EL SECUENCIAL DEL CODIGO DE ATENCION / GIOVANNY TAPIA / 26/10/2012                  
                         /* NUMERO DE ATENCION, nos da el numero de atencion del paciente*/    
                              select @NumeroAtencion=ISNULL(COUNT(*),0)+1 from [his3000]..ATENCIONES where PAC_CODIGO=@PAC_CODIGO  -- GENERA EL SECUENCIAL DEL CODIGO DE ATENCION / GIOVANNY TAPIA / 26/10/2012                  
                         /************************************************************/       
                         if @Nuevo=1 -- VERIFICO SI ES UNA NUEVA ATENCION                
                         begin                
                         declare @SecuencialDatosAdicionales as int                    
                         SELECT @SecuencialDatosAdicionales=ISNULL(MAX(DAP_CODIGO),0)+1 FROM [his3000]..PACIENTES_DATOS_ADICIONALES  -- GENERA EL SECUENCIAL DE DATOS ADICIONALES DE UN PACIENTE     
                         insert into [his3000]..PACIENTES_DATOS_ADICIONALES  -- INSERTA LOS DATOS ADICIONALES DEL PACIENTE     
                         values                    
                         (                    
                         @SecuencialDatosAdicionales,                    
                         GETDATE(),                    
                         57,                    
                         17,                    
                         1701,                    
                         null,                    
                         null,                    
                         @DireccionPaciente ,                    
                         @TelefonoPaciente,                    
                         @CelularPaciente,                    
                         NULL,                    
                         NULL,                    
                         NULL,                    
                         NULL,                    
                         NULL,   
                         NULL,                    
                         1,                    
                         1,                    
                         1,                    
                         @ID_USUSARIO,                    
        @PAC_CODIGO,                    
                         1,                    
                         NULL                    
                         )                    
                         end                      
                         else -- CASO CONTRARIO RECUPERO EL CODIGO DE LOS DATOS ADICIONALES                
                         begin                
                         select top 1 @SecuencialDatosAdicionales = DAP_CODIGO  from [his3000]..PACIENTES_DATOS_ADICIONALES where PAC_CODIGO=@PAC_CODIGO                
                         end                
                                                           
                         INSERT INTO [dbo].[ATENCIONES]    
                                   ([ATE_CODIGO]    
                                   ,[ATE_NUMERO_ATENCION]    
                                   ,[ATE_FECHA]    
                                   ,[ATE_NUMERO_CONTROL]    
                                   ,[ATE_FACTURA_PACIENTE]    
                                   ,[ATE_FACTURA_FECHA]    
                                   ,[ATE_FECHA_INGRESO]    
                                   ,[ATE_FECHA_ALTA]    
                                   ,[ATE_REFERIDO]    
                                   ,[ATE_REFERIDO_DE]    
                                   ,[ATE_EDAD_PACIENTE]    
                                   ,[ATE_ACOMPANANTE_NOMBRE]    
                                   ,[ATE_ACOMPANANTE_CEDULA]    
                                   ,[ATE_ACOMPANANTE_PARENTESCO]    
                                   ,[ATE_ACOMPANANTE_TELEFONO]    
                                   ,[ATE_ACOMPANANTE_DIRECCION]    
                                   ,[ATE_ACOMPANANTE_CIUDAD]    
                                   ,[ATE_GARANTE_NOMBRE]    
                                   ,[ATE_GARANTE_CEDULA]    
                                   ,[ATE_GARANTE_PARENTESCO]    
                                   ,[ATE_GARANTE_MONTO_GARANTIA]    
                                   ,[ATE_GARANTE_TELEFONO]    
                                   ,[ATE_GARANTE_DIRECCION]    
                                   ,[ATE_GARANTE_CIUDAD]    
                                   ,[ATE_DIAGNOSTICO_INICIAL]    
                                   ,[ATE_DIAGNOSTICO_FINAL]    
                                   ,[ATE_OBSERVACIONES]    
                                   ,[ATE_ESTADO]    
                                   ,[ATE_FACTURA_NOMBRE]    
                                   ,[ATE_DIRECTORIO]    
                                   ,[PAC_CODIGO]    
                                   ,[DAP_CODIGO]    
                                   ,[HAB_CODIGO]    
                                   ,[CAJ_CODIGO]    
                                   ,[TIA_CODIGO]    
                                   ,[ID_USUSARIO]    
                                   ,[TIR_CODIGO]    
                                   ,[AFL_CODIGO]    
                                   ,[MED_CODIGO]    
                                   ,[TIP_CODIGO]    
                                   ,[TIF_CODIGO]    
                                   ,[TIF_OBSERVACION]    
                                   ,[ATE_NUMERO_ADMISION]    
                                   ,[ATE_EN_QUIROFANO]    
                                   ,[FOR_PAGO]    
                                   ,[ATE_QUIEN_ENTREGA_PAC]    
                                   ,[ATE_CIERRE_HC]    
                                   ,[ATE_FEC_ING_HABITACION]    
                                   ,[ESC_CODIGO]    
                                   ,[CUE_ESTADO]    
                                   ,[TipoAtencion]    
                                   ,[ate_discapacidad]    
             ,[ate_carnet_conadis]    
                                   ,[ATE_ID_ACCIDENTE]    
                                   ,[idTipoDescuento])    
                              VALUES    
                                   (@SecuencialCodigoAtencion,                    
                         @SecuencialNumeroAtencion,                    
                         GETDATE(),              
                         @ATE_NUMERO_CONTROL,                    
                         @ATE_FACTURA_PACIENTE,                    
                         /*@ATE_FACTURA_FECHA*/null,                    
                         getdate()/*@ATE_FECHA_INGRESO*/,                    
                         getdate()/*@ATE_FECHA_ALTA*/,                    
                         @ATE_REFERIDO,                    
                         @ATE_REFERIDO_DE,                    
                         @ATE_EDAD_PACIENTE,                    
                         @ATE_ACOMPANANTE_NOMBRE,                    
                         @ATE_ACOMPANANTE_CEDULA,                    
                         @ATE_ACOMPANANTE_PARENTESCO,                    
                         @ATE_ACOMPANANTE_TELEFONO,                    
                         @ATE_ACOMPANANTE_DIRECCION,                    
                         @ATE_ACOMPANANTE_CIUDAD,                    
                         @ATE_GARANTE_NOMBRE,                    
                         @ATE_GARANTE_CEDULA,                    
                         @ATE_GARANTE_PARENTESCO,                    
                         @ATE_GARANTE_MONTO_GARANTIA,                    
                         @ATE_GARANTE_TELEFONO,                    
                         @ATE_GARANTE_DIRECCION,                    
                         @ATE_GARANTE_CIUDAD,                    
                         @ATE_DIAGNOSTICO_INICIAL,                    
                         @ATE_DIAGNOSTICO_FINAL,                    
                         @ATE_OBSERVACIONES,                    
                         1,                    
                         @ATE_FACTURA_NOMBRE,                    
                         @ATE_DIRECTORIO,                    
                         @PAC_CODIGO,                    
                         @SecuencialDatosAdicionales,                    
                         0,                    
                         1,                    
                         4/*@TIA_CODIGO*/,                    
                         @ID_USUSARIO,                    
                         @TIR_CODIGO/*Tipo referido*/,                    
                         1 /*@AFL_CODIGO*/,                    
                         @MED_CODIGO,                    
                         4/*@TIP_CODIGO*/,                    
                         1/*@TIF_CODIGO*/,                    
                         @TIF_OBSERVACION,                    
                         @NumeroAtencion,                    
                         0/*@ATE_EN_QUIROFANO*/,                    
                         null/*@FOR_PAGO*/,                    
                         @ATE_QUIEN_ENTREGA_PAC,                    
                         @ATE_CIERRE_HC,                    
                         null,                    
                         @ESC_CODIGO,                    
                         0/*@CUE_ESTADO*//* aumentar al momento de actualizar TipoAtencion*/  ,                    
                         @TipoAtencion,    
                         0,    
                         null,    
                         null,    
                         1                  
                         )     /*                 
                         PRINT @SecuencialDatosAdicionales                    
                                   */        
                         select @SecuencialCodigoAtencion, @NumeroAtencion, @SecuencialNumeroAtencion, @ATE_NUMERO_CONTROL  -- Retorna el numero de atencion generada / GIOVANNY TAPIA / 26/10/2012                  
                                            
                                            
                                            
                         end
						 
-----------------------------------------------------------				 
						 
CREATE TABLE ATENCION_SINTOMAS
(AS_CODIGO BIGINT IDENTITY(1,1) NOT NULL,
ATE_CODIGO BIGINT,
SC_CODIGO BIGINT)
----------------------------------------------------------------------------
CREATE PROCEDURE sp_GuardaAtencionSintomas
@ate_codigo bigint,
@sc_codigo bigint
AS
INSERT INTO ATENCION_SINTOMAS VALUES(@ate_codigo, @sc_codigo)
GO