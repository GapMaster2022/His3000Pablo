using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Entidades.General
{
    public class DtoPacientesAtencionesActivas
    {
        public Int16 codigoHabitacion { get; set; }
        public string numeroHabitacion {get;set;}
        //public string codigoPaciente { get; set; }
        public string cedula {get;set;}
        public string nombrePaciente {get;set;}
        public string historiaClincia {get;set;}
        public string numeroAtencion {get;set;}
        public int codAtencion { get; set; }
        public string sexo {get;set;}
        public string aseguradora {get;set;}
        public DateTime fechaIngreso {get;set;}
        //public string codigoMedico { get; set; }
        public string medicoTratante {get;set;}
        public string tipoTratamiento {get;set;}
        public string diagnosticoInicial {get;set;}        
        public int DiasHospitalizado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string FechaIngresoString { set; get; }
    }
}
