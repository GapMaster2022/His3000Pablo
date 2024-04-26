-----------------------------------------------------------------------2023/02/16-------------------------------------------------------------------------------------------
alter table PEDIDOS_DETALLE ADD PRO_BODEGA_SIC INT DEFAULT 10 NOT NULL 
go
-----------------------------------------------------------------------2023/02/14-------------------------------------------------------------------------------------------
alter trigger [dbo].[ti_PedidosDetalle] 
on [dbo].[PEDIDOS_DETALLE] 
for insert as begin declare @FechaT as date 
declare @cantidad as decimal(18,4)                       
declare @producto as varchar(16) 
declare @CodigoPedido as Bigint 
declare @AreaPedidoHis as int 
declare @LocalSic as int 
declare @Usuario as int 
declare @CostoProducto as Decimal(18,4) 
declare @CostoProductoUlt as Decimal(10,4)
declare @Proveedor as int 
declare @PrecioVenta as Decimal(18,4) 
declare @CodigoAtencion as Bigint             
declare @HistoriaClinica as Bigint set @FechaT=getdate() -- Transformo la fecha al formato 'dd/mm/yyyy' 
declare @Fecha as BigInt select @Fecha = dbo.TransformaFercha(@FechaT)-- Transformo la fecha a numero 
declare @Grupo as int 
declare @Seccion as int
declare @Departamento as int
declare @SubGrupo as int 
declare @Division as int 
select @cantidad=INSERTED.PDD_CANTIDAD --Capturo la cantidad
from inserted
select @producto=INSERTED.PRO_CODIGO  --Capturo el codigo del producto
from inserted
select @CodigoPedido=INSERTED.PED_CODIGO --Capturo el codigo del pedido
from inserted      
select @LocalSic=INSERTED.PRO_BODEGA_SIC --Capturo el codigo del pedido
from inserted
--select @AreaPedidoHis=PEDIDOS.PEA_CODIGO --Capturo el codigo de donde se pide--from PEDIDOS--WHERE PEDIDOS.PED_CODIGO=@CodigoPedido                  
SET @AreaPedidoHis=1 -- Por defecto es la 1 Farmacia                                   
select @Usuario=PEDIDOS.ID_USUARIO --Capturo el usuario que creo el movimiento
from PEDIDOS WHERE PEDIDOS.PED_CODIGO=@CodigoPedido 
select @CodigoAtencion=PEDIDOS.ATE_CODIGO --Capturo el codigo de la atencion 
from PEDIDOS WHERE PEDIDOS.PED_CODIGO=@CodigoPedido                         
select @HistoriaClinica=PACIENTES.PAC_HISTORIA_CLINICA from   ATENCIONES,PACIENTES where  ATENCIONES.PAC_CODIGO=PACIENTES.PAC_CODIGO and ATE_CODIGO=@CodigoAtencion
--SELECT @LocalSic=codlocal--FROM SIC3000..LOCALES--where LOCALES.local_his=@AreaPedidoHis
--set @LocalSic= (select codlocal from Sic3000..Locales where Local_His = 1)      
Select @CostoProducto=cospro,@CostoProductoUlt=precos,@PrecioVenta=preven,@Grupo =codgru,@Seccion =codsec,@Departamento =coddep,
@SubGrupo =codsub,@Division =coddiv from sic3000..PRODUCTO where PRODUCTO.codpro=@producto select top 1 @Proveedor=codprv    
from sic3000..Prodprov where Prodprov.codpro=@producto
update sic3000..Bodega set existe=existe - @cantidad where codbod=@LocalSic and codpro=@producto
-- select * from Sic3000..KARDEX               
insert into sic3000..kardex values(@producto,GETDATE(),@CodigoPedido,'PED',@LocalSic,1,0,abs(@cantidad),0,
'PEDIDO HIS',@Usuario,--@CostoProducto,
@CostoProductoUlt,(@CostoProductoUlt*@cantidad),@CostoProducto,@Fecha,
@Proveedor,@PrecioVenta,@Grupo ,@Seccion ,@Departamento ,@SubGrupo ,@Division ,@Fecha,(@CostoProducto*@cantidad),null,
--/, descomentar al agregar los campos HistoriaClinica ,AtencionCodigo , Factura/
@HistoriaClinica,@CodigoAtencion,null,0,null)

end

