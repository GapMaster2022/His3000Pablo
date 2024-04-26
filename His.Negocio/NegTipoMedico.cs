using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegTipoMedico
    {
        public static Int16 RecuperaMaximoTipoMedico()
        {
            return new DatTipoMedico().RecuperaMaximoTipoMedico();
        }
        public static List<TIPO_MEDICO> RecuperaTipoMedicos()
        {
            return new DatTipoMedico().RecuperaTipoMedicos();
        }
        public static void CrearTipoMedico(TIPO_MEDICO tipomedico)
        {
            new DatTipoMedico().CrearTipoMedico(tipomedico);
        }
        public static void GrabarTipoMedico(TIPO_MEDICO tipomedicoModificada, TIPO_MEDICO tipomedicoOriginal)
        {
            new DatTipoMedico().GrabarTipoMedico(tipomedicoModificada, tipomedicoOriginal);
        }
        public static void EliminarTipoMedico(TIPO_MEDICO tipomedico)
        {
            new DatTipoMedico().EliminarTipoMedico(tipomedico);
        }
    }
}
