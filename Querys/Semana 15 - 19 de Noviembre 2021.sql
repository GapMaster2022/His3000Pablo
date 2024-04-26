 ALTER procedure sp_mostrarcongregaciones    
  @tipo int    
  as    
  if(@tipo =0)    
  begin    
  select ase_nombre as CONGREGACION, ase_ruc as RUC,   
  ase_direccion as DIRECCION, ase_ciudad as CIUDAD, ase_telefono as TELEFONO,
  ISNULL((SELECT email FROM SIC3000..Cliente WHERE ASE_RUC = ruccli), '') AS EMAIL
  from his3000..aseguradoras_empresas where te_codigo = 6    
  end    
  else    
  begin    
  select ase_nombre as CONGREGACION, ase_ruc as RUC,     
  ase_direccion as DIRECCION, ase_ciudad as CIUDAD, ase_telefono as TELEFONO,
   ISNULL((SELECT email FROM SIC3000..Cliente WHERE ASE_RUC = ruccli), '') AS EMAIL
  from his3000..aseguradoras_empresas where te_codigo = 1    
  end 
  --------------------------------------------------------------
  
  CREATE PROCEDURE sp_OcuparControlADS
AS
DECLARE @fechaControl nvarchar(10)
SET @fechaControl = (CONVERT(VARCHAR(2),MONTH(GETDATE()))) + '/' + DATENAME(YEAR, GETDATE())
PRINT @fechaControl
SELECT numdoc_zv FROM Cg3000..Cgzonval WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD' AND ocupado = 0

IF((SELECT numdoc_zv FROM Cg3000..Cgzonval WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD' AND ocupado = 0) > 0)
BEGIN
	UPDATE Cg3000..Cgzonval SET ocupado = 1 WHERE fecval_zv = @fechaControl AND tipdoc_zv = 'AD'
END
GO
