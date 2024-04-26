------------------------------------------------------------10/07/2023----------------------------------------------------------------------------
alter table HC_SIGNOS_DATOS_ADICIONALES add ID_FRECUENCIA INT
GO
------------------------------------------------------------11/07/2023----------------------------------------------------------------------------
alter table KARDEXMEDICAMENTOS add usuario_modifica int 
GO
alter table KARDEXMEDICAMENTOS add fecha_modifica datetime
GO

CREATE TABLE ABREVIACIONES(
AB_CODIGO INT PRIMARY KEY IDENTITY(1,1),
AB_ABREVIACION VARCHAR(20),
AB_DESCRIPCION VARCHAR(100)
)
GO
------------------------------------------------------------14/07/2023----------------------------------------------------------------------------
CREATE TABLE PERFILES_PROTOCOLO(
PP_CODIGO INT PRIMARY KEY IDENTITY(1,1),
MED_CODIGO INT,
PP_NOMBRE_PERFIL varchar(250),
PP_DETALLE VARCHAR(500)
)
GO
------------------------------------------------------------14/07/2023----------------------------------------------------------------------------
insert into PARAMETROS_DETALLE values (53, 8, 'CxC HON.CUENTA.MEDICOS', null,'000000-000', 0)
GO

drop table HC_SIGNOS_VITALES
GO

USE [His3000]
GO

/****** Object:  Table [dbo].[HC_SIGNOS_VITALES]    Script Date: 19/07/2023 09:05:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HC_SIGNOS_VITALES](
	[SV_CODIGO] [int] IDENTITY(1,1) NOT NULL,
	[ATE_CODIGO] [bigint] NULL,
	[SV_DIA] [int] NULL,
	[SV_FECHA] [datetime] NULL,
	[SV_INTERACCION] [varchar](100) NULL,
	[SV_POSTQUIRURGICO] [varchar](100) NULL,
	[SV_ING_PARENTAL] [varchar](10) NULL,
	[SV_ING_ORAL] [varchar](10) NULL,
	[SV_ING_TOTAL] [varchar](10) NULL,
	[SV_ELM_ORINA] [varchar](10) NULL,
	[SV_ELM_DRENAJE] [varchar](10) NULL,
	[SV_ELM_OTROS] [varchar](10) NULL,
	[SV_ELM_TOTAL] [varchar](10) NULL,
	[SV_BAÑO] [varchar](20) NULL,
	[SV_PESO] [varchar](20) NULL,
	[SV_DIETA_ADMINISTRADA] [varchar](20) NULL,
	[SV_NUMERO_COMIDAS] [varchar](20) NULL,
	[SV_NUMERO_MEDICIONES] [varchar](20) NULL,
	[SV_NUMERO_DEPOSICIONES] [varchar](20) NULL,
	[SV_ACTIVIDAD_FISICA] [varchar](20) NULL,
	[SV_CAMBIO_SONDA] [varchar](20) NULL,
	[SV_RECANALIZACION] [varchar](20) NULL,
	[SV_RESPONSABLE] [varchar](20) NULL,
	[SV_PORCENTAJE] [varchar](10) NULL,
	[SV_HOJA] [int] NULL,
	[SV_RESPALDO_DIA] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SV_CODIGO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


