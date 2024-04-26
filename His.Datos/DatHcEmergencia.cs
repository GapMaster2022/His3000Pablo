using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatHcEmergencia
    {
        //. Recupera el numero mayor del codigo de Emergencias
        public int RecuperaMaximoHcEmergenciaCodigo()
        {          
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.HC_EMERGENCIA.Select(h => h.COD_EMERGENCIA).Count();
                    if (id > 0)
                        return contexto.HC_EMERGENCIA.Select(h => h.COD_EMERGENCIA).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }

        }
        //public int AtencionesCodigo()
        //{
        //    try
        //    {
        //        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    }

        //}


        public void CrearHcEmergencia(HC_EMERGENCIA emergencia)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA(emergencia);
                contexto.SaveChanges();
            }
        }

        ////. Recupera la lista por defecto de pacientes
        //public List<HC_EMERGENCIA> RecuperarEmergenciasLista()
        //{
        //    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {

        //        return (from p in contexto.HC_EMERGENCIA
        //                select p).ToList();


        //    }
        //}


        /// <summary>
        /// 
        /// Recupera la lista por Emergencias según código de paciente
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA> RecuperarHcEmergencias(int codigoPaciente)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {                      
                    return contexto.HC_EMERGENCIA.Include("PACIENTES").Where(p => p.PACIENTES.PAC_CODIGO == codigoPaciente).ToList();
                   
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public HC_EMERGENCIA RecuperarUltimaEmergencia()
        {
            try
            {
                using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA.Include("PACIENTES").OrderByDescending(p => p.COD_EMERGENCIA).FirstOrDefault();    
                }
            }
            catch (Exception err)
            { throw err; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoPaciente"></param>
        /// <returns></returns>
        public HC_EMERGENCIA RecuperarUltimaEmergenciaPorPaciente(int codigoPaciente)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA.Include("PACIENTES").Where(p=>p.PACIENTES.PAC_CODIGO==codigoPaciente).OrderByDescending(p => p.COD_EMERGENCIA).FirstOrDefault();
                }
            }
            catch (Exception err)
            { throw err; }
        }
        /// <summary>
        /// Permite modificar una Emergencia Existente
        /// </summary>
        /// <param name="emergenciaNueva"></param>
        public void ActualizarHcEmergencia(HC_EMERGENCIA emergenciaNueva)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA emergenciaOriginal = contexto.HC_EMERGENCIA.FirstOrDefault(hc => hc.COD_EMERGENCIA == emergenciaNueva.COD_EMERGENCIA);
                    emergenciaOriginal.EME_ALCOHOL = emergenciaNueva.EME_ALCOHOL;
                    emergenciaOriginal.EME_TABACO = emergenciaNueva.EME_TABACO;
                    emergenciaOriginal.EME_DROGAS = emergenciaNueva.EME_DROGAS;
                    emergenciaOriginal.EME_ALERGIAS = emergenciaNueva.EME_ALERGIAS;
                    emergenciaOriginal.EME_FUM = emergenciaNueva.EME_FUM;
                    emergenciaOriginal.EME_FECHA_INICIO = emergenciaNueva.EME_FECHA_INICIO;
                    emergenciaOriginal.HCC_CAT_ATENCION = emergenciaNueva.HCC_CAT_ATENCION;
                    emergenciaOriginal.HCC_CAT_PROCEDIMIENTO = emergenciaNueva.HCC_CAT_PROCEDIMIENTO;
                    emergenciaOriginal.HCC_CAT_TRATAMIENTO = emergenciaNueva.HCC_CAT_TRATAMIENTO;
                    emergenciaOriginal.EME_DIRECTO = emergenciaNueva.EME_DIRECTO;
                    emergenciaOriginal.EME_INDIRECTO = emergenciaNueva.EME_INDIRECTO;
                    emergenciaOriginal.EME_T = emergenciaNueva.EME_T;
                    emergenciaOriginal.EME_TA = emergenciaNueva.EME_TA;
                    emergenciaOriginal.EME_SAT = emergenciaNueva.EME_SAT;
                    emergenciaOriginal.EME_FC = emergenciaNueva.EME_FC;
                    emergenciaOriginal.EME_FR = emergenciaNueva.EME_FR;
                    emergenciaOriginal.EME_GLASGOW = emergenciaNueva.EME_GLASGOW;
                    emergenciaOriginal.EME_NOURGE = emergenciaNueva.EME_NOURGE;
                    emergenciaOriginal.EME_EMERGENCIA = emergenciaNueva.EME_EMERGENCIA;
                    emergenciaOriginal.EME_CRITICO = emergenciaNueva.EME_CRITICO;
                    emergenciaOriginal.EME_OTRAS = emergenciaNueva.EME_OTRAS;
                    emergenciaOriginal.EME_ALCOHOL_ACTUAL = emergenciaNueva.EME_ALCOHOL_ACTUAL;
                    emergenciaOriginal.EME_TABACO_ACTUAL = emergenciaNueva.EME_TABACO_ACTUAL;
                    emergenciaOriginal.EME_DROGAR_ACTUAL = emergenciaNueva.EME_DROGAR_ACTUAL;
                    emergenciaOriginal.EME_OTRAS_ACTUAL = emergenciaNueva.EME_OTRAS_ACTUAL;
                    emergenciaOriginal.EME_ORL = emergenciaNueva.EME_ORL;
                    emergenciaOriginal.EME_CABEZA = emergenciaNueva.EME_CABEZA;
                    emergenciaOriginal.EME_CUELLO = emergenciaNueva.EME_CUELLO;
                    emergenciaOriginal.EME_TORAX = emergenciaNueva.EME_TORAX;
                    emergenciaOriginal.EME_CARDIACO = emergenciaNueva.EME_CARDIACO;
                    emergenciaOriginal.EME_PULMONAR = emergenciaNueva.EME_PULMONAR;
                    emergenciaOriginal.EME_ABDOMEN = emergenciaNueva.EME_ABDOMEN;
                    emergenciaOriginal.EME_GENETALES = emergenciaNueva.EME_GENETALES;
                    emergenciaOriginal.EME_PERINE = emergenciaNueva.EME_PERINE;
                    emergenciaOriginal.EME_EXTREMIDADES = emergenciaNueva.EME_EXTREMIDADES;
                    emergenciaOriginal.EME_FOSAS_LUM = emergenciaNueva.EME_FOSAS_LUM;
                    emergenciaOriginal.EME_LINFATICO = emergenciaNueva.EME_LINFATICO;
                    emergenciaOriginal.EME_NEUROLOGICO = emergenciaNueva.EME_NEUROLOGICO;
                    emergenciaOriginal.EME_PIEL = emergenciaNueva.EME_PIEL;
                    emergenciaOriginal.EME_OBSERVACIONES = emergenciaNueva.EME_OBSERVACIONES;
                    emergenciaOriginal.EME_DIAG_EMER = emergenciaNueva.EME_DIAG_EMER;
                    emergenciaOriginal.EME_DIAG_DEFENITICO = emergenciaNueva.EME_DIAG_DEFENITICO;
                    emergenciaOriginal.ESP_CLIN = emergenciaNueva.ESP_CLIN;
                    emergenciaOriginal.ESP_QUIR = emergenciaNueva.ESP_QUIR;
                    emergenciaOriginal.ESP_OTRAS = emergenciaNueva.ESP_OTRAS;
                    emergenciaOriginal.EME_BUSQUEDA_ICD = emergenciaNueva.EME_BUSQUEDA_ICD;
                    emergenciaOriginal.EME_TRAUMA = emergenciaNueva.EME_TRAUMA;
                    emergenciaOriginal.EME_INFECCIONES = emergenciaNueva.EME_INFECCIONES;
                    emergenciaOriginal.EME_CARDIOVASCULAR = emergenciaNueva.EME_CARDIOVASCULAR;
                    emergenciaOriginal.EME_QUIRURGUCAS = emergenciaNueva.EME_QUIRURGUCAS;
                    emergenciaOriginal.EME_OTRAS_DIS = emergenciaNueva.EME_OTRAS_DIS;
                    emergenciaOriginal.EME_REFERIDO = emergenciaNueva.EME_REFERIDO;
                    emergenciaOriginal.EME_PRIVADO = emergenciaNueva.EME_PRIVADO;
                    emergenciaOriginal.EME_ALTA = emergenciaNueva.EME_ALTA;
                    emergenciaOriginal.EME_PISO = emergenciaNueva.EME_PISO;
                    emergenciaOriginal.EME_QUIR = emergenciaNueva.EME_QUIR;
                    emergenciaOriginal.EME_CORON = emergenciaNueva.EME_CORON;
                    emergenciaOriginal.EME_UCI = emergenciaNueva.EME_UCI;
                    emergenciaOriginal.EME_MUERTO = emergenciaNueva.EME_MUERTO;
                    emergenciaOriginal.COD_TRANSFERIDO = emergenciaNueva.COD_TRANSFERIDO;
                    emergenciaOriginal.EME_TRANSFERIDO = emergenciaNueva.EME_TRANSFERIDO;
                    emergenciaOriginal.EME_TRANFERIDO = emergenciaNueva.EME_TRANFERIDO;
                    emergenciaOriginal.EME_FECHA_ESTANCIA = emergenciaNueva.EME_FECHA_ESTANCIA;
                    emergenciaOriginal.EME_VIVO = emergenciaNueva.EME_VIVO;
                    emergenciaOriginal.EME_MUERTO_ESTADO = emergenciaNueva.EME_MUERTO_ESTADO;
                    emergenciaOriginal.EME_TRANFERIDO = emergenciaNueva.EME_TRANFERIDO;
                    emergenciaOriginal.EME_OBSER_FINAL = emergenciaNueva.EME_OBSER_FINAL;
                    emergenciaOriginal.COD_VIDEOS = emergenciaNueva.COD_VIDEOS;
                    emergenciaOriginal.COD_IMAGENES = emergenciaNueva.COD_IMAGENES;
                    emergenciaOriginal.EME_ESTADO = emergenciaNueva.EME_ESTADO;
                    emergenciaOriginal.EME_RESUMEN = emergenciaNueva.EME_RESUMEN;
                    emergenciaOriginal.EME_PROCEDIMIENTOS = emergenciaNueva.EME_PROCEDIMIENTOS;
                    emergenciaOriginal.EME_EVO_COMP = emergenciaNueva.EME_EVO_COMP;
                    emergenciaOriginal.EME_TRATAMIENTOS = emergenciaNueva.EME_TRATAMIENTOS;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }           
        }

        /// <summary>
        /// recupera la mergencia  segun la  atencion
        /// </summary>
        /// <param name="codAtencion"></param>
        public HC_EMERGENCIA recuperaremergenciaPorAtencion(Int64 codAtencion)
        {
            HC_EMERGENCIA emeregencia;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                emeregencia = (from d in contexto.HC_EMERGENCIA
                             where d.ATE_CODIGO == codAtencion
                             select d).FirstOrDefault();
                return emeregencia;
            }
        }
        public HC_EMERGENCIA_FORM recuperaremergenciaFormPorAtencion(Int64 codAtencion)
        {
            HC_EMERGENCIA_FORM emeregencia;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                emeregencia = (from d in contexto.HC_EMERGENCIA_FORM
                             where d.ATENCIONES.ATE_CODIGO == codAtencion
                             select d).FirstOrDefault();
                return emeregencia;
            }
        }
        public void cerrarHcEmergencia(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA emergenciaOriginal = contexto.HC_EMERGENCIA.FirstOrDefault(hc => hc.COD_EMERGENCIA == codigoEmergencia);
                    emergenciaOriginal.EME_ESTADO = true;
                    contexto.SaveChanges();
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Permite eliminar Emergencias según el código
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergencia(HC_EMERGENCIA emergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA emerg = contexto.HC_EMERGENCIA.Where(e => e.COD_EMERGENCIA == emergencia.COD_EMERGENCIA).FirstOrDefault();
                    contexto.DeleteObject(emerg);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<DtoHcEmergencias> RecuperarHcEmergenciasPorFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    //return contexto.HC_EMERGENCIA.Where(e => (e.EME_FECHA >= fechaInicio && e.EME_FECHA <= fechaFinal)).ToList();                    
                    return (from e in contexto.HC_EMERGENCIA
                            join p in contexto.PACIENTES on e.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                            where e.EME_FECHA >= fechaInicio && e.EME_FECHA <= fechaFinal
                            select new DtoHcEmergencias
                            {
                                codEmergencia = e.COD_EMERGENCIA,
                                codPaciente = p.PAC_CODIGO,
                                nombrePaciente = p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
                                apellidoPaciente = p.PAC_APELLIDO_PATERNO + "" + p.PAC_APELLIDO_MATERNO,
                                genero = p.PAC_GENERO,
                                estado = p.PAC_ESTADO,
                                urgente = (bool)e.EME_NOURGE,
                                emergente = (bool)e.EME_EMERGENCIA,
                                critico = (bool)e.EME_CRITICO,
                                otras = e.EME_OTRAS,
                                clinicas = (int)e.ESP_CLIN,
                                quirur = (int)e.ESP_QUIR,
                                otrasEsp = (int)e.ESP_OTRAS
                            }).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int ActualizaDiagnostico(string Diagnostico, Int32 CodigoAtencion)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
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

            Sqlcmd = new SqlCommand("sp_ActualizaDiagnosticoFinal", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@DescripcionDiagnostico", SqlDbType.VarChar);
            Sqlcmd.Parameters["@DescripcionDiagnostico"].Value = (Diagnostico);

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return 0;
        }

        public void ActualizaEstadoHoja08(Int32 CodigoAtencion)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
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

            Sqlcmd = new SqlCommand("sp_ActualizaEstadoHoja", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

        }
        public List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> cieDiagnostico(Int64 ATE_CODIGO, string tipo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from e in db.HC_EMERGENCIA_FORM
                        join d in db.HC_EMERGENCIA_FORM_DIAGNOSTICOS on e.EMER_CODIGO equals d.HC_EMERGENCIA_FORM.EMER_CODIGO
                        where e.ATENCIONES.ATE_CODIGO == ATE_CODIGO && d.ED_TIPO == tipo
                        select d).ToList();
            }
        }
    }
}
