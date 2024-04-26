using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data;

namespace His.Datos
{
    public class ControllerCgMaestro
    {
        public bool AnularAD(Int64 hom_codigo)
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
                command = new SqlCommand("sp_HonorariosAnulacionTotal", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@hom_codigo", hom_codigo);
                command.Parameters.AddWithValue("@usuario", His.Entidades.Clases.Sesion.codUsuario);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
                return false;
            }
        }
    }
}