GO
-----------------------------------------------------------------------2023/02/16-------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_QuirofanoAgregarPedidoProducto]    
@codpro varchar(15),    
@prodesc varchar(500),    
@cantidad float,    
@valor float,    
@total float,    
@ped_codigo int,     
@iva float,
@bodega int
AS    
DECLARE @detalle int    
SET @detalle = (isnull((SELECT MAX(PDD_CODIGO) FROM PEDIDOS_DETALLE), 0))  
INSERT INTO PEDIDOS_DETALLE 
VALUES(@detalle + 1, @ped_codigo, @codpro, @prodesc, @cantidad, @valor, @iva, @total,1, 0, NULL, NULL, NULL, NULL, @codpro,@bodega)

GO
-----------------------------------------------------------------------2023/02/16-------------------------------------------------------------------------------------------
      
ALTER procedure [dbo].[sp_GuardaDescuentosProductos]            
(            
 @codRubro as smallint,          
 @codProducto as nchar(15),            
 @Descuento as float,            
 @Porcentaje as float,      
 @Atencion as INT,      
 @CueCodigo as bigint      
)      
            
as            
begin         
      
    DECLARE @T_Db_Iva as float      
    DECLARE @T_Db_ValorIva as float      
    DECLARE @T_Db_Valor_Unitario AS FLOAT      
    DECLARE @T_Db_Cantidad AS FLOAT      
    declare @T_Db_Base as float      
      
 set @T_Db_Iva=0      
 set @T_Db_ValorIva=0      
 set @T_Db_Valor_Unitario=0      
 SET @T_Db_Cantidad=0      
 set @T_Db_Base=0      
      
      
   -----buscar si el producto tiene iva o no----------13/12/2019      
    select       
 @T_Db_Iva=paga_iva ,      
 @T_Db_ValorIva=iva       
 from sic3000..Producto where codpro= @codProducto      
      
    select  @T_Db_Valor_Unitario= CUE_VALOR_UNITARIO ,      
         @T_Db_Cantidad=CUE_CANTIDAD        
 from His3000..CUENTAS_PACIENTES      
 WHERE       
 PRO_CODIGO =@codProducto       
 and CUE_CODIGO =@CueCodigo      
 and ATE_CODIGO =@Atencion      
 and RUB_CODIGO= @codRubro      
      
 -----------------BUSCA VALORES PARA EL PROCESO-------------------      
 set @T_Db_Base= ROUND((@T_Db_Valor_Unitario*@T_Db_Cantidad)-@Descuento,3)      
      
 UPDATE His3000.[dbo].[CUENTAS_PACIENTES]       
 SET Descuento= @Descuento,      
 PorDescuento= @porcentaje,      
 CUE_VALOR=@T_Db_Base,      
 CUE_IVA=(@T_Db_Base*@T_Db_ValorIva)/100    
 WHERE       
 PRO_CODIGO =@codProducto       
 and CUE_CODIGO =@CueCodigo      
 and ATE_CODIGO =@Atencion      
 and RUB_CODIGO= @codRubro    
      
 end 
 go
-----------------------------------------------------------------------2023/02/16-------------------------------------------------------------------------------------------

 ALTER procedure [dbo].[sp_BuscaProductoSic3000all]                    
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
  and isnull(p.activo,0)=1   and sic3000..bodega.existe>0   
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
  and sic3000..bodega.existe>0  
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
  and sic3000..bodega.existe>0  
  and isnull(p.activo,0)=1  -- Verifico que el producto no este dado de baja                                                                                  
  order by producto asc                               
 end                                                   
 END 

 GO

-----------------------------------------------------------------------2023/02/16-------------------------------------------------------------------------------------------
ALTER procedure [dbo].[sp_ValoresAutomaticosCuentas]          
/*GENERA LOS VALORES AUTOMATICOS DE LAS CUENTAS*/        
(          
@p_CodigoAtencion as BigInt,          
@p_CodigoUsuario as int,        
@p_Dias as int,         
@p_Habitacion as varchar(30),        
@p_Convenio as int,        
@p_Empresa as int        
)          
          
