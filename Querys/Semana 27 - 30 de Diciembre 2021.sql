-- ALTER TABLE RECETA_MEDICA
-- ADD MED_TELEFONO NVARCHAR(100)

-- -----------------------------------------

-- ALTER PROCEDURE sp_RecetaCrear      
-- @ate_codigo bigint,      
-- @alergias nvarchar(255),      
-- @med_codigo bigint,      
-- @cita datetime,      
-- @id_usuario int,      
-- @signos nvarchar(500),      
-- @farmacos nvarchar(1000),      
-- @tipo int,      
-- @consulta int,
-- @telefono nvarchar(100)
-- AS      
-- INSERT INTO RECETA_MEDICA(ATE_CODIGO, RM_ALERGIAS, MED_CODIGO, RM_CITA, ID_USUARIO, RM_SIGNO, RM_FARMACOS, TIP_CODIGO, TC_CONSULTA, MED_TELEFONO)      
-- VALUES(@ate_codigo, @alergias, @med_codigo, @cita, @id_usuario, @signos, @farmacos, @tipo, @consulta, @telefono); 

-- --------------------------------------------------------------------------------
-- alter PROCEDURE sp_RecetaEditar      
-- @rm_codigo bigint,      
-- @alergias nvarchar(255),      
-- @med_codigo bigint,      
-- @cita datetime,      
-- @id_usuario int,      
-- @signos nvarchar(500),      
-- @farmacos nvarchar(1000),      
-- @tipo int,      
-- @consulta int,
-- @telefono nvarchar(100)
-- AS      
-- UPDATE RECETA_MEDICA SET RM_ALERGIAS = @alergias, MED_CODIGO = @med_codigo, RM_CITA = @cita, RM_SIGNO = @signos,      
-- RM_FARMACOS = @farmacos, TIP_CODIGO = @tipo, TC_CONSULTA = @consulta, MED_TELEFONO = @telefono WHERE RM_CODIGO = @rm_codigo      
      
-- DELETE FROM RECETA_DIAGNOSTICO WHERE RM_CODIGO = @rm_codigo      
-- DELETE FROM RECETA_MEDICAMENTOS WHERE RM_CODIGO = @rm_codigo 

-- -------------------------------------------------------------------------------------
-- USE [His3000]
-- GO

-- /****** Object:  View [dbo].[DESPACHOS]    Script Date: 28/12/2021 10:41:23 ******/
-- SET ANSI_NULLS ON
-- GO

-- SET QUOTED_IDENTIFIER ON
-- GO



-- ALTER VIEW [dbo].[DESPACHOS]
-- AS
-- SELECT ISNULL((CD.CD_ESTADO), 'false') AS 'despachados',  
             -- P.PED_CODIGO AS 'CODIGO PEDIDO', COUNT(PD.PRO_DESCRIPCION) AS CANTIDAD,  
             -- P.PED_FECHA AS FECHA, CONVERT(varchar, PED_FECHA, 8) AS HORA,  
             -- PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2 AS PACIENTE,
			 -- NP.NIV_CODIGO, NP.NIV_NOMBRE AS PISO, H.hab_Codigo, H.hab_Numero AS 'HAB',
             -- PA.PAC_HISTORIA_CLINICA AS 'HIST. CLINICA', A.ATE_NUMERO_ATENCION AS 'Nº ATENCION',  
             -- U.APELLIDOS + ' ' + U.NOMBRES AS 'PEDIDO POR',  
             -- ISNULL((U1.APELLIDOS + ' ' + U1.NOMBRES), '') AS 'DESPACHADO POR', ISNULL((CD.CD_FECHA), '') AS 'FECHA DESPACHO',  
             -- ISNULL((CONVERT(varchar, CD.CD_FECHA, 8)), '00:00:00') AS 'HORA DESPACHO', ISNULL((CD.CD_OBSERVACION), '') AS OBSERVACION,
			 -- A.ATE_CODIGO, P.MED_CODIGO
             -- FROM PEDIDOS P  
             -- INNER JOIN PEDIDOS_DETALLE PD ON PD.PED_CODIGO = P.PED_CODIGO  
			 -- INNER JOIN Sic3000..Producto SP ON PD.PRO_CODIGO = SP.codpro AND SP.clasprod <> 'S'
             -- INNER JOIN ATENCIONES A ON P.ATE_CODIGO = A.ATE_CODIGO  
             -- INNER JOIN PACIENTES PA ON A.PAC_CODIGO = PA.PAC_CODIGO  
             -- INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo  
             -- LEFT JOIN CONTROL_DESPACHO CD ON P.PED_CODIGO = CD.PED_CODIGO  
             -- INNER JOIN NIVEL_PISO NP ON NP.NIV_CODIGO = H.NIV_CODIGO  
             -- INNER JOIN USUARIOS U ON P.ID_USUARIO = U.ID_USUARIO  
             -- LEFT JOIN USUARIOS U1 ON CD.ID_USUARIO = U1.ID_USUARIO
			 -- GROUP BY P.PED_CODIGO, CD.CD_ESTADO, P.PED_FECHA,  
             -- PA.PAC_APELLIDO_PATERNO + ' ' + PA.PAC_APELLIDO_MATERNO + ' ' + PA.PAC_NOMBRE1 + ' ' + PA.PAC_NOMBRE2,  
             -- PA.PAC_HISTORIA_CLINICA, A.ATE_NUMERO_ATENCION, U.APELLIDOS + ' ' + U.NOMBRES,  
             -- U1.APELLIDOS + ' ' + U1.NOMBRES, CD.CD_FECHA, CD.CD_OBSERVACION, NP.NIV_CODIGO,
			 -- NP.NIV_NOMBRE, H.hab_Codigo, H.hab_Numero,  A.ATE_CODIGO, P.MED_CODIGO
-- GO
-- -----------------------------------------------------------------------------------------------

-- --ESTO VA EN LA BASE DE DATOS SERIES3000

-- ALTER TABLE CG_AUDTORIA
-- ADD PRIMARY KEY (CG_CODIGO);

-- ---FIN BASE DE DATOS SERIES3000
-- -------------------------------------------------------------------------------------------------