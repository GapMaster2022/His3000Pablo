--------------------------------------------------13-03-2023-----------------------------------------------------------------------------------
alter table PEDIDOS ADD IP_MAQUINA VARCHAR (50) DEFAULT '' 
GO
-----------------------------------------------------------------16-30-2023------------------------------------------------------------------------------
alter PROCEDURE [dbo].[sp_QuirofanoDetalleProducto]        
@desde datetime,        
@hasta datetime,  
@bodega int  
AS        
BEGIN        
        
SELECT P.codpro AS CODIGO, P.despro AS PRODUCTO, SUM( RQ.RQ_CANTIDAD) AS TOTAL,       
RQ.RQ_FECHACREACION AS 'F. PROCEDI', Convert(Varchar(10),RQ_FECHAPEDIDO,103)  AS 'F. PEDI', RQ_FECHAREPOSICION AS 'F. REPOSI',      
RQ.ATE_CODIGO, RQ.PCI_CODIGO,      
PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2      
+ ' - ' + PC.PCI_DESCRIPCION AS 'PACIENTE - PROCEDIMIENTO', rq.PED_CODIGO AS 'Nº PEDIDO', RQ_NUMREPOSICION AS 'Nº REPOSICION'      
FROM Sic3000..Producto P      
INNER JOIN REPOSICION_QUIROFANO RQ ON P.codpro = RQ.CODPRO      
INNER JOIN ATENCIONES A ON RQ.ATE_CODIGO = A.ATE_CODIGO      
INNER JOIN PACIENTES PA ON A.PAC_CODIGO = PA.PAC_CODIGO      
INNER JOIN PROCEDIMIENTOS_CIRUGIA PC ON RQ.PCI_CODIGO = PC.PCI_CODIGO   
inner join PEDIDOS pp on rq.PED_CODIGO = pp.PED_CODIGO
--inner join PEDIDOS_DETALLE pd on rq.PED_CODIGO = pd.PED_CODIGO
WHERE RQ_FECHAPEDIDO BETWEEN @desde AND @hasta and  
 RQ_FECHAREPOSICION IS NULL  and  (select top 1 PRO_BODEGA_SIC from PEDIDOS_DETALLE where PED_CODIGO = pp.PED_CODIGO)= @bodega
 --AND RQ.ID_USUARIO = @usuario  
GROUP BY P.codpro, P.despro, RQ_CANTIDAD, RQ_FECHACREACION, Convert(Varchar(10),RQ_FECHAPEDIDO,103), RQ_FECHAREPOSICION,      
PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2      
+ ' - ' + PC.PCI_DESCRIPCION, rq.PED_CODIGO, RQ_NUMREPOSICION, RQ.ATE_CODIGO, RQ.PCI_CODIGO      
ORDER BY 2 ASC      

END   
GO
-----------------------------------------------------------------16-30-2023------------------------------------------------------------------------------
  
ALTER PROCEDURE sp_Reposicion            
@desde datetime,            
@hasta datetime,        
@bodega int        
AS        
        
select SUM(CUE_CANTIDAD) as 'CUE_CANTIDAD', CUE_DETALLE,PRO_CODIGO        
from CUENTAS_PACIENTES c      
inner join Sic3000..Producto p on c.PRO_CODIGO = p.codpro      
where Codigo_Pedido in(select ped_codigo from REPOSICION_PENDIENTE rp        
inner join REGISTRO_QUIROFANO rq on rp.id_registro_quirofano = rq.id_registro_quirofano  
inner join INTERVENCIONES_REGISTRO_QUIROFANO iq on rq.id_registro_quirofano = iq.id_registro_quirofano  
where cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)>= @desde And cast(convert(varchar(11),rq.fecha_registro ,13) as datetime)<= @hasta and   
rp.estado = 0 and iq.general = @bodega) and p.clasprod = 'B'      
group by CUE_DETALLE,PRO_CODIGO 
-----------------------------------------------------------------16-30-2023------------------------------------------------------------------------------

CREATE TABLE [dbo].[INTERVENCIONES_REGISTRO_QUIROFANO](
	[id_intervenciones] [bigint] IDENTITY(1,1) NOT NULL,
	[id_registro_quirofano] [bigint] NOT NULL,
	[cie_10] [bigint] NOT NULL,
	[general] [int] NOT NULL,
 CONSTRAINT [PK_INTERVENCIONES_REGISTRO_QUIROFANO] PRIMARY KEY CLUSTERED 
(
	[id_intervenciones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

