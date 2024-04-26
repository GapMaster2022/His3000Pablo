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
    public class DatAtencionDetalleCategorias
    {
        public void Crear(ATENCION_DETALLE_CATEGORIAS nuevoDetalleAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.Crear("ATENCION_DETALLE_CATEGORIAS", nuevoDetalleAtencion);
                    //contexto.dispose();
                }

            }
            catch (Exception err) { throw err; }
        }
       

        public int ultimoCodigoDetalleCategorias()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                int maxim = 0;

                if (contexto.ATENCION_DETALLE_CATEGORIAS.ToList().Count > 0)
                    maxim = (int)contexto.ATENCION_DETALLE_CATEGORIAS.Max(emp => emp.ADA_CODIGO);

                return maxim;

            }
        }

        public int ultimoCodigoDetalleCategorias_sp()
        {

            // GIOVANNY TAPIA / 07/08/2012

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
            Sqlcmd = new SqlCommand("sp_SecuencialCategoriaConvenios", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Convert.ToInt32(Dts.Rows[0][0]);
            }
            else
            {
                return 0;
            }


        }

        public List<ATENCION_DETALLE_CATEGORIAS> RecuperarDetalleCategoriasAtencion(int codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ATENCION_DETALLE_CATEGORIAS> det = (from d in contexto.ATENCION_DETALLE_CATEGORIAS
                                                         join a in contexto.ATENCIONES on d.ATENCIONES.ATE_CODIGO equals a.ATE_CODIGO
                                                         join c in contexto.CATEGORIAS_CONVENIOS on d.CATEGORIAS_CONVENIOS.CAT_CODIGO equals c.CAT_CODIGO
                                                         where d.ATENCIONES.ATE_CODIGO == codAtencion
                                                         select d).ToList();
                if (det.Count > 0)
                    return det;
                else
                    return null;
            }
        }
       

        public void eliminarDetalleCategorias(int codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ATENCION_DETALLE_CATEGORIAS> detalle = (from d in contexto.ATENCION_DETALLE_CATEGORIAS
                                                            join a in contexto.ATENCIONES on d.ATENCIONES.ATE_CODIGO equals a.ATE_CODIGO
                                                            join g in contexto.CATEGORIAS_CONVENIOS on d.CATEGORIAS_CONVENIOS.CAT_CODIGO equals g.CAT_CODIGO
                                                            where d.ATENCIONES.ATE_CODIGO == codAtencion
                                                            select d).ToList();

                foreach (ATENCION_DETALLE_CATEGORIAS acceso in detalle)
                {
                    contexto.Eliminar(acceso);
                }

            }
        }
        /// <summary>
        /// Metodo que elimina una ATENCION_DETALLE_CATEGORIAS
        /// </summary>
        /// <param name="codDetalleCategoria">codigo del detalle categoria</param>
        public bool eliminarAtencionDetalleCategorias(Int64 codDetalleCategoria)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCION_DETALLE_CATEGORIAS acceso = contexto.ATENCION_DETALLE_CATEGORIAS.FirstOrDefault(a => a.ADA_CODIGO == codDetalleCategoria);
                    contexto.Eliminar(acceso);
                    return true;
                }
            }
            catch (Exception err) { throw err; }
        }
        public ATENCION_DETALLE_CATEGORIAS RecuperarDetalleCategoriasID(int codDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from c in contexto.ATENCION_DETALLE_CATEGORIAS
                        where c.ADA_CODIGO == codDetalle
                        select c).FirstOrDefault();
            }
        }
        public void editarDetalleCategoria(ATENCION_DETALLE_CATEGORIAS detalleModif)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCION_DETALLE_CATEGORIAS detalleOrigin = contexto.ATENCION_DETALLE_CATEGORIAS.FirstOrDefault(c=>c.ADA_CODIGO == detalleModif.ADA_CODIGO);
                detalleOrigin.ADA_AUTORIZACION = detalleModif.ADA_AUTORIZACION;
                detalleOrigin.ADA_CONTRATO = detalleModif.ADA_CONTRATO;
                detalleOrigin.ADA_MONTO_COBERTURA = detalleModif.ADA_MONTO_COBERTURA;
                detalleOrigin.HCC_CODIGO_TS = detalleModif.HCC_CODIGO_TS;
                detalleOrigin.HCC_CODIGO_DE = detalleModif.HCC_CODIGO_DE;
                detalleOrigin.HCC_CODIGO_ES = detalleModif.HCC_CODIGO_ES;
                contexto.SaveChanges();
            }
        }

        public int EliminaAtencion_Detalle_Categorias(Int64 ate_codigo)
        {

            // PABLO ROCHA 21/07/2014

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
            Sqlcmd = new SqlCommand("sp_EliminaAtencion_Detalle_Categorias", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@Ate_codigo", SqlDbType.BigInt);
            Sqlcmd.Parameters["@Ate_codigo"].Value = ate_codigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
