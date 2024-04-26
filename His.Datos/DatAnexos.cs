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
    public class DatAnexos
    {
        public ANEXOS_IESS RecuperarAnexos(int codidoAnexo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.ANEXOS_IESS.Where(a => a.ANI_CODIGO == codidoAnexo).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public string RecuperarAnexos1(int codidoAnexo)
        {

            // PABLO ROCHA 18/09/2013

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("sp_RecuperaTipo", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoTipo", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoTipo"].Value = codidoAnexo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Convert.ToString(Dts.Rows[0][0]);
            }
            else
            {
                return "";
            }


        }


        public List<ANEXOS_IESS> RecuperarListaAnexos(string codidoAnexo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var lista = (from a in contexto.ANEXOS_IESS
                                 where a.ANI_COD_PADRE == codidoAnexo
                                 select a).ToList();
                    return lista;
                    //ulgdbListadoCatSub.DataSource = lista;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
