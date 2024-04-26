
-- CREATE TABLE MEDICOS_ALTA(
-- MA_CODIGO BIGINT IDENTITY(1,1) NOT NULL,
-- MA_MEDICO NVARCHAR(500),
-- ATE_CODIGO BIGINT,
-- MA_OBSERVACION NVARCHAR(500))


-- ALTER PROCEDURE sp_SaveMedicosAlta
-- @medico nvarchar(500),
-- @ate_codigo bigint,
-- @observacion nvarchar(500)
-- AS
-- IF NOT EXISTS (SELECT * FROM MEDICOS_ALTA WHERE ATE_CODIGO = @ate_codigo AND MA_MEDICO = @medico)
-- BEGIN
	-- INSERT INTO MEDICOS_ALTA VALUES(@medico, @ate_codigo, @observacion)
-- END	
-- GO

-- CREATE PROCEDURE sp_CargarMedicosAlta
-- @ate_codigo bigint
-- AS
-- SELECT MA_MEDICO AS MEDICOS, MA_OBSERVACION AS OBSERVACION FROM MEDICOS_ALTA WHERE ATE_CODIGO = @ate_codigo 
-- GO






-- alter PROCEDURE [dbo].[sp_DtoPacientesAtencionesActivas_2]      
-- (    
    
 -- @PISO AS SMALLINT    
    
-- )        
    
-- AS          
    
-- BEGIN          
    
 -- SET ROWCOUNT  1000          
    
-- IF(@PISO=0)    
-- BEGIN    
  -- SELECT CodigoHabitacion = a.HAB_CODIGO,          
    
   -- NumeroHabitacion = h.hab_Numero,          
    
   -- Cedula = p.PAC_IDENTIFICACION,          
    
   -- NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),          
    
   -- HistoriaClincia = p.PAC_HISTORIA_CLINICA,          
    
   -- NumeroAtencion = a.ATE_NUMERO_ATENCION,          
    
   -- Atencion = a.ATE_CODIGO,          
    
   -- Sexo = p.PAC_GENERO,          
    
   -- Aseguradora = cc.CAT_NOMBRE,          
    
   -- FechaIngreso = a.ATE_FECHA_INGRESO,          
    
   -- MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),          
    
   -- TipoTratamiento = tt.TIA_DESCRIPCION,          
    
   -- DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,    
    
   -- DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),     
      -- CONVERT(DATE,GETDATE()) ))       
                
   -- , FechaNacimiento = PAC_FECHA_NACIMIENTO    
   -- , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )    
   -- , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO     
      -- WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO)    
    
    
    
   -- FROM ATENCIONES a          
    
     -- JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO          
    
     -- LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO          
    
     -- LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO          
    
     -- LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO          
    
     -- LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO          
    
     -- LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO     
    
   -- WHERE     
     
    -- A.ESC_CODIGO=1        
    
  -- ORDER BY h.hab_Numero asc          
    
      
    
-- END    
    
