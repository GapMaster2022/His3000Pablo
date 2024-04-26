
--NO ME DEJA CREAR EL PROCEDURE POR QUE EL CAMPO IDKARDEX ES IDENTIFIED
-- CREATE PROCEDURE sp_ControlInventarioQuirofano
-- @codpro varchar(15),
-- @numdoc varchar(20),
-- @tipdoc varchar(5),
-- @id float,
-- @egreso nvarchar(100),
-- @codusu nvarchar(10),
-- @costo float,
-- @costoTotal float,
-- @fechaaux nvarchar(15),
-- @codprv float,
-- @venta float, 
-- @codsec float,
-- @coddep float,
-- @codsub float,
-- @coddiv float,
-- @hc bigint,
-- @ate_codigo bigint
-- AS
-- INSERT INTO Sic3000..Kardex(codpro, fecha, numdoc, tipdoc, codlocal, id, ingreso, egreso, saldo, descrip, codusu,
-- costo, costotot, costo2, fecha_aux, codprv, venta, codgru, codsec, coddep, codsub, coddiv, auxFecha, totCosto2, signo,
-- HistoriaClinica, AtencionCodigo, Factura, asientoContable, tipoAsiento)

-- VALUES(@codpro, GETDATE(), @numdoc, @tipdoc, '12', @id, '0', @egreso, 0, 'PEDIDO HIS',
-- @codusu, @costo, @costoTotal, @costo, @fechaaux, @codprv, @venta, 0, @codsec, @coddep, @codsub, @coddiv, @fechaaux, 
-- @costoTotal, NULL, @hc, @ate_codigo, NULL, 0, NULL)



--PARA TODOS
USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_DivideFactura4]    Script Date: 11/06/2021 08:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_DivideFactura4] 

	@Ate_Codigo1 as int
as 
begin
DECLARE @ATE_CODIGO INT
DECLARE @ATE_NUMERO_ATENCION INT
DECLARE @ATE_NUMERO_ADMISION INT
SELECT @ATE_CODIGO=MAX(ATE_CODIGO)+1 FROM ATENCIONES

--SELECT @ATE_NUMERO_ATENCION=MAX(cast(ATE_NUMERO_ATENCION AS INT)+1) FROM ATENCIONES 
--SELECT @ATE_NUMERO_ATENCION=NUMCON from numero_control  WHERE CODCON=8
 
SELECT @ATE_NUMERO_ADMISION =MAX(ATE_NUMERO_ADMISION)+1 FROM ATENCIONES 
WHERE PAC_CODIGO=(SELECT PAC_CODIGO FROM ATENCIONES WHERE ATE_CODIGO = @Ate_Codigo1)


insert into ATENCIONES
select @ATE_CODIGO, TRIM(ATE_NUMERO_ATENCION) + '*',ATE_FECHA,ATE_NUMERO_CONTROL,ATE_FACTURA_PACIENTE,
ATE_FACTURA_FECHA,ATE_FECHA_INGRESO,ATE_FECHA_ALTA,ATE_REFERIDO,ATE_REFERIDO_DE,
ATE_EDAD_PACIENTE, ATE_ACOMPANANTE_NOMBRE, ATE_ACOMPANANTE_CEDULA, ATE_ACOMPANANTE_PARENTESCO,
ATE_ACOMPANANTE_TELEFONO, ATE_ACOMPANANTE_DIRECCION, ATE_ACOMPANANTE_CIUDAD, ATE_GARANTE_NOMBRE,
ATE_GARANTE_CEDULA,ATE_GARANTE_PARENTESCO, ATE_GARANTE_MONTO_GARANTIA, ATE_GARANTE_TELEFONO,
ATE_GARANTE_DIRECCION, ATE_GARANTE_CIUDAD, ATE_DIAGNOSTICO_INICIAL, ATE_DIAGNOSTICO_FINAL,
ATE_OBSERVACIONES, ATE_ESTADO, ATE_FACTURA_NOMBRE, ATE_DIRECTORIO, PAC_CODIGO, DAP_CODIGO,
HAB_CODIGO, CAJ_CODIGO, TIA_CODIGO, ID_USUSARIO, TIR_CODIGO, AFL_CODIGO, MED_CODIGO,TIP_CODIGO,
TIF_CODIGO, TIF_OBSERVACION, @ATE_NUMERO_ADMISION, ATE_EN_QUIROFANO, FOR_PAGO, ATE_QUIEN_ENTREGA_PAC,
ATE_CIERRE_HC, ATE_FEC_ING_HABITACION, ESC_CODIGO, CUE_ESTADO, TipoAtencion, ate_discapacidad,
ate_carnet_conadis, ATE_ID_ACCIDENTE,1 from ATENCIONES
where ATE_CODIGO=@Ate_Codigo1

