using Core.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace His.Datos
{
    public class DatDietetica
    {



        public DataTable getDataTable(string tabla, string codigo = "0", string codigo2 = "0", string[] values = null, string codigo3 = "0")
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();


            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string query = "";
            switch (tabla)
            {
                case "GetEstudiosAgendamientoImagen":
                    query = ("select * from HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS where id_imagenologia_agendamientos=" + codigo + " ");
                    break;

                case "KardexInsumos_CuentasProdsPorIngresar":
                    query = ("	DECLARE @ate_codigo VARCHAR(10)\n" +
                        "SET @ate_codigo = " + codigo + " \n" +
                        "SELECT CUENTAS_PACIENTES.ATE_CODIGO, PRO_CODIGO, CUE_CODIGO, CUE_DETALLE AS PRO_DESCRIPCION, cast(CUE_CANTIDAD as int) AS CANTIDAD FROM CUENTAS_PACIENTES WHERE CUE_CODIGO IN( SELECT CUENTA.CUE_CODIGO FROM (select CUENTAS_PACIENTES.ATE_CODIGO, PRO_CODIGO, CUE_CODIGO, CUE_DETALLE AS PRO_DESCRIPCION, cast(CUE_CANTIDAD as int) AS CANTIDAD\n" +
                        "from CUENTAS_PACIENTES where RUB_CODIGO = 27 and ATE_CODIGO = @ate_codigo) CUENTA WHERE CUE_CODIGO IN(SELECT DISTINCT CUE_CODIGO FROM KARDEX_INSUMOS where ATE_CODIGO = @ate_codigo AND CUE_CODIGO <> 0) )");
                    break;
                case "KardexInsumos":
                    query = ("SELECT dbo.KARDEXINSUMOS.*, CONCAT(dbo.USUARIOS.NOMBRES,' ',dbo.USUARIOS.APELLIDOS) as USUARIO " +
                            "FROM dbo.KARDEXINSUMOS " +
                            "left JOIN dbo.USUARIOS ON dbo.KARDEXINSUMOS.IdUsuario = dbo.USUARIOS.ID_USUARIO where AteCodigo = " + codigo + " ");
                    break;
                case "KardexGrid":
                    query = ("SELECT id,ATE_CODIGO,CUE_CODIGO,PRO_CODIGO,PRO_DESCRIPCION,Administrado,NoAdministrado,fecha,observacion,k.ID_USUARIO,u.APELLIDOS+' '+u.NOMBRES as USUARIO from KARDEX_INSUMOS k left join USUARIOS u on k.ID_USUARIO = u.ID_USUARIO WHERE ATE_CODIGO = " + codigo + "order by CUE_CODIGO desc ");
                    //query = ("SELECT id,ATE_CODIGO,CUE_CODIGO,PRO_CODIGO,PRO_DESCRIPCION,Administrado,NoAdministrado,fecha,observacion,k.ID_USUARIO,u.APELLIDOS+' '+u.NOMBRES as USUARIO from KARDEX_INSUMOS k left join USUARIOS u on k.ID_USUARIO = u.ID_USUARIO WHERE ATE_CODIGO = " + codigo + " ");
                    break;
                case "EscCodigo_byAteCodigo":
                    query = ("select ESC_CODIGO from ATENCIONES where ATE_CODIGO=" + codigo + " ");
                    break;

                case "Form022_Insumos":
                    query = ("update KARDEX_INSUMOS set observacion = '' where NoAdministrado=0  select  distinct PRO_DESCRIPCION from KARDEX_INSUMOS where (Administrado=1 or NoAdministrado=1) and ATE_CODIGO = " + codigo + " ");
                    break;
                case "Form022_IFechas":
                    query = ("select  distinct CONVERT(VARCHAR(10), fecha, 103) as Fecha from KARDEX_INSUMOS where (Administrado=1 or NoAdministrado=1) and ATE_CODIGO = " + codigo + " ");
                    break;
                case "Form022_IRegistros":
                    query = ("select fecha, PRO_DESCRIPCION,observacion, observacion, NOMBRES,APELLIDOS,DEP_NOMBRE ,Administrado, NoAdministrado, count (*) as CANTIDAD\n" +
                            "from\n" +
                            "    (select  CONVERT(VARCHAR(10), fecha, 103) as fecha,KARDEX_INSUMOS.observacion, PRO_DESCRIPCION, USUARIOS.NOMBRES, USUARIOS.APELLIDOS, DEPARTAMENTOS.DEP_NOMBRE, Administrado, NoAdministrado\n" +
                            "    from KARDEX_INSUMOS\n" +
                            "    left JOIN dbo.USUARIOS ON dbo.KARDEX_INSUMOS.ID_USUARIO = dbo.USUARIOS.ID_USUARIO  INNER JOIN dbo.DEPARTAMENTOS ON dbo.USUARIOS.DEP_CODIGO = dbo.DEPARTAMENTOS.DEP_CODIGO\n" +
                            "    where(Administrado = 1 or NoAdministrado = 1) AND ATE_CODIGO = " + codigo + ") ii \n" +
                       "     where fecha = '" + values[0] + "' and PRO_DESCRIPCION = '" + values[1] + "'\n" +
                    "    group by fecha, PRO_DESCRIPCION, NOMBRES, APELLIDOS, DEP_NOMBRE, Administrado,observacion, NoAdministrado ");
                    break;

                case "Form022_Medicamentos":
                    query = ("select  distinct CueCodigo, Presentacion,dosis, via,Frecuencia from KARDEXMEDICAMENTOS where atecodigo = " + codigo + " ");
                    break;
                case "Form022_Fechas":
                    query = ("select  distinct FechaAdministración from KARDEXMEDICAMENTOS where atecodigo = " + codigo + " ");
                    break;
                case "Form022_Registros":
                    query = ("select KARDEXMEDICAMENTOS.Hora, dbo.USUARIOS.NOMBRES,dbo.USUARIOS.APELLIDOS, DEPARTAMENTOS.DEP_NOMBRE, KARDEXMEDICAMENTOS.NoAdministrado,KARDEXMEDICAMENTOS.Administrado,KARDEXMEDICAMENTOS.Observacion   FROM dbo.KARDEXMEDICAMENTOS left JOIN dbo.USUARIOS ON dbo.KARDEXMEDICAMENTOS.IDUSUARIO = dbo.USUARIOS.ID_USUARIO  INNER JOIN dbo.DEPARTAMENTOS ON dbo.USUARIOS.DEP_CODIGO = dbo.DEPARTAMENTOS.DEP_CODIGO\n" +
                         "where atecodigo = " + codigo + " and FechaAdministración = '" + values[0] + "' and Presentacion = '" + values[1] + "'\n" +
                         "   and dosis = '" + values[2] + "' and via = '" + values[3] + "' and Frecuencia = '" + values[4] + "'");
                    break;



                case "GetUSERdepartamento":
                    query = ("SELECT TOP (1) dbo.DEPARTAMENTOS.DEP_NOMBRE FROM dbo.USUARIOS INNER JOIN dbo.DEPARTAMENTOS ON dbo.USUARIOS.DEP_CODIGO = dbo.DEPARTAMENTOS.DEP_CODIGO \n" +
                              "WHERE dbo.USUARIOS.ID_USUARIO = " + codigo + " ");
                    break;

                case "KardexMedicamentos":
                    query = ("SELECT       dbo.EMPRESA.EMP_NOMBRE, dbo.PACIENTES.PAC_NOMBRE1, dbo.PACIENTES.PAC_NOMBRE2, dbo.PACIENTES.PAC_APELLIDO_PATERNO, dbo.PACIENTES.PAC_APELLIDO_MATERNO, dbo.PACIENTES.PAC_GENERO, \n" +
                    "     dbo.PACIENTES.PAC_HISTORIA_CLINICA, dbo.KARDEXMEDICAMENTOS.Presentacion, dbo.KARDEXMEDICAMENTOS.Via, dbo.KARDEXMEDICAMENTOS.Dosis, dbo.KARDEXMEDICAMENTOS.Frecuencia, \n" +
                    "     dbo.KARDEXMEDICAMENTOS.FechaAdministración, dbo.KARDEXMEDICAMENTOS.Hora, dbo.USUARIOS.NOMBRES, dbo.USUARIOS.APELLIDOS, dbo.DEPARTAMENTOS.DEP_NOMBRE\n" +
                    "FROM            dbo.PACIENTES INNER JOIN dbo.ATENCIONES ON dbo.PACIENTES.PAC_CODIGO = dbo.ATENCIONES.PAC_CODIGO INNER JOIN dbo.KARDEXMEDICAMENTOS ON dbo.ATENCIONES.ATE_CODIGO = dbo.KARDEXMEDICAMENTOS.AteCodigo INNER JOIN\n" +
                    "     dbo.USUARIOS ON dbo.KARDEXMEDICAMENTOS.IdUsuario = dbo.USUARIOS.ID_USUARIO INNER JOIN dbo.DEPARTAMENTOS ON dbo.USUARIOS.DEP_CODIGO = dbo.DEPARTAMENTOS.DEP_CODIGO CROSS JOIN dbo.EMPRESA\n" +
                    "WHERE        dbo.ATENCIONES.ATE_CODIGO = " + codigo + " ");
                    break;

                case "Fallecido":
                    query = ("SELECT dbo.PACIENTES_DATOS_ADICIONALES2.FALLECIDO, dbo.PACIENTES.PAC_CODIGO FROM dbo.PACIENTES INNER JOIN dbo.PACIENTES_DATOS_ADICIONALES2 ON dbo.PACIENTES.PAC_CODIGO = dbo.PACIENTES_DATOS_ADICIONALES2.PAC_CODIGO\n" +
                        "WHERE dbo.PACIENTES_DATOS_ADICIONALES2.FALLECIDO = 1 AND dbo.PACIENTES.PAC_CODIGO =  " + codigo + " ");
                    break;
                case "EnHabitacion":
                    query = ("SELECT count(dbo.PACIENTES.PAC_HISTORIA_CLINICA) AS HC\n" +
                        "FROM dbo.TIPO_REFERIDO INNER JOIN dbo.HABITACIONES_ESTADO INNER JOIN dbo.HABITACIONES ON dbo.HABITACIONES_ESTADO.HES_CODIGO = dbo.HABITACIONES.HES_CODIGO INNER JOIN dbo.ATENCIONES ON dbo.HABITACIONES.hab_Codigo = dbo.ATENCIONES.HAB_CODIGO INNER JOIN\n" +
                        "    dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN dbo.MEDICOS ON dbo.ATENCIONES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO ON dbo.TIPO_REFERIDO.TIR_CODIGO = dbo.ATENCIONES.TIR_CODIGO LEFT OUTER JOIN dbo.CATEGORIAS_CONVENIOS INNER JOIN\n" +
                        "    dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO\n" +
                        "WHERE(dbo.HABITACIONES_ESTADO.HES_ACTIVO = 1) AND(dbo.ATENCIONES.ESC_CODIGO = 1) AND(dbo.HABITACIONES.HES_CODIGO = 1) and dbo.PACIENTES.PAC_HISTORIA_CLINICA = '" + codigo + "' ");
                    break;
                case "AtencionesServiciosExternos":
                    query = ("SELECT      dbo.ATENCIONES.ATE_NUMERO_ATENCION as NUMERO_ATE, dbo.ATENCIONES.ATE_CODIGO, dbo.PACIENTES.PAC_HISTORIA_CLINICA AS HC, CONCAT( dbo.PACIENTES.PAC_APELLIDO_PATERNO ,' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO,' ' , dbo.PACIENTES.PAC_NOMBRE1,' ' , \n" +
                             "        dbo.PACIENTES.PAC_NOMBRE2 ) AS PACIENTE, dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE, dbo.ATENCIONES.ATE_FECHA_INGRESO, dbo.ATENCIONES.ATE_FECHA_ALTA, dbo.HABITACIONES.hab_Numero, \n" +
                             "        dbo.tipos_atenciones.name AS TIPO_ATENCION\n" +
                             "   FROM            dbo.HABITACIONES INNER JOIN\n" +
                             "        dbo.PACIENTES INNER JOIN\n" +
                             "        dbo.ATENCIONES ON dbo.PACIENTES.PAC_CODIGO = dbo.ATENCIONES.PAC_CODIGO \n" +
                             "        ON dbo.HABITACIONES.hab_Codigo = dbo.ATENCIONES.HAB_CODIGO INNER JOIN\n" +
                             "        dbo.tipos_atenciones ON dbo.ATENCIONES.TipoAtencion = dbo.tipos_atenciones.id LEFT OUTER JOIN\n" +
                             "        dbo.CATEGORIAS_CONVENIOS INNER JOIN\n" +
                             "        dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO ON\n" +
                             "        dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO\n" +
                             "   WHERE(dbo.tipos_atenciones.list = '8') AND(dbo.ATENCIONES.ESC_CODIGO = 1)  and dbo.HABITACIONES.hab_Numero='PROCE'");
                    break;
                case "DetalleEvolucionEnfermeria":
                    query = ("SELECT       dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_CODIGO, dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.ID_USUARIO , (dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.NOM_USUARIO) as USUARIO, (dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_FECHA) as FECHA_REPORTE, (dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_FECHA_INSERT) AS FECHA_SISTEMA,\n" +
                      "   dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_DESCRIPCION as NOTA ,(SELECT dbo.USUARIOS_FIRMA.URL FROM dbo.USUARIOS_FIRMA WHERE dbo.USUARIOS_FIRMA.ID_USUARIO = dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.ID_USUARIO) AS URL \n" +
                    "FROM            dbo.HC_EVOLUCION_ENFERMERA INNER JOIN\n" +
                     "    dbo.HC_EVOLUCION_ENFERMERIA_DETALLE ON dbo.HC_EVOLUCION_ENFERMERA.EVO_CODIGO = dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVO_CODIGO\n" +
                "WHERE  ESTADO=1 AND  dbo.HC_EVOLUCION_ENFERMERA.ATE_CODIGO = " + codigo + " order by dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_FECHA asc ");
                    //    query = ("SELECT       dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_CODIGO, dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.ID_USUARIO , (dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.NOM_USUARIO) as USUARIO, (dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_FECHA) as FECHA_REPORTE, (dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_FECHA_INSERT) AS FECHA_SISTEMA,\n" +
                    //      "   dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_DESCRIPCION as NOTA\n" +
                    //    "FROM            dbo.HC_EVOLUCION_ENFERMERA INNER JOIN\n" +
                    //     "    dbo.HC_EVOLUCION_ENFERMERIA_DETALLE ON dbo.HC_EVOLUCION_ENFERMERA.EVO_CODIGO = dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVO_CODIGO\n" +
                    //"WHERE  ESTADO=1 AND  dbo.HC_EVOLUCION_ENFERMERA.ATE_CODIGO = " + codigo + " order by dbo.HC_EVOLUCION_ENFERMERIA_DETALLE.EVD_FECHA asc ");



                    break;
                case "HMGeneradoAsiento":
                    query = ("INSERT INTO His3000.dbo.HONORARIOS_MEDICOS_DATOSADICIONALES (HOM_CODIGO, FEC_CAD_FACTURA) \n" +
                        "SELECT HOM_CODIGO, HOM_FACTURA_FECHA FROM His3000..HONORARIOS_MEDICOS WHERE HOM_CODIGO NOT IN(SELECT HOM_CODIGO FROM His3000.dbo.HONORARIOS_MEDICOS_DATOSADICIONALES)\n" +
                        "select GENERADO_ASIENTO from His3000..HONORARIOS_MEDICOS_DATOSADICIONALES where HOM_CODIGO=" + codigo + " ");
                    break;
                case "getCgCueCliente":
                    query = ("SELECT CG3000.dbo.Cgcodcon.codigo_c FROM dbo.MEDICOS INNER JOIN    CG3000.dbo.Cgcodcon ON dbo.MEDICOS.MED_RUC = CG3000.dbo.Cgcodcon.campo4 " +
                                "WHERE MED_CODIGO = " + codigo + " ");
                    break;
                case "EMPRESA":
                    query = ("SELECT * FROM EMPRESA ");
                    break;

                case "Paciente_Convenio":
                    query = ("SELECT       dbo.TIPO_REFERIDO.TIR_CODIGO AS id_referido, dbo.TIPO_REFERIDO.TIR_NOMBRE AS referido, dbo.tipos_atenciones.id AS id_tipo_atencion, dbo.tipos_atenciones.name AS tipo_atencion, \n" +
                                " dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO AS id_convenio, dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE AS convenio, dbo.TIPO_EMPRESA.TE_CODIGO AS tipo_convenio, \n" +
                                " dbo.TIPO_EMPRESA.TE_DESCRIPCION AS id_tipo_convenio\n" +
                            "FROM dbo.CATEGORIAS_CONVENIOS INNER JOIN\n" +
                                " dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO INNER JOIN\n" +
                                " dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO INNER JOIN\n" +
                                " dbo.TIPO_EMPRESA ON dbo.ASEGURADORAS_EMPRESAS.TE_CODIGO = dbo.TIPO_EMPRESA.TE_CODIGO RIGHT OUTER JOIN\n" +
                                " dbo.ATENCIONES ON dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO LEFT OUTER JOIN\n" +
                                " dbo.tipos_atenciones ON dbo.ATENCIONES.TipoAtencion = dbo.tipos_atenciones.id LEFT OUTER JOIN\n" +
                                " dbo.TIPO_REFERIDO ON dbo.ATENCIONES.TIR_CODIGO = dbo.TIPO_REFERIDO.TIR_CODIGO\n" +
                                "WHERE dbo.ATENCIONES.ATE_CODIGO = " + codigo + " ");
                    break;


                case "getNumeroControlAsiento":
                    query = ("INSERT INTO His3000.dbo.HONORARIOS_MEDICOS_DATOSADICIONALES (HOM_CODIGO, FEC_CAD_FACTURA) \n" +
                        "SELECT HOM_CODIGO, HOM_FACTURA_FECHA FROM His3000..HONORARIOS_MEDICOS WHERE HOM_CODIGO NOT IN(SELECT HOM_CODIGO FROM His3000.dbo.HONORARIOS_MEDICOS_DATOSADICIONALES)\n" +
                        "update His3000.dbo.HONORARIOS_MEDICOS_DATOSADICIONALES set GENERADO_ASIENTO=1 where HOM_CODIGO=" + codigo + " \n" +
                        "update sic3000..Numero_Control set numcon=(select TOP 1 numcon+1 from sic3000..Numero_Control where modcon like '%ASIENTO%') where modcon like '%ASIENTO%'\n" +
                        "select TOP 1 numcon-1 from sic3000..Numero_Control where modcon like '%ASIENTO%'");
                    break;
                case "GetEgreso_DatosPaciente":
                    query = ("SELECT TOP 1 dbo.PACIENTES.PAC_HISTORIA_CLINICA, concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO,' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO,' ', dbo.PACIENTES.PAC_NOMBRE1,' ', dbo.PACIENTES.PAC_NOMBRE2) as PACIENTE, \n" +
                        " dbo.PACIENTES.PAC_IDENTIFICACION, dbo.PACIENTES.PAC_FECHA_NACIMIENTO, dbo.HABITACIONES.hab_Numero, dbo.ATENCIONES.ATE_FECHA_INGRESO, dbo.ATENCIONES.ATE_CODIGO, \n" +
                        " dbo.ATENCIONES.ATE_NUMERO_ATENCION,  concat(dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_APELLIDO_MATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1, ' ', dbo.MEDICOS.MED_NOMBRE2) as MEDICO, \n" +
                        " dbo.TIPO_INGRESO.TIP_DESCRIPCION, dbo.TIPO_TRATAMIENTO.TIA_DESCRIPCION, dbo.ATENCIONES.ATE_DIAGNOSTICO_INICIAL, ISNULL((select top 1 HED_DESCRIPCION from HC_EPICRISIS HE INNER JOIN HC_EPICRISIS_DIAGNOSTICO HED ON HE.EPI_CODIGO = HED.EPI_CODIGO where ATE_CODIGO = " + codigo + " AND HED_TIPO = 'E') , '') AS ATE_DIAGNOSTICO_FINAL, CONCAT(dbo.USUARIOS.APELLIDOS, ' ', dbo.USUARIOS.NOMBRES) AS USUARIO, dbo.TIPO_REFERIDO.TIR_NOMBRE AS REFERIDO,  ISNULL((dbo.MEDICOS_ALTA.FECHA_ALTA), GETDATE()) AS FECHA_ALTA\n" +
                        "FROM dbo.PACIENTES INNER JOIN\n" +
                        " dbo.ATENCIONES ON dbo.PACIENTES.PAC_CODIGO = dbo.ATENCIONES.PAC_CODIGO INNER JOIN\n" +
                        " dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo INNER JOIN\n" +
                        " dbo.MEDICOS ON dbo.ATENCIONES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO INNER JOIN\n" +
                        " dbo.TIPO_TRATAMIENTO ON dbo.ATENCIONES.TIA_CODIGO = dbo.TIPO_TRATAMIENTO.TIA_CODIGO INNER JOIN\n" +
                        " dbo.TIPO_INGRESO ON dbo.ATENCIONES.TIP_CODIGO = dbo.TIPO_INGRESO.TIP_CODIGO INNER JOIN dbo.USUARIOS ON dbo.PACIENTES.ID_USUARIO = dbo.USUARIOS.ID_USUARIO\n" +
                        " INNER JOIN dbo.TIPO_REFERIDO ON dbo.ATENCIONES.TIR_CODIGO = dbo.TIPO_REFERIDO.TIR_CODIGO left JOIN dbo.MEDICOS_ALTA on dbo.ATENCIONES.ATE_CODIGO = dbo.MEDICOS_ALTA.ATE_CODIGO  \n" +
                        "WHERE dbo.ATENCIONES.ATE_CODIGO = " + codigo + " order by FECHA_ALTA DESC");
                    break;
                case "getHMCtasPctes":
                    query = ("SELECT  dbo.CUENTAS_PACIENTES.MED_CODIGO, CONCAT(dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_APELLIDO_MATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1, ' ',dbo.MEDICOS.MED_NOMBRE2) AS MEDICO,dbo.CUENTAS_PACIENTES.CUE_FECHA AS FECHA, dbo.CUENTAS_PACIENTES.CUE_VALOR AS VALOR,\n" +
                             "  dbo.CUENTAS_PACIENTES.NumVale AS FACT_MEDICO,(SELECT    TOP(1) dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE  FROM dbo.ATENCIONES a INNER JOIN dbo.ATENCION_DETALLE_CATEGORIAS ON a.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN dbo.CATEGORIAS_CONVENIOS\n" +
                             "  ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO WHERE a.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO) as SEGURO, dbo.USUARIOS.APELLIDOS + ' ' + dbo.USUARIOS.NOMBRES as USUARIO, dbo.CUENTAS_PACIENTES.ID_USUARIO\n" +
                            "FROM dbo.CUENTAS_PACIENTES INNER JOIN dbo.PRODUCTO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO INNER JOIN dbo.MEDICOS ON dbo.CUENTAS_PACIENTES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO INNER JOIN dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN dbo.USUARIOS ON dbo.CUENTAS_PACIENTES.ID_USUARIO = dbo.USUARIOS.ID_USUARIO\n" +
                            "WHERE(dbo.PRODUCTO.PRO_DESCRIPCION LIKE '%HONORARIO%') AND  dbo.CUENTAS_PACIENTES.NumVale NOT IN " + codigo2 + " AND  dbo.CUENTAS_PACIENTES.NumVale NOT IN " + codigo3 + " AND dbo.ATENCIONES.ATE_CODIGO = " + codigo + " ");
                    break;
                case "getLastTurnoDietetica":
                    query = ("SELECT dbo.PACIENTES.PAC_HISTORIA_CLINICA as HC, dbo.ATENCIONES.ATE_CODIGO as ATE_COD, concat('[', dbo.PACIENTES.PAC_IDENTIFICACION, '] ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2, ' ', dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO) as PACIENTE,  dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.fecha_agendamiento as FECHA, \n" +
                    "concat(dbo.MEDICOS.MED_NOMBRE1, ' ', dbo.MEDICOS.MED_APELLIDO_PATERNO) as TECNOLOGO, concat(MEDICOS_1.MED_NOMBRE1, ' ', MEDICOS_1.MED_APELLIDO_PATERNO) AS RAIOLOGO, concat(dbo.USUARIOS.NOMBRES, ' ', dbo.USUARIOS.APELLIDOS) as USUARIO\n" +
                    "FROM dbo.ATENCIONES INNER JOIN dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS ON dbo.ATENCIONES.ATE_CODIGO = dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ATE_CODIGO INNER JOIN dbo.USUARIOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.ID_USUARIO = dbo.USUARIOS.ID_USUARIO LEFT OUTER JOIN dbo.MEDICOS AS MEDICOS_1 ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_radiologo = MEDICOS_1.MED_CODIGO LEFT OUTER JOIN\n" +
                    "dbo.MEDICOS ON dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.med_tecnologo = dbo.MEDICOS.MED_CODIGO\n" +
                    "where dbo.HC_IMAGENOLOGIA_AGENDAMIENTOS.id = (select max(id) from[HC_IMAGENOLOGIA_AGENDAMIENTOS] where ATE_CODIGO = " + codigo + ")");
                    break;
                case "GetPreciosConvenio":
                    query = ("SELECT dbo.ASEGURADORAS_EMPRESAS.ASE_NOMBRE, dbo.ASEGURADORAS_EMPRESAS.ASE_RUC, dbo.ASEGURADORAS_EMPRESAS.ASE_DIRECCION, dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE, \n" +
                            "dbo.PRECIOS_POR_CONVENIOS.PRE_VALOR, dbo.PRECIOS_POR_CONVENIOS.PRE_PORCENTAJE, dbo.CATALOGO_COSTOS.CAC_NOMBRE, dbo.CATALOGO_COSTOS_TIPO.CCT_NOMBRE\n" +
                            "FROM dbo.CATALOGO_COSTOS_TIPO INNER JOIN dbo.CATALOGO_COSTOS ON dbo.CATALOGO_COSTOS_TIPO.CCT_CODIGO = dbo.CATALOGO_COSTOS.CCT_CODIGO INNER JOIN\n" +
                            "dbo.PRECIOS_POR_CONVENIOS ON dbo.CATALOGO_COSTOS.CAC_CODIGO = dbo.PRECIOS_POR_CONVENIOS.CAC_CODIGO INNER JOIN dbo.CATEGORIAS_CONVENIOS INNER JOIN\n" +
                            "dbo.ASEGURADORAS_EMPRESAS ON dbo.CATEGORIAS_CONVENIOS.ASE_CODIGO = dbo.ASEGURADORAS_EMPRESAS.ASE_CODIGO ON dbo.PRECIOS_POR_CONVENIOS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO\n" +
                            "where CATEGORIAS_CONVENIOS.CAT_CODIGO = " + codigo + " ");
                    break;
                case "GetHorarioMedicos":
                    query = ("SELECT dbo.HORARIO_MEDICOS.Id, dbo.HORARIO_MEDICOS.INGRESO, dbo.HORARIO_MEDICOS.SALIDA, dbo.HORARIO_MEDICOS.MED_CODIGO,concat(dbo.MEDICOS.MED_NOMBRE1,' ',dbo.MEDICOS.MED_NOMBRE2,' ',dbo.MEDICOS.MED_APELLIDO_PATERNO,' ',dbo.MEDICOS.MED_APELLIDO_MATERNO) as MEDICO, dbo.ESPECIALIDADES_MEDICAS.ESP_NOMBRE\n" +
                            "FROM dbo.HORARIO_MEDICOS INNER JOIN dbo.MEDICOS ON dbo.HORARIO_MEDICOS.MED_CODIGO = dbo.MEDICOS.MED_CODIGO INNER JOIN dbo.ESPECIALIDADES_MEDICAS ON dbo.MEDICOS.ESP_CODIGO = dbo.ESPECIALIDADES_MEDICAS.ESP_CODIGO\n" +
                            "where dbo.HORARIO_MEDICOS.INGRESO between '" + codigo + "' and '" + codigo2 + "'");
                    break;
                case "HorariosMedicosAsientos":
                    if (codigo3 == "1")
                    {
                        query = ("SELECT 'false' as Seleccion, dbo.PACIENTES.PAC_HISTORIA_CLINICA as HC, dbo.ATENCIONES.ATE_CODIGO, \n" +
                       "     concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO, ' ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2) as paciente, dbo.ATENCIONES.ATE_FACTURA_PACIENTE, \n" +
                       "     dbo.MEDICOS.MED_CODIGO,\n" +
                      "           CONCAT(dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_APELLIDO_MATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1, ' ',dbo.MEDICOS.MED_NOMBRE2) AS MEDICO,\n" +
                     "         dbo.HONORARIOS_MEDICOS.HOM_CODIGO, dbo.HONORARIOS_MEDICOS.HOM_FACTURA_FECHA AS FECHA_FACTURA_MED, dbo.HONORARIOS_MEDICOS.HOM_FACTURA_MEDICO as FACTURA,dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.AUTORIZACION_SRI AS AUTORIZACION,dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.FEC_CAD_FACTURA AS CADUCIDAD, \n" +
                    "            dbo.HONORARIOS_MEDICOS.HOM_VALOR_NETO as VALOR,\n" +
                   "              dbo.HONORARIOS_MEDICOS.HOM_COMISION_CLINICA AS COMISION,\n" +
                  "                dbo.HONORARIOS_MEDICOS.HOM_APORTE_LLAMADA AS APORTE,\n" +
                   "                dbo.HONORARIOS_MEDICOS.HOM_RETENCION AS RETENCION, dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_CUBIERTO AS VALOR_CUBIERTO, dbo.HONORARIOS_MEDICOS.HOM_RECORTE as RECORTE, dbo.RETENCIONES_FUENTE.RET_REFERENCIA as COD_RET," +
 "                  dbo.RETENCIONES_FUENTE.COD_CUE AS CTA_RETENCION ,\n" +
                   "CASE\n" +
                        "WHEN dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA = 1 THEN(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR FUERA')\n" +
                        "WHEN dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA = 'true' THEN(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR FUERA')\n" +
                        "ELSE(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR DENTRO')\n" +
                    "END AS CTA_HONORARIOS\n" +
                    ", dbo.MEDICOS.MED_CUENTA_CONTABLE AS CTA_MEDICO,(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'APORTE LLAMADAS') AS CTA_APORTE\n" +
                    ",(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'COMISION') AS CTA_COMISION\n" +
                  ",              dbo.HONORARIOS_MEDICOS.HOM_POR_PAGAR AS A_PAGAR, ISNULL(dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.GENERADO_ASIENTO, 0) AS GENERADO,(select concat(u.NOMBRES,' ',u.APELLIDOS) as USUARIO from USUARIOS u where ID_USUARIO=dbo.HONORARIOS_MEDICOS.ID_USUARIO) AS USUARIO\n" +
        "          ,  dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA AS HON_X_FUERA,'SEGUROS' = CONVERT(BIT, CASE WHEN (SELECT C.codclas FROM Sic3000..Clasificacion C INNER JOIN Sic3000..Forma_Pago FP ON C.codclas = FP.claspag WHERE FORMA_PAGO.forpag = FP.forpag) = 7 THEN 1 else 0 end), dbo.FORMA_PAGO.forpag, 		  'FORMA PAGO' = (SELECT despag FROM Sic3000..Forma_Pago WHERE Sic3000..Forma_Pago.forpag = dbo.FORMA_PAGO.forpag), dbo.FORMA_PAGO.FOR_DESCRIPCION as 'CORRIENTE / DIFERIDO'\n" +
       "           FROM dbo.HONORARIOS_MEDICOS_DATOSADICIONALES RIGHT OUTER JOIN\n" +
      "                   dbo.HONORARIOS_MEDICOS INNER JOIN\n" +
     "                    dbo.ATENCIONES ON dbo.HONORARIOS_MEDICOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN\n" +
    "                     dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO LEFT OUTER JOIN\n" +
   "                      dbo.FORMA_PAGO ON dbo.HONORARIOS_MEDICOS.FOR_CODIGO = dbo.FORMA_PAGO.FOR_CODIGO ON\n" +
  "                       dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HOM_CODIGO = dbo.HONORARIOS_MEDICOS.HOM_CODIGO LEFT OUTER JOIN\n" +
 "                        dbo.RETENCIONES_FUENTE INNER JOIN\n" +
"                         dbo.MEDICOS ON dbo.RETENCIONES_FUENTE.RET_CODIGO = dbo.MEDICOS.RET_CODIGO ON dbo.HONORARIOS_MEDICOS.MED_CODIGO = dbo.MEDICOS.MED_CODIGO\n" +
                            "where dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.GENERADO_ASIENTO<>1 and HONORARIOS_MEDICOS.HOM_FACTURA_FECHA BETWEEN '" + codigo + "' and '" + codigo2 + "' AND (SELECT top 1 DEP_CODIGO FROM USUARIOS U WHERE U.ID_USUARIO = " + His.Entidades.Clases.Sesion.codUsuario + ") <> 14 ");
                    }
                    else
                    {
                        query = ("SELECT 'false' as Seleccion, dbo.PACIENTES.PAC_HISTORIA_CLINICA as HC, dbo.ATENCIONES.ATE_CODIGO, \n" +
                       "     concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO, ' ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2) as paciente, dbo.ATENCIONES.ATE_FACTURA_PACIENTE, \n" +
                       "     dbo.MEDICOS.MED_CODIGO,\n" +
                      "           CONCAT(dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_APELLIDO_MATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1, ' ',dbo.MEDICOS.MED_NOMBRE2) AS MEDICO,\n" +
                     "         dbo.HONORARIOS_MEDICOS.HOM_CODIGO, dbo.HONORARIOS_MEDICOS.HOM_FACTURA_FECHA AS FECHA_FACTURA_MED, dbo.HONORARIOS_MEDICOS.HOM_FACTURA_MEDICO as FACTURA,dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.AUTORIZACION_SRI AS AUTORIZACION,dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.FEC_CAD_FACTURA AS CADUCIDAD, \n" +
                    "            dbo.HONORARIOS_MEDICOS.HOM_VALOR_NETO as VALOR,\n" +
                   "              dbo.HONORARIOS_MEDICOS.HOM_COMISION_CLINICA AS COMISION,\n" +
                  "                dbo.HONORARIOS_MEDICOS.HOM_APORTE_LLAMADA AS APORTE,\n" +
                   "                dbo.HONORARIOS_MEDICOS.HOM_RETENCION AS RETENCION, dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_CUBIERTO AS VALOR_CUBIERTO,dbo.HONORARIOS_MEDICOS.HOM_RECORTE as RECORTE, dbo.RETENCIONES_FUENTE.RET_REFERENCIA as COD_RET," +
 "                  dbo.RETENCIONES_FUENTE.COD_CUE AS CTA_RETENCION ,\n" +
                   "CASE\n" +
                        "WHEN dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA = 1 THEN(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR FUERA')\n" +
                        "WHEN dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA = 'true' THEN(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR FUERA')\n" +
                        "ELSE(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'HONORARIOS POR DENTRO')\n" +
                    "END AS CTA_HONORARIOS\n" +
                    ", dbo.MEDICOS.MED_CUENTA_CONTABLE AS CTA_MEDICO,(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'APORTE LLAMADAS') AS CTA_APORTE\n" +
                    ",(select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE like 'COMISION') AS CTA_COMISION\n" +
                  ",              dbo.HONORARIOS_MEDICOS.HOM_POR_PAGAR AS A_PAGAR, ISNULL(dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.GENERADO_ASIENTO, 0) AS GENERADO,(select concat(u.NOMBRES,' ',u.APELLIDOS) as USUARIO from USUARIOS u where ID_USUARIO=dbo.HONORARIOS_MEDICOS.ID_USUARIO) AS USUARIO\n" +
        "          ,  dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA AS HON_X_FUERA,'SEGUROS' = CONVERT(BIT, CASE WHEN (SELECT C.codclas FROM Sic3000..Clasificacion C INNER JOIN Sic3000..Forma_Pago FP ON C.codclas = FP.claspag WHERE FORMA_PAGO.forpag = FP.forpag) = 7 THEN 1 else 0 end), dbo.FORMA_PAGO.forpag, 		  'FORMA PAGO' = (SELECT despag FROM Sic3000..Forma_Pago WHERE Sic3000..Forma_Pago.forpag = dbo.FORMA_PAGO.forpag), dbo.FORMA_PAGO.FOR_DESCRIPCION as 'CORRIENTE / DIFERIDO'\n" +
       "           FROM dbo.HONORARIOS_MEDICOS_DATOSADICIONALES RIGHT OUTER JOIN\n" +
      "                   dbo.HONORARIOS_MEDICOS INNER JOIN\n" +
     "                    dbo.ATENCIONES ON dbo.HONORARIOS_MEDICOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN\n" +
    "                     dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO LEFT OUTER JOIN\n" +
   "                      dbo.FORMA_PAGO ON dbo.HONORARIOS_MEDICOS.FOR_CODIGO = dbo.FORMA_PAGO.FOR_CODIGO ON\n" +
  "                       dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HOM_CODIGO = dbo.HONORARIOS_MEDICOS.HOM_CODIGO LEFT OUTER JOIN\n" +
 "                        dbo.RETENCIONES_FUENTE INNER JOIN\n" +
"                         dbo.MEDICOS ON dbo.RETENCIONES_FUENTE.RET_CODIGO = dbo.MEDICOS.RET_CODIGO ON dbo.HONORARIOS_MEDICOS.MED_CODIGO = dbo.MEDICOS.MED_CODIGO\n" +
                            "where dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.GENERADO_ASIENTO<>1 and HONORARIOS_MEDICOS.HOM_FACTURA_FECHA BETWEEN '" + codigo + "' and '" + codigo2 + "'");
                    }

                    break;
                case "HorariosMedicosAsientos2":
                    query = ("SELECT  dbo.PACIENTES.PAC_HISTORIA_CLINICA as HC, dbo.ATENCIONES.ATE_CODIGO, \n" +
                       "     concat(dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2, ' ', dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO) as paciente, \n" +

                      "           CONCAT(dbo.MEDICOS.MED_NOMBRE1, ' ', dbo.MEDICOS.MED_APELLIDO_PATERNO) AS MEDICO,\n" +
                     "         dbo.HONORARIOS_MEDICOS.HOM_FACTURA_FECHA AS FECHA_FACTURA_MED, dbo.HONORARIOS_MEDICOS.HOM_FACTURA_MEDICO as FACTURA,dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.AUTORIZACION_SRI AS AUTORIZACION,dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.FEC_CAD_FACTURA AS CADUCIDAD, \n" +
                    "            dbo.HONORARIOS_MEDICOS.HOM_VALOR_NETO as VALOR,\n" +
                   "              dbo.HONORARIOS_MEDICOS.HOM_COMISION_CLINICA AS COMISION,\n" +
                  "                dbo.HONORARIOS_MEDICOS.HOM_APORTE_LLAMADA AS APORTE,\n" +
                   "                dbo.HONORARIOS_MEDICOS.HOM_RETENCION AS RETENCION,dbo.HONORARIOS_MEDICOS.HOM_POR_PAGAR AS A_PAGAR , dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HON_FUERA AS POR_FUERA,  (select concat(u.NOMBRES,' ',u.APELLIDOS) as USUARIO from USUARIOS u where ID_USUARIO=dbo.HONORARIOS_MEDICOS.ID_USUARIO) AS USUARIO\n" +
                 "FROM dbo.RETENCIONES_FUENTE INNER JOIN dbo.MEDICOS ON dbo.RETENCIONES_FUENTE.RET_CODIGO = dbo.MEDICOS.RET_CODIGO RIGHT OUTER JOIN dbo.HONORARIOS_MEDICOS INNER JOIN dbo.ATENCIONES ON dbo.HONORARIOS_MEDICOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN\n" +
                "dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO LEFT OUTER JOIN dbo.HONORARIOS_MEDICOS_DATOSADICIONALES ON dbo.HONORARIOS_MEDICOS.HOM_CODIGO = dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.HOM_CODIGO ON  dbo.MEDICOS.MED_CODIGO = dbo.HONORARIOS_MEDICOS.MED_CODIGO\n" +
                            "where dbo.HONORARIOS_MEDICOS_DATOSADICIONALES.GENERADO_ASIENTO=1 and HONORARIOS_MEDICOS.HOM_FACTURA_FECHA BETWEEN '" + codigo + "' and '" + codigo2 + "'");
                    break;
                case "GetTempPedidoDietetica":
                    query = ("SELECT * FROM [TEMP_DIETETICA] where ATE_CODIGO = " + codigo + " ");
                    break;
                case "getPlacasImagen":
                    query = ("SELECT sum([30X40]) as [30X40] ,sum([8x10]) as [8x10] ,sum([14x14]) as [14x14] ,sum([14x17]) as [14x17] ,sum([18x24]) as [18x24] ,sum([ODONT]) as [ODONT] ,sum([DANADAS]) as [DANADAS] ,sum([ENVIADAS]) as [ENVIADAS] ,sum([MEDIO_CONTRASTE]) as [MEDIO_CONTRASTE], [CD], [DVD] " +
                        "FROM [His3000].[dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS] where id_imagenologia_agendamientos=" + codigo + "  group by id_imagenologia_agendamientos, CD, DVD");
                    break;
                case "GetEgreso_MedicosEvolucion":
                    query = ("SELECT CAST(dbo.HC_EVOLUCION_DETALLE.NOM_USUARIO AS NVARCHAR(MAX)) AS NOM_USUARIO FROM dbo.HC_EVOLUCION INNER JOIN dbo.HC_EVOLUCION_DETALLE ON dbo.HC_EVOLUCION.EVO_CODIGO = dbo.HC_EVOLUCION_DETALLE.EVO_CODIGO\n" +
                        "WHERE dbo.HC_EVOLUCION.ATE_CODIGO =" + codigo + " UNION \r\n" +
                        "SELECT('Dr/a: ' + M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2) AS NOM_USUARIO FROM ATENCIONES A \r\n" +
                        "INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO \r\n" +
                        "WHERE A.ATE_CODIGO = " + codigo + " UNION SELECT('Dr/a: ' + M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2) \r\n" +
                        " AS NOM_USUARIO FROM REGISTRO_QUIROFANO R INNER JOIN MEDICOS M ON r.cirujano = M.MED_CODIGO WHERE R.ATE_CODIGO = " + codigo);
                    break;

                case "getTurnoRadiologo":
                    query = ("SELECT TOP 1 dbo.MEDICOS.MED_CODIGO FROM  dbo.MEDICOS INNER JOIN dbo.ESPECIALIDADES_MEDICAS ON dbo.MEDICOS.ESP_CODIGO = dbo.ESPECIALIDADES_MEDICAS.ESP_CODIGO INNER JOIN dbo.HORARIO_MEDICOS ON dbo.MEDICOS.MED_CODIGO = dbo.HORARIO_MEDICOS.MED_CODIGO\n" +
                        "where INGRESO<=getdate() and SALIDA>getdate() AND ESP_NOMBRE LIKE '%RADIOLOGO%'");
                    break;
                case "getTurnoTecnologo":
                    query = ("SELECT TOP 1 dbo.MEDICOS.MED_CODIGO FROM  dbo.MEDICOS INNER JOIN dbo.ESPECIALIDADES_MEDICAS ON dbo.MEDICOS.ESP_CODIGO = dbo.ESPECIALIDADES_MEDICAS.ESP_CODIGO INNER JOIN dbo.HORARIO_MEDICOS ON dbo.MEDICOS.MED_CODIGO = dbo.HORARIO_MEDICOS.MED_CODIGO\n" +
                        "where INGRESO<=getdate() and SALIDA>getdate() AND ESP_NOMBRE LIKE '%TECNOLOGO%'");
                    break;
                case "GetReferenciaDx":
                    query = ("SELECT dbo.CIE10.CIE_CODIGO, dbo.CIE10.CIE_DESCRIPCION, dbo.HC_REFERENCIA_DIAGNOSTICOS.DEFINITIVO\n" +
                            "FROM dbo.HC_REFERENCIA_DIAGNOSTICOS INNER JOIN dbo.CIE10 ON dbo.HC_REFERENCIA_DIAGNOSTICOS.CIE_CODIGO = dbo.CIE10.CIE_CODIGO\n" +
                            "WHERE dbo.HC_REFERENCIA_DIAGNOSTICOS.HC_REFERENCIA_Id = " + codigo);
                    break;
                case "GetContrareferenciaDx":
                    query = ("SELECT dbo.CIE10.CIE_CODIGO, dbo.CIE10.CIE_DESCRIPCION, dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS.DEFINITIVO\n" +
                            "FROM dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS INNER JOIN dbo.CIE10 ON dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS.CIE_CODIGO = dbo.CIE10.CIE_CODIGO\n" +
                            "WHERE dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS.HC_CONTRAREFERENCIA_Id = " + codigo);
                    break;

                case "GetReferencia":
                    query = ("INSERT INTO HC_REFERENCIA(ID_USUARIO, MED_CODIGO,ATE_CODIGO)   \n" +
                            "SELECT " + codigo2 + ", 0, " + codigo + "\n" +
                            "WHERE NOT EXISTS(SELECT * FROM HC_REFERENCIA WHERE ATE_CODIGO = " + codigo + ");\n" +
                            "select* from HC_REFERENCIA WHERE ATE_CODIGO = " + codigo);
                    break;
                case "GetContrareferencia":
                    query = ("INSERT INTO HC_CONTRAREFERENCIA(ID_USUARIO, MED_CODIGO,ATE_CODIGO)   \n" +
                            "SELECT " + codigo2 + ", 1160   , " + codigo + "\n" +
                            "WHERE NOT EXISTS(SELECT * FROM HC_CONTRAREFERENCIA WHERE ATE_CODIGO = " + codigo + ");\n" +
                            "select* from HC_CONTRAREFERENCIA WHERE ATE_CODIGO = " + codigo);
                    break;
                case "GetTipoAtencion":
                    query = ("SELECT  *  FROM [His3000].[dbo].[tipos_atenciones] where id =" + codigo);
                    break;

                case "GetMedicos":
                    query = ("SELECT dbo.MEDICOS.MED_CODIGO, concat(dbo.MEDICOS.MED_NOMBRE1,' ',dbo.MEDICOS.MED_NOMBRE2,' ',dbo.MEDICOS.MED_APELLIDO_PATERNO,' ',dbo.MEDICOS.MED_APELLIDO_MATERNO) as MEDICO, dbo.ESPECIALIDADES_MEDICAS.ESP_NOMBRE AS ESPECIALIDAD FROM dbo.MEDICOS INNER JOIN dbo.ESPECIALIDADES_MEDICAS ON dbo.MEDICOS.ESP_CODIGO = dbo.ESPECIALIDADES_MEDICAS.ESP_CODIGO");
                    break;

                case "ReferenciaEncabezado":
                    query = ("SELECT (select TOP 1 EMP_NOMBRE FROM EMPRESA) AS EMPRESA, dbo.PACIENTES_DATOS_ADICIONALES.COD_PARROQUIA, dbo.PACIENTES_DATOS_ADICIONALES.COD_CANTON, dbo.PACIENTES_DATOS_ADICIONALES.COD_PROVINCIA, dbo.PACIENTES.PAC_HISTORIA_CLINICA, \n" +
                            "    dbo.PACIENTES.PAC_APELLIDO_PATERNO, dbo.PACIENTES.PAC_APELLIDO_MATERNO, dbo.PACIENTES.PAC_NOMBRE1, dbo.PACIENTES.PAC_NOMBRE2, dbo.PACIENTES.PAC_IDENTIFICACION, \n" +
                            "    convert(char(8), dbo.HC_REFERENCIA.FECHA, 108) as HORA,\n" +
                            "    CAST(dbo.HC_REFERENCIA.FECHA AS DATE) AS FECHA,\n" +
                            "    CONVERT(int, ROUND(DATEDIFF(hour, dbo.PACIENTES.PAC_FECHA_NACIMIENTO, GETDATE()) / 8766.0, 0)) AS EDAD,\n" +
                            "           dbo.PACIENTES.PAC_GENERO, \n" +
                            "    dbo.ESTADO_CIVIL.ESC_NOMBRE AS ESTADO_CIVIL, dbo.PACIENTES_DATOS_ADICIONALES.DAP_INSTRUCCION, \n" +
                            "    dbo.PACIENTES_DATOS_ADICIONALES.DAP_EMP_NOMBRE, \n" +
                            "    (SELECT    TOP(1) dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE\n" +
                            "       FROM dbo.ATENCIONES a INNER JOIN dbo.ATENCION_DETALLE_CATEGORIAS ON a.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO\n" +
                            "    WHERE a.ATE_CODIGO = aa.ATE_CODIGO) as SEGURO, dbo.HC_REFERENCIA.ESTABLECIMIENTO, dbo.HC_REFERENCIA.SERVICIO, dbo.HC_REFERENCIA.MOTIVO, dbo.HC_REFERENCIA.RESUMEN, \n" +
                            "    dbo.HC_REFERENCIA.HALLAZGOS, dbo.HC_REFERENCIA.TRATAMIENTO, dbo.HABITACIONES.hab_Numero AS HABITACION, CONCAT(	dbo.MEDICOS.MED_NOMBRE1,' ', dbo.MEDICOS.MED_APELLIDO_PATERNO) AS MEDICO, \n" +
                            "    Concat(dbo.MEDICOS.MED_CODIGO_LIBRO, '-', dbo.MEDICOS.MED_CODIGO_FOLIO) as LIBRO_FOLIO\n" +
                            "FROM dbo.HC_REFERENCIA INNER JOIN\n" +
                            "    dbo.ATENCIONES aa ON dbo.HC_REFERENCIA.ATE_CODIGO = aa.ATE_CODIGO INNER JOIN\n" +
                            "    dbo.PACIENTES ON aa.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN dbo.PACIENTES_DATOS_ADICIONALES ON dbo.PACIENTES.PAC_CODIGO = dbo.PACIENTES_DATOS_ADICIONALES.PAC_CODIGO LEFT OUTER JOIN\n" +
                            "    dbo.MEDICOS ON aa.MED_CODIGO = dbo.MEDICOS.MED_CODIGO LEFT OUTER JOIN dbo.HABITACIONES ON aa.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo LEFT OUTER JOIN dbo.ESTADO_CIVIL ON dbo.PACIENTES_DATOS_ADICIONALES.ESC_CODIGO = dbo.ESTADO_CIVIL.ESC_CODIGO\n" +
                            "where DAP_ESTADO = 1  and aa.ATE_CODIGO = " + codigo);
                    break;

                case "ContrareferenciaEncabezado":
                    query = ("SELECT (select TOP 1 EMP_NOMBRE FROM EMPRESA) AS EMPRESA, dbo.PACIENTES_DATOS_ADICIONALES.COD_PARROQUIA, dbo.PACIENTES_DATOS_ADICIONALES.COD_CANTON, dbo.PACIENTES_DATOS_ADICIONALES.COD_PROVINCIA, dbo.PACIENTES.PAC_HISTORIA_CLINICA, \n" +
                                "dbo.PACIENTES.PAC_APELLIDO_PATERNO, dbo.PACIENTES.PAC_APELLIDO_MATERNO, dbo.PACIENTES.PAC_NOMBRE1, dbo.PACIENTES.PAC_NOMBRE2, dbo.PACIENTES.PAC_IDENTIFICACION, \n" +
                                "convert(char(8), dbo.HC_CONTRAREFERENCIA.FECHA, 108) as HORA, CAST(dbo.HC_CONTRAREFERENCIA.FECHA AS DATE) AS FECHA, CONVERT(int, ROUND(DATEDIFF(hour, dbo.PACIENTES.PAC_FECHA_NACIMIENTO, GETDATE()) / 8766.0, 0)) AS EDAD,\n" +
                                "dbo.PACIENTES.PAC_GENERO, dbo.ESTADO_CIVIL.ESC_NOMBRE AS ESTADO_CIVIL, dbo.PACIENTES_DATOS_ADICIONALES.DAP_INSTRUCCION,  dbo.PACIENTES_DATOS_ADICIONALES.DAP_EMP_NOMBRE, \n" +
                                "(SELECT    TOP(1) dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE FROM dbo.ATENCIONES a INNER JOIN dbo.ATENCION_DETALLE_CATEGORIAS ON a.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO\n" +
                                "WHERE a.ATE_CODIGO = aa.ATE_CODIGO) as SEGURO, dbo.HC_CONTRAREFERENCIA.ESTABLECIMIENTO, dbo.HC_CONTRAREFERENCIA.SERVICIO, dbo.HC_CONTRAREFERENCIA.TRATAMIENTO_REALIZADO AS MOTIVO, dbo.HC_CONTRAREFERENCIA.RESUMEN, \n" +
                                "dbo.HC_CONTRAREFERENCIA.HALLAZGOS, dbo.HC_CONTRAREFERENCIA.TRATAMIENTO_RECOMENDADO AS TRATAMIENTO, dbo.HABITACIONES.hab_Numero AS HABITACION, CONCAT(dbo.MEDICOS.MED_NOMBRE1, ' ', dbo.MEDICOS.MED_APELLIDO_PATERNO) AS MEDICO, Concat(dbo.MEDICOS.MED_CODIGO_LIBRO, '-', dbo.MEDICOS.MED_CODIGO_FOLIO) as LIBRO_FOLIO\n" +
                            "FROM dbo.HC_CONTRAREFERENCIA INNER JOIN dbo.ATENCIONES aa ON dbo.HC_CONTRAREFERENCIA.ATE_CODIGO = aa.ATE_CODIGO INNER JOIN dbo.PACIENTES ON aa.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN dbo.PACIENTES_DATOS_ADICIONALES ON dbo.PACIENTES.PAC_CODIGO = dbo.PACIENTES_DATOS_ADICIONALES.PAC_CODIGO LEFT OUTER JOIN\n" +
                                "dbo.MEDICOS ON aa.MED_CODIGO = dbo.MEDICOS.MED_CODIGO LEFT OUTER JOIN dbo.HABITACIONES ON aa.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo LEFT OUTER JOIN dbo.ESTADO_CIVIL ON dbo.PACIENTES_DATOS_ADICIONALES.ESC_CODIGO = dbo.ESTADO_CIVIL.ESC_CODIGO\n" +
                            "where DAP_ESTADO = 1   and aa.ATE_CODIGO = " + codigo);
                    break;

                case "GetDxReferencia":
                    query = (" SELECT dbo.CIE10.CIE_CODIGO, dbo.CIE10.CIE_DESCRIPCION, dbo.HC_REFERENCIA_DIAGNOSTICOS.DEFINITIVO\n" +
                            "FROM dbo.HC_REFERENCIA_DIAGNOSTICOS INNER JOIN dbo.CIE10 ON dbo.HC_REFERENCIA_DIAGNOSTICOS.CIE_CODIGO = dbo.CIE10.CIE_CODIGO\n" +
                            "WHERE dbo.HC_REFERENCIA_DIAGNOSTICOS.HC_REFERENCIA_Id = " + codigo);
                    break;
                case "GetDxContrareferencia":
                    query = (" SELECT dbo.CIE10.CIE_CODIGO, dbo.CIE10.CIE_DESCRIPCION, dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS.DEFINITIVO\n" +
                            "FROM dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS INNER JOIN dbo.CIE10 ON dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS.CIE_CODIGO = dbo.CIE10.CIE_CODIGO\n" +
                            "WHERE dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS.HC_CONTRAREFERENCIA_Id = " + codigo);
                    break;
                case "GetProductos": //cambios Edgar 20201216 se agrego el codigo de pedido desde servicios externos para poder hacer la devolucion.
                    query = ("SELECT  dbo.CUENTAS_PACIENTES.PRO_CODIGO AS CODIGO,  dbo.PRODUCTO.PRO_DESCRIPCION AS PRODUCTO,  dbo.CUENTAS_PACIENTES.CUE_VALOR_UNITARIO as VALOR_UNITARIO, dbo.CUENTAS_PACIENTES.CUE_CANTIDAD AS CANTIDAD, dbo.CUENTAS_PACIENTES.CUE_VALOR AS VALOR, dbo.CUENTAS_PACIENTES.CUE_IVA AS IVA, (dbo.CUENTAS_PACIENTES.CUE_VALOR + dbo.CUENTAS_PACIENTES.CUE_IVA) as TOTAL, dbo.ATENCIONES.ATE_CODIGO, dbo.CUENTAS_PACIENTES.Codigo_Pedido\n" +
                            "FROM dbo.CUENTAS_PACIENTES INNER JOIN dbo.PRODUCTO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO INNER JOIN dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO LEFT OUTER JOIN dbo.MEDICOS ON dbo.ATENCIONES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO \n" +
                            "where ATENCIONES.ATE_CODIGO = " + codigo + "AND dbo.CUENTAS_PACIENTES.CUE_CANTIDAD > 0");
                    break;
                case "GetProductos1":
                    query = ("SELECT dbo.CUENTAS_PACIENTES.CUE_CODIGO\n" +
                            "FROM dbo.CUENTAS_PACIENTES INNER JOIN dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO\n" +
                            "where ATENCIONES.ATE_CODIGO = " + codigo);
                    break;
                case "PathLocalHC":
                    query = ("select PAD_VALOR from PARAMETROS_DETALLE where PAD_NOMBRE = 'PATH_LOCAL'");
                    break;



                case "GetEgreso_Garantias":
                    query = (" SELECT        dbo.ATENCION_DETALLE_GARANTIAS.ADG_FECHA, dbo.TIPO_GARANTIA.TG_NOMBRE, dbo.ATENCION_DETALLE_GARANTIAS.ADG_VALOR, dbo.ATENCION_DETALLE_GARANTIAS.ADG_DESCRIPCION AS TITULAR, \n" +
                         "dbo.ATENCION_DETALLE_GARANTIAS.ADG_BANCO, dbo.ATENCION_DETALLE_GARANTIAS.ADG_DOCUMENTO, dbo.ATENCION_DETALLE_GARANTIAS.ADG_TIPOTARJETA, dbo.ATENCION_DETALLE_GARANTIAS.ADG_ESTATUS,  dbo.ATENCION_DETALLE_GARANTIAS.ADG_OBSERVACION\n" +
                    "   FROM            dbo.ATENCION_DETALLE_GARANTIAS INNER JOIN dbo.TIPO_GARANTIA ON dbo.ATENCION_DETALLE_GARANTIAS.TG_CODIGO = dbo.TIPO_GARANTIA.TG_CODIGO WHERE ATE_CODIGO =" + codigo);
                    break;
                case "GetEgreso_HistorialHabitacion":
                    query = ("SELECT  'OBSERVACION'=hh.HAD_OBSERVACION ,'FECHA_MOVIMIENTO'=CONVERT(VARCHAR(11), hh.HAH_FECHA_INGRESO,103),  'HORA'= CONVERT(VARCHAR(11), hh.HAH_FECHA_INGRESO,108),'HABITACION'=(SELECT Hab_Numero FROM HABITACIONES WHERE hab_Codigo=hh.HAB_CODIGO),\n" +
                            "'ANTERIOR' = (SELECT hab_Numero FROM HABITACIONES WHERE HAB_CODIGO = (SELECT  HABITACIONES_DETALLE.hab_Codigo FROM  HABITACIONES_DETALLE WHERE HAD_CODIGO = hh.HAH_REGISTRO_ANTERIOR)),'ESTADO' = (SELECT dbo.HABITACIONES_ESTADO.HES_NOMBRE FROM dbo.HABITACIONES_ESTADO\n" +
                             "    WHERE dbo.HABITACIONES_ESTADO.HES_CODIGO = CONVERT(INT, hh.HAH_ESTADO) ),'USUARIO' = (select U.USR from USUARIOS u where u.ID_USUARIO = hh.ID_USUARIO) FROM dbo.HABITACIONES_HISTORIAL hh\n" +
                            "WHERE hh.ATE_CODIGO=" + codigo);
                    break;
                case "GetEgreso_ConvenioSeguro":
                    query = ("SELECT   dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE, dbo.ATENCION_DETALLE_CATEGORIAS.ADA_FECHA_INICIO, dbo.ATENCION_DETALLE_CATEGORIAS.ADA_FECHA_FIN, \n" +
                         "dbo.ATENCION_DETALLE_CATEGORIAS.ADA_MONTO_COBERTURA\n" +
                        "FROM            dbo.ATENCIONES INNER JOIN\n" +
                        " dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN\n" +
                        " dbo.CATEGORIAS_CONVENIOS ON dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO\n" +
                        "WHERE dbo.ATENCIONES.ATE_CODIGO =" + codigo);
                    break;






                case "HistorialDietas":
                    //query = ("SELECT dbo.PEDIDOS.PED_CODIGO AS 'Cod. Pedido', dbo.PEDIDOS.PED_FECHA AS 'F. Pedido', dbo.PEDIDOS_DETALLE.PRO_DESCRIPCION AS Descripcion, dbo.PEDIDOS_DETALLE.PDD_CANTIDAD AS Cantidad, dbo.PEDIDOS.PED_DESCRIPCION AS Observacion, concat(dbo.USUARIOS.APELLIDOS,' ', dbo.USUARIOS.NOMBRES) AS 'Solicitado Por'\n" +
                    //        /*dbo.PEDIDOS_DETALLE.PDD_VALOR, dbo.PEDIDOS_DETALLE.PDD_IVA, dbo.PEDIDOS_DETALLE.PDD_TOTAL, */
                    //        "FROM dbo.PEDIDOS_DETALLE left JOIN dbo.PEDIDOS ON dbo.PEDIDOS_DETALLE.PED_CODIGO = dbo.PEDIDOS.PED_CODIGO left JOIN dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO left JOIN dbo.PEDIDOS_AREAS ON dbo.RUBROS.RUB_CODIGO = dbo.PEDIDOS_AREAS.RUB_CODIGO ON dbo.PEDIDOS_DETALLE.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO INNER JOIN dbo.ATENCIONES ON dbo.PEDIDOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO left JOIN dbo.USUARIOS ON dbo.PEDIDOS.ID_USUARIO = dbo.USUARIOS.ID_USUARIO \n" +
                    //        "WHERE        (dbo.RUBROS.RUB_ASOCIADO = 3) and dbo.ATENCIONES.ATE_CODIGO=" + codigo + "  \n"+
                    //        "ORDER BY dbo.PEDIDOS.PED_FECHA DESC");
                    query = ("SELECT CP.Codigo_Pedido AS 'Cod. Pedido', CP.CUE_FECHA AS 'F. Pedido', \n "
 + " dbo.PEDIDOS_DETALLE.PRO_DESCRIPCION AS Descripcion, CP.CUE_CANTIDAD AS Cantidad, \n "
 + " dbo.PEDIDOS.PED_DESCRIPCION AS Observacion, concat(dbo.USUARIOS.APELLIDOS, ' ', dbo.USUARIOS.NOMBRES) AS 'Solicitado Por' \n "
 + " FROM dbo.PEDIDOS_DETALLE  \n "
 + " left JOIN dbo.PEDIDOS ON dbo.PEDIDOS_DETALLE.PED_CODIGO = dbo.PEDIDOS.PED_CODIGO \n "
 + " left JOIN dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub \n "
 + " AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv \n "
 + " INNER JOIN dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO \n "
 + " left JOIN dbo.PEDIDOS_AREAS ON dbo.RUBROS.RUB_CODIGO = dbo.PEDIDOS_AREAS.RUB_CODIGO ON dbo.PEDIDOS_DETALLE.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO \n "
 + " INNER JOIN dbo.ATENCIONES ON dbo.PEDIDOS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO\n "
 + " left JOIN dbo.USUARIOS ON dbo.PEDIDOS.ID_USUARIO = dbo.USUARIOS.ID_USUARIO \n "
 + "INNER JOIN CUENTAS_PACIENTES CP ON PEDIDOS.PED_CODIGO = CP.Codigo_Pedido \n "
 + " WHERE(dbo.RUBROS.RUB_ASOCIADO = 3) and dbo.ATENCIONES.ATE_CODIGO = " + codigo + "\n "
 + " ORDER BY dbo.PEDIDOS.PED_FECHA DESC");
                    break;

                case "getProductosAlimentacion":
                    query = ("SELECT P.codpro AS CODIGO, P.despro AS NOMBRE, dbo.RUBROS.RUB_NOMBRE SUBDIVISION, dbo.RUBROS.RUB_CODIGO CODSUBD " +
                                "FROM Sic3000.dbo.Producto P " +
                                "INNER JOIN Sic3000.dbo.ProductoSubdivision ON P.codsub = Sic3000.dbo.ProductoSubdivision.codsub AND P.coddiv = Sic3000.dbo.ProductoSubdivision.coddiv " +
                                "INNER JOIN dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO " +
                                "where dbo.RUBROS.RUB_ASOCIADO = (select PEA_CODIGO from PEDIDOS_AREAS where PEA_NOMBRE LIKE '%ALIMENTACION%' OR PEA_NOMBRE LIKE '%DIETETICA%')");
                    break;
                case "getProductosiNSUMOS":
                    query = ("SELECT P.codpro AS CODIGO, P.despro AS NOMBRE, dbo.RUBROS.RUB_NOMBRE SUBDIVISION, dbo.RUBROS.RUB_CODIGO CODSUBD FROM Sic3000.dbo.Producto P \n " +
                        "INNER JOIN Sic3000.dbo.ProductoSubdivision ON P.codsub = Sic3000.dbo.ProductoSubdivision.codsub AND P.coddiv = Sic3000.dbo.ProductoSubdivision.coddiv  \n " +
                        "INNER JOIN dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO WHERE dbo.RUBROS.RUB_NOMBRE  LIKE '%INSUMOS%'  ");
                    break;
                case "getObservacionAtencion":
                    query = (" select ATE_OBSERVACIONES from ATENCIONES where ATE_CODIGO=" + codigo);
                    break;
                case "getDetalleGaratias":
                    query = ("SELECT dbo.ATENCION_DETALLE_GARANTIAS.*, concat(dbo.USUARIOS.APELLIDOS,' ',dbo.USUARIOS.NOMBRES) as userName FROM dbo.ATENCION_DETALLE_GARANTIAS LEFT JOIN dbo.USUARIOS ON dbo.ATENCION_DETALLE_GARANTIAS.ADG_USER = dbo.USUARIOS.ID_USUARIO where ADG_CODIGO=" + codigo);
                    break;
                case "getExploradorRubros":
                    query = ("SELECT  dbo.PACIENTES.PAC_HISTORIA_CLINICA AS HC,  \n" +
                          "  concat(dbo.PACIENTES.PAC_APELLIDO_MATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2) as Paciente,  \n" +
                         "   dbo.ATENCIONES.ATE_NUMERO_ATENCION AS ATE, dbo.HABITACIONES.hab_Numero AS HAB, dbo.ATENCIONES.ATE_FECHA_INGRESO, dbo.ATENCIONES.ATE_FECHA_ALTA, \n" +
                        "    concat(dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1) as Medico \n" +
                       "     , dbo.CUENTAS_PACIENTES.CUE_FECHA AS Fecha_Pedido, Concat(dbo.USUARIOS.NOMBRES, ' ', dbo.USUARIOS.APELLIDOS) AS Usuario_Solicita, dbo.PEDIDOS_AREAS.PEA_NOMBRE AS Area,  \n" +
                      "      dbo.RUBROS.RUB_NOMBRE AS Subarea, dbo.RUBROS.RUB_GRUPO AS Grupo_Facturacion, dbo.PRODUCTO.PRO_DESCRIPCION AS Producto, dbo.CUENTAS_PACIENTES.PRO_CODIGO,  \n" +
                     "       dbo.CUENTAS_PACIENTES.CUE_VALOR_UNITARIO, dbo.CUENTAS_PACIENTES.CUE_CANTIDAD, dbo.CUENTAS_PACIENTES.CUE_VALOR, dbo.CUENTAS_PACIENTES.CUE_IVA, (dbo.CUENTAS_PACIENTES.CUE_VALOR + dbo.CUENTAS_PACIENTES.CUE_IVA) as Total \n" +
                        "FROM dbo.USUARIOS INNER JOIN dbo.CUENTAS_PACIENTES ON dbo.USUARIOS.ID_USUARIO = dbo.CUENTAS_PACIENTES.ID_USUARIO INNER JOIN dbo.PRODUCTO INNER JOIN \n" +
                                "Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN \n" +
                                "dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN dbo.PEDIDOS_AREAS ON dbo.RUBROS.PED_CODIGO = dbo.PEDIDOS_AREAS.DIV_CODIGO ON dbo.CUENTAS_PACIENTES.PRO_CODIGO = dbo.PRODUCTO.PRO_CODIGO INNER JOIN \n" +
                                "dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN \n" +
                                "dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo INNER JOIN dbo.MEDICOS ON dbo.ATENCIONES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO");
                    break;
                case "getGrupoRubros":
                    query = ("SELECT distinct[RUB_GRUPO] FROM[His3000].[dbo].[RUBROS]");
                    break;
                case "Defunciones":
                    query = ("SELECT  dbo.PACIENTES_DATOS_ADICIONALES2.PAC_CODIGO,    dbo.PACIENTES_DATOS_ADICIONALES2.FECHA_FALLECIDO, dbo.PACIENTES_DATOS_ADICIONALES2.FOLIO, concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO,' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO,' ', dbo.PACIENTES.PAC_NOMBRE1, ' ',dbo.PACIENTES.PAC_NOMBRE2) as PACIENTE,  \n" +
                         "dbo.PACIENTES.PAC_IDENTIFICACION, dbo.PACIENTES.PAC_NACIONALIDAD, \n" +
                         " dbo.DIVISION_POLITICA.DIPO_NOMBRE AS PROVINCIA, DIVISION_POLITICA_1.DIPO_NOMBRE AS CANTON, DIVISION_POLITICA_2.DIPO_NOMBRE AS PARROQUIA,dbo.PACIENTES_DATOS_ADICIONALES.DAP_DIRECCION_DOMICILIO,  dbo.PACIENTES.PAC_GENERO, \n" +
                         "dbo.PACIENTES.PAC_FECHA_NACIMIENTO\n" +
                        "FROM            dbo.PACIENTES INNER JOIN\n" +
                        " dbo.PACIENTES_DATOS_ADICIONALES2 ON dbo.PACIENTES.PAC_CODIGO = dbo.PACIENTES_DATOS_ADICIONALES2.PAC_CODIGO INNER JOIN\n" +
                        " dbo.PACIENTES_DATOS_ADICIONALES ON dbo.PACIENTES.PAC_CODIGO = dbo.PACIENTES_DATOS_ADICIONALES.PAC_CODIGO INNER JOIN\n" +
                        " dbo.ESTADO_CIVIL ON dbo.PACIENTES_DATOS_ADICIONALES.ESC_CODIGO = dbo.ESTADO_CIVIL.ESC_CODIGO LEFT OUTER JOIN\n" +
                        " dbo.DIVISION_POLITICA AS DIVISION_POLITICA_1 ON dbo.PACIENTES_DATOS_ADICIONALES.COD_CANTON = DIVISION_POLITICA_1.DIPO_CODIINEC LEFT OUTER JOIN\n" +
                        " dbo.DIVISION_POLITICA ON dbo.PACIENTES_DATOS_ADICIONALES.COD_PROVINCIA = dbo.DIVISION_POLITICA.DIPO_CODIINEC LEFT OUTER JOIN\n" +
                        " dbo.DIVISION_POLITICA AS DIVISION_POLITICA_2 ON dbo.PACIENTES_DATOS_ADICIONALES.COD_PARROQUIA = DIVISION_POLITICA_2.DIPO_CODIINEC\n" +
                        "WHERE(dbo.PACIENTES_DATOS_ADICIONALES.DAP_ESTADO = 1) AND(dbo.PACIENTES_DATOS_ADICIONALES2.FALLECIDO = 1) AND dbo.PACIENTES_DATOS_ADICIONALES2.FECHA_FALLECIDO BETWEEN " + codigo);
                    break;
                case "getHabitaciones":
                    query = ("SELECT dbo.HABITACIONES.hab_Numero AS HABITACION, dbo.PACIENTES.PAC_IDENTIFICACION AS IDENTIFICACION, concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO,' ',dbo.PACIENTES.PAC_APELLIDO_MATERNO,' ', dbo.PACIENTES.PAC_NOMBRE1,' ',dbo.PACIENTES.PAC_NOMBRE2) as PACIENTE, \n" +
                                        "       dbo.ATENCIONES.ATE_FECHA_INGRESO AS INGRESO, dbo.PACIENTES.PAC_HISTORIA_CLINICA AS HC, dbo.ATENCIONES.ATE_CODIGO AS ATENCION, dbo.PACIENTES.PAC_GENERO,\n" +
                                         "    dbo.MEDICOS.MED_CODIGO,  concat(dbo.MEDICOS.MED_APELLIDO_PATERNO,' ',dbo.MEDICOS.MED_APELLIDO_MATERNO,' ', dbo.MEDICOS.MED_NOMBRE2,' ', dbo.MEDICOS.MED_NOMBRE1) as MEDICO, dbo.TIPO_TRATAMIENTO.TIA_DESCRIPCION AS TRATAMIENTO,\n" +
                                         "     dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE as CONVENIO, dbo.ATENCIONES.ATE_DIAGNOSTICO_INICIAL, DATEDIFF(year, dbo.PACIENTES.PAC_FECHA_NACIMIENTO, GETDATE()) as EDAD, dbo.ATENCIONES.ATE_DIAGNOSTICO_INICIAL AS 'DIAGNO. INICIAL', dbo.PACIENTES.PAC_FECHA_NACIMIENTO AS NACIMIENTO\n" +
                                         "     , 'false' as OK\n" +
                                        "FROM   dbo.CATEGORIAS_CONVENIOS INNER JOIN dbo.ATENCION_DETALLE_CATEGORIAS ON dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO RIGHT OUTER JOIN\n" +
                                                "dbo.HABITACIONES_ESTADO INNER JOIN dbo.HABITACIONES ON dbo.HABITACIONES_ESTADO.HES_CODIGO = dbo.HABITACIONES.HES_CODIGO INNER JOIN dbo.ATENCIONES ON dbo.HABITACIONES.hab_Codigo = dbo.ATENCIONES.HAB_CODIGO INNER JOIN  dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN\n" +
                                                "dbo.MEDICOS ON dbo.ATENCIONES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO INNER JOIN dbo.TIPO_TRATAMIENTO ON dbo.ATENCIONES.TIA_CODIGO = dbo.TIPO_TRATAMIENTO.TIA_CODIGO ON dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO\n" +
                                        "WHERE(dbo.HABITACIONES_ESTADO.HES_ACTIVO = 1) AND (dbo.ATENCIONES.ESC_CODIGO = 1) AND(dbo.HABITACIONES.HES_CODIGO = 1)\n" +
                                        "ORDER BY dbo.HABITACIONES.hab_Numero");
                    break;

                default:
                    query = ("Nothing");
                    break;
            }



            DataTable tablas = new DataTable();
            try
            {
                Sqlcmd = new SqlCommand(query, Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;

                SqlDataReader reader;
                Sqldap.SelectCommand = Sqlcmd;
                reader = Sqlcmd.ExecuteReader();
                tablas.Load(reader);
                reader.Close();
                Sqldap.Fill(Dts);
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (Dts.Rows.Count > 0)
                return Dts;
            else
                return tablas;
        }



        public void saveDxReferencia(List<PedidoImagen_diagnostico> dx, int codigo)
        {
            string cadena_sql;
            cadena_sql = "delete from dbo.HC_REFERENCIA_DIAGNOSTICOS WHERE HC_REFERENCIA_Id = " + codigo + "\n";
            List<PedidoImagen_diagnostico> diag = dx;
            foreach (var diagnostico in diag)
            {
                cadena_sql += "INSERT INTO [dbo].[HC_REFERENCIA_DIAGNOSTICOS] ([HC_REFERENCIA_Id] ,[CIE_CODIGO] ,[DEFINITIVO]) VALUES (" +
                            codigo
                           + ", '" + diagnostico.CIE_CODIGO
                           + "', " + diagnostico.DEFINITIVO
                           + ")\n";
            }

            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void saveDxContrareferencia(List<PedidoImagen_diagnostico> dx, int codigo)
        {
            string cadena_sql;
            cadena_sql = "delete from dbo.HC_CONTRAREFERENCIA_DIAGNOSTICOS WHERE HC_CONTRAREFERENCIA_Id = " + codigo + "\n";
            List<PedidoImagen_diagnostico> diag = dx;
            foreach (var diagnostico in diag)
            {
                cadena_sql += "INSERT INTO [dbo].[HC_CONTRAREFERENCIA_DIAGNOSTICOS] ([HC_CONTRAREFERENCIA_Id] ,[CIE_CODIGO] ,[DEFINITIVO]) VALUES (" +
                            codigo
                           + ", '" + diagnostico.CIE_CODIGO
                           + "', " + diagnostico.DEFINITIVO
                           + ")\n";
            }

            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public void setROW(string tabla, object[] values, string code = "")
        {
            string query = "";
            switch (tabla)
            {
                case "HoraKardexMedicamentos":

                    query = ("UPDATE [dbo].[KARDEXMEDICAMENTOS]     \n" +
                              "  SET [Hora] = '" + values[1] + "'           \n" +
                             "WHERE ID_KARDEX_MEDICAMENTOS =" + values[0] + " ");
                    break;
                case "UpdateKardexInsumo":

                    query = ("UPDATE [dbo].[KARDEX_INSUMOS]     \n" +
                              " SET[Administrado] = " + values[0] + "           \n" +
                              "    ,[NoAdministrado] = " + values[1] + "        \n" +
                              "    ,[fecha] = '" + values[2] + "'         \n" +
                              "    ,[ID_USUARIO] = " + values[3] + "           \n" +
                              "    ,[observacion] = '" + values[4] + "'           \n" +
                             "WHERE id =" + code + " ");
                    break;
                case "DetalleEvolucionEnfermeria":
                    if (code != "NUEVO")
                        query = ("update HC_EVOLUCION_ENFERMERIA_DETALLE set  ESTADO = 0  WHERE  EVD_CODIGO = " + code + " \n");


                    query += ("  IF not EXISTS(SELECT * FROM [HC_EVOLUCION_ENFERMERA] WHERE ATE_CODIGO = " + values[0] + " ) \n" +
                       "INSERT INTO[dbo].[HC_EVOLUCION_ENFERMERA](ATE_CODIGO, [PAC_CODIGO],[EVO_FECHA_CREACION] ,[ID_USUARIO],[NOM_USUARIO])\n" +
                        "VALUES(" + values[0] + ",(SELECT a.PAC_CODIGO FROM [His3000].[dbo].ATENCIONES a where a.ATE_CODIGO= " + values[0] + ") ,GETDATE()\n" +
                        "		," + values[1] + ",'" + values[2] + "'   )\n" +
                        "insert INTO [dbo].[HC_EVOLUCION_ENFERMERIA_DETALLE] ([EVO_CODIGO] ,[ID_USUARIO] ,[NOM_USUARIO] ,[EVD_FECHA] ,[EVD_DESCRIPCION] ,[EVD_FECHA_INSERT],[ESTADO])\n" +
                         "VALUES((SELECT ee.EVO_CODIGO FROM [HC_EVOLUCION_ENFERMERA] ee WHERE ATE_CODIGO = " + values[0] + " ) ," + values[1] + ",'" + values[2] + "','" + values[3] + "'\n" +
                               ",'" + values[4] + "' , GETDATE(),1)");
                    //query += ("  IF not EXISTS(SELECT * FROM [HC_EVOLUCION_ENFERMERA] WHERE ATE_CODIGO = " + values[0] + " ) \n" +
                    //   "INSERT INTO[dbo].[HC_EVOLUCION_ENFERMERA](ATE_CODIGO, [PAC_CODIGO],[EVO_FECHA_CREACION] ,[ID_USUARIO],[NOM_USUARIO])\n" +
                    //    "VALUES(" + values[0] + ",(SELECT a.PAC_CODIGO FROM [His3000].[dbo].ATENCIONES a where a.ATE_CODIGO= " + values[0] + ") ,GETDATE()\n" +
                    //    "		," + values[1] + ",'" + values[2] + "'   )\n" +
                    //    "insert INTO [dbo].[HC_EVOLUCION_ENFERMERIA_DETALLE] ([EVO_CODIGO] ,[ID_USUARIO] ,[NOM_USUARIO] ,[EVD_FECHA] ,[EVD_DESCRIPCION] ,[EVD_FECHA_INSERT])\n" +
                    //     "VALUES((SELECT ee.EVO_CODIGO FROM [HC_EVOLUCION_ENFERMERA] ee WHERE ATE_CODIGO = " + values[0] + " ) ," + values[1] + ",'" + values[2] + "','" + values[3] + "'\n" +
                    //           ",'" + values[4] + "' , GETDATE())");
                    break;
                case "SetAgendamientoImagen":
                    query = ("UPDATE [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS] " +
                    "SET[30X40] =  " + values[7] +
                    "    ,[8x10] =  " + values[8] +
                    "    ,[14x14] = " + values[9] +
                    "    ,[14x17] =  " + values[10] +
                    "    ,[18x24] =  " + values[11] +
                    "    ,[ODONT] =  " + values[12] +
                    "    ,[DANADAS] =  " + values[13] +
                    "    ,[ENVIADAS] =  " + values[14] +
                    "    ,[FECHA_REALIZADO] =  '" + values[6] +
                    "'   ,[MEDIO_CONTRASTE] = " + values[15] +
                    "    ,[CD] = " + values[17].ToString() +
                    "    , [DVD] = " + values[18].ToString() +
                    "    ,[ID_USUARIO] = " + values[16] +
                    " WHERE[id_imagenologia_agendamientos] = " + values[0] +
                    "     AND[CUE_CODIGO] = " + values[1] + "\n" +
                    "INSERT INTO [dbo].[HC_IMAGENOLOGIA_AGENDAMIENTOS_ESTUDIOS_JN] ([id_imagenologia_agendamientos],[CUE_CODIGO],[30X40],[8x10],[14x14],[14x17],[18x24],[ODONT],[DANADAS],[ENVIADAS],[FECHA_REALIZADO],[MEDIO_CONTRASTE],[ID_USUARIO]) " +
                    "VALUES (   " + values[0] +
                    " , " + values[1] +
                    " , " + values[7] +
                    " , " + values[8] +
                    " , " + values[9] +
                    " , " + values[10] +
                    " , " + values[11] +
                    " , " + values[12] +
                    " , " + values[13] +
                    " , " + values[14] +
                    " , '" + values[6] +
                    "', " + values[15] +
                    " , " + values[16] + ")  ");
                    break;
                case "AsientoContable":
                    query = ("INSERT INTO Cg3000.[dbo].[CgContabilidad] ([numing] ,[tipord],[fecha] ,[linea] ,[codcue] ,[columna] ,[valor],[codlocal] ,[codzona] ,[estado] ,[fecaux] ,[comentario] ,[cuecliente] " +
                    ",[nocomp] ,[numche] ,[tipmov] ,[codcentrocosto] ,[codrubro] ,[codactividad],[autorizacion],[fechacad],[observa],[fecpago],[codretencion],[cajachica], [forpag], [FOR_DESCRIPCION], [HOM_CODIGO], [codusu]) VALUES( \n" +
                    " " + values[0] +
                    " , " + values[1] +
                    " , '" + values[2] +
                    "' , " + values[3] +
                    " , '" + values[4] +
                    "' , '" + values[5] +
                    "' , '0" + values[6] +
                    "' , " + values[7] +
                    " , " + values[8] +
                    " , '" + values[9] +
                    "' , '" + values[10] +
                    "' , '" + values[11] +
                    "' , '" + values[12] +
                    "' , '" + values[13] +
                    "' , '" + values[14] +
                    "' , '" + values[15] +
                    "' , " + values[16] +
                    " , " + values[17] +
                    " , " + values[18] +
                    " , '" + values[19] +
                    "' , '" + values[20] +
                    "' , '" + values[21] +
                    "' , '" + values[22] +
                    "' , '" + values[23] +
                    "' , " + values[24] +
                    " , '" + values[25] +  //FORPAG
                    "' , '" + values[26] + //FOR_DESCRIPCION
                    "', " + values[27] +
                    ", " + values[28] +
                    ")  ");
                    break;
                case "KardexMedicamentos_ItemsCuentaPaciente":
                    query = ("INSERT INTO [dbo].[KARDEX_INSUMOS]   ([ATE_CODIGO] ,[CUE_CODIGO] ,[PRO_CODIGO] ,[PRO_DESCRIPCION] ) " +
                    "VALUES (" + values[0] +
                    " , '" + values[2] +
                    "' , '" + values[1] +
                    "' , '" + values[3] +
                    "')  ");




                    break;
                case "SetPedidoDietetica":
                    query = ("IF not EXISTS(SELECT id FROM [TEMP_DIETETICA] WHERE ATE_CODIGO = " + values[0] + " ) "
                   + "INSERT INTO [dbo].[TEMP_DIETETICA] ([ATE_CODIGO],[PRO_CODIGO],[observacion] ,[PRO_NOMBRE], [diagnostico])"
                           + "VALUES(" + values[0] + ", " + values[1] + ", '" + values[2] + "', '"
                           + values[3] + ", " + values[4] + "') "
                   + "ELSE "
                       + "UPDATE TEMP_DIETETICA SET PRO_CODIGO = " + values[1] + ", "
                       + "observacion = '" + values[2] + "', "
                       + "PRO_NOMBRE = '" + values[3] + "' ,"
                       + "diagnostico = '" + values[4] + "' "
                       + "WHERE ATE_CODIGO = " + values[0] + "  ");
                    break;
                case "DeleteTempPedidoDietetica":
                    query = ("DELETE FROM TEMP_DIETETICA WHERE  ATE_CODIGO = " + code + " ");
                    break;
                case "DeleteHorario":
                    query = ("DELETE FROM HORARIO_MEDICOS WHERE  Id = " + code + " ");
                    break;
                case "DeleteTempPedidosDietetica":
                    query = ("DELETE FROM TEMP_DIETETICA ");
                    break;

                case "InsertHorarioMedico":
                    query = ("INSERT INTO [dbo].[HORARIO_MEDICOS] ([MED_CODIGO],[INGRESO],[SALIDA]) VALUES( " + values[0] +
                            ",'" + values[1] + "','" + values[2] + "') ");
                    break;


                case "setBitacora":
                    query = ("DELETE FROM [dbo].[Bitacora_Admisiones] WHERE TIPO='" + values[1] + "' AND ATE_CODIGO= " + values[0] + "  \n" +
                            "INSERT INTO[dbo].[Bitacora_Admisiones] ([ATE_CODIGO] ,[TIPO] ,[OBSERVACION] ,[ID_USUARIO]) VALUES( " + values[0] +
                            ",'" + values[1] + "','" + values[2] + "'," + values[3] + ") ");
                    break;
                case "setObservacionAtencion":
                    query = ("UPDATE [dbo].[ATENCIONES]\n" +
                               "SET [ATE_OBSERVACIONES] = '" + values[0] + "'\n" +
                             "WHERE ATE_CODIGO= " + code + " ");
                    break;
                case "setAsCategoria0":
                    query = ("INSERT INTO [dbo].[ATENCION_DETALLE_CATEGORIAS] ([ADA_CODIGO] ,[ATE_CODIGO] ,[CAT_CODIGO] ,[ADA_FECHA_INICIO] ,[ADA_ORDEN] ,[ADA_ESTADO]) VALUES ( (select max(ADA_CODIGO)+1 from [dbo].[ATENCION_DETALLE_CATEGORIAS]) ,\n" +
                        "(select ATE_CODIGO from ATENCIONES where ATE_NUMERO_ATENCION=" + code + ")" + "," + values[0].ToString() + " ,GETDATE() ,1 ,1) ");
                    break;
                case "DxReferencia":
                    query = ("UPDATE [dbo].[HC_REFERENCIA]\n" +
                            "   SET[FECHA] = '" + values[1] + "'\n" +
                            "      ,[ESTABLECIMIENTO] = '" + values[2] + "'\n" +
                            "      ,[SERVICIO] = '" + values[3] + "'\n" +
                            "      ,[MED_CODIGO] = " + values[4] + "\n" +
                            "      ,[MOTIVO] = '" + values[5] + "'\n" +
                            "      ,[RESUMEN] = '" + values[6] + "'\n" +
                            "      ,[HALLAZGOS] = '" + values[7] + "'\n" +
                            "      ,[TRATAMIENTO] = '" + values[8] + "'\n" +
                             "WHERE id = " + values[0] + " ");
                    break;
                case "DxContrareferencia":
                    query = ("UPDATE [dbo].[HC_CONTRAREFERENCIA]\n" +
                            "   SET[FECHA] = '" + values[1] + "'\n" +
                            "      ,[ESTABLECIMIENTO] = '" + values[2] + "'\n" +
                            "      ,[SERVICIO] = '" + values[3] + "'\n" +
                            "      ,[MED_CODIGO] = " + values[4] + "\n" +
                            "      ,[RESUMEN] = '" + values[5] + "'\n" +
                            "      ,[HALLAZGOS] = '" + values[6] + "'\n" +
                            "      ,[TRATAMIENTO_REALIZADO] = '" + values[7] + "'\n" +
                            "      ,[TRATAMIENTO_RECOMENDADO] = '" + values[8] + "'\n" +
                             "WHERE id = " + values[0] + " ");
                    break;


                case "setDetalleGarantia":

                    int x = 0;
                    if (values[3] != null)
                    {
                        x = Convert.ToInt32(values[3]);
                    }
                    string xUser = "";
                    if (Convert.ToBoolean(values[13]) == true)
                    {
                        xUser = "    ,[ADG_USER] = '" + Convert.ToString(values[12]) + "' ";
                    }


                    query = ("UPDATE [dbo].[ATENCION_DETALLE_GARANTIAS]\n" +
                             "  SET[ADG_BANCO] = '" + values[0] + "'\n" +
                              "    ,[ADG_TIPOTARJETA] = '" + values[1] + "'\n" +
                             "     ,[ADG_CCV] = '" + values[2] + "'\n" +
                            "      ,[ADG_DIASVENCIMIENTO] = " + x + "\n" +
                           "       ,[ADG_CADUCIDAD] = '" + values[4] + "'\n" +
                          "        ,[ADG_LOTE] = '" + values[5] + "'\n" +
                         "         ,[ADG_AUTORIZACION] = '" + values[6] + "'\n" +
                                 " ,[ADG_NUMERO_AUT] = '" + values[7] + "'\n" +
                                "  ,[ADG_PERSONA_AUT] = '" + values[8] + "'\n" +
                                "   ,[ADG_FECHA_AUT] = '" + values[9] + "' \n" +
                              "    ,[ADG_ESTABLECIMIENTO] = '" + values[10] + "'\n" +
                               "    ,[ADG_NROTARJETA] = '" + values[11] + "'\n" + xUser +
                               ", [ADG_IDENTIFICACION] = '" + values[14] + "'\n" +
                               ",[ADG_TELEFONOS] = '" + values[15] + "'\n" +
                             "WHERE[ADG_CODIGO] = " + code + " ");
                    break;



                default:
                    query = ("Nothing");
                    break;
            }

            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(query, Sqlcon);

            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public DataTable getExploradorRubrosNew(DateTime desde, DateTime hasta, string hc,
            bool ingreso, bool alta, bool area, int pea_codigo, bool subarea, int rub_codigo,
            bool facturacion, bool departamento, string coddep)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();
            string xWhere = "WHERE ";

            if (ingreso)
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "CP.CUE_FECHA BETWEEN @desde AND DATEADD(DAY,1,@hasta)\r\n";
                else
                    xWhere = xWhere + " AND CP.CUE_FECHA BETWEEN @desde AND @hasta\r\n";
            }
            if (alta)
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "CP.CUE_FECHA BETWEEN @desde AND @hasta\r\n";
                else
                    xWhere = xWhere + " AND CP.CUE_FECHA BETWEEN @desde AND @hasta\r\n";
            }
            if (hc != "0")
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "P.PAC_HISTORIA_CLINICA = @hc\r\n";
                else
                    xWhere = xWhere + " AND P.PAC_HISTORIA_CLINICA = @hc\r\n";
            }
            if (area)
            {
                if (subarea)
                {
                    if (xWhere.Length <= 6)
                        xWhere = xWhere + "R.RUB_CODIGO = @rubro\r\n";
                    else
                        xWhere = xWhere + " AND R.RUB_CODIGO = @rubro\r\n";
                }
                else
                {
                    if (xWhere.Length <= 6)
                        xWhere = xWhere + "PA.PEA_CODIGO = @pea_codigo\r\n";
                    else
                        xWhere = xWhere + " AND PA.PEA_CODIGO = @pea_codigo\r\n";
                }
            }
            if (subarea && !area)
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "R.RUB_CODIGO = @rubro\r\n";
                else
                    xWhere = xWhere + " AND R.RUB_CODIGO = @rubro\r\n";
            }
            if (departamento)
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "(SELECT TOP 1 SPD.coddep " +
                    "FROM Sic3000..ProductoSubdivision SPS " +
                    "INNER JOIN Sic3000..ProductoDepartamento SPD ON SPS.codsub = SPD.codSub " +
                    "WHERE SP.codsub = SPS.codsub) = @coddep\r\n";
                else
                    xWhere = xWhere + " AND (SELECT TOP 1 SPD.coddep " +
                   "FROM Sic3000..ProductoSubdivision SPS " +
                   "INNER JOIN Sic3000..ProductoDepartamento SPD ON SPS.codsub = SPD.codSub " +
                   "WHERE SP.codsub = SPS.codsub) = @coddep\r\n";
            }
            command = new SqlCommand("SELECT P.PAC_HISTORIA_CLINICA AS HC, P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,\n" +
            "A.ATE_NUMERO_ATENCION AS ATENCION, H.hab_Numero AS HAB, A.ATE_FECHA_INGRESO AS 'F. INGRESO', A.ATE_FECHA_ALTA AS 'F. ALTA',\n" +
            "M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,\n" +
            "CP.CUE_FECHA AS 'F. PEDIDO', ISNULL((SELECT CONVERT(varchar,PED_FECHA,8) FROM PEDIDOS WHERE PED_CODIGO = CP.Codigo_Pedido),'00:00:00') AS 'H. PEDIDO',  CP.Codigo_Pedido AS 'N° PEDIDO', U.APELLIDOS + ' ' + U.NOMBRES AS 'SOLICITADO POR',\n" +
            "PA.PEA_NOMBRE AS AREA, R.RUB_NOMBRE AS SUBAREA, " +
            "(SELECT TOP 1 SPD.desdep FROM  Sic3000..ProductoDepartamento SPD WHERE SPD.coddep = sp.coddep)AS DEPARTAMENTO,\n" +
            "SP.despro AS PRODUCTO, SP.codpro AS 'COD. PRODUCTO', A.ATE_FACTURA_PACIENTE AS 'FACTURA',\n" +
            "A.ATE_FACTURA_FECHA AS 'F. FACTURA', CP.CUE_VALOR_UNITARIO AS 'V. UNITARIO', CP.CUE_CANTIDAD AS CANTIDAD, (CP.CUE_CANTIDAD * CUE_VALOR_UNITARIO) AS VALOR,\n" +
            "CP.CUE_IVA AS IVA, ((CP.CUE_CANTIDAD * CUE_VALOR_UNITARIO) + CP.CUE_IVA) AS TOTAL, CP.Descuento AS DESCUENTO, CC.CAT_NOMBRE AS SEGURO, TR.TIR_NOMBRE AS REFERIDO, TIA.name AS 'T. ATENCION' \n" +
            "FROM Sic3000..Producto SP\n" +
            "INNER JOIN CUENTAS_PACIENTES CP ON SP.codpro = CP.PRO_CODIGO\n" +
            "INNER JOIN ATENCIONES A ON CP.ATE_CODIGO = A.ATE_CODIGO\n" +
            "INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO\n" +
            "INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO\n" +
            "INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo\n" +
            "INNER JOIN USUARIOS U ON CP.ID_USUARIO = U.ID_USUARIO\n" +
            "INNER JOIN RUBROS R ON CP.RUB_CODIGO = R.RUB_CODIGO\n" +
            "INNER JOIN PEDIDOS_AREAS PA ON R.PED_CODIGO = PA.DIV_CODIGO\n" +
            "INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO\n" +
            "INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO\n" +
            "INNER JOIN TIPO_REFERIDO TR ON A.TIR_CODIGO = TR.TIR_CODIGO  LEFT JOIN tipos_atenciones TIA ON A.TipoAtencion = TIA.id " + xWhere, connection);

            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@pea_codigo", pea_codigo);
            command.Parameters.AddWithValue("@rubro", rub_codigo);
            command.Parameters.AddWithValue("@coddep", coddep);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable getExploradorRubrosNew2(DateTime desde, DateTime hasta, string hc, bool ingreso,
    bool pisos, bool habitacion, Int64 hab_Codigo, Int64 NIV_CODIGO)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();
            string xWhere = "WHERE ";
            if (ingreso)
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "cast(convert(varchar(11),p.PED_FECHA,13) as datetime)>= @desde And cast(convert(varchar(11),p.PED_FECHA,13) as datetime)<= @hasta\r\n";
                else
                    xWhere = xWhere + " AND cast(convert(varchar(11),p.PED_FECHA,13) as datetime)>= @desde And cast(convert(varchar(11),p.PED_FECHA,13) as datetime)<= @hasta\r\n";
            }
            if (pisos)
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "h.NIV_CODIGO = @piso\r\n";
                else
                    xWhere = xWhere + " AND h.NIV_CODIGO= @piso\r\n";
            }
            if (habitacion)
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "h.hab_Codigo = @habitacion\r\n";
                else
                    xWhere = xWhere + " AND h.hab_Codigo = @habitacion\r\n";
            }
            if (hc != "0")
            {
                if (xWhere.Length <= 6)
                    xWhere = xWhere + "PC.PAC_HISTORIA_CLINICA = @hc\r\n";
                else
                    xWhere = xWhere + " AND PC.PAC_HISTORIA_CLINICA = @hc\r\n";
            }
            command = new SqlCommand("select  PC.PAC_HISTORIA_CLINICA AS HC, PC.PAC_APELLIDO_PATERNO + ' ' + PC.PAC_APELLIDO_MATERNO + ' ' + PC.PAC_NOMBRE1 + ' ' + PC.PAC_NOMBRE2 AS PACIENTE,\n" +
            "A.ATE_NUMERO_ATENCION AS ATENCION, H.hab_Numero AS HAB, A.ATE_FECHA_INGRESO AS 'F. INGRESO', A.ATE_FECHA_ALTA AS 'F. ALTA',\n" +
            "M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,\n" +
            "p.PED_FECHA AS 'F. PEDIDO', '00:00:00' AS 'H. PEDIDO', p.PED_CODIGO AS 'N° PEDIDO',\n" +
            "U.APELLIDOS + ' ' + U.NOMBRES AS 'SOLICITADO POR','' AS AREA, '' AS SUBAREA, '' AS DEPARTAMENTO,\n" +
            "SP.despro AS PRODUCTO, SP.codpro AS 'COD. PRODUCTO', A.ATE_FACTURA_PACIENTE AS 'FACTURA',\n" +
            "A.ATE_FACTURA_FECHA AS 'F. FACTURA', pd.PDD_VALOR AS 'V. UNITARIO', pd.PDD_CANTIDAD AS CANTIDAD, (pd.PDD_CANTIDAD * pd.PDD_VALOR) AS VALOR,\n" +
            "pd.PDD_IVA AS IVA, ((pd.PDD_CANTIDAD * pd.PDD_VALOR) + pd.PDD_IVA) AS TOTAL, '0' AS DESCUENTO, '' AS SEGURO, '' AS REFERIDO, '' AS 'T. ATENCION' \n" +
            "FROM Sic3000..Producto SP\n" +
            "inner join PEDIDOS_DETALLE pd on sp.codpro = pd.PRO_CODIGO\n" +
            "inner join PEDIDOS p on pd.PED_CODIGO = p.PED_CODIGO\n" +
            "inner join ATENCIONES a on p.ATE_CODIGO = a.ATE_CODIGO\n" +
            "inner join HABITACIONES h on p.HAB_CODIGO = h.hab_Codigo\n" +
            //"inner join NIVEL_PISO n on h.NIV_CODIGO = n.NIV_CODIGO\n" +
            "inner join PACIENTES pc on a.PAC_CODIGO = pc.PAC_CODIGO\n" +
            "INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO\n" +
            "inner join USUARIOS u on p.ID_USUARIO = u.ID_USUARIO " + xWhere, connection);

            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@desde", desde);
            command.Parameters.AddWithValue("@hasta", hasta);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@piso", NIV_CODIGO);
            command.Parameters.AddWithValue("@habitacion", hab_Codigo);
            command.CommandTimeout = 380;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable getExploradorRubros(string desde, string hasta, bool ingreso, bool alta, bool facturacion, bool Fingreso, int Cod_Ingreso, bool Ftratamiento, int Cod_Tratamiento, bool Fhc, int hc, bool Fformulario, string formulario, bool departamento)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string xWhere = "";
            int count = 0;
            if (ingreso)
            {
                xWhere += "  ATENCIONES.ATE_FECHA_INGRESO BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (alta)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  ATENCIONES.ATE_FECHA_ALTA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (facturacion)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  ATENCIONES.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }

            if (departamento)
            {
                if (count > 0)
                    xWhere += " OR ";
                xWhere += "  ATENCIONES.ATE_FACTURA_FECHA BETWEEN '" + desde + "' AND '" + hasta + "' ";
                count++;
            }
            if (count > 0)
                xWhere = " (" + xWhere + ") ";


            if (Fingreso)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += " dbo.RUBROS.RUB_CODIGO=" + Cod_Tratamiento + " ";
                count++;
            }

            if (Ftratamiento)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += " dbo.PEDIDOS_AREAS.PEA_CODIGO=" + Cod_Ingreso + " ";
                count++;
            }

            if (Fhc)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  PACIENTES.PAC_HISTORIA_CLINICA=" + hc + " ";
                count++;
            }

            if (Fformulario)
            {
                if (count > 0)
                    xWhere += " AND ";
                xWhere += "  dbo.RUBROS.RUB_GRUPO='" + formulario + "' ";
                count++;
            }
            if (departamento)
            {

            }
            if (count > 0)
                xWhere = "Where " + xWhere;

            //        Sqlcmd = new SqlCommand("SELECT  dbo.PACIENTES.PAC_HISTORIA_CLINICA AS HC,  \n" +
            //                      "  concat(dbo.PACIENTES.PAC_APELLIDO_PATERNO, ' ', dbo.PACIENTES.PAC_APELLIDO_MATERNO, ' ', dbo.PACIENTES.PAC_NOMBRE1, ' ', dbo.PACIENTES.PAC_NOMBRE2) as Paciente,  \n" +
            //                     "   dbo.ATENCIONES.ATE_NUMERO_ATENCION AS ATE, dbo.HABITACIONES.hab_Numero AS HAB, dbo.ATENCIONES.ATE_FECHA_INGRESO, dbo.ATENCIONES.ATE_FECHA_ALTA, \n" +
            //                    "    concat(dbo.MEDICOS.MED_APELLIDO_PATERNO, ' ', dbo.MEDICOS.MED_NOMBRE1) as Medico \n" +
            //                   "     , dbo.CUENTAS_PACIENTES.CUE_FECHA AS Fecha_Pedido,dbo.CUENTAS_PACIENTES.Codigo_Pedido as Cod_Pedido, Concat(dbo.USUARIOS.NOMBRES, ' ', dbo.USUARIOS.APELLIDOS) AS Usuario_Solicita, dbo.PEDIDOS_AREAS.PEA_NOMBRE AS Area,  \n" +
            //                  "      dbo.RUBROS.RUB_NOMBRE AS Subarea, dbo.RUBROS.RUB_GRUPO AS Grupo_Facturacion, dbo.PRODUCTO.PRO_DESCRIPCION AS Producto, dbo.CUENTAS_PACIENTES.PRO_CODIGO,  \n" +
            //                 "      Sic3000.dbo.Nota.numfac as Fecha_Factura, Sic3000.dbo.Nota.fecha as Factura,        dbo.CUENTAS_PACIENTES.CUE_VALOR_UNITARIO, dbo.CUENTAS_PACIENTES.CUE_CANTIDAD, dbo.CUENTAS_PACIENTES.CUE_VALOR, dbo.CUENTAS_PACIENTES.CUE_IVA, (dbo.CUENTAS_PACIENTES.CUE_VALOR + dbo.CUENTAS_PACIENTES.CUE_IVA) as Total, dbo.CATEGORIAS_CONVENIOS.CAT_NOMBRE as Seguro, dbo.TIPO_REFERIDO.TIR_NOMBRE as Referido \n" +
            //                    "FROM  dbo.PRODUCTO INNER JOIN Sic3000.dbo.ProductoSubdivision ON dbo.PRODUCTO.PRO_CODSUB = Sic3000.dbo.ProductoSubdivision.codsub AND dbo.PRODUCTO.PRO_CODDIV = Sic3000.dbo.ProductoSubdivision.coddiv INNER JOIN\n" +
            //"dbo.RUBROS ON Sic3000.dbo.ProductoSubdivision.Pea_Codigo_His = dbo.RUBROS.RUB_CODIGO INNER JOIN    dbo.PEDIDOS_AREAS ON dbo.RUBROS.PED_CODIGO = dbo.PEDIDOS_AREAS.DIV_CODIGO INNER JOIN\n" +
            //"dbo.CUENTAS_PACIENTES ON dbo.PRODUCTO.PRO_CODIGO = dbo.CUENTAS_PACIENTES.PRO_CODIGO INNER JOIN    dbo.ATENCIONES ON dbo.CUENTAS_PACIENTES.ATE_CODIGO = dbo.ATENCIONES.ATE_CODIGO INNER JOIN\n" +
            //"dbo.PACIENTES ON dbo.ATENCIONES.PAC_CODIGO = dbo.PACIENTES.PAC_CODIGO INNER JOIN dbo.ATENCION_DETALLE_CATEGORIAS on dbo.ATENCIONES.ATE_CODIGO = dbo.ATENCION_DETALLE_CATEGORIAS.ATE_CODIGO INNER JOIN dbo.CATEGORIAS_CONVENIOS on dbo.ATENCION_DETALLE_CATEGORIAS.CAT_CODIGO = dbo.CATEGORIAS_CONVENIOS.CAT_CODIGO INNER JOIN dbo.TIPO_REFERIDO on dbo.ATENCIONES.TIR_CODIGO = dbo.TIPO_REFERIDO.TIR_CODIGO LEFT OUTER JOIN    Sic3000.dbo.Nota ON dbo.ATENCIONES.ATE_FACTURA_PACIENTE = Sic3000.dbo.Nota.numnot LEFT OUTER JOIN\n" +
            //"dbo.HABITACIONES ON dbo.ATENCIONES.HAB_CODIGO = dbo.HABITACIONES.hab_Codigo LEFT OUTER JOIN    dbo.USUARIOS ON dbo.CUENTAS_PACIENTES.ID_USUARIO = dbo.USUARIOS.ID_USUARIO LEFT OUTER JOIN    dbo.MEDICOS ON dbo.ATENCIONES.MED_CODIGO = dbo.MEDICOS.MED_CODIGO\n" + xWhere, Sqlcon);

            //CAMBIOS EDGAR 20210729 
            Sqlcmd = new SqlCommand("SELECT P.PAC_HISTORIA_CLINICA AS HC, P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS PACIENTE,\n" +
            "A.ATE_NUMERO_ATENCION AS ATENCION, H.hab_Numero AS HAB, A.ATE_FECHA_INGRESO AS 'F. INGRESO', A.ATE_FECHA_ALTA AS 'F. ALTA',\n" +
            "M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS MEDICO,\n" +
            "CP.CUE_FECHA AS 'F. PEDIDO', CP.Codigo_Pedido AS 'N° PEDIDO', U.APELLIDOS + ' ' + U.NOMBRES AS 'SOLICITADO POR',\n" +
            "PA.PEA_NOMBRE AS AREA, R.RUB_NOMBRE AS SUBAREA, (SELECT TOP 1 SPD.desdep FROM Sic3000..ProductoSubdivision SPS\n" +
            "INNER JOIN Sic3000..ProductoDepartamento SPD ON SPS.codsub = SPD.codSub\n" +
            "WHERE SP.codsub = SPS.codsub)AS DEPARTAMENTO,\n" +
            "SP.despro AS PRODUCTO, SP.codpro AS 'COD. PRODUCTO', A.ATE_FACTURA_PACIENTE AS 'FACTURA',\n" +
            "A.ATE_FACTURA_FECHA AS 'F. FACTURA', CP.CUE_VALOR_UNITARIO AS 'V. UNITARIO', CP.CUE_CANTIDAD AS CANTIDAD, (CP.CUE_CANTIDAD * CUE_VALOR_UNITARIO) AS VALOR,\n" +
            "CP.CUE_IVA AS IVA, ((CP.CUE_CANTIDAD * CUE_VALOR_UNITARIO) + CP.CUE_IVA) AS TOTAL, CC.CAT_NOMBRE AS SEGURO, TR.TIR_NOMBRE AS REFERIDO\n" +
            "FROM Sic3000..Producto SP\n" +
            "INNER JOIN CUENTAS_PACIENTES CP ON SP.codpro = CP.PRO_CODIGO\n" +
            "INNER JOIN ATENCIONES A ON CP.ATE_CODIGO = A.ATE_CODIGO\n" +
            "INNER JOIN PACIENTES P ON A.PAC_CODIGO = P.PAC_CODIGO\n" +
            "INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO\n" +
            "INNER JOIN HABITACIONES H ON A.HAB_CODIGO = H.hab_Codigo\n" +
            "INNER JOIN USUARIOS U ON CP.ID_USUARIO = U.ID_USUARIO\n" +
            "INNER JOIN RUBROS R ON CP.RUB_CODIGO = R.RUB_CODIGO\n" +
            "INNER JOIN PEDIDOS_AREAS PA ON R.PED_CODIGO = PA.DIV_CODIGO\n" +
            "INNER JOIN ATENCION_DETALLE_CATEGORIAS ADC ON A.ATE_CODIGO = ADC.ATE_CODIGO\n" +
            "INNER JOIN CATEGORIAS_CONVENIOS CC ON ADC.CAT_CODIGO = CC.CAT_CODIGO\n" +
            "INNER JOIN TIPO_REFERIDO TR ON A.TIR_CODIGO = TR.TIR_CODIGO" + xWhere, Sqlcon);


            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        public DataTable VerDatosDevolucion(Int64 codigobarras)
        {
            DataTable Tabla = new DataTable();
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("sp_VerDevolucionDatos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.Parameters.AddWithValue("@codigopedido", codigobarras);
            Sqlcmd.CommandTimeout = 180;
            Sqldap = Sqlcmd.ExecuteReader();
            Tabla.Load(Sqldap);
            Sqlcmd.Parameters.Clear();
            Sqlcon.Close();
            return Tabla;
        }
        public DataTable ProductosDevolucion(Int64 ped_codigo)
        {
            DataTable Tabla = new DataTable();
            SqlConnection conexion;
            SqlCommand command;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command = new SqlCommand("sp_AdmisionDevolucion", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ped_codigo", ped_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public void Devolucion(Int64 ate_codigo, string codigo_barra, Int64 codigo_pedido)
        {
            SqlConnection conexion;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command = new SqlCommand("sp_AdmisionDevolucionCambio", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@codigo_barra", codigo_barra);
            command.Parameters.AddWithValue("@codigo_pedido", codigo_pedido);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public bool DiasFestivos(DateTime Hoy)
        {
            bool ok;
            int dia = Hoy.Day;
            int mes = Hoy.Month;
            SqlConnection conexion;
            var command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "SELECT * FROM FESTIVIDADES WHERE FES_DIA = @dia AND FES_MES = @mes";
            command.Parameters.AddWithValue("@dia", dia);
            command.Parameters.AddWithValue("@mes", mes);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                ok = true;
            }
            else
            {
                ok = false;
            }
            return ok;
        }

        public void RecargoImagen(Int64 cue_codigo)
        {
            SqlConnection conexion;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command = new SqlCommand("sp_RecargoImagen", conexion);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cue_codigo", cue_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public DataTable getReferencias(Int64 atecodigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Sqlcmd = new SqlCommand("SELECT Id AS ID, '[MEDICO: ' + M.MED_APELLIDO_PATERNO + ' ' + M.MED_NOMBRE1 + '] - [MOTIVO: ' + R.MOTIVO + ']' AS DATOS FROM HC_REFERENCIA R INNER JOIN MEDICOS M ON R.MED_CODIGO = M.MED_CODIGO WHERE R.ATE_CODIGO = " + atecodigo, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable ReporteHonorarios(Int64 ate_codigo, Int64 codigo_pedido)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_ReporteHonorarios", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@codigo_pedido", codigo_pedido);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable HonorariosAsientosContables(DateTime fechaInicio, DateTime fechaFinal, bool porFuera, bool porSeguro, int valor)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();



            command = new SqlCommand("sp_HonorariosAsientoFiltro", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            command.Parameters.AddWithValue("@fechaFinal", fechaFinal);
            command.Parameters.AddWithValue("@porFuera", porFuera);
            command.Parameters.AddWithValue("@porSeguro", porSeguro);
            command.Parameters.AddWithValue("@id_usuario", His.Entidades.Clases.Sesion.codUsuario);
            command.Parameters.AddWithValue("@valor", valor);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
    }
}
