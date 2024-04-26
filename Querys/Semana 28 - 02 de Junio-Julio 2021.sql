alter PROCEDURE sp_RecuperaParametros  
AS  
BEGIN  
  
 SELECT * FROM sic3000..ParametrosFactura WHERE codpar IN ('97','98','99','166') order by cast(codpar as int) asc  
  
END  


alter PROCEDURE sp_RecuperaEmpresa  
AS  
BEGIN  
  
 SELECT *, replace(substring(telefono1,6,10),'T','') as telefono FROM Sic3000..Empresa  
  
END  



ALTER TABLE CERTIFICADO_MEDICO
ALTER COLUMN CER_OBSERVACION nvarchar(MAX)



USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_Certificado_InsertarPaciente]    Script Date: 29/06/2021 09:29:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_Certificado_InsertarPaciente]
@ate_codigo int,
@med_codigo int,
@observacion varchar(4000),
@reposo int,
@actividad nvarchar(1000),
@contingencia nvarchar(500),
@tratamiento nvarchar(1000),
@procedimiento nvarchar(1000),
@ingreso int
AS
INSERT INTO CERTIFICADO_MEDICO(ATE_CODIGO, MED_CODIGO, CER_OBSERVACION, CER_FECHA, 
CER_DIAS_REPOSO, CER_ACTIVIDAD_LABORAL, CER_CONTINGENCIA, CER_TRATAMIENTO, CER_PROCEDIMIENTO,
CER_TIPO_INGRESO)
VALUES (@ate_codigo, @med_codigo, @observacion, GETDATE(), @reposo, 
@actividad, @contingencia, @tratamiento, @procedimiento, @ingreso)







ALTER PROCEDURE [dbo].[sp_ListaPedidoPacienteTodosRubros]  
(  
 @codigoAtencion AS INT  
  
)  
AS  
BEGIN   
  
 SELECT c.Codigo_Pedido PED_CODIGO, c.RUB_CODIGO PEA_CODIGO, c.PRO_CODIGO PEE_CODIGO, c.CUE_DETALLE PED_DESCRIPCION, c.CUE_FECHA PED_FECHA,  
 c.CUE_CANTIDAD, u.ID_USUARIO, u.APELLIDOS + ' ' + u.NOMBRES USUARIO, p.PAC_HISTORIA_CLINICA HISTORIA_CLINICA, a.ATE_CODIGO,  
 p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2 PACIENTE, c.CUE_CODIGO PDD_CODIGO,  
 c.PRO_CODIGO, c.CUE_DETALLE PRO_DESCRIPCION, CONCAT(m.MED_APELLIDO_PATERNO, +' '+ m.MED_NOMBRE1) MEDICO
 , ISNULL((SELECT PDD.DevDetCantidad
 FROM PEDIDO_DEVOLUCION PD
 INNER JOIN PEDIDO_DEVOLUCION_DETALLE PDD ON PD.DevCodigo = PDD.DevCodigo
 WHERE C.Codigo_Pedido = PD.Ped_Codigo AND C.PRO_CODIGO = PDD.PRO_CODIGO), 0) AS 'CANT. DEV'
 FROM ATENCIONES a, PACIENTES p, CUENTAS_PACIENTES c, USUARIOS u, MEDICOS m 
 WHERE a.PAC_CODIGO = p.PAC_CODIGO AND a.ATE_CODIGO = c.ATE_CODIGO AND c.ID_USUARIO = u.ID_USUARIO AND  
 c.MED_CODIGO = m.MED_CODIGO 
 AND c.ATE_CODIGO = @codigoAtencion
 order by c.PED_CODIGO, c.CUE_FECHA desc  
  
END



alter PROCEDURE [dbo].[sp_ListaPedidoPaciente]  
(  
  
 @codigoArea AS INT,  
 @codigoAtencion AS INT  
  
)  
AS  
BEGIN   
  
 SELECT c.Codigo_Pedido PED_CODIGO, c.RUB_CODIGO PEA_CODIGO, c.PRO_CODIGO PEE_CODIGO, c.CUE_DETALLE PED_DESCRIPCION, c.CUE_FECHA PED_FECHA,  
 u.ID_USUARIO, u.APELLIDOS + ' ' + u.NOMBRES USUARIO, p.PAC_HISTORIA_CLINICA HISTORIA_CLINICA, a.ATE_CODIGO,  
 p.PAC_APELLIDO_PATERNO + ' ' + p.PAC_APELLIDO_MATERNO + ' ' + p.PAC_NOMBRE1 + ' ' + p.PAC_NOMBRE2 PACIENTE, c.CUE_CODIGO PDD_CODIGO,  
 c.PRO_CODIGO, c.CUE_DETALLE PRO_DESCRIPCION, c.CUE_CANTIDAD, CONCAT(m.MED_APELLIDO_PATERNO, +' '+ m.MED_NOMBRE1) MEDICO,
 ISNULL((SELECT PDD.DevDetCantidad
 FROM PEDIDO_DEVOLUCION PD
 INNER JOIN PEDIDO_DEVOLUCION_DETALLE PDD ON PD.DevCodigo = PDD.DevCodigo
 WHERE C.Codigo_Pedido = PD.Ped_Codigo AND C.PRO_CODIGO = PDD.PRO_CODIGO), 0) AS 'CANT. DEV'
 FROM ATENCIONES a, PACIENTES p, CUENTAS_PACIENTES c, USUARIOS u, MEDICOS m  
 WHERE a.PAC_CODIGO = p.PAC_CODIGO AND a.ATE_CODIGO = c.ATE_CODIGO AND c.ID_USUARIO = u.ID_USUARIO AND  
 c.MED_CODIGO = m.MED_CODIGO AND c.RUB_CODIGO = @codigoArea AND c.ATE_CODIGO = @codigoAtencion  
 order by c.PED_CODIGO, c.CUE_FECHA desc  
  