INSERT INTO ATENCION_DETALLE_CATEGORIAS
SELECT (SELECT MAX(ADA_CODIGO)+1 FROM ATENCION_DETALLE_CATEGORIAS), @ATE_CODIGO, CAT_CODIGO, ADA_FECHA_INICIO, ADA_FECHA_FIN, ADA_AUTORIZACION, ADA_CONTRATO,
ADA_MONTO_COBERTURA, ADA_ORDEN, ADA_ESTADO, HCC_CODIGO_TS, HCC_CODIGO_DE, HCC_CODIGO_ES
FROM ATENCION_DETALLE_CATEGORIAS
where ATE_CODIGO=@Ate_Codigo1


UPDATE CUENTAS_PACIENTES SET ATE_CODIGO=@ATE_CODIGO WHERE ATE_CODIGO=@Ate_Codigo1 AND DivideFactura='S'

UPDATE CUENTAS_PACIENTES_AUDITORIA SET AUDITADA = 0, CANTIDAD = 0 WHERE CUE_CODIGO IN (SELECT CUE_CODIGO FROM CUENTAS_PACIENTES WHERE ATE_CODIGO = @Ate_Codigo1)
--UPDATE PEDIDOS SET ATE_CODIGO=@ATE_CODIGO WHERE ATE_CODIGO=@Ate_Codigo1

--UPDATE NUMERO_CONTROL SET NUMCON=@ATE_NUMERO_ATENCION+1 WHERE CODCON=8


end



alter PROCEDURE sp_DivisionCuenta
@ate_codigo bigint,
@cantidad decimal(18, 4),
@cue_codigo bigint
AS

	DECLARE @codigo_cuenta bigint
	SET @codigo_cuenta = (SELECT MAX(CUE_CODIGO)+1 FROM CUENTAS_PACIENTES)

	INSERT INTO CUENTAS_PACIENTES
	SELECT @codigo_cuenta, @ate_codigo, CUE_FECHA, PRO_CODIGO, CUE_DETALLE, CUE_VALOR_UNITARIO,
	@cantidad, CUE_VALOR, CUE_IVA, CUE_ESTADO, CUE_NUM_FAC, RUB_CODIGO, PED_CODIGO, ID_USUARIO, 
	CAT_CODIGO, PRO_CODIGO_BARRAS, CUE_NUM_CONTROL, CUE_OBSERVACION, MED_CODIGO, CUE_ORDER_IMPRESION, 
	Codigo_Pedido, Id_Tipo_Medico, COSTO, NumVale, 'S', Descuento, PorDescuento, USUARIO_FACTURA, FECHA_FACTURA
	FROM CUENTAS_PACIENTES WHERE CUE_CODIGO = @cue_codigo

	UPDATE CUENTAS_PACIENTES SET CUE_VALOR = (CUE_CANTIDAD * CUE_VALOR_UNITARIO),
	CUE_IVA = ((CUE_CANTIDAD * CUE_VALOR_UNITARIO) * (SELECT iva FROM Sic3000..Parametros))/100 
	WHERE CUE_CODIGO = @codigo_cuenta

	UPDATE CUENTAS_PACIENTES SET CUE_CANTIDAD = CUE_CANTIDAD - @cantidad
	WHERE CUE_CODIGO = @cue_codigo

	UPDATE CUENTAS_PACIENTES SET CUE_VALOR = (CUE_CANTIDAD * CUE_VALOR_UNITARIO),
	CUE_IVA = ((CUE_CANTIDAD * CUE_VALOR_UNITARIO) * (SELECT iva FROM Sic3000..Parametros))/100 
	WHERE CUE_CODIGO = @cue_codigo

	UPDATE CUENTAS_PACIENTES_AUDITORIA SET AUDITADA = 0, CANTIDAD = 0 WHERE CUE_CODIGO IN (SELECT CUE_CODIGO FROM CUENTAS_PACIENTES WHERE ATE_CODIGO = @ate_codigo)
GO





--FIN TODOS


--SOLO ALIANZA
USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_QuirofanoProcedimientos]    Script Date: 09/06/2021 16:50:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_QuirofanoProcedimientos]
AS
SELECT PCI_CODIGO as Codigo, PCI_DESCRIPCION as Descripcion
FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_ESTADO = 1
ORDER BY PCI_DESCRIPCION ASC

