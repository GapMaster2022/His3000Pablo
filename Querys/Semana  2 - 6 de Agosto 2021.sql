-- CREATE PROCEDURE sp_QuirofanoNombreProcedimiento
-- @pci_descripcion nvarchar(200)
-- AS
-- IF EXISTS(SELECT * FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @pci_descripcion)
-- BEGIN
	-- SELECT 'ESTADO' = 1 FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @pci_descripcion
-- END
-- ELSE
-- BEGIN
	-- INSERT INTO PROCEDIMIENTOS_CIRUGIA VALUES(@pci_descripcion, 1)
		-- SELECT TOP 1 'ESTADO' = 0 FROM PROCEDIMIENTOS_CIRUGIA
-- END
-- GO


-- USE [His3000]
-- GO

-- /****** Object:  View [dbo].[HONORARIOS_VISTA]    Script Date: 06/08/2021 13:15:53 ******/
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
					  -- TR.TIR_NOMBRE AS REFERIDO
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





-- ALTER TABLE HONORARIOS_MEDICOS_DATOSADICIONALES
-- ADD HON_EXCESO DECIMAL(10,2) DEFAULT 0





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
-- @autorizacion nvarchar(50),
-- @exceso decimal(10,2)
-- AS      
-- UPDATE HONORARIOS_MEDICOS SET MED_CODIGO = @med_codigo, FOR_CODIGO = @for_codigo, TMO_CODIGO = @tmo_codigo,      
-- HOM_FACTURA_MEDICO = @factura, HOM_FACTURA_FECHA = @fechaFactura, HOM_VALOR_NETO = @valorNeto, HOM_COMISION_CLINICA = @comision,      
-- HOM_APORTE_LLAMADA = @aporte, HOM_RETENCION = @retencion, HOM_VALOR_PAGADO = @Pagado, HOM_RECORTE = @recorte,      
-- HOM_VALOR_CANCELADO = @cancelado, HOM_LOTE = @lote, HOM_OBSERVACION = @observacion, HOM_VALE = @vale, VALOR = @cubierto  
-- WHERE HOM_CODIGO = @hom_codigo      
      
-- UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES SET FEC_CAD_FACTURA = @fechaCaduca, HON_CUBIERTO = @cubierto, AUTORIZACION_SRI = @autorizacion, HON_EXCESO = @exceso where HOM_CODIGO = @hom_codigo    
  