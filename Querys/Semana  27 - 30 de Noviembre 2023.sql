--------------------------------------------------27/11/2023-------------------------------------------------------------------------------------------------------
USE [His3000]
GO

/****** Object:  Table [dbo].[REPORTE_CURVA_TERMICA]    Script Date: 28/11/2023 09:58:06 ******/
SET ANSI_NULLS ON
GO
DROP TABLE [REPORTE_CURVA_TERMICA]
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[REPORTE_CURVA_TERMICA](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TEMPERATURA] FLOAT NULL,
	[HORA] [varchar](5) NULL,
 CONSTRAINT [PK_REPORTE_CURVA_TERMICA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
--------------------------------------------------30/11/2023-------------------------------------------------------------------------------------------------------
USE [His3000]
GO
DROP TABLE [REPORTE_SV]
GO
/****** Object:  Table [dbo].[REPORTE_SV]    Script Date: 30/11/2023 08:43:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[REPORTE_SV](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HORA] [nvarchar](50) NOT NULL,
	[PULSO] [float] NULL,
	[TEMPERATURA] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
