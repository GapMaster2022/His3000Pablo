ALTER PROCEDURE [dbo].[sp_HonorarioAnticipo]    
@valido int,    
@valorAnticipo float,    
@numrec NVARCHAR(10)    
AS    
BEGIN    
IF(@valido = 1) --OCUPO TODO EL MONTO DEL ANTICIPO    
BEGIN    
 UPDATE Sic3000..Anticipo SET monto = 0, utilizado = 1, cancelado = 1 WHERE numrec = @numrec    
END    
ELSE IF(@valido = 0) --SOLO RESTA LO OCUPADO    
BEGIN  
-- DECLARE @inicial FLOAT  
-- SET @inicial = (SELECT montoini FROM Sic3000..Anticipo WHERE numrec = @numrec)  
-- IF((SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec) > @inicial)  
-- BEGIN  
--  DECLARE @usado float  
--  SET @usado = ROUND((SELECT SUM(convert(float, parcial)) FROM Sic3000..FacturaPago WHERE cheque_caduca = @numrec AND forpag = 15 ),2)  
--  UPDATE Sic3000..Anticipo SET monto = (montoini - @usado) WHERE numrec = @numrec  
--  UPDATE Sic3000..Anticipo SET monto = ROUND((monto - @valorAnticipo),2) WHERE numrec = @numrec    
-- END  
-- ELSE  
-- BEGIN  
--  UPDATE Sic3000..Anticipo SET monto = ROUND((monto - @valorAnticipo),2) WHERE numrec = @numrec   
-- END  
--END  
	UPDATE Sic3000..Anticipo SET monto = monto - @valorAnticipo WHERE numrec = @numrec
	END 
END

-----------------------------------------------------------------------------------------

ALTER PROCEDURE sp_ReponerAnticipoSic    
@numrec nvarchar(10),    
@monto float    
    
AS    
IF((SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec) = 0)    
BEGIN    
 UPDATE Sic3000..Anticipo SET monto = @monto, utilizado = 0, cancelado = 0 where numrec = @numrec    
END    
ELSE    
BEGIN  
	UPDATE Sic3000..Anticipo SET monto = monto + @monto WHERE numrec = @numrec
 --DECLARE @inicial FLOAT  
 --DECLARE @montoActual FLOAT  
 --SET @inicial = (SELECT montoini FROM Sic3000..Anticipo WHERE numrec = @numrec)  
 --SET @montoActual = (SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec)  
 --IF((SELECT monto FROM Sic3000..Anticipo WHERE numrec = @numrec) > @inicial)  
 --BEGIN  
 -- UPDATE Sic3000..Anticipo SET monto = monto WHERE numrec = @numrec   
 --END  
 --ELSE  
 --BEGIN  
 -- IF((SELECT montoini FROM Sic3000..Anticipo WHERE numrec = @numrec) > @montoActual + @monto)  
 -- BEGIN  
 --  UPDATE Sic3000..Anticipo SET monto = monto + @monto WHERE numrec = @numrec   
 -- END  
 -- ELSE  
 -- BEGIN  
 --  UPDATE Sic3000..Anticipo SET monto = monto WHERE numrec = @numrec   
 -- END  
 --END  
END

------------------------------------------------------------------------------

alter PROCEDURE [dbo].[sp_QuirofanoSoloProcedimientos] 
@bodega int
AS    
SELECT QPP.PCI_CODIGO,  C.PCI_DESCRIPCION AS Procedimiento    
FROM QUIROFANO_PROCE_PRODU QPP    
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO    
WHERE QPP.PAC_CODIGO IS NULL  AND C.PCI_ESTADO = 1 and C.pci_bodega = @bodega  
GROUP BY C.PCI_DESCRIPCION, QPP.PCI_CODIGO 

-----------------------------------------------------------------------------
alter PROCEDURE sp_QuirofanoNombreProcedimiento  
@pci_descripcion nvarchar(200),
@bodega int
AS  
IF EXISTS(SELECT * FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @pci_descripcion and pci_bodega = @bodega)  
BEGIN  
 SELECT 'ESTADO' = 1 FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @pci_descripcion  
END  
ELSE  
BEGIN  
 INSERT INTO PROCEDIMIENTOS_CIRUGIA VALUES(@pci_descripcion, 1, @bodega)  
  SELECT TOP 1 'ESTADO' = 0 FROM PROCEDIMIENTOS_CIRUGIA  
