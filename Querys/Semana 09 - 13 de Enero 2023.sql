------------------------------------------------------------------------------- 2023-01-09 --------------------------------------------------------------------------------------------  
CREATE PROCEDURE sp_ConectarMedicoAsistencia  
@id nvarchar(15)  
AS  
SELECT ID_USUARIO FROM MEDICOS where MED_RUC like '%'+ @id + '%'  
