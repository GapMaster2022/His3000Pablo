-------------------------------------------------------------------------------------------------------------------------------------------------------------------
create trigger tr_valida_stock_servicios
on Bodega
after update
as begin

	update Bodega set existe = 9999 where codpro in (select codpro from Producto where clasprod='S')

end
---------------------------------------------------------------------------------------27/02/2023----------------------------------------------------------------------------
ALTER TABLE HC_EVOLUCION_DETALLE ADD MED_TRATANTE INT DEFAULT 0 NOT NULL
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROCEDURE sp_Fechas_Medico_Evolucion
 @CODIGO BIGINT,
 @MEDICO BIGINT
AS
BEGIN
	SELECT a.FECHA_INICIO
	FROM (
		select 
			TOP 1
			FECHA_INICIO from HC_EVOLUCION_DETALLE 
			where EVO_CODIGO = @CODIGO and MED_TRATANTE = @MEDICO
			order by FECHA_INICIO 
	) as a
	UNION  
	SELECT b.FECHA_FIN 
	FROM (
		select 
			TOP 1
			FECHA_FIN from HC_EVOLUCION_DETALLE 
			where EVO_CODIGO = @CODIGO and MED_TRATANTE = @MEDICO
			order by FECHA_FIN desc
	) as b 
END
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------
create procedure sp_recuperar_Tarifarios_Cirugia
as begin
select TOP 100 TAD_REFERENCIA CODIGO, TRIM(UPPER(TAD_NOMBRE)) DESCRIPCION, TAD_UVR UVR from TARIFARIOS_DETALLE where EST_CODIGO in (
select EST_CODIGO from ESPECIALIDADES_TARIFARIOS where EST_CODIGO in (
select EST_CODIGO from ESPECIALIDADES_TARIFARIOS where EST_PADRE in (
select EST_CODIGO from ESPECIALIDADES_TARIFARIOS where EST_PADRE=34))
) AND TAD_REFERENCIA <> '' ORDER BY 1
end
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[REGISTRO_QUIROFANO](
	[id_registro_quirofano] [bigint] IDENTITY(1,1) NOT NULL,
	[ate_codigo] [bigint] NOT NULL,
	[fecha_registro] [datetime] NOT NULL,
	[cirujano] [int] NOT NULL,
	[anestecia] [int] NOT NULL,
	[hora_inicio] [time](7) NOT NULL,
	[hora_fin] [time](7) NOT NULL,
	[instrumentista] [int] NOT NULL,
	[ayudante] [int] NULL,
	[recuperacion] [bit] NOT NULL,
	[circulante] [int] NULL,
	[patologia] [int] NULL,
	[quirofano] [int] NOT NULL,
	[ayudantia] [int] NULL,
	[anasteciologo] [int] NOT NULL,
	[intervencion] [varchar](5000) NOT NULL,
	[tipo_Atencion] [bit] NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_REGISTRO_QUIROFANO] PRIMARY KEY CLUSTERED 
(
	[id_registro_quirofano] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
GO
-------------------------------------------------------------------------------------------------------------------------------------------------------------------