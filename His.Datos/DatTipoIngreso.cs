using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatTipoIngreso
    {
        public List<TIPO_INGRESO> ListaTipoIngreso()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_INGRESO
                        orderby t.TIP_DESCRIPCION
                        select t).ToList();
            }
        }

        public List<TIPO_INGRESO> ListaTipoIngresoNombre(String Filtro)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_INGRESO
                        where t.TIP_DESCRIPCION.Contains(Filtro) 
                        orderby t.TIP_CODIGO
                        select t).ToList();
            }
        }

        public TIPO_INGRESO TipoPorId(Int16 codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from h in contexto.TIPO_INGRESO
                        where h.TIP_CODIGO == codigo
                        select h).FirstOrDefault();
            }
        }
        public int RecuperarPorAtencion(Int64 ate_Codigo)
        {
            int tip_codigo = 0;
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("Select TIP_CODIGO from ATENCIONES where ATE_CODIGO = @ate_codigo", connection);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.AddWithValue("@ate_codigo", ate_Codigo);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                tip_codigo = Convert.ToInt32(reader["TIP_CODIGO"].ToString());
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return tip_codigo;
        }
    }
}