as          
begin          
          
 declare @ValorServicioClinica as decimal(18,4)          
 declare @ValorAdministracionMed as decimal(18,4)          
 declare @ValorDerechoRecuperacion as decimal(18,4)          
 declare @ValorDerechoAnestecia as decimal(18,4)           
 declare @ValorHospitalizacionPaciente as decimal(18,4)          
         
 declare @PreciosHabitacion as decimal(18,4)          
 declare @DescripcionHabitacion as varchar(200)        
         
 declare @ServicioClinica as int        
 declare @AdministracionMed as int        
 declare @DerechoRecuperacion as int        
 declare @DerechoAnestecia as int        
 declare @HospitalizacionPaciente as int        
           
 declare @SecuencialCuentas as bigint          
         
 /*verifico si los valores automaticos ya han sido generados*/        
         
     
 select @p_Habitacion=codpro from SIC3000..Producto where codbar ='HAB-' + @DescripcionHabitacion        
 select @HospitalizacionPaciente=COUNT(*)         
 from CUENTAS_PACIENTES        
 where RUB_CODIGO=5 and PRO_CODIGO=@p_Habitacion-- hospitalizacion paciente        
 and ATE_CODIGO=@p_CodigoAtencion         
        
         
 select @AdministracionMed=COUNT(*)         
 from CUENTAS_PACIENTES        
 where RUB_CODIGO=17 --AdministracionMed        
 and ATE_CODIGO=@p_CodigoAtencion         
         
 if @AdministracionMed>0        
 begin        
         
  delete from CUENTAS_PACIENTES        
  where RUB_CODIGO=17 --AdministracionMed        
  and ATE_CODIGO=@p_CodigoAtencion           
 end        
       
        
         
 /***********************************************************/          
 /************VALORES DE HOSPITALIZACION********************/        
 select @PreciosHabitacion=pc.PRE_VALOR, @DescripcionHabitacion=cc.CAC_NOMBRE   
 from CATALOGO_COSTOS cc, PRECIOS_POR_CONVENIOS pc, CATEGORIAS_CONVENIOS cca        
 where cc.CAC_CODIGO=pc.CAC_CODIGO and cca.CAT_CODIGO=pc.CAT_CODIGO and cca.ASE_CODIGO=@p_Convenio   
 and CAC_NOMBRE like '%'+@p_Habitacion+'%'        
        
DECLARE @HAB_CODIGO AS INT        
  SELECT @HAB_CODIGO=hab_Codigo FROM HABITACIONES WHERE hab_Numero=@p_Habitacion        
DECLARE @PED_CODIGO AS INT        
 SELECT @PED_CODIGO=MAX(PED_CODIGO)+1 FROM PEDIDOS        
 if (@PreciosHabitacion>0)          
 begin          
  INSERT INTO PEDIDOS VALUES        
 (        
 @PED_CODIGO,        
 100,        
 5,        
 'HOSPITALIZACION PACIENTE',        
 1,        
 GETDATE(),        
 @p_CodigoUsuario,        
 @p_CodigoAtencion,        
 3,        
 3,        
 NULL,        
 0,        
 @HAB_CODIGO        
 )        
 DECLARE @PED_CODIGO_DETALLE AS INT        
 SELECT @PED_CODIGO_DETALLE=MAX(PDD_CODIGO)+1 FROM PEDIDOS_DETALLE        
 DECLARE @NombreProducto varchar(50)        
 select @p_Habitacion=codpro from SIC3000..Producto where despro=@DescripcionHabitacion        
 INSERT INTO PEDIDOS_DETALLE VALUES          
 (        
 @PED_CODIGO_DETALLE,        
 @PED_CODIGO,        
 @p_Habitacion,        
 @DescripcionHabitacion,        
 @p_Dias,        
 @PreciosHabitacion,        
 0.00,        
 @p_Dias * @PreciosHabitacion,        
 1,        
 0,        
 NULL,        
 NULL,        
 NULL,        
 NULL,        
 @p_Habitacion,
 10
 )          
  /*SECUENCIAL CUENTAS_PACIENTES*/          
          
  SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )          
  FROM CUENTAS_PACIENTES              
        
  delete from CUENTAS_PACIENTES        
  where PRO_CODIGO = @p_Habitacion -- hospitalizacion paciente        
  and ATE_CODIGO=@p_CodigoAtencion         
          
 insert into HIS3000..CUENTAS_PACIENTES           
  values          
  (          
   @SecuencialCuentas /*CUE_CODIGO*/,          
   @p_CodigoAtencion /*ATE_CODIGO*/,          
   GETDATE()/*CUE_FECHA*/,          
   @p_Habitacion /*PRO_CODIGO*/,          
   @DescripcionHabitacion  /*CUE_DETALLE*/,          
   @PreciosHabitacion /*CUE_VALOR_UNITARIO*/,          
   @p_Dias /*CUE_CANTIDAD*/,          
   @p_Dias * @PreciosHabitacion /*CUE_VALOR*/,          
   0  /*CUE_IVA*/,          
   1  /*CUE_ESTADO*/,          
   '' /*CUE_NUM_FAC*/,          
   5 /*RUB_CODIGO*/,          
   @PED_CODIGO /*PED_CODIGO*/,          
   @p_CodigoUsuario /*ID_USUARIO*/,          
   0 /*CAT_CODIGO*/,          
   @p_Habitacion /*PRO_CODIGO_BARRAS*/,          
   '' /*CUE_NUM_CONTROL*/,          
   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,          
   0 /*MED_CODIGO*/,          
   0 /*CUE_ORDER_IMPRESION*/,          
   @PED_CODIGO /*Codigo_Pedido*/  ,        
   '', --idtipo medico  23102019        
   0, ---costo        
   '', --numvale        
   'N' ---divide factura        
   ,0        
   ,0        
   ,0        
   ,''        
  )          
           
 end          
         
 /**********************************************************/          
