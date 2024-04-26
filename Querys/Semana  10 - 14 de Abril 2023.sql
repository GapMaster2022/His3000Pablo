----------------------------------------------------------------------------10/04/2023-------------------------------------------------------------------------
alter table MODULO add ESTADO BIT not null DEFAULT 1
GO
----------------------------------------------------------------------------11/04/2023-------------------------------------------------------------------------
USE Cg3000
GO

CREATE TABLE [dbo].[cgmodulo](
	[codmod] [int] NOT NULL,
	[nommod] [nvarchar](50) NOT NULL,
	[estmod] [bit] NULL
) ON [PRIMARY]
GO

----------------------------------------------------------------------------11/04/2023-------------------------------------------------------------------------
alter table Cgopcion add codmod int not null default 1
GO
----------------------------------------------------------------------------14/04/2023-------------------------------------------------------------------------
USE His3000
GO

alter procedure [dbo].[sp_TodosProcedimientos]      
@desde datetime,          
@hasta datetime          
AS          
select  cp.Codigo_Pedido as 'PEDIDO',p.PAC_HISTORIA_CLINICA as 'HC',p.PAC_APELLIDO_PATERNO+' '+p.PAC_APELLIDO_MATERNO+' '+p.PAC_NOMBRE1+' '+p.PAC_NOMBRE2 as 'PACIENTE',a.ATE_NUMERO_ATENCION as 'ATENCION',  
p.PAC_IDENTIFICACION AS 'IDENTIFICACION',(select hab_Numero from His3000..HABITACIONES where hab_Codigo = a.HAB_CODIGO) as 'HABITACION',a.ATE_FECHA_INGRESO AS 'F.INGRESO',    
c.CAT_NOMBRE AS 'ASEGURADORA',(select TIP_DESCRIPCION from His3000..TIPO_INGRESO where TIP_CODIGO = a.TIP_CODIGO) AS TIPO, P.PAC_GENERO AS 'GENERO',  
(select TIA_DESCRIPCION from His3000..TIPO_TRATAMIENTO where TIA_CODIGO = a.TIA_CODIGO) as 'SERVICIO', rq.intervencion as 'PROCEDIMIENTO'  
,cp.CUE_DETALLE as 'PRODUCTO',cp.CUE_CANTIDAD as 'CANTIDAD',CP.CUE_FECHA AS 'FECHA PEDIDO',U.APELLIDOS+' '+U.NOMBRES AS 'USUARIO PEDIDO'
,(select hab_Numero from HABITACIONES where hab_Codigo = rq.quirofano )AS 'QUIROFANO',    
(select TA_DESCRIPCION from His3000..TIPO_ANESTESIA where TA_CODIGO = anestecia)AS 'TIPO ANESTESIA',    
(SELECT MED_APELLIDO_PATERNO + ' ' +MED_APELLIDO_MATERNO + ' ' + MED_NOMBRE1 + ' ' + MED_NOMBRE2 from MEDICOS where MED_CODIGO = rq.cirujano)AS 'CIRUJANO',
(SELECT (SELECT ESP_NOMBRE FROM ESPECIALIDADES_MEDICAS E WHERE E.ESP_CODIGO = M.ESP_CODIGO) FROM MEDICOS M WHERE M.MED_CODIGO = rq.cirujano)AS 'C.ESPECIALIDAD',    
(SELECT MED_APELLIDO_PATERNO + ' ' +MED_APELLIDO_MATERNO + ' ' + MED_NOMBRE1 + ' ' + MED_NOMBRE2 from MEDICOS where MED_CODIGO = rq.ayudante)AS 'AYUDANTE',      
(SELECT MED_APELLIDO_PATERNO + ' ' +MED_APELLIDO_MATERNO + ' ' + MED_NOMBRE1 + ' ' + MED_NOMBRE2 from MEDICOS where MED_CODIGO = rq.ayudantia)AS 'AYUDANTIA',      
(SELECT MED_APELLIDO_PATERNO + ' ' +MED_APELLIDO_MATERNO + ' ' + MED_NOMBRE1 + ' ' + MED_NOMBRE2 from MEDICOS where MED_CODIGO = rq.anasteciologo)AS 'ANESTESIÓLOGO',      
(SELECT APELLIDOS + ' ' + NOMBRES from USUARIOS where ID_USUARIO = rq.instrumentista)AS 'INSTRUMENTISTA',      
(SELECT MED_APELLIDO_PATERNO + ' ' +MED_APELLIDO_MATERNO + ' ' + MED_NOMBRE1 + ' ' + MED_NOMBRE2 from MEDICOS where MED_CODIGO = rq.patologia)AS 'PATOLOGO',    
rq.hora_inicio,rq.hora_fin,    
(CASE (rq.recuperacion)    
WHEN 1 THEN 'SI'      
ELSE 'NO'      
END) AS 'RECUPERACION'    
from His3000..PACIENTES p    
inner join ATENCIONES a on p.PAC_CODIGO = a.PAC_CODIGO    
INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO                
INNER JOIN CATEGORIAS_CONVENIOS C ON ADC.CAT_CODIGO = C.CAT_CODIGO     
inner join REGISTRO_QUIROFANO rq on a.ATE_CODIGO = rq.ate_codigo    
inner join INTERVENCIONES_REGISTRO_QUIROFANO iq on rq.id_registro_quirofano = iq.id_registro_quirofano  
inner join REPOSICION_PENDIENTE rp on rq.id_registro_quirofano = rp.id_registro_quirofano    
inner join CUENTAS_PACIENTES cp on rp.ped_codigo = cp.Codigo_Pedido
INNER JOIN USUARIOS U ON CP.ID_USUARIO = U.ID_USUARIO
WHERE a.ATE_FECHA_INGRESO BETWEEN @desde and @hasta 

