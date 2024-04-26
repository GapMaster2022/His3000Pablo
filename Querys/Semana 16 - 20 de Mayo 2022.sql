CREATE PROCEDURE sp_FormaPagoHonorarios
@numfac nvarchar(15)
AS
SELECT FSP.forpag, C.desclas FROM SIC3000..FacturaPago FP
LEFT JOIN SIC3000..Anticipo A ON FP.cheque_caduca = A.numrec
LEFT JOIN SIC3000..FormasPago FSP ON A.forpag = FSP.forpag
LEFT JOIN SIC3000..Clasificacion C ON FSP.claspag = C.codclas
WHERE FP.numdoc = @numfac
GO
-----------------------------------------------------------------
CREATE procedure sp_RecuperaDescuentoXrubroSinIva 
@ate_codigo bigint,  
@rubro int  
as begin  
select SUM(CUE_VALOR_UNITARIO * CUE_CANTIDAD), SUM(Descuento) from CUENTAS_PACIENTES 
where ATE_CODIGO=@ate_codigo and RUB_CODIGO=@rubro and CUE_IVA = 0 AND CUE_ESTADO = 1
end
---------------------------------------------------------------------------------------
CREATE procedure sp_RecuperaDescuentoXrubroConIva 
@ate_codigo bigint,  
@rubro int  
as begin  
select SUM(CUE_VALOR_UNITARIO * CUE_CANTIDAD), SUM(Descuento) from CUENTAS_PACIENTES 
where ATE_CODIGO=@ate_codigo and RUB_CODIGO=@rubro and CUE_IVA > 0 AND CUE_ESTADO = 1 
end