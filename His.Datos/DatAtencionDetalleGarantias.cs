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
    public class DatAtencionDetalleGarantias
    {
        public void Crear(ATENCION_DETALLE_GARANTIAS nuevoDetalleGarantia)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("ATENCION_DETALLE_GARANTIAS", nuevoDetalleGarantia);
            }
        }

        public int ultimoCodigoDetalleGarantias()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                int maxim = 0;

                if (contexto.ATENCION_DETALLE_GARANTIAS.ToList().Count > 0)
                    maxim = contexto.ATENCION_DETALLE_GARANTIAS.Max(emp => emp.ADG_CODIGO);

                return maxim;

            }
        }

        public List<ATENCION_DETALLE_GARANTIAS> RecuperarDetalleGarantiasAtencion(int codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ATENCION_DETALLE_GARANTIAS> det= (from d in contexto.ATENCION_DETALLE_GARANTIAS
                        join a in contexto.ATENCIONES on d.ATENCIONES.ATE_CODIGO equals a.ATE_CODIGO
                        join t in contexto.TIPO_GARANTIA on d.TIPO_GARANTIA.TG_CODIGO equals t.TG_CODIGO
                        where d.ATENCIONES.ATE_CODIGO == codAtencion
                        select d).ToList();

                if (det.Count > 0)
                    return det;
                else
                    return null;
            }
        }

        public void eliminarDetalleGarantias(int codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ATENCION_DETALLE_GARANTIAS> detalle = (from d in contexto.ATENCION_DETALLE_GARANTIAS
                                                            join a in contexto.ATENCIONES on d.ATENCIONES.ATE_CODIGO equals a.ATE_CODIGO
                                                            join g in contexto.TIPO_GARANTIA on d.TIPO_GARANTIA.TG_CODIGO equals g.TG_CODIGO
                                                            where d.ATENCIONES.ATE_CODIGO == codAtencion
                                                            select d).ToList();

                foreach (ATENCION_DETALLE_GARANTIAS acceso in detalle)
                {
                    contexto.Eliminar(acceso);
                }
 
            }
        }

        public ATENCION_DETALLE_GARANTIAS RecuperarDetalleGarantiasID(int codDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from g in contexto.ATENCION_DETALLE_GARANTIAS
                        where g.ADG_CODIGO == codDetalle
                        select g).FirstOrDefault();
            }
        }

        public void editarDetalleGarantia(ATENCION_DETALLE_GARANTIAS detalleModif)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCION_DETALLE_GARANTIAS detalleOrigin = contexto.ATENCION_DETALLE_GARANTIAS.FirstOrDefault(g=>g.ADG_CODIGO==detalleModif.ADG_CODIGO);
                detalleOrigin.ADG_DESCRIPCION = detalleModif.ADG_DESCRIPCION;
                detalleOrigin.ADG_DOCUMENTO = detalleModif.ADG_DOCUMENTO;
                detalleOrigin.ADG_VALOR = detalleModif.ADG_VALOR;
                contexto.SaveChanges();
            }
        }

        //RECUPERA INFORMACION GARANTIAS PABLO ROCHA 05/09/2014
        public DataTable SPRecuperaGarantia(int codAtencion)
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

            Sqlcmd = new SqlCommand("sp_RecuperaGarantia", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@codAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@codAtencion"].Value = (codAtencion);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }

        //RECUPERA INFORMACION GARANTIAS CON FILTRO PABLO ROCHA 05/09/2014
        public DataTable SPRecuperaGarantiaFecha(string fechaInicio, string fechaFin)
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

            Sqlcmd = new SqlCommand("sp_RecuperaGarantiaconFechas", Sqlcon);

            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@fechaInicio", SqlDbType.VarChar);
            Sqlcmd.Parameters["@fechaInicio"].Value = (fechaInicio);

            Sqlcmd.Parameters.Add("@fechaFin", SqlDbType.VarChar);
            Sqlcmd.Parameters["@fechaFin"].Value=(fechaFin);

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }
    }
}
