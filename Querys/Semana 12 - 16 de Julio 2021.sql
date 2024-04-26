-- CREATE PROCEDURE sp_ReponerAnticipoSic
-- @numrec nvarchar(10),
-- @monto float

-- AS
-- IF((SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec) = 0)
-- BEGIN
	-- UPDATE Sic3000..Anticipo SET monto = @monto, utilizado = 0, cancelado = 0 where numrec = @numrec
-- END
-- ELSE
-- BEGIN
	-- UPDATE Sic3000..Anticipo SET monto = monto + @monto WHERE numrec = @numrec
-- END
-- GO





-- alter PROCEDURE sp_CrearMedicos  
-- @med_codigo int,  
-- @esp_codigo int,  
-- @med_nombre1 nvarchar(100),  
-- @med_nombre2 nvarchar(100),  
-- @med_apellido1 nvarchar(100),  
-- @med_apellido2 nvarchar(100),  
-- @fechanacimiento datetime,  
-- @med_direccion nvarchar(500),  
-- @med_direccionC nvarchar(160),  
-- @med_ruc nvarchar(16),  
-- @med_email nvarchar(80),  
-- @med_genero char(1),  
-- @telefono_casa nvarchar(16),  
-- @telefono_consu nvarchar(16),  
-- @celular nvarchar(16),  
-- @transferencia bit,  
-- @tim_codigo int,  
-- @tih_codigo int ,
-- @ret_codigo int
-- AS  
  
-- INSERT INTO MEDICOS VALUES(@med_codigo, @ret_codigo, 10, @esp_codigo, 1, 1, @tim_codigo,@tih_codigo, NULL, GETDATE(), GETDATE(), @med_nombre1,   
-- @med_nombre2, @med_apellido1, @med_apellido2, @fechanacimiento, @med_direccion, @med_direccionC, @med_ruc,   
-- @med_email, @med_genero, NULL, 'C', NULL, @telefono_casa, @telefono_consu, @celular, '000000', NULL, NULL, NULL, @transferencia, 0,  
-- 1, null, null) 





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
 -- PEDIDOS.PED_DESCRIPCION AS OBSERVACION  
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





-- CREATE PROCEDURE sp_DatosClienteSic
-- @ruccli nvarchar(15)
-- AS
-- SELECT nomcli AS CLIENTE, dircli AS DIRECCION, email AS EMAIL, telcli
-- FROM Sic3000..Cliente where ruccli = @ruccli
-- GO


-- CREATE PROCEDURE sp_HonorarioActualizar
-- @hom_codigo int,
-- @med_codigo int,
-- @for_codigo int,
-- @tmo_codigo int,
-- @factura nvarchar(20),
-- @fechaFactura datetime,
-- @valorNeto decimal(10,2),
-- @comision decimal(10,2),
-- @aporte decimal(10,2),
-- @retencion decimal(10,2),
-- @Pagado decimal(10,2),
-- @recorte decimal(10,2),
-- @cancelado decimal(10,2),
-- @lote nvarchar(10),
-- @observacion nvarchar(1000),
-- @vale nvarchar(13),
-- @fechaCaduca datetime
-- AS
-- UPDATE HONORARIOS_MEDICOS SET MED_CODIGO = @med_codigo, FOR_CODIGO = @for_codigo, TMO_CODIGO = @tmo_codigo,
-- HOM_FACTURA_MEDICO = @factura, HOM_FACTURA_FECHA = @fechaFactura, HOM_VALOR_NETO = @valorNeto, HOM_COMISION_CLINICA = @comision,
-- HOM_APORTE_LLAMADA = @aporte, HOM_RETENCION = @retencion, HOM_VALOR_PAGADO = @Pagado, HOM_RECORTE = @recorte,
-- HOM_VALOR_CANCELADO = @cancelado, HOM_LOTE = @lote, HOM_OBSERVACION = @observacion, HOM_VALE = @vale WHERE HOM_CODIGO = @hom_codigo

-- UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET FEC_CAD_FACTURA = @fechaCaduca where HOM_CODIGO = @hom_codigo
-- GO


-- CREATE PROCEDURE sp_HMDatosAdicionales
-- @hom_codigo int
-- AS
-- SELECT * FROM HONORARIOS_MEDICOS_DATOSADICIONALES WHERE HOM_CODIGO = @hom_codigo
-- GO





-- CREATE PROCEDURE sp_ReferidoPaciente
-- @ate_codigo bigint
-- AS
-- SELECT TR.TIR_NOMBRE FROM ATENCIONES A
-- INNER JOIN TIPO_REFERIDO TR ON A.TIR_CODIGO = TR.TIR_CODIGO
-- WHERE A.ATE_CODIGO = @ate_codigo
-- GO




-- alter PROCEDURE sp_FormaPagoFactura  
-- @numFactura nvarchar(25)  
-- AS  
-- SELECT FPP.CodigoSRI, SU.APELLIDOS + ' ' + NOMBRES AS Cajero, FP.fecha AS FECHA
-- FROM Sic3000..FacturaPago FP  
-- INNER JOIN Sic3000..Forma_Pago FPP ON FP.forpag = FPP.forpag  
-- INNER JOIN Sic3000..SeguridadUsuario SU ON FP.cajero = SU.codusu  
-- WHERE FP.numdoc = @numFactura 


-- alter PROCEDURE sp_QuirofanoDuplicado  
-- @ate_codigo bigint,  
-- @pci_codigo int,  
-- @codpro nvarchar(15) ,
-- @ped_codigo bigint
-- AS  
-- SELECT PRO_CODIGO FROM CUENTAS_PACIENTES WHERE ATE_CODIGO = @ate_codigo AND PRO_CODIGO = @codpro AND CUE_OBSERVACION = 'PEDIDO GENERADO POR QUIROFANO' AND Codigo_Pedido = @ped_codigo
-- go




-- ALTER TABLE HONORARIOS_MEDICOS_DATOSADICIONALES
-- ADD HON_CUBIERTO DECIMAL(10,2) DEFAULT 0



-- ALTER PROCEDURE sp_HonorarioActualizar  
-- @hom_codigo int,  
-- @med_codigo int,  
-- @for_codigo int,  
-- @tmo_codigo int,  
-- @factura nvarchar(20),  
-- @fechaFactura datetime,  
-- @valorNeto decimal(10,2),  
-- @comision decimal(10,2),  
-- @aporte decimal(10,2),  
-- @retencion decimal(10,2),  
-- @Pagado decimal(10,2),  
-- @recorte decimal(10,2),  
-- @cancelado decimal(10,2),  
-- @lote nvarchar(10),  
-- @observacion nvarchar(1000),  
-- @vale nvarchar(13),  
-- @fechaCaduca datetime,
-- @cubierto decimal(10,2)
-- AS  
-- UPDATE HONORARIOS_MEDICOS SET MED_CODIGO = @med_codigo, FOR_CODIGO = @for_codigo, TMO_CODIGO = @tmo_codigo,  
-- HOM_FACTURA_MEDICO = @factura, HOM_FACTURA_FECHA = @fechaFactura, HOM_VALOR_NETO = @valorNeto, HOM_COMISION_CLINICA = @comision,  
-- HOM_APORTE_LLAMADA = @aporte, HOM_RETENCION = @retencion, HOM_VALOR_PAGADO = @Pagado, HOM_RECORTE = @recorte,  
-- HOM_VALOR_CANCELADO = @cancelado, HOM_LOTE = @lote, HOM_OBSERVACION = @observacion, HOM_VALE = @vale WHERE HOM_CODIGO = @hom_codigo  
  
-- UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET FEC_CAD_FACTURA = @fechaCaduca, HON_CUBIERTO = @cubierto where HOM_CODIGO = @hom_codigo  




