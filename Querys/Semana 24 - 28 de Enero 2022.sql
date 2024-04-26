CREATE PROCEDURE sp_EditarForm002ConsultaExterna   
 @Motivo AS varchar(1000) ,  
 @AntecedentesPersonales AS varchar(1000) ,  
 @Cardiopatia AS varchar(1) ,  
 @Diabetes AS varchar(1) ,  
 @Vascular AS varchar(1) ,  
 @Hipertencion AS varchar(1) ,  
 @Cancer AS varchar(1) ,  
 @tuberculosis AS varchar(1) ,  
 @mental AS varchar(1) ,  
 @infecciosa AS varchar(1) ,  
 @malformacion AS varchar(1) ,  
 @otro AS varchar(1) ,  
 @antecedentesFamiliares AS varchar(1000) ,  
 @enfermedadActual AS nchar(1000) ,  
 @sentidos AS varchar(1) ,  
 @sentidossp AS varchar(1) ,  
 @respiratorio AS varchar(1) ,  
 @respiratoriosp AS varchar(1) ,  
 @cardioVascular AS varchar(1) ,  
 @cardioVascularsp AS varchar(1) ,  
 @digestivo AS varchar(1) ,  
 @digestivosp AS varchar(1) ,  
 @genital AS varchar(1) ,  
 @genitalsp AS varchar(1) ,  
 @urinario AS varchar(1) ,  
 @urinariosp AS varchar(1) ,  
 @esqueletico AS varchar(1) ,  
 @esqueleticosp AS varchar(1) ,  
 @endocrino AS varchar(1) ,  
 @endocrinosp AS varchar(1) ,  
 @linfatico AS varchar(1) ,  
 @linfaticosp AS varchar(1) ,  
 @nervioso AS varchar(1) ,  
 @nerviososp AS varchar(1) ,  
 @revisionactual AS varchar(1000) ,  
 @fechamedicion AS varchar(20) ,  
 @temperatura AS varchar(10) ,  
 @presion1 AS varchar(10) ,  
 @presion2 AS varchar(10) ,  
 @pulso AS varchar(10) ,  
 @frecuenciaRespiratoria AS varchar(10) ,  
 @peso AS varchar(10) ,  
 @talla AS varchar(10) ,  
 @cabeza AS varchar(1) ,  
 @cabezasp AS varchar(1) ,  
 @cuello AS varchar(1) ,  
 @cuellosp AS varchar(1) ,  
 @torax AS varchar(1) ,  
 @toraxsp AS varchar(1) ,  
 @abdomen AS varchar(1) ,  
 @abdomensp AS varchar(1) ,  
 @pelvis AS varchar(1) ,  
 @pelvissp AS varchar(1) ,  
 @extremidades AS varchar(1) ,  
 @extremidadessp AS varchar(1) ,  
 @examenFisico AS varchar(1000) ,  
 @diagnostico1 AS varchar(100) ,  
 @diagnostico1cie AS varchar(10) ,  
 @diagnostico1pre AS varchar(1) ,  
 @diagnostico1def AS varchar(1) ,  
 @diagnostico2 AS varchar(100) ,  
 @diagnostico2cie AS varchar(10) ,  
 @diagnostico2pre AS varchar(1) ,  
 @diagnostico2def AS varchar(1) ,  
 @diagnostico3 AS varchar(100) ,  
 @diagnostico3cie AS varchar(10) ,  
 @diagnostico3def AS varchar(1) ,  
 @diagnostico3pre AS varchar(1) ,  
 @diagnostico4 AS varchar(100) ,  
 @diagnostico4cie AS varchar(10) ,  
 @diagnostico4def AS varchar(1) ,  
 @diagnostico4pre AS varchar(1) ,  
 @planesTratamiento AS varchar(1000) ,  
 @evolucion AS varchar(1000) ,  
 @prescripciones AS varchar(1000) ,  
 @fecha AS varchar(20) ,  
 @hora AS varchar(10) ,  
 @dr AS varchar(50) ,  
 @codigo AS varchar(20),
 @id bigint
