using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatTarifario
    {
        public List<TARIFARIOS_DETALLE> listaTarifarios()
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<TARIFARIOS_DETALLE> tarifarios = new List<TARIFARIOS_DETALLE>();
                tarifarios = contexto.TARIFARIOS_DETALLE.ToList();
                return tarifarios;

            }

        }
        

        public TARIFARIO_IESS RecuperarTarifarioIess(int codigoT)
        {
            using ( var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TARIFARIO_IESS
                        where t.COD_IESS == codigoT
                        select t).FirstOrDefault();
            }
        }

       
        public TARIFARIOS_DETALLE RecuperarTarifarioHono(string codigoH)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from td in contexto.TARIFARIOS_DETALLE
                        join e in contexto.ESPECIALIDADES_TARIFARIOS on td.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals e.EST_CODIGO
                        join t in contexto.TARIFARIOS on e.TARIFARIOS.TAR_CODIGO equals 1                                                 
                        where td.TAD_REFERENCIA == codigoH
                        orderby  td.TAD_UVR
                        select td).FirstOrDefault();               
            }
        }

        public List<TARIFARIOS_DETALLE> ListaRecuperarTarifarioHono(string codigoH)
            {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TARIFARIOS_DETALLE
                        where t.TAD_REFERENCIA == codigoH
                        select t).ToList();
            }
        }

        public DataTable recuperar_Tarifarios_Cirugia()
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
            Sqlcmd = new SqlCommand("sp_recuperar_Tarifarios_Cirugia", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;      

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }
        public DataTable recuperar_Tarifarios(string  codigo,string descripcion)
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
            Sqlcmd = new SqlCommand("sp_ListaTarifarios", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@codigo", SqlDbType.VarChar);
            Sqlcmd.Parameters["@codigo"].Value = Convert.ToString(codigo);

            Sqlcmd.Parameters.Add("@descripcion", SqlDbType.VarChar);
            Sqlcmd.Parameters["@descripcion"].Value = Convert.ToString(descripcion);

      

            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        public DataTable Tarifario(string busqueda, bool codigo, bool descripcion)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            SqlDataReader reader;

            connection = obj.ConectarBd();
            connection.Open();
            if(busqueda == "")
            {
                command = new SqlCommand("SELECT DISTINCT TAD_REFERENCIA AS CODIGO, LTRIM(TAD_DESCRIPCION) AS DESCRIPCION FROM tarifario_detalle ORDER BY LTRIM(TAD_DESCRIPCION) ASC", connection);
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 180;
                reader = command.ExecuteReader();
                Tabla.Load(reader);
                reader.Close();
                connection.Close();
            }
            else
            {
                if (descripcion)
                {
                    command = new SqlCommand("SELECT DISTINCT TAD_REFERENCIA AS CODIGO, LTRIM(TAD_DESCRIPCION) AS DESCRIPCION FROM tarifario_detalle WHERE TAD_DESCRIPCION LIKE '%' + @filtro + '%' ORDER BY LTRIM(TAD_DESCRIPCION) ASC", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    connection.Close();
                }
                else if (codigo)
                {
                    command = new SqlCommand("SELECT DISTINCT TAD_REFERENCIA AS CODIGO, LTRIM(TAD_DESCRIPCION) AS DESCRIPCION FROM tarifario_detalle WHERE TAD_REFERENCIA LIKE '%' + @filtro + '%' ORDER BY LTRIM(TAD_DESCRIPCION) ASC", connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@filtro", busqueda);
                    command.CommandTimeout = 180;
                    reader = command.ExecuteReader();
                    Tabla.Load(reader);
                    reader.Close();
                    command.Parameters.Clear();
                    connection.Close();
                }
            }

            return Tabla;
        }
        public bool GuardaHonorarioCuentaPaciente(string ateCodigo, double total, string medico, int usuario, DateTime hora)
        {

            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection con = null;

            SqlCommand cmd = null;
            bool response = false;
            try
            {
                con = obj.ConectarBd();
                cmd = new SqlCommand("sp_CreaHonorarioDesdeTaridario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ateCodigo", ateCodigo);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@idUsuario", usuario);
                cmd.Parameters.AddWithValue("@codMedico", medico);
                cmd.Parameters.AddWithValue("@hora", hora);
                con.Open();
                int filas = cmd.ExecuteNonQuery();
                if (filas > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                response = false;
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return response;
        }

        public DataTable ReporteTarifarioH(int hon_codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            DataTable Tabla = new DataTable();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_TarifarioHonorarioReporte", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@hon_codigo", hon_codigo);

            reader = command.ExecuteReader();
            Tabla.Load(reader);
            command.Parameters.Clear();
            reader.Close();
            connection.Close();
            return Tabla;

        }
    }
}
