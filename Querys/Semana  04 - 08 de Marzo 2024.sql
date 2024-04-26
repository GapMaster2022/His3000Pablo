USE [His3000]
GO

CREATE TABLE [dbo].[CUENTAS_PACIENTES_COPAGO](
	[CUC_CODIGO] [bigint] NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[CUE_CODIGO] [bigint] NOT NULL,
	[ATE_CODIGO] [int] NULL,
	[CUE_FECHA] [datetime] NULL,
	[PRO_CODIGO] [varchar](15) NULL,
	[CUE_DETALLE] [varchar](1000) NULL,
	[CUE_VALOR_UNITARIO] [decimal](12, 3) NULL,
	[CUE_CANTIDAD] [decimal](12, 4) NULL,
	[CUE_VALOR] [decimal](12, 3) NULL,
	[CUE_IVA] [decimal](12, 4) NULL,
	[CUE_ESTADO] [bigint] NULL,
	[CUE_NUM_FAC] [varchar](15) NULL,
	[RUB_CODIGO] [smallint] NULL,
	[PED_CODIGO] [int] NULL,
	[ID_USUARIO] [smallint] NULL,
	[CAT_CODIGO] [int] NULL,
	[PRO_CODIGO_BARRAS] [varchar](15) NULL,
	[CUE_NUM_CONTROL] [varchar](15) NULL,
	[CUE_OBSERVACION] [varchar](5000) NULL,
	[MED_CODIGO] [int] NULL,
	[CUE_ORDER_IMPRESION] [int] NULL,
	[Codigo_Pedido] [bigint] NULL,
	[Id_Tipo_Medico] [int] NULL,
	[COSTO] [float] NOT NULL,
	[NumVale] [varchar](20) NULL,
	[DivideFactura] [varchar](1) NOT NULL,
	[Descuento] [float] NOT NULL,
	[PorDescuento] [float] NOT NULL,
	[USUARIO_FACTURA] [int] NOT NULL,
	[FECHA_FACTURA] [datetime] NOT NULL,
)
GO 

create table COPAGO(
COP_CODIGO BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
ATE_CODIGO BIGINT NOT NULL,
ATE_CODIGO_COPAGO BIGINT NOT NULL
)



