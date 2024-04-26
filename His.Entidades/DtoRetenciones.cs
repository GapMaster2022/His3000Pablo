using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoRetenciones
    {
        public string RET_CODIGO { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public Int16 CAJ_CODIGO { get; set; }
        public DateTime RET_FECHA { get; set; }
        public Int16 RET_RET_CODIGO { get; set; }
        public Int16 RET_EJERCICIO_FISCAL { get; set; }
        public string RET_SUJETO_RETENCION { get; set; }
        public string RET_RUC { get; set; }
        public string RET_DOCUMENTO_AFECTADO { get; set; }
        public string RET_DOCUMENTO_TIPO { get; set; }
        public decimal RET_BASE { get; set; }
        public decimal RET_PORCENTAJE { get; set; }
        public decimal RET_VALOR { get; set; }
        public bool RET_IMPRESA{ get; set; }
        public bool RET_ANULADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
