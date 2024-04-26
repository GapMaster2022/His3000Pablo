using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoCuentasPacientes
    {
        public int CODIGO { get; set; }
        public Int64 PED_CODIGO { get; set; }
        public Int16 PEA_CODIGO { get; set; }
        public Int16 PEE_CODIGO { get; set; }
        public string ESTACION { get; set; }
        public string PED_DESCRIPCION { get; set; }
        public Int16 PED_ESTADO { get; set; }
        public string ESTADO { get; set; }
        public DateTime PED_FECHA { get; set; }
        public DateTime PED_FECHA_ALTA { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string HISTORIA_CLINICA { get; set; }
        public int ATE_CODIGO { get; set; }
        public string PACIENTE { get; set; }
        public string IDENTIFICACION { get; set; }
        public Int16 TIP_PEDIDO { get; set; }
        public string TIPO { get; set; }
        public Int64 PDD_CODIGO { get; set; }
        public int PRO_CODIGO { get; set; }
        public string PRO_DESCRIPCION { get; set; }
        public int PDD_CANTIDAD { get; set; }
        public Decimal PDD_VALOR { get; set; }
        public Decimal PDD_IVA { get; set; }
        public Decimal PDD_TOTAL { get; set; }
        public bool PDD_ESTADO { get; set; }
        public string HABITACION { get; set; }
        public string FACTURA { get; set; }
        public string NUMCONTROL { get; set; }
        public DateTime FECHAFACTURA { get; set; }
        public bool REFERIDO { get; set; }
        public int ESC_CODIGO { get; set; }
    }
}
