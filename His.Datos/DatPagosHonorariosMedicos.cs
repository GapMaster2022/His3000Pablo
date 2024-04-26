using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;
//using Core.Entidades;

namespace His.Datos
{
    public class DatPagosHonorariosMedicos
    {
        /// <summary>
        /// Devuelve una instancia de CANCELACION_FAC_MEDICOS
        /// </summary>
        /// <param name="codigo">codigo de la cancelacion del honorario medico</param>
        /// <returns>Instancia de CANCELACION_FAC_MEDICOS</returns>
        public CANCELACION_FAC_MEDICOS RecuperaCancelacionHonorarioMedicoID(Int64 codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CANCELACION_FAC_MEDICOS.FirstOrDefault(p => p.CFM_CODIGO == codigo);
            }
        }
        /// <summary>
        /// Devuelve una instancia de CANCELACION_FAC_MEDICOS_DETALLE
        /// </summary>
        /// <param name="codigo"> codigo del detalle cancelacion del honorario medico</param>
        /// <returns>Instancia de CANCELACION_FAC_MEDICOS_DETALLE</returns>
        public CANCELACION_FAC_MEDICOS_DETALLE RecuperaCancelacionHonorarioMedicoDetalleID(Int64 codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CANCELACION_FAC_MEDICOS_DETALLE.FirstOrDefault(p => p.CFD_CODIGO == codigo);
            }
        }
        /// <summary>
        /// Recupera la lista con los cancelaciones a los honorarios  medicos
        /// </summary>
        /// <returns>lista con los cancelaciones a los honorarios  medicos</returns>
        public List<CANCELACION_FAC_MEDICOS_VIEW> RecuperarCancelacionHonorarioMedicos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CANCELACION_FAC_MEDICOS_VIEW.ToList();
            }
        }
        public DataTable PagosHonorarios()
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
            Sqlcmd = new SqlCommand("SP_HONORARIOS_MEDICOS", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

          
            Sqlcmd.Parameters.Add("@consulta", SqlDbType.Int);
            Sqlcmd.Parameters["@consulta"].Value = 1;



            Sqldap = new SqlDataAdapter();
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");
            return Dts.Tables["tabla"];

        }
        /// <summary>
        /// Recupera el Detalle de un cancelacion de honorario especifica
        /// </summary>
        /// <param name="codigoPagoHonorario">codigo de la cancelacion del honorario medico</param>
        /// <returns>lista de CANCELACION_FAC_MEDICOS_DETALLE de un determinado pago </returns>
        public List<CANCELACION_FAC_MEDICOS_DETALLE> RecuperarCancelacionHonorarioMedicosDetalle(Int64 codigoPagoHonorario)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.CANCELACION_FAC_MEDICOS_DETALLE.Where(p => p.CANCELACION_FAC_MEDICOS.CFM_CODIGO == codigoPagoHonorario).ToList();
            }
        }
        /// <summary>
        /// Crear un nueva cancelacion de honorarios medicos
        /// </summary>
        /// <param name="cancelacionHonorarioMedico">cancelacion honorario medico</param>
        public CANCELACION_FAC_MEDICOS CrearCancelacionHonorariosMedicos(CANCELACION_FAC_MEDICOS cancelacionHonorarioMedico)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToCANCELACION_FAC_MEDICOS(cancelacionHonorarioMedico);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException.Message);
                throw ex;
            }
            return cancelacionHonorarioMedico;
        }
        /// <summary>
        /// Crear un nuevo detalle de la cancelacion de honorarios
        /// </summary>
        /// <param name="cancelacionHonorariosMedicosDetalle">detalle cancelacion de honorarios medicos</param>
        public void CrearCancelacionHonorariosMedicosDetalle(CANCELACION_FAC_MEDICOS_DETALLE cancelacionHonorariosMedicosDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("CANCELACION_FAC_MEDICOS_DETALLE", cancelacionHonorariosMedicosDetalle);
            }
        }
        /// <summary>
        /// Recupera la lista de tipos de cancelacion
        /// </summary>
        /// <returns>retorna una lista de TIPO_CANCELACION </returns>
        public List<TIPO_CANCELACION> RecuperaTiposDeCancelacion()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.TIPO_CANCELACION.Where(c => c.TCA_ESTADO == true).ToList();  
            }
        }
        /// <summary>
        /// Retorna un nuevo numero de transferencia
        /// </summary>
        /// <returns>nuevo numero de transferencia CFM_NUMERO_DOCUMENTO</returns>
        public Int64 RecuperarMaxNumeroTransferencia()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var numMaximo = (from c in contexto.CANCELACION_FAC_MEDICOS 
                                where c.TIPO_CANCELACION.TCA_CODIGO ==1 
                                select c.CFM_NUMERO_DOCUMENTO).Max();
                Int64 maximo = 1 + Convert.ToInt64(numMaximo);
                return maximo;
            }
        }
 
    }
}
