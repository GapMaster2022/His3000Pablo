using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades.Pedidos
{
    public class DtoPedidos
    {
        public int CODIGO { get; set; }
        public Int64 PED_CODIGO { get; set; }
        public Int64 PEA_CODIGO { get; set; }
        public Int64 PEE_CODIGO { get; set; }
        public string ESTACION { get; set; }
        public string PED_DESCRIPCION { get; set; }
        public Int64 PED_ESTADO { get; set; }
        public string ESTADO { get; set; }
        public DateTime PED_FECHA { get; set; }
        public DateTime PED_FECHA_ALTA { get; set; }
        public Int64 ID_USUARIO { get; set; }
        public string USUARIO { get; set; }
        public string HISTORIA_CLINICA { get; set; }
        public string HABITACION { get; set; }
        public Int64 ATE_CODIGO { get; set; }
        public string PACIENTE { get; set; }
        public string IDENTIFICACION { get; set; }
        public Int64 TIP_PEDIDO { get; set; }
        public string TIPO { get; set; }
        public Int64 PDD_CODIGO { get; set; }
        public Int64 PRO_CODIGO { get; set; }
        public string PRO_DESCRIPCION { get; set; }
        public Decimal PDD_CANTIDAD { get; set; }
        public Decimal PDD_VALOR { get; set; }
        public Decimal PDD_IVA { get; set; }
        public Decimal PDD_TOTAL { get; set; }
        public bool PDD_ESTADO { get; set; }
        public string ATE_NUMERO { get; set; } // Aumento el numero de atencion para ser utilizado en vez del codigo de atencion /30/10/2012 / GIOVANNY TAPIA
        public string MedicoDatos { get; set; } //  Aumento los datos del medico /18/10/2012 / GIOVANNY TAPIA
        public int ESC_CODIGO { get; set; } // Aumento el estado de la cuenta / Giovanny Tapia / 22/05/2013
        public string FACTURA { get; set; }
        public string TipoAtencion { get; set; }
        public string PED_FECHA2 { get; set; }
        public string MEDICO { get; set; }
        public string fechaPablo { get; set; }

        public string ESC_DESCRIPCION { get; set; } //se agrega la descripcion del estado de cuenta

        public string CANT_DEV { get; set; }

        public DateTime ATE_FECHA_FACTURA { get; set; }

    }
}
