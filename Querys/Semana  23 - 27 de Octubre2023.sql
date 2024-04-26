alter PROCEDURE sp_CrearMedicos  
@med_codigo int,        
@esp_codigo int,        
@med_nombre1 nvarchar(100),        
@med_nombre2 nvarchar(100),        
@med_apellido1 nvarchar(100),        
@med_apellido2 nvarchar(100),        
@fechanacimiento datetime,        
@med_direccion nvarchar(500),        
@med_direccionC nvarchar(160),        
@med_ruc nvarchar(16),        
@med_email nvarchar(80),        
@med_genero char(1),    
@med_cuenta_contable nvarchar(10),    
@telefono_casa nvarchar(16),        
@telefono_consu nvarchar(16),        
@celular nvarchar(16),        
@transferencia bit,        
@tim_codigo int,        
@tih_codigo int ,      
@ret_codigo int ,
@llamada bit   
AS    
declare @codigo_med nvarchar (15)    
set @codigo_med = (select codigo_c from Cg3000..Cgcodcon where campo4 = @med_ruc)    
IF EXISTS (select codigo_c from Cg3000..Cgcodcon where campo4 = @med_ruc)    
begin    
INSERT INTO MEDICOS VALUES(@med_codigo, @ret_codigo, 10, @esp_codigo, 1, 1, @tim_codigo,@tih_codigo, @codigo_med, GETDATE(), GETDATE(), @med_nombre1,         
@med_nombre2, @med_apellido1, @med_apellido2, @fechanacimiento, @med_direccion, @med_direccionC, @med_ruc,         
@med_email, @med_genero, NULL, 'C', @med_cuenta_contable, @telefono_casa, @telefono_consu, @celular, '000000', NULL, NULL, NULL, @transferencia, @llamada,        
1, null, null,1)    
end     
else     
begin    
INSERT INTO MEDICOS VALUES(@med_codigo, @ret_codigo, 10, @esp_codigo, 1, 1, @tim_codigo,@tih_codigo, NULL, GETDATE(), GETDATE(), @med_nombre1,         
@med_nombre2, @med_apellido1, @med_apellido2, @fechanacimiento, @med_direccion, @med_direccionC, @med_ruc,         
@med_email, @med_genero, NULL, 'C', @med_cuenta_contable, @telefono_casa, @telefono_consu, @celular, '000000', NULL, NULL, NULL, @transferencia, @llamada,        
1, null, null,1)    
end  