END




--SOLO ALUANZA
INSERT INTO PARAMETROS VALUES(17, 1, 'EMERGENCIA HABITACIONES', 'RESPIRATORIO ES UN PISO EMERGENTE POR EL COVID', 0)
INSERT INTO PARAMETROS_DETALLE VALUES (28, 17, 'EMERGENCIA HABITACIONES', NULL, 'RESPIRATORIO ES UN PISO EMERGENTE POR EL COVID', 0)
--FIN




UPDATE CIE10 SET CIE_DESCRIPCION = UPPER(C.DESCRIPCION) FROM CIE11 C
WHERE CIE10.CIE_CODIGO = C.CODIGO AND CIE_IDPADRE = ''























ALTER PROCEDURE sp_ImagenDx
@id int
AS
SELECT dbo.CIE10.CIE_DESCRIPCION, dbo.CIE10.CIE_CODIGO
FROM dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME INNER JOIN
dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS INNER JOIN
dbo.CIE10 ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS.CIE_CODIGO = dbo.CIE10.CIE_CODIGO ON
dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME.id = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS.id_imagenologia_agendamiento_informe
WHERE dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS_DIAGNOSTICOS.id_imagenologia_agendamiento_informe = @id
go


alter PROCEDURE [dbo].[sp_QuirofanoPacientes]  
AS  
SELECT A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_HISTORIA_CLINICA AS HC,  
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,  
P.PAC_IDENTIFICACION AS Identificacion, H.hab_Numero AS Habitacion, A.ATE_FECHA_INGRESO AS 'F. Ingreso',  
P.PAC_CODIGO, A.ATE_CODIGO,  
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico,  
C.CAT_NOMBRE AS Aseguradora, T.TIP_DESCRIPCION AS TIPO, P.PAC_GENERO, TA.TIA_DESCRIPCION, P.PAC_FECHA_NACIMIENTO,  
ISNULL((SELECT COUNT(DISTINCT QPP.PCI_CODIGO) FROM QUIROFANO_PROCE_PRODU QPP   
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO AND QPP.PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO), NULL) AS 'PROCE_AGREGADOS',  
ISNULL((SELECT COUNT(QPP_CIERRE) FROM QUIROFANO_PROCE_PRODU QPP  
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO  
WHERE QPP_CIERRE = 1 AND PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO),NULL) AS 'PROCE_CERRADOS',  
ISNULL((SELECT COUNT(QPP.PCI_CODIGO) FROM QUIROFANO_PROCE_PRODU QPP  
INNER JOIN PROCEDIMIENTOS_CIRUGIA C ON QPP.PCI_CODIGO = C.PCI_CODIGO  
WHERE PAC_CODIGO = P.PAC_CODIGO AND ATE_CODIGO = A.ATE_CODIGO),NULL) AS 'CANT_PROCE'  
FROM PACIENTES P  
INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO  
INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo  
INNER JOIN TIPO_INGRESO T ON A.TIP_CODIGO = T.TIP_CODIGO  
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO  
INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO  
INNER JOIN CATEGORIAS_CONVENIOS C ON ADC.CAT_CODIGO = C.CAT_CODIGO  
INNER JOIN TIPO_TRATAMIENTO TA ON A.TIA_CODIGO = TA.TIA_CODIGO  
WHERE A.ATE_ESTADO = 1 AND T.TIP_CODIGO in(1,2, 3 )
ORDER BY P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2  






  




 alter PROCEDURE [dbo].[sp_QuirofanoBodega]  
@codpro nvarchar(20),  
@existe float , 
@bodega float
AS  
UPDATE Sic3000.dbo.Bodega SET existe = existe - @existe WHERE codpro = @codpro AND codbod = @bodega
go


select * from PARAMETROS_DETALLE order by 1 desc

insert into PARAMETROS values (18, 1, '12', 'BODEGA DE QUIROFANO', 1)

