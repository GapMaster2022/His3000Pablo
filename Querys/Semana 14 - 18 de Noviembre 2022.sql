--------------------------------------------------16-11-2022---------------------------------------------------------------------------------------
USE [His3000]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PERFILES_LABORATORIO](
	[PL_CODIGO] [int] IDENTITY(1,1) NOT NULL,
	[PL_PERFIL] [nvarchar](100) NULL,
	[PL_CANTIDAD] [int] NULL
) ON [PRIMARY]
GO
------------------------------------------------------------------------------------------------------------------------------------------------
CREATE TABLE [dbo].[SicProductoSubdivision](
	[codsub] [int] NOT NULL,
	[dessub] [nvarchar](200) NULL,
	[coddiv] [nvarchar](10) NULL,
	[Pea_Codigo_His] [int] NOT NULL
) ON [PRIMARY]
GO