ALTER trigger [dbo].[ti_PedidosDetalle] on [dbo].[PEDIDOS_DETALLE] for insert              
as              
begin              
declare @FechaT as date            
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
declare @HistoriaClinica as Bigint      
             
 set @FechaT=CAST(CONVERT(varchar(11),getdate(),103) as date) -- Transformo la fecha al formato 'dd/mm/yyyy'            
             
 declare @Fecha as BigInt            
 select @Fecha = dbo.TransformaFercha(@FechaT)-- Transformo la fecha a numero            
             
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
       
            
       
 --select @AreaPedidoHis=PEDIDOS.PEA_CODIGO --Capturo el codigo de donde se pide            
 --from PEDIDOS             
 --WHERE PEDIDOS.PED_CODIGO=@CodigoPedido            
     
 SET @AreaPedidoHis=1 -- Por defecto es la 1 Farmacia    
             
 select @Usuario=PEDIDOS.ID_USUARIO --Capturo el usuario que creo el movimiento            
 from PEDIDOS             
 WHERE PEDIDOS.PED_CODIGO=@CodigoPedido         
       
 select @CodigoAtencion=PEDIDOS.ATE_CODIGO --Capturo el codigo de la atencion            
 from PEDIDOS             
 WHERE PEDIDOS.PED_CODIGO=@CodigoPedido        
       
 select @HistoriaClinica=PACIENTES.PAC_HISTORIA_CLINICA       
 from   ATENCIONES,PACIENTES      
 where  ATENCIONES.PAC_CODIGO=PACIENTES.PAC_CODIGO      
 and    ATE_CODIGO=@CodigoAtencion      
             
 --SELECT @LocalSic=codlocal             
 --FROM SIC3000..LOCALES            
 --where LOCALES.local_his=@AreaPedidoHis            
     
 set @LocalSic= (select codlocal from Sic3000..Locales where Local_His = 1)
             
 Select @CostoProducto=cospro,  
 @CostoProductoUlt=precos,             
 @PrecioVenta=preven,            
 @Grupo =codgru,            
 @Seccion =codsec,            
 @Departamento =coddep,            
 @SubGrupo =codsub,            
 @Division =coddiv            
 from sic3000..PRODUCTO             
 where PRODUCTO.codpro=@producto            
             
 select top 1 @Proveedor=codprv            
 from sic3000..Prodprov             
 where Prodprov.codpro=@producto            
             
 update sic3000..Bodega set existe=existe - @cantidad              
 where codbod=@LocalSic              
 and codpro=@producto              
             
 -- select * from Sic3000..KARDEX            
             
 insert into sic3000..kardex            
 values            
 (            
 @producto,            
 GETDATE(),            
 @CodigoPedido,            
 'PED',            
 @LocalSic,            
 1,            
 0,            
 @cantidad,            
 0,            
 'PEDIDO HIS',            
 @Usuario,            
 --@CostoProducto,  
 @CostoProductoUlt,            
 (@CostoProductoUlt*@cantidad),            
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
 (@CostoProducto*@cantidad),            
 null,/*, descomentar al agregar los campos HistoriaClinica , AtencionCodigo , Factura*/      
 @HistoriaClinica,      
 @CodigoAtencion,      
 null    ,  
 null    ,  
 null      
 )            
             
end



    

USE [His3000]
GO
/***** Object:  StoredProcedure [dbo].[sp_ArreglaIVA]    Script Date: 21/05/2021 15:20:43 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[sp_ArreglaIVA]

(
  @Atenciones as varchar(10),
  @CUE_CODIGO AS BIGINT,
  @CODPRO AS VARCHAR(13)
)
as
begin

	DECLARE @IVA DECIMAL(18,2)
	SET @IVA = (select iva from Sic3000..Parametros)
	
   update CUENTAS_PACIENTES set CUE_VALOR=(CUE_VALOR_UNITARIO * CUE_CANTIDAD)
   where ate_codigo=@Atenciones 

   DECLARE @PAGAIVA BIT
   SELECT @PAGAIVA=paga_iva FROM Sic3000..Producto WHERE codpro=@CODPRO

   IF(@PAGAIVA = 1)
   BEGIN
	update cuentas_pacientes set cue_iva=((cue_valor_unitario*cue_cantidad) * @IVA )/100 
	where ate_codigo=@Atenciones AND CUE_CODIGO=@CUE_CODIGO
   END

end



ALTER TABLE CUENTAS_PACIENTES_AUDITORIA
ADD CANTIDAD DECIMAL (18,4) NOT NULL DEFAULT 0



ALTER TABLE ATENCIONES
ALTER COLUMN ATE_NUMERO_ATENCION NVARCHAR(20)


--SOLO ALIANZA



--SOLO PASTEUR

-- USE [His3000]
-- GO
-- /****** Object:  StoredProcedure [dbo].[sp_ActualizaDescAtencion]    Script Date: 11/06/2021 11:34:03 ******/
-- SET ANSI_NULLS ON
-- GO
-- SET QUOTED_IDENTIFIER ON
-- GO
-- ALTER  procedure [dbo].[sp_ActualizaDescAtencion] (@CodigoAtencion as int) as  
-- Set NoCount On      
-- Set Ansi_Warnings On          
-- declare @T_st_Factura nvarchar(20)            

    -- DECLARE @T_Db_Iva as float
    -- DECLARE @T_Db_ValorIva as float
    -- DECLARE @T_Db_Valor_Unitario AS FLOAT
    -- DECLARE @T_Db_Cantidad AS FLOAT
    -- declare @T_Db_Base as float
	-- declare @codProducto as varchar(15) 
	-- declare @CueCodigo as bigint 
	-- declare @codRubro as smallint
	-- DECLARE @T_SW_Ingresa as int 

	-- set @T_Db_Iva=0
    -- set @T_Db_ValorIva=0
    -- set @T_Db_Valor_Unitario=0
	-- SET @T_Db_Cantidad=0
	-- set @T_Db_Base=0
	-- set @T_SW_Ingresa=0


