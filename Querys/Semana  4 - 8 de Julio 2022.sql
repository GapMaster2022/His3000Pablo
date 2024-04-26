--2022.07.04 pb
alter table PACIENTES alter column PAC_IDENTIFICACION varchar(30)
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create procedure sp_grabarPerfilSic
@cod float,
@ip float,
@descrip varchar(30),
@bodega varchar(5)
as
INSERT INTO Sic3000..SeguridadUsuarioOpciones(codusu,codopc,staopc,sw)VALUES(@cod,@ip,@descrip,@bodega)
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--pb
ALTER VIEW [dbo].[PacienteLaboratorio] AS 
select distinct a.ATE_CODIGO, p.PAC_HISTORIA_CLINICA, p.PAC_IDENTIFICACION, a.ATE_NUMERO_ATENCION, 
(p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2) as PACIENTE,
pda.DAP_DIRECCION_DOMICILIO, pda.DAP_TELEFONO2, p.PAC_EMAIL, h.hab_Numero, t.TIA_DESCRIPCION, cc.CAT_NOMBRE,
m.MED_RUC, (m.MED_APELLIDO_PATERNO +' '+ m.MED_APELLIDO_MATERNO +' '+ m.MED_NOMBRE1 +' '+ m.MED_NOMBRE2) as MEDICO,
a.ATE_FECHA_INGRESO, p.PAC_FECHA_NACIMIENTO, 
CASE p.PAC_GENERO
WHEN 'M' THEN 'MASCULINO'
ELSE 'FEMENINO' END AS PAC_GENERO, 
CASE p.PAC_TIPO_IDENTIFICACION
when 'C' THEN 'CEDULA DE IDENTIDAD'
when 'P' THEN 'PASAPORTE'
when 'R' THEN 'CARNET DE REFUGIADO'
when 'E' THEN 'CEDULA DE CIUDADANIA'
when 'N' THEN 'IDENTIFICACION RECIEN NACIDO'
ELSE
	'SIN IDENTIFICACION'
END
AS PAC_TIPO_IDENTIFICACION
from PACIENTES p 
INNER JOIN PACIENTES_DATOS_ADICIONALES pda ON p.PAC_CODIGO = pda.PAC_CODIGO
INNER JOIN ATENCIONES a ON p.PAC_CODIGO = a.PAC_CODIGO
INNER JOIN HABITACIONES h ON a.HAB_CODIGO = h.hab_Codigo
INNER JOIN TIPO_TRATAMIENTO t on a.TIA_CODIGO = t.TIA_CODIGO
INNER JOIN ATENCION_DETALLE_CATEGORIAS adc ON a.ATE_CODIGO = adc.ATE_CODIGO
INNER JOIN CATEGORIAS_CONVENIOS cc ON adc.CAT_CODIGO = cc.CAT_CODIGO
INNER JOIN MEDICOS m ON a.MED_CODIGO = m.MED_CODIGO
WHERE a.ESC_CODIGO = 1 and ATE_ESTADO = 1
GO


