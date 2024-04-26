------------------------SP QUE CREAR REGISTROS EN Cgopciusu  ---------------------14-02-2024               CRISTIAN RUIZ

USE [Cg3000]
GO
/****** Object:  StoredProcedure [dbo].[GenerarCombinacionesUsuariosOpcionesCGcodusu]    Script Date: 14/02/2024 11:58:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Añadir el parámetro codusu al SP
CREATE PROCEDURE [dbo].[GenerarCombinacionesUsuariosOpcionesCGcodusu]
    @codusu INT  -- Parámetro de entrada para el código de usuario
AS
BEGIN
    -- Verificar si la tabla temporal ya existe
    IF OBJECT_ID('tempdb..#Combinaciones') IS NOT NULL
        DROP TABLE #Combinaciones;

    -- Crear una tabla temporal para almacenar las combinaciones
    CREATE TABLE #Combinaciones (
        Usuario INT,
        Opcion INT
    )

    -- Insertar las combinaciones en la tabla temporal
    INSERT INTO #Combinaciones (Usuario, Opcion)
    SELECT @codusu, o.codopc
    FROM Cgopcion o;

    -- Mostrar el resultado temporal
    SELECT * FROM #Combinaciones;

    -- Insertar las combinaciones en la tabla permanente (Cgopciusu)
    INSERT INTO Cgopciusu (codusu, codopc, staopc, sw)
    SELECT c.Usuario, c.Opcion, 'N', LEFT(CAST(c.Opcion AS VARCHAR), 1)
    FROM #Combinaciones c
    WHERE NOT EXISTS (
        SELECT 1
        FROM Cgopciusu
        WHERE codusu = c.Usuario AND codopc = c.Opcion
    );

    -- Eliminar la tabla temporal al finalizar el SP
    DROP TABLE #Combinaciones;
    
    UPDATE Cgopciusu SET sw = '8' WHERE sw = '6' AND codusu = @codusu; -- Actualizar solo las filas correspondientes al codusu especificado
END

------------------------SP QUE CREAR REGISTROS EN SeguridadUsuarioOpciones  ---------------------14-02-2024               CRISTIAN RUIZ


USE [Sic3000]
GO
/****** Object:  StoredProcedure [dbo].[GenerarCombinacionesUsuariosOpcionesSICcodusu]    Script Date: 14/02/2024 11:58:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Añadir el parámetro codusu al SP
CREATE PROCEDURE [dbo].[GenerarCombinacionesUsuariosOpcionesSICcodusu]
    @codusu INT  -- Parámetro de entrada para el código de usuario
AS
BEGIN
    -- Verificar si la tabla temporal ya existe
    IF OBJECT_ID('tempdb..#Combinaciones') IS NOT NULL
        DROP TABLE #Combinaciones;

    -- Crear una tabla temporal para almacenar las combinaciones
    CREATE TABLE #Combinaciones (
        Usuario INT,
        Opcion INT
    )

    -- Insertar las combinaciones en la tabla temporal
    INSERT INTO #Combinaciones (Usuario, Opcion)
    SELECT @codusu, o.codopc  -- Utilizar el parámetro codusu
    FROM SeguridadOpciones o;

    -- Mostrar el resultado temporal
    SELECT * FROM #Combinaciones;

    -- Insertar las combinaciones en la tabla permanente (SeguridadUsuarioOpciones)
    INSERT INTO SeguridadUsuarioOpciones (codusu, codopc, staopc, sw)
    SELECT c.Usuario, c.Opcion, 'N', LEFT(CAST(c.Opcion AS VARCHAR), 1)
    FROM #Combinaciones c
    WHERE NOT EXISTS (
        SELECT 1
        FROM SeguridadUsuarioOpciones
        WHERE codusu = c.Usuario AND codopc = c.Opcion
    );

    -- Eliminar la tabla temporal al finalizar el SP
    DROP TABLE #Combinaciones;
END
--------------------------------------SP PARA QUE GENERE LOS CG
ALTER PROCEDURE [dbo].[sp_Registro_Urs_Sistemas]  
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
   
   EXEC cg3000..GenerarCombinacionesUsuariosOpcionesCGcodusu @idUsuario
   EXEC sic3000..GenerarCombinacionesUsuariosOpcionesSICcodusu @idUsuario

   Return 1  
  
  END  
  
  
 END TRY  
  
  
 BEGIN CATCH  
      print 'rollback'  
   ROLLBACK TRANSACTION REGISTROUSUARIOS  
   Return 0  
 END CATCH  
  

GO

----------------------
-------se crea en el his3000 va ha ser el mismo para todos del cg
CREATE TABLE ParametroTemporal (
    Parametro INT
);

-- Procedimiento almacenado que ejecuta el trigger se crea en el cg
alter PROCEDURE EjecutarTR_Cggruopc_AUD
    @Parametro INT
AS
BEGIN
    -- Eliminar todos los registros de la tabla temporal
	DELETE FROM his3000..ParametroTemporal;

    -- Insertar el parámetro en la tabla temporal
    INSERT INTO his3000..ParametroTemporal (Parametro) VALUES (@Parametro);

END;


----- Trigger que utiliza el parámetro almacenado en la tabla temporal
create TRIGGER TR_Cggruopc_AUD
ON CG3000..Cggruopc
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el valor del parámetro desde la tabla temporal
    DECLARE @Parametro INT;
    SELECT @Parametro = Parametro FROM his3000..ParametroTemporal;

    -- Insertar en SERIES3000_AUDITORIA..Cggruopc_AUD utilizando el parámetro
    INSERT INTO SERIES3000_AUDITORIA..Cggruopc_AUD (
        codopc, codgru, staopc, codusumod, fechamod
    )
    SELECT
        codopc, codgru, staopc, @Parametro, GETDATE()
    FROM INSERTED;
END;

----- Trigger que utiliza el parámetro almacenado en la tabla temporal
 create TRIGGER TR_Cgopciusu_AUD
ON CG3000..Cgopciusu
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el valor del parámetro desde la tabla temporal
    DECLARE @Parametro INT;
    SELECT @Parametro = Parametro FROM his3000..ParametroTemporal;

    -- Insertar en SERIES3000_AUDITORIA..Cgopciusu_AUD utilizando el parámetro
    INSERT INTO SERIES3000_AUDITORIA..Cgopciusu_AUD (
        codusu,codopc,staopc,sw, codusumod, fechamod
    )
    SELECT
        codusu,codopc,staopc,sw, @Parametro, GETDATE()
    FROM INSERTED;
END;

------------------------------------------------------------AUDITORIA SEGURIDAD GRUPO 
create TRIGGER TR_SeguridadGrupo_AUD
ON SIC3000..SeguridadGrupoOpciones
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el valor del parámetro desde la tabla temporal
    DECLARE @Parametro INT;
    SELECT @Parametro = Parametro FROM his3000..ParametroTemporal;

    -- Insertar en SERIES3000_AUDITORIA..Cggruopc_AUD utilizando el parámetro
    INSERT INTO SERIES3000_AUDITORIA..SeguridadGrupoOpciones_AUD (
        codopc, codgru, staopc, codusumod, fechamod
    )
    SELECT
        codopc, codgru, staopc, @Parametro, GETDATE()
    FROM INSERTED;
END;

------------------------------------------------------------AUDITORIA SEGURIDAD opciones usuario


----- Trigger que utiliza el parámetro almacenado en la tabla temporal
 create TRIGGER TR_SeguridadOpcUsu_AUD
ON sic3000..SeguridadUsuarioOpciones
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el valor del parámetro desde la tabla temporal
    DECLARE @Parametro INT;
    SELECT @Parametro = Parametro FROM his3000..ParametroTemporal;

    -- Insertar en SERIES3000_AUDITORIA..Cgopciusu_AUD utilizando el parámetro
    INSERT INTO SERIES3000_AUDITORIA..SeguridadUsuarioOpciones_AUD (
        codusu,codopc,staopc,sw, codusumod, fechamod
    )
    SELECT
        codusu,codopc,staopc,sw, @Parametro, GETDATE()
    FROM INSERTED;
END;

-----------------------AUDITORIA ACCESO PERFILES

----- Trigger que utiliza el parámetro almacenado en la tabla temporal
 create TRIGGER TR_PERFILES_ACCESOS_AUD
ON his3000..PERFILES_ACCESOS
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el valor del parámetro desde la tabla temporal
    DECLARE @Parametro INT;
    SELECT @Parametro = Parametro FROM his3000..ParametroTemporal;

    -- Insertar en SERIES3000_AUDITORIA..Cgopciusu_AUD utilizando el parámetro
    INSERT INTO SERIES3000_AUDITORIA..PERFILES_ACCESOS_AUD (
        ID_ACCESO,ID_PERFIL,ID_PERFILES_ACCESOS,codusumod, fechamod
    )
    SELECT
        ID_ACCESO,ID_PERFIL,ID_PERFILES_ACCESOS, @Parametro, GETDATE()
    FROM INSERTED;
END;

-----------------------AUDITORIA PERFILES USUARIOS

----- Trigger que utiliza el parámetro almacenado en la tabla temporal
 create TRIGGER TR_USUARIOS_PERFILES_AUD_AUD
ON his3000..USUARIOS_PERFILES
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Obtener el valor del parámetro desde la tabla temporal
    DECLARE @Parametro INT;
    SELECT @Parametro = Parametro FROM his3000..ParametroTemporal;

    -- Insertar en SERIES3000_AUDITORIA..Cgopciusu_AUD utilizando el parámetro
    INSERT INTO SERIES3000_AUDITORIA..USUARIOS_PERFILES_AUD (
        ID_PERFIL,ID_USUARIO,ID_USUARIOS_PERFILES,INSERTAR,ACTUALIZAR,ELIMINAR,codusumod, fechamod
    )
    SELECT
        ID_PERFIL,ID_USUARIO,ID_USUARIOS_PERFILES,INSERTAR,ACTUALIZAR,ELIMINAR, @Parametro, GETDATE()
    FROM INSERTED;
END;

----------------------------CREAR TABLAS EN SERIES3000_AUDITORIA
select * into  series3000_auditoria..Cggruopc_AUD from cg3000..Cggruopc
select * into  series3000_auditoria..Cgopciusu_AUD from cg3000..Cgopciusu
select * into  series3000_auditoria..SeguridadGrupoOpciones_AUD from Sic3000..SeguridadGrupoOpciones
select * into  series3000_auditoria..SeguridadUsuarioOpciones_AUD from Sic3000..SeguridadUsuarioOpciones
select * into  series3000_auditoria..PERFILES_ACCESOS_AUD from HIS3000..PERFILES_ACCESOS
select * into  series3000_auditoria..USUARIOS_PERFILES_AUD from HIS3000..USUARIOS_PERFILES

select * into  series3000_auditoria..SeguridadesUsuarioGrupo_AUD from Sic3000..SeguridadesUsuarioGrupo
select * into  series3000_auditoria..Cgusugrup_AUD from cg3000..Cgusugrup





--------------------------AUTOALIZACION TRIGER SeguridadGrupoOpciones-- Procedimiento para crear un perfil de usuario en SIC
ALTER PROCEDURE [dbo].[sp_ActualizarUsuarioGrupo]
    @CODUSU BIGINT,
    @CODGRU BIGINT
AS
BEGIN
    DECLARE @Parametro INT;
    SELECT @Parametro = Parametro FROM his3000..ParametroTemporal;

    -- Primera operación: Actualizar SeguridadUsuarioOpciones
    UPDATE sic3000..SeguridadUsuarioOpciones
    SET staopc = 'N'
    WHERE codusu = @CODUSU;

    -- Segunda operación: Actualizar SeguridadUsuarioOpciones basado en SeguridadGrupoOpciones
    IF EXISTS (
        SELECT 1
        FROM sic3000..SeguridadGrupoOpciones
        WHERE codgru = @CODGRU AND staopc = 'S'
    )
    BEGIN
        UPDATE sic3000..SeguridadUsuarioOpciones
        SET staopc = 'S'
        WHERE codusu = @CODUSU
        AND codopc IN (
            SELECT codopc
            FROM sic3000..SeguridadGrupoOpciones
            WHERE codgru = @CODGRU AND staopc = 'S'
        );

      
		
			INSERT INTO SERIES3000_AUDITORIA..SeguridadesUsuarioGrupo_AUD (codusu,codgru,staopc, codusumod, fechamod)values(
		 @CODUSU,@CODGRU,1,@Parametro,GETDATE());
    END;
END;
--------SP PARA QUE GUARDE EN AUDITORIA
USE [His3000]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarUsuarioGrupoCg]    Script Date: 19/02/2024 13:30:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE 
[dbo].[sp_ActualizarUsuarioGrupoCg]


    @CODUSU BIGINT,
    @CODGRU BIGINT
AS
BEGIN
	DECLARE @Parametro INT;
    SELECT @Parametro = Parametro FROM his3000..ParametroTemporal;
    -- Primera operación: Actualizar SeguridadUsuarioOpciones
    UPDATE cg3000..Cgopciusu
    SET staopc = 'N'
    WHERE codusu = @CODUSU;

    -- Segunda operación: Actualizar SeguridadUsuarioOpciones basado en SeguridadGrupoOpciones
    IF EXISTS (
        SELECT 1
        FROM Cg3000..Cggruopc
        WHERE codgru = @CODGRU AND staopc = 'S'
    )
    BEGIN
        UPDATE cg3000..Cgopciusu
        SET staopc = 'S'
        WHERE codusu = @CODUSU
        AND codopc IN (
            SELECT codopc
            FROM Cg3000..Cggruopc
            WHERE codgru = @CODGRU AND staopc = 'S'
        );
    END;
	 	INSERT INTO SERIES3000_AUDITORIA..Cgusugrup_AUD (codusu,codgru,staopc, codusumod, fechamod)values(
		 @CODUSU,@CODGRU,1,@Parametro,GETDATE());


END;
