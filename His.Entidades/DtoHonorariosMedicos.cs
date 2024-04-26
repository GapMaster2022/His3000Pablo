using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoHonorariosMedicos
    {
        public int HOM_CODIGO { get; set; }
        public int ATE_CODIGO { get; set; }
        public int MED_CODIGO { get; set; }
        public int FOR_CODIGO { get; set; }
        public int CAJ_CODIGO { get; set; }
        public string FOR_DESCRIPCION { get; set; }
        public String MED_NOMBRE { get; set; }
        public string HOM_FACTURA_MEDICO { get; set; }
        public DateTime HOM_FACTURA_FECHA { get; set; }
        public decimal HOM_VALOR_NETO { get; set; }
        public decimal HOM_COMISION_CLINICA { get; set; }
        public DateTime HOM_FECHA_INGRESO { get; set; }
        public decimal HOM_APORTE_LLAMADA { get; set; }
        public decimal HOM_RETENCION { get; set; }
        public decimal HOM_VALOR_TOTAL { get; set; }
        public decimal HOM_VALOR_PAGADO { get; set; }
        public string HOM_LOTE { get; set; }
        public string HOM_OBSERVACION { get; set; }
        public Int16 HOM_ESTADO { get; set; }
        public string RET_CODIGO { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public decimal HOM_VALOR_CANCELADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
        public string HOM_VALE { get; set; }
        public bool HOM_FUERA { get; set; }
        public string FORMAPAGO { get; set; }
    }
}
