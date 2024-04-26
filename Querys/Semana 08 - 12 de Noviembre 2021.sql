-- CREATE PROCEDURE sp_Hab_Disponible
-- @hab_codigo int 
-- AS
-- SELECT HES_CODIGO FROM HABITACIONES WHERE hab_Codigo = @hab_codigo
-- GO
---------------------------------------------------------------------

-- CREATE PROCEDURE sp_ReversionTiempo
-- @ate_codigo bigint
-- AS
-- SELECT DATEDIFF(SECOND, ISNULL((ATE_FECHA_ALTA), GETDATE()), GETDATE()) / 3600.0 AS HORAS FROM ATENCIONES WHERE ATE_CODIGO = @ate_codigo
-- GO

--------------------------------------------------------------------

-- ALTER PROCEDURE [dbo].[sp_QuirofanoVerTickets]  
-- @ate_codigo int  
-- AS  
-- SELECT A.ATE_NUMERO_ATENCION AS Atencion, PA.PAC_HISTORIA_CLINICA AS HC, P.PED_CODIGO AS 'Pedido No.',   
-- PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2 AS Paciente,  
-- P.PED_FECHA AS Fecha, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico,  
-- U.APELLIDOS + ' ' + U.NOMBRES AS Usuario  
-- FROM PEDIDOS P   
-- INNER JOIN PEDIDOS_DETALLE PD ON P.PED_CODIGO = PD.PED_CODIGO  
-- INNER JOIN USUARIOS U ON P.ID_USUARIO = U.ID_USUARIO  
-- INNER JOIN MEDICOS M ON P.MED_CODIGO = M.MED_CODIGO  
-- INNER JOIN ATENCIONES A ON P.ATE_CODIGO = A.ATE_CODIGO  
-- INNER JOIN PACIENTES PA ON A.PAC_CODIGO = PA.PAC_CODIGO  
-- INNER JOIN Sic3000.dbo.Producto PRO ON PD.PRO_CODIGO = codpro  
-- WHERE P.ATE_CODIGO = @ate_codigo AND P.PED_DESCRIPCION LIKE '%QUIROFANO%'
-- GROUP BY A.ATE_NUMERO_ATENCION,PA.PAC_HISTORIA_CLINICA, P.PED_CODIGO,  
-- PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2,  
-- P.PED_FECHA, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2,  
-- U.APELLIDOS + ' ' + U.NOMBRES  
-- ORDER BY P.PED_CODIGO ASC  
----------------------------------------------------------------------------------------------------

-- --REVISAR CUAL ES EL ULTIMO CODIGO DE LA TABLA
-- SELECT * FROM PARAMETROS ORDER BY 1 DESC

-- INSERT INTO PARAMETROS VALUES(23, 1, 'ACCESO ADMISION', 'PERMISOS PARA EL MODULO DE ADMISION', 1)

-- --REVISAR CUAL ES EL ULTIMO CODIDO DE LA TABLA
-- SELECT * FROM PARAMETROS_DETALLE ORDER BY 1 DESC

-- INSERT INTO PARAMETROS_DETALLE VALUES(34, 23, 'ACCESO ADMISION', NULL, 'PARAMETRO DE ACCESO ADMISION', 1)

-- CREATE PROCEDURE sp_ParametroAdmisionAcceso
-- AS
-- SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 34
-- GO
-- -----------------------------------------------------------------------------------------------------------
-- alter PROCEDURE sp_ConectarMedico
-- @id nvarchar(15)
-- AS
-- SELECT MED_CODIGO FROM MEDICOS where MED_RUC like '%'+ @id + '%'
-- go
-- -------------------------------------------------------------------
-- alter table OBSTETRICA_CONSULTAEXTERNA
-- alter column Semanas_Gestacion decimal(18,1)
-- --------------------------------------------------------------------
-- alter PROCEDURE [dbo].[sp_GrabaObstetricaConsultaExterna]  
-- (  
  
 -- @lblHistoria AS VARCHAR(20),  
 -- @lblAtencion BIGINT,  
 -- @txt_Gesta AS smallint,  
 -- @txt_Partos AS SMALLINT,  
 -- @txt_Abortos AS SMALLINT,  
 -- @txt_Cesareas AS SMALLINT,  
 -- @dtp_ultimaMenst1 AS DATE,  
 -- @txt_SemanaG AS float,  
 -- @movFetal AS SMALLINT,  
 -- @txt_FrecCF AS SMALLINT,  
 -- @memRotas AS SMALLINT,  
 -- @txt_Tiempo AS VARCHAR(10),  
 -- @txt_AltU AS SMALLINT,  
 -- @txt_Presentacion AS VARCHAR(10),  
 -- @txt_Dilatacion AS SMALLINT,  
 -- @txt_Borramiento AS SMALLINT,  
 -- @txt_Plano AS VARCHAR(50),  
 -- @pelvis AS SMALLINT,  
 -- @sangrado AS SMALLINT,  
 -- @txt_Contracciones AS VARCHAR(50)  
  
-- )  
-- AS  
-- BEGIN  
  
 -- DECLARE @ID_AGENDA_PACIENTE AS BIGINT  
 -- SET @ID_AGENDA_PACIENTE=(SELECT ID_AGENDAMIENTO FROM AGENDA_PACIENTE WHERE Identificacion=  
       -- (SELECT PAC_IDENTIFICACION FROM PACIENTES WHERE PAC_HISTORIA_CLINICA=@lblHistoria))  
 -- IF(@ID_AGENDA_PACIENTE IS NULL)  
 -- BEGIN  
  -- SET @ID_AGENDA_PACIENTE=1  
 -- END  
 -- INSERT INTO OBSTETRICA_CONSULTAEXTERNA VALUES  
 -- (  
  -- @ID_AGENDA_PACIENTE,  
  -- @lblAtencion,  
  -- @txt_Gesta,  
  -- @txt_Partos,  
  -- @txt_Abortos,  
  -- @txt_Cesareas,  
  -- @dtp_ultimaMenst1,  
  -- @txt_SemanaG,  
  -- @movFetal,  
  -- @txt_FrecCF,  
  -- @memRotas,  
  -- @txt_Tiempo,  
  -- @txt_AltU,  
  -- @txt_Presentacion,  
  -- @txt_Dilatacion,  
  -- @txt_Borramiento,  
  -- @txt_Plano,  
  -- @pelvis,  
  -- @sangrado,  
  -- @txt_Contracciones  
 -- )  
  
-- END  

------------------------------------------------------------------
CREATE  procedure [dbo].[sp_GuardaPedidoDevolucionDetalleQuirofano]          
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
 @OBSERVACION AS VARCHAR(5000) --CAMBIOS EDGAR CONTIENE LA RAZON DE DEVOLUCION 20201120    
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
     
                           
 --select @LocalSic=codlocal from Sic3000..Locales                
 --where Local_His=@Division              

 set @LocalSic= convert(int, (select PAR_NOMBRE from PARAMETROS where PAR_CODIGO = 18)) --18 es parametro de numero de bodega
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
  'DEVOLUCION PEDIDO HIS - QUIROFANO',                  
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
  
 