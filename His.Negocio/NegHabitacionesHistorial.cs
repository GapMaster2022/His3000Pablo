using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using System.Data; 

namespace His.Negocio
{
    public class NegHabitacionesHistorial
    {
        public static void CrearHabitacionHistorial(HABITACIONES_HISTORIAL habitacionHistorial)
        {
            new DatHabitacionHistorial().CrearHabitacionHistorial(habitacionHistorial);
        }
        public static int RecuperaMaximoHabitacionHistorial()
        {
            return new DatHabitacionHistorial().RecuperaMaximoHabitacionHistorial();
        }

        public static void HabHistorialArea(Int64 ate_codigo, Int64 hah_codigo, Int64 had_codigo)
        {
            new DatHabitacionHistorial().HabTipoIngreso(ate_codigo, hah_codigo, had_codigo);
        }

        public static void FechaAltaHistorial(Int64 ateCodigo)
        {
            new DatHabitacionHistorial().FechaAltaHistorial(ateCodigo);
        }
    }
}
