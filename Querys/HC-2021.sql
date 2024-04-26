USE His3000
GO
--------------------------------------------11/10/2023-------------------------------PasoFormulario 2021------------------------------------------------------------------------
alter table PACIENTES_DATOS_ADICIONALES add DAP_DIRECCION_DOMICILIO2 VARCHAR(200)
alter table PACIENTES_DATOS_ADICIONALES add DAP_REFERENCIA VARCHAR(200)
GO
--------------------------------------------19/10/2023-------------------------------PasoFormulario 2021------------------------------------------------------------------------
ALTER TABLE Form002MSP ADD endocrinoF VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD piel VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD pielsp VARCHAR(1) default'O' not null

ALTER TABLE Form002MSP ADD cardiopatiaP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD hipertencionP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD vascularP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD endocrinoP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD cancerP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD tuberculosisP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD mentalP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD infecciosaP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD malformacionP VARCHAR(1) default'O' not null
ALTER TABLE Form002MSP ADD otroP VARCHAR(1) default 'O' not null

ALTER TABLE Form002MSP ADD rPiel VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rOjos VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rOidos VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rNariz VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rBoca VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rOrofaringe VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rAxilas VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rColumna VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rIngle VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD rInferior VARCHAR(1) default 'O' not null

ALTER TABLE Form002MSP ADD sSentidos VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sRespiratorio VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sCardio VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sDigestivo VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sGenital VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sUrinario VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sMusculo VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sEndocrino VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sLimfatico VARCHAR(1) default 'O' not null
ALTER TABLE Form002MSP ADD sNeurologico VARCHAR(1) default 'O' not null

ALTER TABLE Form002MSP ADD diagnostico5 VARCHAR(150) default '' not null 	
ALTER TABLE Form002MSP ADD diagnostico5cie VARCHAR(10) default '' not null
ALTER TABLE Form002MSP ADD diagnostico5def VARCHAR(1) default 'O' not null	
ALTER TABLE Form002MSP ADD diagnostico5pre VARCHAR(1) default 'O' not null

ALTER TABLE Form002MSP ADD diagnostico6 VARCHAR(150) default '' not null 	
ALTER TABLE Form002MSP ADD diagnostico6cie VARCHAR(10) default '' not null
ALTER TABLE Form002MSP ADD diagnostico6def VARCHAR(1) default 'O' not null	
ALTER TABLE Form002MSP ADD diagnostico6pre VARCHAR(1) default 'O' not null

ALTER TABLE Form002MSP ADD MED_CODIGO VARCHAR(15)
ALTER TABLE Form002MSP ADD ID_USUARIO INT
GO
-----------------------------------------------31/01/2024 CREAR CAMPOS EN PREADMISION
ALTER TABLE PREADMISION ADD PRE_DIRECCION2 VARCHAR(200);
ALTER TABLE PREADMISION ADD PRE_REFERENCIA VARCHAR(200);

GO

---------------30/01/2024 MODIFICAR SP
ALTER procedure [dbo].[sp_DatosAtencionSimplificada]                  
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
                              @TipoAtencion as varchar(128),
                              @DIRECCION2 as varchar(250), 
                              @REFERENCIA as varchar(250)                     
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
                         NULL,@DIRECCION2,@REFERENCIA                
                         )                  
                         end                    
                         else -- CASO CONTRARIO RECUPERO EL CODIGO DE LOS DATOS ADICIONALES              
                         begin              
	                         select top 1 @SecuencialDatosAdicionales = DAP_CODIGO  from [his3000]..PACIENTES_DATOS_ADICIONALES where PAC_CODIGO=@PAC_CODIGO    
	                         
	                         update [his3000]..PACIENTES_DATOS_ADICIONALES   set DAP_DIRECCION_DOMICILIO2 = @DIRECCION2,  DAP_REFERENCIA = @REFERENCIA     where DAP_CODIGO = @SecuencialDatosAdicionales          
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
                         @TIP_CODIGO,                  
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
GO

