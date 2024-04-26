USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_HabitacionNombre]    Script Date: 14/04/2021 12:58:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_HabitacionNombre](
	@codHabitacion  as int
)
AS
BEGIN

	SELECT N.NIV_NOMBRE FROM HABITACIONES H, NIVEL_PISO N WHERE H.NIV_CODIGO=N.NIV_CODIGO AND H.hab_Codigo=@codHabitacion

END






USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_VerificaH008]    Script Date: 14/04/2021 13:02:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_VerificaH008](
	@ate_codigo int
)
AS
BEGIN

	SELECT * FROM ATENCIONES A, HC_EMERGENCIA_FORM E
	WHERE A.ATE_CODIGO=E.ATE_CODIGO AND A.ATE_CODIGO=@ate_codigo

END






USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_DtoPacientesAtencionesActivas_1]    Script Date: 15/04/2021 08:27:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_DtoPacientesAtencionesActivas_1]  
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
	
			 A.ESC_CODIGO=1  or a.ESC_CODIGO = 2
			 and a.ATE_ESTADO = 1

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
	
			 A.ESC_CODIGO=1 or a.ESC_CODIGO = 2
			 and  
			 H.NIV_CODIGO=NP.NIV_CODIGO
			 AND NP.NIV_CODIGO = @NIVEL_MAQUINA
			 and a.ATE_ESTADO = 1

			    

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
	
			A.ESC_CODIGO=1 or a.ESC_CODIGO = 2 	and  a.ESC_CODIGO = 1 and
			 H.NIV_CODIGO=NP.NIV_CODIGO
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
	
			A.ESC_CODIGO=1 or a.ESC_CODIGO = 2 	and  a.ESC_CODIGO = 1 	and  
			H.NIV_CODIGO=NP.NIV_CODIGO
			 AND NP.NIV_CODIGO IN (7,8,9,13)

   

		ORDER BY h.hab_Numero asc      

	END
	 set rowcount 0      
END






USE [His3000]
GO
/***** Object:  StoredProcedure [dbo].[sp_AltaProgramada]    Script Date: 15/04/2021 09:28:55 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_AltaProgramada]
(
@ATE_CODIGO AS INT
)      
AS BEGIN  
    
UPDATE HABITACIONES SET HES_CODIGO=2, hab_fec_cambio_est=GETDATE()   
WHERE HAB_CODIGO=(SELECT HAB_CODIGO FROM ATENCIONES WHERE ATE_CODIGO=@ATE_CODIGO)

--UPDATE HABITACIONES_DETALLE SET HAD_FECHA_DISPONIBILIDAD=GETDATE() 
--WHERE HAB_CODIGO=(SELECT HAB_CODIGO FROM ATENCIONES WHERE ATE_CODIGO=@ATE_CODIGO) 

UPDATE ATENCIONES SET ESC_CODIGO=2, ATE_FECHA_ALTA=GETDATE(), ATE_ESTADO=1 WHERE ATE_CODIGO=@ATE_CODIGO

END




--- solo para la pasteur, alianza ya esta actualizada.
update ATENCIONES set ATE_ESTADO = 0 where ESC_CODIGO <> 1  and ATE_ESTADO = 1
--fin





CREATE PROCEDURE sp_ValidaParametroSIC
AS
BEGIN

	SELECT activado FROM Sic3000..ParametrosFactura WHERE codpar=208

END





USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_EvolucionEliminar]    Script Date: 15/04/2021 11:20:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_EvolucionEliminar]
@observacion varchar(200),
@id_usuario smallint,
@ate_codigo int,
@evo_codigo int,
@evd_codigo bigint
AS
BEGIN
	INSERT INTO HISTORIAS VALUES(GETDATE(), @observacion, @id_usuario, @ate_codigo)
	--DELETE FROM HC_EVOLUCION WHERE EVO_CODIGO = @evo_codigo
	DELETE FROM HC_EVOLUCION_DETALLE WHERE EVO_CODIGO = @evo_codigo and EVD_CODIGO = @evd_codigo
END





USE [His3000]
GO
/** Object:  StoredProcedure [dbo].[sp_CreaHonorarioDesdeTaridario]    Script Date: 11/11/2020 17:36:58 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_CreaHonorarioDesdeTaridario]
	@ateCodigo INT,
	@total decimal(12,3),
	@idUsuario int,
	@codMedico int,
	@hora datetime
AS BEGIN
	
	DECLARE @NUMPEDIDO BIGINT
	SET @NUMPEDIDO =(SELECT MAX(PED_CODIGO)+1 FROM PEDIDOS)
	INSERT INTO PEDIDOS VALUES
	(
		@NUMPEDIDO,
		16,
		0,
		'HONORARIOS DESDE TARIFARIO',
		2,
		GETDATE(),
		@idUsuario,
		@ateCodigo,
		3,
		1,
		@NUMPEDIDO,
		@codMedico,
		NULL
	)


	DECLARE @PED_DETALLE BIGINT
	SET @PED_DETALLE = (SELECT MAX(PDD_CODIGO)+1 FROM PEDIDOS_DETALLE)

	INSERT INTO PEDIDOS_DETALLE VALUES 
	(
		@PED_DETALLE,
		@NUMPEDIDO,
		2022,
		'HONORARIOS MEDICOS',
		1,
		@total,
		0,
		@total,
		1,
		0,
		NULL,
		0,
		NULL,
		NULL,
		2022
	)
	
	
	
	DECLARE @NUM BIGINT
	SET @NUM=(SELECT MAX(CUE_CODIGO)+1 FROM CUENTAS_PACIENTES)
	INSERT INTO CUENTAS_PACIENTES VALUES
	(
		@NUM,
		@ateCodigo,
		GETDATE(),
		'2022',
		'HONORARIOS MEDICOS',
		@total,
		1,
		@total,
		0.00,
		1,
		'0',
		28,
		16,
		@idUsuario,
		0,
		'2022',
		NULL,
		'HONORARIO CREADO DESDE MÓDULO TARIFARIO',
		@codMedico,
		NULL,
		@NUMPEDIDO,
		NULL,
		0.1,
		'SIN FACTURA',
		'N',
		0,
		0, 
		@idUsuario, 
		''
	)

	UPDATE HONORARIOS_TARIFARIO SET Codigo_Pedido=@NUMPEDIDO WHERE HON_FECHA=@hora AND MED_CODIGO=@codMedico AND HON_TOTAL=@total

END



USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_ValidaEstatusAtencion]    Script Date: 15/04/2021 16:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ValidaEstatusAtencion]
	@atencion bigint

AS BEGIN 
	declare @ATE_CODIGO bigint
	set @ATE_CODIGO = (select ATE_CODIGO from ATENCIONES where ATE_NUMERO_ATENCION=@atencion and ESC_CODIGO not in (6,13))
	
	--SELECT ate_codigo, Status FROM STATUS_ATENCION where Ate_Codigo=@ATE_CODIGO
	--union
	SELECT isnull(sum(CUE_CANTIDAD),0) as Cantidad FROM CUENTAS_PACIENTES where Ate_Codigo=@ATE_CODIGO

END