declare @auxPerporsentaje as decimal(18,4)        
         
         
 ----------------------------------------------------------------------        
 ----------------------------------------------------------------------        
 -----------CREA PEDIDOS TABLA PEDIDOS---------------------------------        
         
 SELECT @PED_CODIGO=MAX(PED_CODIGO)+1 FROM PEDIDOS        
 select @auxPerporsentaje=pc.PRE_PORCENTAJE   
 from CATALOGO_COSTOS cc, PRECIOS_POR_CONVENIOS pc, CATEGORIAS_CONVENIOS cca        
 where cc.CAC_CODIGO=pc.CAC_CODIGO and cca.CAT_CODIGO=pc.CAT_CODIGO and cca.ASE_CODIGO=@p_Convenio  
  and cc.CAC_NOMBRE='ADMINISTRACION MEDICAMENTOS'        
  
select @ValorAdministracionMed=SUM(CUE_VALOR_UNITARIO * CUE_CANTIDAD) from CUENTAS_PACIENTES           
where RUB_CODIGO in (1,27) and ATE_CODIGO=@p_CodigoAtencion         
set @ValorAdministracionMed=(@ValorAdministracionMed * @auxPerporsentaje)/100        
  /*SECUENCIAL CUENTAS_PACIENTES*/          
  SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )          
  FROM CUENTAS_PACIENTES          
  SELECT @HAB_CODIGO=hab_Codigo FROM HABITACIONES WHERE hab_Numero=@p_Habitacion        
