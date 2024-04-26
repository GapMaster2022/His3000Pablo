-- alter PROCEDURE [dbo].[sp_QuirofanoControlPedidoAdicional]  
-- @ate_codigo int,  
-- @cie_codigo bigint,  
-- @codpro varchar(13),   
-- @cantadicional int  
-- AS  
-- UPDATE QUIROFANO_PROCE_PRODU SET QPP_CANTIDAD = QPP_CANTIDAD + @cantadicional  
-- WHERE ATE_CODIGO = @ate_codigo AND PCI_CODIGO = @cie_codigo AND CODPRO = @codpro  


-- alter PROCEDURE [dbo].[sp_QuirofanoPacienteProcedimiento]    
-- @orden int,    
-- @cie_codigo bigint,    
-- @codpro varchar(13),    
-- @cantidad int,    
-- @paciente int,    
-- @atencion int,    
-- @usada int,    
-- @usuario varchar(100),
-- @cerrado int
-- AS    
-- DECLARE @fecha DATETIME    
-- SET @fecha = GETDATE();  
-- if(@cerrado = 0)
-- begin
 -- INSERT INTO QUIROFANO_PROCE_PRODU VALUES(@orden, @cie_codigo, @codpro, @cantidad, @paciente, @atencion, @fecha, 0, @usuario, 0,    
 -- NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) 
-- END
-- ELSE
-- BEGIN
-- INSERT INTO QUIROFANO_PROCE_PRODU VALUES(@orden, @cie_codigo, @codpro, @cantidad, @paciente, @atencion, @fecha, 0, @usuario, 0,    
 -- NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1) 
 -- END
  
  
  
  -- alter PROCEDURE [dbo].[sp_QuirofanoMostrarProcedimientoPaciente]    
-- @cie_codigo bigint,    
-- @atencion int    
-- AS    
-- IF EXISTS (SELECT * FROM QUIROFANO_PROCE_PRODU WHERE PCI_CODIGO = @cie_codigo AND ATE_CODIGO = @atencion AND QPP_ORDEN IS NOT NULL)    
-- BEGIN    
 -- SELECT QPP.CODPRO AS Codigo, P.despro AS Producto, QP.QP_GRUPO AS Grupo,    
 -- B.existe AS Stock, QPP.QPP_CANTIDAD AS 'Cant. Original',     
 -- QPP.QPP_CANT_ADICIONAL AS 'Cant. Adicional',     
 -- ISNULL((SELECT TOP 1 PDD.DevDetCantidad FROM CUENTAS_PACIENTES CP    
 -- INNER JOIN PEDIDO_DEVOLUCION PD ON CP.Codigo_Pedido = PD.Ped_Codigo    
 -- INNER JOIN PEDIDO_DEVOLUCION_DETALLE PDD ON PD.DevCodigo = PDD.DevCodigo AND PDD.PRO_CODIGO = QPP.CODPRO    
 -- and cp.ATE_CODIGO = @atencion    
 -- ) , 0) AS 'Devoluciones',    
 -- QPP.QPP_FECHA AS Fecha, QPP.QPP_ORDEN, QPP_USUARIO as Usuario, p.preven    
 -- FROM QUIROFANO_PROCE_PRODU QPP    
 -- INNER JOIN QUIROFANO_PRODUCTOS QP ON QPP.CODPRO = QP.CODPRO    
 -- INNER JOIN Sic3000.dbo.Producto P ON QP.CODPRO = P.codpro    
 -- INNER JOIN Sic3000.dbo.Bodega B ON P.codpro = B.codpro    
 -- WHERE QPP.PCI_CODIGO = @cie_codigo AND QPP.ATE_CODIGO = @atencion AND B.codbod = 12  
 -- order by QPP.QPP_ORDEN asc  
