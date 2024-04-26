using Core.Datos;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace His.Datos
{
    public class DatHC_SignosVitales
    {
        public HC_SIGNOS_VITALES CargarDatosSignosVitales(Int64 ate_codigo, Int32 dia)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from s in db.HC_SIGNOS_VITALES
                        where s.ATE_CODIGO == ate_codigo && s.SV_DIA == dia
                        select s).FirstOrDefault();
            }
            //SqlCommand command;
            //SqlConnection connection;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //connection = obj.ConectarBd();
            //connection.Open();
            //DataTable Tabla = new DataTable();

            //command = new SqlCommand("Select * From HC_SIGNOS_VITALES where ATE_CODIGO = @ate_codigo and SV_DIA = @sv_dia order by SV_DIA", connection);
            //command.CommandType = CommandType.Text;
            //command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            //command.Parameters.AddWithValue("@sv_dia", dia);
            //SqlDataReader reader = command.ExecuteReader();
            //Tabla.Load(reader);
            //reader.Close();
            //command.Parameters.Clear();
            //connection.Close();
            //if (Tabla.Rows.Count > 0)
            //{
            //    return Tabla;
            //}
            //else
            //{
            //    return Tabla = null;
            //}
        }
        public List<HC_SIGNOS_VITALES> CargarImpresion(Int64 ate_codigo, Int32 SV_HOJA)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from s in db.HC_SIGNOS_VITALES
                        where s.ATE_CODIGO == ate_codigo && s.SV_HOJA == SV_HOJA
                        orderby s.SV_FECHA
                        select s).ToList();
              

            }
        }
        public void GuardarSignosVitales(Int64 ate_codigo, Int32 SV_DIA, DateTime SV_FECHA, string SV_INTERACCION, string SV_POSTQUIRURGICO,
            string SV_ING_PARENTAL, string SV_ING_ORAL, string SV_ING_TOTAL, string SV_ELM_ORINA, string SV_ELM_DRENAJE, string SV_ELM_OTROS,
            string SV_ELM_TOTAL, string SV_BAÑO, string SV_PESO, string SV_DIETA_ADMINISTRADA, string SV_NUMERO_COMIDAS, string SV_NUMERO_MEDICIONES, string SV_NUMERO_DEPOSICIONES,
            string SV_ACTIVIDAD_FISICA, string SV_CAMBIO_SONDA, string SV_RECANALIZACION, string SV_RESPONSABLE, string SV_PORCENTAJE, Int32 SV_HOJA, Int32 SV_RESPALDO_DIA)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw;
            }
            command = new SqlCommand("sp_GuardarSignosVitales", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ATE_CODIGO", ate_codigo);
            command.Parameters.AddWithValue("@SV_DIA", SV_DIA);
            command.Parameters.AddWithValue("@SV_FECHA", SV_FECHA);
            command.Parameters.AddWithValue("@SV_INTERACCION", SV_INTERACCION);
            command.Parameters.AddWithValue("@SV_POSTQUIRURGICO", SV_POSTQUIRURGICO);
            command.Parameters.AddWithValue("@SV_ING_PARENTAL", SV_ING_PARENTAL);
            command.Parameters.AddWithValue("@SV_ING_ORAL", SV_ING_ORAL);
            command.Parameters.AddWithValue("@SV_ING_TOTAL", SV_ING_TOTAL);
            command.Parameters.AddWithValue("@SV_ELM_ORINA", SV_ELM_ORINA);
            command.Parameters.AddWithValue("@SV_ELM_DRENAJE", SV_ELM_DRENAJE);
            command.Parameters.AddWithValue("@SV_ELM_OTROS", SV_ELM_OTROS);
            command.Parameters.AddWithValue("@SV_ELM_TOTAL", SV_ELM_TOTAL);
            command.Parameters.AddWithValue("@SV_BAÑO", SV_BAÑO);
            command.Parameters.AddWithValue("@SV_PESO", SV_PESO);
            command.Parameters.AddWithValue("@SV_DIETA_ADMINISTRADA", SV_DIETA_ADMINISTRADA);
            command.Parameters.AddWithValue("@SV_NUMERO_COMIDAS", SV_NUMERO_COMIDAS);
            command.Parameters.AddWithValue("@SV_NUMERO_MEDICIONES", SV_NUMERO_MEDICIONES);
            command.Parameters.AddWithValue("@SV_NUMERO_DEPOSICIONES", SV_NUMERO_DEPOSICIONES);
            command.Parameters.AddWithValue("@SV_ACTIVIDAD_FISICA", SV_ACTIVIDAD_FISICA);
            command.Parameters.AddWithValue("@SV_CAMBIO_SONDA", SV_CAMBIO_SONDA);
            command.Parameters.AddWithValue("@SV_RECANALIZACION", SV_RECANALIZACION);
            command.Parameters.AddWithValue("@SV_RESPONSABLE", SV_RESPONSABLE);
            command.Parameters.AddWithValue("@SV_PORCENTAJE", SV_PORCENTAJE);
            command.Parameters.AddWithValue("@SV_HOJA", SV_HOJA);
            command.Parameters.AddWithValue("@SV_RESPALDO_DIA", SV_RESPALDO_DIA);
            command.CommandTimeout = 180;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            command.Parameters.Clear();
            connection.Close();

        }

        public List<DtoExploSignosVitales> CargarSignosAtencion(Int64 ate_codigo)
        {
            List<DtoExploSignosVitales> sv = new List<DtoExploSignosVitales>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var dt = (from s in db.HC_SIGNOS_VITALES
                          join d in db.HC_SIGNOS_DATOS_ADICIONALES on s.SV_CODIGO equals d.SV_CODIGO
                          join u in db.USUARIOS on d.ID_USUARIO equals u.ID_USUARIO
                          where s.ATE_CODIGO == ate_codigo
                          orderby s.SV_FECHA, d.SVD_HORA
                          select new { s, d, u }).ToList();
                foreach (var item in dt)
                {
                    DtoExploSignosVitales dv = new DtoExploSignosVitales();
                    dv.CODIGO = item.d.SVD_CODIGO;
                    dv.FECHA = item.s.SV_FECHA.Value.ToShortDateString();
                    dv.HORA = Convert.ToString(item.d.SVD_HORA);
                    dv.F_CARDIACA = item.d.SVD_PULSO_AM;
                    dv.TEMPERATURA = item.d.SVD_TEMPERATURA_AM;
                    dv.F_RESPIRATORIA = item.d.SVD_FRESPIRATORIA;
                    dv.P_SISTONICA = item.d.SVD_SISTONICA;
                    dv.P_DIASTONICA = item.d.SVD_DIASTONICA;
                    dv.P_ARTERIAL = item.d.SVD_SISTONICA + " / " + item.d.SVD_DIASTONICA;
                    dv.S_OXIGENO = item.d.SVD_SATURACION;
                    dv.MEDICO = item.u.USR;
                    sv.Add(dv);
                }
                return sv;
            }
        }
        public DataTable diaConsecutivo(Int64 ate_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();

            command = new SqlCommand("select top 1 SV_DIA from HC_SIGNOS_VITALES where ATE_CODIGO = @ate_codigo order by SV_DIA desc", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            SqlDataReader reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            if (Tabla.Rows.Count > 0)
            {
                return Tabla;
            }
            else
            {
                return Tabla = null;
            }
        }
        public DataTable diaConsecutivoHoja(Int64 ate_codigo, Int64 hoja)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();

            command = new SqlCommand("select top 1 SV_RESPANDO_DIA from HC_SIGNOS_VITALES where ATE_CODIGO = @ate_codigo and SV_HOJA = @hoja order by SV_DIA desc", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@hoja", hoja);
            SqlDataReader reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            if (Tabla.Rows.Count > 0)
            {
                return Tabla;
            }
            else
            {
                return Tabla = null;
            }
        }
        public DataTable diaRepetido(Int64 ate_codigo, Int32 SV_HOJA)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();

            command = new SqlCommand(" select SV_RESPALDO_DIA from HC_SIGNOS_VITALES WHERE ATE_CODIGO =  @ate_codigo and SV_HOJA= 1 ORDER BY 1 DESC", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@SV_HOJA", SV_HOJA);
            SqlDataReader reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            if (Tabla.Rows.Count > 0)
            {
                return Tabla;
            }
            else
            {
                return Tabla = null;
            }
        }
        public DataTable diaConsecutivoRespaldo(Int64 ate_codigo, string SV_RESPALDO_DIA)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            DataTable Tabla = new DataTable();

            command = new SqlCommand("select top 1 SV_RESPALDO_DIA from HC_SIGNOS_VITALES where ATE_CODIGO = @ate_codigo and SV_RESPALDO_DIA = '@SV_RESPALDO_DIA' order by SV_RESPALDO_DIA desc", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@SV_RESPALDO_DIA", SV_RESPALDO_DIA);
            SqlDataReader reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            if (Tabla.Rows.Count > 0)
            {
                return Tabla;
            }
            else
            {
                return Tabla = null;
            }
        }
        public DataTable getSignos(Int64 ate_codigo)
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
            Sqlcmd = new SqlCommand("select s.SV_CODIGO,'[FECHA:'+CONVERT(varchar,s.SV_FECHA,23)+'] - [DIA:'+CONVERT(varchar,s.SV_DIA)+'] - [RESPONSABLE: '+U.USR+']'  AS DATOS , s.SV_DIA,s.SV_HOJA from HC_SIGNOS_VITALES S INNER JOIN USUARIOS U ON S.SV_RESPONSABLE = U.ID_USUARIO where s.ATE_CODIGO =" + ate_codigo + " order by s.SV_DIA desc ", Sqlcon);
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
        public bool GrabarSignosVitalesDat(HC_SIGNOS_DATOS_ADICIONALES svdatos)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    contexto.Crear("HC_SIGNOS_DATOS_ADICIONALES", svdatos);
                    contexto.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }

        }
        public bool GrabarSignosVitales(HC_SIGNOS_VITALES svitales)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    contexto.Crear("HC_SIGNOS_VITALES", svitales);
                    //contexto.AddToHC_SIGNOS_VITALES(svitales);
                    contexto.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                    //throw;
                }
            }
        }
        public Int32 ultimoRegistro()
        {
            Int32 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_SIGNOS_VITALES> sv = contexto.HC_SIGNOS_VITALES.ToList();
                if (sv.Count > 0)
                    maxim = contexto.HC_SIGNOS_VITALES.Max(emp => emp.SV_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public bool EditarHojasignos(HC_SIGNOS_VITALES sv, Int64 SV_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    HC_SIGNOS_VITALES x = (from s in db.HC_SIGNOS_VITALES
                                           where s.SV_CODIGO == SV_CODIGO
                                           select s).FirstOrDefault();
                    x.SV_INTERACCION = sv.SV_INTERACCION;
                    x.SV_POSTQUIRURGICO = sv.SV_POSTQUIRURGICO;
                    x.SV_ING_PARENTAL = sv.SV_ING_PARENTAL;
                    x.SV_ING_ORAL = sv.SV_ING_ORAL;
                    x.SV_ING_TOTAL = sv.SV_ING_TOTAL;
                    x.SV_ELM_ORINA = sv.SV_ELM_ORINA;
                    x.SV_ELM_DRENAJE = sv.SV_ELM_DRENAJE;
                    x.SV_ELM_OTROS = sv.SV_ELM_OTROS;
                    x.SV_ELM_TOTAL = sv.SV_ELM_TOTAL;
                    x.SV_BAÑO = sv.SV_BAÑO;
                    x.SV_PESO = sv.SV_PESO;
                    x.SV_DIETA_ADMINISTRADA = sv.SV_DIETA_ADMINISTRADA;
                    x.SV_NUMERO_COMIDAS = sv.SV_NUMERO_COMIDAS;
                    x.SV_NUMERO_MEDICIONES = sv.SV_NUMERO_MEDICIONES;
                    x.SV_NUMERO_DEPOSICIONES = sv.SV_NUMERO_DEPOSICIONES;
                    x.SV_ACTIVIDAD_FISICA = sv.SV_ACTIVIDAD_FISICA;
                    x.SV_CAMBIO_SONDA = sv.SV_CAMBIO_SONDA;
                    x.SV_RECANALIZACION = sv.SV_RECANALIZACION;
                    x.SV_RESPONSABLE = sv.SV_RESPONSABLE;
                    x.SV_PORCENTAJE = sv.SV_PORCENTAJE;
                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }

        }
        public bool EditarSignosVit(HC_SIGNOS_DATOS_ADICIONALES sv)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transa = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    HC_SIGNOS_DATOS_ADICIONALES x = (from s in db.HC_SIGNOS_DATOS_ADICIONALES
                                                     where s.SVD_CODIGO == sv.SVD_CODIGO
                                                     select s).FirstOrDefault();
                    x.SVD_HORA = sv.SVD_HORA;
                    x.SVD_PULSO_AM = sv.SVD_PULSO_AM;
                    x.SVD_TEMPERATURA_AM = sv.SVD_TEMPERATURA_AM;
                    x.SVD_FRESPIRATORIA = sv.SVD_FRESPIRATORIA;
                    x.SVD_SISTONICA = sv.SVD_SISTONICA;
                    x.SVD_DIASTONICA = sv.SVD_DIASTONICA;
                    x.SVD_SATURACION = sv.SVD_SATURACION;
                    x.ID_FRECUENCIA = sv.ID_FRECUENCIA;
                    db.SaveChanges();
                    transa.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transa.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }

        }
        public HC_SIGNOS_DATOS_ADICIONALES CargarDatosSignosDatos(Int32 SVD_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from s in db.HC_SIGNOS_DATOS_ADICIONALES
                        where s.SVD_CODIGO == SVD_CODIGO
                        select s).FirstOrDefault();
            }
        }
        //public HC_SIGNOS_VITALES ConsultaSignosXfecha(Int64 ate_codigo,DateTime fecha)
        //{
        //    using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //    {
        //        return (from s in db.HC_SIGNOS_VITALES
        //                where s.ATE_CODIGO == ate_codigo && s.SV_FECHA == fecha
        //                select s).FirstOrDefault();
        //    }
        //}
        public DataTable ConsultaSignosXfecha(Int64 ate_codigo, DateTime fecha)
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
            Sqlcmd = new SqlCommand("select * from His3000..HC_SIGNOS_VITALES where ATE_CODIGO = " + ate_codigo + " and \n" +
                " YEAR(SV_FECHA) = " + fecha.Year + " and MONTH(SV_FECHA) = " + fecha.Month + " and DAY (SV_FECHA) = " + fecha.Day, Sqlcon);
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
        public List<HC_SIGNOS_DATOS_ADICIONALES> ValidaHora(Int64 ate_codigo, Int32 dia)
        {
            List<HC_SIGNOS_DATOS_ADICIONALES> sv = new List<HC_SIGNOS_DATOS_ADICIONALES>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var x = (from s in db.HC_SIGNOS_VITALES
                         join d in db.HC_SIGNOS_DATOS_ADICIONALES on s.SV_CODIGO equals d.SV_CODIGO
                         where s.ATE_CODIGO == ate_codigo && s.SV_DIA == dia
                         select d).ToList();
                foreach (var item in x)
                {
                    HC_SIGNOS_DATOS_ADICIONALES da = new HC_SIGNOS_DATOS_ADICIONALES();
                    da.SVD_HORA = item.SVD_HORA;
                    sv.Add(da);
                }
            }
            return sv;
        }
        public bool EditarDesdeIngestaEliminacion(HC_SIGNOS_VITALES sv, Int64 ATE_CODIGO, DateTime SV_FECHA)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            try
            {
                command = new SqlCommand("update His3000..HC_SIGNOS_VITALES set SV_ING_ORAL = '" + sv.SV_ING_ORAL + "', SV_ING_PARENTAL = '" + sv.SV_ING_PARENTAL + "', SV_ING_TOTAL ='" + sv.SV_ING_TOTAL + " \n" +
                "',SV_ELM_ORINA ='" + sv.SV_ELM_ORINA + "',SV_ELM_DRENAJE ='" + sv.SV_ELM_DRENAJE + "',SV_ELM_OTROS ='" + sv.SV_ELM_OTROS + "',SV_ELM_TOTAL='" + sv.SV_ELM_TOTAL + "' \n" +
                ",SV_NUMERO_COMIDAS = '" + sv.SV_NUMERO_COMIDAS + "', SV_NUMERO_MEDICIONES = '" + sv.SV_NUMERO_MEDICIONES + "', SV_NUMERO_DEPOSICIONES ='" + sv.SV_NUMERO_DEPOSICIONES + "' \n" +
                "where ATE_CODIGO = " + ATE_CODIGO + " and YEAR(SV_FECHA) = " + SV_FECHA.Year + " and MONTH(SV_FECHA) = " + SV_FECHA.Month + " and DAY(SV_FECHA) = " + SV_FECHA.Day + "", connection);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 380;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                return false;
            }

        }
        public List<DtoExploSignosVitales> cargarSignosXatencion(Int64 ate_codigo)
        {
            List<DtoExploSignosVitales> sv = new List<DtoExploSignosVitales>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var dt = (from s in db.HC_SIGNOS_VITALES
                          join d in db.HC_SIGNOS_DATOS_ADICIONALES on s.SV_CODIGO equals d.SV_CODIGO
                          join u in db.USUARIOS on d.ID_USUARIO equals u.ID_USUARIO
                          where s.ATE_CODIGO == ate_codigo
                          orderby s.SV_FECHA, d.SVD_HORA
                          select new { s, d, u }).ToList().Take(2);
                foreach (var item in dt)
                {
                    DtoExploSignosVitales dv = new DtoExploSignosVitales();
                    dv.CODIGO = item.d.SVD_CODIGO;
                    dv.FECHA = item.s.SV_FECHA.Value.ToShortDateString();
                    dv.HORA = Convert.ToString(item.d.SVD_HORA);
                    dv.F_CARDIACA = item.d.SVD_PULSO_AM;
                    dv.TEMPERATURA = item.d.SVD_TEMPERATURA_AM;
                    dv.F_RESPIRATORIA = item.d.SVD_FRESPIRATORIA;
                    dv.P_SISTONICA = item.d.SVD_SISTONICA;
                    dv.P_DIASTONICA = item.d.SVD_DIASTONICA;
                    dv.P_ARTERIAL = item.d.SVD_SISTONICA + " / " + item.d.SVD_DIASTONICA;
                    dv.S_OXIGENO = item.d.SVD_SATURACION;
                    dv.MEDICO = item.u.USR;
                    sv.Add(dv);
                }
                return sv;
            }
        }
        public bool validadeSv(Int64 ate_codigo, DateTime SV_FECHA)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            string SV_ACTIVIDAD_FISICA = "";
            conexion = obj.ConectarBd();
            conexion.Open();
            try
            {
                command.Connection = conexion;
                command.CommandText = "select SV_ACTIVIDAD_FISICA from HC_SIGNOS_VITALES where ATE_CODIGO = " + ate_codigo + " and YEAR(SV_FECHA) = " + SV_FECHA.Year + " and MONTH(SV_FECHA) = " + SV_FECHA.Month + " and DAY(SV_FECHA) = " + SV_FECHA.Day;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                    {
                        SV_ACTIVIDAD_FISICA = reader.GetString(0);
                    }
                    conexion.Close();
                    if (SV_ACTIVIDAD_FISICA.Trim() == "Ambulatorio")
                        return true;
                    else
                        return false;
                }
                else
                {
                    conexion.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conexion.Close();
                Console.WriteLine(ex.Message); ;
                return false;
            }
        }
        public Int64 calculoDiasQuirurgico(Int64 ATE_CODIGO)
        {
            Int64 dias = 0;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_SIGNOS_VITALES sv = (from s in db.HC_SIGNOS_VITALES
                                        where s.ATE_CODIGO == ATE_CODIGO && s.SV_POSTQUIRURGICO == "CIRUGIA"
                                        select s).FirstOrDefault();
                if (sv != null)
                {
                    TimeSpan diasTransCurrid = DateTime.Now - (DateTime)sv.SV_FECHA;
                    dias = diasTransCurrid.Days;
                }
            }
            return dias;
        }
        public List<TIPO_DIETA> cargaDieta()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from td in db.TIPO_DIETA select td).OrderBy(x => x.TDI_DESCRIPCION).ToList();
            }
        }
        public bool cargaSignosVitales(Int64 ATE_CODIGO, Int32 SV_HOJA)
        {
            //DataTable sigVital = new DataTable(); // se comenta se encontro una maneja de trabajar mucho mejor // Mario Valencia // 30-11-2023

            //DataColumn dc = new DataColumn();
            //dc.ColumnName = "PULSO";
            //dc.DataType = typeof(string);

            //DataColumn dc1 = new DataColumn();
            //dc1.ColumnName = "TEMPERATURA";
            //dc1.DataType = typeof(string);

            //sigVital.Columns.AddRange(new DataColumn[] { dc, dc1 });

            DateTime h1 = new DateTime(2023, 8, 4, 06, 00, 0);
            DateTime h2 = new DateTime(2023, 8, 4, 16, 00, 0);
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();

                List<DtoSignosVitales> lstSv = new List<DtoSignosVitales>();
                //var sv = (from s in db.HC_SIGNOS_VITALES
                //          join sd in db.HC_SIGNOS_DATOS_ADICIONALES on s.SV_CODIGO equals sd.SV_CODIGO
                //          where s.ATE_CODIGO == ATE_CODIGO && s.SV_HOJA == SV_HOJA
                //          select new { s, sd }).OrderBy(x => x.s.SV_FECHA).OrderBy(y => y.sd.SVD_HORA).ToList();

                var sv = SignosVitales(ATE_CODIGO, SV_HOJA);

                var sv1 = (from s in db.HC_SIGNOS_VITALES
                           join sd in db.HC_SIGNOS_DATOS_ADICIONALES on s.SV_CODIGO equals sd.SV_CODIGO
                           where s.ATE_CODIGO == ATE_CODIGO && s.SV_HOJA == SV_HOJA
                           select new { s, sd }).OrderBy(x => x.s.SV_FECHA).OrderBy(y => y.sd.SVD_HORA).FirstOrDefault();

                if (sv1.sd.SVD_HORA != h1.TimeOfDay && sv1.sd.SVD_HORA != h2.TimeOfDay)
                {
                    DtoSignosVitales dtSv = new DtoSignosVitales();
                    dtSv.PULSO = sv1.sd.SVD_PULSO_AM;
                    dtSv.TEMPERATURA = sv1.sd.SVD_TEMPERATURA_AM;
                    dtSv.HORA = (TimeSpan)sv1.sd.SVD_HORA;
                    lstSv.Add(dtSv);
                }

                foreach (var item in sv)
                {
                    if (item.HORA == h1.TimeOfDay || item.HORA == h2.TimeOfDay)
                    {
                        DtoSignosVitales dtSv = new DtoSignosVitales();
                        dtSv.PULSO = item.PULSO;
                        dtSv.TEMPERATURA = item.TEMPERATURA;
                        dtSv.HORA = (TimeSpan)item.HORA;
                        lstSv.Add(dtSv);
                    }
                }
                //var listSv = lstSv.Where(x => x.HORA == h1.TimeOfDay || x.HORA == h2.TimeOfDay);
                try
                {
                    int contador = 1;
                    foreach (var item in lstSv.Take(9))
                    {
                        REPORTE_SV rsv = db.REPORTE_SV.FirstOrDefault(x => x.ID == contador);
                        if (rsv != null)
                        {
                            rsv.HORA = Convert.ToString(item.HORA).Substring(0, 5);
                            rsv.PULSO = Convert.ToDouble(item.PULSO);
                            rsv.TEMPERATURA = Convert.ToDouble(item.TEMPERATURA);
                        }
                        else
                        {
                            REPORTE_SV svt = new REPORTE_SV();
                            svt.HORA = Convert.ToString(item.HORA).Substring(0, 5);
                            svt.PULSO = Convert.ToDouble(item.PULSO);
                            svt.TEMPERATURA = Convert.ToDouble(item.TEMPERATURA);
                            db.Crear("REPORTE_SV", svt);
                        }
                        contador++;
                    }
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }

                //int cont = 0;
                //foreach (var item in listSv)
                //{
                //    sigVital.Rows.Add(new object[] { item.PULSO, item.TEMPERATURA });
                //    cont++;
                //}
                //for (int i = cont; i < 19; i++)
                //{
                //    sigVital.Rows.Add(new object[] { "NULL", "NULL" });
                //}
            }
        }
        public bool grabartablaSignos(DataTable sigVit)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            try
            {
                command = new SqlCommand("update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[0][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[0][1].ToString() + " where DIA = '1AM' \n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[1][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[1][1].ToString() + " where DIA = '1PM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[2][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[2][1].ToString() + "  where DIA = '2AM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[3][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[3][1].ToString() + "  where DIA = '2PM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[4][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[4][1].ToString() + "  where DIA = '3AM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[5][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[5][1].ToString() + "  where DIA = '3PM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[6][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[6][1].ToString() + "  where DIA = '4AM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[7][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[7][1].ToString() + "  where DIA = '4PM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[8][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[8][1].ToString() + "  where DIA = '5AM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[9][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[9][1].ToString() + "  where DIA = '5PM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[10][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[10][1].ToString() + "  where DIA = '6AM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[11][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[11][1].ToString() + "  where DIA = '6PM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[12][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[12][1].ToString() + "  where DIA = '7AM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[13][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[13][1].ToString() + "  where DIA = '7PM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[14][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[14][1].ToString() + "  where DIA = '8AM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[15][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[15][1].ToString() + "  where DIA = '8PM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[16][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[16][1].ToString() + "  where DIA = '9AM'\n" +
                    "update His3000..REPORTE_SV set PULSO = " + sigVit.Rows[17][0].ToString() + ",TEMPERATURA = " + sigVit.Rows[17][1].ToString() + "  where DIA = '9PM'\n", connection);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                connection.Close();
                return false;
            }
        }
        public bool editarReporteSv()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    var rs = db.REPORTE_SV.ToList();
                    foreach (var item in rs)
                    {
                        item.PULSO = null;
                        item.TEMPERATURA = null;
                        item.HORA = "";
                    }
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
            //SqlCommand command; // se comenta para trabajar con Entity // Mario Valencia /30-11-2023
            //SqlConnection connection;
            //SqlDataReader reader;
            //BaseContextoDatos obj = new BaseContextoDatos();
            //connection = obj.ConectarBd();
            //connection.Open();
            //try
            //{
            //    command = new SqlCommand("update His3000..REPORTE_SV set PULSO = null,TEMPERATURA = null", connection);
            //    command.CommandType = CommandType.Text;
            //    command.CommandTimeout = 180;
            //    reader = command.ExecuteReader();
            //    connection.Close();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    connection.Close();
            //    return false;
            //}
        }
        public bool editarReporteCT()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var rCT = db.REPORTE_CURVA_TERMICA.ToList();
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    foreach (var item in rCT)
                    {
                        item.TEMPERATURA = null;
                        item.HORA = null;
                    }
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }

            }
        }
        public bool editarReporteCTF(int fila)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                      
                    var rCT = db.REPORTE_CURVA_TERMICA.ToList();
                    fila = rCT.Count - 1;
                    for (int i = fila; i< rCT.Count; i++)
                    {
                        var item = rCT[i];
                        item.TEMPERATURA = null;
                        item.HORA = null;
                    }
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public bool cargaCurvaTermica(HC_SIGNOS_DATOS_ADICIONALES sv, Int64 contador)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    //var sv = (from s in db.HC_SIGNOS_VITALES
                    //          join sd in db.HC_SIGNOS_DATOS_ADICIONALES on s.SV_CODIGO equals sd.SV_CODIGO
                    //          where s.ATE_CODIGO == ATE_CODIGO
                    //          select new { s, sd }).OrderBy(y => y.sd.SVD_HORA).OrderBy(x => x.s.SV_FECHA).ToList();
                    //int contador = 1;
                    //foreach (var item in sv)
                    //{
                    REPORTE_CURVA_TERMICA ct = db.REPORTE_CURVA_TERMICA.FirstOrDefault(x => x.ID == contador);
                    if (ct != null)
                    {
                        ct.TEMPERATURA = Convert.ToDouble(sv.SVD_TEMPERATURA_AM);
                        ct.HORA = Convert.ToString(sv.SVD_HORA).Substring(0, 5);
                    }
                    else
                    {
                        REPORTE_CURVA_TERMICA rct = new REPORTE_CURVA_TERMICA();
                        rct.TEMPERATURA = Convert.ToDouble(sv.SVD_TEMPERATURA_AM);
                        rct.HORA = Convert.ToString(sv.SVD_HORA).Substring(0, 5);
                        db.Crear("REPORTE_CURVA_TERMICA", rct);
                    }
                    //contador++;
                    //}

                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }

            }
        }
        public List<HC_SIGNOS_DATOS_ADICIONALES> listaSVdatos(Int64 ATE_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_SIGNOS_DATOS_ADICIONALES> lsv = new List<HC_SIGNOS_DATOS_ADICIONALES>();
                var sv = (from s in db.HC_SIGNOS_VITALES
                          join sd in db.HC_SIGNOS_DATOS_ADICIONALES on s.SV_CODIGO equals sd.SV_CODIGO
                          where s.ATE_CODIGO == ATE_CODIGO
                          orderby s.SV_FECHA, sd.SVD_HORA
                          select new { sd }).ToList();

                foreach (var item in sv)
                {
                    HC_SIGNOS_DATOS_ADICIONALES svd = new HC_SIGNOS_DATOS_ADICIONALES();
                    svd.SVD_CODIGO = item.sd.SVD_CODIGO;
                    svd.SV_CODIGO = item.sd.SV_CODIGO;
                    svd.SVD_HORA = item.sd.SVD_HORA;
                    svd.SVD_PULSO_AM = item.sd.SVD_PULSO_AM;
                    svd.SVD_TEMPERATURA_AM = item.sd.SVD_TEMPERATURA_AM;
                    svd.SVD_FRESPIRATORIA = item.sd.SVD_FRESPIRATORIA;
                    svd.SVD_SISTONICA = item.sd.SVD_SISTONICA;
                    svd.SVD_DIASTONICA = item.sd.SVD_DIASTONICA;
                    svd.SVD_SATURACION = item.sd.SVD_SATURACION;
                    svd.ID_USUARIO = item.sd.ID_USUARIO;
                    svd.ID_FRECUENCIA = item.sd.ID_FRECUENCIA;
                    lsv.Add(svd);
                }
                return lsv;
            }
        }


        public List<DtoSignosVitales> SignosVitales(Int64 ate_codigo, int hoja)
        {
            List<DtoSignosVitales> listaSignosVitales = new List<DtoSignosVitales>();
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();

            conexion = obj.ConectarBd();

            try
            {
                command.Connection = conexion;
                command.CommandText = "SELECT s.*, sd.* FROM HC_SIGNOS_VITALES s JOIN HC_SIGNOS_DATOS_ADICIONALES sd ON s.SV_CODIGO = sd.SV_CODIGO WHERE s.ATE_CODIGO = @ATE_CODIGO AND s.SV_HOJA = @SV_HOJA ORDER BY s.SV_FECHA ASC, sd.SVD_HORA";
                command.Parameters.AddWithValue("@ATE_CODIGO", ate_codigo);
                command.Parameters.AddWithValue("@SV_HOJA", hoja);

                conexion.Open();
                SqlDataReader Sqldr = command.ExecuteReader();

                while (Sqldr.Read())
                {
                    DtoSignosVitales sv = new DtoSignosVitales();
                    sv.HORA = Sqldr["SVD_HORA"] != DBNull.Value ? (TimeSpan)Sqldr["SVD_HORA"] : TimeSpan.Zero;
                    sv.PULSO =  Sqldr["SVD_PULSO_AM"].ToString() ;                    // Asumiendo que PULSO es un entero
                    sv.TEMPERATURA =  (string)Sqldr["SVD_TEMPERATURA_AM"].ToString();   // Asumiendo que TEMPERATURA es un decimal
                                                                                                                    // Asigna otras propiedades si es necesario
                    listaSignosVitales.Add(sv);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente, puede lanzarla o registrarla
                Console.WriteLine("Error al recuperar los signos vitales: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión en el bloque finally
                if (conexion != null && conexion.State != ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }

            return listaSignosVitales;
        }





    }
}
