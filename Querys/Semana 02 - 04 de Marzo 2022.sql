ALTER TABLE QUIROFANO_PRODUCTOS
ADD QP_BODEGA INT 

UPDATE QUIROFANO_PRODUCTOS SET QP_BODEGA = 12

--------------------------------------------------------------------------------
SELECT * FROM PARAMETROS ORDER BY PAR_CODIGO DESC --CONSULTA EL ULTIMO NUMERO 

INSERT INTO PARAMETROS VALUES(27, 1, '19', 'BODEGA DE GASTROENTEROLOGIA', 1) --EN ALIANZA DEBE ESTAR DESACTIVADO

SELECT * FROM PARAMETROS_DETALLE ORDER BY PAD_CODIGO DESC --CONSULTA EL ULTIMO NUMERO

INSERT INTO PARAMETROS_DETALLE VALUES(38, 27, 'BODEGA DE GASTROENTEROLOGIA', NULL, 19, 1) --EN ALIANZA DEBE ESTAR DESACTIVADO.

--------------------------------------------------------------------------------
CREATE PROCEDURE sp_ParametroBodegaGastro
AS
IF((SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 38) = 1)
BEGIN
	SELECT PAD_VALOR FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 38
END
ELSE
BEGIN
	SELECT '0' AS PAD_VALOR FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 38
END
-------------------------------------------------------------------------------

ALTER PROCEDURE sp_QuirofanoNoRepetirProductos  
@codigoproducto varchar(13) OUTPUT,  
@codpro varchar(13),
@bodega int
AS  
SET @codigoproducto = (SELECT CODPRO FROM QUIROFANO_PRODUCTOS WHERE CODPRO = @codpro AND QP_BODEGA = @bodega)  

---------------------------------------------------------------------------------

ALTER PROCEDURE sp_QuirofanoAgregarProducto  
@codpro varchar(13),  
@grupo varchar(100),
@bodega int
AS  
IF NOT EXISTS (SELECT * FROM QUIROFANO_PRODUCTOS WHERE CODPRO = @codpro AND QP_BODEGA = @bodega)  
BEGIN  
 INSERT INTO QUIROFANO_PRODUCTOS VALUES(@codpro, @grupo, @bodega)  
END  
----------------------------------------------------------------------------------
ALTER TABLE PROCEDIMIENTOS_CIRUGIA
ADD PCI_BODEGA INT

UPDATE PROCEDIMIENTOS_CIRUGIA SET PCI_BODEGA = 12

---------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_QuirofanoTodosProcedimientos] 
@bodega int
AS  
SELECT C.PCI_DESCRIPCION AS Procedimiento, QPP.PCI_CODIGO  
FROM QUIROFANO_PROCE_PRODU QPP  
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO  
WHERE C.PCI_BODEGA = @bodega
GROUP BY C.PCI_DESCRIPCION, QPP.PCI_CODIGO  
ORDER BY C.PCI_DESCRIPCION ASC

--------------------------------------------------------------------
USE [His3000]
GO