-- IF(@PISO=1)    
-- BEGIN    
    
  -- SELECT CodigoHabitacion = a.HAB_CODIGO,          
    
   -- NumeroHabitacion = h.hab_Numero,          
    
   -- Cedula = p.PAC_IDENTIFICACION,          
    
   -- NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),          
    
   -- HistoriaClincia = p.PAC_HISTORIA_CLINICA,          
    
   -- NumeroAtencion = a.ATE_NUMERO_ATENCION,          
    
   -- Atencion = a.ATE_CODIGO,          
    
   -- Sexo = p.PAC_GENERO,          
    
   -- Aseguradora = cc.CAT_NOMBRE,          
    
   -- FechaIngreso = a.ATE_FECHA_INGRESO,          
    
   -- MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),          
    
   -- TipoTratamiento = tt.TIA_DESCRIPCION,          
    
   -- DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,    
    
   -- DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),     
      -- CONVERT(DATE,GETDATE()) ))       
    
         -- , FechaNacimiento = PAC_FECHA_NACIMIENTO    
   -- , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )    
   -- , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO     
      -- WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO),
	  -- Area = Np.NIV_NOMBRE
    
   -- FROM ATENCIONES a          
    
     -- JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO          
    
     -- LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO          
    
     -- LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO          
    
     -- LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO          
    
     -- LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO          
    
     -- LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO, NIVEL_PISO NP          
    
   -- WHERE     
     
    -- A.ESC_CODIGO=1      
    -- and  H.NIV_CODIGO=NP.NIV_CODIGO    
    -- AND NP.NIV_CODIGO IN (2,3,4,5,7,11,12)    
       
    
  -- ORDER BY h.hab_Numero asc          
    
 -- END    
    
 -- IF(@PISO=2)    
 -- BEGIN    
    
  -- SELECT CodigoHabitacion = a.HAB_CODIGO,          
    
   -- NumeroHabitacion = h.hab_Numero,          
    
   -- Cedula = p.PAC_IDENTIFICACION,          
    
   -- NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),          
    
   -- HistoriaClincia = p.PAC_HISTORIA_CLINICA,          
    
   -- NumeroAtencion = a.ATE_NUMERO_ATENCION,          
    
   -- Atencion = a.ATE_CODIGO,          
    
   -- Sexo = p.PAC_GENERO,          
    
   -- Aseguradora = cc.CAT_NOMBRE,          
    
   -- FechaIngreso = a.ATE_FECHA_INGRESO,          
    
   -- MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),          
    
   -- TipoTratamiento = tt.TIA_DESCRIPCION,          
    
   -- DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,    
    
   -- DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),     
      -- CONVERT(DATE,GETDATE()) ))       
    
         -- , FechaNacimiento = PAC_FECHA_NACIMIENTO    
   -- , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )    
   -- , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO     
      -- WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO) ,
	   -- Area = Np.NIV_NOMBRE
    
    
   -- FROM ATENCIONES a          
    
     -- JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO          
    
     -- LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO          
    
     -- LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO          
    
     -- LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO          
    
     -- LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO          
    
     -- LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO, NIVEL_PISO NP          
    
   -- WHERE     
     
    -- A.ESC_CODIGO=1      
    -- and  H.NIV_CODIGO=NP.NIV_CODIGO    
    -- AND NP.NIV_CODIGO IN (1, 6)  
       
    
  -- ORDER BY h.hab_Numero asc          
    
 -- END    
    
 -- IF(@PISO=3)    
 -- BEGIN    
    
  -- SELECT CodigoHabitacion = a.HAB_CODIGO,          
    
   -- NumeroHabitacion = h.hab_Numero,          
    
   -- Cedula = p.PAC_IDENTIFICACION,          
    
   -- NombrePaciente = (p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2),          
    
   -- HistoriaClincia = p.PAC_HISTORIA_CLINICA,          
    
   -- NumeroAtencion = a.ATE_NUMERO_ATENCION,          
    
   -- Atencion = a.ATE_CODIGO,          
    
   -- Sexo = p.PAC_GENERO,          
    
   -- Aseguradora = cc.CAT_NOMBRE,          
    
   -- FechaIngreso = a.ATE_FECHA_INGRESO,          
    
   -- MedicoTratante = concat(m.MED_APELLIDO_PATERNO , ' ' , m.MED_APELLIDO_MATERNO , ' ' , m.MED_NOMBRE1 , ' ' , m.MED_NOMBRE2),          
    
   -- TipoTratamiento = tt.TIA_DESCRIPCION,          
    
   -- DiagnosticoInicial = a.ATE_DIAGNOSTICO_INICIAL,    
    
   -- DiasHospitalizado=(SELECT DATEDIFF(DAY,CONVERT(DATE,ATE_FECHA_INGRESO),     
      -- CONVERT(DATE,GETDATE()) ))       
    
   -- , FechaNacimiento = PAC_FECHA_NACIMIENTO    
   -- , Referido = ( select TIR_NOMBRE from tipo_referido  where a.TIR_CODIGO= TIR_CODIGO )    
   -- , TipoEmpresa = (SELECT dbo.TIPO_EMPRESA.TE_DESCRIPCION FROM dbo.TIPO_EMPRESA INNER JOIN dbo.ASEGURADORAS_EMPRESAS ON dbo.TIPO_EMPRESA.TE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO     
      -- WHERE dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = cc.ASE_CODIGO)    
    
   -- FROM ATENCIONES a          
    
     -- JOIN PACIENTES p ON P.PAC_CODIGO = a.PAC_CODIGO          
    
     -- LEFT JOIN HABITACIONES h ON h.hab_Codigo = a.HAB_CODIGO          
    
     -- LEFT JOIN MEDICOS m ON m.MED_CODIGO = a.MED_CODIGO          
    
     -- LEFT JOIN TIPO_TRATAMIENTO tt ON tt.TIA_CODIGO = a.TIA_CODIGO          
    
     -- LEFT JOIN ATENCION_DETALLE_CATEGORIAS d ON d.ATE_CODIGO = a.ATE_CODIGO          
    
     -- LEFT JOIN CATEGORIAS_CONVENIOS cc ON cc.CAT_CODIGO = d.CAT_CODIGO, NIVEL_PISO NP          
    
   -- WHERE     
     
    -- A.ESC_CODIGO=1      
    -- and  H.NIV_CODIGO=NP.NIV_CODIGO    
    -- AND NP.NIV_CODIGO IN (30,31,32,33,34,35)    
    
       
    
  -- ORDER BY h.hab_Numero asc          
    
 -- END    
  -- set rowcount 0          
