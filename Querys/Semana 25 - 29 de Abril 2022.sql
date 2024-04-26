
-- ALTER TABLE AGENDAMIENTO
-- ADD MedicoCelular nvarchar(10)

---------------------------------------------------------------------------------------------------------------------------------

-- alter PROCEDURE sp_GrabaAgendaConsultaExterna  
-- (  
 -- @dtpFechaCita AS DATE,  
 -- @cmbEspecialidades AS VARCHAR(50),  
 -- @lblMedico AS VARCHAR(100),  
 -- @lblMailMed AS VARCHAR(100),  
 -- @cmbConsultorios AS VARCHAR(100),  
 -- @cmbHora AS VARCHAR(15),  
 -- @txtMotivo AS VARCHAR(1000),  
 -- @txtNotas AS VARCHAR(1000),  
 -- @txtIdentificacion AS VARCHAR(20),
 -- @medicoCelular nvarchar(10)
-- )  
-- AS  
-- BEGIN  
  
 -- DECLARE @IDPACIENTE AS BIGINT  
 -- SET @IDPACIENTE=(SELECT ID_AGENDAMIENTO FROM AGENDA_PACIENTE WHERE Identificacion=@txtIdentificacion)  
 -- INSERT INTO AGENDAMIENTO VALUES   
 -- (@IDPACIENTE,@cmbEspecialidades,@lblMedico,@lblMailMed,@cmbConsultorios,@dtpFechaCita,@cmbHora,@txtMotivo,@txtNotas, @medicoCelular)  
  
-- END

---------------------------------------------------------------------------------------------------------------------------------
 -- alter PROCEDURE sp_AgendamientoView    
 -- @desde date,    
 -- @hasta date    
 -- AS    
 -- SELECT A.ID_AGENDAMIENTO as Codigo, FechaAgenda as 'Fecha Cita Medica', Hora as 'Hora Cita Medica', Consultorio,  
 -- TRIM(Apellidos) + ' ' + TRIM(Nombres) as 'Paciente', Identificacion, TRIM(Direccion) as Direccion,    
 -- Telefono, Celular, Email, Medico, Especialidad,   
 -- A.MedicoCelular as CelularMedico,  
 -- EmailMedico AS 'Email Medico', MotivoConsulta as 'Motivo de la Consulta',  
 -- ObservacionesConsulta AS 'Observaciones'    
 -- FROM AGENDA_PACIENTE AP    
 -- INNER JOIN AGENDAMIENTO A ON AP.ID_AGENDAMIENTO = A.ID_AGENDA_PACIENTE    
 -- WHERE FechaAgenda BETWEEN @desde and @hasta
 
 -------------------------------------------------------------------------------------------------------------------------
 --FALTA EN LA ALIANZA
 -- ALTER PROCEDURE [dbo].[sp_RecuperarCuentaPaciente]        
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
--FALTA EN LA ALIANZA
-----------------------------------------------------------------------------------------
--SOLO PARA ALIANZA
-- USE [His3000]
-- GO

-- /****** Object:  Table [dbo].[SUCURSALES]    Script Date: 27/04/2022 15:57:49 ******/
-- SET ANSI_NULLS ON
-- GO

-- SET QUOTED_IDENTIFIER ON
-- GO

-- CREATE TABLE [dbo].[SUCURSALES](
	-- [SUC_CODIGO] [bigint] IDENTITY(1,1) NOT NULL,
	-- [SUC_NOMBRE] [nvarchar](150) NULL,
	-- [SUC_DIRECCION] [nvarchar](250) NULL,
	-- [SUC_TELEFONO] [nvarchar](150) NULL,
	-- [SUC_EMAIL] [nvarchar](250) NULL,
	-- [SUC_SECTOR] [nvarchar](150) NULL
-- ) ON [PRIMARY]
-- GO

--SOLO PONER EN LA ALIANZA
-------------------------------------------------------------------------------------------