-- BEGIN TRANSACTION CUENTADESC
-- BEGIN TRY

	-- select top 1 
	-- @T_st_Factura=LTRIM(RTRIM(isnull(a.ATE_FACTURA_PACIENTE,0)) )
	-- from  ATENCIONES a, CUENTAS_PACIENTES c
	-- where 
	-- a.ATE_CODIGO = @CodigoAtencion
	-- and a.ATE_CODIGO =c.ATE_CODIGO
    
	 -- if isnull(@T_st_Factura,0)='0'
	 -- set @T_SW_Ingresa=1

	 -- if isnull(@T_st_Factura,0)=' '	
	 -- set @T_SW_Ingresa=1

	 -- if @T_SW_Ingresa=1 

     -- begin --1       

	    -- -----
		-- --print 'ingreso '
		-- --print @T_SW_Ingresa

		-- Declare Registros Cursor For   
	 
		-- SELECT 
			-- PRO_CODIGO,
			-- CUE_CODIGO,
			-- RUB_CODIGO
		-- from 
			-- His3000..CUENTAS_PACIENTES 
		-- where 
		-- ATE_CODIGO = @CodigoAtencion


		-- Open Registros
		-- Fetch Next From Registros
    
		-- Into @codProducto,@CueCodigo,@codRubro

			 -- While @@Fetch_Status = 0  
                
			 -- BEGIN   
		
					-- select 
					-- @T_Db_Iva=paga_iva ,
					-- @T_Db_ValorIva=iva 
					-- from sic3000..Producto where codpro= @codProducto

					-- select  @T_Db_Valor_Unitario= CUE_VALOR_UNITARIO ,
							-- @T_Db_Cantidad=CUE_CANTIDAD 	
					-- from His3000..CUENTAS_PACIENTES
					-- WHERE 
					-- PRO_CODIGO =@codProducto 
					-- and CUE_CODIGO =@CueCodigo
					-- and ATE_CODIGO =@CodigoAtencion
					-- and RUB_CODIGO= @codRubro

					-- set @T_Db_Base= ROUND((@T_Db_Valor_Unitario*@T_Db_Cantidad),4)

					-- update  HIS3000..CUENTAS_PACIENTES  
					-- set PorDescuento =0, Descuento =0
					-- where ATE_CODIGO=@CodigoAtencion
		
					-- UPDATE His3000.[dbo].[CUENTAS_PACIENTES] 
					-- SET 
					-- CUE_VALOR=@T_Db_Base,
					-- CUE_IVA=(@T_Db_Base*@T_Db_ValorIva)/100
					-- WHERE 
					-- PRO_CODIGO =@codProducto 
					-- and CUE_CODIGO =@CueCodigo
					-- and ATE_CODIGO =@CodigoAtencion
					-- and RUB_CODIGO= @codRubro


					-- fetch Next From Registros
					-- Into @codProducto,@CueCodigo,@codRubro
			 
			 -- END

			 -- Close Registros
			 -- Deallocate Registros
			 -- PRINT'ACTUALIZO'
			 -- COMMIT TRANSACTION  CUENTADESC 

			 -- return 1
	   -- END
	-- ELSE
		-- begin
	 	    -- PRINT 'NO HACE NADA'
			 -- --Close Registros
			 -- --Deallocate Registros
			 -- ROLLBACK TRANSACTION CUENTADESC	
		   -- return 0  
		-- end
-- END TRY
    
	

-- BEGIN CATCH

	-- DEALLOCATE Registros
	-- ROLLBACK TRANSACTION CUENTA
	-- PRINT 'ERROR REVERSAR PROCESO'	
	-- return 0

-- END CATCH

--FIN PASTEUR