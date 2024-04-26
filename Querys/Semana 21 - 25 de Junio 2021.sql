--SOLO PARA LA ALIANZA


select * from CERTIFICADO_MEDICO

ALTER TABLE CERTIFICADO_MEDICO
ADD CER_ACTIVIDAD_LABORAL NVARCHAR(100)

ALTER TABLE CERTIFICADO_MEDICO
ADD CER_CONTINGENCIA NVARCHAR(500)

ALTER TABLE CERTIFICADO_MEDICO
ADD CER_TRATAMIENTO NVARCHAR(100)

ALTER TABLE CERTIFICADO_MEDICO
ADD CER_FECHA_CIRUGIA DATETIME DEFAULT GETDATE()

ALTER TABLE CERTIFICADO_MEDICO
ADD CER_PROCEDIMIENTO NVARCHAR(500)

ALTER TABLE CERTIFICADO_MEDICO
ADD CER_TIPO_INGRESO INT

ALTER TABLE CERTIFICADO_MEDICO
ADD CER_ESTADO BIT DEFAULT 1



USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_Certificado_InsertarPaciente]    Script Date: 21/06/2021 16:08:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_Certificado_InsertarPaciente]
@ate_codigo int,
@med_codigo int,
@observacion varchar(200),
@reposo int,
@actividad nvarchar(100),
@contingencia nvarchar(500),
@tratamiento nvarchar(100),
@procedimiento nvarchar(500),
@ingreso int
AS
INSERT INTO CERTIFICADO_MEDICO(ATE_CODIGO, MED_CODIGO, CER_OBSERVACION, CER_FECHA, 
CER_DIAS_REPOSO, CER_ACTIVIDAD_LABORAL, CER_CONTINGENCIA, CER_TRATAMIENTO, CER_PROCEDIMIENTO,
CER_TIPO_INGRESO)
VALUES (@ate_codigo, @med_codigo, @observacion, GETDATE(), @reposo, 
@actividad, @contingencia, @tratamiento, @procedimiento, @ingreso)
GO



