using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegPagosHonorariosMedicos
    {
        public static List<CANCELACION_FAC_MEDICOS_VIEW> RecuperarCancelacionHonorarioMedicos()
        {
            return new DatPagosHonorariosMedicos().RecuperarCancelacionHonorarioMedicos();
        }

        public static CANCELACION_FAC_MEDICOS CrearCancelacionHonorariosMedicos(CANCELACION_FAC_MEDICOS cancelacion)
        {
            return new DatPagosHonorariosMedicos().CrearCancelacionHonorariosMedicos(cancelacion);
        }
        public static DataTable PagosHonorarios()
        {
            return new DatPagosHonorariosMedicos().PagosHonorarios();
        }

        public static void CrearCancelacionHonorariosMedicosDetalle(CANCELACION_FAC_MEDICOS_DETALLE detalle)
        {
            new DatPagosHonorariosMedicos().CrearCancelacionHonorariosMedicosDetalle(detalle);
        }

        public static CANCELACION_FAC_MEDICOS RecuperaCancelacionHonorarioMedicoID(Int64 codigo)
        {
            return new DatPagosHonorariosMedicos().RecuperaCancelacionHonorarioMedicoID(codigo);
        }

        public static List<CANCELACION_FAC_MEDICOS_DETALLE> RecuperarCancelacionHonorarioMedicosDetalle(Int64 codigoCancelacion)
        {
            return new DatPagosHonorariosMedicos().RecuperarCancelacionHonorarioMedicosDetalle(codigoCancelacion);
        }

        public static CANCELACION_FAC_MEDICOS_DETALLE RecuperaCancelacionHonorarioMedicoDetalleID(Int64 codigo)
        {
            return new DatPagosHonorariosMedicos().RecuperaCancelacionHonorarioMedicoDetalleID(codigo);
        }
        /// <summary>
        /// Recupera la lista de tipos de cancelacion
        /// </summary>
        /// <returns>retorna una lista de TIPO_CANCELACION </returns>
        public static List<TIPO_CANCELACION> RecuperaTiposDeCancelacion()
        {
            return new DatPagosHonorariosMedicos().RecuperaTiposDeCancelacion(); 
        }
        /// <summary>
        /// Retorna un nuevo numero de transferencia
        /// </summary>
        /// <returns>nuevo numero de transferencia CFM_NUMERO_DOCUMENTO</returns>
        public static Int64 RecuperarMaxNumeroTransferencia()
        {
             return new DatPagosHonorariosMedicos().RecuperarMaxNumeroTransferencia();
        }
    }
}
