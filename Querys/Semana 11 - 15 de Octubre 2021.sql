-- CREATE TABLE HONORARIOS_MEDICOS(
-- CODIGO BIGINT IDENTITY(1,1) NOT NULL,
-- HOM_CODIGO BIGINT, 
-- MED_CODIGO BIGINT,
-- ATE_CODIGO BIGINT, 
-- FOR_CODIGO INT,
-- ID_USUARIO BIGINT,
-- TMO_CODIGO INT,
-- HOM_FECHA DATETIME,
-- HOM_FACTURA_MEDICO NVARCHAR(20),
-- HOM_FECHA_FACTURA DATETIME,
-- HOM_VALOR_NETO FLOAT,
-- HOM_COMISION_CLINICA FLOAT,
-- HOM_APORTE_LLAMADA FLOAT,
-- HOM_RETENCION FLOAT,
-- HOM_POR_PAGAR FLOAT,
-- HOM_POR_RECUPERAR FLOAT,
-- HOM_VALOR_PAGADO FLOAT,
-- HOM_RECORTE FLOAT, 
-- HOM_NETO_PAGAR FLOAT, 
-- HOM_VALOR_CANCELADO FLOAT, HOM_VALOR_TOTAL FLOAT, HOM_LOTE NVARCHAR(20),
-- HOM_OBSERVACION NVARCHAR(1000), HOM_FUERA BIT, GENERADO_ASIENTO BIT, CAJA NVARCHAR(5), HOM_CUBIERTO FLOAT,
--HOM_ESTADO NVARCHAR(20), HOM_VALE NVARCHAR(50))

-----------------------------------------------------------------------------------------------------------------


-- CREATE PROCEDURE sp_HM_CreaAuditoria
-- @hom_codigo bigint,
-- @med_codigo bigint, 
-- @ate_codigo bigint,
-- @for_codigo int, 
-- @tmo_codigo int,
-- @usuario bigint,
-- @hom_fecha datetime,
-- @factura nvarchar(20),
-- @fecha_factura datetime,
-- @valor_neto float,
-- @comision float,
-- @aporte float,
-- @retencion float, 
-- @por_pagar float, 
-- @por_recuperar float, 
-- @valor_pagado float, 
-- @recorte float, 
-- @neto_pagar float, 
-- @cancelado float, @valor_total float, @lote nvarchar(20),
-- @observacion nvarchar(1000), @fuera bit, @asiento bit, @caja nvarchar(5),
-- @cubierto float, @estado nvarchar(100), @vale nvarchar(50)
-- AS

-- INSERT INTO SERIES3000_AUDITORIA..HONORARIOS_MEDICOS VALUES(
	-- @hom_codigo, @med_codigo, @ate_codigo, @for_codigo, @usuario, @tmo_codigo, GETDATE(), @factura,
	-- @fecha_factura, @valor_neto, @comision, @aporte, @retencion, @por_pagar, @por_recuperar, @valor_pagado, 
	-- @recorte, @neto_pagar, @cancelado, @valor_total, @lote, @observacion, @fuera, @asiento, @caja, @cubierto, @estado, @vale)
-- go
	


------------------------------------------------------------------------------------------------------------------------------



-- ALTER PROCEDURE [dbo].[sp_BuscarProductoSic3000_ServiciosPasteur]          
          
-- @p_Opcion as int,                        
-- @p_filtro as varchar(128),                        
-- @p_Division as int,                        
-- @p_Bodega as int,                        
-- @CodigoEmpresa as int,                        
-- @CodigoConvenio as int           
-- AS          
      
-- BEGIN          
 -- SELECT           
 -- nomlocal AS DIVISION,          
 -- P.codpro AS CODIGO,          
 -- despro AS PRODUCTO,          
 -- ISNULL(existe, 0) AS STOCK,          
 -- iva AS IVA,          
 -- isnull(dbo.f_precioConvenio(@CodigoConvenio,p.codpro),isnull(dbo.f_Precio(@CodigoConvenio,@CodigoEmpresa,p.codpro),0)) as VALOR,        
 -- CantDecimal AS Cantidad, p.clasprod        
 -- FROM Sic3000..Producto P        
 -- INNER JOIN Sic3000..ProductoSubdivision PD ON P.codsub = PD.codsub        
 -- INNER JOIN Sic3000..Bodega B ON P.codpro = B.codpro          
 -- INNER JOIN Sic3000..Locales L ON B.codbod = L.codlocal       
 -- WHERE PD.Pea_Codigo_His IN (SELECT RUB_CODIGO FROM RUBROS        
 -- WHERE RUB_ASOCIADO IN (7,2,5,6)) AND P.clasprod = 'S'         
 -- AND B.codbod = @p_Bodega      
 -- AND (despro LIKE '%' + @p_filtro + '%')          
 -- AND ISNULL(P.ACTIVO, 0) = 1         
 -- order by PRODUCTO       
       
 -- END     
 
 
 -----------------------------------------
 ALTER procedure [dbo].[sp_VerificaFactura]       
      
 @Ate_Codigo as int      
as       
begin      
select ate_factura_paciente, esc_codigo from atenciones        
where ATE_CODIGO = @Ate_Codigo      
end 

------------------------------------------
ALTER PROCEDURE [dbo].[sp_RecuperaMedicamentosKardex]  
(  
   
 @ate_codigo VARCHAR(10),  
 @rubro int,  
 @check int  
)  
AS  
BEGIN  
   
 IF(@check=0)  
 BEGIN  
  --SET @ate_codigo=(SELECT ATE_CODIGO FROM ATENCIONES WHERE ATE_NUMERO_ATENCION=@ate_codigo)  
  select CUE_CODIGO AS CODIGO, CUE_DETALLE AS PRODUCTO, cast(CUE_CANTIDAD as int) AS CANTIDAD  
  from CUENTAS_PACIENTES   
  where ATE_CODIGO=@ate_codigo AND RUB_CODIGO=@rubro AND   
    CUE_CODIGO NOT IN (SELECT CueCodigo FROM KARDEXMEDICAMENTOS WHERE AteCodigo=@ate_codigo) AND   
    CUE_CODIGO NOT IN (SELECT CueCodigo FROM KARDEXINSUMOS WHERE AteCodigo=@ate_codigo)  
 END  
 ELSE  
 BEGIN  
     SELECT P.codpro AS CODIGO, P.despro AS PRODUCTO,1 AS CANTIDAD FROM Sic3000.dbo.Producto P   
    INNER JOIN Sic3000.dbo.ProductoSubdivision ON P.codsub = Sic3000.dbo.ProductoSubdivision.codsub AND P.coddiv = Sic3000.dbo.ProductoSubdivision.coddiv   
    INNER JOIN dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO WHERE dbo.RUBROS.RUB_CODIGO=@rubro  
  
  
 END  
END  

-----------------------------------------------
ALTER PROCEDURE [dbo].[sp_RecuperaKardex]  
 @ateCodigo varchar(10)  
  
AS  
BEGIN   
 --set @ateCodigo = (select ATE_CODIGO from ATENCIONES where ATE_NUMERO_ATENCION = @ateCodigo)  
  
 SELECT * FROM KARDEXMEDICAMENTOS  
 where AteCodigo = @ateCodigo  
 order by 1 desc  
  
END  


-------------------------------------
alter table TRIAJE_CONSUTAEXTERNA
add No_Urgente2 smallint