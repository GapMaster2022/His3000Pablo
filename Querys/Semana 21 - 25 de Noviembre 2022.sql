alter table HC_LABORATORIO_CLINICO
add LCL_ESTADO BIT
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------
alter PROCEDURE sp_QuiroCrearVariosProcedimientos      
@procedimiento nvarchar(150),  
@bodega int  
AS      
IF NOT EXISTS(SELECT * FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @procedimiento)      
BEGIN      
print 'paso 1'    
 INSERT INTO PROCEDIMIENTOS_CIRUGIA (PCI_DESCRIPCION,PCI_ESTADO,PCI_BODEGA) VALUES(@procedimiento, 0,@bodega) --AQUI GENERA UN NUEVO PROCEDIMIENTO N VECES.      
 SELECT * FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @procedimiento --AQUI ENVIO EL PCI_CODIGO PARA AVANZAR      
END      
ELSE      
BEGIN      
 SELECT * FROM PROCEDIMIENTOS_CIRUGIA WHERE PCI_DESCRIPCION = @procedimiento --AQUI ENVIO EL PCI_CODIGO PARA AVANZAR      
END 