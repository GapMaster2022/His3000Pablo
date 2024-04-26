CREATE view ProductosReposicion    
as  
SELECT P.codpro AS CODIGO, P.despro AS PRODUCTO, SUM( RQ.RQ_CANTIDAD) AS TOTAL,         
RQ.RQ_FECHACREACION AS 'F. PROCEDI',        
RQ.ATE_CODIGO, RQ.PCI_CODIGO,        
PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2        
+ ' - ' + PC.PCI_DESCRIPCION AS 'PACIENTE - PROCEDIMIENTO', PED_CODIGO AS 'Nº PEDIDO'  
FROM Sic3000..Producto P        
INNER JOIN REPOSICION_QUIROFANO RQ ON P.codpro = RQ.CODPRO        
INNER JOIN ATENCIONES A ON RQ.ATE_CODIGO = A.ATE_CODIGO        
INNER JOIN PACIENTES PA ON A.PAC_CODIGO = PA.PAC_CODIGO        
INNER JOIN PROCEDIMIENTOS_CIRUGIA PC ON RQ.PCI_CODIGO = PC.PCI_CODIGO        
GROUP BY P.codpro, P.despro, RQ_CANTIDAD, RQ_FECHACREACION, Convert(Varchar(10),RQ_FECHAPEDIDO,103), RQ_FECHAREPOSICION,        
PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2        
+ ' - ' + PC.PCI_DESCRIPCION, PED_CODIGO, RQ_NUMREPOSICION, RQ.ATE_CODIGO, RQ.PCI_CODIGO 


------------------------------------------------------------------------------------------------------------------------------------------------

CREATE procedure sp_TodosProcedimientos
@desde datetime,  
@hasta datetime  
AS  
declare @bodega int
set @bodega = 12   
SELECT A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_HISTORIA_CLINICA AS HC,        
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,        
P.PAC_IDENTIFICACION AS Identificacion, H.hab_Numero AS Habitacion, A.ATE_FECHA_INGRESO AS 'F. Ingreso',        
P.PAC_CODIGO, A.ATE_CODIGO,        
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico,        
C.CAT_NOMBRE AS Aseguradora, T.TIP_DESCRIPCION AS TIPO, P.PAC_GENERO, TA.TIA_DESCRIPCION,
pr.PRODUCTO,PR.TOTAL,PR.ATE_CODIGO,PCI_CODIGO
FROM PACIENTES P        
INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO        
INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo        
INNER JOIN TIPO_INGRESO T ON A.TIP_CODIGO = T.TIP_CODIGO        
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO        
INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO        
INNER JOIN CATEGORIAS_CONVENIOS C ON ADC.CAT_CODIGO = C.CAT_CODIGO        
INNER JOIN TIPO_TRATAMIENTO TA ON A.TIA_CODIGO = TA.TIA_CODIGO
left join ProductosReposicion pr on a.ATE_CODIGO = pr.ATE_CODIGO
WHERE  T.TIP_CODIGO in(1,2,3) and a.ATE_FECHA_INGRESO BETWEEN @desde and @hasta     
--AND  A.ATE_ESTADO = 1    
ORDER BY
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 

------------------------------------------------------------------------------------------------------------------------------------------------

alter PROCEDURE [dbo].[sp_RecuperaMedicamentosKardex]    
(    
     
 @ate_codigo VARCHAR(10),    
 @rubro int,    
 @check int    
)    
AS    
BEGIN    

  --SET @ate_codigo=(SELECT ATE_CODIGO FROM ATENCIONES WHERE ATE_NUMERO_ATENCION=@ate_codigo)    
  select CUE_CODIGO AS CODIGO, CUE_DETALLE AS PRODUCTO, cast(CUE_CANTIDAD as int) AS CANTIDAD ,PRO_CODIGO    
  from CUENTAS_PACIENTES     
  where ATE_CODIGO=@ate_codigo AND RUB_CODIGO=@rubro AND     
    CUE_CODIGO NOT IN (SELECT CueCodigo FROM KARDEXMEDICAMENTOS WHERE AteCodigo=@ate_codigo) AND     
    CUE_CODIGO NOT IN (SELECT CUE_CODIGO FROM KARDEX_INSUMOS WHERE ATE_CODIGO=@ate_codigo group by CUE_CODIGO)    
	order by CUE_DETALLE
END 
  

