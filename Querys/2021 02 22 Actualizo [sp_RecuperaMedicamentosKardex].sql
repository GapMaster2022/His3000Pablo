USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_RecuperaMedicamentosKardex]    Script Date: 22/02/2021 14:12:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_RecuperaMedicamentosKardex]
(
	
	@ate_codigo VARCHAR(10),
	@rubro int,
	@check int
)
AS
BEGIN
	
	IF(@check=0)
	BEGIN
		SET @ate_codigo=(SELECT ATE_CODIGO FROM ATENCIONES WHERE ATE_NUMERO_ATENCION=@ate_codigo)
		select CUE_CODIGO AS CODIGO, CUE_DETALLE AS PRODUCTO, cast(CUE_CANTIDAD as int) AS CANTIDAD
		from CUENTAS_PACIENTES 
		where ATE_CODIGO=@ate_codigo AND RUB_CODIGO=@rubro AND 
				CUE_CODIGO NOT IN (SELECT CueCodigo FROM KARDEXMEDICAMENTOS WHERE AteCodigo=@ate_codigo) AND 
				CUE_CODIGO NOT IN (SELECT CueCodigo FROM KARDEXINSUMOS WHERE AteCodigo=@ate_codigo)
	END
	ELSE
	BEGIN

		SELECT codpro as CODIGO, despro as PRODUCTO, 1 as CANTIDAD FROM Sic3000..Producto
		

	END
END