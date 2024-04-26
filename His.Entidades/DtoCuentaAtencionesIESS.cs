using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades
{
    public class DtoCuentaAtencionesIESS
    {
        public int PAC_CODIGO;
        public string PAC_HISTORIA_CLINICA;
        public string PAC_APELLIDOS;
        public string PAC_NOMBRES;
        public string PAC_IDENTIFICACION;
        public string PAC_GENERO;
        public DateTime PAC_FECHA_NACIMIENTO;

        public int ATE_CODIGO;
        public DateTime ATE_FECHA_INGRESO;
        public int ATE_EDAD_PACIENTE ;

        public int CAT_CODIGO;
        public int CAT_CAT_NOMBRE;

        public string ADA_AUTORIZACION;
        public string HCC_CODIGO_TS;
        public string HCC_CODIGO_DE;

        public int ADS_CODIGO;
        public int ADA_CODIGO;
        public string ADS_ASEGURADO_NOMBRE;
        public int ADS_ASEGURADO_CEDULA;
        public string ADS_ASEGURADO_PARENTESCO;
        public int ADA_PAC_EDAD;
        public string ADA_CONTINGENCIA;
        public string ADA_TIPO_DIAGNOSTICO;

        public int ASD_CODIGO;
        public string CIE_CODIGO;
        
    }
}
