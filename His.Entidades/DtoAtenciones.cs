using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoAtenciones
    {
        public string ATE_NUMERO_ATENCION { get; set; }
        public string ATE_NUMERO_CONTROL { get; set; }
        public string ATE_FACTURA_PACIENTE { get; set; }
        public Int32 ATE_CODIGO { get; set; }
        public Int32 PAC_CODIGO { get; set; }
        public Int32 DAP_CODIGO { get; set; }
        public Int32 CAJ_CODIGO { get; set; }
        public DateTime ATE_FECHA { get; set; }
        public DateTime ATE_FACTURA_FECHA { get; set; }
        public string PAC_NOMBRE { get; set; }
        public string PAC_NOMBRE2 { get; set; }
        public string PAC_APELLIDO_PATERNO { get; set; }
        public string PAC_APELLIDO_MATERNO { get; set; }
        public DateTime ATE_FECHA_INGRESO { get; set; }
        public DateTime ATE_FECHA_ALTA { get; set; }
        public string PAC_HCL { get; set; }
        public string PAC_DIRECCION { get; set; }
        public string PAC_TELEFONO { get; set; }
        public string PAC_CEDULA { get; set; }
        public Int16 HAB_CODIGO { get; set; }
        public string HAB_NUMERO { get; set; }
        public bool ATE_REFERIDO { get; set; }
        public bool ATE_ESTADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
        public Int16 TIP_CODIGO { get; set; }
        public Int16 TIA_CODIGO { get; set; }
        public string TIP_DESCRIPCION { get; set; }
        public string TIA_DESCRIPCION { get; set; }
        
            public string ATE_DIAGNOSTICOINICIAL { get; set; }
    }
}