---------------------------SE AUMETEN CAMPOS EN SIGNOSVITALES CONSULTA EXTERNA  01/02/2024  ----CRISTIAN
ALTER TABLE SIGNOSVITALES_CONSULTAEXTERNA add PerimetroA  DECIMAL(18,2);
ALTER TABLE SIGNOSVITALES_CONSULTAEXTERNA ADD Hemoglobina  DECIMAL(18,2);
ALTER TABLE SIGNOSVITALES_CONSULTAEXTERNA add ID_USUARIO  INT;
ALTER TABLE SIGNOSVITALES_CONSULTAEXTERNA add Glucosa_Capilar  DECIMAL(18,2);
GO
--------------------------------------se cambia sp para SIGNOSVITALES_CONSULTAEXTERNA ---editar -----CRISTIAN
ALTER PROCEDURE [dbo].[sp_EditaSignosVitalesConsultaExterna]
(

	@lblHistoria AS VARCHAR(20),
	@lblAtencion BIGINT,
	@txt_PresionA1 AS Decimal(18,2),
	@txt_PresionA2 AS Decimal(18,2),
	@txt_FCardiaca AS Decimal(18,2),
	@txt_FResp AS Decimal(18,2),
	@txt_TBucal AS Decimal(18,2),
	@txt_TAxilar AS Decimal(18,2),
	@txt_SaturaO AS Decimal(18,2),
	@txt_PesoKG AS Decimal(18,2),
	@txt_Talla AS Decimal(18,2),
	@txtIMCorporal AS Decimal(18,2),
	@txt_PerimetroC AS Decimal(18,2),
	@txt_Glicemia AS Decimal(18,2),
	@txt_TotalG AS Decimal(18,2),
	@cmb_Ocular AS SMALLINT, 
	@cmb_Verbal AS SMALLINT,
	@cmb_Motora AS SMALLINT,
	@txt_DiamPDV AS SMALLINT,
	@cmb_ReacPDValor AS VARCHAR(20),
	@txt_DiamPIV AS SMALLINT,
	@cmb_ReacPIValor AS VARCHAR(20),
	-------se aumenta para formularios 2021   ----- 01/02/2024
	@perimetro AS Decimal(18,2),
	@hemoglobina AS Decimal(18,2),
	@glucosa AS Decimal(18,2),
	@pulsioximetria AS Decimal(18,2),
	@id_usuario AS int
)
AS
BEGIN

	UPDATE SIGNOSVITALES_CONSULTAEXTERNA SET Presion1 = @txt_PresionA1, Presion2 = @txt_PresionA2, F_Cardiaca = @txt_FCardiaca,
	F_Respiratoria = @txt_FResp, T_Bucal = @txt_TBucal, T_Axilar = @txt_TAxilar, S_Oxigeno = @txt_SaturaO, PesoKG = @txt_PesoKG,
	TallaM = @txt_Talla, Ind_Masa = @txtIMCorporal, Perimetro_Cef = @txt_PerimetroC, Glisemia_Capilar = @txt_Glicemia,
	Glasgow = @txt_TotalG, Ocular = @cmb_Ocular, Verbal = @cmb_Verbal, Motora = @cmb_Motora, Diametro_Der = @txt_DiamPDV,
	Reaccion_Der = @cmb_ReacPDValor, Diametro_Iz = @txt_DiamPIV, Reaccion_Iz = @cmb_ReacPIValor ,
	PerimetroA = @perimetro,Hemoglobina = @hemoglobina,ID_USUARIO = @id_usuario	
	
	WHERE ATE_CODIGO = @lblAtencion

END
GO