-- alter PROCEDURE sp_EsOtraFormaPago  
-- @ate_codigo bigint,
-- @med_codigo bigint
-- AS  
  
-- SELECT FOR_CODIGO FROM HONORARIOS_MEDICOS WHERE ATE_CODIGO = @ate_codigo AND MED_CODIGO = @med_codigo 
-- go




-- alter PROCEDURE sp_HonorarioActualizar    
-- @hom_codigo int,    
-- @med_codigo int,    
-- @for_codigo int,    
-- @tmo_codigo int,    
-- @factura nvarchar(20),    
-- @fechaFactura datetime,    
-- @valorNeto decimal(10,2),    
-- @comision decimal(10,2),    
-- @aporte decimal(10,2),    
-- @retencion decimal(10,2),    
-- @Pagado decimal(10,2),    
-- @recorte decimal(10,2),    
-- @cancelado decimal(10,2),    
-- @lote nvarchar(10),    
-- @observacion nvarchar(1000),    
-- @vale nvarchar(13),    
-- @fechaCaduca datetime,  
-- @cubierto decimal(10,2),
-- @autorizacion nvarchar(50)
-- AS    
-- UPDATE HONORARIOS_MEDICOS SET MED_CODIGO = @med_codigo, FOR_CODIGO = @for_codigo, TMO_CODIGO = @tmo_codigo,    
-- HOM_FACTURA_MEDICO = @factura, HOM_FACTURA_FECHA = @fechaFactura, HOM_VALOR_NETO = @valorNeto, HOM_COMISION_CLINICA = @comision,    
-- HOM_APORTE_LLAMADA = @aporte, HOM_RETENCION = @retencion, HOM_VALOR_PAGADO = @Pagado, HOM_RECORTE = @recorte,    
-- HOM_VALOR_CANCELADO = @cancelado, HOM_LOTE = @lote, HOM_OBSERVACION = @observacion, HOM_VALE = @vale WHERE HOM_CODIGO = @hom_codigo    
    
-- UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET FEC_CAD_FACTURA = @fechaCaduca, HON_CUBIERTO = @cubierto, AUTORIZACION_SRI = @autorizacion where HOM_CODIGO = @hom_codigo  