AS
BEGIN  
  UPDATE Form002MSP SET Motivo = @Motivo, AntecedentesPersonales = @AntecedentesPersonales, Cardiopatia = @Cardiopatia,
  Diabetes = @Diabetes, Vascular = @Vascular, Hipertencion = @Hipertencion, Cancer = @Cancer, tuberculosis = @tuberculosis,
  mental = @mental, infecciosa = @infecciosa, malformacion = @malformacion, otro = @otro, antecedentesFamiliares = @antecedentesFamiliares,
  enfermedadActual = @enfermedadActual, sentidos = @sentidos, sentidossp = @sentidossp, respiratorio = @respiratorio, 
  respiratoriosp = @respiratoriosp, cardioVascular = @cardioVascular, cardioVascularsp = @cardioVascularsp, digestivo = @digestivo,
  digestivosp = @digestivosp, genital = @genital, genitalsp = @genitalsp, urinario = @urinario, urinariosp = @urinariosp, 
  esqueletico = @esqueletico, esqueleticosp = @esqueleticosp, endocrino = @endocrino, endocrinosp = @endocrinosp, linfatico = @linfatico,
  linfaticosp = @linfaticosp, nervioso = @nervioso, nerviososp = @nerviososp, revisionactual = @revisionactual, fechamedicion = @fechamedicion,
  temperatura = @temperatura, presion1 = @presion1, presion2 = @presion2, pulso = @pulso, frecuenciaRespiratoria = @frecuenciaRespiratoria,
  peso = @peso, talla = @talla, cabeza = @cabeza, cabezasp = @cabezasp, cuello = @cuello, cuellosp = @cuellosp, torax = @torax,
  toraxsp = @toraxsp, abdomen = @abdomen, abdomensp = @abdomensp, pelvis = @pelvis, pelvissp = @pelvissp, extremidades = @extremidades,
  extremidadessp = @extremidadessp, examenFisico = @examenFisico, diagnostico1 = @diagnostico1, diagnostico1cie = @diagnostico1cie,
  diagnostico1pre = @diagnostico1pre, diagnostico1def = @diagnostico1def, diagnostico2 = @diagnostico2, diagnostico2cie = @diagnostico2cie,
  diagnostico2pre = @diagnostico2pre, diagnostico2def = @diagnostico2def, diagnostico3 = @diagnostico3, diagnostico3cie = @diagnostico3cie,
  diagnostico3def = @diagnostico3def, diagnostico3pre = @diagnostico3pre, diagnostico4 = @diagnostico4, diagnostico4cie = @diagnostico4cie,
  diagnostico4def = @diagnostico4def, diagnostico4pre = @diagnostico4pre, planesTratamiento = @planesTratamiento, evolucion = @evolucion,
  prescripciones = @prescripciones, fecha = @fecha, hora = @hora, dr = @dr, codigo = @codigo WHERE ID_FORM002 = @id
  
END
-------------------------------------------------------------
--solo falta en la pasteur
CREATE PROCEDURE sp_ParametroFormularios  
AS  
SELECT PAD_ACTIVO FROM PARAMETROS_DETALLE WHERE PAD_CODIGO = 37  

------------------------------------------------------------
CREATE TABLE FORMULARIOS_MSP_CERRADOS(
FMC_CODIGO BIGINT IDENTITY(1,1) PRIMARY KEY NOT NULL,
FMC_FORMULARIO NVARCHAR(10),
ATE_CODIGO BIGINT,
ID_USUARIO INT,
FMC_FECHA DATETIME DEFAULT GETDATE())

-----------------------------------------
CREATE PROCEDURE sp_CxE_Cerrar
@formulario nvarchar(10),
@ate_codigo bigint,
@id_usuario int
AS
INSERT INTO FORMULARIOS_MSP_CERRADOS(FMC_FORMULARIO, ATE_CODIGO, ID_USUARIO) VALUES(@formulario, @ate_codigo, @id_usuario)
GO

------------------------------------------------
CREATE PROCEDURE sp_CxE_Abrir
@ate_codigo bigint
AS
DELETE FROM FORMULARIOS_MSP_CERRADOS WHERE ATE_CODIGO = @ate_codigo
GO
--------------------------------------------
CREATE PROCEDURE [dbo].[sp_CertificadoMedicoPacienteMushugñan]    
AS    
BEGIN    
SELECT P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_IDENTIFICACION AS Identificacion,    
P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,    
A.ATE_FECHA_INGRESO AS 'F. Ingreso', A.ATE_CODIGO, A.ATE_FECHA_ALTA, M.MED_EMAIL, M.MED_RUC,    
M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico    
FROM PACIENTES P     
INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO    
INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO    
INNER JOIN USUARIOS U ON M.ID_USUARIO = U.ID_USUARIO    
WHERE A.TIP_CODIGO = 10
ORDER BY A.ATE_FECHA_INGRESO DESC    
END

----------------------------------------------
ALTER PROCEDURE [dbo].[sp_CertificadoPaciente]  
@ate_codigo bigint  
AS  
SELECT p.PAC_APELLIDO_PATERNO, p.PAC_APELLIDO_MATERNO, p.PAC_NOMBRE1, p.PAC_NOMBRE2, A.ATE_CODIGO  
, TI.TIP_DESCRIPCION, PAC_IDENTIFICACION, p.PAC_FECHA_NACIMIENTO, p.PAC_NACIONALIDAD, p.PAC_GENERO,  
(select EMP_NOMBRE from EMPRESA), a.ATE_NUMERO_ATENCION, TI.TIP_CODIGO
from PACIENTES p   
inner join ATENCIONES a on p.PAC_CODIGO = a.PAC_CODIGO  
inner join TIPO_INGRESO TI on a.TIP_CODIGO = ti.TIP_CODIGO  
where a.ATE_CODIGO = @ate_codigo  

--------------------------------
CREATE PROCEDURE sp_ActualizaKardexSicMushuñan    
@numdoc nvarchar(20)    
AS    
UPDATE Sic3000..Kardex SET codlocal = 61 WHERE numdoc = @numdoc    