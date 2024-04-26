using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoNotaCreditoDebito
    {
        public string NOT_NUMERO { get; set; }
        public Int16 CAJ_CODIGO { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public Int16 TID_CODIGO { get; set; }
        public DateTime NOT_FECHA { get; set; }
        public int HOM_CODIGO1 { get; set; }
        public Int16 FOR_CODIGO1 { get; set; }
        public int MED_CODIGO1 { get; set; }
        public Int16 TNO_CODIGO { get; set; }
        public string NOT_RAZON_SOCIAL { get; set; }
        public string NOT_RUC { get; set; }
        public string NOT_DOCUMENTO_AFECTADO { get; set; }
        public string NOT_DOCUMENTO_TIPO { get; set; }
        public string NOT_MOTIVO_MODIFICACION { get; set; }
        public decimal NOT_VALOR { get; set; }
        public decimal NOT_IVA { get; set; }
        public bool NOT_CANCELADO { get; set; }
        public bool NOT_ANULADO { get; set; }
        public string NOT_CUENTA_CONTABLE { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }

    }
}
