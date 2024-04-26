-- CREATE PROCEDURE sp_QuirofanoProductoSic
-- AS
-- SELECT P.codpro AS CODIGO, P.despro AS PRODUCTO, B.existe AS STOCK, QP.QP_GRUPO AS GRUPO
-- FROM QUIROFANO_PRODUCTOS QP
-- INNER JOIN Sic3000..Producto P ON QP.CODPRO = P.codpro
-- INNER JOIN Sic3000..Bodega B ON P.codpro = B.codpro 
-- WHERE B.codbod = 12
-- ORDER BY 2 ASC
-- GO






-- ALTER PROCEDURE sp_ReponerAnticipoSic  
-- @numrec nvarchar(10),  
-- @monto float  
  
-- AS  
-- IF((SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec) = 0)  
-- BEGIN  
 -- UPDATE Sic3000..Anticipo SET monto = @monto, utilizado = 0, cancelado = 0 where numrec = @numrec  
-- END  
-- ELSE  
-- BEGIN
	-- DECLARE @inicial FLOAT
	-- DECLARE @montoActual FLOAT
	-- SET @inicial = (SELECT montoini FROM Sic3000..Anticipo WHERE numrec = @numrec)
	-- SET @montoActual = (SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec)
	-- IF((SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec) > @inicial)
	-- BEGIN
		-- UPDATE Sic3000..Anticipo SET monto = monto WHERE numrec = @numrec 
	-- END
	-- ELSE
	-- BEGIN
		-- IF((SELECT montoini FROM Sic3000..Anticipo WHERE numrec = @numrec) > @montoActual + @monto)
		-- BEGIN
			-- UPDATE Sic3000..Anticipo SET monto = monto + @monto WHERE numrec = @numrec 
		-- END
		-- ELSE
		-- BEGIN
			-- UPDATE Sic3000..Anticipo SET monto = monto WHERE numrec = @numrec 
		-- END
	-- END
-- END


-- ALTER PROCEDURE sp_HonorarioAnticipo  
-- @valido int,  
-- @valorAnticipo float,  
-- @numrec NVARCHAR(10)  
-- AS  
-- BEGIN  
-- IF(@valido = 1) --OCUPO TODO EL MONTO DEL ANTICIPO  
-- BEGIN  
 -- UPDATE Sic3000..Anticipo SET monto = 0, utilizado = 1, cancelado = 1 WHERE numrec = @numrec  
-- END  
-- ELSE IF(@valido = 0) --SOLO RESTA LO OCUPADO  
-- BEGIN
	-- DECLARE @inicial FLOAT
	-- SET @inicial = (SELECT montoini FROM Sic3000..Anticipo WHERE numrec = @numrec)
	-- IF((SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec) > @inicial)
	-- BEGIN
		-- UPDATE Sic3000..Anticipo SET monto = montoini WHERE numrec = @numrec
		-- UPDATE Sic3000..Anticipo SET monto = ROUND((monto - @valorAnticipo),2) WHERE numrec = @numrec  
	-- END
	-- ELSE
	-- BEGIN
		-- UPDATE Sic3000..Anticipo SET monto = ROUND((monto - @valorAnticipo),2) WHERE numrec = @numrec 
	-- END
-- END  
-- END  

-- USE [His3000]
-- GO

-- /****** Object:  View [dbo].[HONORARIOS_VISTA]    Script Date: 02/09/2021 12:30:06 ******/
-- SET ANSI_NULLS ON
-- GO

-- SET QUOTED_IDENTIFIER ON
-- GO




