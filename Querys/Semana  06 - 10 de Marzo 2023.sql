------------------------------------------------------------------------------------------------------------------
ALTER procedure sp_recuperar_Tarifarios_Cirugia
as begin
select TOP 100 TAD_REFERENCIA CODIGO, TRIM(UPPER(TAD_NOMBRE)) DESCRIPCION, TAD_UVR UVR from TARIFARIOS_DETALLE where EST_CODIGO in (
select EST_CODIGO from ESPECIALIDADES_TARIFARIOS where EST_CODIGO in (
select EST_CODIGO from ESPECIALIDADES_TARIFARIOS where EST_PADRE in (
select EST_CODIGO from ESPECIALIDADES_TARIFARIOS where EST_PADRE=34))
) AND TAD_REFERENCIA <> '' ORDER BY 1
end
GO
--------------------------------------------------------------------------------------------------------------------
--CREATE PROCEDURE sp_GuardaTotalUCI    
--@ateCodigo as BIGINT,    
--@p_CodigoUsuario AS INT,    
--@p_Dias as int,    
--@HAB_CODIGO AS INT,    
--@CodigoConvenio as INT    
--AS    
--BEGIN    
-- DECLARE @CUECODIGO AS BIGINT  
-- SET @CUECODIGO=0  
-- SELECT @CUECODIGO=CUE_CODIGO FROM CUENTAS_PACIENTES WHERE ATE_CODIGO=@ateCodigo AND PRO_CODIGO=107405  
-- IF(@CUECODIGO <> 0)  
-- BEGIN   
  
--  DELETE FROM CUENTAS_PACIENTES WHERE ATE_CODIGO=@ateCodigo AND PRO_CODIGO=107405  
  
-- END  
-- DECLARE @PED_CODIGO AS bigint     
-- SELECT @PED_CODIGO=MAX(PED_CODIGO)+1 FROM PEDIDOS       
-- INSERT INTO PEDIDOS VALUES        
-- (        
-- @PED_CODIGO,        
-- 100,        
-- 3,        
-- (select despro from SIC3000..Producto where codpro=107405),        
-- 1,        
-- GETDATE(),        
-- @p_CodigoUsuario,        
-- @ateCodigo,        
-- 3,        
-- 3,        
-- NULL,        
-- 0,        
-- @HAB_CODIGO        
-- )     
     
     
-- DECLARE @PED_CODIGO_DETALLE AS INT        
-- SELECT @PED_CODIGO_DETALLE=MAX(PDD_CODIGO)+1 FROM PEDIDOS_DETALLE        
-- DECLARE @NombreProducto varchar(50)          
     
-- INSERT INTO PEDIDOS_DETALLE VALUES          
-- (        
-- @PED_CODIGO_DETALLE,        
-- @PED_CODIGO,        
-- 107405,        
-- (select despro from SIC3000..Producto where codpro=107405),        
-- @p_Dias,        
-- isnull(dbo.f_precioConvenio(@CodigoConvenio,107405), (SELECT PREVEN from SIC3000..Producto where codpro=107405)),    
-- 0.00,        
-- isnull(dbo.f_precioConvenio(@CodigoConvenio,107405) * @p_Dias, (SELECT PREVEN * @p_Dias from SIC3000..Producto where codpro=107405)),       
-- 1,        
-- 0,        
-- NULL,        
-- NULL,        
-- NULL,        
-- NULL,        
-- 107405        
-- )          
--  /*SECUENCIAL CUENTAS_PACIENTES*/          
--  declare @SecuencialCuentas as bigint          
--  SELECT @SecuencialCuentas = (ISNULL(MAX(CUE_CODIGO),0) + 1 )          
--  FROM CUENTAS_PACIENTES       
-- insert into CUENTAS_PACIENTES           
--  values          
--  (          
--   @SecuencialCuentas /*CUE_CODIGO*/,          
--   @ateCodigo /*ATE_CODIGO*/,          
--   GETDATE()/*CUE_FECHA*/,          
--   107405 /*PRO_CODIGO*/,          
--   (select despro from SIC3000..Producto where codpro=107405)  /*CUE_DETALLE*/,          
--   isnull(dbo.f_precioConvenio(@CodigoConvenio,107405), (SELECT PREVEN from SIC3000..Producto where codpro=107405)) /*CUE_VALOR_UNITARIO*/,          
--   @p_Dias /*CUE_CANTIDAD*/,          
--   isnull(dbo.f_precioConvenio(@CodigoConvenio,107405) * @p_Dias, (SELECT PREVEN * @p_Dias from SIC3000..Producto where codpro=107405)) /*CUE_VALOR*/,          
--   0  /*CUE_IVA*/,          
--   1  /*CUE_ESTADO*/,          
--   '' /*CUE_NUM_FAC*/,      
--   12 /*RUB_CODIGO*/,          
--   @PED_CODIGO /*PED_CODIGO*/,          
--   @p_CodigoUsuario /*ID_USUARIO*/,          
--   0 /*CAT_CODIGO*/,          
--   107405 /*PRO_CODIGO_BARRAS*/,          
--   '' /*CUE_NUM_CONTROL*/,          
--   'VALORES AUTOMATICOS HIS3000' /*CUE_OBSERVACION*/,          
--   0 /*MED_CODIGO*/,          
--   0 /*CUE_ORDER_IMPRESION*/,          
--   @PED_CODIGO /*Codigo_Pedido*/  ,        
--   '', --idtipo medico  23102019        
--   0, ---costo        
--   '', --numvale        
--   'N' ---divide factura        
--   ,0        
--   ,0        
--   ,0        
--   ,''  
--  )          
--END 

