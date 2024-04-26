using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using His.Entidades;
using Core.Datos;
using System.Data;

namespace His.Datos
{
    public class DatosGarantias
    {
        public DataTable Garantias()
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "sp_CargarPacienteGarantia";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable GarantiasTodo()
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
            command.CommandText = "sp_CargarPacienteGarantiaTodo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable TipoGarantia()
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            command.Connection = conexion;
            command.CommandText = "Select * From TIPO_GARANTIA";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public DataTable Banco()
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
            command.CommandText = "Select * From BANCOS";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public DataTable TipoTarjeta()
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
            command.CommandText = "select forpag as codigo, despag as name from Sic3000..Forma_Pago where claspag=4";
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.ExecuteNonQuery();
            conexion.Close();
            return Tabla;
        }
        public void InsertarGarantia(int tipogarantia, int atencion, string beneficiario, string documento, 
        double valor, string fecha, string banco, string tipotarjeta, string ccv,
            int dias, string lote, string autorizacion, string persona, string fechaau, 
            string establecimiento, string numtarjeta, string usuario,
            string identificacion, string telefono, string caducidad)
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
            command.CommandText = "sp_InsertarGarantia";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@tipogarantia", tipogarantia);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@beneficiario", beneficiario);
            command.Parameters.AddWithValue("@documento", documento);
            command.Parameters.AddWithValue("@valor", valor);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.Parameters.AddWithValue("@banco", banco);
            command.Parameters.AddWithValue("@tipotarjeta", tipotarjeta);
            command.Parameters.AddWithValue("@ccv", ccv);
            command.Parameters.AddWithValue("@dias", dias);
            command.Parameters.AddWithValue("@lote", lote);
            command.Parameters.AddWithValue("@autorizacion", autorizacion);
            command.Parameters.AddWithValue("@persona", persona);
            command.Parameters.AddWithValue("@fechaau", fechaau);
            command.Parameters.AddWithValue("@establecimiento", establecimiento);
            command.Parameters.AddWithValue("@numtarjeta", numtarjeta);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.Parameters.AddWithValue("@identificacion", identificacion);
            command.Parameters.AddWithValue("@telefono", telefono);
            command.Parameters.AddWithValue("@caducidad", caducidad);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            command.Parameters.Clear();
            conexion.Close();
        }
        public void ModificarGarantia(int tipogarantia, string beneficiario, string documento, 
            double valor, string fecha, string banco, string tipotarjeta, string ccv,
            int dias, string lote, string autorizacion, string persona, string fechaau, 
            string establecimiento, int codigo, string numtarjeta, string usuario
            ,string identificacion, string telefono, string caducidad)
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
            command.CommandText = "sp_ModificarGarantia";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@tipogarantia", tipogarantia);
            //command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@beneficiario", beneficiario);
            command.Parameters.AddWithValue("@documento", documento);
            command.Parameters.AddWithValue("@valor", valor);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.Parameters.AddWithValue("@banco", banco);
            command.Parameters.AddWithValue("@tipotarjeta", tipotarjeta);
            command.Parameters.AddWithValue("@ccv", ccv);
            command.Parameters.AddWithValue("@dias", dias);
            command.Parameters.AddWithValue("@lote", lote);
            command.Parameters.AddWithValue("@autorizacion", autorizacion);
            command.Parameters.AddWithValue("@persona", persona);
            command.Parameters.AddWithValue("@fechaau", fechaau);
            command.Parameters.AddWithValue("@establecimiento", establecimiento);
            command.Parameters.AddWithValue("@codigo", codigo);
            command.Parameters.AddWithValue("@numtarjeta", numtarjeta);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.Parameters.AddWithValue("@identificacion", identificacion);
            command.Parameters.AddWithValue("@telefono", telefono);
            command.Parameters.AddWithValue("@caducidad", caducidad);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public string Aseguradora(string codigo)
        {
            SqlConnection conexion;
            SqlCommand command = new SqlCommand();
            SqlDataReader reader;
            BaseContextoDatos obj = new BaseContextoDatos();
            string seguro = null;
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
            command.CommandText = "sp_CategoriasGarantiaPaciente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@codigo", codigo);
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.IsDBNull(reader.GetOrdinal("Aseguradora")))
            {
                seguro = reader.GetString(reader.GetOrdinal("Aseguradora"));
                command.Parameters.Clear();
                conexion.Close();
                return seguro;
            }
            else
            {
                seguro = "";
                command.Parameters.Clear();
                conexion.Close();
                return seguro;
            }
        }
        public DataTable CargarGarantiaFechas(string fechainicio, string fechafin, string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechaTodas";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarGarantiaFechasCaduca(string fechainicio, string fechafin, string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechaCaduca";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarGarantiaFechasCancelado(string fechainicio, string fechafin, string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechaCancelado";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarGarantiaFechasVigente(string fechainicio, string fechafin, string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechaVigente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void InsertarAnulacion(string fechaanular, string usuarioanula, int codigo, string observacion)
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
            command.CommandText = "sp_AnularPreAutorizacion";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechaanular", fechaanular);
            command.Parameters.AddWithValue("@usuarioanula", usuarioanula);
            command.Parameters.AddWithValue("@codigo", codigo);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void InsertarAprobacion(string fechaaprobado, string usuarioaprueba, int codigo, string observacion)
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
            command.CommandText = "sp_Aprobar_Autorizacion";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechaaprobado", fechaaprobado);
            command.Parameters.AddWithValue("@usuarioaprueba", usuarioaprueba);
            command.Parameters.AddWithValue("@codigo", codigo);
            command.Parameters.AddWithValue("@observacion", observacion);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void CaducaPreAutorizacion(int codigo, string fechahisto)
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
            command.CommandText = "sp_PreAutorizacionCaduca";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@codigo", codigo);
            command.Parameters.AddWithValue("@fechahisto", fechahisto);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable CargarPacienteGarantiaTodo(string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteTodoFiltro";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@HC", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarPacienteGarantiaCaduca(string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteTodoCaducadas";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@HC", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarPacienteGarantiaCancelada(string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteTodoCanceladas";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@HC", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarPacienteGarantiaVigente(string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteTodoVigente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@HC", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable CargarGarantiaHC(string hc)
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
            command.CommandText = "sp_CargarPacienteHC";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        //Aqui
        public DataTable PorFechas(DateTime fechainicio, DateTime fechafin)
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
            command.CommandText = "sp_CargarGarantiaFechaSolo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechasHc(string fechainicio, string fechafin, string hc)
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
            command.CommandText = "sp_CargarGarantiaFechaHC";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechasHcCaducada(string fechainicio, string fechafin, string hc)
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
            command.CommandText = "sp_CargarGarantiaFechaHCCaducada";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechasHcCancelada(string fechainicio, string fechafin, string hc)
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
            command.CommandText = "sp_CargarGarantiaFechaHCCancelado";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechasHcVigente(string fechainicio, string fechafin, string hc)
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
            command.CommandText = "sp_CargarGarantiaFechaHCVigente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechasHcTipo(string fechainicio, string fechafin, string hc, int tipo)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechaTipo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorHc(string hc)
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
            command.CommandText = "sp_CargarPacienteGarantiaHc";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorHcCaducada(string hc)
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
            command.CommandText = "sp_CargarPacienteGarantiaHcCaducada";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorHcCancelada(string hc)
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
            command.CommandText = "sp_CargarPacienteGarantiaHcCancelada";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorHcVigente(string hc)
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
            command.CommandText = "sp_CargarPacienteGarantiaHcVigente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@hc", hc);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorTodo()
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
            command.CommandText = "sp_CargarTodo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorCaducada()
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
            command.CommandText = "sp_CargarCaducada";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorCancelada()
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
            command.CommandText = "sp_CargarCancelada";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorVigente()
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
            command.CommandText = "sp_CargarVigente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorTipo(int tipo)
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
            command.CommandText = "sp_CargarPacienteGarantiaTipo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechayTipo(string fechainicio, string fechafin, int tipo)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechayTipo";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@tipo", tipo);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechayCaducada(string fechainicio, string fechafin)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechayCaducada";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechayCancelada(string fechainicio, string fechafin)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechayCancelada";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechayVigente(string fechainicio, string fechafin)
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
            command.CommandText = "sp_CargarPacienteGarantiaFechayVigente";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorFechasTodo(DateTime fechainicio, DateTime fechafin)
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
            command.CommandText = "sp_CargarPacienteGarantiaTodoFecha";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
    }
}