------------------cambio sp de grabar signos vitales consulta externa   01/02/2024   
ALTER PROCEDURE [dbo].[sp_GrabaSignosVitalesConsultaExterna]
(

	@lblHistoria AS VARCHAR(20),
	@lblAtencion BIGINT,
	@txt_PresionA1 AS Decimal(18,2),
	@txt_PresionA2 AS Decimal(18,2),
	@txt_FCardiaca AS Decimal(18,2),
	@txt_FResp AS Decimal(18,2),
	@txt_TBucal AS Decimal(18,2),
	@txt_TAxilar AS Decimal(18,2),
	@txt_SaturaO AS Decimal(18,2),
	@txt_PesoKG AS Decimal(18,2),
	@txt_Talla AS Decimal(18,2),
	@txtIMCorporal AS Decimal(18,2),
	@txt_PerimetroC AS Decimal(18,2),
	@txt_Glicemia AS Decimal(18,2),
	@txt_TotalG AS Decimal(18,2),
	@cmb_Ocular AS SMALLINT, 
	@cmb_Verbal AS SMALLINT,
	@cmb_Motora AS SMALLINT,
	@txt_DiamPDV AS SMALLINT,
	@cmb_ReacPDValor AS VARCHAR(20),
	@txt_DiamPIV AS SMALLINT,
	@cmb_ReacPIValor AS VARCHAR(20),    	-------se aumenta para formularios 2021   ----- 01/02/2024
	@perimetro AS Decimal(18,2),
	@hemoglobina AS Decimal(18,2),
	@glucosa AS Decimal(18,2),
	@pulsioximetria AS Decimal(18,2),
	@id_usuario AS int
)
AS
BEGIN

	DECLARE @ID_AGENDA_PACIENTE AS BIGINT
	SET @ID_AGENDA_PACIENTE=(SELECT ID_AGENDAMIENTO FROM AGENDA_PACIENTE WHERE Identificacion=
							(SELECT PAC_IDENTIFICACION FROM PACIENTES WHERE PAC_HISTORIA_CLINICA=@lblHistoria))
	IF(@ID_AGENDA_PACIENTE IS NULL)
	BEGIN
		SET @ID_AGENDA_PACIENTE=1
	END
	INSERT INTO SIGNOSVITALES_CONSULTAEXTERNA VALUES
	(
	@ID_AGENDA_PACIENTE,
	@lblAtencion,
	@txt_PresionA1,
	@txt_PresionA2,
	@txt_FCardiaca,
	@txt_FResp,
	@txt_TBucal,
	@txt_TAxilar,
	@txt_SaturaO,
	@txt_PesoKG,
	@txt_Talla,
	@txtIMCorporal,
	@txt_PerimetroC,
	@txt_Glicemia,
	@txt_TotalG,
	@cmb_Ocular, 
	@cmb_Verbal,
	@cmb_Motora,
	@txt_DiamPDV,
	@cmb_ReacPDValor,
	@txt_DiamPIV,
	@cmb_ReacPIValor,
	@perimetro,
	@hemoglobina,
	@id_usuario
	)

END

GO

