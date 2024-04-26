using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatEspecialidades
    {
        public List<ESPECIALIDADES_MEDICAS> ListaEspecialidades()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ESPECIALIDADES_MEDICAS.OrderBy(x => x.ESP_NOMBRE) .ToList();
            }
        }

        //RECUPERA EL CODIGO MAXIMO DEL MEDICO PABLO ROCHA 08-03-2019
        public DataTable RecuperaMaximoMedico()
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

            Sqlcmd = new SqlCommand("sp_RecuperaMaximoMedico", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;
            
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Dts.Tables["tabla"];

        }

        public Int16 RecuperaMaximoEspecialidad()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ESPECIALIDADES_MEDICAS> especialidades = contexto.ESPECIALIDADES_MEDICAS.ToList();
                if (especialidades.Count > 0)
                    maxim = contexto.ESPECIALIDADES_MEDICAS.Max(emp => emp.ESP_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }

        public void CrearEspecialidad(ESPECIALIDADES_MEDICAS especialidad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("ESPECIALIDADES_MEDICAS", especialidad);

            }
        }
        public void GrabarEspecialidad(ESPECIALIDADES_MEDICAS especialidadModificada, ESPECIALIDADES_MEDICAS especialidadOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(especialidadModificada, especialidadOriginal);
            }
        }
        public void EliminarEspecialidad(ESPECIALIDADES_MEDICAS especialidad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(especialidad);
            }
        }
        public string EspecialidadMedica(int med_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            connection = obj.ConectarBd();
            connection.Open();
            SqlDataReader reader;
            string especialidad = "";
            command = new SqlCommand("select e.ESP_NOMBRE from ESPECIALIDADES_MEDICAS e inner join MEDICOS m on e.ESP_CODIGO = m.ESP_CODIGO where m.MED_CODIGO = @med_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@med_codigo", med_codigo);

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                especialidad = reader["ESP_NOMBRE"].ToString();
            }
            command.Parameters.Clear();
            connection.Close();
            reader.Close();
            return especialidad;
        }
    }
    
}

