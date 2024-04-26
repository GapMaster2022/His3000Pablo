using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Entidades
{
    public class DtoPacienteDatosAdicionales
    {
        public int CODIGO { get; set; }
        public string PAIS { get; set; }
        public string PROVINCIA { get; set; }
        public string CANTON { get; set; }
        public string PARROQUIA { get; set; }
        public string SECTOR { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO_1 { get; set; }
        public string TELEFONO_2 { get; set; }
        public string OCUPACION { get; set; }
        public string INSTRUCCION { get; set; }
        public string NOMBRE_EMPRESA { get; set; }
        //public string DIRECCION_EMPRESA { get; set; }
        //public string CIUDAD_EMPRESA { get; set; }
        //public string TELEFONO_EMPRESA { get; set; }
        public string ESTADO_CIVIL { get; set; }
        public string TIPO_CIUDADANO { get; set; }

    }
}
