using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoFormaPago
    {
        public Int16 FOR_CODIGO { get; set; }
        public Int16 TIF_CODIGO { get; set; }
        public string TIF_NOMBRE { get; set; }
        public string FOR_DESCRIPCION { get; set; }
        public string FOR_CUENTA_CONTABLE { get; set; }
        public decimal FOR_COMISION { get; set; }
        public decimal FOR_REFERIDO { get; set; }
        public bool FOR_ACTIVO { get; set; }
        public bool FOR_ESTADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
