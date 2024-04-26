using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatEvolucion
    {
        public void crearEvolucion(HC_EVOLUCION nuevaEvolucion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EVOLUCION(nuevaEvolucion);
                contexto.SaveChanges();
            }
            

        }

        public HC_EVOLUCION recuperarEvolucionPorAtencion(Int64 codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from e in contexto.HC_EVOLUCION
                        join a in contexto.ATENCIONES on e.ATENCIONES.ATE_CODIGO equals a.ATE_CODIGO
                        where e.ATENCIONES.ATE_CODIGO == codAtencion
                        select e).FirstOrDefault();
            }
        }        

        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var listaCodigos = (from e in contexto.HC_EVOLUCION
                                    select e.EVO_CODIGO).ToList();

                if (listaCodigos.Count > 0)
                    return listaCodigos.Max();

                return 0;
            }
        }

        public DataTable VerificaDepartamento (int usuario)
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
            Sqlcmd = new SqlCommand("sp_VerificaDepartamento", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@Usiario", SqlDbType.Int);
            Sqlcmd.Parameters["@Usiario"].Value = usuario;

            Sqldap = new SqlDataAdapter();
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

        public DataTable Dietetica(string historiaClinica)
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
            Sqlcmd = new SqlCommand("sp_RecuperaDietetica", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@historiaClinica", SqlDbType.VarChar);
            Sqlcmd.Parameters["@historiaClinica"].Value = historiaClinica;

            Sqldap = new SqlDataAdapter();
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


    }
}