-----------------------------------------------------------------------01/02/2024------------------------------------------------------------------------------------------------
alter PROCEDURE [dbo].[REGISTRO_CAMBIOS] (@codigoPaciente int)    
AS    
SELECT * FROM (  
  
SELECT 1 AS N_ORDEN,    
 (SELECT DA.DAP_CODIGO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS DAP_CODIGO,    
 (SELECT DA.DAP_FECHA_INGRESO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS FECHA,    
 (SELECT E.ESC_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ESTADO_CIVIL AS E ON E.ESC_CODIGO = DA.ESC_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS ESTADO_CIVIL,    
 (SELECT DA.DAP_INSTRUCCION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS INSTRUCCION,    
 (SELECT DA.DAP_OCUPACION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS OCUPACION,    
 (SELECT DA.DAP_EMP_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS EMPRESA,    
 (SELECT top 1 TR.TIR_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ATENCIONES AS A ON A.DAP_CODIGO = DA.DAP_CODIGO INNER JOIN TIPO_REFERIDO AS TR ON TR.TIR_CODIGO = A.TIR_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) aS TIPO_SEGURO,    
 (SELECT DA.DAP_DIRECCION_DOMICILIO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS DIRECCION,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS BARRIO,    
 (SELECT TL.TILO_SIMBOLO FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC INNER JOIN TIPO_LOCALIDAD AS TL ON TL.TILO_CODIGO = DP.TILO_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = 
@codigoPaciente) AS ZONA,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PARROQUIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS PARROQUIA,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_CANTON = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS CANTON,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PROVINCIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS PROVINCIA,    
 (SELECT DA.DAP_TELEFONO1 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 1 AND DA.PAC_CODIGO = @codigoPaciente) AS TELEFONO ,   
  (SELECT DA.DAP_DIRECCION_DOMICILIO2 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS CALLESECUNARIA,  
 (SELECT DA.DAP_REFERENCIA FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS REFERENCIA    
 UNION    
SELECT 2 AS N_ORDEN,    
 (SELECT DA.DAP_CODIGO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS DAP_CODIGO,    
 (SELECT DA.DAP_FECHA_INGRESO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS FECHA,    
 (SELECT E.ESC_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ESTADO_CIVIL AS E ON E.ESC_CODIGO = DA.ESC_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS ESTADO_CIVIL,    
 (SELECT DA.DAP_INSTRUCCION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS INSTRUCCION,    
 (SELECT DA.DAP_OCUPACION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS OCUPACION,    
 (SELECT DA.DAP_EMP_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS EMPRESA,    
 (SELECT top 1 TR.TIR_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ATENCIONES AS A ON A.DAP_CODIGO = DA.DAP_CODIGO INNER JOIN TIPO_REFERIDO AS TR ON TR.TIR_CODIGO = A.TIR_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS TIPO_SEGURO,    
 (SELECT DA.DAP_DIRECCION_DOMICILIO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS DIRECCION,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS BARRIO,    
 (SELECT TL.TILO_SIMBOLO FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC INNER JOIN TIPO_LOCALIDAD AS TL ON TL.TILO_CODIGO = DP.TILO_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = 
@codigoPaciente) AS ZONA,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PARROQUIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS PARROQUIA,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_CANTON = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS CANTON,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PROVINCIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS PROVINCIA,    
 (SELECT DA.DAP_TELEFONO1 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS TELEFONO,  
 (SELECT DA.DAP_DIRECCION_DOMICILIO2 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS CALLESECUNARIA,  
 (SELECT DA.DAP_REFERENCIA FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS REFERENCIA      
 UNION    
SELECT 3 AS N_ORDEN,    
 (SELECT DA.DAP_CODIGO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS DAP_CODIGO,    
 (SELECT DA.DAP_FECHA_INGRESO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS FECHA,    
 (SELECT E.ESC_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ESTADO_CIVIL AS E ON E.ESC_CODIGO = DA.ESC_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS ESTADO_CIVIL,    
 (SELECT DA.DAP_INSTRUCCION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS INSTRUCCION,    
 (SELECT DA.DAP_OCUPACION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS OCUPACION,    
(SELECT DA.DAP_EMP_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS EMPRESA,    
 (SELECT top 1 TR.TIR_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ATENCIONES AS A ON A.DAP_CODIGO = DA.DAP_CODIGO INNER JOIN TIPO_REFERIDO AS TR ON TR.TIR_CODIGO = A.TIR_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS TIPO_SEGURO,    
 (SELECT DA.DAP_DIRECCION_DOMICILIO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS DIRECCION,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS BARRIO,    
 (SELECT TL.TILO_SIMBOLO FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC INNER JOIN TIPO_LOCALIDAD AS TL ON TL.TILO_CODIGO = DP.TILO_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = 
@codigoPaciente) AS ZONA,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PARROQUIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS PARROQUIA,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_CANTON = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS CANTON,    
 (SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PROVINCIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS PROVINCIA,    
 (SELECT DA.DAP_TELEFONO1 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 3 AND DA.PAC_CODIGO = @codigoPaciente) AS TELEFONO,    
 (SELECT DA.DAP_DIRECCION_DOMICILIO2 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS CALLESECUNARIA,  
 (SELECT DA.DAP_REFERENCIA FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS REFERENCIA      
   
 ) AS Resultado  
   
ORDER BY   
    CASE WHEN FECHA IS NULL THEN 1 ELSE 0 END, -- Las filas con FECHA nula se marcarán con 1 y se colocarán al final  
    FECHA asc;   
  
;  
   
 --UNION    
--SELECT 4 AS N_ORDEN,    
 --(SELECT DA.DAP_CODIGO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS DAP_CODIGO,    
 --(SELECT DA.DAP_FECHA_INGRESO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS FECHA,    
 --(SELECT E.ESC_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ESTADO_CIVIL AS E ON E.ESC_CODIGO = DA.ESC_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS ESTADO_CIVIL,    
 --(SELECT DA.DAP_INSTRUCCION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS INSTRUCCION,    
 --(SELECT DA.DAP_OCUPACION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS OCUPACION,    
 --(SELECT DA.DAP_EMP_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS EMPRESA,    
 --(SELECT top 1 TR.TIR_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ATENCIONES AS A ON A.DAP_CODIGO = DA.DAP_CODIGO INNER JOIN TIPO_REFERIDO AS TR ON TR.TIR_CODIGO = A.TIR_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente)   
--AS TIPO_SEGURO,    
 --(SELECT DA.DAP_DIRECCION_DOMICILIO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS DIRECCION,    
 --(SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS BARRIO,    
 --(SELECT TL.TILO_SIMBOLO FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC INNER JOIN TIPO_LOCALIDAD AS TL ON TL.TILO_CODIGO = DP.TILO_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS ZONA,    
 --(SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PARROQUIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS PARROQUIA,    
 --(SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_CANTON = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS CANTON,    
 --(SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PROVINCIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS PROVINCIA,    
 --(SELECT DA.DAP_TELEFONO1 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 4 AND DA.PAC_CODIGO = @codigoPaciente) AS TELEFONO,  
 --(SELECT DA.DAP_DIRECCION_DOMICILIO2 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS CALLESECUNARIA,  
 --(SELECT DA.DAP_REFERENCIA FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS REFERENCIA    
 --SELECT 5 AS N_ORDEN,    
 --(SELECT DA.DAP_CODIGO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS DAP_CODIGO,    
 --(SELECT DA.DAP_FECHA_INGRESO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS FECHA,    
 --(SELECT E.ESC_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ESTADO_CIVIL AS E ON E.ESC_CODIGO = DA.ESC_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS ESTADO_CIVIL,    
 --(SELECT DA.DAP_INSTRUCCION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS INSTRUCCION,    
-- (SELECT DA.DAP_OCUPACION FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS OCUPACION,    
 --(SELECT DA.DAP_EMP_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS EMPRESA,    
-- (SELECT top 1 TR.TIR_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ATENCIONES AS A ON A.DAP_CODIGO = DA.DAP_CODIGO INNER JOIN TIPO_REFERIDO AS TR ON TR.TIR_CODIGO = A.TIR_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente)   
--AS TIPO_SEGURO,    
 --(SELECT DA.DAP_DIRECCION_DOMICILIO FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS DIRECCION,    
 --(SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS BARRIO,    
 --(SELECT TL.TILO_SIMBOLO FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC INNER JOIN TIPO_LOCALIDAD AS TL ON TL.TILO_CODIGO = DP.TILO_CODIGO WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS ZONA,    
 --(SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PARROQUIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS PARROQUIA,    
 --(SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_CANTON = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS CANTON,    
 --(SELECT DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PROVINCIA = DP.DIPO_CODIINEC WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS PROVINCIA,    
 --(SELECT DA.DAP_TELEFONO1 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 5 AND DA.PAC_CODIGO = @codigoPaciente) AS TELEFONO,  
  --(SELECT DA.DAP_DIRECCION_DOMICILIO2 FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS CALLESECUNARIA,  
 --(SELECT DA.DAP_REFERENCIA FROM PACIENTES_DATOS_ADICIONALES AS DA WHERE DA.DAP_NUMERO_REGISTRO = 2 AND DA.PAC_CODIGO = @codigoPaciente) AS REFERENCIA

GO
-------------------02/02/2024  CAMBIOS REGISTRO DE CAMBIOS  ADMISION   CRISTIAN RUIZ
ALTER PROCEDURE [dbo].[REGISTRO_CAMBIOS] (@codigoPaciente int)    
AS    

SELECT TOP 3 * FROM ( 

select DA.PAC_CODIGO,DA.DAP_CODIGO,DA.DAP_FECHA_INGRESO
  ,DA.DAP_INSTRUCCION,DA.DAP_OCUPACION,DA.DAP_EMP_NOMBRE
  ,DA.DAP_DIRECCION_DOMICILIO,
  (SELECT TOP 1 E.ESC_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ESTADO_CIVIL AS E ON E.ESC_CODIGO = DA.ESC_CODIGO WHERE DA.PAC_CODIGO = 12424) AS ESTADO_CIVIL,
  (SELECT TOP 1 TR.TIR_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ATENCIONES AS A ON A.DAP_CODIGO = DA.DAP_CODIGO INNER JOIN TIPO_REFERIDO AS TR ON TR.TIR_CODIGO = A.TIR_CODIGO WHERE DA.PAC_CODIGO = 12424 ORDER BY A.ATE_FECHA DESC) AS TIPO_SEGURO,
  (SELECT TOP 1 DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC WHERE DA.PAC_CODIGO = 12424) AS BARRIO,  
  (SELECT TOP 1 TL.TILO_SIMBOLO FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC INNER JOIN TIPO_LOCALIDAD AS TL ON TL.TILO_CODIGO = DP.TILO_CODIGO WHERE DA.PAC_CODIGO = 12424) AS ZONA,    
  (SELECT TOP 1 DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PARROQUIA = DP.DIPO_CODIINEC WHERE DA.PAC_CODIGO = 12424) AS PARROQUIA,    
  (SELECT TOP 1 DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_CANTON = DP.DIPO_CODIINEC WHERE DA.PAC_CODIGO = 12424) AS CANTON,    
  (SELECT TOP 1 DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PROVINCIA = DP.DIPO_CODIINEC WHERE DA.PAC_CODIGO = 12424) AS PROVINCIA,    
  
  DA.DAP_TELEFONO1,DA.DAP_DIRECCION_DOMICILIO2,DA.DAP_REFERENCIA
  from 
  
  PACIENTES_DATOS_ADICIONALES DA

  WHERE  DA.PAC_CODIGO = @codigoPaciente
 ) AS Resultado  


GO

 -----------------------------------------------------------------------02/02/2024------------------------------------------------------------------------------------------------

alter table PACIENTES_DATOS_ADICIONALES2 ADD NACIONA_ETNICA VARCHAR(100)
alter table PACIENTES_DATOS_ADICIONALES2 ADD PUEBLO VARCHAR(100)
alter table PACIENTES_DATOS_ADICIONALES2 ADD PAC_ESTADO_NIVEDU VARCHAR(150)
alter table PACIENTES_DATOS_ADICIONALES2 ADD PAC_TIP_EMPRESA VARCHAR(150)
alter table PACIENTES_DATOS_ADICIONALES2 ADD PAC_SEG_SALUD VARCHAR(150)
alter table PACIENTES_DATOS_ADICIONALES2 ADD PAC_TIP_BONO VARCHAR(150)
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
alter table PACIENTES ADD PAC_ESTADO_NIVEDU VARCHAR(150)
alter table PACIENTES ADD PAC_TIP_EMPRESA VARCHAR(150)
alter table PACIENTES ADD PAC_SEG_SALUD VARCHAR(150)
alter table PACIENTES ADD PAC_TIP_BONO VARCHAR(150)

go
-------------------02/02/2024  CAMBIOS REGISTRO DE CAMBIOS  ADMISION   CRISTIAN RUIZ
USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[REGISTRO_CAMBIOS]    Script Date: 05/02/2024 12:22:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[REGISTRO_CAMBIOS] (@codigoPaciente int)    
AS    

SELECT TOP 3 * FROM ( 

select DA.PAC_CODIGO,DA.DAP_CODIGO,DA.DAP_FECHA_INGRESO
  ,DA.DAP_INSTRUCCION,DA.DAP_OCUPACION,DA.DAP_EMP_NOMBRE
  ,DA.DAP_DIRECCION_DOMICILIO,
  (SELECT TOP 1 E.ESC_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ESTADO_CIVIL AS E ON E.ESC_CODIGO = DA.ESC_CODIGO WHERE DA.PAC_CODIGO = @codigoPaciente) AS ESTADO_CIVIL,
  (SELECT TOP 1 TR.TIR_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN ATENCIONES AS A ON A.DAP_CODIGO = DA.DAP_CODIGO INNER JOIN TIPO_REFERIDO AS TR ON TR.TIR_CODIGO = A.TIR_CODIGO WHERE DA.PAC_CODIGO = @codigoPaciente ORDER BY A.ATE_FECHA DESC) AS TIPO_SEGURO,
  (SELECT TOP 1 DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC WHERE DA.PAC_CODIGO = @codigoPaciente) AS BARRIO,  
  (SELECT TOP 1 TL.TILO_SIMBOLO FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_SECTOR = DP.DIPO_CODIINEC INNER JOIN TIPO_LOCALIDAD AS TL ON TL.TILO_CODIGO = DP.TILO_CODIGO WHERE DA.PAC_CODIGO = @codigoPaciente) AS ZONA,    
  (SELECT TOP 1 DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PARROQUIA = DP.DIPO_CODIINEC WHERE DA.PAC_CODIGO = @codigoPaciente) AS PARROQUIA,    
  (SELECT TOP 1 DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_CANTON = DP.DIPO_CODIINEC WHERE DA.PAC_CODIGO = @codigoPaciente) AS CANTON,    
  (SELECT TOP 1 DP.DIPO_NOMBRE FROM PACIENTES_DATOS_ADICIONALES AS DA INNER JOIN DIVISION_POLITICA AS DP ON DA.COD_PROVINCIA = DP.DIPO_CODIINEC WHERE DA.PAC_CODIGO = @codigoPaciente) AS PROVINCIA,    
  
  DA.DAP_TELEFONO1,DA.DAP_DIRECCION_DOMICILIO2,DA.DAP_REFERENCIA,
   (SELECT TOP 1 DA2.PAC_SEG_SALUD  FROM  PACIENTES_DATOS_ADICIONALES2 DA2 WHERE DA2.PAC_CODIGO = @codigoPaciente)  AS PAC_SEG_SALUD
  from 
  
  PACIENTES_DATOS_ADICIONALES DA

  WHERE  DA.PAC_CODIGO = @codigoPaciente
 ) AS Resultado  order by 3 desc

GO
---------------------------------------------------------------------------------------------------------------------------------------------------------

alter PROCEDURE [dbo].[sp_DatosPacienteSimplificada]        
                    (        
                    @PAC_CODIGO as int,        
                    @PAC_HISTORIA_CLINICA as nchar(20),        
                    @ID_USUARIO as smallint,        
                    @DIPO_CODIINEC as varchar(20),        
                    @E_CODIGO as smallint ,        
                    @PAC_FECHA_CREACION as datetime,        
                    @PAC_NOMBRE1 as varchar(50),        
                    @PAC_NOMBRE2 as varchar(50),        
                    @PAC_APELLIDO_PATERNO as varchar(50),        
                    @PAC_APELLIDO_MATERNO as varchar(50),        
                    @PAC_FECHA_NACIMIENTO as datetime,        
                    @PAC_NACIONALIDAD as varchar(25),        
                    @PAC_TIPO_IDENTIFICACION as varchar(1),        
                    @PAC_IDENTIFICACION as varchar(13),        
                    @PAC_EMAIL as varchar(50),        
                    @PAC_GENERO as char(1),        
                    @PAC_IMAGEN as varchar(120),        
                    @PAC_ESTADO as bit,        
                    @PAC_DIRECTORIO as varchar(250),        
                    @PAC_REFERENTE_NOMBRE as nchar(120),        
                    @PAC_REFERENTE_PARENTESCO as nchar(40),        
                    @PAC_REFERENTE_TELEFONO as nchar(20),        
                    @PAC_ALERGIAS as varchar(250),        
                    @PAC_OBSERVACIONES as varchar(500),        
                    @GS_CODIGO as smallint,        
                    @PAC_REFERENTE_DIRECCION as varchar(250),        
                    @PAC_DATOS_INCOMPLETOS as bit,
					@PAC_ESTADO_NIVEDU	as varchar(250),
					@PAC_TIP_EMPRESA as varchar(250),
					@PAC_SEG_SALUD as varchar(250),
					@PAC_TIP_BONO as varchar(250)
                    )        
                    as        
                    begin        
                           
                    insert into PACIENTES values        
                    (        
                    @PAC_CODIGO,        
                    @PAC_HISTORIA_CLINICA,        
                    @ID_USUARIO,        
                    @DIPO_CODIINEC,        
                    @E_CODIGO,        
                    @PAC_FECHA_CREACION,        
                    @PAC_NOMBRE1,        
                    @PAC_NOMBRE2,        
                    @PAC_APELLIDO_PATERNO,        
                    @PAC_APELLIDO_MATERNO,        
                    @PAC_FECHA_NACIMIENTO,        
                    @PAC_NACIONALIDAD,        
                    @PAC_TIPO_IDENTIFICACION,        
                    @PAC_IDENTIFICACION,        
                    @PAC_EMAIL,        
                    @PAC_GENERO,        
                    @PAC_IMAGEN,        
                    @PAC_ESTADO,        
                    @PAC_DIRECTORIO,        
                    @PAC_REFERENTE_NOMBRE,        
                    @PAC_REFERENTE_PARENTESCO,        
                    @PAC_REFERENTE_TELEFONO,        
                    @PAC_ALERGIAS,        
                    @PAC_OBSERVACIONES,        
                    @GS_CODIGO,        
                    @PAC_REFERENTE_DIRECCION,        
                    @PAC_DATOS_INCOMPLETOS,
					@PAC_ESTADO_NIVEDU,
					@PAC_TIP_EMPRESA,
					@PAC_SEG_SALUD,
					@PAC_TIP_BONO
                    )        
                    end  