alter table QUIROFANO_PROCE_PRODU
alter column QPP_CANTIDAD float
-------------------------------------------------------------------------------------------

alter PROCEDURE sp_QuirofanoActulizarCantidades  
@codpro nvarchar(15),  
@pci_codigo int,  
@cantidad float,  
@ate_codigo bigint  
AS  
UPDATE QUIROFANO_PROCE_PRODU SET QPP_CANTIDAD = @cantidad   
WHERE PCI_CODIGO = @pci_codigo AND CODPRO = @codpro  
AND ATE_CODIGO = @ate_codigo 

-----------------------------------------------------------------------------------------

ALTER TABLE QUIROFANO_PROCE_PRODU
ALTER COLUMN QPP_CANT_ADICIONAL FLOAT

----------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_QuirofanoPedidoAdicional]  
@atencion int,   
@paciente int,  
@cie_codigo bigint,  
@cant_adicional float,  
@codpro varchar(13)  
AS  
IF EXISTS (SELECT * FROM QUIROFANO_PROCE_PRODU   
WHERE ATE_CODIGO = @atencion AND PAC_CODIGO = @paciente AND PCI_CODIGO = @cie_codigo AND QPP_CANT_ADICIONAL IS NULL)  
BEGIN   
 UPDATE QUIROFANO_PROCE_PRODU SET QPP_CANT_ADICIONAL = @cant_adicional  
 WHERE ATE_CODIGO = @atencion AND PCI_CODIGO = @cie_codigo AND CODPRO = @codpro  
END

-----------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[sp_QuirofanoPacienteProcedimiento]      
@orden int,      
@cie_codigo bigint,      
@codpro varchar(13),      
@cantidad float,      
@paciente int,      
@atencion int,      
@usada int,      
@usuario varchar(100),  
@cerrado int  
AS      
DECLARE @fecha DATETIME      
SET @fecha = GETDATE();    
if(@cerrado = 0)  
begin  
 INSERT INTO QUIROFANO_PROCE_PRODU VALUES(@orden, @cie_codigo, @codpro, @cantidad, @paciente, @atencion, @fecha, 0, @usuario, 0,      
 NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)   
END  
ELSE  
BEGIN  
INSERT INTO QUIROFANO_PROCE_PRODU VALUES(@orden, @cie_codigo, @codpro, @cantidad, @paciente, @atencion, @fecha, 0, @usuario, 0,      
 NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)   
 END  

---------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_QuirofanoControlPedidoAdicional]    
@ate_codigo int,    
@cie_codigo bigint,    
@codpro varchar(13),     
@cantadicional FLOAT    
AS    
UPDATE QUIROFANO_PROCE_PRODU SET QPP_CANTIDAD = QPP_CANTIDAD + @cantadicional    
WHERE ATE_CODIGO = @ate_codigo AND PCI_CODIGO = @cie_codigo AND CODPRO = @codpro 

---------------------------------------------------------------------------------------------
ALTER TABLE PEDIDO_DEVOLUCION_DETALLE
ALTER COLUMN DevDetCantidad FLOAT

---------------------------------------------------------------------------------------------