--GO
------------------------------------------------------------------------------------------------------------------
alter table REGISTRO_QUIROFANO add bodega int default 12 not null
GO
------------------------------------------------------------------------------------------------------------------
alter PROCEDURE [dbo].[sp_QuirofanoPacientes]     
@bodega int  
AS          
SELECT A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_HISTORIA_CLINICA AS HC,          
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,          
P.PAC_IDENTIFICACION AS Identificacion, H.hab_Numero AS Habitacion, A.ATE_FECHA_INGRESO AS 'F. Ingreso',          
P.PAC_CODIGO, A.ATE_CODIGO,          
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico,          
C.CAT_NOMBRE AS Aseguradora, T.TIP_DESCRIPCION AS TIPO, P.PAC_GENERO, TA.TIA_DESCRIPCION, P.PAC_FECHA_NACIMIENTO,          
ISNULL((SELECT COUNT(ATE_CODIGO) FROM REGISTRO_QUIROFANO where ATE_CODIGO=A.ATE_CODIGO and bodega=@bodega ), 0) AS 'PROCE_AGREGADOS',          
ISNULL((SELECT COUNT(ATE_CODIGO) FROM REGISTRO_QUIROFANO where ATE_CODIGO=A.ATE_CODIGO and estado=0 and bodega=@bodega), 0) AS 'PROCE_CERRADOS',
ISNULL((SELECT COUNT(ATE_CODIGO) FROM REGISTRO_QUIROFANO where ATE_CODIGO=A.ATE_CODIGO and estado=1 and bodega=@bodega), 0) AS 'CANT_PROCE'          
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

GO
------------------------------------------------------------------------------------------------------------------
  
alter PROCEDURE sp_QuirofanoPacientesGastro  
@bodega int  
AS        
SELECT A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_HISTORIA_CLINICA AS HC,        
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,        
P.PAC_IDENTIFICACION AS Identificacion, H.hab_Numero AS Habitacion, A.ATE_FECHA_INGRESO AS 'F. Ingreso',        
P.PAC_CODIGO, A.ATE_CODIGO,        
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico,        
C.CAT_NOMBRE AS Aseguradora, T.TIP_DESCRIPCION AS TIPO, P.PAC_GENERO, TA.TIA_DESCRIPCION, P.PAC_FECHA_NACIMIENTO,        
ISNULL((SELECT COUNT(ATE_CODIGO) FROM REGISTRO_QUIROFANO where ATE_CODIGO=A.ATE_CODIGO and bodega=@bodega ), 0) AS 'PROCE_AGREGADOS',          
ISNULL((SELECT COUNT(ATE_CODIGO) FROM REGISTRO_QUIROFANO where ATE_CODIGO=A.ATE_CODIGO and estado=0 and bodega=@bodega), 0) AS 'PROCE_CERRADOS',
ISNULL((SELECT COUNT(ATE_CODIGO) FROM REGISTRO_QUIROFANO where ATE_CODIGO=A.ATE_CODIGO and estado=1 and bodega=@bodega), 0) AS 'CANT_PROCE'      
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

GO
------------------------------------------------------------------------------------------------------------------
create procedure sp_QuirofanoMostrarProcedimientoPacienteRecuperado
@pci_codigo bigint,
@bodega int,
@usuar int  
as begin  
  
 declare @usuario varchar(500)  
 set @usuario = (select CONCAT( NOMBRES, ' ', APELLIDOS ) from USUARIOS where ID_USUARIO=@usuar) 
 
 select p.codpro Codigo, p.despro Producto, ps.dessub Grupo, b.existe Stock,rq.RQ_CANTIDAD 'Cant. Original', 
 rq.RQ_CANTIDADADICIONAL 'Cant. Adicional', rq.RQ_CANTIDADDEVOLUCION Devoluciones, 
 rq.RQ_FECHACREACION Fecha, rq.PED_CODIGO Orden, @usuario Usuario, p.preven
 from REPOSICION_QUIROFANO rq inner join Sic3000..Producto p on rq.CODPRO = p.codpro
 inner join Sic3000..bodega b on p.codpro=b.codpro 
 inner join Sic3000..ProductoSubdivision ps on p.codsub=ps.codsub
 where PCI_CODIGO=@pci_codigo and b.codbod=@bodega
  
end
