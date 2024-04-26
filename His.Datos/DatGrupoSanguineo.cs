using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatGrupoSanguineo
    {
        public List<GRUPO_SANGUINEO> ListaGrupoSanguineo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from g in contexto.GRUPO_SANGUINEO
                        select g).ToList();
            }
        }
        public GRUPO_SANGUINEO RecuperarGrupoSanguineoID(int codigoGrupo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from g in contexto.GRUPO_SANGUINEO
                        where g.GS_CODIGO==codigoGrupo
                        select g).FirstOrDefault();
            }
        }
        public string RecupararGS(Int64 pac_codigo)
        {
            string gs = "";
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
            command.CommandText = "select g.GS_NOMBRE from PACIENTES p, GRUPO_SANGUINEO g where p.GS_CODIGO = g.GS_CODIGO and PAC_CODIGO =" + pac_codigo ;
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                gs = reader["GS_NOMBRE"].ToString();
            }
            conexion.Close();
            return gs;
        }
    }
}
