alter PROCEDURE [dbo].[sp_reporte_PRUEBA1_paquete]                      
@FechaInicio date,                      
@FechaFin date,                      
@estadofactura int,                      
@consulta int,                      
@atencion xml,                  
@Nombres as varchar(128),                  
@HC as varchar(64),                  
@NumeroAtencion as varchar(64),
@TipoSeguro int
                        
AS                      
BEGIN                      
                  
declare @FiltroNombres as varchar(128)                  
declare @FiltroHC as varchar(128)                  
declare @FiltroAtencion as varchar(128)                  
                    
 if(@consulta=1)                      
 begin                      
                       
 SELECT TOP 0    'DEPENDENCIA'=dbo.anexos( dbo.ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_DE,1),                       
     dbo.ATENCIONES.ATE_NUMERO_ATENCION  AS PLANILLA,                      
      'FECHA INGRESO'= dbo.ATENCIONES.ATE_FECHA_ALTA,                      
      'TIPO SEGURO'=dbo.anexos( dbo.ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_TS,1),                      
      'IDENTIFICACION'=  dbo.PACIENTES.PAC_IDENTIFICACION,                      
      'PACIENTE'=dbo.PACIENTES.PAC_APELLIDO_PATERNO+' '+ dbo.PACIENTES.PAC_APELLIDO_MATERNO+' '+                      
       dbo.PACIENTES.PAC_NOMBRE1+' '+ dbo.PACIENTES.PAC_NOMBRE2,                      
      'SEXO BENEFICIARIO'= dbo.PACIENTES.PAC_GENERO,                      
      'FECHA NACIMIENTO'= dbo.PACIENTES.PAC_FECHA_NACIMIENTO,                      
      DATEDIFF(yy,dbo.PACIENTES.PAC_FECHA_NACIMIENTO,dbo.ATENCIONES.ATE_FECHA) as  EDAD ,                      
      'TIPO DE   EXAMEN'=DBO.anexos(DBO.ATENCIONES_DETALLE_SEGUROS.ADA_TIPO_EXAMEN,1),                      
      'CODIGO PROCEDIMIENTO'=dbo.CUENTAS_PACIENTES.PRO_CODIGO_BARRAS,                      
      'DESCRIPCION '=dbo.CUENTAS_PACIENTES.CUE_DETALLE,                             
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,1) AS diagnostico_1,                      
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,2) AS diagnostico_2 ,                      
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,3) AS diagnostico_3 ,                      
      'CANTIDAD'= dbo.CUENTAS_PACIENTES.CUE_CANTIDAD,                      
      'VALOR UNITARIO'=  dbo.CUENTAS_PACIENTES.CUE_VALOR_UNITARIO,                       
      'DURACION  CONSULTA'=dbo.CUENTAS_PACIENTES.CUE_VALOR,                      
      'PARENTESCO'=dbo.anexos( dbo.ATENCIONES_DETALLE_SEGUROS.ADS_ASEGURADO_PARENTESCO,1),                       
      'CEDULA ASEGURADO'= dbo.ATENCIONES_DETALLE_SEGUROS.ADS_ASEGURADO_CEDULA,                      
      'NOMBRE ASEGURADO'= dbo.ATENCIONES_DETALLE_SEGUROS.ADS_ASEGURADO_NOMBRE,                      
      'CODIGO DERIVACION'= SUBSTRING(dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION,0,CHARINDEX('-', dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION)),                      
      'NUMERO DERIVACION'=SUBSTRING(dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION,CHARINDEX('-', dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION)+1, LEN(dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION)),                      
      'CONTINGENCIA'=dbo.anexos(  dbo.ATENCIONES_DETALLE_SEGUROS.ADA_CONTINGENCIA, 1),                      
      'DIAGNOSTICO P/D'=(SELECT CASE WHEN CA_DESCRIPCION='PRESUNTIVO' THEN 'P'                       
      WHEN CA_DESCRIPCION='DEFINITIVO' THEN 'D' END   FROM HC_CATALOGO_SUBNIVEL WHERE CA_CODIGO=ATENCIONES_DETALLE_SEGUROS.ADA_TIPO_DIAGNOSTICO),                                  
     'TIEMPO ANESTESIA'=dbo.anexos( dbo.CUENTAS_PACIENTES.ATE_CODIGO,2),                       
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,4) AS diagnostico_4 ,                      
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,5) AS diagnostico_5 ,                      
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,6) AS diagnostico_6 ,                                 
     'PORCENTAJE IVA'= CASE                      
      WHEN dbo.CUENTAS_PACIENTES.CUE_IVA >0 THEN 12                      
      WHEN dbo.CUENTAS_PACIENTES.CUE_IVA =0 THEN 0                      
      END,                      
     'IVA'=  DBO.CUENTAS_PACIENTES.CUE_IVA  ,                                          
      dbo.ATENCIONES.ESC_CODIGO,                      
    -- 'COMVENIO'= dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE,                                 
      dbo.ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,                           
     'MARCA FINAL'=dbo.ATENCIONES_DETALLE_SEGUROS.ADA_MARCA,                      
      dbo.CUENTAS_PACIENTES.RUB_CODIGO ,           
      isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=CUENTAS_PACIENTES.ATE_CODIGO),0) as 'RADICACION',                 
      isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=CUENTAS_PACIENTES.ATE_CODIGO),0) as 'TRAMITE' ,        
      (select ANI_DESCRIPCION from ANEXOS_IESS where ANEXOS_IESS.ANI_CODIGO=ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_TS) as SEGURO         
                                
 FROM dbo.CUENTAS_PACIENTES INNER JOIN                      
      dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN                      
      dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN                      
      dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN                      
      dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO INNER JOIN                      
      dbo.ATENCIONES_DETALLE_SEGUROS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCIONES_DETALLE_SEGUROS.ATE_CODIGO INNER JOIN                      
      dbo.RUBROS ON dbo.CUENTAS_PACIENTES.RUB_CODIGO = dbo.RUBROS.RUB_CODIGO                      
                                                  
  WHERE  dbo.CUENTAS_PACIENTES.PRO_CODIGO_BARRAS !='CS001'                   
  AND dbo.CUENTAS_PACIENTES.CUE_ESTADO=1                   
  and (dbo.ATENCIONES.ESC_CODIGO = @estadofactura)                  
  AND  dbo.ATENCIONES.ATE_FECHA_INGRESO BETWEEN (@FechaInicio)AND (@FechaFin)                      
  order by dbo.PACIENTES.PAC_APELLIDO_PATERNO, dbo.RUBROS.RUB_ORDEN_IMPRESION asc                      
  end                      
                        
 if(@consulta=2)  --consulta el estado de cuenta de un cliente                    
 begin--ORDER BY  DBO.ATENCIONES.CUE_ESTADO DESC                      
             
 if(@Nombres <>'' or @HC <>'' or @NumeroAtencion <> '' )              
 begin            
                    
     select rank() OVER (ORDER BY q.PROCESOS ,q.atencion DESC) as NUM,                      
     *                      
     from                       
     (                      
                           
    SELECT  'PACIENTE'= dbo.PACIENTES.PAC_APELLIDO_PATERNO                      
    +' ' +dbo.PACIENTES.PAC_APELLIDO_MATERNO+' '+                      
     dbo.PACIENTES.PAC_NOMBRE1+' '+ dbo.PACIENTES.PAC_NOMBRE2,                       
                         
    'HC'=dbo.PACIENTES.PAC_HISTORIA_CLINICA,                       
                         
    'ATENCION'=dbo.ATENCIONES.ATE_NUMERO_ATENCION,                      
                         
    'FECHA INGRESO'=dbo.ATENCIONES.ATE_FECHA_INGRESO,                      
                      
    'FECHA ALTA'=dbo.ATENCIONES.ATE_FECHA_ALTA,                       
                      
     'HABITACION'=dbo.HABITACIONES.hab_Numero,                       
                         
    dbo.ATENCIONES.ESC_CODIGO,                         
                      
    'CONVENIO'= dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE,                      
                      
    'ESTADO CUENTA'= dbo.ESTADOS_CUENTA.ESC_NOMBRE,                      
                      
    dbo.ATENCIONES.ATE_CODIGO,                      
                      
    'TRATAMIENTO'=DBO.TIPO_TRATAMIENTO.TIA_DESCRIPCION,                      
                      
    'PROCESOS'=DBO.ATENCIONES.CUE_ESTADO  ,           
      isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=ATENCIONES.ATE_CODIGO),0) as 'RADICACION',                 
      isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=ATENCIONES.ATE_CODIGO),0) as 'TRAMITE'  ,        
      (select ANI_DESCRIPCION from ANEXOS_IESS where ANEXOS_IESS.ANI_CODIGO=ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_TS) as SEGURO        
    /*,                    
     --,convert(varchar,dbo.ATENCIONES.ATE_CODIGO)as valor                      
     --,'VALOR'=dbo.anexos( dbo.ATENCIONES.ATE_CODIGO,3)                      
     'TOTAL' = (select SUM(CUE_VALOR) from CUENTAS_PACIENTES where  ATENCIONES.ate_codigo=CUENTAS_PACIENTES.ATE_CODIGO )                     
    */                      
    FROM dbo.ATENCIONES                    
    INNER JOIN  dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO                     
    INNER JOIN  dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo                     
    INNER JOIN  dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO                     
    INNER JOIN  dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO                     
    INNER JOIN  dbo.ESTADOS_CUENTA ON dbo.ATENCIONES.ESC_CODIGO = dbo.ESTADOS_CUENTA.ESC_CODIGO                     
    INNER JOIN  dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO                     
    INNER JOIN  dbo.TIPO_TRATAMIENTO on dbo.ATENCIONES.TIA_CODIGO=dbo.TIPO_TRATAMIENTO.TIA_CODIGO                      
                      
    WHERE   (dbo.ATENCIONES.ESC_CODIGO =@estadofactura)                    
    AND  CAST(convert(varchar(11),dbo.ATENCIONES.ATE_FECHA_INGRESO,103)as datetime) BETWEEN (@FechaInicio)AND (@FechaFin)                 
   -- and dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = 13                      
    and dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO =@TipoSeguro /*Para que el cliente pueda escoger el tipo de seguro / Giovanny Tapia / 06/09/2012 */            
    AND ATENCIONES.ATE_CODIGO=  (SELECT  TOP 1 S.ATE_CODIGO  FROM CUENTAS_PACIENTES S WHERE S.ATE_CODIGO=ATENCIONES.ATE_CODIGO)                      
    AND ATENCIONES.ATE_CODIGO=  (SELECT  TOP 1 S.ATE_CODIGO  FROM ATENCIONES_DETALLE_SEGUROS S WHERE S.ATE_CODIGO=ATENCIONES.ATE_CODIGO)                      
                      
    and PACIENTES.PAC_APELLIDO_PATERNO +' ' +PACIENTES.PAC_APELLIDO_MATERNO+' '+PACIENTES.PAC_NOMBRE1+' '+PACIENTES.PAC_NOMBRE2 like '%' + @Nombres + '%'                  
    and pacientes.PAC_HISTORIA_CLINICA like @HC + '%'                    
    and ATENCIONES.ATE_NUMERO_ATENCION like @NumeroAtencion + '%'                   
                    
                    
                         
     ) as Q                      
     group by Q.PACIENTE,Q.HC,Q.ATENCION,Q.[FECHA INGRESO],Q.HABITACION,Q.CONVENIO,Q.[ESTADO CUENTA],Q.TRATAMIENTO,Q.[ATE_CODIGO],Q.PROCESOS                      
    ,Q.ESC_CODIGO,Q.[FECHA ALTA],Q.[RADICACION],Q.[TRAMITE],q.SEGURO                                  
                
 end -- fin de la verificacion de los parametros de busqueda            
             
 else            
 begin            
              
    select rank() OVER (ORDER BY q.PROCESOS ,q.atencion DESC) as NUM,                      
    *                      
    from                       
    (                      
                          
   SELECT  'PACIENTE'= dbo.PACIENTES.PAC_APELLIDO_PATERNO                      
   +' ' +dbo.PACIENTES.PAC_APELLIDO_MATERNO+' '+                      
    dbo.PACIENTES.PAC_NOMBRE1+' '+ dbo.PACIENTES.PAC_NOMBRE2,                       
                        
   'HC'=dbo.PACIENTES.PAC_HISTORIA_CLINICA,                       
                        
   'ATENCION'=dbo.ATENCIONES.ATE_NUMERO_ATENCION,                      
                        
   'FECHA INGRESO'=dbo.ATENCIONES.ATE_FECHA_INGRESO,             
                     
   'FECHA ALTA'=dbo.ATENCIONES.ATE_FECHA_ALTA,                       
                     
    'HABITACION'=dbo.HABITACIONES.hab_Numero,                       
                        
   dbo.ATENCIONES.ESC_CODIGO,                         
                     
   'CONVENIO'= dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE,                      
          
   'ESTADO CUENTA'= dbo.ESTADOS_CUENTA.ESC_NOMBRE,                      
                     
   dbo.ATENCIONES.ATE_CODIGO,                      
                     
   'TRATAMIENTO'=DBO.TIPO_TRATAMIENTO.TIA_DESCRIPCION,                      
                     
   'PROCESOS'=DBO.ATENCIONES.CUE_ESTADO    ,           
      isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=ATENCIONES.ATE_CODIGO),0) as 'RADICACION',                 
      isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=ATENCIONES.ATE_CODIGO),0) as 'TRAMITE'  ,        
      (select ANI_DESCRIPCION from ANEXOS_IESS where ANEXOS_IESS.ANI_CODIGO=ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_TS) as SEGURO        
   /*,                    
    --,convert(varchar,dbo.ATENCIONES.ATE_CODIGO)as valor                      
    --,'VALOR'=dbo.anexos( dbo.ATENCIONES.ATE_CODIGO,3)                      
    'TOTAL' = (select SUM(CUE_VALOR) from CUENTAS_PACIENTES where  ATENCIONES.ate_codigo=CUENTAS_PACIENTES.ATE_CODIGO )                     
   */                      
   FROM dbo.ATENCIONES                    
   INNER JOIN  dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO                     
   INNER JOIN  dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo                     
   INNER JOIN  dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO                     
   INNER JOIN  dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO                     
   INNER JOIN  dbo.ESTADOS_CUENTA ON dbo.ATENCIONES.ESC_CODIGO = dbo.ESTADOS_CUENTA.ESC_CODIGO                     
   INNER JOIN  dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO                     
   INNER JOIN  dbo.TIPO_TRATAMIENTO on dbo.ATENCIONES.TIA_CODIGO=dbo.TIPO_TRATAMIENTO.TIA_CODIGO                      
                     
   WHERE   (dbo.ATENCIONES.ESC_CODIGO =@estadofactura)                    
   AND  CAST(convert(varchar(11),dbo.ATENCIONES.ATE_FECHA_INGRESO,103)as datetime) BETWEEN  (@FechaInicio)AND (@FechaFin)                    
               
   -- and dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = 13                      
               
   and dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO =@TipoSeguro /*Para que el cliente pueda escoger el tipo de seguro / Giovanny Tapia / 06/09/2012 */            
   AND ATENCIONES.ATE_CODIGO=  (SELECT  TOP 1 S.ATE_CODIGO  FROM CUENTAS_PACIENTES S WHERE S.ATE_CODIGO=ATENCIONES.ATE_CODIGO)              
   AND ATENCIONES.ATE_CODIGO=  (SELECT  TOP 1 S.ATE_CODIGO  FROM ATENCIONES_DETALLE_SEGUROS S WHERE S.ATE_CODIGO=ATENCIONES.ATE_CODIGO)                     
    ) as Q                      
    group by Q.PACIENTE,Q.HC,Q.ATENCION,Q.[FECHA INGRESO],Q.HABITACION,Q.CONVENIO,Q.[ESTADO CUENTA],Q.TRATAMIENTO,Q.[ATE_CODIGO],Q.PROCESOS                      
   ,Q.ESC_CODIGO,Q.[FECHA ALTA],Q.[RADICACION],Q.[TRAMITE],q.SEGURO                           
              
 end  -- fin del caso contario de los parametros de busqueda            
                         
    print @estadofactura                    
    print @consulta                       
    --print @atencion                  
 end                      
                       
 if(@consulta=3)                      
 begin                      
 SELECT     'DEPENDENCIA'=dbo.anexos( dbo.ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_DE,1),                       
    dbo.ATENCIONES.ATE_NUMERO_ATENCION  as PLANILLA,                      
      'FECHA INGRESO'=convert(datetime,convert(varchar(10), dbo.ATENCIONES.ATE_FECHA_ALTA ,103)) ,                      
      'TIPO SEGURO'=dbo.anexos( dbo.ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_TS,1),                      
      'IDENTIFICACION'=  dbo.PACIENTES.PAC_IDENTIFICACION,                      
      'PACIENTE'=dbo.PACIENTES.PAC_APELLIDO_PATERNO+' '+ dbo.PACIENTES.PAC_APELLIDO_MATERNO+' '+                      
         dbo.PACIENTES.PAC_NOMBRE1+' '+ dbo.PACIENTES.PAC_NOMBRE2,                      
    'SEXO BENEFICIARIO'= dbo.PACIENTES.PAC_GENERO,                      
    'FECHA NACIMIENTO'=convert (datetime,convert(varchar(10), dbo.PACIENTES.PAC_FECHA_NACIMIENTO,103)),                      
    DATEDIFF(yy,dbo.PACIENTES.PAC_FECHA_NACIMIENTO,dbo.ATENCIONES.ATE_FECHA) as  EDAD ,                      
    'TIPO DE   EXAMEN'=DBO.anexos(DBO.ATENCIONES_DETALLE_SEGUROS.ADA_TIPO_EXAMEN,1),                      
      'CODIGO PROCEDIMIENTO'=dbo.CUENTAS_PACIENTES.PRO_CODIGO_BARRAS,                      
 'DESCRIPCION '=dbo.CUENTAS_PACIENTES.CUE_DETALLE,                       
                              
    dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,1) AS diagnostico_1,                      
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,2) AS diagnostico_2 ,                      
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,3) AS diagnostico_3 ,                      
      'CANTIDAD'= dbo.CUENTAS_PACIENTES.CUE_CANTIDAD,                      
      'VALOR UNITARIO'=  dbo.CUENTAS_PACIENTES.CUE_VALOR_UNITARIO,                       
      'DURACION  CONSULTA'=dbo.CUENTAS_PACIENTES.CUE_VALOR,                      
      'PARENTESCO'=dbo.anexos( dbo.ATENCIONES_DETALLE_SEGUROS.ADS_ASEGURADO_PARENTESCO,1),                       
      'CEDULA ASEGURADO'= dbo.ATENCIONES_DETALLE_SEGUROS.ADS_ASEGURADO_CEDULA,                      
      'NOMBRE ASEGURADO'= dbo.ATENCIONES_DETALLE_SEGUROS.ADS_ASEGURADO_NOMBRE,                      
      'CODIGO DERIVACION'= SUBSTRING(dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION,0,CHARINDEX('-', dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION)),                      
  'NUMERO DERIVACION'=SUBSTRING(dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION,CHARINDEX('-', dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION)+1, LEN(dbo.ATENCION_DETALLE_CATEGORIAS.ADA_AUTORIZACION)),                      
    'CONTINGENCIA'= dbo.anexos(dbo.ATENCIONES_DETALLE_SEGUROS.ADA_CONTINGENCIA,1),                      
    'DIAGNOSTICO P/D'=(SELECT CASE WHEN CA_DESCRIPCION='PRESUNTIVO' THEN 'P'                       
    WHEN CA_DESCRIPCION='DEFINITIVO' THEN 'D' END   FROM HC_CATALOGO_SUBNIVEL WHERE CA_CODIGO=ATENCIONES_DETALLE_SEGUROS.ADA_TIPO_DIAGNOSTICO),                      
                                   
     'TIEMPO ANESTESIA'=dbo.anexos( dbo.CUENTAS_PACIENTES.ATE_CODIGO,2),                       
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,4) AS diagnostico_4 ,                      
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,5) AS diagnostico_5 ,                      
      dbo.diagnosticos(ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,6) AS diagnostico_6 ,                                   
     'PORCENTAJE IVA'= CASE                      
    WHEN dbo.CUENTAS_PACIENTES.RUB_CODIGO =27 Or dbo.CUENTAS_PACIENTES.CUE_IVA>0  THEN 12                      
    WHEN dbo.CUENTAS_PACIENTES.RUB_CODIGO !=27 THEN 0                      
    END,                      
    'IVA'=  DBO.CUENTAS_PACIENTES.CUE_IVA  ,                          
                                   dbo.ATENCIONES.ESC_CODIGO,                      
     -- 'COMVENIO'= dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE,                               
    dbo.ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO,                                         
     'MARCA FINAL'=dbo.ATENCIONES_DETALLE_SEGUROS.ADA_MARCA,                      
     dbo.CUENTAS_PACIENTES.RUB_CODIGO,dbo.RUBROS.RUB_ORDEN_IMPRESION  ,           
      isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=CUENTAS_PACIENTES.ATE_CODIGO),0) as 'RADICACION',                 
      isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=CUENTAS_PACIENTES.ATE_CODIGO),0) as 'TRAMITE'  ,        
      (select ANI_DESCRIPCION from ATENCION_DETALLE_CATEGORIAS,ANEXOS_IESS where ANEXOS_IESS.ANI_CODIGO=ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_TS  and ATENCION_DETALLE_CATEGORIAS.ate_codigo=ATENCIONES.ATE_CODIGO) as SEGURO        
                              
                                 
 FROM      dbo.CUENTAS_PACIENTES INNER JOIN                      
        dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN                      
        dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN                      
        dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN                      
        dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO INNER JOIN                      
        dbo.ATENCIONES_DETALLE_SEGUROS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCIONES_DETALLE_SEGUROS.ATE_CODIGO INNER JOIN                      
        dbo.RUBROS ON dbo.CUENTAS_PACIENTES.RUB_CODIGO = dbo.RUBROS.RUB_CODIGO                      
                                                
 WHERE  dbo.CUENTAS_PACIENTES.PRO_CODIGO_BARRAS !='CS001' AND  dbo.CUENTAS_PACIENTES.CUE_ESTADO=1 and   dbo.ATENCIONES.ATE_NUMERO_ATENCION in(select                        
 CAST(colx.query('data(codigo) ')as varchar)as codigos  from @atencion.nodes('DocumentElement/Atenciones')as TABX(COLX))                      
 order by dbo.PACIENTES.PAC_APELLIDO_PATERNO,dbo.PACIENTES.PAC_APELLIDO_MATERNO, dbo.RUBROS.RUB_ORDEN_IMPRESION   asc                      
 end                      
                       
 if(@consulta=4)                      
 begin                      
 SELECT  'PACIENTE'= dbo.PACIENTES.PAC_APELLIDO_PATERNO                      
 +' ' +dbo.PACIENTES.PAC_APELLIDO_MATERNO+' '+                      
  dbo.PACIENTES.PAC_NOMBRE1+' '+ dbo.PACIENTES.PAC_NOMBRE2,                       
 'HC'=dbo.PACIENTES.PAC_HISTORIA_CLINICA,                       
 'ATENCION'=dbo.ATENCIONES.ATE_NUMERO_ATENCION,                       
 'FECHA INGRESO'=dbo.ATENCIONES.ATE_FECHA_INGRESO,                      
 'FECHA ALTA'=dbo.ATENCIONES.ATE_FECHA_ALTA,                       
  'HABITACION'=dbo.HABITACIONES.hab_Numero ,                       
 'CONVENIO'= dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE,                      
 'ESTADO CUENTA'= dbo.ESTADOS_CUENTA.ESC_NOMBRE,                      
 'TRATAMIENTO'=DBO.TIPO_TRATAMIENTO.TIA_DESCRIPCION ,           
  isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=ATENCIONES.ATE_CODIGO),0) as 'RADICACION',                 
  isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=ATENCIONES.ATE_CODIGO),0) as 'TRAMITE',            
  (select ANI_DESCRIPCION from ANEXOS_IESS where ANEXOS_IESS.ANI_CODIGO=ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_TS) as SEGURO        
                       
 FROM         dbo.ATENCIONES INNER JOIN                      
        dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN                      
        dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo INNER JOIN                      
        dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN                    
        dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO INNER JOIN                      
        dbo.ESTADOS_CUENTA ON dbo.ATENCIONES.ESC_CODIGO = dbo.ESTADOS_CUENTA.ESC_CODIGO INNER JOIN                      
        dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO INNER JOIN                      
     dbo.TIPO_TRATAMIENTO on dbo.ATENCIONES.TIA_CODIGO=dbo.TIPO_TRATAMIENTO.TIA_CODIGO                      
 WHERE    dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = @TipoSeguro and                       
 dbo.ATENCIONES.ATE_NUMERO_ATENCION in(select                        
 CAST(colx.query('data(codigo) ')as varchar)as codigos  from @atencion.nodes('DocumentElement/Atenciones')as TABX(COLX))                      
                       
                       
 ORDER BY  DBO.ATENCIONES.CUE_ESTADO DESC                      
                       
 end                      
                      
 if(@consulta=5)  --consulta el estado de cuenta de un cliente y agrega el total                    
                    
  begin--ORDER BY  DBO.ATENCIONES.CUE_ESTADO DESC                      
   select rank() OVER (ORDER BY q.atencion DESC) as NUM,                      
   *                      
   from                       
   (                      
                         
  SELECT  'PACIENTE'= dbo.PACIENTES.PAC_APELLIDO_PATERNO                      
  +' ' +dbo.PACIENTES.PAC_APELLIDO_MATERNO+' '+                      
   dbo.PACIENTES.PAC_NOMBRE1+' '+ dbo.PACIENTES.PAC_NOMBRE2,                       
                       
  'HC'=dbo.PACIENTES.PAC_HISTORIA_CLINICA,                       
                       
  'ATENCION'=dbo.ATENCIONES.ATE_NUMERO_ATENCION,                      
                       
  'FECHA INGRESO'=dbo.ATENCIONES.ATE_FECHA_INGRESO,                      
                    
  'FECHA ALTA'=dbo.ATENCIONES.ATE_FECHA_ALTA,                       
                    
   'HABITACION'=dbo.HABITACIONES.hab_Numero,                       
                       
  --dbo.ATENCIONES.ESC_CODIGO,                         
                    
  'CONVENIO'= dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE,                      
                    
  'ESTADO CUENTA'= dbo.ESTADOS_CUENTA.ESC_NOMBRE,                      
                    
  dbo.ATENCIONES.ATE_CODIGO,                      
                    
  'TRATAMIENTO'=DBO.TIPO_TRATAMIENTO.TIA_DESCRIPCION,                      
                    
  --'PROCESOS'=DBO.ATENCIONES.CUE_ESTADO  ,                    
                    
  --,convert(varchar,dbo.ATENCIONES.ATE_CODIGO)as valor                      
  --,'VALOR'=dbo.anexos( dbo.ATENCIONES.ATE_CODIGO,3)                      
                    
   'VALOR CUENTA' = (select SUM(CUE_VALOR) from CUENTAS_PACIENTES where  ATENCIONES.ate_codigo=CUENTAS_PACIENTES.ATE_CODIGO ),           
   isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=ATENCIONES.ATE_CODIGO),0) as 'RADICACION',                 
   isnull((select top 1 NumeroRadicacionPaquete from PaqueteCuentas where PaqueteCuentas.ate_codigo=ATENCIONES.ATE_CODIGO),0) as 'TRAMITE'  ,        
   (select ANI_DESCRIPCION from ANEXOS_IESS where ANEXOS_IESS.ANI_CODIGO=ATENCION_DETALLE_CATEGORIAS.HCC_CODIGO_TS) as SEGURO        
                        
  FROM dbo.ATENCIONES                    
  INNER JOIN  dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO                     
  INNER JOIN  dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo                     
  INNER JOIN  dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO                     
  INNER JOIN  dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO                     
  INNER JOIN  dbo.ESTADOS_CUENTA ON dbo.ATENCIONES.ESC_CODIGO = dbo.ESTADOS_CUENTA.ESC_CODIGO                     
  INNER JOIN  dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO                     
  INNER JOIN  dbo.TIPO_TRATAMIENTO on dbo.ATENCIONES.TIA_CODIGO=dbo.TIPO_TRATAMIENTO.TIA_CODIGO                      
                    
  WHERE   (dbo.ATENCIONES.ESC_CODIGO =@estadofactura)                    
  AND  CAST(convert(varchar(11),dbo.ATENCIONES.ATE_FECHA_INGRESO,103)as datetime) BETWEEN  (@FechaInicio)AND (@FechaFin)                    
  and dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO = @TipoSeguro                      
  AND ATENCIONES.ATE_CODIGO=  (SELECT  TOP 1 S.ATE_CODIGO  FROM CUENTAS_PACIENTES S WHERE S.ATE_CODIGO=ATENCIONES.ATE_CODIGO)                      
  AND ATENCIONES.ATE_CODIGO=  (SELECT  TOP 1 S.ATE_CODIGO  FROM ATENCIONES_DETALLE_SEGUROS S WHERE S.ATE_CODIGO=ATENCIONES.ATE_CODIGO)                      
                        
   ) as Q                      
   group by Q.PACIENTE,Q.HC,Q.ATENCION,Q.[FECHA INGRESO],Q.HABITACION,Q.CONVENIO,Q.[ESTADO CUENTA],Q.TRATAMIENTO,Q.[ATE_CODIGO]/*,Q.PROCESOS                      
  ,Q.ESC_CODIGO*/,Q.[FECHA ALTA]  ,Q.[Valor Cuenta]  ,Q.[RADICACION],Q.[TRAMITE] ,q.SEGURO                    
                        
    print @estadofactura                    
    print @consulta                       
    --print @atencion                     
                        
  end                      
                    
END 

----------------------------------------------------------------------
ALTER procedure sp_mostrarcongregaciones      
  @tipo int      
  as      
  if(@tipo =0)      
  begin      
   select ase_nombre as CONGREGACION, ase_ruc as RUC,     
  ase_direccion as DIRECCION, ase_ciudad as CIUDAD, ase_telefono as TELEFONO  
  --ISNULL((SELECT email FROM SIC3000..Cliente WHERE ASE_RUC = ruccli), '') AS EMAIL  
  , ISNULL(C.email, (SELECT Texto FROM Sic3000..ParametrosFactura WHERE codpar = 114)) AS EMAIL
  from his3000..aseguradoras_empresas A
  INNER JOIN Sic3000..Cliente C ON A.ASE_RUC = C.ruccli
  where te_codigo = 6  
  end      
  else      
  begin      
  select ase_nombre as CONGREGACION, ase_ruc as RUC,       
  ase_direccion as DIRECCION, ase_ciudad as CIUDAD, ase_telefono as TELEFONO,  
   ISNULL((SELECT email FROM SIC3000..Cliente WHERE ASE_RUC = ruccli), '') AS EMAIL  
  from his3000..aseguradoras_empresas where te_codigo = 1      
  end 