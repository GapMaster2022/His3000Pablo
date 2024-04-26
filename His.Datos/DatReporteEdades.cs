using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Core.Datos;

namespace His.Datos
{
    public class DatReporteEdades
    {
        SqlConnection conexion;
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;
        BaseContextoDatos obj = new BaseContextoDatos();

        public DataTable CargarAtenciones() 
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
            command.CommandText = "sp_ReporteEdades_Atenciones";
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }

        public DataTable EdadesxAtencion(int tip_codigo, DateTime fechadesde, DateTime fechahasta)
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
            command.CommandText = "sp_ReporteEdades_PacientexAtencion";
            command.Parameters.AddWithValue("@tip_codigo", tip_codigo);
            command.Parameters.AddWithValue("@fechadesde", fechadesde);
            command.Parameters.AddWithValue("@fechahasta", fechahasta);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            conexion.Close();
            return Tabla;
        }
    }
}