-- END










-- alter PROCEDURE sp_EsOtraFormaPago
-- @ate_codigo bigint
-- AS

-- SELECT FOR_CODIGO FROM HONORARIOS_MEDICOS WHERE ATE_CODIGO = @ate_codigo
-- GO




-- ALTER TABLE HONORARIOS_MEDICOS_DATOSADICIONALES
-- ADD numrec nvarchar(10)





-- CREATE PROCEDURE sp_QuirofanoDuplicado
-- @ate_codigo bigint,
-- @pci_codigo int,
-- @codpro nvarchar(15)
-- AS
-- SELECT PRO_CODIGO FROM CUENTAS_PACIENTES WHERE ATE_CODIGO = @ate_codigo AND PRO_CODIGO = @codpro AND CUE_OBSERVACION = 'PEDIDO GENERADO POR QUIROFANO'
-- GO


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
	-- UPDATE Sic3000..Anticipo SET monto = ROUND((monto - @valorAnticipo),2) WHERE numrec = @numrec
-- END
-- END
-- GO


-- CREATE PROCEDURE sp_CrearMedicos
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
-- @tih_codigo int
-- AS

-- INSERT INTO MEDICOS VALUES(@med_codigo, 1, 10, @esp_codigo, 1, 1, @tim_codigo,@tih_codigo, NULL, GETDATE(), GETDATE(), @med_nombre1, 
-- @med_nombre2, @med_apellido1, @med_apellido2, @fechanacimiento, @med_direccion, @med_direccionC, @med_ruc, 
-- @med_email, @med_genero, NULL, 'C', NULL, @telefono_casa, @telefono_consu, @celular, '000000', NULL, NULL, NULL, @transferencia, 0,
-- 1, null, null)
-- go











-- USE [His3000]
-- GO

-- /****** Object:  View [dbo].[HONORARIOS_VISTA]    Script Date: 11/07/2021 17:42:17 ******/
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
                      -- Hm.HOM_VALOR_TOTAL - Hm.HOM_VALOR_CANCELADO AS DIFERENCIA, HMD.HON_FUERA