/****** Object:  View [dbo].[VistaQuirofanoProductos]    Script Date: 02/03/2022 15:36:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER view [dbo].[VistaQuirofanoProductos]
as
select QP.CODPRO AS CODIGO, P.despro AS DESCRIPCION, QP.QP_GRUPO AS GRUPO, QP.QP_BODEGA from QUIROFANO_PRODUCTOS QP
INNER JOIN Sic3000..Producto P ON QP.CODPRO = P.codpro
GO

------------------------------------------------------------------------------------

ALTER PROCEDURE sp_QuirofanoPacientesGastro
@bodega int
AS      
SELECT A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_HISTORIA_CLINICA AS HC,      
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,      
P.PAC_IDENTIFICACION AS Identificacion, H.hab_Numero AS Habitacion, A.ATE_FECHA_INGRESO AS 'F. Ingreso',      
P.PAC_CODIGO, A.ATE_CODIGO,      
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico,      
C.CAT_NOMBRE AS Aseguradora, T.TIP_DESCRIPCION AS TIPO, P.PAC_GENERO, TA.TIA_DESCRIPCION, P.PAC_FECHA_NACIMIENTO,      
ISNULL((SELECT COUNT(DISTINCT QPP.PCI_CODIGO) FROM QUIROFANO_PROCE_PRODU QPP       
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO AND QPP.PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO AND C.PCI_BODEGA = @bodega), NULL) AS 'PROCE_AGREGADOS',      
ISNULL((SELECT COUNT(QPP_CIERRE) FROM QUIROFANO_PROCE_PRODU QPP      
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO      
WHERE QPP_CIERRE = 1 AND PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO AND C.PCI_BODEGA = @bodega),NULL) AS 'PROCE_CERRADOS',      
ISNULL((SELECT COUNT(QPP.PCI_CODIGO) FROM QUIROFANO_PROCE_PRODU QPP      
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO      
WHERE PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO AND C.PCI_BODEGA = @bodega),NULL) AS 'CANT_PROCE'      
FROM PACIENTES P      
INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO      
INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo      
INNER JOIN TIPO_INGRESO T ON A.TIP_CODIGO = T.TIP_CODIGO      
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO      
INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO      
INNER JOIN CATEGORIAS_CONVENIOS C ON ADC.CAT_CODIGO = C.CAT_CODIGO      
INNER JOIN TIPO_TRATAMIENTO TA ON A.TIA_CODIGO = TA.TIA_CODIGO      
WHERE A.ESC_CODIGO = 1 AND T.TIP_CODIGO NOT IN (10)
--AND  A.ATE_ESTADO = 1  
ORDER BY P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2       
  
  
---------------------------------------------------------------------------------------


ALTER PROCEDURE [dbo].[sp_QuirofanoProcedimientos]
@bodega int
AS  
SELECT PCI_CODIGO as Codigo, PCI_DESCRIPCION as Descripcion  
FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_ESTADO = 1  AND PCI_BODEGA = @bodega
ORDER BY PCI_DESCRIPCION ASC

------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[sp_QuirofanoMostrarProcedimientoPaciente]          
@cie_codigo bigint,          
@atencion int,  
@bodega int  
AS          
IF EXISTS (SELECT * FROM QUIROFANO_PROCE_PRODU WHERE PCI_CODIGO = @cie_codigo AND ATE_CODIGO = @atencion AND QPP_ORDEN IS NOT NULL)          
BEGIN          
 SELECT QPP.CODPRO AS Codigo, P.despro AS Producto, QP.QP_GRUPO AS Grupo,          
 B.existe AS Stock, QPP.QPP_CANTIDAD AS 'Cant. Original',           
 QPP.QPP_CANT_ADICIONAL AS 'Cant. Adicional',           
 --ISNULL((SELECT TOP 1 PDD.DevDetCantidad FROM CUENTAS_PACIENTES CP          
 --INNER JOIN PEDIDO_DEVOLUCION PD ON CP.Codigo_Pedido = PD.Ped_Codigo          
 --INNER JOIN PEDIDO_DEVOLUCION_DETALLE PDD ON PD.DevCodigo = PDD.DevCodigo AND PDD.PRO_CODIGO = QPP.CODPRO          
 --and cp.ATE_CODIGO = 4788          
 --) , 0) AS Devoluciones,       
'0' AS Devoluciones,          
 QPP.QPP_FECHA AS Fecha, QPP.QPP_ORDEN, QPP_USUARIO as Usuario, p.preven          
 FROM QUIROFANO_PROCE_PRODU QPP          
 INNER JOIN QUIROFANO_PRODUCTOS QP ON QPP.CODPRO = QP.CODPRO          
 INNER JOIN Sic3000.dbo.Producto P ON QP.CODPRO = P.codpro          
 INNER JOIN Sic3000.dbo.Bodega B ON P.codpro = B.codpro          
 WHERE QPP.PCI_CODIGO = @cie_codigo AND QPP.ATE_CODIGO = @atencion AND b.codbod = @bodega AND QP.QP_BODEGA = @bodega     
 order by QPP.QPP_ORDEN asc        
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
 WHERE QPP.PCI_CODIGO = @cie_codigo AND QPP.ATE_CODIGO IS NULL AND B.codbod = @bodega AND QP.QP_BODEGA = @bodega         
 order by QPP.QPP_ORDEN asc        
END


-------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[sp_GastroVerTickets]    
@ate_codigo int    
AS    
SELECT A.ATE_NUMERO_ATENCION AS Atencion, PA.PAC_HISTORIA_CLINICA AS HC, P.PED_CODIGO AS 'Pedido No.',     
PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2 AS Paciente,    
P.PED_FECHA AS Fecha, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico,    
U.APELLIDOS + ' ' + U.NOMBRES AS Usuario    
FROM PEDIDOS P     
INNER JOIN PEDIDOS_DETALLE PD ON P.PED_CODIGO = PD.PED_CODIGO    
INNER JOIN USUARIOS U ON P.ID_USUARIO = U.ID_USUARIO    
INNER JOIN MEDICOS M ON P.MED_CODIGO = M.MED_CODIGO    
INNER JOIN ATENCIONES A ON P.ATE_CODIGO = A.ATE_CODIGO    
INNER JOIN PACIENTES PA ON A.PAC_CODIGO = PA.PAC_CODIGO    
INNER JOIN Sic3000.dbo.Producto PRO ON PD.PRO_CODIGO = codpro    
WHERE P.ATE_CODIGO = @ate_codigo AND P.PED_DESCRIPCION LIKE '%GASTROENTEROLOGIA%'  
GROUP BY A.ATE_NUMERO_ATENCION,PA.PAC_HISTORIA_CLINICA, P.PED_CODIGO,    
PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2,    
P.PED_FECHA, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2,    
U.APELLIDOS + ' ' + U.NOMBRES    
ORDER BY P.PED_CODIGO ASC    
  
 -----------------------------------------------------------------------------------------
 
ALTER PROCEDURE sp_QuirofanoProductoSic   
@filtro nvarchar(100),
@bodega int 
AS    
SELECT P.codpro AS CODIGO, P.despro AS PRODUCTO, B.existe AS STOCK, QP.QP_GRUPO AS GRUPO, P.preven    
FROM QUIROFANO_PRODUCTOS QP    
INNER JOIN Sic3000..Producto P ON QP.CODPRO = P.codpro    
INNER JOIN Sic3000..Bodega B ON P.codpro = B.codpro     
WHERE B.codbod = @bodega and P.despro like '%'+ @filtro + '%'  
ORDER BY 2 ASC 

----------------------------------------------

ALTER PROCEDURE sp_ActualizaKardexSic    
@numdoc nvarchar(20),
@bodega int
AS    
UPDATE Sic3000..Kardex SET codlocal = @bodega WHERE numdoc = @numdoc    
--------------------------------------------------------------------

ALTER PROCEDURE [dbo].[sp_QuirofanoCuentaPacientes]  
@ate_codigo bigint,  
@codpro varchar(15),  
@cue_detalle varchar(500),  
@cue_valor float,  
@cue_cantidad float,  
@cue_total float,  
@cue_iva float,  
@rub_codigo int,  
@id_usuario int,  
@codigo_pedido bigint,  
@costo float,
@descripcion nvarchar(10)
AS  
INSERT INTO CUENTAS_PACIENTES VALUES((SELECT MAX(CUE_CODIGO) + 1 FROM CUENTAS_PACIENTES), @ate_codigo,   
GETDATE(), @codpro, @cue_detalle, @cue_valor, @cue_cantidad, @cue_total, @cue_iva, 1, '0', @rub_codigo,  
1,@id_usuario, 0, @codpro, NULL, @descripcion, 0, NULL, @codigo_pedido, NULL, @costo, NULL,   
'N', 0, 0, 0, GETDATE())

------------------------------------------------------------------------------------------------


ALTER  procedure [dbo].[sp_GuardaPedidoDevolucionDetalleQuirofano]              
(                    
 @DevCodigo as bigint,                    
 @PRO_CODIGO as bigint,                    
 @PRO_DESCRIPCION as varchar(50),                    
 @DevDetCantidad as int ,                    
 @DevDetValor as decimal(10,2) ,                    
 @DevDetIva as decimal(10,2),                    
 @DevDetIvaTotal as decimal(10,2),                    
 @PDD_CODIGO as bigint,                    
 @PED_CODIGO as bigint,            
 @ATE_CODIGO as bigint,        
 @OBSERVACION AS VARCHAR(5000), --CAMBIOS EDGAR CONTIENE LA RAZON DE DEVOLUCION 20201120     
 @BODEGA as int,
 @MODULO AS NVARCHAR(100)
)                    
as                    
begin        
 declare @Division as int                    
 declare @LocalSic as int                    
 declare @Usuario as int                    
 declare @Grupo as int                      
 declare @Seccion as int                      
 declare @Departamento as int                      
 declare @SubGrupo as int                      
 declare @FechaT as date                      
 declare @cantidad as decimal(18,4)                        
 declare @producto as varchar(16)                        
 declare @CodigoPedido as int                      
 declare @AreaPedidoHis as int                      
 declare @CostoProducto as Decimal(10,4)         
 declare @CostoProductoUlt as Decimal(10,4)                      
 declare @Proveedor as int                     
 declare @PrecioVenta as Decimal(10,2)          
 declare @CodigoAtencion as Bigint                 
 declare @HistoriaClinica as Bigint        
 declare @Fecha as BigInt                      
 set @FechaT=CAST(CONVERT(varchar(11),getdate(),103) as date) -- Transformo la fecha al formato 'dd/mm/yyyy'                      
 select @Fecha = dbo.TransformaFercha(@FechaT)-- Transformo la fecha a numero                      
 --select dbo.Transformafercha('18/10/2012')-- Transformo la fecha a numero        
 insert into PEDIDO_DEVOLUCION_DETALLE values                 
 (                    
  @DevCodigo ,                    
  @PRO_CODIGO ,        
  @PRO_DESCRIPCION ,                    
  @DevDetCantidad ,                    
  @DevDetValor ,                    
  @DevDetIva ,                    
  @DevDetIvaTotal ,                    
  @PDD_CODIGO                     
 )                    
 select @Division=pea_codigo from PEDIDOS                    
 where PEDIDOS.PED_CODIGO=@PED_CODIGO            
         
 select @CodigoAtencion=PEDIDOS.ATE_CODIGO --Capturo el codigo de la atencion                  
 from PEDIDOS                   
 WHERE PEDIDOS.PED_CODIGO=@PED_CODIGO           
             
 select @HistoriaClinica=PACIENTES.PAC_HISTORIA_CLINICA             
 from   ATENCIONES,PACIENTES            
 where  ATENCIONES.PAC_CODIGO=PACIENTES.PAC_CODIGO            
 and    ATE_CODIGO=@CodigoAtencion            
         
 DECLARE @reposicionCantidad int  
 set @reposicionCantidad = (SELECT RQ_CANTIDAD FROM REPOSICION_QUIROFANO WHERE PED_CODIGO =  @PED_CODIGO AND CODPRO =  @PRO_CODIGO)  
 IF(@reposicionCantidad < @cantidad)  
 BEGIN  
 UPDATE REPOSICION_QUIROFANO SET RQ_CANTIDAD = RQ_CANTIDAD - @cantidad  WHERE PED_CODIGO =  @PED_CODIGO AND CODPRO =  @PRO_CODIGO    
END  
ELSE  
begin  
 IF(@reposicionCantidad = @cantidad)  
 BEGIN  
  DELETE FROM REPOSICION_QUIROFANO WHERE PED_CODIGO =  @PED_CODIGO AND CODPRO =  @PRO_CODIGO    
 END  
END  
    
 set @LocalSic= @BODEGA 
 select @Usuario=id_usuario from PEDIDO_DEVOLUCION                    
 where DevCodigo=@DevCodigo                    
         
 update Sic3000..Bodega set existe=existe+@DevDetCantidad                     
 where codpro=@PRO_CODIGO                    
 and codbod=@LocalSic                    
         
 ----cambio hr 2019 ultimo costo por costo promedio        
  ---Select @CostoProducto=precos,            
  Select @CostoProducto=cospro,            
  @CostoProductoUlt=precos,              
  @PrecioVenta=preven,        
  @Grupo =codgru,                      
  @Seccion =codsec,                     
  @Departamento =coddep,                      
  @SubGrupo =codsub,                      
  @Division =coddiv                      
  from sic3000..PRODUCTO                       
  where PRODUCTO.codpro=@PRO_CODIGO        
          
  insert into sic3000..kardex                      
  values                      
  (                      
  @PRO_CODIGO,                      
  GETDATE(),                      
  @DevCodigo,                      
  'DEVP',                      
  @LocalSic,                      
  1,                      
  @DevDetCantidad,                    
  0,                      
  0,                        
  'DEVOLUCION PEDIDO HIS - ' + @MODULO,                      
  @Usuario,                     
  @CostoProductoUlt,                      
  --(@CostoProductoUlt*@cantidad),              
  (@CostoProductoUlt*@DevDetCantidad),                     
  @CostoProducto,                      
  @Fecha,                      
  @Proveedor,                      
  @PrecioVenta,                     
  @Grupo ,                      
  @Seccion ,                      
  @Departamento ,                      
  @SubGrupo ,                      
  @Division ,                      
  @Fecha,                      
  --(@CostoProducto*@cantidad) ,         
  (@CostoProducto*@DevDetCantidad) ,                 
  null,                     
  @HistoriaClinica, --/ Historia Clinica / ,                  
  @CodigoAtencion, --/ AtencionCodigo / ,                  
  null, --/ Factura /          
  0,        
  null        
  )                     
 /*actualizo la cuenta del paciente*/            
 declare @CantidadCuenta as decimal(18,4) -- ALMACENA LA CANTIDAD ANTERIOR            
 declare @ValorUnitarioCuenta as decimal(18,4)  -- ALMACENA EL VALOR UNITARIO DE LA CUENTA            
 declare @IVACuenta as decimal(18,4)-- -- ALMACENA EL IVA ANTERIOR DE LA CUENTA            
 declare @CantidadCuentaNueva as decimal(18,4) -- PARA EL CALCULO DE LA NUEVA CANTIDAD DE LA CUENTA            
 declare @TotalCuentaNueva as decimal(18,4) -- PARA EL CALCULO DE EL NUEVO TOTAL DE LA CUENTA            
 declare @IVACuentaNueva as decimal(18,4) -- PARA EL CALCULO DE EL NUEVO IVA DE LA CUENTA            
 /*CAPTURO LOS VALORES DE LA CUENTA PARA ESE PRODUCTO*/            
 select             
 @CantidadCuenta=CUE_CANTIDAD,            
 @ValorUnitarioCuenta=CUE_VALOR_UNITARIO,            
 @IVACuenta=CUE_IVA            
 from CUENTAS_PACIENTES            
 where ATE_CODIGO=@ATE_CODIGO            
 and Codigo_Pedido=@PED_CODIGO            
 and PRO_CODIGO=@PRO_CODIGO            
 /****************************************************/           
 /*CALCULO LA NUEVA CANTIDAD DE LA CUENTA ********************/            
 set @CantidadCuentaNueva= @CantidadCuenta - @DevDetCantidad            
 /************************************************************/           
 if @CantidadCuentaNueva=0 -- SI LA NUEVA CANTIDAD ES CERO ACTUALIZO LOS VALORES EN 0            
 begin        
 update CUENTAS_PACIENTES            
 set CUE_CANTIDAD=0,            
 CUE_IVA=0,            
 CUE_VALOR=0,            
 CUE_OBSERVACION='DEVOLUCION N.' + CAST(@DevCodigo AS VARCHAR(64)) + ' ' + @OBSERVACION        
 where ATE_CODIGO=@ATE_CODIGO            
 and Codigo_Pedido=@PED_CODIGO            
 and PRO_CODIGO=@PRO_CODIGO        
 end        
 else -- CASO CONTRARIO CALCULO LOS NUEVOS VALORES Y ACTUALIZO            
 begin            
        
             
        
 if @IVACuenta!=0            
        
 begin            
        
  SET @IVACuentaNueva= ((@CantidadCuentaNueva*@ValorUnitarioCuenta)*12)/100            
        
 end            
        
 else            
        
 begin            
        
  SET @IVACuentaNueva=0            
        
 end            
        
             
        
 SET @TotalCuentaNueva=(@CantidadCuentaNueva*@ValorUnitarioCuenta)        
        
             
        
 update CUENTAS_PACIENTES            
  
 set CUE_CANTIDAD=@CantidadCuentaNueva,            
        
 CUE_IVA=@IVACuentaNueva,            
        
 CUE_VALOR=@TotalCuentaNueva,            
        
 CUE_OBSERVACION='DEVOLUCION N.' + CAST(@DevCodigo AS VARCHAR(64)) + ' ' + @OBSERVACION  ,        
        
 COSTO=ROUND(ISNULL(@CostoProducto,0)*ISNULL(@CantidadCuentaNueva,0),2)        
        
 where ATE_CODIGO=@ATE_CODIGO            
        
 and Codigo_Pedido=@PED_CODIGO            
        
 and PRO_CODIGO=@PRO_CODIGO            
        
            
        
 end        
 print @Fecha        
