using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegTipoHonorario
    {
        public static Int16 RecuperaMaximoTipoHonorario()
        {
            return new DatTipoHonorario().RecuperaMaximoTipoHonorario();
        }
        public static List<TIPO_HONORARIO> RecuperaTipoHonorarios()
        {
            return new DatTipoHonorario().RecuperaTipoHonorarios();
        }
        public static void CrearTipoHonorario(TIPO_HONORARIO tipohonorario)
        {
            new DatTipoHonorario().CrearTipoHonorario(tipohonorario);
        }
        public static void GrabarTipoHonorario(TIPO_HONORARIO tipohonorarioModificada, TIPO_HONORARIO tipohonorarioOriginal)
        {
            new DatTipoHonorario().GrabarTipoHonorario(tipohonorarioModificada, tipohonorarioOriginal);
        }
        public static void EliminarTipoHonorario(TIPO_HONORARIO tipohonorario)
        {
            new DatTipoHonorario().EliminarTipoHonorario(tipohonorario);
        }
    }
}