-- FROM         dbo.HONORARIOS_MEDICOS AS Hm 
-- INNER JOIN dbo.MEDICOS AS Md ON Hm.MED_CODIGO = Md.MED_CODIGO 
-- INNER JOIN HONORARIOS_MEDICOS_DATOSADICIONALES HMD ON Hm.HOM_CODIGO = HMD.HOM_CODIGO
-- INNER JOIN dbo.TIPO_MOVIMIENTO AS Tm ON Hm.TMO_CODIGO = Tm.TMO_CODIGO LEFT OUTER JOIN
                      -- dbo.FORMA_PAGO AS Fp ON Hm.FOR_CODIGO = Fp.FOR_CODIGO 
					  -- INNER JOIN Sic3000..Forma_Pago SFP ON Fp.forpag = SFP.forpag
					  -- LEFT OUTER JOIN
                      -- dbo.ATENCIONES AS Ae ON Hm.ATE_CODIGO = Ae.ATE_CODIGO LEFT OUTER JOIN
                      -- dbo.PACIENTES AS Pc ON Ae.PAC_CODIGO = Pc.PAC_CODIGO
-- GO



-- alter PROCEDURE sp_HonorariosAsientoFiltro  
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
  





-- ALTER TABLE MEDICOS_ALTA
-- ADD FECHA_ALTA DATETIME DEFAULT GETDATE()




-- alter TABLE MEDICOS_ALTA
-- ADD ID_USUARIO INT


-- alter PROCEDURE sp_SaveMedicosAlta  
-- @medico nvarchar(500),  
-- @ate_codigo bigint,  
-- @observacion nvarchar(500) ,
-- @usuario int
-- AS  
-- IF NOT EXISTS (SELECT * FROM MEDICOS_ALTA WHERE ATE_CODIGO = @ate_codigo AND MA_MEDICO = @medico)  
-- BEGIN  
 -- INSERT INTO MEDICOS_ALTA VALUES(@medico, @ate_codigo, @observacion, GETDATE(), @usuario)  
-- END  



-- ALTER PROCEDURE sp_CargarMedicosAlta  
-- @ate_codigo bigint  
-- AS  
-- SELECT MA_MEDICO AS MEDICOS, MA_OBSERVACION AS OBSERVACION, FECHA_ALTA, ID_USUARIO FROM MEDICOS_ALTA WHERE ATE_CODIGO = @ate_codigo  



-- CREATE PROCEDURE sp_FormaPagoFactura
-- @numFactura nvarchar(25)
-- AS
-- SELECT FPP.CodigoSRI, SU.APELLIDOS + ' ' + NOMBRES AS Cajero FROM Sic3000..FacturaPago FP
-- INNER JOIN Sic3000..Forma_Pago FPP ON FP.forpag = FPP.forpag
-- INNER JOIN Sic3000..SeguridadUsuario SU ON FP.cajero = SU.codusu
-- WHERE FP.numdoc = @numFactura
-- GO