-- ALTER VIEW [dbo].[HONORARIOS_VISTA]
-- AS
-- SELECT     Tm.TMO_NOMBRE, Hm.TMO_CODIGO, Hm.HOM_CODIGO, Hm.ATE_CODIGO, Hm.FOR_CODIGO, Hm.ID_USUARIO, Hm.MED_CODIGO, 
                      -- Md.MED_APELLIDO_PATERNO + SPACE(1) + Md.MED_NOMBRE1 + SPACE(1) + Md.MED_NOMBRE2 AS MED_NOMBRE_MEDICO, Md.MED_RUC, 
                      -- Hm.HOM_FACTURA_MEDICO, Hm.HOM_FACTURA_FECHA, Pc.PAC_APELLIDO_PATERNO + SPACE(1) + Pc.PAC_APELLIDO_MATERNO + SPACE(1) 
                      -- + Pc.PAC_NOMBRE1 + SPACE(1) + Pc.PAC_NOMBRE2 AS PAC_NOMBRE_PACIENTE, Ae.ATE_NUMERO_CONTROL, Ae.ATE_FACTURA_PACIENTE, 
                      -- Ae.ATE_FACTURA_FECHA, SFP.despag,Fp.FOR_DESCRIPCION, Hm.HOM_LOTE, Hm.HOM_FECHA_INGRESO, Hm.HOM_VALOR_NETO, Hm.RET_CODIGO1, 
                      -- Hm.HOM_RETENCION, Hm.HOM_COMISION_CLINICA, Hm.HOM_APORTE_LLAMADA, Hm.HOM_VALOR_TOTAL, 
                      -- Hm.HOM_VALOR_NETO - Hm.HOM_COMISION_CLINICA AS VALOR_POR_RECUPERAR, Hm.HOM_RECORTE, Hm.HOM_OBSERVACION, 
                      -- Hm.HOM_VALOR_PAGADO, Hm.HOM_VALOR_CANCELADO, Hm.HOM_VALOR_TOTAL AS VALOR_RECUPERADO, 
                      -- Hm.HOM_VALOR_TOTAL - Hm.HOM_VALOR_CANCELADO AS DIFERENCIA, 
					  -- HMD.HON_FUERA, U.APELLIDOS + ' ' + U.NOMBRES AS RESPONSABLE,
					  -- TR.TIR_NOMBRE AS REFERIDO,
					  -- 'NRO ASIENTO' = ISNULL((SELECT numdoc FROM Cg3000..Cgcabmae CG WHERE CG.HOM_CODIGO = Hm.HOM_CODIGO), 0)
-- FROM         dbo.HONORARIOS_MEDICOS AS Hm 
-- INNER JOIN dbo.MEDICOS AS Md ON Hm.MED_CODIGO = Md.MED_CODIGO 
-- INNER JOIN HONORARIOS_MEDICOS_DATOSADICIONALES HMD ON Hm.HOM_CODIGO = HMD.HOM_CODIGO
-- INNER JOIN USUARIOS U ON HM.ID_USUARIO = U.ID_USUARIO
-- INNER JOIN dbo.TIPO_MOVIMIENTO AS Tm ON Hm.TMO_CODIGO = Tm.TMO_CODIGO LEFT OUTER JOIN
                      -- dbo.FORMA_PAGO AS Fp ON Hm.FOR_CODIGO = Fp.FOR_CODIGO 
					  -- INNER JOIN Sic3000..Forma_Pago SFP ON Fp.forpag = SFP.forpag
					  -- LEFT OUTER JOIN
                      -- dbo.ATENCIONES AS Ae ON Hm.ATE_CODIGO = Ae.ATE_CODIGO LEFT OUTER JOIN
                      -- dbo.PACIENTES AS Pc ON Ae.PAC_CODIGO = Pc.PAC_CODIGO
-- INNER JOIN TIPO_REFERIDO TR ON Ae.TIR_CODIGO = TR.TIR_CODIGO
-- GO


