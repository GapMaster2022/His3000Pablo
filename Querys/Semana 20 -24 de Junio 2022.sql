-- Sp Pablo 
CREATE PROCEDURE sp_ADMISIONES_nuevo
(@codigoPaciente int)  
AS 
BEGIN
 
 select top 20 ROW_NUMBER() OVER(
 ORDER BY ATE_FECHA_INGRESO) AS N_ORDEN, a.ATE_CODIGO AS ATE_CODIGO, a.ATE_FECHA_INGRESO AS FECHA_INGRESO,
 floor((cast(convert(varchar(8),a.ATE_FECHA_INGRESO,112) as int)-cast(convert(varchar(8),p.PAC_FECHA_NACIMIENTO,112) as int) ) / 10000) as EDAD,
 (M.MED_APELLIDO_PATERNO + ' ' + M.MED_NOMBRE1) as REFERIDO_DE, U.USR  AS COD_ADMISIONISTA, A.PAC_CODIGO AS COD_PACIENTE, 
 '' as PRIMERA,'X' as SUBSEC 
 from ATENCIONES a
 inner join PACIENTES p on a.PAC_CODIGO = p.PAC_CODIGO
 inner join MEDICOS m on a.MED_CODIGO = m.MED_CODIGO
 inner join USUARIOS u on a.ID_USUSARIO = u.ID_USUARIO
 where a.PAC_CODIGO=@codigoPaciente order by ATE_FECHA_INGRESO desc

END
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
alter procedure sp_ActualizaValoresCuentas        
(       
@p_CUE_CODIGO as bigint,        
@p_CodigoAtencion as bigint,        
@p_CodigoPedido as int,        
@p_CodigoProducto as varchar(64),        
@p_Cantidad as decimal(10,2),        
@p_Valor as decimal(10,3),        
@p_Iva as decimal(10,4),        
@p_Total as decimal(10,2)        
)        
as        
begin        
        
/*actualizo pedidos detalle*/        
update PEDIDOS_DETALLE        
set PDD_CANTIDAD=@p_Cantidad,        
PDD_VALOR=@p_Valor,        
PDD_IVA=@p_Iva,        
PDD_TOTAL=@p_Valor   
FROM CUENTAS_PACIENTES P  
INNER JOIN PEDIDOS_DETALLE PD ON P.Codigo_Pedido=PD.PED_CODIGO  
where PD.PRO_CODIGO=@p_CodigoProducto        
and PD.PED_CODIGO=@p_CodigoPedido    
AND P.CUE_CODIGO=@p_CUE_CODIGO  
        
/*actualizo cuentas pacientes*/        
update CUENTAS_PACIENTES        
set CUE_CANTIDAD=@p_Cantidad,        
CUE_VALOR_UNITARIO=@p_Valor,        
CUE_IVA=@p_Iva,        
CUE_VALOR=@p_Valor        
where PRO_CODIGO=@p_CodigoProducto        
and Codigo_Pedido=@p_CodigoPedido        
and ATE_CODIGO=@p_CodigoAtencion  
AND CUE_CODIGO=@p_CUE_CODIGO  
        
/*actualizo el kardex*/        
        
update sic3000..Kardex        
set egreso=@p_Cantidad,        
venta=@p_Valor   
FROM CUENTAS_PACIENTES C  
INNER JOIN sic3000..Kardex K ON C.PRO_CODIGO=K.codpro  
where codpro=@p_CodigoProducto        
and numdoc=@p_CodigoPedido        
and AtencionCodigo=@p_CodigoAtencion        
and tipdoc='PED'   
AND C.CUE_CODIGO=@p_CUE_CODIGO  
  
          
end 