-- ALTER PROCEDURE sp_ListaPedidosEstaciones(@CodigoEstacion as int, @EstadoDetallePedido as int,@FiltroFecha as bit,@FechaInicio as date,@FechaFin as date)  
-- as  
-- begin  
 -- if @EstadoDetallePedido=2  
 -- begin  
  -- if @FiltroFecha=1  
  -- begin  
   -- select   
   -- PEDIDOS_DETALLE.PDD_ESTADO AS [ESTADO PEDIDO],  
   -- pedidos.PED_CODIGO as [NUMERO PEDIDO],  
   -- pedidos.PED_FECHA AS [FECHA],  
   -- PEE_NOMBRE AS [ESTACION] ,  
   -- USUARIOS.USR,  
   -- MEDICOS.MED_APELLIDO_PATERNO + ' ' + MEDICOS.MED_APELLIDO_MATERNO + ' ' + MEDICOS.MED_NOMBRE1 + ' ' + MEDICOS.MED_NOMBRE2 AS [MEDICO], 
      -- PACIENTES.PAC_APELLIDO_PATERNO + ' ' + PACIENTES.PAC_NOMBRE1 AS PACIENTE,
   -- PEDIDOS_DETALLE.PRO_CODIGO AS [CODIGO PRODUCTO],  
   -- DBO.DatosProducto(PEDIDOS_DETALLE.PRO_CODIGO) AS [DESCRIPCION PRODUCTO],  
   -- PEDIDOS_DETALLE.PDD_CANTIDAD as CANTIDAD  
   -- from pedidos,PEDIDOS_DETALLE ,PEDIDOS_ESTACIONES,USUARIOS,MEDICOS, ATENCIONES, PACIENTES  
   -- where pedidos.PED_CODIGO=PEDIDOS_DETALLE.PED_CODIGO  
   -- and pedidos.PEE_CODIGO=PEDIDOS_ESTACIONES.PEE_CODIGO  
   -- AND PEDIDOS.ID_USUARIO=USUARIOS.ID_USUARIO  
   -- AND MEDICOS.MED_CODIGO=PEDIDOS.MED_CODIGO  
      -- AND PEDIDOS.ATE_CODIGO = ATENCIONES.ATE_CODIGO
   -- AND ATENCIONES.PAC_CODIGO = PACIENTES.PAC_CODIGO
   -- and PEDIDOS_ESTACIONES.PEE_CODIGO=@CodigoEstacion  
   -- --and PEDIDOS_DETALLE.PDD_ESTADO=@EstadoDetallePedido  
   -- and CAST(CONVERT(varchar(11),ped_fecha,103) as DATE) between @FechaInicio and @FechaFin  
   -- order by [NUMERO PEDIDO] desc  
  -- end  
  -- else  
  -- begin  
   -- select   
   -- PEDIDOS_DETALLE.PDD_ESTADO AS [ESTADO PEDIDO],  
   -- pedidos.PED_CODIGO as [NUMERO PEDIDO],  
   -- pedidos.PED_FECHA AS [FECHA],  
   -- PEE_NOMBRE AS [ESTACION] ,  
   -- USUARIOS.USR,  
   -- MEDICOS.MED_APELLIDO_PATERNO + ' ' + MEDICOS.MED_APELLIDO_MATERNO + ' ' + MEDICOS.MED_NOMBRE1 + ' ' + MEDICOS.MED_NOMBRE2 AS [MEDICO], 
      -- PACIENTES.PAC_APELLIDO_PATERNO + ' ' + PACIENTES.PAC_NOMBRE1 AS PACIENTE,
   -- PEDIDOS_DETALLE.PRO_CODIGO AS [CODIGO PRODUCTO],  
   -- DBO.DatosProducto(PEDIDOS_DETALLE.PRO_CODIGO) AS [DESCRIPCION PRODUCTO],  
   -- PEDIDOS_DETALLE.PDD_CANTIDAD as CANTIDAD  
   -- from pedidos,PEDIDOS_DETALLE ,PEDIDOS_ESTACIONES,USUARIOS,MEDICOS, ATENCIONES, PACIENTES  
   -- where pedidos.PED_CODIGO=PEDIDOS_DETALLE.PED_CODIGO  
   -- and pedidos.PEE_CODIGO=PEDIDOS_ESTACIONES.PEE_CODIGO  
   -- AND PEDIDOS.ID_USUARIO=USUARIOS.ID_USUARIO  
   -- AND MEDICOS.MED_CODIGO=PEDIDOS.MED_CODIGO  
      -- AND PEDIDOS.ATE_CODIGO = ATENCIONES.ATE_CODIGO
   -- AND ATENCIONES.PAC_CODIGO = PACIENTES.PAC_CODIGO
   -- and PEDIDOS_ESTACIONES.PEE_CODIGO=@CodigoEstacion  
   -- --and PEDIDOS_DETALLE.PDD_ESTADO=@EstadoDetallePedido     
   -- order by [NUMERO PEDIDO] desc  
    
  -- end  
 -- end  
   
 -- if @EstadoDetallePedido<>2  
 -- begin    
  -- if @FiltroFecha=1  
  -- begin  
     
   -- select   
   -- PEDIDOS_DETALLE.PDD_ESTADO AS [ESTADO PEDIDO],  
   -- pedidos.PED_CODIGO as [NUMERO PEDIDO],  
   -- pedidos.PED_FECHA AS [FECHA],  
   -- PEE_DESCRIPCION AS [ESTACION] ,  
   -- USUARIOS.USR,  
   -- MEDICOS.MED_APELLIDO_PATERNO + ' ' + MEDICOS.MED_APELLIDO_MATERNO + ' ' + MEDICOS.MED_NOMBRE1 + ' ' + MEDICOS.MED_NOMBRE2 AS [MEDICO],
      -- PACIENTES.PAC_APELLIDO_PATERNO + ' ' + PACIENTES.PAC_NOMBRE1 AS PACIENTE,
   -- PEDIDOS_DETALLE.PRO_CODIGO AS [CODIGO PRODUCTO],  
   -- DBO.DatosProducto(PEDIDOS_DETALLE.PRO_CODIGO) AS [DESCRIPCION PRODUCTO],  
   -- PEDIDOS_DETALLE.PDD_CANTIDAD as CANTIDAD  
   -- from pedidos,PEDIDOS_DETALLE ,PEDIDOS_ESTACIONES,USUARIOS,MEDICOS, ATENCIONES, PACIENTES
   -- where pedidos.PED_CODIGO=PEDIDOS_DETALLE.PED_CODIGO  
   -- and pedidos.PEE_CODIGO=PEDIDOS_ESTACIONES.PEE_CODIGO  
   -- AND PEDIDOS.ID_USUARIO=USUARIOS.ID_USUARIO  
   -- AND MEDICOS.MED_CODIGO=PEDIDOS.MED_CODIGO  
   -- and PEDIDOS_ESTACIONES.PEE_CODIGO=@CodigoEstacion  
   -- and PEDIDOS_DETALLE.PDD_ESTADO=@EstadoDetallePedido 
      -- AND PEDIDOS.ATE_CODIGO = ATENCIONES.ATE_CODIGO
   -- AND ATENCIONES.PAC_CODIGO = PACIENTES.PAC_CODIGO
   -- and CAST(CONVERT(varchar(11),ped_fecha,103) as DATE) between @FechaInicio and @FechaFin  
   -- order by [NUMERO PEDIDO] desc  
      
  -- end  
  -- else  
  -- begin  
   -- select   
   -- PEDIDOS_DETALLE.PDD_ESTADO AS [ESTADO PEDIDO],  
   -- pedidos.PED_CODIGO as [NUMERO PEDIDO],  
   -- pedidos.PED_FECHA AS [FECHA],  
   -- PEE_DESCRIPCION AS [ESTACION] ,  
   -- USUARIOS.USR,  
   -- MEDICOS.MED_APELLIDO_PATERNO + ' ' + MEDICOS.MED_APELLIDO_MATERNO + ' ' + MEDICOS.MED_NOMBRE1 + ' ' + MEDICOS.MED_NOMBRE2 AS [MEDICO],
   -- PACIENTES.PAC_APELLIDO_PATERNO + ' ' + PACIENTES.PAC_NOMBRE1 AS PACIENTE,
   -- PEDIDOS_DETALLE.PRO_CODIGO AS [CODIGO PRODUCTO],  
   -- DBO.DatosProducto(PEDIDOS_DETALLE.PRO_CODIGO) AS [DESCRIPCION PRODUCTO],  
   -- PEDIDOS_DETALLE.PDD_CANTIDAD as CANTIDAD  
   -- from pedidos,PEDIDOS_DETALLE ,PEDIDOS_ESTACIONES,USUARIOS,MEDICOS, ATENCIONES, PACIENTES
   -- where pedidos.PED_CODIGO=PEDIDOS_DETALLE.PED_CODIGO  
   -- and pedidos.PEE_CODIGO=PEDIDOS_ESTACIONES.PEE_CODIGO  
   -- AND PEDIDOS.ID_USUARIO=USUARIOS.ID_USUARIO  
   -- AND MEDICOS.MED_CODIGO=PEDIDOS.MED_CODIGO  
   -- and PEDIDOS_ESTACIONES.PEE_CODIGO=@CodigoEstacion  
   -- and PEDIDOS_DETALLE.PDD_ESTADO=@EstadoDetallePedido 
   -- AND PEDIDOS.ATE_CODIGO = ATENCIONES.ATE_CODIGO
   -- AND ATENCIONES.PAC_CODIGO = PACIENTES.PAC_CODIGO
   -- order by [NUMERO PEDIDO] desc  
    
  -- end  
 -- end  
   
-- end





