--el comentario indica que ya fue actualizado todo esto en la pasteur pero no en otras clinicas
--PASTEUR ACTUALIZADO

-- create procedure sp_RecuoeraDescuentoXrubro
-- @ate_codigo bigint,
-- @rubro int
-- as begin
-- select PRO_CODIGO, Descuento from CUENTAS_PACIENTES where ATE_CODIGO=@ate_codigo and RUB_CODIGO=@rubro
-- end

-------------------------------------------------------------------
-- alter PROCEDURE [dbo].[sp_RecuperarCuentaPaciente]      
-- AS      
-- BEGIN      
-- select r.RUB_GRUPO, r.RUB_NOMBRE, cp.CUE_FECHA, cp.CUE_DETALLE, cp.PRO_CODIGO_BARRAS, cp.CUE_VALOR_UNITARIO,      
-- cp.CUE_CANTIDAD, cp.CUE_VALOR, cp.CUE_IVA, cp.DESCUENTO, cp.RUB_CODIGO, cp.RUB_CODIGO, r.RUB_GRUPO, m.MED_CODIGO,      
-- m.MED_APELLIDO_PATERNO +' '+ m.MED_APELLIDO_MATERNO +' '+ m.MED_NOMBRE1 +' '+ m.MED_APELLIDO_MATERNO,      
-- cp.CUE_NUM_CONTROL, cp.Id_Tipo_Medico, p.paga_iva
-- from ATENCIONES a, CUENTAS_PACIENTES cp, RUBROS r, MEDICOS m, Sic3000..Producto p      
-- where a.ATE_CODIGO=cp.ATE_CODIGO and cp.RUB_CODIGO=r.RUB_CODIGO and cp.MED_CODIGO=m.MED_CODIGO and  
-- cp.PRO_CODIGO=p.codpro and 
-- cp.ATE_CODIGO IN (SELECT ATE_CODIGO FROM AUXAGRUPACION) and cp.CUE_ESTADO=1  order by 1  
-- end 

---------------------------------------------------------------------
-- CREATE TABLE LIQUIDACION_DETALLE (
-- LDE_CODIGO BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
-- LED_TIPO NVARCHAR(3),
-- LIQ_NUMDOC BIGINT,
-- LDE_FECHA DATETIME,
-- LDE_CUENTA NVARCHAR(10),
-- LDE_CODIGO_C FLOAT,
-- LDE_FACTURA NVARCHAR(15),
-- LDE_DEBE FLOAT,
-- LDE_HABER FLOAT,
-- LDE_ESTADO BIT,
-- LDE_ANULA NVARCHAR(500),
-- ID_USUARIO INT,
-- ID_USUARIO_ANULA INT)
-----------------------------------------------------
-- ALTER TABLE NIVEL_PISO_MAQUINA
-- ADD NIV_BODEGA INT

-----------------------------------------------------
-- ALTER TABLE LIQUIDACION 
-- ADD LIQ_ASIENTO FLOAT