-- END    
-- ELSE    
-- BEGIN    
 -- SELECT QPP.CODPRO AS Codigo, P.despro AS Producto, QP.QP_GRUPO AS Grupo,    
 -- B.existe AS Stock, QPP.QPP_CANTIDAD AS 'Cant. Original',     
 -- QPP.QPP_CANT_ADICIONAL AS 'Cant. Adicional', 0 AS 'Devoluciones',    
 -- QPP.QPP_FECHA AS Fecha, QPP.QPP_ORDEN, QPP_USUARIO as Usuario, P.preven    
 -- FROM QUIROFANO_PROCE_PRODU QPP    
 -- INNER JOIN QUIROFANO_PRODUCTOS QP ON QPP.CODPRO = QP.CODPRO    
 -- INNER JOIN Sic3000.dbo.Producto P ON QP.CODPRO = P.codpro    
 -- INNER JOIN Sic3000.dbo.Bodega B ON P.codpro = B.codpro    
 -- WHERE QPP.PCI_CODIGO = @cie_codigo AND QPP.ATE_CODIGO IS NULL AND B.codbod = 12    
 -- order by QPP.QPP_ORDEN asc  
-- END    

ALTER PROCEDURE sp_HonorariosDetalleReporte  
@ate_codigo bigint  
AS  
SELECT P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,  
'DIRECCION' = (SELECT TOP 1 PDA.DAP_DIRECCION_DOMICILIO FROM PACIENTES_DATOS_ADICIONALES PDA WHERE PDA.PAC_CODIGO = P.PAC_CODIGO),   
A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA, P.PAC_IDENTIFICACION AS CEDULA, A.ATE_FACTURA_PACIENTE,  
P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS NUMATENCION, H.hab_Numero AS HAB,  
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,  
HM.HOM_FACTURA_MEDICO AS FACTURAMEDICO, HM.HOM_FECHA_INGRESO AS FECHAH, HM.HOM_VALOR_NETO AS NETO, HM.HOM_COMISION_CLINICA AS COMISION,  
HM.HOM_APORTE_LLAMADA AS APORTE, HM.HOM_RETENCION AS RETENCION, HM.VALOR AS CUBIERTO, HM.HOM_VALOR_TOTAL AS TOTAL,   
SFP.despag AS 'FORMA_PAGO', FP.FOR_DESCRIPCION AS 'DIFIERE DE', TI.TIR_NOMBRE AS REFERIDO, U.APELLIDOS + ' ' + U.NOMBRES AS USUARIO,  
HM.HOM_VALE, HM.HOM_RECORTE, 
ISNULL((SELECT 'CLIENTE: ' +  SC.nomcli + ' RUC: ' + SC.ruccli +  ' DIRECCIÓN: '  + SC.dircli + ' TELEFONO: ' + SC.telcli + ' EMAIL: ' + SC.email
FROM Sic3000..FacturaPago SSFP 
INNER JOIN Sic3000..Cliente SC ON SSFP.codcli = SC.codcli
WHERE A.ATE_FACTURA_PACIENTE = SSFP.numdoc), ' ')   AS CLIENTE 
FROM HONORARIOS_MEDICOS HM  
INNER JOIN ATENCIONES A ON HM.ATE_CODIGO = A.ATE_CODIGO  
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO  
INNER JOIN FORMA_PAGO FP ON HM.FOR_CODIGO = FP.FOR_CODIGO  
INNER JOIN Sic3000..Forma_Pago SFP ON FP.forpag = SFP.forpag  
INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo  
INNER JOIN MEDICOS M ON HM.MED_CODIGO = M.MED_CODIGO  
INNER JOIN TIPO_REFERIDO TI ON A.TIR_CODIGO = TI.TIR_CODIGO  
INNER JOIN USUARIOS U ON HM.ID_USUARIO = U.ID_USUARIO  
WHERE HM.ATE_CODIGO = @ate_codigo  


--CRUCE TABLA A OTRA TABLA
update QUIROFANO_PROCE_PRODU set QPP_ORDEN = (select QPP_ORDEN from plantilla where QUIROFANO_PROCE_PRODU.CODPRO = plantilla.CODPRO) where ATE_CODIGO IS NULL



-- CREATE PROCEDURE sp_HonorariosCerrar
-- @ate_codigo bigint
-- AS
-- UPDATE HONORARIOS_MEDICOS SET HOM_ESTADO = 1 WHERE ATE_CODIGO = @ate_codigo
-- go

-- CREATE PROCEDURE sp_HonorarioValidaCerrado
-- @ate_codigo bigint
-- AS
-- SELECT HOM_ESTADO FROM HONORARIOS_MEDICOS WHERE ATE_CODIGO = @ate_codigo ORDER BY HOM_CODIGO ASC
-- GO