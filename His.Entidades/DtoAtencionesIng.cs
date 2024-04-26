using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoAtencionesIng
    {
        public Int32 ATE_CODIGO { get; set; }
        public Int32 PAC_CODIGO { get; set; }
        public Int16 HAB_CODIGO { get; set; }
        public Int16 CAJ_CODIGO { get; set; }
        public Int16 TIA_CODIGO { get; set; }
        public Int16 ID_USUARIO { get; set; }
        public Int16 TIR_CODIGO { get; set; }
        public Int16 ASE_CODIGO { get; set; }
        public DateTime ATE_FECHA { get; set; }
        public bool ATE_REFERIDO { get; set; }
        public string ATE_FORMA_LLEGADA { get; set; }
        public string ATE_FUENTE_INFORMACION { get; set; }
        public string ATE_FACTURA_PACIENTE { get; set; }
        public string ATE_PERSONA_ENTREGA_PACIENTE { get; set; }
        public string ATE_NUMERO_CONTROL { get; set; }
        public DateTime ATE_FACTURA_FECHA { get; set; }
        public DateTime ATE_FECHA_INGRESO { get; set; }
        public DateTime ATE_FECHA_ALTA { get; set; }
        public string ATE_MEDICO_TRATANTE { get; set; }
        public string ATE_MEDICO_DIRECCION { get; set; }
        public string ATE_MEDICO_ATENCION { get; set; }
        public string ATE_DIAGNOSTICO_FINAL { get; set; }
        public bool ATE_ESTADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
