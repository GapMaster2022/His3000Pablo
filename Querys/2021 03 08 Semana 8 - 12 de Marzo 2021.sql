USE [His3000]
GO

/****** Object:  StoredProcedure [dbo].[sp_ActualizaEstadoHoja]    Script Date: 08/03/2021 11:56:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_ActualizaEstadoHoja]
@CodigoAtencion bigint
AS
UPDATE HC_EMERGENCIA_FORM SET EMER_ESTADO = NULL WHERE ATE_CODIGO = @CodigoAtencion
GO


---solo alianza

alter table HC_EMERGENCIA_FORM
add  EMER_OBSER_GENERAL VARCHAR(5000)
----fin


USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_GrabaTriajeConsultaExterna]    Script Date: 08/03/2021 16:27:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_EditaTriajeConsultaExterna]
(
	
	@lblHistoria VarChar(20),
	@lblAtencion BIGINT,
    @nourgente SMALLINT,
    @urgente SMALLINT,
	@critico SMALLINT,
	@muerto SMALLINT,
	@alcohol SMALLINT,
	@drogas SMALLINT,
	@otros SMALLINT,
	@txtOtrasActual VarChar(5000),
	@txtObserEnfer VarChar(5000)
	
)
AS
BEGIN
	
	UPDATE TRIAJE_CONSUTAEXTERNA SET No_urgente = @nourgente, Urgente = @urgente, Critico = @critico, 
	Muerto = @muerto, Alcohol = @alcohol, Drogas = @drogas, Otros = @otros, NotasOtros = @txtOtrasActual, 
	NotasEnfermeria = @txtObserEnfer WHERE ATE_CODIGO = @lblAtencion

END


USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_GrabaSignosVitalesConsultaExterna]    Script Date: 08/03/2021 16:42:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EditaSignosVitalesConsultaExterna]
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
	@cmb_ReacPIValor AS VARCHAR(20)

)
AS
BEGIN

	UPDATE SIGNOSVITALES_CONSULTAEXTERNA SET Presion1 = @txt_PresionA1, Presion2 = @txt_PresionA2, F_Cardiaca = @txt_FCardiaca,
	F_Respiratoria = @txt_FResp, T_Bucal = @txt_TBucal, T_Axilar = @txt_TAxilar, S_Oxigeno = @txt_SaturaO, PesoKG = @txt_PesoKG,
	TallaM = @txt_Talla, Ind_Masa = @txtIMCorporal, Perimetro_Cef = @txt_PerimetroC, Glisemia_Capilar = @txt_Glicemia,
	Glasgow = @txt_TotalG, Ocular = @cmb_Ocular, Verbal = @cmb_Verbal, Motora = @cmb_Motora, Diametro_Der = @txt_DiamPDV,
	Reaccion_Der = @cmb_ReacPDValor, Diametro_Iz = @txt_DiamPIV, Reaccion_Iz = @cmb_ReacPIValor WHERE ATE_CODIGO = @lblAtencion

END



USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_GrabaObstetricaConsultaExterna]    Script Date: 08/03/2021 16:49:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EditarObstetricaConsultaExterna]
(

	@lblHistoria AS VARCHAR(20),
	@lblAtencion BIGINT,
	@txt_Gesta AS SMALLINT,
	@txt_Partos AS SMALLINT,
	@txt_Abortos AS SMALLINT,
	@txt_Cesareas AS SMALLINT,
	@dtp_ultimaMenst1 AS DATE,
	@txt_SemanaG AS SMALLINT,
	@movFetal AS SMALLINT,
	@txt_FrecCF AS SMALLINT,
	@memRotas AS SMALLINT,
	@txt_Tiempo AS VARCHAR(10),
	@txt_AltU AS SMALLINT,
	@txt_Presentacion AS VARCHAR(10),
	@txt_Dilatacion AS SMALLINT,
	@txt_Borramiento AS SMALLINT,
	@txt_Plano AS VARCHAR(50),
	@pelvis AS SMALLINT,
	@sangrado AS SMALLINT,
	@txt_Contracciones AS VARCHAR(50)

)
AS
BEGIN

UPDATE OBSTETRICA_CONSULTAEXTERNA SET Gesta = @txt_Gesta, Partos = @txt_Partos, Abortos = @txt_Abortos, Cesarea = @txt_Cesareas,
F_Menstruacion = @dtp_ultimaMenst1, Semanas_Gestacion = @txt_SemanaG, Movimiento_Fetal = @movFetal, Mebrana_Rota = @memRotas,
Tiempo = @txt_Tiempo, Altura_Uterina = @txt_AltU, Presentacion = @txt_Presentacion, Dilatacion = @txt_Dilatacion,
Borramiento = @txt_Borramiento, Plano = @txt_Plano, Pelvis_Util = @pelvis, Sangrado_Vajinal = @sangrado, Contracciones = @txt_Contracciones
WHERE ATE_CODIGO = @lblAtencion


END




alter table HC_INTERCONSULTA
ADD HIN_CAMA_NUEVO VARCHAR(10)


alter table HC_INTERCONSULTA
ADD HIN_MEDICO VARCHAR(100)

alter table HC_INTERCONSULTA
ADD HIN_MEDICO_CODIGO VARCHAR(10)


