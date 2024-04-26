using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using System.Data;

namespace His.Negocio
{
    public class NegEspecialidades
    {
        public static List<ESPECIALIDADES_MEDICAS> ListaEspecialidades()
        {
            return new DatEspecialidades().ListaEspecialidades();
        }

        public static DataTable RecuperaMaximoMedico()
        {
            return new DatEspecialidades().RecuperaMaximoMedico();
        }

        public static Int16 RecuperaMaximoEspecialidad()
        {
            return new DatEspecialidades().RecuperaMaximoEspecialidad();
        }
        public static void CrearEspecialidad(ESPECIALIDADES_MEDICAS especialidad)
        {
            new DatEspecialidades().CrearEspecialidad(especialidad);
        }
        public static void GrabarEspecialidad(ESPECIALIDADES_MEDICAS especialidadModificada, ESPECIALIDADES_MEDICAS especialidadOriginal)
        {
            new DatEspecialidades().GrabarEspecialidad(especialidadModificada, especialidadOriginal);
        }
        public static void EliminarEspecialidad(ESPECIALIDADES_MEDICAS especialidad)
        {
            new DatEspecialidades().EliminarEspecialidad(especialidad);
        }

        public static string Especialidad(int med_codigo)
        {
            return new DatEspecialidades().EspecialidadMedica(med_codigo);
        }

    }


}