INSERT INTO PARAMETROS_DETALLE VALUES(29, 18, 'BODEGA QUIROFANO', NULL, '12', 1)

CREATE PROCEDURE sp_ParametroBodegaQuirofano
as
SELECT PAD_VALOR FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 29
GO





--SP_HELPTEXT [ti_PedidosDetalle]  
  
alter trigger [dbo].[ti_PedidosDetalle] on [dbo].[PEDIDOS_DETALLE] for insert                  
as                  
begin                  
declare @FechaT as date                
declare @cantidad as decimal(18,4)                  
declare @producto as varchar(16)                  
declare @CodigoPedido as Bigint                
declare @AreaPedidoHis as int                
declare @LocalSic as int                
declare @Usuario as int                
declare @CostoProducto as Decimal(18,4)         
 declare @CostoProductoUlt as Decimal(10,4)                  
declare @Proveedor as int                
declare @PrecioVenta as Decimal(18,4)           
declare @CodigoAtencion as Bigint               
declare @HistoriaClinica as Bigint          
                 
 set @FechaT=CAST(CONVERT(varchar(11),getdate(),103) as date) -- Transformo la fecha al formato 'dd/mm/yyyy'                
                 
 declare @Fecha as BigInt                
 select @Fecha = dbo.TransformaFercha(@FechaT)-- Transformo la fecha a numero                
                 
 declare @Grupo as int                
 declare @Seccion as int                
 declare @Departamento as int                
 declare @SubGrupo as int                
 declare @Division as int                
                   
 select @cantidad=INSERTED.PDD_CANTIDAD --Capturo la cantidad                
 from inserted                  
                 
 select @producto=INSERTED.PRO_CODIGO  --Capturo el codigo del producto                
 from inserted                  
                 
 select @CodigoPedido=INSERTED.PED_CODIGO --Capturo el codigo del pedido                
 from inserted           
           
                
           
 --select @AreaPedidoHis=PEDIDOS.PEA_CODIGO --Capturo el codigo de donde se pide                
 --from PEDIDOS                 
 --WHERE PEDIDOS.PED_CODIGO=@CodigoPedido                
         
 SET @AreaPedidoHis=1 -- Por defecto es la 1 Farmacia        
                 
 select @Usuario=PEDIDOS.ID_USUARIO --Capturo el usuario que creo el movimiento                
 from PEDIDOS                 
 WHERE PEDIDOS.PED_CODIGO=@CodigoPedido             
           
 select @CodigoAtencion=PEDIDOS.ATE_CODIGO --Capturo el codigo de la atencion                
 from PEDIDOS                 
 WHERE PEDIDOS.PED_CODIGO=@CodigoPedido            
           
 select @HistoriaClinica=PACIENTES.PAC_HISTORIA_CLINICA           
 from   ATENCIONES,PACIENTES          
 where  ATENCIONES.PAC_CODIGO=PACIENTES.PAC_CODIGO          
 and    ATE_CODIGO=@CodigoAtencion          
                 
 --SELECT @LocalSic=codlocal                 
 --FROM SIC3000..LOCALES                
 --where LOCALES.local_his=@AreaPedidoHis                
         
 set @LocalSic= (select codlocal from Sic3000..Locales where Local_His = 1)    
                 
 Select @CostoProducto=cospro,      
 @CostoProductoUlt=precos,                 
 @PrecioVenta=preven,                
 @Grupo =codgru,                
 @Seccion =codsec,                
 @Departamento =coddep,                
 @SubGrupo =codsub,                
 @Division =coddiv                
 from sic3000..PRODUCTO                 
 where PRODUCTO.codpro=@producto                
                 
 select top 1 @Proveedor=codprv                
 from sic3000..Prodprov                 
 where Prodprov.codpro=@producto                
                 
 --update sic3000..Bodega set existe=existe - @cantidad                  
 --where codbod=@LocalSic                  
 --and codpro=@producto                  
                 
 -- select * from Sic3000..KARDEX                
                 
 insert into sic3000..kardex                
 values                
 (                
 @producto,                
 GETDATE(),                
 @CodigoPedido,                
 'PED',                
 @LocalSic,                
 1,                
 0,                
 @cantidad,                
 0,                
 'PEDIDO HIS',                
 @Usuario,                
 --@CostoProducto,      
 @CostoProductoUlt,                
 (@CostoProductoUlt*@cantidad),                
 @CostoProducto,                
 @Fecha,                
 @Proveedor,                
 @PrecioVenta,                
 @Grupo ,                
 @Seccion ,               @Departamento ,                
 @SubGrupo ,                
 @Division ,                
 @Fecha,                
 (@CostoProducto*@cantidad),                
 null,/*, descomentar al agregar los campos HistoriaClinica , AtencionCodigo , Factura*/          
 @HistoriaClinica,          
 @CodigoAtencion,          
 null    ,     
 0    ,      
 null          
 )                
                 
end