if (@ValorAdministracionMed>0)          
begin          
        
 INSERT INTO PEDIDOS VALUES        
 (        
 @PED_CODIGO,        
 100,        
 14,        
 'GENERACION AUTOMATICA DE ADMINISTRACION DE MEDICAMENTOS',        
 1,        
 GETDATE(),        
 @p_CodigoUsuario,        
 @p_CodigoAtencion,        
 3,        
 3,        
 NULL,        
 0,        
 @HAB_CODIGO        
 )        
        
 SELECT @PED_CODIGO_DETALLE=MAX(PDD_CODIGO)+1 FROM PEDIDOS_DETALLE        
         
 SELECT @NombreProducto = despro from Sic3000..PRODUCTO where codpro='106234'        
        
 INSERT INTO PEDIDOS_DETALLE VALUES          
 (        
 @PED_CODIGO_DETALLE,        
 @PED_CODIGO,        
 '106234',        
 @NombreProducto,        
 1,        
 @ValorAdministracionMed,        
 0.00,        
 @ValorAdministracionMed,        
 1,        
 0,        
 NULL,        
 NULL,        
 NULL,        
 NULL,        
 '106234',
 10
 )        
            
  insert into CUENTAS_PACIENTES           
  values          
  (          
   @SecuencialCuentas /*CUE_CODIGO*/,          
   @p_CodigoAtencion /*ATE_CODIGO*/,          
   GETDATE()/*CUE_FECHA*/,          
   '106234' /*PRO_CODIGO*/,          
   @NombreProducto  /*CUE_DETALLE*/,          
   @ValorAdministracionMed /*CUE_VALOR_UNITARIO*/,          
   1 /*CUE_CANTIDAD*/,          
   @ValorAdministracionMed /*CUE_VALOR*/,          
   0  /*CUE_IVA*/,          
   1  /*CUE_ESTADO*/,          
   '' /*CUE_NUM_FAC*/,          
   17 /*RUB_CODIGO*/,          
   @PED_CODIGO /*PED_CODIGO*/,          
   @p_CodigoUsuario /*ID_USUARIO*/,          
   0 /*CAT_CODIGO*/,          
   '106234' /*PRO_CODIGO_BARRAS*/,          
   '' /*CUE_NUM_CONTROL*/,          
   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,          
   0 /*MED_CODIGO*/,          
   0 /*CUE_ORDER_IMPRESION*/,          
   @PED_CODIGO /*Codigo_Pedido*/,        
   '', --idtipo medico  23102019        
   0, ---costo        
   '', --numvale        
   'N' ---divide factura          
   ,0        
   ,0        
   ,0        
   ,''        
  )          
           
 end          
  
 -----------CREA PEDIDOS TABLA PEDIDOS---------------------------------   
 -----------CREAR DERECHO DE RECUEPERACIÓN---------------------------------   
 DECLARE @PORCENTAJE_DERECHOANESTESIA AS DECIMAL(10,2)  
 DECLARE @VALOR_DERECHOANESTESIA AS DECIMAL(10,2)  
 SELECT @PED_CODIGO=MAX(PED_CODIGO)+1 FROM PEDIDOS        
 select @PORCENTAJE_DERECHOANESTESIA=pc.PRE_PORCENTAJE, @VALOR_DERECHOANESTESIA=PC.PRE_VALOR  
 from CATALOGO_COSTOS cc, PRECIOS_POR_CONVENIOS pc, CATEGORIAS_CONVENIOS cca        
 where cc.CAC_CODIGO=pc.CAC_CODIGO and cca.CAT_CODIGO=pc.CAT_CODIGO and cca.ASE_CODIGO=@p_Convenio  
  and cc.CAC_NOMBRE='DERECHO DE ANESTESIA'        
SELECT @HAB_CODIGO=hab_Codigo FROM HABITACIONES WHERE hab_Numero=@p_Habitacion   
IF(@PORCENTAJE_DERECHOANESTESIA > 0)  
BEGIN  
 select @ValorDerechoAnestecia=SUM(CUE_VALOR_UNITARIO * CUE_CANTIDAD) from CUENTAS_PACIENTES           
 where RUB_CODIGO in (7) and ATE_CODIGO=@p_CodigoAtencion  AND CUE_ESTADO=1    
  
 set @ValorDerechoAnestecia=(@ValorDerechoAnestecia * @PORCENTAJE_DERECHOANESTESIA)/100        
 /*SECUENCIAL CUENTAS_PACIENTES*/          
 SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )          
 FROM CUENTAS_PACIENTES     
END  
IF(@VALOR_DERECHOANESTESIA > 0)  
BEGIN  
 set @ValorDerechoAnestecia = @VALOR_DERECHOANESTESIA      
 /*SECUENCIAL CUENTAS_PACIENTES*/          
 SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )          
 FROM CUENTAS_PACIENTES  
