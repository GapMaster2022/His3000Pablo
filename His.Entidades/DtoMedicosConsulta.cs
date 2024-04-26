using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoMedicosConsulta
    {
        public int MED_CODIGO { get; set; }
        public string MED_NOMBRE { get; set; }
        public string MED_RUC { get; set; }
        public string ESP_NOMBRE { get; set; }
        public string MED_DIRECCION { get; set; }
        public string MED_DIRECCION_CONSULTORIO { get; set; }
        public string MED_TELEFONO_CASA { get; set; }
        public string MED_TELEFONO_CONSULTORIO { get; set; }
        public string MED_TELEFONO_CELULAR { get; set; }
        public string MED_EMAIL { get; set; }
        public bool MED_ESTADO { get; set; }
        public string ENTITYSETNAME { get; set; }
        public string ENTITYID { get; set; }
    }
}