-----------------------------------------------------
-- USE [His3000]
-- GO
-- /****** Object:  StoredProcedure [dbo].[sp_GuardaPedidoDevolucionDetalle]    Script Date: 08/04/2022 15:05:33 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- ALTER  procedure [dbo].[sp_GuardaPedidoDevolucionDetalle]          
-- (                
 -- @DevCodigo as bigint,                
 -- @PRO_CODIGO as bigint,                
 -- @PRO_DESCRIPCION as varchar(50),                
 -- @DevDetCantidad as int ,                
 -- @DevDetValor as decimal(10,2) ,                
 -- @DevDetIva as decimal(10,2),                
 -- @DevDetIvaTotal as decimal(10,2),                
 -- @PDD_CODIGO as bigint,                
 -- @PED_CODIGO as bigint,        
 -- @ATE_CODIGO as bigint,    
 -- @OBSERVACION AS VARCHAR(5000) --CAMBIOS EDGAR CONTIENE LA RAZON DE DEVOLUCION 20201120
-- )                
-- as                
-- begin    
 -- declare @Division as int                
 -- declare @LocalSic as int                
 -- declare @Usuario as int                
 -- declare @Grupo as int                  
 -- declare @Seccion as int                  
 -- declare @Departamento as int                  
 -- declare @SubGrupo as int                  
 -- declare @FechaT as date                  
 -- declare @cantidad as decimal(18,4)                    
 -- declare @producto as varchar(16)                    
 -- declare @CodigoPedido as int                  
 -- declare @AreaPedidoHis as int                  
 -- declare @CostoProducto as Decimal(10,4)     
 -- declare @CostoProductoUlt as Decimal(10,4)                  
 -- declare @Proveedor as int                 
 -- declare @PrecioVenta as Decimal(10,2)      
 -- declare @CodigoAtencion as Bigint             
 -- declare @HistoriaClinica as Bigint    
 -- declare @Fecha as BigInt                  
 -- set @FechaT=CAST(CONVERT(varchar(11),getdate(),103) as date) -- Transformo la fecha al formato 'dd/mm/yyyy'                  
 -- select @Fecha = dbo.TransformaFercha(@FechaT)-- Transformo la fecha a numero                  
 -- --select dbo.Transformafercha('18/10/2012')-- Transformo la fecha a numero    
 -- insert into PEDIDO_DEVOLUCION_DETALLE values             
 -- (                
  -- @DevCodigo ,                
  -- @PRO_CODIGO ,    
  -- @PRO_DESCRIPCION ,                
  -- @DevDetCantidad ,                
  -- @DevDetValor ,                
  -- @DevDetIva ,                
  -- @DevDetIvaTotal ,                
  -- @PDD_CODIGO                 
 -- )                
 -- select @Division=pea_codigo from PEDIDOS                
 -- where PEDIDOS.PED_CODIGO=@PED_CODIGO        
     
 -- select @CodigoAtencion=PEDIDOS.ATE_CODIGO --Capturo el codigo de la atencion              
 -- from PEDIDOS               
 -- WHERE PEDIDOS.PED_CODIGO=@PED_CODIGO       
         
 -- select @HistoriaClinica=PACIENTES.PAC_HISTORIA_CLINICA         
 -- from   ATENCIONES,PACIENTES        
 -- where  ATENCIONES.PAC_CODIGO=PACIENTES.PAC_CODIGO        
 -- and    ATE_CODIGO=@CodigoAtencion        
     
                           
 -- --select @LocalSic=codlocal from Sic3000..Locales                
 -- --where Local_His=@Division              
 -- set @LocalSic= (select codlocal from Sic3000..Kardex where numdoc = @PED_CODIGO and codpro = @PRO_CODIGO) -- (select codlocal from Sic3000..Locales where Local_His = 1 )  
 -- select @Usuario=id_usuario from PEDIDO_DEVOLUCION                
 -- where DevCodigo=@DevCodigo                
     
 -- update Sic3000..Bodega set existe=existe+@DevDetCantidad                 
 -- where codpro=@PRO_CODIGO                
 -- and codbod=@LocalSic                
     
 -- ----cambio hr 2019 ultimo costo por costo promedio    
  -- ---Select @CostoProducto=precos,        
  -- Select @CostoProducto=cospro,        
  -- @CostoProductoUlt=precos,               
  -- @PrecioVenta=preven,    
  -- @Grupo =codgru,                  
  -- @Seccion =codsec,                 
  -- @Departamento =coddep,                  
  -- @SubGrupo =codsub,                  
  -- @Division =coddiv                  
  -- from sic3000..PRODUCTO                   
  -- where PRODUCTO.codpro=@PRO_CODIGO    
      
  -- insert into sic3000..kardex                  
  -- values                  
  -- (                  
  -- @PRO_CODIGO,                  
  -- GETDATE(),                  
  -- @DevCodigo,                  
  -- 'DEVP',                  
  -- @LocalSic,                  
  -- 1,                  
  -- @DevDetCantidad,                
  -- 0,                  
  -- 0,                    
  -- 'DEVOLUCION PEDIDO HIS',                  
  -- @Usuario,                 
  -- @CostoProductoUlt,                  
  -- --(@CostoProductoUlt*@cantidad),          
  -- (@CostoProductoUlt*@DevDetCantidad),                 
  -- @CostoProducto,                  
  -- @Fecha,                  
  -- @Proveedor,                  
  -- @PrecioVenta,                 
  -- @Grupo ,                  
  -- @Seccion ,                  
  -- @Departamento ,                  
  -- @SubGrupo ,                  
  -- @Division ,                  
  -- @Fecha,                  
  -- --(@CostoProducto*@cantidad) ,     
  -- (@CostoProducto*@DevDetCantidad) ,             
  -- null,                 
  -- @HistoriaClinica, --/ Historia Clinica / ,              
  -- @CodigoAtencion, --/ AtencionCodigo / ,              
  -- null, --/ Factura /      
  -- 0,    
  -- null    
  -- )                 
 -- /*actualizo la cuenta del paciente*/        
 -- declare @CantidadCuenta as decimal(18,4) -- ALMACENA LA CANTIDAD ANTERIOR        
 -- declare @ValorUnitarioCuenta as decimal(18,4)  -- ALMACENA EL VALOR UNITARIO DE LA CUENTA        
 -- declare @IVACuenta as decimal(18,4)-- -- ALMACENA EL IVA ANTERIOR DE LA CUENTA        
 -- declare @CantidadCuentaNueva as decimal(18,4) -- PARA EL CALCULO DE LA NUEVA CANTIDAD DE LA CUENTA        
 -- declare @TotalCuentaNueva as decimal(18,4) -- PARA EL CALCULO DE EL NUEVO TOTAL DE LA CUENTA        
 -- declare @IVACuentaNueva as decimal(18,4) -- PARA EL CALCULO DE EL NUEVO IVA DE LA CUENTA        
 -- /*CAPTURO LOS VALORES DE LA CUENTA PARA ESE PRODUCTO*/        
 -- select         
 -- @CantidadCuenta=CUE_CANTIDAD,        
 -- @ValorUnitarioCuenta=CUE_VALOR_UNITARIO,        
 -- @IVACuenta=CUE_IVA        
 -- from CUENTAS_PACIENTES        
 -- where ATE_CODIGO=@ATE_CODIGO        
 -- and Codigo_Pedido=@PED_CODIGO        
 -- and PRO_CODIGO=@PRO_CODIGO        
 -- /****************************************************/       
 -- /*CALCULO LA NUEVA CANTIDAD DE LA CUENTA ********************/        
 -- set @CantidadCuentaNueva= @CantidadCuenta - @DevDetCantidad        
 -- /************************************************************/       
 -- if @CantidadCuentaNueva=0 -- SI LA NUEVA CANTIDAD ES CERO ACTUALIZO LOS VALORES EN 0        
 -- begin    
 -- update CUENTAS_PACIENTES        
 -- set CUE_CANTIDAD=0,        
 -- CUE_IVA=0,        
 -- CUE_VALOR=0,        
 -- CUE_OBSERVACION='DEVOLUCION N.' + CAST(@DevCodigo AS VARCHAR(64)) + ' ' + @OBSERVACION    
 -- where ATE_CODIGO=@ATE_CODIGO        
 -- and Codigo_Pedido=@PED_CODIGO        
 -- and PRO_CODIGO=@PRO_CODIGO    
 -- end    
 -- else -- CASO CONTRARIO CALCULO LOS NUEVOS VALORES Y ACTUALIZO        
 -- begin        
    
         
    
 -- if @IVACuenta!=0        
    
 -- begin        
    
  -- SET @IVACuentaNueva= ((@CantidadCuentaNueva*@ValorUnitarioCuenta)*12)/100        
    
 -- end        
    
 -- else        
    
 -- begin        
    
  -- SET @IVACuentaNueva=0        
    
 -- end        
    
         
    
 -- SET @TotalCuentaNueva=(@CantidadCuentaNueva*@ValorUnitarioCuenta)    
    
         
    
 -- update CUENTAS_PACIENTES        
    
 -- set CUE_CANTIDAD=@CantidadCuentaNueva,        
    
 -- CUE_IVA=@IVACuentaNueva,        
    
 -- CUE_VALOR=@TotalCuentaNueva,        
    
 -- CUE_OBSERVACION='DEVOLUCION N.' + CAST(@DevCodigo AS VARCHAR(64)) + ' ' + @OBSERVACION  ,    
    
 -- COSTO=ROUND(ISNULL(@CostoProducto,0)*ISNULL(@CantidadCuentaNueva,0),2)    
    
 -- where ATE_CODIGO=@ATE_CODIGO        
    
 -- and Codigo_Pedido=@PED_CODIGO        
    
 -- and PRO_CODIGO=@PRO_CODIGO        
    
        
    
 -- end    
 -- print @Fecha    
-- end

---------------------------------------------------------------------
-- alter PROCEDURE sp_ActualizaKardexSicMushuñan      
-- @numdoc nvarchar(20),
-- @usuario int
-- AS      
-- UPDATE Sic3000..Kardex SET codlocal = 61 WHERE numdoc = @numdoc and codusu = @usuario
-------------------------------------------------------------------------
-- INSERT INTO NUMERO_CONTROL VALUES (10, 'LIQUIDACIONES', 'A', 1, 0)

-------------------------------------------------------------------------
-- UPDATE SIC3000..ProductoSubdivision SET Pea_Codigo_His = 44 WHERE codsub = 102

-- INSERT INTO RUBROS VALUES (44, 'CONSULTA EXTERNA', 102, 1, 'SP13', 44, 'ACTIVO', 1, 1, 'CONSULTA EXTERNA', 44, 0, 'N')

------------------------------------------------------------------------------------

---PONER EL RUBRO DE CONSULTA EXTERNA 
--CREAR NIVEL PISO DE CONSULTA EXTERNA Y HABITACIONES COORDINAR CON MARCO 