END  
------------------------------------------------------------------------------
CREATE VIEW PagosAnulados
AS
SELECT REPLACE(CG_ESTADO, 'REVERSION DE LIQUIDACIÓN ', '') AS LIQUIDACION,
cast(CONVERT(char(10), CG_FECHA ,103) as date) AS FECHA, U.apellidos + ' ' + U.nombres AS USUARIO,
COUNT(REPLACE(CG_ESTADO, 'REVERSION DE LIQUIDACIÓN ', '')) AS CANTIDAD, SUM(CG_DEBE) AS TOTAL
FROM SERIES3000_AUDITORIA..CG_AUDTORIA CA
INNER JOIN Cg3000..Cgusuario U ON CA.CG_USUARIO = U.codusu
WHERE CA.CG_ESTADO LIKE '%REVERS%'
GROUP BY REPLACE(CG_ESTADO, 'REVERSION DE LIQUIDACIÓN ', ''),  cast(CONVERT(char(10), CG_FECHA ,103) as date), U.apellidos + ' ' + U.nombres
GO

------------------------------------------------------------------------------
USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_Certificado_Mostrar]    Script Date: 09/03/2022 15:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_Certificado_Mostrar]    
@ate_codigo int    
AS    
SELECT TOP 1 P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' +P.PAC_NOMBRE2 AS PACIENTE,    
P.PAC_IDENTIFICACION, P.PAC_HISTORIA_CLINICA, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA,     
CM.CER_DIAS_REPOSO, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,    
M.MED_RUC, M.MED_EMAIL, CM.CER_CODIGO, CM.CER_OBSERVACION, (SELECT top 1 EMP_NOMBRE FROM EMPRESA), (SELECT top 1 EMP_DIRECCION FROM EMPRESA),    
(SELECT top 1 EMP_TELEFONO FROM EMPRESA), PD.DAP_DIRECCION_DOMICILIO, PD.DAP_TELEFONO2  
FROM CERTIFICADO_MEDICO CM    
INNER JOIN ATENCIONES A ON CM.ATE_CODIGO = A.ATE_CODIGO    
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO    
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO 
INNER JOIN PACIENTES_DATOS_ADICIONALES PD ON P.PAC_CODIGO = PD.PAC_CODIGO
WHERE CM.ATE_CODIGO = @ate_codigo    
ORDER BY CM.CER_FECHA DESC 

------------------------------------------------------------------------------------

--CREAR ESTO EN LA BASE DE SERIES 
CREATE VIEW VistaAnulados
AS
SELECT REPLACE(CG_ESTADO, 'REVERSION DE LIQUIDACIÓN ', '') AS LIQUIDACION,
cast(CONVERT(char(10), CG_FECHA ,103) as date) AS FECHA, U.apellidos + ' ' + U.nombres AS USUARIO,
COUNT(REPLACE(CG_ESTADO, 'REVERSION DE LIQUIDACIÓN ', '')) AS CANTIDAD, SUM(CG_DEBE) AS TOTAL
FROM SERIES3000_AUDITORIA..CG_AUDTORIA CA
INNER JOIN Cg3000..Cgusuario U ON CA.CG_USUARIO = U.codusu
WHERE CA.CG_ESTADO LIKE '%REVERS%'
GROUP BY REPLACE(CG_ESTADO, 'REVERSION DE LIQUIDACIÓN ', ''),  cast(CONVERT(char(10), CG_FECHA ,103) as date), U.apellidos + ' ' + U.nombres
GO


----------------------------------------------------------------------
ALTER PROCEDURE sp_QuirofanoProductoSic     
@filtro nvarchar(100),  
@bodega int   
AS      
SELECT P.codpro AS CODIGO, P.despro AS PRODUCTO, B.existe AS STOCK, QP.QP_GRUPO AS GRUPO, P.preven      
FROM QUIROFANO_PRODUCTOS QP      
INNER JOIN Sic3000..Producto P ON QP.CODPRO = P.codpro      
INNER JOIN Sic3000..Bodega B ON P.codpro = B.codpro       
WHERE B.codbod = @bodega AND QP_BODEGA = @bodega and P.despro like '%'+ @filtro + '%'    
ORDER BY 2 ASC 

-----------------------------------------------------------------------
CREATE PROCEDURE sp_PedidoInsert
@ped_codigo int,
@pea_codigo int,
@pee_codigo int,
@ped_descripcion nvarchar(1000),
@ped_fecha datetime,
@id_usuario int,
@ate_codigo int,
@tip_pedido int,
@ped_prioridad int,
@ped_transaccion int,
@med_codigo int,
@hab_codigo int
AS
INSERT INTO PEDIDOS VALUES(@ped_codigo, @pea_codigo, @pee_codigo, @ped_descripcion, 1,@ped_fecha,
@id_usuario, @ate_codigo, @tip_pedido, @ped_prioridad, @ped_transaccion, @med_codigo, @hab_codigo);
GO

