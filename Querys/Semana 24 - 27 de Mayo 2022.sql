--SOLO ACTUALIZAR EN FALCONI   NO!!!!!!!!!
-- alter procedure [dbo].[sp_ActualizaEstadoCuenta]            
-- (@CodigoAtencion as int,            
-- @Estado as int,            
-- @Factura as varchar(32),    
-- @usuario as int         
-- )            
-- as                  
-- begin     
-- declare @fechaFactura datetime --se declara variable para que no tome siempre GETDATE en atenciones y cuentapaciente  
-- set @fechaFactura = (select top 1 FAC_FECHA from FACTURA where ATE_CODIGO = @CodigoAtencion) 
 -- update ATENCIONES set ESC_CODIGO=@Estado,             
 -- ATE_FACTURA_PACIENTE=@Factura,            
 -- ATE_FACTURA_FECHA=@fechaFactura,    
 -- ATE_ESTADO=0            
 -- where ATE_CODIGO=@CodigoAtencion       
     
 -- UPDATE CUENTAS_PACIENTES SET USUARIO_FACTURA=@Usuario,    
 -- FECHA_FACTURA=@fechaFactura     
 -- WHERE ATE_CODIGO=@CodigoAtencion              
    
 -- declare @habCodigo as int    
 -- declare @hesCodigo as int    
 -- select @habCodigo=hab_Codigo, @hesCodigo=HES_CODIGO from HABITACIONES where hab_Codigo=(select hab_Codigo from ATENCIONES where ATE_CODIGO=@CodigoAtencion)    
     
 -- if(@hesCodigo<>1)    
 -- begin    
 -- update HABITACIONES set HES_CODIGO=5 where hab_Codigo=@habCodigo    
 -- end    
-- end
-----SOLO ACTUALIZAR EN FALCONI YA ESTA ACTUALIZADO

-------------------------------------------------------------------------------------------------------------
--(INICIO) ACTUALIZADO EN PASTEUR Y FALCONI  
CREATE PROCEDURE sp_LaboratorioGrupos
@Codigo int 
AS
IF(@Codigo = 6) --HEMATOLOGIA DENTRO DE LA TABLA HC_CATALOGOS_TIPO
BEGIN
	select PD.coddep, PD.desdep, PS.codsec, P.codpro, P.despro from SIC3000..Producto P
	INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep
	INNER JOIN SIC3000..ProductoSeccion PS ON P.codsec = PS.codsec
	WHERE PD.coddep = 401001
END
ELSE IF(@Codigo = 7) --UROANALISIS DENTRO DE LA TABLA HC_CATALOGOS_TIPO
BEGIN
	select PD.coddep, PD.desdep, PS.codsec, P.codpro, P.despro from SIC3000..Producto P
	INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep
	INNER JOIN SIC3000..ProductoSeccion PS ON P.codsec = PS.codsec
	WHERE PD.coddep = 401002
END
ELSE IF(@Codigo = 8)
BEGIN
	select PD.coddep, PD.desdep, PS.codsec, P.codpro, P.despro from SIC3000..Producto P
	INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep
	INNER JOIN SIC3000..ProductoSeccion PS ON P.codsec = PS.codsec
	WHERE PD.coddep = 401003
END
ELSE IF(@Codigo = 9)
BEGIN
	select PD.coddep, PD.desdep, PS.codsec, P.codpro, P.despro from SIC3000..Producto P
	INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep
	INNER JOIN SIC3000..ProductoSeccion PS ON P.codsec = PS.codsec
	WHERE PD.coddep = 401004
END
ELSE IF(@Codigo = 10)
BEGIN
	select PD.coddep, PD.desdep, PS.codsec, P.codpro, P.despro from SIC3000..Producto P
	INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep
	INNER JOIN SIC3000..ProductoSeccion PS ON P.codsec = PS.codsec
	WHERE PD.coddep = 401005
END
ELSE IF(@Codigo = 11)
BEGIN
	select PD.coddep, PD.desdep, PS.codsec, P.codpro, P.despro from SIC3000..Producto P
	INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep
	INNER JOIN SIC3000..ProductoSeccion PS ON P.codsec = PS.codsec
	WHERE PD.coddep = 401006
END
ELSE IF(@Codigo = 12)
BEGIN
	select PD.coddep, PD.desdep, PS.codsec, P.codpro, P.despro from SIC3000..Producto P
	INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep
	INNER JOIN SIC3000..ProductoSeccion PS ON P.codsec = PS.codsec
	WHERE PD.coddep = 401007
END
-----------------------------------------------------------------------------------------------------
alter procedure sp_existeCgcabmae
@HOM_CODIGO int
as
select * from cg3000..Cgcabmae where HOM_CODIGO = @HOM_CODIGO 
go

alter procedure sp_existeCgContabilidad
@HOM_CODIGO int
as
select * from cg3000..CgContabilidad where HOM_CODIGO = @HOM_CODIGO
go

create procedure sp_cambiarEstadoHOMdatos
@HOM_CODIGO int
as
UPDATE HONORARIOS_MEDICOS_DATOSADICIONALES
SET GENERADO_ASIENTO = 0
where HOM_CODIGO = @HOM_CODIGO
go

-----------------------------------------------------------------------------------
DROP TABLE HC_LABORATORIO_CLINICO_DETALLE
DROP TABLE HC_LABORATORIO_CLINICO
-----------------------------------------------------------------------------------
CREATE TABLE HC_LABORATORIO_CLINICO(
LCL_CODIGO BIGINT IDENTITY(1,1) PRIMARY KEY,
ATE_CODIGO BIGINT,
MED_CODIGO INT,
LCL_FECHA_MUESTRA DATETIME,
LCL_NOMBRE_RECIBE NVARCHAR(100),
LCL_SERVICIO NVARCHAR(50),
LCL_SALA NVARCHAR(30),
LCL_HABITACION NVARCHAR(10),
LCL_PRIORIDAD_R BIT,
LCL_PRIORIDAD_C BIT,
LCL_PRIORIDAD_U BIT,
LCL_FECHA_CREACION DATETIME,
ID_USUARIO INT)

------------------------------------------------------------------------------------

CREATE TABLE HC_LABORATORIO_CLINICO_DETALLE(
LCD_CODIGO BIGINT IDENTITY(1,1) PRIMARY KEY,
LCL_CODIGO BIGINT,
LCD_CODPRO NVARCHAR(50),
LCD_NOMBRE_EXAMEN NVARCHAR(200),
ID_USUARIO INT)

-------------------------------------------------------------------------------

CREATE PROCEDURE sp_LaboratorioProducto
	@codpro NVARCHAR(50)
	AS
	select PD.coddep, PD.desdep, 
	PS.codsec, P.codpro, P.despro 
	from SIC3000..Producto P
	INNER JOIN SIC3000..ProductoDepartamento PD ON P.coddep = PD.coddep
	INNER JOIN SIC3000..ProductoSeccion PS ON P.codsec = PS.codsec
	WHERE P.codpro = @codpro
	go
	
--(FIN) ACTUALIZADO EN PASTEUR Y FALCONI
---------------------------------------------------------------------------------

alter table HC_LABORATORIO_CLINICO
add LCL_MUESTRA NVARCHAR(100)