create PROCEDURE [dbo].[sp_Actualiza_Urs_Sistemas]    
(    
  @nombre as nvarchar(150),    
  @apellidos as nvarchar(50),    
  @identificacion as nvarchar(20),    
  @nombreusu as nvarchar(20),    
  @codusu as float,     
  @clave as nvarchar(20),     
  @codigo_c as float,    
  @fechaIngreso as datetime,     
  @fechaCaducidad as datetime,     
  @estado as int,    
  @tipoUsuario  as int,    
  @idUsuario int,
  @coddep as float,
  @direccion as nvarchar(50)
)    
AS    
    
    
Set NoCount On          
Set Ansi_Warnings On          
    
    
declare @T_dt_Fecha as date    
DECLARE @T_Db_FechaNumero as numeric    
declare @T_Db_FechaIngNumero as numeric    
declare @T_Dt_FechaSistema as date    
declare @T_Ln_Codigo_Usuario as numeric    
declare @T_Db_cargo as float    
declare @T_Db_codgru as float    
declare @T_Db_coddep as float    
declare @T_Db_CodUsrP AS Float  
    
set @T_Db_cargo=1    
set @T_Db_coddep= @coddep    
set @T_Db_codgru=1    
    
    
BEGIN TRANSACTION REGISTROUSUARIOSMOD    
    
 BEGIN TRY    
    
       PRINT 'INGRESA AL PROCEDIMIENTO'    
         
     set @T_Dt_FechaSistema=(CONVERT(nvarchar(8),getdate(), 112))      
         
      PRINT 'INGRESA Aactualizar usuario'  
     
   SELECT @T_Db_CodUsrP=codusu from Cg3000..TP_USUARIOS   where codusu=@idUsuario    
  
         
   update Cg3000..TP_USUARIOS    
   set    
   nombre=@nombre,    
   apellidos=@apellidos,    
   identificacion=@identificacion,    
   nombreusu=@nombreusu,    
   --codusu=@codusu,    
   clave=@clave,    
   codigo_c=@codigo_c,    
   --fechaIngreso=@fechaIngreso,    
   fechaCaducidad=@fechaCaducidad,    
   estado=@estado,    
   tipoUsuario=@tipoUsuario    
   where codusu=@idUsuario    
    
   --/*    
         
   UPDATE  Sic3000..SeguridadUsuario      
   SET     
   --codusu=@codusu,     
   coddep=@T_Db_coddep,     
   --codgru=@T_Db_codgru,     
   nomusu=@nombreusu,     
   claacc=@clave,     
   feccad=@fechaCaducidad ,    
   --cargo=@T_Db_cargo,    
   --GeneraAsContable=0,    
   APELLIDOS=@apellidos,    
   NOMBRES=@nombre,    
   CEDULA=@identificacion,    
   codigoCg=@codigo_c    
   WHERE codusu=@T_Db_CodUsrP  
   --@codusu    
       
   -----///////// ACTUALIZA CG3000/    
         
   update Cg3000..Cgusuario     
   set    
   --codusu=@codusu,     
   coddep=@T_Db_coddep,     
   --codgru=@T_Db_codgru,     
   nomusu=@nombreusu,     
   claacc=@clave,     
   feccad=@fechaCaducidad,     
   --cargo=@T_Db_cargo,     
   --caducado=,    
   apellidos=@apellidos,    
   nombres=@nombre,    
   cedula=@identificacion,    
   codigoCg=@codigo_c    
   WHERE codusu=@T_Db_CodUsrP  
   --@codusu    
    
    
   UPDATE His3000..USUARIOS    
   SET     
   --ID_USUARIO=,    
   DEP_CODIGO=@T_Db_coddep,    
   NOMBRES=@nombre,    
   APELLIDOS=@apellidos,    
   IDENTIFICACION=@identificacion,    
   --FECHA_INGRESO=,    
   FECHA_VENCIMIENTO=@fechaCaducidad,    
   DIRECCION= @direccion,    
   --EMAIL=,    
   ESTADO= @estado,    
   USR=@nombreusu,    
   PWD=@clave,    
   --LOGEADO=,    
   Codigo_Rol=@codusu    
   WHERE ID_USUARIO=@T_Db_CodUsrP  
   --@codusu    
       
       
   COMMIT TRANSACTION REGISTROUSUARIOSMOD     
       
   Return 1    
    
 END TRY    
    
    
 BEGIN CATCH    
   print 'rollback'    
   ROLLBACK TRANSACTION REGISTROUSUARIOSMOD    
   Return 0    
 END CATCH
 
 go
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 create PROCEDURE [dbo].[sp_buscar_Usuario_id]    
    
 @idUsuario bigint     
AS    
    
BEGIN    
    
 SET NOCOUNT ON;    
    -- Insert statements for procedure here    
     
 select     
 idUsuario,   
 nombre,   
 apellidos,   
 identificacion,   
 nombreusu,   
 codusu,   
 clave,   
 codigo_c,   
 fechaIngreso,   
 fechaCaducidad,   
 estado,   
 tipoUsuario  
 from Cg3000..TP_USUARIOS         
 WHERE codusu =@idUsuario  
      
END
go
--------------------------------------------------------------------------------------------------------------------------------------------------------


create PROCEDURE [dbo].[sp_Registro_Urs_Sistemas]  
(  
  @nombre as nvarchar(150),  
  @apellidos as nvarchar(50),  
  @identificacion as nvarchar(20),  
  @nombreusu as nvarchar(20),  
  @codusu as float,   
  @clave as nvarchar(20),   
  @codigo_c as float,  
  @fechaIngreso as date,   
  @fechaCaducidad as date,   
  @estado as int,  
  @tipoUsuario  as int,
  @coddep as float,
  @direccion as nvarchar(50),
  
  @idUsuario int OUT    
)  
AS  
  
  
Set NoCount On        
Set Ansi_Warnings On        
  
  
declare @T_dt_Fecha as date  
DECLARE @T_Db_FechaNumero as numeric  
declare @T_Db_FechaIngNumero as numeric  
declare @T_Dt_FechaSistema as date  
declare @T_Ln_Codigo_Usuario as numeric  
declare @T_Db_cargo as float  
declare @T_Db_codgru as float  
declare @T_Db_coddep as float  
  
  
set @T_Db_cargo=1  
set @T_Db_coddep=@coddep  
set @T_Db_codgru=1  
  
  
BEGIN TRANSACTION REGISTROUSUARIOS  
  
 BEGIN TRY  
  
       PRINT 'INGRESA AL PROCEDIMIENTO'  
       
  set @T_Dt_FechaSistema=(CONVERT(nvarchar(8),getdate(), 112))    
       
  SELECT   
  @T_Ln_Codigo_Usuario=secuencial  FROM CG3000..tp_parametros_usuarios where   codigo =1  
  
  print '@T_St_Codigo_Usuario'  
  print @T_Ln_Codigo_Usuario  
  
  -----busca el numero de control---------  
    
  if @T_Ln_Codigo_Usuario is not null  
  begin  
   print '@T_St_Codigo_Usuario'   
   PRINT @T_Ln_Codigo_Usuario  
   Update Cg3000..tp_parametros_usuarios set  secuencial=secuencial+1  where  codigo =1  
  end  
  ELSE  
  BEGIN  
   print'NO GENERA NUMERO DE CONTROL USUARIOS'  
      ROLLBACK TRANSACTION REGISTROUSUARIOS  
   RETURN 0  
  END  
  --------------------------------------end  
  
  
  IF @T_Ln_Codigo_Usuario IS NOT NULL  
  
  BEGIN  
  
      PRINT 'INGRESA A GRABAR'  
     INSERT INTO Cg3000..TP_USUARIOS(nombre,apellidos,identificacion,nombreusu,codusu,clave,codigo_c,fechaIngreso,fechaCaducidad,estado,tipoUsuario)  
   VALUES(@nombre,@apellidos,@identificacion,@nombreusu,@T_Ln_Codigo_Usuario,@clave,@codigo_c,@fechaIngreso,@fechaCaducidad,@estado,@tipoUsuario)  
     
   --/*  
   INSERT INTO   Sic3000..SeguridadUsuario  (codusu, coddep, codgru, nomusu, claacc, feccad ,cargo,clave,GeneraAsContable,APELLIDOS,NOMBRES,CEDULA,codigoCg)  
   VALUES (@T_Ln_Codigo_Usuario, @T_Db_coddep, @T_Db_codgru, @nombreusu, @clave, @fechaCaducidad, @T_Db_cargo,1,'',@apellidos,@nombre,@identificacion,@codigo_c)  
  
   INSERT INTO  Cg3000..Cgusuario (codusu, coddep, codgru, nomusu, claacc, feccad, cargo, caducado,apellidos,nombres,cedula,codigoCg)  
   VALUES (@T_Ln_Codigo_Usuario, @T_Db_coddep, @T_Db_codgru, @nombreusu, @clave, @fechaCaducidad, @T_Db_cargo,0,@apellidos,@nombre,@identificacion,@codigo_c)  
  
   INSERT INTO His3000..USUARIOS   
   (ID_USUARIO,DEP_CODIGO,NOMBRES,APELLIDOS,IDENTIFICACION,FECHA_INGRESO,FECHA_VENCIMIENTO,DIRECCION,EMAIL,ESTADO,USR,PWD,LOGEADO,Codigo_Rol)  
   VALUES (@T_Ln_Codigo_Usuario, @T_Db_coddep,@nombre,@apellidos,@identificacion, @fechaIngreso, @fechaCaducidad,@direccion,'',@estado,@nombreusu, @clave,0,@T_Ln_Codigo_Usuario)  
   --*/  
   set @idUsuario=@T_Ln_Codigo_Usuario   
  
   COMMIT TRANSACTION REGISTROUSUARIOS   

   Return 1  
  
  END  
  
  
 END TRY  
  
  
 BEGIN CATCH  
      print 'rollback'  
   ROLLBACK TRANSACTION REGISTROUSUARIOS  
   Return 0  
 END CATCH  
 -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 alter PROCEDURE sp_MedicoPatologo  
AS  
BEGIN  
SELECT M.MED_CODIGO AS CODIGO, M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' '  
+ M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO, EM.ESP_NOMBRE AS ESPECIALIDAD  
FROM MEDICOS M  
INNER JOIN ESPECIALIDADES_MEDICAS EM ON M.ESP_CODIGO = EM.ESP_CODIGO  
WHERE M.ESP_CODIGO = 148  
END  

