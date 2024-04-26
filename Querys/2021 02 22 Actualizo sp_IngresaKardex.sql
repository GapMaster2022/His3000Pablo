USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_IngresaKardex]    Script Date: 22/02/2021 11:25:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_IngresaKardex]
(

	@codUsuario int,
	@cue_codigo bigint,
	@ateCodigo bigint,
	@presentacion varchar(500),
	@via varchar(500),
	@dosis varchar(500),
	@frecuencia varchar(500),
	@hora time(7),
	@eventual bit,
	@medPropio bit,
	@fechaAdmi date

)
AS
BEGIN
	SET @ateCodigo=(SELECT ATE_CODIGO FROM ATENCIONES WHERE ATE_NUMERO_ATENCION=@ateCodigo)
	INSERT INTO KARDEXMEDICAMENTOS (IdUsuario,AteCodigo,CueCodigo,Presentacion,Dosis,Frecuencia,Via,Hora,FechaAdministración,MedicamentoEventual,MedicamentoPropio)VALUES 
	(

		@codUsuario,
		@ateCodigo,
		@cue_codigo,
		@presentacion,
		@dosis,
		@frecuencia,
		@via,
		@hora,
		@fechaAdmi,
		@eventual,
		@medPropio

	)

END