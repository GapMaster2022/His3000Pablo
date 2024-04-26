USE His3000
GO
-- ***********************************Mario Valencia 25-03-2024********************************************* --    
alter table ATENCIONES add ATE_FUENTE_INFORMACION VARCHAR(500)
alter table ATENCIONES add ATE_INSTITUCION_ENTREGA VARCHAR(500)
alter table ATENCIONES add ATE_INSTITUCION_TELEFONO VARCHAR(500)
GO
-- ***********************************Mario Valencia 25-03-2024********************************************* --    
alter table CUENTAS_PACIENTES_COPAGO add ID_USUARIO_MODIFICA BIGINT 
alter table CUENTAS_PACIENTES_COPAGO add ESTADO BIT
alter table COPAGO add ESTADO BIT
GO

update CUENTAS_PACIENTES_COPAGO set ID_USUARIO_MODIFICA = 1
update CUENTAS_PACIENTES_COPAGO set ESTADO = 0
update COPAGO set ESTADO = 0
GO
-------------------------------------------------------------------------------------------------------
-- ***********************************Mario Valencia 25-03-2024********************************************* --    
ALTER procedure [dbo].[sp_ArreglaIVABase]      
 @ate varchar(20)      
as      
begin      

UPDATE CUENTAS_PACIENTES SET CUE_IVA=(CUE_VALOR_UNITARIO*CUE_CANTIDAD - DESCUENTO) * (SELECT PORCENTAJE FROM TABLA17sri WHERE ACTIVO = 1) WHERE ATE_CODIGO=@ate AND CUE_IVA>0       
      
UPDATE CUENTAS_PACIENTES SET CUE_IVA=(CUE_VALOR_UNITARIO*CUE_CANTIDAD - DESCUENTO) * (SELECT PORCENTAJE FROM TABLA17sri WHERE ACTIVO = 1) WHERE CUE_CODIGO IN      
(      
SELECT CUE_CODIGO FROM CUENTAS_PACIENTES WHERE PRO_CODIGO IN (      
SELECT codpro FROM Sic3000..Producto WHERE codpro IN (      
SELECT PRO_CODIGO FROM CUENTAS_PACIENTES WHERE CUE_IVA = 0 AND ATE_CODIGO = @ate) AND paga_iva=1)      
AND ATE_CODIGO=@ate AND CUE_CANTIDAD>0 AND CUE_VALOR_UNITARIO>0 and RUB_CODIGO<>46)      
      
UPDATE CUENTAS_PACIENTES SET CUE_IVA=0 WHERE CUE_CODIGO IN      
(      
SELECT CUE_CODIGO FROM CUENTAS_PACIENTES WHERE PRO_CODIGO IN (      
SELECT codpro FROM Sic3000..Producto WHERE codpro IN (      
SELECT PRO_CODIGO FROM CUENTAS_PACIENTES WHERE CUE_IVA > 0 AND ATE_CODIGO = @ate) AND paga_iva=0)      
AND ATE_CODIGO=@ate AND CUE_CANTIDAD>0 AND CUE_VALOR_UNITARIO>0 and RUB_CODIGO<>46)      
      
end 