-----------------------------------------------------------------------
 
alter PROCEDURE sp_ActualizaKardexSic      
@numdoc nvarchar(20),  
@bodega int,
@id_usuario int
AS      
UPDATE Sic3000..Kardex SET codlocal = @bodega WHERE numdoc = @numdoc AND codusu = @id_usuario
----------------------------------------------------------------------

alter table medicos
alter column MED_TELEFONO_CONSULTORIO nvarchar(30)

--------------------------------------------------
UPDATE MEDICOS SET MED_TELEFONO_CONSULTORIO = '(02)2992400' WHERE MED_TELEFONO_CONSULTORIO IS NULL 

--------------------------------------------------
alter PROCEDURE sp_Atencion   
(  
 @ate_codigo as int  
)    
AS  
BEGIN  
  
 SELECT PAC_CODIGO, MED_CODIGO, ATE_FECHA_ALTA FROM ATENCIONES WHERE ATE_CODIGO=@ate_codigo  
  
END
-------------------------------------------------


alter table CERTIFICADO_MEDICO add DIRECCION_PACIENTE VARCHAR(500)


alter table CERTIFICADO_MEDICO add TELEFONO_PACIENTE VARCHAR(500)

-----------------------------------------------------------------

ALTER PROCEDURE sp_Certificado_Mostrar    
@ate_codigo int    
AS    
SELECT TOP 1 P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' +P.PAC_NOMBRE2 AS PACIENTE,    
P.PAC_IDENTIFICACION, P.PAC_HISTORIA_CLINICA, A.ATE_FECHA_INGRESO, A.ATE_FECHA_ALTA,     
CM.CER_DIAS_REPOSO, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,    
M.MED_RUC, M.MED_EMAIL, CM.CER_CODIGO, CM.CER_OBSERVACION, (SELECT top 1 EMP_NOMBRE FROM EMPRESA), (SELECT top 1 EMP_DIRECCION FROM EMPRESA),    
(SELECT top 1 EMP_TELEFONO FROM EMPRESA), PD.DAP_DIRECCION_DOMICILIO, PD.DAP_TELEFONO2  
FROM CERTIFICADO_MEDICO CM    
INNER JOIN ATENCIONES A ON CM.ATE_CODIGO = A.ATE_CODIGO    
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO    
INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO 
INNER JOIN PACIENTES_DATOS_ADICIONALES PD ON P.PAC_CODIGO = PD.PAC_CODIGO
WHERE CM.ATE_CODIGO = @ate_codigo    
ORDER BY CM.CER_FECHA DESC 
------------------------------------------------------

CREATE PROCEDURE [dbo].[sp_Certificado_ActualizaPaciente]    
@ate_codigo int,    
@DIRECCION varchar(500),    
@TELEFONO varchar(500)   
 
AS BEGIN   
UPDATE CERTIFICADO_MEDICO SET DIRECCION_PACIENTE= @DIRECCION, TELEFONO_PACIENTE = @TELEFONO
WHERE ATE_CODIGO=@ate_codigo
END
------------------------------------------------------

alter PROCEDURE [dbo].[sp_CertificadoPaciente]      
@ate_codigo bigint      
AS      
SELECT p.PAC_APELLIDO_PATERNO, p.PAC_APELLIDO_MATERNO, p.PAC_NOMBRE1, p.PAC_NOMBRE2, A.ATE_CODIGO      
, TI.TIP_DESCRIPCION, PAC_IDENTIFICACION, p.PAC_FECHA_NACIMIENTO, p.PAC_NACIONALIDAD, p.PAC_GENERO,      
(select top 1 EMP_NOMBRE from EMPRESA), a.ATE_NUMERO_ATENCION, TI.TIP_CODIGO,
pd.DAP_TELEFONO2, pd.DAP_DIRECCION_DOMICILIO
from PACIENTES p       
inner join ATENCIONES a on p.PAC_CODIGO = a.PAC_CODIGO      
inner join TIPO_INGRESO TI on a.TIP_CODIGO = ti.TIP_CODIGO      
inner join PACIENTES_DATOS_ADICIONALES PD on p.PAC_CODIGO=PD.PAC_CODIGO
where a.ATE_CODIGO = @ate_codigo 

