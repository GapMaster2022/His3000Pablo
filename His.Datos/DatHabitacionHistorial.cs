using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;
using Microsoft.Data.Extensions;

namespace His.Datos
{
   public class DatHabitacionHistorial
    {
       public void CrearHabitacionHistorial(HABITACIONES_HISTORIAL habitacionHistorial)
       {
           using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
           {
               contexto.Crear("HABITACIONES_HISTORIAL", habitacionHistorial);
           }
       }
       public int RecuperaMaximoHabitacionHistorial()
       {
           using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
           {
               var id = contexto.HABITACIONES_HISTORIAL.OrderByDescending(h => h.HAH_CODIGO).FirstOrDefault();
               if (id != null) return id.HAH_CODIGO + 1;
               return 1;
           }
       }
        public void HabTipoIngreso(Int64 ate_codigo, Int64 hah_codigo, Int64 had_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_CambiosHistorialAreas", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ate_codigo", ate_codigo);
            command.Parameters.AddWithValue("@hah_codigo", hah_codigo);
            command.Parameters.AddWithValue("@had_codigo", had_codigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }

        public void FechaAltaHistorial(Int64 ateCodigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_FechaAltaHistorial", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ateCodigo", ateCodigo);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
    }
}
