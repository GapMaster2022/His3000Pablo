using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Datos;
using His.Entidades;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace His.Datos
{
    public class DatHcEmergenciaForm
    {

        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EMERGENCIA_FORM
                             select d.EMER_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void CrearHCEmergenciaF(HC_EMERGENCIA_FORM emergenciaF)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    //int codEmergenciaF;
                    //HC_EMERGENCIA_FORM codEmergencia = contexto.HC_EMERGENCIA_FORM.OrderByDescending(c => c.EMER_CODIGO).FirstOrDefault();
                    //codEmergenciaF = codEmergencia != null ? codEmergencia.EMER_CODIGO + 1 : 1;
                    //emergenciaF.EMER_CODIGO = codEmergenciaF;
                    contexto.AddToHC_EMERGENCIA_FORM(emergenciaF);
                    contexto.SaveChanges();
                    //return codEmergenciaF;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public void GuardarObservacionGeneral(int ate_codigo, string observacion)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
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
            command.CommandText = "update HC_EMERGENCIA_FORM set EMER_OBSER_GENERAL = @observacion where ATE_CODIGO = @atecodigo";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@atecodigo", ate_codigo);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }

        public string CargarObservacionGeneral(int ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            string observacion = "";
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
            command.CommandText = "select EMER_OBSER_GENERAL from HC_EMERGENCIA_FORM where ATE_CODIGO = @atecodigo";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@atecodigo", ate_codigo);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                observacion = reader["EMER_OBSER_GENERAL"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return observacion;
        }

        public void ModificarHCEmergenciaF(HC_EMERGENCIA_FORM emergenciaF)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_FORM emergenciaFOri = contexto.HC_EMERGENCIA_FORM.Where(e => e.EMER_CODIGO == emergenciaF.EMER_CODIGO).FirstOrDefault();
                    emergenciaFOri.EMER_TRAUMA = emergenciaF.EMER_TRAUMA;
                    emergenciaFOri.EMER_CAUSA_CLINICA = emergenciaF.EMER_CAUSA_CLINICA;
                    emergenciaFOri.EMER_CUASA_GO = emergenciaF.EMER_CUASA_GO;
                    emergenciaFOri.EMER_CUASA_Q = emergenciaF.EMER_CUASA_Q;
                    emergenciaFOri.EMER_OTRO_MOTIVO = emergenciaF.EMER_OTRO_MOTIVO;
                    emergenciaFOri.EMER_FECHA = emergenciaF.EMER_FECHA;
                    emergenciaFOri.EMER_FECHA_EVENTO = emergenciaF.EMER_FECHA_EVENTO;
                    emergenciaFOri.HCC_CODIGO_LE = emergenciaF.HCC_CODIGO_LE;
                    emergenciaFOri.EMER_DIRECCION_EVENTO = emergenciaF.EMER_DIRECCION_EVENTO;
                    emergenciaFOri.EMER_CUSTODIA_POLICIAL = emergenciaF.EMER_CUSTODIA_POLICIAL;
                    emergenciaFOri.HCC_CODIGO_AVIEQ = emergenciaF.HCC_CODIGO_AVIEQ;
                    emergenciaFOri.EMER_OBS_AVIEQ = emergenciaF.EMER_OBS_AVIEQ;
                    emergenciaFOri.EMER_ALIENTO_ETIL = emergenciaF.EMER_ALIENTO_ETIL;
                    emergenciaFOri.EMER_VALOR_ALCOH = emergenciaF.EMER_VALOR_ALCOH;
                    emergenciaFOri.EMER_VIA_AEREA = emergenciaF.EMER_VIA_AEREA;
                    emergenciaFOri.EMER_VIA_AEREA_OBT = emergenciaF.EMER_VIA_AEREA_OBT;
                    emergenciaFOri.EMER_COND_EST = emergenciaF.EMER_COND_EST;
                    emergenciaFOri.EMER_COND_INEST = emergenciaF.EMER_COND_INEST;
                    emergenciaFOri.EMER_ENFACT_OBS = emergenciaF.EMER_ENFACT_OBS;
                    emergenciaFOri.EMER_PRES_A = emergenciaF.EMER_PRES_A;
                    emergenciaFOri.EMER_PRES_B = emergenciaF.EMER_PRES_B;
                    emergenciaFOri.EMER_FREC_CARDIACA = emergenciaF.EMER_FREC_CARDIACA;
                    emergenciaFOri.EMER_FREC_RESPIRATORIA = emergenciaF.EMER_FREC_RESPIRATORIA;
                    emergenciaFOri.EMER_TEMP_BUCAL = emergenciaF.EMER_TEMP_BUCAL;
                    emergenciaFOri.EMER_TEMP_AXILAR = emergenciaF.EMER_TEMP_AXILAR;
                    emergenciaFOri.EMER_SATURA_OXI = emergenciaF.EMER_SATURA_OXI;
                    emergenciaFOri.EMER_PESO = emergenciaF.EMER_PESO;
                    emergenciaFOri.EMER_TALLA = emergenciaF.EMER_TALLA;
                    emergenciaFOri.EMER_GLASGOV = emergenciaF.EMER_GLASGOV;
                    emergenciaFOri.EMER_OCULAR = emergenciaF.EMER_OCULAR;
                    emergenciaFOri.EMER_VERBAL = emergenciaF.EMER_VERBAL;
                    emergenciaFOri.EMER_MOTORA = emergenciaF.EMER_MOTORA;
                    emergenciaFOri.EMER_PUP_DIAD = emergenciaF.EMER_PUP_DIAD;
                    emergenciaFOri.EMER_PUP_READ = emergenciaF.EMER_PUP_READ;
                    emergenciaFOri.EMER_PUP_DIAI = emergenciaF.EMER_PUP_DIAI;
                    emergenciaFOri.EMER_PUP_REAI = emergenciaF.EMER_PUP_REAI;
                    emergenciaFOri.EMER_GLICEMIA_CAPILAR = emergenciaF.EMER_GLICEMIA_CAPILAR;
                    emergenciaFOri.EMER_DOMICILIO = emergenciaF.EMER_DOMICILIO;
                    emergenciaFOri.EMER_CONS_EXT = emergenciaF.EMER_CONS_EXT;
                    emergenciaFOri.EMER_OBS_ALTA = emergenciaF.EMER_OBS_ALTA;
                    emergenciaFOri.EMER_INTER = emergenciaF.EMER_INTER;
                    emergenciaFOri.EMER_PREF = emergenciaF.EMER_PREF;
                    emergenciaFOri.EMER_SER_REF = emergenciaF.EMER_SER_REF;
                    emergenciaFOri.EMER_ESTAB = emergenciaF.EMER_ESTAB;
                    emergenciaFOri.EMER_EGRESA_VIVO = emergenciaF.EMER_EGRESA_VIVO;
                    emergenciaFOri.EMER_MUERTO_EMER = emergenciaF.EMER_MUERTO_EMER;
                    emergenciaFOri.EMER_CONDICION_EST = emergenciaF.EMER_CONDICION_EST;
                    emergenciaFOri.EMER_CONDICION_INES = emergenciaF.EMER_CONDICION_INES;
                    emergenciaFOri.EMER_DIAS_INCAP = emergenciaF.EMER_DIAS_INCAP;
                    emergenciaFOri.EMER_CAUSA_MUERTE = emergenciaF.EMER_CAUSA_MUERTE;
                    emergenciaFOri.ID_USUARIO = emergenciaF.ID_USUARIO;
                    emergenciaFOri.EMER_FECHA_E = emergenciaF.EMER_FECHA_E;
                    emergenciaFOri.EMER_HORA_E = emergenciaF.EMER_HORA_E;
                    emergenciaFOri.EMER_NOMBRE_PROF_E = emergenciaF.EMER_NOMBRE_PROF_E;
                    emergenciaFOri.EMER_CODIGO_PRO_E = emergenciaF.EMER_CODIGO_PRO_E;
                    //CAMPOS NUEVOS FORMULARIO 2021
                    emergenciaFOri.EMER_CL_ESTABLE = emergenciaF.EMER_CL_ESTABLE;
                    emergenciaFOri.EMER_CL_INESTABLE = emergenciaF.EMER_CL_INESTABLE;
                    emergenciaFOri.EMER_CL_FALLECIDO = emergenciaF.EMER_CL_FALLECIDO;
                    emergenciaFOri.EMER_CL_MOTIVO = emergenciaF.EMER_CL_MOTIVO;
                    emergenciaFOri.EMER_ACT_TRA = emergenciaF.EMER_ACT_TRA;
                    emergenciaFOri.EMER_VIO_ARM = emergenciaF.EMER_VIO_ARM;
                    emergenciaFOri.EMER_INT_ALC = emergenciaF.EMER_INT_ALC;
                    emergenciaFOri.EMER_CAIDA = emergenciaF.EMER_CAIDA;
                    emergenciaFOri.EMER_VIO_PUN = emergenciaF.EMER_VIO_PUN;
                    emergenciaFOri.EMER_INT_ALI = emergenciaF.EMER_INT_ALI;
                    emergenciaFOri.EMER_QUEMADURA = emergenciaF.EMER_QUEMADURA;
                    emergenciaFOri.EMER_VIO_RINA = emergenciaF.EMER_VIO_RINA;
                    emergenciaFOri.EMER_INT_DRO = emergenciaF.EMER_INT_DRO;
                    emergenciaFOri.EMER_MORDEDURA = emergenciaF.EMER_MORDEDURA;
                    emergenciaFOri.EMER_VIO_FAM = emergenciaF.EMER_VIO_FAM;
                    emergenciaFOri.EMER_INH_GAS = emergenciaF.EMER_INH_GAS;
                    emergenciaFOri.EMER_AHOGAMIENTO = emergenciaF.EMER_AHOGAMIENTO;
                    emergenciaFOri.EMER_PRE_FIS = emergenciaF.EMER_PRE_FIS;
                    emergenciaFOri.EMER_OTR_INT = emergenciaF.EMER_OTR_INT;
                    emergenciaFOri.EMER_CUE_EXT = emergenciaF.EMER_CUE_EXT;
                    emergenciaFOri.EMER_PRE_PSI = emergenciaF.EMER_PRE_PSI;
                    emergenciaFOri.EMER_PICADURA = emergenciaF.EMER_PICADURA;
                    emergenciaFOri.EMER_APLASTAMIENTO = emergenciaF.EMER_APLASTAMIENTO;
                    emergenciaFOri.EMER_PRE_SEX = emergenciaF.EMER_PRE_SEX;
                    emergenciaFOri.EMER_ENVENENAMIENTO = emergenciaF.EMER_ENVENENAMIENTO;
                    emergenciaFOri.EMER_OTR_ACC = emergenciaF.EMER_OTR_ACC;
                    emergenciaFOri.EMER_NOTIFICACION = emergenciaF.EMER_NOTIFICACION;
                    emergenciaFOri.EMER_ANAFILAXIA = emergenciaF.EMER_ANAFILAXIA;
                    emergenciaFOri.EMER_LLE_CAP = emergenciaF.EMER_LLE_CAP;
                    emergenciaFOri.EMER_SIN_VIT = emergenciaF.EMER_SIN_VIT;
                    emergenciaFOri.EMER_PERIMETRO = emergenciaF.EMER_PERIMETRO;
                    emergenciaFOri.EMER_OBS_EGR = emergenciaF.EMER_OBS_EGR;
                    emergenciaFOri.EMER_HOSPITALIZACION = emergenciaF.EMER_HOSPITALIZACION;
                    emergenciaFOri.EMER_REF_INV = emergenciaF.EMER_REF_INV;
                    emergenciaFOri.EMER_OBS_EGR = emergenciaF.EMER_OBS_EGR;
                    emergenciaFOri.EMER_GRU_PRIO = emergenciaF.EMER_GRU_PRIO;
                    emergenciaFOri.EMER_PRIORITARIO = emergenciaF.EMER_PRIORITARIO;
                    emergenciaFOri.EMER_DERIVACION = emergenciaF.EMER_DERIVACION;
                    emergenciaFOri.EMER_ALT_DEF = emergenciaF.EMER_ALT_DEF;
                    emergenciaFOri.EMER_OBS_EXA_FIS = emergenciaF.EMER_OBS_EXA_FIS;
                    emergenciaFOri.EMER_SCORE = emergenciaF.EMER_SCORE;

                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }





        public void EliminarHCEmergenciaF(HC_EMERGENCIA_FORM emergenciaF)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(emergenciaF);
            }
        }

        public List<HC_EMERGENCIA_FORM> listaHcEmergenciasF()
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_EMERGENCIA_FORM> listaEmergenciaF = new List<HC_EMERGENCIA_FORM>();
                //atenciones = contexto.ATENCIONES.Include("PACIENTES").Include("HABITACIONES").Include("CAJAS").OrderBy(p => p.ATE_FECHA).ToList();
                listaEmergenciaF = (from e in contexto.HC_EMERGENCIA_FORM
                                    join p in contexto.PACIENTES on e.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                    join a in contexto.ATENCIONES on e.ATENCIONES.ATE_CODIGO equals a.ATE_CODIGO
                                    orderby e.EMER_FECHA
                                    select e).ToList();
                return listaEmergenciaF;
            }
        }

        public HC_EMERGENCIA_FORM RecuperarHcEmergenciasFId(int codEmergF)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from e in contexto.HC_EMERGENCIA_FORM
                            where e.EMER_CODIGO == codEmergF
                            select e).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public HC_EMERGENCIA_FORM RecuperarHcEmergenciasFAten(Int64 codAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                        (from e in contexto.HC_EMERGENCIA_FORM
                         where e.ATENCIONES.ATE_CODIGO == codAtencion
                         select e).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }

        /// <summary>
        /// METODO PARA RECUPERAR LA ULTIMA EMERGENCIA
        /// </summary>
        /// <returns></returns>
        public HC_EMERGENCIA_FORM RecuperarUltimaEmergencia()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_FORM.ToList().LastOrDefault();
                    //Include("PACIENTES").OrderByDescending(p => p.EMER_CODIGO).FirstOrDefault();
                }
            }
            catch (Exception err)
            { throw err; }
        }

        public GRUPO_SANGUINEO RecuperarTipoSangre(int codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                GRUPO_SANGUINEO sangre = (from s in db.GRUPO_SANGUINEO
                                          where s.GS_CODIGO == codigo
                                          select s).FirstOrDefault();
                return sangre;
            }
        }


        public OBSTETRICA_CONSULTAEXTERNA RecuperarEObstetrica(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from o in db.OBSTETRICA_CONSULTAEXTERNA
                        where o.ATE_CODIGO == ate_codigo
                        select o).FirstOrDefault();
            }
        }
        public ATENCIONES RecuperarAtencionIDEmerg(int codigo)
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ATENCIONES.Include("PACIENTES").Include("PACIENTES_DATOS_ADICIONALES").Include("MEDICOS").Include("ATENCION_FORMAS_LLEGADA").Include("USUARIOS").FirstOrDefault(a => a.ATE_CODIGO == codigo);
                //return contexto.ATENCIONES.Include("ATENCION_FORMAS_LLEGADA").Include("MEDICOS").Include("USUARIOS").Where(a => a.PACIENTES.PAC_CODIGO == keyPaciente).First();
            }

        }

        public void cerrarHcEmergenciaForm(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_FORM emergenciaOriginal = contexto.HC_EMERGENCIA_FORM.FirstOrDefault(hc => hc.EMER_CODIGO == codigoEmergencia);
                    emergenciaOriginal.EMER_ESTADO = 1;
                    contexto.SaveChanges();
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void cerrarHcEmergenciaForm1(int codigoEmergencia)
        {
            try
            {
                //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                //{
                //    HC_EMERGENCIA_FORM emergenciaOriginal = contexto.HC_EMERGENCIA_FORM.FirstOrDefault(hc => hc.EMER_CODIGO == codigoEmergencia);
                //    emergenciaOriginal.EMER_ESTADO = 10;
                //    contexto.SaveChanges();
                //}

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //Pablo Rocha 20/08/2013

        public void ActualizaSinConsulta(Int32 CodigoAtencion)
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

            Sqlcmd = new SqlCommand("sp_ActualizaSinConsulta", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (CodigoAtencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

        }
        public string Aseguradora(Int64 ate_codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataReader reader;
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

            Sqlcmd = new SqlCommand("sp_AseguradorasAtencion", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            Sqlcmd.CommandTimeout = 180;
            Sqlcmd.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            reader = Sqlcmd.ExecuteReader();
            string seguro = "";

            while (reader.Read())
            {
                seguro = reader["Seguro"].ToString();
            }
            Sqlcmd.Parameters.Clear();
            Sqlcon.Close();
            return seguro;

        }
        //Pablo Rocha 21/08/2013
        public string RecuperaColor(string CodigoAtencion)
        {

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            Int64 medi = Convert.ToInt64(CodigoAtencion);
            string medi1;
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("sp_RecuperaColor", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = (medi);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            Sqlcon.Close();
            try
            {
                medi1 = Dts.Rows[0]["ATE_SINCONSULTA"].ToString();
                if (medi1 == "True" || medi1 == "1")
                    medi1 = "1";
                else
                    medi1 = "0";
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message, "MEDICO NO REGISTRA HONORARIOS", "INFORMACION");
                medi1 = "0";
            }


            return medi1;

        }

        public void GuardarGrupoS(Int64 pac_codigo, int gs_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
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
            command.CommandText = "update PACIENTES set GS_CODIGO = " + gs_codigo + " where PAC_CODIGO = " + pac_codigo;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            conexion.Close();
        }
        public void ModificarHCEmergenciaFNEW(HC_EMERGENCIA_FORM emergenciaF)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    HC_EMERGENCIA_FORM emergenciaFOri = contexto.HC_EMERGENCIA_FORM.Where(e => e.EMER_CODIGO == emergenciaF.EMER_CODIGO).FirstOrDefault();
                    emergenciaFOri.EMER_TRAUMA = emergenciaF.EMER_TRAUMA;
                    emergenciaFOri.EMER_CAUSA_CLINICA = emergenciaF.EMER_CAUSA_CLINICA;
                    emergenciaFOri.EMER_CUASA_GO = emergenciaF.EMER_CUASA_GO;
                    emergenciaFOri.EMER_CUASA_Q = emergenciaF.EMER_CUASA_Q;
                    emergenciaFOri.EMER_OTRO_MOTIVO = emergenciaF.EMER_OTRO_MOTIVO;
                    emergenciaFOri.EMER_FECHA = emergenciaF.EMER_FECHA;
                    emergenciaFOri.EMER_FECHA_EVENTO = emergenciaF.EMER_FECHA_EVENTO;
                    emergenciaFOri.HCC_CODIGO_LE = emergenciaF.HCC_CODIGO_LE;
                    emergenciaFOri.EMER_DIRECCION_EVENTO = emergenciaF.EMER_DIRECCION_EVENTO;
                    emergenciaFOri.EMER_CUSTODIA_POLICIAL = emergenciaF.EMER_CUSTODIA_POLICIAL;
                    emergenciaFOri.HCC_CODIGO_AVIEQ = emergenciaF.HCC_CODIGO_AVIEQ;
                    emergenciaFOri.EMER_OBS_AVIEQ = emergenciaF.EMER_OBS_AVIEQ;
                    emergenciaFOri.EMER_ALIENTO_ETIL = emergenciaF.EMER_ALIENTO_ETIL;
                    emergenciaFOri.EMER_VALOR_ALCOH = emergenciaF.EMER_VALOR_ALCOH;
                    emergenciaFOri.EMER_VIA_AEREA = emergenciaF.EMER_VIA_AEREA;
                    emergenciaFOri.EMER_VIA_AEREA_OBT = emergenciaF.EMER_VIA_AEREA_OBT;
                    emergenciaFOri.EMER_COND_EST = emergenciaF.EMER_COND_EST;
                    emergenciaFOri.EMER_COND_INEST = emergenciaF.EMER_COND_INEST;
                    emergenciaFOri.EMER_ENFACT_OBS = emergenciaF.EMER_ENFACT_OBS;
                    emergenciaFOri.EMER_PRES_A = emergenciaF.EMER_PRES_A;
                    emergenciaFOri.EMER_PRES_B = emergenciaF.EMER_PRES_B;
                    emergenciaFOri.EMER_FREC_CARDIACA = emergenciaF.EMER_FREC_CARDIACA;
                    emergenciaFOri.EMER_FREC_RESPIRATORIA = emergenciaF.EMER_FREC_RESPIRATORIA;
                    emergenciaFOri.EMER_TEMP_BUCAL = emergenciaF.EMER_TEMP_BUCAL;
                    emergenciaFOri.EMER_TEMP_AXILAR = emergenciaF.EMER_TEMP_AXILAR;
                    emergenciaFOri.EMER_SATURA_OXI = emergenciaF.EMER_SATURA_OXI;
                    emergenciaFOri.EMER_PESO = emergenciaF.EMER_PESO;
                    emergenciaFOri.EMER_TALLA = emergenciaF.EMER_TALLA;
                    emergenciaFOri.EMER_GLASGOV = emergenciaF.EMER_GLASGOV;
                    emergenciaFOri.EMER_OCULAR = emergenciaF.EMER_OCULAR;
                    emergenciaFOri.EMER_VERBAL = emergenciaF.EMER_VERBAL;
                    emergenciaFOri.EMER_MOTORA = emergenciaF.EMER_MOTORA;
                    emergenciaFOri.EMER_PUP_DIAD = emergenciaF.EMER_PUP_DIAD;
                    emergenciaFOri.EMER_PUP_READ = emergenciaF.EMER_PUP_READ;
                    emergenciaFOri.EMER_PUP_DIAI = emergenciaF.EMER_PUP_DIAI;
                    emergenciaFOri.EMER_PUP_REAI = emergenciaF.EMER_PUP_REAI;
                    emergenciaFOri.EMER_GLICEMIA_CAPILAR = emergenciaF.EMER_GLICEMIA_CAPILAR;
                    emergenciaFOri.EMER_DOMICILIO = emergenciaF.EMER_DOMICILIO;
                    emergenciaFOri.EMER_CONS_EXT = emergenciaF.EMER_CONS_EXT;
                    emergenciaFOri.EMER_OBS_ALTA = emergenciaF.EMER_OBS_ALTA;
                    emergenciaFOri.EMER_INTER = emergenciaF.EMER_INTER;
                    emergenciaFOri.EMER_PREF = emergenciaF.EMER_PREF;
                    emergenciaFOri.EMER_SER_REF = emergenciaF.EMER_SER_REF;
                    emergenciaFOri.EMER_ESTAB = emergenciaF.EMER_ESTAB;
                    emergenciaFOri.EMER_EGRESA_VIVO = emergenciaF.EMER_EGRESA_VIVO;
                    emergenciaFOri.EMER_MUERTO_EMER = emergenciaF.EMER_MUERTO_EMER;
                    emergenciaFOri.EMER_CONDICION_EST = emergenciaF.EMER_CONDICION_EST;
                    emergenciaFOri.EMER_CONDICION_INES = emergenciaF.EMER_CONDICION_INES;
                    emergenciaFOri.EMER_DIAS_INCAP = emergenciaF.EMER_DIAS_INCAP;
                    emergenciaFOri.EMER_CAUSA_MUERTE = emergenciaF.EMER_CAUSA_MUERTE;
                    emergenciaFOri.ID_USUARIO = emergenciaF.ID_USUARIO;
                    emergenciaFOri.EMER_FECHA_E = emergenciaF.EMER_FECHA_E;
                    emergenciaFOri.EMER_HORA_E = emergenciaF.EMER_HORA_E;
                    emergenciaFOri.EMER_NOMBRE_PROF_E = emergenciaF.EMER_NOMBRE_PROF_E;
                    emergenciaFOri.EMER_CODIGO_PRO_E = emergenciaF.EMER_CODIGO_PRO_E;
                    //CAMPOS NUEVOS FORMULARIO 2021
                    emergenciaFOri.EMER_CL_ESTABLE = emergenciaF.EMER_CL_ESTABLE;
                    emergenciaFOri.EMER_CL_INESTABLE = emergenciaF.EMER_CL_INESTABLE;
                    emergenciaFOri.EMER_CL_FALLECIDO = emergenciaF.EMER_CL_FALLECIDO;
                    emergenciaFOri.EMER_CL_MOTIVO = emergenciaF.EMER_CL_MOTIVO;
                    emergenciaFOri.EMER_ACT_TRA = emergenciaF.EMER_ACT_TRA;
                    emergenciaFOri.EMER_VIO_ARM = emergenciaF.EMER_VIO_ARM;
                    emergenciaFOri.EMER_INT_ALC = emergenciaF.EMER_INT_ALC;
                    emergenciaFOri.EMER_CAIDA = emergenciaF.EMER_CAIDA;
                    emergenciaFOri.EMER_VIO_PUN = emergenciaF.EMER_VIO_PUN;
                    emergenciaFOri.EMER_INT_ALI = emergenciaF.EMER_INT_ALI;
                    emergenciaFOri.EMER_QUEMADURA = emergenciaF.EMER_QUEMADURA;
                    emergenciaFOri.EMER_VIO_RINA = emergenciaF.EMER_VIO_RINA;
                    emergenciaFOri.EMER_INT_DRO = emergenciaF.EMER_INT_DRO;
                    emergenciaFOri.EMER_MORDEDURA = emergenciaF.EMER_MORDEDURA;
                    emergenciaFOri.EMER_VIO_FAM = emergenciaF.EMER_VIO_FAM;
                    emergenciaFOri.EMER_INH_GAS = emergenciaF.EMER_INH_GAS;
                    emergenciaFOri.EMER_AHOGAMIENTO = emergenciaF.EMER_AHOGAMIENTO;
                    emergenciaFOri.EMER_PRE_FIS = emergenciaF.EMER_PRE_FIS;
                    emergenciaFOri.EMER_OTR_INT = emergenciaF.EMER_OTR_INT;
                    emergenciaFOri.EMER_CUE_EXT = emergenciaF.EMER_CUE_EXT;
                    emergenciaFOri.EMER_PRE_PSI = emergenciaF.EMER_PRE_PSI;
                    emergenciaFOri.EMER_PICADURA = emergenciaF.EMER_PICADURA;
                    emergenciaFOri.EMER_APLASTAMIENTO = emergenciaF.EMER_APLASTAMIENTO;
                    emergenciaFOri.EMER_PRE_SEX = emergenciaF.EMER_PRE_SEX;
                    emergenciaFOri.EMER_ENVENENAMIENTO = emergenciaF.EMER_ENVENENAMIENTO;
                    emergenciaFOri.EMER_OTR_ACC = emergenciaF.EMER_OTR_ACC;
                    emergenciaFOri.EMER_NOTIFICACION = emergenciaF.EMER_NOTIFICACION;
                    emergenciaFOri.EMER_ANAFILAXIA = emergenciaF.EMER_ANAFILAXIA;
                    emergenciaFOri.EMER_LLE_CAP = emergenciaF.EMER_LLE_CAP;
                    emergenciaFOri.EMER_SIN_VIT = emergenciaF.EMER_SIN_VIT;
                    emergenciaFOri.EMER_PERIMETRO = emergenciaF.EMER_PERIMETRO;
                    emergenciaFOri.EMER_OBS_EGR = emergenciaF.EMER_OBS_EGR;
                    emergenciaFOri.EMER_HOSPITALIZACION = emergenciaF.EMER_HOSPITALIZACION;
                    emergenciaFOri.EMER_REF_INV = emergenciaF.EMER_REF_INV;
                    emergenciaFOri.EMER_OBS_EGR = emergenciaF.EMER_OBS_EGR;
                    emergenciaFOri.EMER_GRU_PRIO = emergenciaF.EMER_GRU_PRIO;
                    emergenciaFOri.EMER_PRIORITARIO = emergenciaF.EMER_PRIORITARIO;
                    emergenciaFOri.EMER_DERIVACION = emergenciaF.EMER_DERIVACION;
                    emergenciaFOri.EMER_ALT_DEF = emergenciaF.EMER_ALT_DEF;
                    emergenciaFOri.EMER_OBS_EXA_FIS = emergenciaF.EMER_OBS_EXA_FIS;
                    emergenciaFOri.EMER_SCORE = emergenciaF.EMER_SCORE;


                    Int64 ate_codigo = Convert.ToInt64(emergenciaF.ATENCIONESReference.EntityKey.EntityKeyValues[0].Value);
                    SIGNOSVITALES_CONSULTAEXTERNA sv = contexto.SIGNOSVITALES_CONSULTAEXTERNA.FirstOrDefault(x => x.ATE_CODIGO == ate_codigo);

                    if (sv != null)
                    {
                        Decimal indiceMasaCorporal = 0;
                        Decimal talla = Convert.ToDecimal(emergenciaF.EMER_TALLA);
                        if (talla > 0)
                            indiceMasaCorporal = Convert.ToDecimal(emergenciaF.EMER_PESO) / (talla * talla);
                        sv.Presion1 = (decimal)emergenciaF.EMER_PRES_A;
                        sv.Presion2 = (decimal)emergenciaF.EMER_PRES_B;
                        sv.F_Cardiaca = Convert.ToDecimal(emergenciaF.EMER_FREC_CARDIACA);
                        sv.F_Respiratoria = Convert.ToDecimal(emergenciaF.EMER_FREC_RESPIRATORIA);
                        sv.S_Oxigeno = (decimal)emergenciaF.EMER_SATURA_OXI;
                        sv.TallaM = Convert.ToDecimal(emergenciaF.EMER_TALLA); 
                        sv.Ind_Masa = indiceMasaCorporal;
                        sv.Ocular = (short)emergenciaF.EMER_OCULAR;
                        sv.Verbal = (short)emergenciaF.EMER_VERBAL;
                        sv.Motora = (short)emergenciaF.EMER_MOTORA;
                        sv.Glasgow = (decimal)emergenciaF.EMER_GLASGOV;
                        sv.PesoKG = Convert.ToDecimal(emergenciaF.EMER_PESO);
                        sv.T_Bucal = Convert.ToDecimal(emergenciaF.EMER_TEMP_BUCAL);
                        sv.T_Axilar = Convert.ToDecimal(emergenciaF.EMER_TEMP_AXILAR);
                        sv.ID_USUARIO = emergenciaF.ID_USUARIO;
                        sv.Diametro_Der = (short)emergenciaF.EMER_PUP_DIAD;
                        sv.Reaccion_Der = emergenciaF.EMER_PUP_READ;
                        sv.Diametro_Iz = (short)emergenciaF.EMER_PUP_DIAI;
                        sv.Reaccion_Iz = emergenciaF.EMER_PUP_REAI;
                    }
                    contexto.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                }
            }
        }
    }
}