alter  procedure [dbo].[sp_GuardaPedidoDevolucionDetalleQuirofano]                
(                      
 @DevCodigo as bigint,                      
 @PRO_CODIGO as bigint,                      
 @PRO_DESCRIPCION as varchar(50),                      
 @DevDetCantidad as float,                      
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
---------------------------------------------------------------------------------------------------------

USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_GuardaPedidoDevolucionDetalle]    Script Date: 29/04/2022 14:53:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  procedure [dbo].[sp_GuardaPedidoDevolucionDetalle]        
(              
 @DevCodigo as bigint,              
 @PRO_CODIGO as bigint,              
 @PRO_DESCRIPCION as varchar(50),              
 @DevDetCantidad as FLOAT ,              
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
 set @LocalSic= (select codlocal from Sic3000..Locales where Local_His = 1 )
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
  'DEVOLUCION PEDIDO HIS',                
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

-------------------------------------------------------------------------------------------------------
USE [His3000]
GO

/****** Object:  Table [dbo].[atenciones_datos_adicionales]    Script Date: 29/04/2022 16:54:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[atenciones_datos_adicionales](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ate_codigo] [int] NOT NULL,
	[ASEGURADO_EMPRESA] [varchar](50) NULL,
	[ASEGURADO_OBSERVACION] [varchar](500) NULL,
	[id_tiposdiscapacidades] [int] NULL,
	[porcentage] [int] NULL,
	[paquete] [varchar](10) NULL,
 CONSTRAINT [PK_atenciones_datos_adicionales] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[atenciones_datos_adicionales] ADD  CONSTRAINT [DF_atenciones_datos_adicionales_id_tiposdiscapacidades]  DEFAULT ((1)) FOR [id_tiposdiscapacidades]
GO

ALTER TABLE [dbo].[atenciones_datos_adicionales]  WITH CHECK ADD  CONSTRAINT [FK_atenciones_datos_adicionales_tipos_discapacidades] FOREIGN KEY([id_tiposdiscapacidades])
REFERENCES [dbo].[tipos_discapacidades] ([id])
GO

ALTER TABLE [dbo].[atenciones_datos_adicionales] CHECK CONSTRAINT [FK_atenciones_datos_adicionales_tipos_discapacidades]
GO

-----------------------------------------------------------------------------------------------------------
alter PROCEDURE [dbo].[sp_QuirofanoCuentaPacientes]    
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
INSERT INTO CUENTAS_PACIENTES VALUES((SELECT isnull((MAX(CUE_CODIGO)),0) + 1 FROM CUENTAS_PACIENTES), @ate_codigo,     
GETDATE(), @codpro, @cue_detalle, @cue_valor, @cue_cantidad, @cue_total, @cue_iva, 1, '0', @rub_codigo,    
1,@id_usuario, 0, @codpro, NULL, @descripcion, 0, NULL, @codigo_pedido, NULL, @costo, NULL,     
'N', 0, 0, 0, GETDATE())

-------------------------------------------------------------------------------------------------------------

alter PROCEDURE [dbo].[sp_QuirofanoAgregarPedidoProducto]  
@codpro varchar(15),  
@prodesc varchar(500),  
@cantidad float,  
@valor float,  
@total float,  
@ped_codigo int,   
@iva float  
AS  
DECLARE @detalle int  
SET @detalle = (isnull((SELECT MAX(PDD_CODIGO) FROM PEDIDOS_DETALLE), 0))
INSERT INTO PEDIDOS_DETALLE VALUES(@detalle + 1, @ped_codigo, @codpro, @prodesc, @cantidad, @valor, @iva, @total  
,1, 0, NULL, NULL, NULL, NULL, @codpro)

----------------------------------------------------------------------------------------------------
ALTER PROCEDURE sp_DetalleArea
@ateCodigo bigint
AS

SELECT ROUND(sum(CUE_CANTIDAD),2) AS CANTIDAD, CUE_DETALLE as DETALLE,     
 ROUND(SUM(CUE_IVA),4) AS IVA, ROUND(sum(cue_valor),2) AS TOTAL,
 ISNULL((SELECT NIV_NOMBRE FROM NIVEL_PISO NP 
 INNER JOIN HABITACIONES H ON NP.NIV_CODIGO = H.NIV_CODIGO
 WHERE H.hab_Codigo = P.HAB_CODIGO), 'OTROS') AS AREA
 FROM CUENTAS_PACIENTES CP
INNER JOIN ATENCIONES A ON CP.ATE_CODIGO = A.ATE_CODIGO
INNER JOIN PEDIDOS P ON CP.Codigo_Pedido = P.PED_CODIGO
WHERE A.ATE_CODIGO = @ateCodigo
group by PRO_CODIGO, CUE_DETALLE,P.HAB_CODIGO 

GO