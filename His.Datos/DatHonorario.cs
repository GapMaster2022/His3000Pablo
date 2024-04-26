using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;

namespace His.Datos
{
    public class DatHonorario
    {
        SqlConnection conexion;
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        BaseContextoDatos obj = new BaseContextoDatos();

        public DataTable VerPacientes()
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
            command.CommandText = "sp_HE_Pacientes";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha(DateTime fechadesde, DateTime fechahasta)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroHc(string hc)
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
            command.CommandText = "sp_HE_PacientesFiltroHC";
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
        public DataTable FiltroMedico(int med_codigo)
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
            command.CommandText = "sp_HE_PacientesFiltroMedico";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroVale(string numvale)
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
            command.CommandText = "sp_HE_PacientesFiltroVale";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numVale", numvale);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFactura(string numFac)
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
            command.CommandText = "sp_HE_PacientesFiltroFactura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numFac", numFac);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Hc(string fechadesde, string fechahasta, string hc)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Hc";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@hc", hc);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Medico(string fechadesde, string fechahasta, int med_codigo)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Medico";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Vale(string fechadesde, string fechahasta, string numvale)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Vale";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@numvale", numvale);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Factura(string fechadesde, string fechahasta, string numFac)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@numFac", numFac);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
            public DataTable FiltroHc_Medico(string hc, int med_codigo)
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
                command.CommandText = "sp_HE_PacientesFiltroHC_Medico";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@hc", hc);
                command.Parameters.AddWithValue("@med_codigo", med_codigo);
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                command.Parameters.Clear();
                reader.Close();
                conexion.Close();
                return Tabla;
        }
        public DataTable FiltroHc_Vale(string hc, string numvale)
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
            command.CommandText = "sp_HE_PacientesFiltroHC_Vale";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@numvale", numvale);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroHc_Factura(string hc, string numFac)
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
            command.CommandText = "sp_HE_PacientesFiltroHC_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@numFac", numFac);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroMedico_Vale(int med_codigo, string numvale)
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
            command.CommandText = "sp_HE_PacientesFiltroMedico_Vale";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@numvale", numvale);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroMedico_Factura(int med_codigo, string numFac)
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
            command.CommandText = "sp_HE_PacientesFiltroMedico_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@numFac", numFac);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroVale_Factura(string numvale, string numFac)
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
            command.CommandText = "sp_HE_PacientesFiltroVale_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@numvale", numvale);
            command.Parameters.AddWithValue("@numFac", numFac);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Hc_Medico(string fechadesde, string fechahasta, string hc, int med_codigo)
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
            command.CommandText = "sp_HE_PacientesFiltroVale_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Hc_Vale(string fechadesde, string fechahasta, string hc, string numvale)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Hc_Vale";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@numvale", numvale);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Hc_Factura(string fechadesde, string fechahasta, string hc, string numFac)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Hc_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@numFac", numFac);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Medico_Vale(string fechadesde, string fechahasta, int med_codigo, string numvale)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Medico_Vale";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@numvale", numvale);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Medico_Factura(string fechadesde, string fechahasta, int med_codigo, string numFac)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Medico_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@numFac", numFac);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroFecha_Vale_Factura(string fechadesde, string fechahasta, string numvale, string numFac)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Vale_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.Parameters.AddWithValue("@numvale", numvale);
            command.Parameters.AddWithValue("@numFac", numFac);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable FiltroHc_Medico_Vale(string hc, int med_codigo, string numvale)
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
            command.CommandText = "sp_HE_PacientesFiltroFecha_Vale_Factura";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hc", hc);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@numvale", numvale);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public DataTable RecuperarPedido(long cue_codigo)
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
            command.CommandText = "sp_HE_RecuperarPedido";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cue_codigo", cue_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
        public void GuardarPedidoDevolucion(long cue_codigo, int usuario)
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
            command.CommandText = "sp_HE_PedidoDevolucion";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@cue_codigo", cue_codigo);
            command.Parameters.AddWithValue("@usuario", usuario);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void GuardarPedidoDevolucionDetalle(int pro_codigo, string pro_descripcion, int cantidad, double valor, double iva, long pdd_codigo)
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
            command.CommandText = "sp_HE_PedidoDevolucion";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@pro_codigo", pro_codigo);
            command.Parameters.AddWithValue("@pro_descripcion", pro_descripcion);
            command.Parameters.AddWithValue("@cantidad", cantidad);
            command.Parameters.AddWithValue("@valor", valor);
            command.Parameters.AddWithValue("@iva", iva);
            command.Parameters.AddWithValue("@pdd_codigo", pdd_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
        public void Eliminar(long cue_codigo)
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
            command.CommandText = "sp_HE_PedidoDevolucion";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@codigo", cue_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            conexion.Close();
        }
    }
}