-- ALTER procedure [dbo].[sp_ImpresionDevolucion]( @Pedido as int)                
-- as                
-- begin                
 -- select                 
 -- PACIENTES.PAC_APELLIDO_PATERNO + ' ' + PACIENTES.PAC_APELLIDO_MATERNO + ' ' +PACIENTES.PAC_NOMBRE1 + ' ' +PACIENTES.PAC_NOMBRE2 AS PACIENTE,                 
 -- PACIENTES.PAC_IDENTIFICACION AS IDENTIFICACION,                
 -- PEDIDO_DEVOLUCION.DevFecha as PED_FECHA,                
 -- PEDIDO_DEVOLUCION.DevCodigo AS [NUMERO PEDIDO],                
 -- PEDIDO_DEVOLUCION.ID_USUARIO AS [CODIGO USUARIO],                 
 -- USUARIOS.USR AS USUARIO ,                
 -- MEDICOS.MED_APELLIDO_PATERNO + ' ' + MEDICOS.MED_APELLIDO_MATERNO + ' ' + MEDICOS.MED_NOMBRE1 + ' ' + MEDICOS.MED_NOMBRE2 as MEDICOS,                
 -- PEDIDO_DEVOLUCION_DETALLE.PRO_CODIGO, dbo.datosproducto(PEDIDO_DEVOLUCION_DETALLE.PRO_CODIGO) as Producto,                
 -- DevDetCantidad as PDD_CANTIDAD, DevDetValor as PDD_VALOR, DevDetValor as PDD_IVA, DevDetIvaTotal PDD_TOTAL,            
 -- PAC_HISTORIA_CLINICA as HISTORIA,            
 -- ATENCIONES.ATE_NUMERO_ATENCION AS ATENCION,            
 -- (SELECT HABITACIONES.hab_Numero FROM HABITACIONES WHERE HABITACIONES.hab_Codigo=ATENCIONES.HAB_CODIGO)  AS HABITACION,            
 -- (SELECT PEDIDOS_ESTACIONES.PEE_NOMBRE FROM PEDIDOS_ESTACIONES WHERE PEDIDOS_ESTACIONES.PEE_CODIGO=PEDIDOS.PEE_CODIGO) AS ESTACION,     
 -- PEDIDO_DEVOLUCION.DevObservacion AS OBSERVACION    
 -- from PEDIDO_DEVOLUCION,PEDIDO_DEVOLUCION_DETALLE,USUARIOS,MEDICOS,ATENCIONES,PACIENTES,PEDIDOS           
 -- where PEDIDO_DEVOLUCION.ID_USUARIO=USUARIOS.ID_USUARIO                
 -- AND PEDIDO_DEVOLUCION.Ped_Codigo=PEDIDOS.PED_CODIGO               
 -- AND PEDIDO_DEVOLUCION.DevCodigo=PEDIDO_DEVOLUCION_DETALLE.DevCodigo            
 -- and MEDICOS.MED_CODIGO=PEDIDOS.MED_CODIGO                
 -- and PEDIDO_DEVOLUCION.DevCodigo=@Pedido                
 -- and pacientes.PAC_CODIGO=ATENCIONES.PAC_CODIGO                
 -- and ATENCIONES.ATE_CODIGO=pedidos.ATE_CODIGO          
                
 -- order by PRO_CODIGO                
-- end 




-- create PROCEDURE sp_QuirofanoExisteVariosProce
-- @ate_codigo bigint,
-- @pac_codigo int,
-- @procedimiento nvarchar(150)
-- AS
-- SELECT QPP.PCI_CODIGO 
-- FROM QUIROFANO_PROCE_PRODU QPP
-- INNER JOIN PROCEDIMIENTOS_CIRUGIA PC ON QPP.PCI_CODIGO = PC.PCI_CODIGO
-- WHERE ATE_CODIGO = @ate_codigo AND PAC_CODIGO = @pac_codigo AND PC.PCI_DESCRIPCION LIKE '%'+ @procedimiento +'%'
-- GROUP BY QPP.PCI_CODIGO
-- ORDER BY 1 ASC
-- GO


-- CREATE PROCEDURE sp_QuiroCrearVariosProcedimientos
-- @procedimiento nvarchar(150)
-- AS
-- IF NOT EXISTS(SELECT * FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @procedimiento)
-- BEGIN
	-- INSERT INTO PROCEDIMIENTOS_CIRUGIA VALUES(@procedimiento, 0) --AQUI GENERA UN NUEVO PROCEDIMIENTO N VECES.
	-- SELECT * FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @procedimiento --AQUI ENVIO EL PCI_CODIGO PARA AVANZAR
-- END
-- ELSE
-- BEGIN
	-- SELECT * FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @procedimiento --AQUI ENVIO EL PCI_CODIGO PARA AVANZAR
-- END
-- GO