-- ***********************************Mario Valencia 27-03-2024********************************************* --  
alter procedure [dbo].[sp_GuardaFactura]      
(                        
  @numnot as nvarchar(20),                        
  @tipdoc as float(8),                        
  @codloc as nvarchar(20),                        
  @codven as nvarchar(20),                        
  @numfac as nvarchar(20),                        
  @codcli as nvarchar(20),                        
  @tipcli as nvarchar(20),                        
  @fecha as date,                        
  @hora as nvarchar(100),                        
  @ruc as nvarchar(26),                        
  @pordes as nvarchar(106),                        
  @subtotal as float,                        
  @desctot as float,                        
  @totsiva as float,                        
  @totciva as float,                        
  @iva as float,                        
  @total as float,                        
  @regalia as float(8),                        
  @fecha1 as nvarchar(16),                        
  @cancelado as nvarchar(40),                        
  @items as float(8),                        
  @caja as nvarchar(20),                        
  @nomcli as nvarchar(100),                        
  @dircli as nvarchar(100),                        
  @telcli as nvarchar(40),                        
  @ruccli as nvarchar(26),                        
  @obs as nvarchar(500),                        
  @numguirem as nvarchar(20),                        
  @motivo as nvarchar(100),                        
  @ructra as nvarchar(26),                        
  @nomtra as nvarchar(100),                        
  @codcobcli as float(8),                        
  @codvencli as float(8),                        
  @fecven as date,                        
  @numorden as nvarchar(20),                        
  @porven as float(8),                        
  @formpagPro as nvarchar(100),                        
  @validez as nvarchar(100),                        
  @tiempoentrega as nvarchar(100),                        
  @numfac2 as nvarchar(100),                        
  @porcobrar as bit,                        
  @Impresa as bit,                        
  @cajero as float(8),                        
  @subt_Dev as float(8),                        
  @coniva_Dev as float(8),                        
  @siniva_Dev as float(8),                        
  @desct_Dev as float(8),                        
  @iva_Dev as float(8),                        
  @Tot_Dev as float(8),                        
  @pormayor as bit,                        
  @facturada as bit,                        
  @coniva as bit,                        
  @imprimedesct as bit,                        
  @autorizacion as nvarchar(32),                        
  @GrupoCliente as bit,                        
  @EmpId as int,                        
  @ConvId as int ,                      
  @HistoriaClinica as Bigint ,                      
  @CodigoAtencion as Bigint,    
  @tipoIdentificacion as nvarchar(2),
  @porcentajeIva as float(8)
)                         
as                        
begin                
 -------------------Ingreso del paciente en la BD Sic3000-------------------------------------      
 DECLARE @VerificaDatosPaciente as int      
 declare @CodigoCliente varchar(128)      
      
 select @VerificaDatosPaciente=count(*)    
      
 from  sic3000..cliente      
 where ruccli=@ruccli      
      
 --VERIFICO SI EL NUMERO DE CONTROL ESTA CORRECO NUMERO ACTUAL + 1 AL DE LA TABLA CLIENTE      
 DECLARE @CODCLIENTE BIGINT      
 DECLARE @CODNUMEROCONTROL BIGINT      
      
 select @CODCLIENTE = MAX(CAST (codcli as bigint)+1) from Sic3000..Cliente      
      
 UPDATE Sic3000..Numero_Control SET numcon = @CODCLIENTE WHERE codcon=16       
      
 /*      
 SELECT @CODNUMEROCONTROL = numcon FROM Sic3000..Numero_Control WHERE codcon=16      
      
 IF (@CODCLIENTE = @CODNUMEROCONTROL)      
 BEGIN      
 UPDATE Sic3000..Numero_Control SET numcon = numcon + 1 WHERE codcon=16       
 END      
 */      
      
 /*select  @VerificaDatosPaciente*/                       
      
 if @VerificaDatosPaciente=0              
      
 begin                    
      
  declare @DatosCliente as varchar(128)                      
      
  declare @DireccionCliente as varchar(128)                      
      
  declare @TelefonoCliente as varchar(128)                      
      
  declare @CelularCliente as varchar(128)                      
      
  declare @RucCliente as varchar(128)                                     
      
  declare @CodigoPaciente BigInt                    
      
                        
      
  select @CodigoCliente=cast(numcon as varchar(128))                      
      
  from sic3000..Numero_Control                      
      
  where codcon= 16                      
      
                        
      
  select                       
      
  @DatosCliente= PAC_APELLIDO_PATERNO + ' ' + PAC_APELLIDO_MATERNO +' '+ PAC_NOMBRE1 + ' ' + PAC_NOMBRE2,                      
      
  @RucCliente=PAC_IDENTIFICACION,       
      
  @CodigoPaciente=PAC_CODIGO                      
      
  from PACIENTES                      
      
  where PAC_HISTORIA_CLINICA= cast(@HistoriaClinica as NCHAR(20))                      
      
                        
      
  select top 1                      
      
  @TelefonoCliente=PACIENTES_DATOS_ADICIONALES.DAP_TELEFONO1,                      
      
  @CelularCliente= PACIENTES_DATOS_ADICIONALES.DAP_TELEFONO2,                      
      
  @DireccionCliente=PACIENTES_DATOS_ADICIONALES.DAP_DIRECCION_DOMICILIO                      
      
  from PACIENTES_DATOS_ADICIONALES                      
      
  where PAC_CODIGO=@CodigoPaciente                      
      
  order by DAP_CODIGO desc                      
      
            
      
  insert into Sic3000..Cliente                      
      
  (                   
      
  codcli,                      
      
  tipcli,                      
      
  nomcli,                      
      
  dircli,                      
      
  telcli,                      
      
  telcli1,                      
      
  ruccli,      
      
  codcue,                      
      
  EMPID,                
      
  obs,    
      
  TipoIdentificacion    
      
  )                      
      
  values                      
      
  (                      
      
  @CodigoCliente,                      
      
  6,                      
      
  @nomcli,                      
      
  @dircli,                      
      
  @Telcli,                      
      
  @Telcli,                      
      
  @Ruc,        
      
  '112201-001',                    
      
  1,                  
      
  'Historia. ' + cast(@HistoriaClinica as varchar(32)) + '. Atencion. ' + cast(@CodigoAtencion as varchar(32)),    
      
  @tipoIdentificacion    
      
  )                      
      
                        
   ---- ACTUALIZA--      
  update Sic3000..Numero_Control                      
  set numcon= numcon + 1                      
  from sic3000..Numero_Control                      
  where codcon= 16                      
      
       
 end                   
      
 ELSE                  
      
 BEGIN                  
      
                 
      
  select @CodigoCliente=codcli                  
      
  from  sic3000..cliente                     
      
  where ruccli=@ruccli                  
      
                   
      
 END      
      
                    
      
 --------------------------- FIN INGRESO DATOS CLIENTE------------------------------                    
      
                        
      
 insert into Sic3000..Nota values                        
      
 (                         
      
  @numnot,                        
      
  @tipdoc,                        
      
  @codloc,                        
      
  @codven,                        
      
  @numfac,                        
      
  @CodigoCliente,                        
      
  @tipcli,                        
      
  @fecha,                        
      
  @hora,                        
      
  @ruc,                        
      
  @pordes,                        
      
  @subtotal,    
      
  @desctot,                        
      
  @totsiva,                      
      
  @totciva,                        
      
  @iva,                        
      
  @total,                        
      
  @regalia,                        
      
  @fecha1,                        
      
  @cancelado,                        
      
  @items,                        
      
  @caja,                        
      
  @nomcli,                        
      
  @dircli,                        
      
  @telcli,                        
      
  @ruccli,                        
      
  @obs,             
      
  @numguirem,                        
      
  @motivo,                        
      
  @ructra,                        
      
  @nomtra,                        
      
  @codcobcli,                        
      
  @codvencli,                        
      
  @fecven,                        
      
  @numorden,                        
      
  @porven,                        
      
  @formpagPro,                        
      
  @validez,                        
      
  @tiempoentrega,                        
      
  @numfac2,                        
      
  @porcobrar,                        
      
  @Impresa,                        
      
  @cajero,                        
      
  @subt_Dev,                        
      
  @coniva_Dev,       
      
  @siniva_Dev,                        
      
  @desct_Dev,                        
      
  @iva_Dev,                        
      
  @Tot_Dev,                        
      
  @pormayor,                        
      
  @facturada,                        
      
  @coniva,                        
      
  @imprimedesct,                        
      
  @autorizacion,                        
      
  @GrupoCliente,                        
      
  @EmpId,                        
      
 @ConvId,      
       
 NULL,      
      
 NULL,      
      
 0,      
      
 NULL,      
      
 @porcentajeIva     
      
 )                        
      
                       
      
 -- Actualizo los datos de los pedidos en el kardex                      
      
                       
      
 --update Sic3000..kardex set Factura=@numnot                      
      
 --where HistoriaClinica=@HistoriaClinica                      
      
 --and AtencionCodigo=@CodigoAtencion                 
      
             
      
 select @CodigoCliente as RESULTADO -- RETORNO EL CODIGO DE EL CLIENTE CREADO PARA GENERAR LOS DATOS DE CXC            
      
                           
      
end 

