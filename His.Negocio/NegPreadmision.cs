using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegPreadmision
    {
        public static bool crearPreadmision(PREADMISION cabecera, List<PREADMISION_DETALLE> detalle)
        {
            return new DtoPreadmision().crearPreadmision(cabecera, detalle);
        }
        public static PREADMISION recuperarPreadmision(string codigo)
        {
            return new DtoPreadmision().recuperaPreAdmision(codigo);
        }
        public static PREADMISION recuperaPreAdmisionAte(Int64 codigo)
        {
            return new DtoPreadmision().recuperaPreAdmisionAte(codigo);
        }
        public static List<PREADMISION_DETALLE> recuperarPreAdmisionDetalle(Int64 codigo)
        {
            return new DtoPreadmision().recuperaPreAdmisionDetalle(codigo);
        }
        public static List<DtoPreAdmision> listarPreadmision()
        {
            return new DtoPreadmision().listarPreadmision();
        }
        public static void EliminarPreadmision(string codigo)
        {
            new DtoPreadmision().EliminarPreadmision(Convert.ToInt16(codigo));
        }
        public static bool cambioEstadoPreadmsion(Int64 codigo)
        {
            return new DtoPreadmision().modificacionEstadoPreadmision(codigo);
        }
        public static List<DtoPreAtencion> consultaPreatencion(DateTime desde, DateTime hasta, bool hc, string Phc)
        {
            return new DtoPreadmision().consultaPreatencion(desde, hasta,hc,Phc);
        }
    }
}
