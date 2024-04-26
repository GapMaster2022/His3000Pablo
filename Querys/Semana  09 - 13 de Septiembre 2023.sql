---------------------------------------------------------------------------10/10/2023------------------------------------------------------------------------------
alter procedure sp_GuardaDevolucionPedido        
(@Ped_Codigo as int,        
@DevFecha as datetime,        
@ID_USUARIO as smallint,        
@DevObservacion as varchar(256),    
@cue_Codigo as bigint,   
@ip_maquina as varchar(25)  
)        
as        
begin        
        
declare @CodigoDevolucion as BigInt        
declare @CueCodigo as bigint    
if(@cue_Codigo > 0)    
begin    
update HC_IMAGENOLOGIA_AGENDAMIENTOS set estado=2 where ATE_CODIGO=(select ATE_CODIGO from CUENTAS_PACIENTES where CUE_CODIGO=@cue_Codigo)    
end    
select @CodigoDevolucion = ISNULL(MAX(DevCodigo),0) + 1 from PEDIDO_DEVOLUCION        
        
insert into PEDIDO_DEVOLUCION values (@CodigoDevolucion,@Ped_Codigo,@DevFecha,@ID_USUARIO,@DevObservacion,@ip_maquina)        
        
SELECT @CodigoDevolucion        
    
END  
  
  