end

-------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_QuirofanoProcedimientosPaciente]  
@ate_codigo int,   
@pac_codigo int,
@bodega int
AS  
SELECT C.PCI_CODIGO, C.PCI_DESCRIPCION FROM QUIROFANO_PROCE_PRODU QPP  
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO  
WHERE QPP.ATE_CODIGO = @ate_codigo and QPP.PAC_CODIGO = @pac_codigo AND C.PCI_BODEGA = @bodega 
GROUP BY C.PCI_CODIGO, C.PCI_DESCRIPCION  

-------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_QuirofanoProcedimientoCerrado]  
@estado int output,  
@ate_codigo int,  
@pac_codigo int,  
@cie_codigo bigint  
AS  
SET @estado = (SELECT TOP 1 QPP_CIERRE FROM QUIROFANO_PROCE_PRODU  
WHERE ATE_CODIGO = @ate_codigo AND PAC_CODIGO = @pac_codigo AND PCI_CODIGO = @cie_codigo ORDER BY QPP_FECHA)  

------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_QuirofanoPacientes] 
@bodega int
AS      
SELECT A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_HISTORIA_CLINICA AS HC,      
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,      
P.PAC_IDENTIFICACION AS Identificacion, H.hab_Numero AS Habitacion, A.ATE_FECHA_INGRESO AS 'F. Ingreso',      
P.PAC_CODIGO, A.ATE_CODIGO,      
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico,      
C.CAT_NOMBRE AS Aseguradora, T.TIP_DESCRIPCION AS TIPO, P.PAC_GENERO, TA.TIA_DESCRIPCION, P.PAC_FECHA_NACIMIENTO,      
ISNULL((SELECT COUNT(DISTINCT QPP.PCI_CODIGO) FROM QUIROFANO_PROCE_PRODU QPP       
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO 
AND QPP.PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO AND C.PCI_BODEGA = @bodega), NULL) AS 'PROCE_AGREGADOS',      
ISNULL((SELECT COUNT(QPP_CIERRE) FROM QUIROFANO_PROCE_PRODU QPP      
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO      
WHERE QPP_CIERRE = 1 AND PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO AND C.PCI_BODEGA = @bodega),NULL) AS 'PROCE_CERRADOS',      
ISNULL((SELECT COUNT(QPP.PCI_CODIGO) FROM QUIROFANO_PROCE_PRODU QPP      
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO      
WHERE PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO AND C.PCI_BODEGA = @bodega),NULL) AS 'CANT_PROCE'      
FROM PACIENTES P      
INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO      
INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo      
INNER JOIN TIPO_INGRESO T ON A.TIP_CODIGO = T.TIP_CODIGO      
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO      
INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO      
INNER JOIN CATEGORIAS_CONVENIOS C ON ADC.CAT_CODIGO = C.CAT_CODIGO      
INNER JOIN TIPO_TRATAMIENTO TA ON A.TIA_CODIGO = TA.TIA_CODIGO      
WHERE A.ESC_CODIGO = 1 AND T.TIP_CODIGO in(1,2,3)  
--AND  A.ATE_ESTADO = 1  
ORDER BY P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2    