using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;

namespace His.Datos
{
    public class DatControlHc
    {
        SqlConnection conexion;
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        BaseContextoDatos obj = new BaseContextoDatos();

        public DataTable ControlPorFecha(DateTime fechainicio, DateTime fechafin)
        {
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
            command.CommandText = "sp_ControlporFecha";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable ControlPorFechaAlta(DateTime fechainicio, DateTime fechafin)
        {
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
            command.CommandText = "sp_ControlporFechaAlta";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable ControlPorHc(string hc)
        {
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
            command.CommandText = "sp_ControlporHC";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hc", hc);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable MostrarDocumentos()
        {
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
            command.CommandText = "sp_ControlMostrarDocumentos";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable PorAtencionControl(string atencion, int codigo)
        {
            DataTable Tabla = new DataTable();
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
            command.CommandText = "sp_ControlDocumentosHC";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@codigo", codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable ControlPorEstado(string estado)
        {
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
            command.CommandText = "sp_ControlporEstado";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable ControlPorFechayHc(DateTime fechainicio, DateTime fechafin, string hc)
        {
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
            command.CommandText = "sp_ControlporFechayHc";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable ControlPorFechaAltayHc(DateTime fechainicio, DateTime fechafin, string hc)
        {
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
            command.CommandText = "sp_ControlporFechaAltayHc";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable ControlPorFechayEstado(DateTime fechainicio, DateTime fechafin, string estado)
        {
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
            command.CommandText = "sp_ControlFechayEstado";
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
        public DataTable ControlPorFechaAltayEstado(DateTime fechainicio, DateTime fechafin, string estado)
        {
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
            command.CommandText = "sp_ControlFechaAltayEstado";
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
        public DataTable ControlPorHcyEstado(string hc,string estado)
        {
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
            command.CommandText = "sp_ControlHCyEstado";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            command.Parameters.Clear();
            conexion.Close();
            return Tabla;
        }
        public DataTable ControlPorFechaHCEstado(DateTime fechainicio, DateTime fechafin, string hc, string estado)
        {
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
            command.CommandText = "sp_ControlFechaHCEstado";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable ControlPorFechaAltaHCEstado(DateTime fechainicio, DateTime fechafin, string hc, string estado)
        {
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
            command.CommandText = "sp_ControlFechaAltaHCEstado";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechainicio", fechainicio);
            command.Parameters.AddWithValue("@fechafin", fechafin);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@estado", estado);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void ControlInsertar(string estado, string fecha, string usuario, int paciente, int control, 
            int atencion,  string estatus)
        {
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
            command.CommandText = "sp_ControlInsertar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@estado", estado);
            command.Parameters.AddWithValue("@fecha", fecha);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.Parameters.AddWithValue("@paciente", paciente);
            command.Parameters.AddWithValue("@control", control);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@estatus", estatus);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public string Cantidad()
        {
            string valor = null;
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
            command.CommandText = "sp_ControlNumero";
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter v = new SqlParameter("@valor", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            valor = command.Parameters["@valor"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return valor;
        }
        public void ControlActualizar(int atencion, int control)
        {
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
            command.CommandText = "sp_ControlActualizar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@control", control);
            command.Parameters.AddWithValue("@atencion", atencion);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public DataTable TablaControlHc()
        {
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
            command.CommandText = "sp_TablaControlHc";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void ControlActualizaInserta(int codigo, string descripcion)
        {
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
            command.CommandText = "sp_ControlActualizarInsertar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codigo", codigo);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public string UltimoNumero()
        {
            string valor = null;
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
            command.CommandText = "sp_DocumentoNumero";
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter v = new SqlParameter("@numero", 0); v.Direction = ParameterDirection.Output;
            command.Parameters.Add(v);
            command.ExecuteNonQuery();
            valor = command.Parameters["@numero"].Value.ToString();
            command.Parameters.Clear();
            conexion.Close();
            return valor;
        }
        public void ControlEliminar(int codigo)
        {
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
            command.CommandText = "sp_ControlEliminar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codigo", codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void ControlCerrar(Int64 atencion, string estatus)
        {
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
            command.CommandText = "sp_ControlCerrar";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@estatus", estatus);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            try
            { 
            conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ControlAbrir(Int64 atencion, string estatus)
        {
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
            command.CommandText = "sp_ControlAbrir";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@atencion", atencion);
            command.Parameters.AddWithValue("@estatus", estatus);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