END  
if (@ValorDerechoAnestecia > 0)          
begin          
        
 INSERT INTO PEDIDOS VALUES        
 (        
 @PED_CODIGO,        
 100,        
 14,        
 'GENERACION AUTOMATICA DEL DERECHO DE ANESTESIA',        
 1,        
 GETDATE(),        
 @p_CodigoUsuario,        
 @p_CodigoAtencion,        
 3,        
 3,        
 NULL,        
 0,        
 @HAB_CODIGO        
 )        
        
 SELECT @PED_CODIGO_DETALLE=MAX(PDD_CODIGO)+1 FROM PEDIDOS_DETALLE        
         
 SELECT @NombreProducto = despro from Sic3000..PRODUCTO where codpro='110193'        
        
 INSERT INTO PEDIDOS_DETALLE VALUES          
 (        
 @PED_CODIGO_DETALLE,        
 @PED_CODIGO,        
 '110193',        
 @NombreProducto,        
 1,        
 @ValorDerechoAnestecia,        
 0.00,        
 @ValorDerechoAnestecia,        
 1,        
 0,        
 NULL,        
 NULL,        
 NULL,        
 NULL,        
 '110193',
 10
 )        
            
  insert into CUENTAS_PACIENTES           
  values          
  (          
   @SecuencialCuentas /*CUE_CODIGO*/,          
   @p_CodigoAtencion /*ATE_CODIGO*/,          
   GETDATE()/*CUE_FECHA*/,          
   '110193' /*PRO_CODIGO*/,          
   @NombreProducto  /*CUE_DETALLE*/,          
   @ValorDerechoAnestecia /*CUE_VALOR_UNITARIO*/,          
   1 /*CUE_CANTIDAD*/,          
   @ValorDerechoAnestecia /*CUE_VALOR*/,          
   0  /*CUE_IVA*/,          
   1  /*CUE_ESTADO*/,          
   '' /*CUE_NUM_FAC*/,          
   7 /*RUB_CODIGO*/,          
   @PED_CODIGO /*PED_CODIGO*/,          
   @p_CodigoUsuario /*ID_USUARIO*/,          
   0 /*CAT_CODIGO*/,          
   '110193' /*PRO_CODIGO_BARRAS*/,          
   '' /*CUE_NUM_CONTROL*/,          
   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,          
   0 /*MED_CODIGO*/,          
   0 /*CUE_ORDER_IMPRESION*/,          
   @PED_CODIGO /*Codigo_Pedido*/,        
   '', --idtipo medico  23102019        
   0, ---costo        
   '', --numvale        
   'N' ---divide factura          
   ,0        
   ,0        
   ,0        
   ,''        
  )          
           
 end          
          
end        

GO

-----------------------------------------------------------------------2023/02/17-------------------------------------------------------------------------------------------

alter procedure sp_pedido  
@NumeroPedido int,  
@Bodega as int       
as      
begin      
 select       
 PDD_CODIGO,      
 PED_CODIGO,      
 PRO_CODIGO,      
 PRO_DESCRIPCION,      
 PDD_CANTIDAD,      
 ISNULL((      
 SELECT SUM(DevDetCantidad)       
 from PEDIDO_DEVOLUCION PD, PEDIDO_DEVOLUCION_DETALLE PDD      
 WHERE PD.Ped_Codigo=PEDIDOS_DETALLE.PED_CODIGO      
 AND PD.DEVCODIGO=PDD.DEVCODIGO      
 AND PDD.PRO_CODIGO=PEDIDOS_DETALLE.PRO_CODIGO      
 ),0) AS CantidadDevuelta ,      
 PDD_VALOR,      
 PDD_IVA,      
 PDD_TOTAL,      
 PDD_ESTADO,      
 PDD_COSTO,      
 PDD_FACTURA,      
 PDD_ESTADO_FACTURA,      
 PDD_FECHA_FACTURA,      
 PDD_RESULTADO,      
 PRO_CODIGO_BARRAS       
 from       
 PEDIDOS_DETALLE      
 where PED_CODIGO=@NumeroPedido and PRO_BODEGA_SIC = @Bodega  
end 

GO
-----------------------------------------------------------------------2023/02/17-------------------------------------------------------------------------------------------
alter procedure [dbo].[sp_BuscaProductoSic3000]                    
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
  --AND p.codsub= (select codsub from sic3000..ProductoSubdivision where  Pea_Codigo_His=@p_Division)                            
  and (despro like '%' + @p_filtro + '%' )   
   and sic3000..bodega.existe>0    
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
   and sic3000..bodega.existe>0    
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
   and sic3000..bodega.existe>0    
  and isnull(p.activo,0)=1 -- Verifico que el producto no este dado de baja                                                                                  
  order by producto, stock_max asc                               
 end                                                   
                                                   
                                                   
end 

GO

-----------------------------------------------------------------------2023/02/17-------------------------------------------------------------------------------------------
ALTER PROCEDURE [dbo].[sp_FormaPagoHonorarios]
@numfac nvarchar(15)
AS
SELECT FSP.forpag, C.desclas FROM SIC3000..FacturaPago FP
LEFT JOIN SIC3000..Anticipo A ON FP.cheque_caduca = A.numrec
LEFT JOIN SIC3000..Forma_Pago FSP ON A.forpag = FSP.forpag
LEFT JOIN SIC3000..Clasificacion C ON FSP.claspag = C.codclas
WHERE FP.numdoc = @numfac

go

-----------------------------------------------------------------------2023/02/17-------------------------------------------------------------------------------------------
delete from HORARIO_ATENCION

go 

DBCC CHECKIDENT (HORARIO_ATENCION, RESEED, 0)