USE [His3000]
GO
/***** Object:  StoredProcedure [dbo].[sp_DetallePorItem]    Script Date: 21/06/2021 18:17:31 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_DetallePorItem]  
  
 @ateCodigo AS BIGINT  
  
AS  
BEGIN  

 select ROUND(sum(CUE_CANTIDAD),2) AS CANTIDAD, CUE_DETALLE as DETALLE, 
 ROUND(SUM(CUE_IVA),4), ROUND(sum(cue_valor),2) AS TOTAL, R.RUB_NOMBRE
 from CUENTAS_PACIENTES, RUBROS R  
 where ATE_CODIGO=@ateCodigo  and r.RUB_CODIGO = CUENTAS_PACIENTES.RUB_CODIGO
 group by PRO_CODIGO, CUE_DETALLE, R.RUB_NOMBRE
 order by R.RUB_NOMBRE, 2 asc 
  
END



--DESACTIVADO PARA LA ALIANZA
INSERT INTO PARAMETROS VALUES(16, 0, 'HABITACIONES', 'VALIDA DEVOLUCION DE BIENES', 0)
INSERT INTO PARAMETROS_DETALLE VALUES(27, 16, 'HABITACIONES', NULL, 'VALIDA DEVOLUCION DE BIENES', 0)


CREATE PROCEDURE sp_ParametroDevolucionBienes
AS
SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 27
GO



CREATE PROCEDURE sp_QuirofanoActulizarCantidades
@codpro nvarchar(15),
@pci_codigo int,
@cantidad int,
@ate_codigo bigint
AS
UPDATE QUIROFANO_PROCE_PRODU SET QPP_CANTIDAD = @cantidad 
WHERE PCI_CODIGO = @pci_codigo AND CODPRO = @codpro
AND ATE_CODIGO = @ate_codigo
GO



alter PROCEDURE [dbo].[sp_QuirofanoMostrarProcedimientoPaciente]  
@cie_codigo bigint,  
@atencion int  
AS  
IF EXISTS (SELECT * FROM QUIROFANO_PROCE_PRODU WHERE PCI_CODIGO = @cie_codigo AND ATE_CODIGO = @atencion AND QPP_ORDEN IS NOT NULL)  
BEGIN  
 SELECT QPP.CODPRO AS Codigo, P.despro AS Producto, QP.QP_GRUPO AS Grupo,  
 B.existe AS Stock, QPP.QPP_CANTIDAD AS 'Cant. Original',   
 QPP.QPP_CANT_ADICIONAL AS 'Cant. Adicional',   
 ISNULL((SELECT TOP 1 PDD.DevDetCantidad FROM CUENTAS_PACIENTES CP  
 INNER JOIN PEDIDO_DEVOLUCION PD ON CP.Codigo_Pedido = PD.Ped_Codigo  
 INNER JOIN PEDIDO_DEVOLUCION_DETALLE PDD ON PD.DevCodigo = PDD.DevCodigo AND PDD.PRO_CODIGO = QPP.CODPRO  
 and cp.ATE_CODIGO = @atencion  
 ) , 0) AS 'Devoluciones',  
 QPP.QPP_FECHA AS Fecha, QPP.QPP_ORDEN, QPP_USUARIO as Usuario, p.preven  
 FROM QUIROFANO_PROCE_PRODU QPP  
 INNER JOIN QUIROFANO_PRODUCTOS QP ON QPP.CODPRO = QP.CODPRO  
 INNER JOIN Sic3000.dbo.Producto P ON QP.CODPRO = P.codpro  
 INNER JOIN Sic3000.dbo.Bodega B ON P.codpro = B.codpro  
 WHERE QPP.PCI_CODIGO = @cie_codigo AND QPP.ATE_CODIGO = @atencion AND B.codbod = 12
 order by p.despro asc
END  
ELSE  
BEGIN  
 SELECT QPP.CODPRO AS Codigo, P.despro AS Producto, QP.QP_GRUPO AS Grupo,  
 B.existe AS Stock, QPP.QPP_CANTIDAD AS 'Cant. Original',   
 QPP.QPP_CANT_ADICIONAL AS 'Cant. Adicional', 0 AS 'Devoluciones',  
 QPP.QPP_FECHA AS Fecha, QPP.QPP_ORDEN, QPP_USUARIO as Usuario, P.preven  
 FROM QUIROFANO_PROCE_PRODU QPP  
 INNER JOIN QUIROFANO_PRODUCTOS QP ON QPP.CODPRO = QP.CODPRO  
 INNER JOIN Sic3000.dbo.Producto P ON QP.CODPRO = P.codpro  
 INNER JOIN Sic3000.dbo.Bodega B ON P.codpro = B.codpro  
 WHERE QPP.PCI_CODIGO = @cie_codigo AND QPP.ATE_CODIGO IS NULL AND B.codbod = 12  
 order by p.despro asc
END  



-- alter TRIGGER CONTROLAIVA  
  
-- ON dbo.CUENTAS_PACIENTES  
  
-- AFTER INSERT   
  
-- AS  
  
-- begin  
  
-- declare @aux as varchar(max)  
  
   -- --select @aux=MAX(CUE_CODIGO) from CUENTAS_PACIENTES  
  
   -- --update cuentas_pacientes set cue_iva=ROUND((((cue_valor_unitario*cue_cantidad)*12)/100),2,0)  
  
   -- --where CUE_CODIGO=@aux and rub_codigo=27  
  
-- end  



USE [His3000]
GO
/***** Object:  StoredProcedure [dbo].[sp_DetallePorItem]    Script Date: 21/06/2021 18:17:31 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_DetallePorItem]  
  
 @ateCodigo AS BIGINT  
  
AS  
BEGIN  

 select ROUND(sum(CUE_CANTIDAD),2) AS CANTIDAD, CUE_DETALLE as DETALLE, 
 ROUND(SUM(CUE_IVA),4), ROUND(sum(cue_valor),2) AS TOTAL, R.RUB_NOMBRE
 from CUENTAS_PACIENTES, RUBROS R  
 where ATE_CODIGO=@ateCodigo  and r.RUB_CODIGO = CUENTAS_PACIENTES.RUB_CODIGO
 group by PRO_CODIGO, CUE_DETALLE, R.RUB_NOMBRE
 order by R.RUB_NOMBRE, 2 asc 
  
END



CREATE PROCEDURE sp_QuirofanoEliminaRegistro
--ELIMINA EL PRODUCTO DEL PROCEDIMIENTO QUE NO SE HA USADO POR EL USUARIO
@codpro nvarchar(15),
@ate_codigo bigint,
@pci_codigo int
AS
DELETE FROM QUIROFANO_PROCE_PRODU WHERE CODPRO = @codpro AND PCI_CODIGO = @pci_codigo AND ATE_CODIGO = @ate_codigo
GO



CREATE TABLE REPOSICION_QUIROFANO(
RQ_CODIGO BIGINT IDENTITY(1,1)NOT NULL,
ATE_CODIGO INT,
PCI_CODIGO BIGINT,
RQ_CANTIDAD INT,
RQ_CANTIDADADICIONAL INT DEFAULT 0,
RQ_CANTIDADDEVOLUCION INT DEFAULT 0,
RQ_FECHACREACION DATETIME,
RQ_FECHAPEDIDO DATETIME DEFAULT GETDATE(),
RQ_FECHAREPOSICION DATETIME,
PED_CODIGO INT, 
CODPRO NVARCHAR(15),
RQ_NUMREPOSICION BIGINT DEFAULT NULL,
ID_USUARIO INT DEFAULT NULL)


CREATE PROCEDURE sp_DatosReposicion  
@ate_codigo bigint,  
@pci_codigo bigint,  
@cantidad int,  
@fechacreacion datetime,  
@ped_codigo int,   
@codpro nvarchar(15) ,
@usuario int
AS  
  
INSERT INTO REPOSICION_QUIROFANO(ATE_CODIGO, PCI_CODIGO, RQ_CANTIDAD  
, RQ_FECHACREACION,PED_CODIGO, CODPRO, ID_USUARIO) VALUES (@ate_codigo, @pci_codigo, @cantidad, 
@fechacreacion, @ped_codigo, @codpro, @usuario)  
GO



ALTER PROCEDURE [dbo].[sp_QuirofanoDetalleProducto]      
@desde datetime,      
@hasta datetime,
@usuario int
AS      
BEGIN      
      
SELECT P.codpro AS CODIGO, P.despro AS PRODUCTO, SUM( RQ.RQ_CANTIDAD) AS TOTAL,     
RQ.RQ_FECHACREACION AS 'F. PROCEDI', Convert(Varchar(10),RQ_FECHAPEDIDO,103)  AS 'F. PEDI', RQ_FECHAREPOSICION AS 'F. REPOSI',    
RQ.ATE_CODIGO, RQ.PCI_CODIGO,    
PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2    
+ ' - ' + PC.PCI_DESCRIPCION AS 'PACIENTE - PROCEDIMIENTO', PED_CODIGO AS 'Nº PEDIDO', RQ_NUMREPOSICION AS 'Nº REPOSICION'    
FROM Sic3000..Producto P    
INNER JOIN REPOSICION_QUIROFANO RQ ON P.codpro = RQ.CODPRO    
INNER JOIN ATENCIONES A ON RQ.ATE_CODIGO = A.ATE_CODIGO    
INNER JOIN PACIENTES PA ON A.PAC_CODIGO = PA.PAC_CODIGO    
INNER JOIN PROCEDIMIENTOS_CIRUGIA PC ON RQ.PCI_CODIGO = PC.PCI_CODIGO    
WHERE RQ_FECHAPEDIDO BETWEEN @desde AND @hasta    
AND RQ_FECHAREPOSICION IS NULL  AND RQ.ID_USUARIO = @usuario
GROUP BY P.codpro, P.despro, RQ_CANTIDAD, RQ_FECHACREACION, Convert(Varchar(10),RQ_FECHAPEDIDO,103), RQ_FECHAREPOSICION,    
PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2    
+ ' - ' + PC.PCI_DESCRIPCION, PED_CODIGO, RQ_NUMREPOSICION, RQ.ATE_CODIGO, RQ.PCI_CODIGO    
ORDER BY 2 ASC    
END 


--HAY QUE ACTUALIZAR CON EL ACCESS Y EL REPORTE DE HOJA DE ALTA 



create PROCEDURE sp_Reposicion  
@desde datetime,  
@hasta datetime,
@usuario int
AS  
  
SELECT P.codpro AS CODIGO,  P.despro AS PRODUCTO, SUM(RQ_CANTIDAD) AS CANTIDAD  
FROM Sic3000..Producto P  
INNER JOIN REPOSICION_QUIROFANO RQ ON P.codpro = RQ.CODPRO  
WHERE RQ_FECHAREPOSICION IS NULL AND RQ_FECHAPEDIDO BETWEEN @desde AND @hasta  
AND ID_USUARIO = @usuario
GROUP BY P.codpro,  P.despro  
ORDER BY 2 ASC  
go

CREATE PROCEDURE sp_FechaReposicion
@fechareposicion datetime,
@ate_codigo bigint,
@pci_codigo int,
@numdoc bigint
AS
UPDATE REPOSICION_QUIROFANO SET RQ_FECHAREPOSICION = @fechareposicion, RQ_NUMREPOSICION = @numdoc WHERE ATE_CODIGO = @ate_codigo AND PCI_CODIGO = @pci_codigo
GO


CREATE PROCEDURE sp_QuirofanoLiberaControl
AS

Update Sic3000..Numero_Control set ocupado=0 where codcon =44 --LIBERA EL NUMERO DE CONTROL
Update Sic3000..Numero_Control set Numcon=Numcon+1 where codcon =44 -- SUMA EL NUMERO ANTERIOR POR 1 
GO




CREATE PROCEDURE sp_ReposicionSic3000
@numdoc bigint, 
@fecha date,
@hora nvarchar(50),
@bodegaOrigen float,
@bodegaDestino float,
@observacion nvarchar(500),
@estado nchar(1),
@usuario float
AS
INSERT INTO Sic3000..CabPedido(NUMERO_PEDIDO, FECHA_PEDIDO,HORA_PEDIDO, BODEGA_ORIGEN,BODEGA_DESTINO,DESCRIPCION_PEDIDO,ESTADO,codusu)
VALUES(  @numdoc, @fecha, @hora, @bodegaOrigen, @bodegaDestino, @observacion, @estado, @usuario);
GO


alter PROCEDURE sp_DetalleReposicion  
@codpro nvarchar(15),  
@despro nvarchar(500),  
@cant float,  
@linea int,   
@numdoc bigint  
AS  
DECLARE @id_ped bigint  
SET @id_ped = (SELECT MAX(ID_PEDIDO) AS IDPEDIDO FROM Sic3000..CabPedido where NUMERO_PEDIDO= @numdoc)  
  
insert into Sic3000..DetPedido (ID_PEDIDO,CODPRO,DESPRO,CANTIDAD_PEDIDO,CANTIDAD_RECIBIDA,ESTADO,LINEA   )  
 values(  @id_ped, @codpro, @despro, @cant, 0, 'A', @linea ) --Se crea el detalle del producto de reposicion  
  
Update Sic3000..bodega set reservado=isnull(reservado,0)+@cant   where codpro=@codpro and codbod=10 --Se reserva el producto en bodega  
--como defecto la bodega 10  



ALTER PROCEDURE sp_QuirofanoDetalleProductoExportar
@desde datetime,
@hasta datetime
AS
BEGIN

	SELECT QPP.CODPRO AS CODIGO, P.despro AS PRODUCTO,
	(SUM(QPP_CANTIDAD) + SUM(QPP_CANTIDAD_ORIGINAL)+ SUM(QPP_CANT_ADICIONAL)) AS TOTAL
	FROM Sic3000..Producto P 
	INNER JOIN QUIROFANO_PROCE_PRODU QPP ON P.codpro = QPP.CODPRO
	WHERE ATE_CODIGO IS NOT NULL AND QPP_FECHA BETWEEN @desde and @hasta
	AND QPP_CIERRE = 1
	GROUP BY QPP.CODPRO, P.despro
	ORDER BY 2 ASC
END
GO

--FIN DE ALIANZA


--PARA TODOS

ALTER PROCEDURE [dbo].[sp_ListaPedidoPacienteTodosRubros]  
(  
 @codigoAtencion AS INT  
  
)  
AS  
BEGIN   
  
 SELECT c.Codigo_Pedido PED_CODIGO, c.RUB_CODIGO PEA_CODIGO, c.PRO_CODIGO PEE_CODIGO, c.CUE_DETALLE PED_DESCRIPCION, c.CUE_FECHA PED_FECHA,  
 c.CUE_CANTIDAD, u.ID_USUARIO, u.APELLIDOS + ' ' + u.NOMBRES USUARIO, p.PAC_HISTORIA_CLINICA HISTORIA_CLINICA, a.ATE_CODIGO,  
 p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2 PACIENTE, c.CUE_CODIGO PDD_CODIGO,  
 c.PRO_CODIGO, c.CUE_DETALLE PRO_DESCRIPCION, CONCAT(m.MED_APELLIDO_PATERNO, +' '+ m.MED_NOMBRE1) MEDICO
 , ISNULL((SELECT PDD.DevDetCantidad
 FROM PEDIDO_DEVOLUCION PD
 INNER JOIN PEDIDO_DEVOLUCION_DETALLE PDD ON PD.DevCodigo = PDD.DevCodigo
 WHERE C.Codigo_Pedido = PD.Ped_Codigo AND C.PRO_CODIGO = PDD.PRO_CODIGO), 0) AS 'CANT. DEV'
 FROM ATENCIONES a, PACIENTES p, CUENTAS_PACIENTES c, USUARIOS u, MEDICOS m 
 WHERE a.PAC_CODIGO = p.PAC_CODIGO AND a.ATE_CODIGO = c.ATE_CODIGO AND c.ID_USUARIO = u.ID_USUARIO AND  
 c.MED_CODIGO = m.MED_CODIGO 
 AND c.ATE_CODIGO = @codigoAtencion
 order by c.PED_CODIGO, c.CUE_FECHA desc  
  
END



alter PROCEDURE [dbo].[sp_ListaPedidoPaciente]  
(  
  
 @codigoArea AS INT,  
 @codigoAtencion AS INT  
  
)  
AS  
BEGIN   
  
 SELECT c.Codigo_Pedido PED_CODIGO, c.RUB_CODIGO PEA_CODIGO, c.PRO_CODIGO PEE_CODIGO, c.CUE_DETALLE PED_DESCRIPCION, c.CUE_FECHA PED_FECHA,  
 u.ID_USUARIO, u.APELLIDOS + ' ' + u.NOMBRES USUARIO, p.PAC_HISTORIA_CLINICA HISTORIA_CLINICA, a.ATE_CODIGO,  
 p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2 PACIENTE, c.CUE_CODIGO PDD_CODIGO,  
 c.PRO_CODIGO, c.CUE_DETALLE PRO_DESCRIPCION, c.CUE_CANTIDAD, CONCAT(m.MED_APELLIDO_PATERNO, +' '+ m.MED_NOMBRE1) MEDICO,
 ISNULL((SELECT PDD.DevDetCantidad
 FROM PEDIDO_DEVOLUCION PD
 INNER JOIN PEDIDO_DEVOLUCION_DETALLE PDD ON PD.DevCodigo = PDD.DevCodigo
 WHERE C.Codigo_Pedido = PD.Ped_Codigo AND C.PRO_CODIGO = PDD.PRO_CODIGO), 0) AS 'CANT. DEV'
 FROM ATENCIONES a, PACIENTES p, CUENTAS_PACIENTES c, USUARIOS u, MEDICOS m  
 WHERE a.PAC_CODIGO = p.PAC_CODIGO AND a.ATE_CODIGO = c.ATE_CODIGO AND c.ID_USUARIO = u.ID_USUARIO AND  
 c.MED_CODIGO = m.MED_CODIGO AND c.RUB_CODIGO = @codigoArea AND c.ATE_CODIGO = @codigoAtencion  
 order by c.PED_CODIGO, c.CUE_FECHA desc  
  
END

alter PROCEDURE [dbo].[sp_GuardaFechasEvolucionMedica]  
(  
 @nuevaEvolucion INT,  
 @fechaInicio DATETIME,  
 @fechaFin DATETIME,  
 @evdescripcion varchar(200) ,
 @docs varchar(100)
)  
  
AS BEGIN  
  
  UPDATE HC_EVOLUCION_DETALLE SET FECHA_INICIO=@fechaInicio, FECHA_FIN=@fechaFin  
 WHERE EVD_CODIGO=@nuevaEvolucion  
  
  UPDATE HC_EVOLUCION SET NOM_USUARIO = @docs where EVO_CODIGO = (select EVO_CODIGO from HC_EVOLUCION_DETALLE where EVD_CODIGO = @nuevaEvolucion)
END


--FIN TODOS




--HAY QUE TOMAR EN CUENTA QUE ESTO SOLO ES PARA LA PASTEUR NO EN LA ALIANZA PORQUE DARA MUCHOS PROBLEMAS

alter procedure [dbo].[sp_BuscaProductoSic3000all]                
(                
@p_Opcion as int,                
@p_filtro as varchar(128),                
@p_Division as int,                
@p_Bodega as int,                
@CodigoEmpresa as int,                
@CodigoConvenio as int                
)                                              
as                                              
begin                                              
 if (@p_Opcion=1) -- BUSCA POR DATOS                                              
 begin                                              
  select     top 100                                          
  NomLocal as DIVISION,                                              
  p.codpro as CODIGO,                                              
  despro as PRODUCTO,                                              
  isnull(existe,0) as STOCK,                                     
  iva as IVA,                                    
  isnull(dbo.f_precioConvenio(@CodigoConvenio,p.codpro),isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0)) as VALOR,    
  CantDecimal as Cantidad, p.clasprod                                
  from sic3000..producto p                                                                               
  inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub                                             
  inner join sic3000..bodega on p.codpro=bodega.codpro                                            
  inner join sic3000..locales on bodega.codbod=locales.codlocal                                            
  where Sic3000..bodega.codbod=@p_Bodega and (despro like '%' + @p_filtro + '%' )  
  and isnull(p.activo,0)=1  
   union              
  select   top 100                                              
  NomLocal as DIVISION,                                              
  p.codpro as CODIGO,                                              
  despro as PRODUCTO,                                              
  isnull(existe,0) as STOCK,                                                                  
  iva as IVA,                                       
  isnull(dbo.f_precioConvenio(@CodigoConvenio,p.codpro),isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0)) as VALOR,                         
  CantDecimal as Cantidad,      p.clasprod                                                                        
  from sic3000..producto p                                              
    inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub                                              
  inner join sic3000..bodega on p.codpro=bodega.codpro                                              
    inner join sic3000..locales on bodega.codbod=locales.codlocal                                            
  where Sic3000..bodega.codbod=@p_Bodega                         
  and (p.codpro like '%' + @p_filtro + '%' )           
  and isnull(p.activo,0)=1   
  order by producto asc                                              
                                                
 end                                               
                            
 if (@p_Opcion=2)  begin                                         
  select       top 100                                        
  NomLocal as DIVISION,                                               
  p.codpro as CODIGO,                         
  despro as PRODUCTO,                                              
  isnull(existe,0) as STOCK,                                                                 
  iva as IVA,                                          
  isnull(dbo.f_precioConvenio(@CodigoConvenio,p.codpro),isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0)) as VALOR,                           
  CantDecimal as Cantidad, p.clasprod                                          
                                             
  from sic3000..producto p                 
  inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub                                           
  inner join sic3000..bodega on p.codpro=bodega.codpro                                             
  inner join sic3000..locales on bodega.codbod=locales.codlocal                                             
  where /*p.coddiv=@p_Division                                              
  and bodega.codbod=@p_Bodega  */                                            
  Sic3000..bodega.codbod=@p_Bodega                   
  AND p.codsub= (select codsub from sic3000..ProductoSubdivision where  Pea_Codigo_His=@p_Division)                                            
  and p.codpro in                                               
  (                                              
   select top 100  sic3000..producto.codpro                          
   from sic3000..PRODUCTOS_GENERICOS,sic3000..GENERICOs,sic3000..producto                                               
   where PRODUCTOS_GENERICOS.GEN_CODIGO=genericos.GEN_CODIGO                                               
   and producto.codpro=PRODUCTOS_GENERICOS.codpro                                         
   and GEN_NOMBRE like '%' + @p_filtro + '%'                                              
                                             
   union                                          
                              
   select top 100   sic3000..producto.codpro                                               
   from sic3000..PRODUCTOS_GENERICOS,sic3000..GENERICOS,sic3000..producto                                               
   where PRODUCTOS_GENERICOS.GEN_CODIGO=genericos.GEN_CODIGO                                               
   and producto.codpro=PRODUCTOS_GENERICOS.codpro                                              
   and genericos.GEN_CODIGO like '%' + @p_filtro + '%'                                             
                                             
  )              
  and isnull(p.activo,0)=1 -- Verifico que el producto no este dado de baja                                                                              
  order by producto asc                           
 end                                               
 END
 
 
 
 
 ALTER procedure [dbo].[sp_BuscaProductoSic3000_Farmacia]        
