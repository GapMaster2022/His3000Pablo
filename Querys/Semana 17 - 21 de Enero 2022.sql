SELECT * FROM PARAMETROS ORDER BY 1 DESC

SELECT * FROM PARAMETROS_DETALLE ORDER BY 1 DESC

INSERT INTO PARAMETROS VALUES (26, 1, 'FORMULARIOS MSP', 'CAMBIO DE HC POR CEDULA', 0)

INSERT INTO PARAMETROS_DETALLE VALUES(37, 26, 'FORMULARIOS MSP', NULL, 'CAMBIO DE HC POR CEDULA', 0)

----------------------------------------------------------------------------------------------
alter PROCEDURE sp_HonorarioCxp    
@codigo_c float,    
@numasi float,    
@usuario float,    
@nocomp nvarchar(100),    
@haber float,    
@debe float,
@numlinea int,    
@forpag int,    
@despag nvarchar(50),    
@hom_codigo bigint,    
@fechatran datetime,
@cuenta nvarchar(50)
AS    
BEGIN    
DECLARE @fecha nvarchar(50)    
DECLARE @MES NVARCHAR(2)  
DECLARE @DIA NVARCHAR(2)  
SET @MES = CONVERT(VARCHAR(2),MONTH(@fechatran))  
SET @DIA = DATENAME(day, @fechatran)  
IF(LEN(@MES) = 1)  
BEGIN  
 IF(LEN(@DIA) = 1)  
 BEGIN  
  SET @fecha = DATENAME(year,@fechatran) + '-' + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' + '0' +DATENAME(day,@fechatran)  
 END  
 ELSE  
 BEGIN  
  SET @fecha = DATENAME(year,@fechatran) + '-' + '0' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' +DATENAME(day,@fechatran)  
 END  
END  
ELSE  
BEGIN  
 IF(LEN(@DIA) = 1)  
 BEGIN  
  SET @fecha = DATENAME(year,@fechatran) + '-' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' + '0' +DATENAME(day,@fechatran)  
 END  
 ELSE  
 BEGIN  
  SET @fecha = DATENAME(year,@fechatran) + '-' + CONVERT(VARCHAR(2),MONTH(@fechatran)) + '-' +DATENAME(day,@fechatran)  
 END  
END  
INSERT INTO Cg3000..CgCuentasXPagar VALUES(@codigo_c, 1, 'AD', @numasi, @fecha, @fecha, @usuario, @nocomp, @nocomp,    
@fecha, @cuenta, @debe, @haber, 'A', @fecha, @usuario, @numlinea, 'N', NULL, NULL, NULL, NULL, 0, 0, @forpag, 1, @despag, 0, 0,@hom_codigo)    
END 

------------------------------------------------------------------------------------