-- ALTER PROCEDURE sp_HonorariosAsientoFiltro    
-- @fechaInicio datetime,    
-- @fechaFinal datetime,    
-- @porFuera int,    
-- @porSeguro int    
-- AS     
-- SELECT 'false' as Seleccion, dbo.PACIENTES.PAC_HISTORIA_CLINICA as HC, dbo.ATENCIONES.ATE_CODIGO,     
-- concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO, ' ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2) as paciente,  dbo.ATENCIONES.ATE_FACTURA_PACIENTE,   
-- dbo.MEDICOS.MED_CODIGO,    
-- CONCAT(dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_APELLIDO_MATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1, ' ',dbo.MEDICOS.MED_NOMBRE2) AS MEDICO,    
-- dbo.HONORARIOS_MEDICOS.HOM_CODIGO, dbo.HONORARIOS_MEDICOS.HOM_FACTURA_FECHA AS FECHA_FACTURA_MED, dbo.HONORARIOS_MEDICOS.HOM_FACTURA_MEDICO as FACTURA,dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.AUTORIZACION_SRI AS AUTORIZACION,dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.FEC_CAD_FACTURA AS CADUCIDAD,     
-- dbo.HONORARIOS_MEDICOS.HOM_VALOR_NETO as VALOR,    
-- dbo.HONORARIOS_MEDICOS.HOM_COMISION_CLINICA AS COMISION,    
-- dbo.HONORARIOS_MEDICOS.HOM_APORTE_LLAMADA AS APORTE,    
-- dbo.HONORARIOS_MEDICOS.HOM_RETENCION AS RETENCION,     
-- dbo.RETENCIONES_FUENTE.RET_REFERENCIA as COD_RET,                      
-- dbo.RETENCIONES_FUENTE.COD_CUE AS CTA_RETENCION , 
-- dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_CUBIERTO AS VALOR_CUBIERTO,
-- dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA AS HON_X_FUERA,    
-- CASE    
-- WHEN dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA = 1 THEN(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR FUERA')    
-- WHEN dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA = 'true' THEN(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR FUERA')    
-- ELSE(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR DENTRO')    
-- END AS CTA_HONORARIOS    
-- , dbo.MEDICOS.MED_CUENTA_CONTABLE AS CTA_MEDICO,    
-- (select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'APORTE PERSONAL') AS CTA_APORTE    
-- ,(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'COMISION') AS CTA_COMISION    
-- ,dbo.HONORARIOS_MEDICOS.HOM_POR_PAGAR AS A_PAGAR,     
-- ISNULL(dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.GENERADO_ASIENTO, 0) AS GENERADO,    
-- (select concat(u.NOMBRES,' ',u.APELLIDOS) as USUARIO from USUARIOS u where ID_USUARIO=dbo.HONORARIOS_MEDICOS.ID_USUARIO) AS USUARIO  
-- , dbo.FORMA_PAGO.forpag,    
-- 'SEGUROS'= CONVERT(BIT, CASE WHEN (SELECT C.codclas FROM Sic3000..Clasificacion C     
-- INNER JOIN Sic3000..Forma_Pago FP ON C.codclas = FP.claspag WHERE FORMA_PAGO.forpag = FP.forpag) = 7 THEN 1 else 0 end),  
-- 'FORMA PAGO' = (SELECT despag FROM Sic3000..Forma_Pago WHERE Sic3000..Forma_Pago.forpag = dbo.FORMA_PAGO.forpag),  
-- dbo.FORMA_PAGO.FOR_DESCRIPCION  AS 'CORRIENTE / DIFERIDO'  
           -- FROM dbo.HONORARIOS_MEDICOS_DATOSADICIONALES RIGHT OUTER JOIN    
                   -- dbo.HONORARIOS_MEDICOS INNER JOIN    
                    -- dbo.ATENCIONES ON dbo.HONORARIOS_MEDICOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN    
                     -- dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO LEFT OUTER JOIN    
                      -- dbo.FORMA_PAGO ON dbo.HONORARIOS_MEDICOS.FOR_CODIGO = dbo.FORMA_PAGO.FOR_CODIGO ON    
                       -- dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HOM_CODIGO = dbo.HONORARIOS_MEDICOS.HOM_CODIGO LEFT OUTER JOIN    
                        -- dbo.RETENCIONES_FUENTE INNER JOIN    
                         -- dbo.MEDICOS ON dbo.RETENCIONES_FUENTE.RET_CODIGO = dbo.MEDICOS.RET_CODIGO ON dbo.HONORARIOS_MEDICOS.MED_CODIGO = dbo.MEDICOS.MED_CODIGO    
-- where dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.GENERADO_ASIENTO<>1     
-- and HONORARIOS_MEDICOS.HOM_FACTURA_FECHA BETWEEN @fechaInicio and @fechaFinal    
-- AND HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA = @porFuera AND CONVERT(BIT, CASE WHEN (SELECT C.codclas FROM Sic3000..Clasificacion C INNER JOIN Sic3000..Forma_Pago FP ON C.codclas = FP.claspag WHERE FORMA_PAGO.forpag = FP.forpag) = 7 THEN 1 else 0 end
  
-- ) = @porSeguro    




-- ---ESTO VA EN EL SIC3000 PARA CUENTAS POR COOBRAR

-- CREATE VIEW HonorariosMedico
-- AS
-- SELECT HM.HOM_CODIGO AS CODIGO, A.ATE_CODIGO, P.PAC_HISTORIA_CLINICA, 
-- P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE 
-- FROM His3000..HONORARIOS_MEDICOS HM
-- INNER JOIN His3000..ATENCIONES A ON HM.ATE_CODIGO = A.ATE_CODIGO
-- INNER JOIN His3000..PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO
-- GO