(@p_Opcion as int,        
@p_filtro as varchar(128),        
@p_Division as int,        
@p_Bodega as int,        
@CodigoEmpresa as int,            
@CodigoConvenio as int            
)                                          
as                                          
begin                                          
 if (@p_Opcion=1) -- BUSCA POR DATOS                                          
 begin                                          
  select top 100                                         
  dessub as DIVISION,                                           
  p.codpro as CODIGO,                                          
  despro as PRODUCTO,                                          
  isnull(existe,0) as STOCK,                                
  iva as IVA,                                
  isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0) as VALOR,                     
  CantDecimal as Cantidad , p.clasprod                              
                                         
  from sic3000..producto p                                          
                                          
  inner join sic3000..ProductoDivision on p.coddiv=ProductoDivision.coddiv            
  inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub            
  inner join sic3000..bodega on p.codpro=bodega.codpro                                        
  inner join sic3000..locales on bodega.codbod=locales.codlocal                                        
                                              
  where /*p.coddiv=@p_Division                                          
  and bodega.codbod=@p_Bodega  */                                        
  Sic3000..bodega.codbod=@p_Bodega                   
  AND p.coddiv= 2  
  and (despro like '%' + @p_filtro + '%' )    
   and isnull(p.activo, 0)=1                                        
                                            
  union                                          
                                            
  select top 100                                      
  dessub as DIVISION,                                                       
  p.codpro as CODIGO,                                          
  despro as PRODUCTO,                                          
  isnull(existe,0) as STOCK,                                     
  iva as IVA,                                   
  isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0) as VALOR,                      
  CantDecimal as Cantidad   , p.clasprod                                  
                                        
  from sic3000..producto p                                          
  inner join sic3000..ProductoDivision on p.coddiv=ProductoDivision.coddiv              
  inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub                     
  inner join sic3000..bodega on p.codpro=bodega.codpro                                          
    inner join sic3000..locales on bodega.codbod=locales.codlocal                                        
  where /*p.coddiv=@p_Division                                          
  and bodega.codbod=@p_Bodega  */                                        
  Sic3000..bodega.codbod=@p_Bodega                  
 AND p.coddiv= 2  
  and (p.codpro like '%' + @p_filtro + '%' )     
   and isnull(p.activo, 0)=1                                       
  order by producto asc                                          
                                            
 end                                           
                                           
 if (@p_Opcion=2) -- BUSCA POR genericos                                          
 begin                                          
  select top 100        
  dessub as DIVISION,                                                                 
  p.codpro as CODIGO,                                          
  despro as PRODUCTO,                                          
  isnull(existe,0) as STOCK,                                 
  iva as IVA,                                      
  isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0) as VALOR,                     
  CantDecimal as Cantidad    , p.clasprod                                 
                                         
  from sic3000..producto p                       
  inner join sic3000..ProductoDivision on p.coddiv=ProductoDivision.coddiv            
  inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub            
  inner join sic3000..bodega on p.codpro=bodega.codpro                                         
  inner join sic3000..locales on bodega.codbod=locales.codlocal                                         
  where /*p.coddiv=@p_Division                                          
  and bodega.codbod=@p_Bodega  */                                        
  Sic3000..bodega.codbod=@p_Bodega                     
  AND p.coddiv= 2                                       
  and p.codpro in                                    
  (                                          
   select sic3000..producto.codpro                      
   from sic3000..PRO_GEN,sic3000..GENERICOs,sic3000..producto                                           
   where PRO_GEN.GEN_CODIGO=genericos.GEN_CODIGO                                           
   and producto.codpro=PRO_GEN.codpro                                     
   and GEN_NOMBRE like '%' + @p_filtro + '%'                                          
                                         
   union                                      
                          
   select sic3000..producto.codpro                                           
   from sic3000..PRO_GEN,sic3000..GENERICOS,sic3000..producto                                           
   where PRO_GEN.GEN_CODIGO=genericos.GEN_CODIGO                                           
   and producto.codpro=PRO_GEN.codpro                                          
   and genericos.GEN_CODIGO like '%' + @p_filtro + '%'                                         
                                         
  )                                          
  order by producto, stock_max asc                       
 end                                           
                                           
                                           
