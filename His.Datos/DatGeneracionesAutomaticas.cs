using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data.SqlClient;
using System.Data;


namespace His.Datos
{
    public class DatGeneracionesAutomaticas
    {
        public int RecuperaMaximo()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<GENERACIONES_AUTOMATICAS> generacionesa = contexto.GENERACIONES_AUTOMATICAS.ToList();
                if (generacionesa.Count > 0)
                    maxim = contexto.GENERACIONES_AUTOMATICAS.Max(emp => emp.GEN_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }

        }

        public void EliminaTipoDocumento(Int32 CodigoDocumento)
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

            Sqlcmd = new SqlCommand("sp_EliminaTipoDocumento", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoTipoDocumento", SqlDbType.BigInt);
            Sqlcmd.Parameters["@CodigoTipoDocumento"].Value = Convert.ToInt64(CodigoDocumento);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;//ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
        
        }

        public List<GENERACIONES_AUTOMATICAS> ListaGeneracionesAutomaticas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.GENERACIONES_AUTOMATICAS.Include("TIPO_DOCUMENTO").ToList();
            }
        }
        public void CrearGeneracionAutomatica(GENERACIONES_AUTOMATICAS generaAutom)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("GENERACIONES_AUTOMATICAS", generaAutom);
            }
        }
    }
}
