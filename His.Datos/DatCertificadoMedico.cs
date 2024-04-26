using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;
using His.Entidades;
using System.Data.Common;

namespace His.Datos
{
    public class DatCertificadoMedico
    {


        public DataTable BuscarPaciente(Int64 ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable Medico_Paciente()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoMedicoPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable Medico_PacienteMushugñan()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoMedicoPacienteMushugñan";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable Medico_PacienteBrigada()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoMedicoPacienteBrigada";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable Medico_PacienteTodos()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoMedicoPacienteTodos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CertificadosMedicos(DateTime fechainicio, DateTime fechafin, bool estado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadosMedicos";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable CertificadoXmedicos(DateTime fechainicio, DateTime fechafin, int codMedico, bool estado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadosXmedico";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@idMedico", codMedico);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public void ActualizaCertificado(Int64 ate_codigo, string direccion, string telefono)
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
            command.CommandText = "sp_Certificado_ActualizaPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@DIRECCION", direccion);
            command.Parameters.AddWithValue("@TELEFONO", telefono);

            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void ActualizaCertificadoIESS(int ate_codigo, string direccion, string telefono)
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
            command.CommandText = "sp_CertificadoIESS_ActualizaPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@DIRECCION", direccion);
            command.Parameters.AddWithValue("@TELEFONO", telefono);

            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void InsertarCertificado(Int32 cer_codigo, Int64 ate_codigo, int med_codigo, string observacion, int reposo
            , string actividad, string contingencia, string tratamiento, string procedimiento, int ingreso, DateTime fechaCirugia, string empresa)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ConexionEntidades.ConexionEDM.Open();
                DbTransaction transac = ConexionEntidades.ConexionEDM.BeginTransaction();
                try
                {
                    ATENCIONES atencion = db.ATENCIONES.FirstOrDefault(x => x.ATE_CODIGO == ate_codigo);
                    CERTIFICADO_MEDICO cert = new CERTIFICADO_MEDICO();
                    cert.CER_CODIGO = cer_codigo;
                    cert.ATENCIONESReference.EntityKey = atencion.EntityKey;
                    cert.MED_CODIGO = med_codigo;
                    cert.CER_OBSERVACION = observacion;
                    cert.CER_FECHA = DateTime.Now;
                    cert.CER_DIAS_REPOSO = reposo;
                    cert.CER_ACTIVIDAD_LABORAL = actividad;
                    cert.CER_CONTINGENCIA = contingencia;
                    cert.CER_TRATAMIENTO = tratamiento;
                    cert.CER_FECHA_CIRUGIA = fechaCirugia;
                    cert.CER_PROCEDIMIENTO = procedimiento;
                    cert.CER_TIPO_INGRESO = ingreso;
                    cert.CER_ESTADO = true;
                    cert.CER_EMPRESA = empresa;
                    db.Crear("CERTIFICADO_MEDICO", cert);
                    db.SaveChanges();
                    transac.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transac.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                }

            }
            //SqlConnection conexion;
            //SqlCommand command = new SqlCommand();
            //BaseContextoDatos obj = new BaseContextoDatos();
            //conexion = obj.ConectarBd();
            //try
            //{
            //    conexion.Open();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //command.Connection = conexion;
            //command.CommandText = "sp_Certificado_InsertarPaciente";
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@cer_codigo", cer_codigo);
            //command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            //command.Parameters.AddWithValue("@med_codigo", med_codigo);
            //command.Parameters.AddWithValue("@observacion", observacion);
            //command.Parameters.AddWithValue("@reposo", reposo);
            //command.Parameters.AddWithValue("@actividad", actividad);
            //command.Parameters.AddWithValue("@contingencia", contingencia);
            //command.Parameters.AddWithValue("@tratamiento", tratamiento);
            //command.Parameters.AddWithValue("@procedimiento", procedimiento);
            //command.Parameters.AddWithValue("@ingreso", ingreso);
            //command.Parameters.AddWithValue("@fechaCirugia", fechaCirugia);
            //command.CommandTimeout = 180;
            //command.ExecuteNonQuery();
            //command.Parameters.Clear();
            //conexion.Close();
        }
        public void InsertarCertificadoIESS(Int32 CMI_CODIGO, int ATE_CODIGO, int MED_CODIGO, string CMI_INSTITUCION_LABORAL, DateTime CMI_FECHA, string CMI_DIAS_REPOSO, string CMI_ACTIVIDAD_LABORAL,
            string CMI_CONTINGENCIA, string CMI_CONFIRMADO, DateTime CMI_FECHA_CIRUGIA, string CMI_DESCRIPCION_SINTOMAS, string CMI_NOTA, int CMI_TIPO_INGRESO, bool CMI_ESTADO,
            bool CMI_ENFERMEDAD, bool CMI_SINTOMAS, bool CMI_REPOSO, bool CMI_AISLAMIENTO, bool CMI_TELETRABAJO, string DIRECCION_PACIENTE, string TELEFONO_PACIENTE, string CMI_ANULADO, DateTime CMI_FECHA_ALTA,
            string CMI_PROCEDIMIENTO, string CMI_TRATAMIENTO)
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
            command.CommandText = "sp_Certificado_IESS";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CMI_CODIGO", CMI_CODIGO);
            command.Parameters.AddWithValue("@ATE_CODIGO", ATE_CODIGO);
            command.Parameters.AddWithValue("@MED_CODIGO", MED_CODIGO);
            command.Parameters.AddWithValue("@CMI_INSTITUCION_LABORAL", CMI_INSTITUCION_LABORAL);
            command.Parameters.AddWithValue("@CMI_FECHA", CMI_FECHA);
            command.Parameters.AddWithValue("@CMI_DIAS_REPOSO", CMI_DIAS_REPOSO);
            command.Parameters.AddWithValue("@CMI_ACTIVIDAD_LABORAL", CMI_ACTIVIDAD_LABORAL);
            command.Parameters.AddWithValue("@CMI_CONTINGENCIA", CMI_CONTINGENCIA);
            command.Parameters.AddWithValue("@CMI_CONFIRMADO", CMI_CONFIRMADO);
            command.Parameters.AddWithValue("@CMI_FECHA_CIRUGIA", CMI_FECHA_CIRUGIA);
            command.Parameters.AddWithValue("@CMI_DESCRIPCION_SINTOMAS", CMI_DESCRIPCION_SINTOMAS);
            command.Parameters.AddWithValue("@CMI_NOTA", CMI_NOTA);
            command.Parameters.AddWithValue("@CMI_TIPO_INGRESO", CMI_TIPO_INGRESO);
            command.Parameters.AddWithValue("@CMI_ESTADO", CMI_ESTADO);
            command.Parameters.AddWithValue("@CMI_ENFERMEDAD", CMI_ENFERMEDAD);
            command.Parameters.AddWithValue("@CMI_SINTOMAS", CMI_SINTOMAS);
            command.Parameters.AddWithValue("@CMI_REPOSO", CMI_REPOSO);
            command.Parameters.AddWithValue("@CMI_AISLAMIENTO", CMI_AISLAMIENTO);
            command.Parameters.AddWithValue("@CMI_TELETRABAJO", CMI_TELETRABAJO);
            command.Parameters.AddWithValue("@DIRECCION_PACIENTE", DIRECCION_PACIENTE);
            command.Parameters.AddWithValue("@TELEFONO_PACIENTE", TELEFONO_PACIENTE);
            command.Parameters.AddWithValue("@CMI_ANULADO", CMI_ANULADO);
            command.Parameters.AddWithValue("@CMI_FECHA_ALTA", CMI_FECHA_ALTA);
            command.Parameters.AddWithValue("@CMI_PROCEDIMIENTO", CMI_PROCEDIMIENTO);
            command.Parameters.AddWithValue("@CMI_TRATAMIENTO", CMI_TRATAMIENTO);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void InsertarCertificadoDetalle(string cie_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
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
            command.Connection = conexion;
            command.CommandText = "sp_Certificado_InsertarDetallePaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cie_codigo", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void InsertarCertificadoDetalleIESS(string cie_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
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
            command.Connection = conexion;
            command.CommandText = "sp_Certificado_DetalleIESS";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CIE_CODIGO", cie_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable CargarDatosCertificado(int ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_Certificado_Mostrar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarDatosCertificadoN(int ate_codigo, int cer_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_Certificado_MostrarN";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@cer_codigo", cer_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable CargarDatosCertificadoIESS(int ate_codigo, int CMI_CODIGO)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoIESS_Mostrar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@CMI_CODIGO", CMI_CODIGO);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarDatosCertificado_Detalle(Int64 cer_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_Certificado_Mostrar_Detalle";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cer_codigo", cer_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarDatosCertificadoIESS_Detalle(Int64 cer_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoIESS_Mostrar_Detalle";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CMI_CODIGO", cer_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public Int32 ContadorCertificado()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            Int32 CM = 0;
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
            command.CommandText = "select top 1 CER_CODIGO from CERTIFICADO_MEDICO order by 1 desc";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                {
                    CM = reader.GetInt32(0);
                }
                conexion.Close();
                return CM;
            }
            else
            {
                conexion.Close();
                return CM;
            }
        }
        public Int32 ContadorCertificadoEspecial()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            Int32 CME = 0;
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
            command.CommandText = "select top 1 CMI_CODIGO from CERTIFICADO_MEDICO_IESS order by 1 desc";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                {
                    CME = reader.GetInt32(0);
                }
                conexion.Close();
                return CME;
            }
            else
            {
                conexion.Close();
                return CME;
            }
        }
        public string PathImagen()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            string path = "";
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
            command.CommandText = "SELECT top 1 EMP_PATHIMAGEN FROM EMPRESA";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                {
                    path = reader.GetString(0);
                }
                conexion.Close();
                return path;
            }
            else
            {
                conexion.Close();
                return path;
            }
        }

        public string PathImagenPre()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            BaseContextoDatos obj = new BaseContextoDatos();
            string path = "";
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
            command.CommandText = "SELECT EMP_CONTADOR_DIRECCION FROM EMPRESA";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())//Obtenemos los datos de la columna y asignamos a los campos de la Cache de Usuario
                {
                    path = reader.GetString(0);
                }
                conexion.Close();
                return path;
            }
            else
            {
                conexion.Close();
                return path;
            }
        }
        public DataTable CargarCie10Hosp(Int64 ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoCie10Hosp";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarCie10Emerg(Int64 ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoCie10Emerg";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarCie10Consulta(Int64 ate_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoCie10Consulta";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable CargarHoras()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "SELECT * FROM MEDICAMENTOS_HORAS";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarDias()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "SELECT * FROM MEDICAMENTOS_DIAS";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarTipoContingencia()
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "select TC_CODIGO AS CODIGO, TC_DESCRIPCION AS DESCRIPCION from TIPO_CONTINGENCIA";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }

        public DataTable ReimpresionCertificado(Int32 cer_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "select * from CERTIFICADO_MEDICO CM INNER JOIN CERTIFICADO_MEDICO_DETALLE CMD ON CM.CER_CODIGO = CMD.CER_CODIGO where CM.CER_CODIGO = @cer_codigo";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@cer_codigo", cer_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }

        public DataTable ReimpresionCertificadoIESS(int cer_codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "select * from CERTIFICADO_MEDICO_IESS CM INNER JOIN CERTIFICADO_MEDICO_DETALLE_IESS CMD ON CM.CMI_CODIGO = CMD.CMI_CODIGO where CM.CMI_CODIGO = @cer_codigo";
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@cer_codigo", cer_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public bool InhabilitaCertificado(string motivo, string medico, Int32 codigoCertificado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = " UPDATE CERTIFICADO_MEDICO SET CER_ESTADO =0, CER_OBSERVACION += " + "' | " + motivo + "' WHERE CER_CODIGO= " + codigoCertificado;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return true;
        }
        public bool InhabilitaCertificadoIESS(string motivo, string medico, Int32 codigoCertificado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "update CERTIFICADO_MEDICO_IESS set CMI_ESTADO = 0, CMI_ANULADO ='" + motivo + "' WHERE CMI_CODIGO = " + codigoCertificado;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return true;
        }
        public bool InhabilitaRecetamedica(RECETAS_ANULADAS receta)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "update RECETA_MEDICA set RM_ESTADO = 0 WHERE RM_CODIGO = " + receta.RM_CODIGO;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();

            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    db.AddToRECETAS_ANULADAS(receta);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }

        }
        public DataTable TIPO_INGRESO_IESS(Int64 ateCodigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "select I.TIP_DESCRIPCION from ATENCIONES A inner join TIPO_INGRESO I ON A.TIP_CODIGO = I.TIP_CODIGO WHERE A.ATE_CODIGO =" + ateCodigo;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public DataTable VerificaEstado(Int64 ateCodigo, Int64 medCodigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = " SELECT * FROM CERTIFICADO_MEDICO C INNER JOIN ATENCIONES A ON C.ATE_CODIGO = A.ATE_CODIGO WHERE A.ATE_CODIGO = " + ateCodigo + " AND C.MED_CODIGO =" + medCodigo + " AND A.ATE_FECHA_ALTA <> ''  ORDER BY 2 DESC ";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public DataTable VerificaEstadoIESS(Int64 ateCodigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = " SELECT * FROM CERTIFICADO_MEDICO_IESS C INNER JOIN ATENCIONES A ON C.ATE_CODIGO = A.ATE_CODIGO  WHERE A.ATE_CODIGO = " + ateCodigo + " ORDER BY 1 DESC ";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }


        public Int64 Med_Codigo(string id)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            Int64 codigo = 0;
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
            try
            {
                command.Connection = conexion;
                command = new SqlCommand("sp_ConectarMedico", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    codigo = Convert.ToInt64(reader["MED_CODIGO"].ToString());
                }
                reader.Close();
                command.Parameters.Clear();
                conexion.Close();
                return codigo;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return codigo;
            }

        }

        public Int64 idReceta()
        {
            Int64 id = 0;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<RECETA_MEDICA> receta = db.RECETA_MEDICA.ToList();
                if (receta.Count > 0)
                    id = db.RECETA_MEDICA.Max(i => i.RM_CODIGO);
                else
                    id = 0;
                return id;
            }
        }
        public Int64 idRecetaDiagnostico()
        {
            Int64 id = 0;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<RECETA_DIAGNOSTICO> receta = db.RECETA_DIAGNOSTICO.ToList();
                if (receta.Count > 0)
                    id = db.RECETA_DIAGNOSTICO.Max(i => i.RD_CODIGO);
                else
                    id = 0;
                return id;
            }
        }
        public Int64 idRecetaMedicamentos()
        {
            Int64 id = 0;
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<RECETA_MEDICAMENTOS> receta = db.RECETA_MEDICAMENTOS.ToList();
                if (receta.Count > 0)
                    id = db.RECETA_MEDICAMENTOS.Max(i => i.RMD_CODIGO);
                else
                    id = 0;
                return id;
            }
        }

        public bool InsertarReceta(RECETA_MEDICA receta, List<RECETA_DIAGNOSTICO> dignostico, List<RECETA_MEDICAMENTOS> medicamento)
        {

            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            connection = obj.ConectarBd();
            connection.Open();

            transaction = connection.BeginTransaction();
            try
            {
                command = new SqlCommand("sp_RecetaCrear", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@ate_codigo", receta.ATE_CODIGO);
                command.Parameters.AddWithValue("@alergias", receta.RM_ALERGIAS);
                command.Parameters.AddWithValue("@med_codigo", receta.MED_CODIGO);
                command.Parameters.AddWithValue("@cita", receta.RM_CITA);
                command.Parameters.AddWithValue("@id_usuario", receta.ID_USUARIO);
                command.Parameters.AddWithValue("@signos", receta.RM_SIGNO);
                command.Parameters.AddWithValue("@farmacos", receta.RM_FARMACOS);
                command.Parameters.AddWithValue("@tipo", receta.TIP_CODIGO);
                command.Parameters.AddWithValue("@consulta", receta.TC_CONSULTA);
                command.Parameters.AddWithValue("@telefono", receta.MED_TELEFONO);

                command.ExecuteNonQuery();
                command.Parameters.Clear();

                foreach (var item in dignostico)
                {
                    command = new SqlCommand("sp_RecetaDiagnosticoCrear", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@rm_codigo", item.RM_CODIGO);
                    command.Parameters.AddWithValue("@cie", item.CIE_CODIGO);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                foreach (var item in medicamento)
                {
                    command = new SqlCommand("sp_RecetaMedicamentosCrear", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@rm_codigo", item.RM_CODIGO);
                    command.Parameters.AddWithValue("@codpro", item.CODPRO);
                    command.Parameters.AddWithValue("@indicaciones", item.RMD_INDICACIONES);
                    command.Parameters.AddWithValue("@administracion", item.RMD_ADMINISTRACION);
                    command.Parameters.AddWithValue("@presentacion", item.RMD_PRESENTACION);
                    command.Parameters.AddWithValue("@concentracion", item.RMD_CONCENTRACION);
                    command.Parameters.AddWithValue("@cantidad", item.RMD_CANTIDAD);
                    command.Parameters.AddWithValue("@comercial", item.RMD_COMERCIAL);
                    command.Parameters.AddWithValue("@descripcion", item.RMD_DESCRIPCION);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.Write(ex.Message);
                return false;
            }
        }

        public bool UpdateReceta(RECETA_MEDICA receta, List<RECETA_DIAGNOSTICO> diagnostico, List<RECETA_MEDICAMENTOS> medicamentos)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlTransaction transaction;

            connection = obj.ConectarBd();
            connection.Open();

            transaction = connection.BeginTransaction();

            try
            {
                command = new SqlCommand("sp_RecetaEditar", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@rm_codigo", receta.RM_CODIGO);
                command.Parameters.AddWithValue("@alergias", receta.RM_ALERGIAS);
                command.Parameters.AddWithValue("@med_codigo", receta.MED_CODIGO);
                command.Parameters.AddWithValue("@cita", receta.RM_CITA);
                command.Parameters.AddWithValue("@id_usuario", receta.ID_USUARIO);
                command.Parameters.AddWithValue("@signos", receta.RM_SIGNO);
                command.Parameters.AddWithValue("@farmacos", receta.RM_FARMACOS);
                command.Parameters.AddWithValue("@tipo", receta.TIP_CODIGO);
                command.Parameters.AddWithValue("@consulta", receta.TC_CONSULTA);
                command.Parameters.AddWithValue("@telefono", receta.MED_TELEFONO);

                command.ExecuteNonQuery();
                command.Parameters.Clear();
                foreach (var item in diagnostico)
                {
                    command = new SqlCommand("sp_RecetaDiagnosticoCrear", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@rm_codigo", item.RM_CODIGO);
                    command.Parameters.AddWithValue("@cie", item.CIE_CODIGO);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                foreach (var item in medicamentos)
                {
                    command = new SqlCommand("sp_RecetaMedicamentosCrear", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Transaction = transaction;
                    command.Parameters.AddWithValue("@rm_codigo", item.RM_CODIGO);
                    command.Parameters.AddWithValue("@codpro", item.CODPRO);
                    command.Parameters.AddWithValue("@indicaciones", item.RMD_INDICACIONES);
                    command.Parameters.AddWithValue("@administracion", item.RMD_ADMINISTRACION);
                    command.Parameters.AddWithValue("@presentacion", item.RMD_PRESENTACION);
                    command.Parameters.AddWithValue("@concentracion", item.RMD_CONCENTRACION);
                    command.Parameters.AddWithValue("@cantidad", item.RMD_CANTIDAD);
                    command.Parameters.AddWithValue("@comercial", item.RMD_COMERCIAL);
                    command.Parameters.AddWithValue("@descripcion", item.RMD_DESCRIPCION);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                transaction.Rollback();
                return false;
            }
        }
        public bool ExisteReceta(Int64 ate_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var existe = (from rc in db.RECETA_MEDICA
                              where rc.ATE_CODIGO == ate_codigo
                              select rc).FirstOrDefault();

                if (existe != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public bool ExisteRecetaMedico(Int64 ate_codigo, Int64 med_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var existe = (from rc in db.RECETA_MEDICA
                              where rc.ATE_CODIGO == ate_codigo && rc.MED_CODIGO == med_codigo && rc.RM_ESTADO == true
                              select rc).FirstOrDefault();

                if (existe != null)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public RECETA_MEDICA RecuperaCabecera(Int64 ate_codigo, Int64 RM_CODIGO)
        {
            RECETA_MEDICA receta = new RECETA_MEDICA();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                receta = (from rc in db.RECETA_MEDICA
                          where rc.ATE_CODIGO == ate_codigo && rc.RM_ESTADO == true && rc.RM_CODIGO == RM_CODIGO
                          select rc).FirstOrDefault();
                return receta;
            }
        }
        public List<RECETA_DIAGNOSTICO> RecuperarDiagnostico(Int64 rm_codigo)
        {
            List<RECETA_DIAGNOSTICO> diagnostico = new List<RECETA_DIAGNOSTICO>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                diagnostico = (from d in db.RECETA_DIAGNOSTICO
                               where d.RM_CODIGO == rm_codigo
                               select d).ToList();
                return diagnostico;
            }
        }
        public CERTIFICADO_PRESENTACION RecuperarCertificadoPresentacion(Int64 ate_codigo, Int64 medico)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from d in db.CERTIFICADO_PRESENTACION
                        where d.ate_codigo == ate_codigo && d.medico == medico && d.estado == true
                        select d).FirstOrDefault();
            }
        }

        public CERTIFICADO_MEDICO_IESS RecuperaCertificadoIESSDuplicado(Int64 ate_codigo, Int64 medico)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from d in db.CERTIFICADO_MEDICO_IESS
                        where d.ATE_CODIGO == ate_codigo && d.MED_CODIGO == medico && d.CMI_ESTADO == true
                        select d).FirstOrDefault();
            }
        }
        public List<RECETA_MEDICAMENTOS> RecuperaMedicamentos(Int64 rm_codigo)
        {
            List<RECETA_MEDICAMENTOS> medicamentos = new List<RECETA_MEDICAMENTOS>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                medicamentos = (from m in db.RECETA_MEDICAMENTOS
                                where m.RM_CODIGO == rm_codigo
                                select m).ToList();
                return medicamentos;
            }
        }
        public List<VIA_ADMINISTRACION_MEDICAMENTO> ViaAdministracionTodo()
        {
            List<VIA_ADMINISTRACION_MEDICAMENTO> via = new List<VIA_ADMINISTRACION_MEDICAMENTO>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                via = (from v in db.VIA_ADMINISTRACION_MEDICAMENTO
                       orderby v.Detalle ascending
                       select v).ToList();

                return via;
            }
        }
        public List<VIA_ADMINISTRACION_MEDICAMENTO> RecuperarPorNombre(string via)
        {
            List<VIA_ADMINISTRACION_MEDICAMENTO> Lvia = new List<VIA_ADMINISTRACION_MEDICAMENTO>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Lvia = (from v in db.VIA_ADMINISTRACION_MEDICAMENTO
                        where v.Detalle == via
                        select v).ToList();
                return Lvia;
            }
        }

        public bool GuardaCertificadoPresentacion(CERTIFICADO_PRESENTACION obj)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    obj.fechaEmision = DateTime.Now;
                    obj.estado = true;
                    db.AddToCERTIFICADO_PRESENTACION(obj);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
        public DataTable RecuperarPBasico(string codigo)
        {

            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT DESCRIPCION FROM PRODUCTOS_TEMPORALES where CODIGO = @codigo GROUP BY DESCRIPCION", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@codigo", codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;

        }
        public DataTable ProductosBasicos(string filtro)
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT * FROM PRODUCTOS_TEMPORALES where DESCRIPCION LIKE '%' + @filtro + '%'", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@filtro", filtro);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }
        public DataTable CuadroBasico()
        {
            SqlCommand command;
            SqlConnection connection;
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select CODIGO, DESCRIPCION + ' ' + CONCENTRACION AS PRODUCTO from PRODUCTOS_TEMPORALES", connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return Tabla;
        }

        public List<TIPO_PRESENTACION> presentacion()
        {
            List<TIPO_PRESENTACION> Lpresentacion = new List<TIPO_PRESENTACION>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Lpresentacion = (from p in db.TIPO_PRESENTACION
                                 orderby p.TP_DESCRIPCION ascending
                                 select p).ToList();
                return Lpresentacion;
            }
        }
        public List<TIPO_CONSULTA> consulta()
        {
            List<TIPO_CONSULTA> Lconsulta = new List<TIPO_CONSULTA>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                Lconsulta = (from c in db.TIPO_CONSULTA
                             orderby c.TC_DESCRIPCION ascending
                             select c).ToList();
                return Lconsulta;
            }
        }
        public List<TIPO_CONSULTA> ConsultaHospitalaria()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in db.TIPO_CONSULTA
                        where c.TC_CODIGO == 1 || c.TC_CODIGO == 3
                        orderby c.TC_DESCRIPCION ascending
                        select c).ToList();
            }
        }
        public double UltimoRegistroPT()
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            double idPT = 0;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select MAX(CODIGO) AS CODIGO from PRODUCTOS_TEMPORALES", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                idPT = Convert.ToDouble(reader["CODIGO"].ToString());
            }
            reader.Close();
            connection.Close();
            return idPT;
        }

        public List<DtoPacienteReceta> ExploradorReceta(DateTime desde, DateTime hasta)
        {
            List<DtoPacienteReceta> pacienteRecetas = new List<DtoPacienteReceta>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var Receta = (from r in db.RECETA_MEDICA
                              join a in db.ATENCIONES on r.ATE_CODIGO equals a.ATE_CODIGO
                              join p in db.PACIENTES on a.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                              join h in db.HABITACIONES on a.HABITACIONES.hab_Codigo equals h.hab_Codigo
                              join m in db.MEDICOS on r.MED_CODIGO equals m.MED_CODIGO
                              where r.RM_FECHA >= desde && r.RM_FECHA <= hasta && r.RM_ESTADO == true
                              orderby r.RM_FECHA descending
                              select new
                              {
                                  r,
                                  a,
                                  p,
                                  h,
                                  m
                              }).ToList();
                foreach (var item in Receta)
                {
                    DtoPacienteReceta pReceta = new DtoPacienteReceta();
                    pReceta.Atencion = item.a.ATE_NUMERO_ATENCION;
                    pReceta.ate_codigo = item.a.ATE_CODIGO;
                    pReceta.Codigo = item.r.RM_CODIGO;
                    pReceta.Fecha = (DateTime)item.r.RM_FECHA;
                    pReceta.Hab = item.h.hab_Numero;
                    pReceta.HCL = item.p.PAC_HISTORIA_CLINICA;
                    pReceta.Medico = item.m.MED_APELLIDO_PATERNO + " " + item.m.MED_APELLIDO_MATERNO + " " + item.m.MED_NOMBRE1 + " " + item.m.MED_NOMBRE2;
                    pReceta.Paciente = item.p.PAC_APELLIDO_PATERNO + " " + item.p.PAC_APELLIDO_MATERNO + " " + item.p.PAC_NOMBRE1 + " " + item.p.PAC_NOMBRE2;

                    pacienteRecetas.Add(pReceta);

                }
            }
            return pacienteRecetas;
        }
        public TIPO_CONSULTA RecuperarConsulta(int codigo)
        {
            TIPO_CONSULTA consulta = new TIPO_CONSULTA();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                consulta = (from tc in db.TIPO_CONSULTA
                            where tc.TC_CODIGO == codigo
                            select tc).FirstOrDefault();
            }
            return consulta;
        }
        public DataTable CertificadosPresentacion(DateTime fechainicio, DateTime fechafin, bool estado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoPresentacion";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CertificadoPresentacionXmedicos(DateTime fechainicio, DateTime fechafin, int codMedico, bool estado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_CertificadoPresentacionXmedico";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@idMedico", codMedico);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public MEDICOS DatosMedicos(Int64 _MED_CODIGO)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from m in db.MEDICOS
                        where m.USUARIOS.ID_USUARIO == _MED_CODIGO
                        select m).FirstOrDefault();
            }
        }
        public bool InhabilitaCertificadoPrecentacion(string motivo, string medico, Int32 codigoCertificado)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
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
            command.CommandText = "update CERTIFICADO_PRESENTACION set estado = 0 WHERE id = " + codigoCertificado;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return true;
        }
        public Int64 Med_CodigoCertificadoAsistencia(string id)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            Int64 codigo = 0;
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
            try
            {
                command.Connection = conexion;
                command = new SqlCommand("sp_ConectarMedicoAsistencia", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", id);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    codigo = Convert.ToInt64(reader["ID_USUARIO"].ToString());
                }
                reader.Close();
                command.Parameters.Clear();
                conexion.Close();
                return codigo;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return codigo;
            }

        }
    }
}