end





  
  
ALTER procedure [dbo].[sp_BuscaProductoSic3000]                
(                
@p_Opcion as int,                
@p_filtro as varchar(128),                
@p_Division as int,                
@p_Bodega as int,                
@CodigoEmpresa as int,                
@CodigoConvenio as int                
)                                              
as                                              
begin                                              
 if (@p_Opcion=1) -- BUSCA POR DATOS                                              
 begin                                              
  select                                               
  NomLocal as DIVISION,                                              
  p.codpro as CODIGO,                                              
  despro as PRODUCTO,                                              
  isnull(existe,0) as STOCK,                                     
  iva as IVA,                                    
  isnull(dbo.f_precioConvenio(@CodigoConvenio,p.codpro),isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0)) as VALOR /*FUNCION QUE RECUPERA LOS PRECIOS SEGUN LA EMPRESA Y EL CONVENIO*/,                          
  CantDecimal as Cantidad, p.clasprod                                                                       
  from sic3000..producto p                                              
  inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub                                             
  inner join sic3000..bodega on p.codpro=bodega.codpro                                            
  inner join sic3000..locales on bodega.codbod=locales.codlocal                                                                                         
  where Sic3000..bodega.codbod=@p_Bodega                          
  AND p.codsub= (select codsub from sic3000..ProductoSubdivision where  Pea_Codigo_His=@p_Division)                        
  and (despro like '%' + @p_filtro + '%' )         
  and isnull(p.activo, 0)=1 -- Verifico que el producto no este dado de baja                                              
                                                
  union                                                                                       
  select                                               
  NomLocal as DIVISION,                                              
  p.codpro as CODIGO,                                              
  despro as PRODUCTO,                                              
  isnull(existe,0) as STOCK,                                                                  
  iva as IVA,                                       
  isnull(dbo.f_precioConvenio(@CodigoConvenio,p.codpro),isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0)) as VALOR,                         
  CantDecimal as Cantidad, p.clasprod                                                                                   
  from sic3000..producto p                                              
    inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub                                              
  inner join sic3000..bodega on p.codpro=bodega.codpro                                              
    inner join sic3000..locales on bodega.codbod=locales.codlocal                                            
  where Sic3000..bodega.codbod=@p_Bodega                                
  AND p.codsub= (select codsub from sic3000..ProductoSubdivision where  Pea_Codigo_His=@p_Division)                                     
  and (p.codpro like '%' + @p_filtro + '%' )           
  and isnull(p.activo,0)=1 -- Verifico que el producto no este dado de baja                                                                           
  order by producto asc                                              
                                                
 end                                               
                            
 if (@p_Opcion=2) -- BUSCA POR genericos          
 begin                                         
  select                                               
  NomLocal as DIVISION,                                               
  p.codpro as CODIGO,                         
  despro as PRODUCTO,                                              
  isnull(existe,0) as STOCK,                                                                 
  iva as IVA,                                          
  isnull(dbo.f_precioConvenio(@CodigoConvenio,p.codpro),isnull(dbo.f_Precio(@CodigoEmpresa,@CodigoConvenio,p.codpro),0)) as VALOR,                         
  CantDecimal as Cantidad, p.clasprod                                         
                                             
  from sic3000..producto p                 
  inner join sic3000..ProductoSubdivision on p.codsub=ProductoSubdivision.codsub                                           
  inner join sic3000..bodega on p.codpro=bodega.codpro                                             
  inner join sic3000..locales on bodega.codbod=locales.codlocal                                             
  where /*p.coddiv=@p_Division                                              
  and bodega.codbod=@p_Bodega  */                                            
  Sic3000..bodega.codbod=@p_Bodega                            
  AND p.codsub= (select codsub from sic3000..ProductoSubdivision where  Pea_Codigo_His=@p_Division)                                            
  and p.codpro in                                               
  (                                              
   select sic3000..producto.codpro                          
   from sic3000..PRO_GEN,sic3000..GENERICOs,sic3000..producto                                               
   where PRO_GEN.GEN_CODIGO=genericos.GEN_CODIGO                                               
   and producto.codpro=PRO_GEN.codpro                                         
   and GEN_NOMBRE like '%' + @p_filtro + '%'                                              
                                             
   union                                          
                              
   select sic3000..producto.codpro                                               
   from sic3000..PRO_GEN,sic3000..GENERICOS,sic3000..producto                                               
   where PRO_GEN.GEN_CODIGO=genericos.GEN_CODIGO                                               
   and producto.codpro=PRO_GEN.codpro                                              
   and genericos.GEN_CODIGO like '%' + @p_filtro + '%'                                             
                                             
  )              
  and isnull(p.activo,0)=1 -- Verifico que el producto no este dado de baja                                                                              
  order by producto, stock_max asc                           
 end                                               
                                               
                                               
end 

--FIN PARA SOLO PASTEUR
















CREATE PROCEDURE sp_ProtocoloCertificado
@ate_codigo bigint
AS
SELECT PROT_PREOPERATORIO FROM HC_PROTOCOLO_OPERATORIO WHERE ATE_CODIGO = @ate_codigo
GO