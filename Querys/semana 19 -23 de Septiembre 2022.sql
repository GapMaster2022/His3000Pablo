--select codsub as CODIGO, dessub AS DESCRIPCION from Sic3000..ProductoSubdivision WHERE Pea_Codigo_His in (1, 13, 27, 19, 3)

--select * from Sic3000..ProductoSubdivision

--select * from His3000..EMPRESA
ALTER procedure sp_ActualizaValoresCuentasBienes                
(               
@p_CUE_CODIGO as bigint,      
@CUE_CANTIDAD as decimal(18,2),  
@p_CodigoAtencion as bigint,                
@p_CodigoPedido as int,                
@p_CodigoProducto as varchar(64),                
@p_Valor as decimal(10,3),                
@p_Iva as decimal(10,4),                
@p_Total as decimal(10,2)                
)                
as                
begin                
                
/*actualizo pedidos detalle*/                
update PEDIDOS_DETALLE                
set               
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
set              
CUE_VALOR_UNITARIO=@p_Valor,                
CUE_IVA=@p_Iva,                
CUE_VALOR=@p_Valor * @CUE_CANTIDAD,
CUE_CANTIDAD = @CUE_CANTIDAD       
where PRO_CODIGO=@p_CodigoProducto                
and Codigo_Pedido=@p_CodigoPedido                
and ATE_CODIGO=@p_CodigoAtencion          
AND CUE_CODIGO=@p_CUE_CODIGO          
                
/*actualizo el kardex*/                
                
update sic3000..Kardex                
set           
venta=@p_Total           
FROM CUENTAS_PACIENTES C          
INNER JOIN sic3000..Kardex K ON C.PRO_CODIGO=K.codpro          
where codpro=@p_CodigoProducto                
and numdoc=@p_CodigoPedido                
and AtencionCodigo=@p_CodigoAtencion                
and tipdoc='PED'           
AND C.CUE_CODIGO=@p_CUE_CODIGO          
          
                  
end 
go
------------------------------------------------------------------2022.09.22-------------------------------------------------------------------------------------

insert into PARAMETROS values (28,1,'PRODUCTOS SUBDIVIDION','PARA LA CARGA DE COMBO QUIROFANO',1)
go

insert into PARAMETROS_DETALLE VALUES(44,28,'PRODUCTOS SUBDIVIDION','NULL','P',0)
go

CREATE PROCEDURE sp_ParametroComboQuirofano  
as  
SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 44  

go