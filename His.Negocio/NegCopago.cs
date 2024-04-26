using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegCopago
    {
        public static COPAGO recuperaCopago(Int64 ate_codigo)
        {
            return new DatCopago().recuperaCopago(ate_codigo);
        }
        public static List<DtoReporteCopago> valoresCuentas(Int64 ate_codigo)
        {
            return new DatCopago().valoresCuentas(ate_codigo);
        }
        public static List<DtoReporteCopago> cuentaAuditoria(Int64 ate_codigo)
        {
            return new DatCopago().cuentaAuditoria(ate_codigo);
        }
        public static List<DtopAnulaCopago> cuentasCopago(DateTime desde, DateTime hasta)
        {
            return new DatCopago().cuentasCopago(desde, hasta);
        }
        public static bool revertirCopago(Int64 ate_codigo)
        {
            return new DatCopago().revertirCopago(ate_codigo);
        }
    }
}
