using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegGrupoSanguineo
    {
        DatGrupoSanguineo Gs = new DatGrupoSanguineo();
        public static List<GRUPO_SANGUINEO> ListaGrupoSanguineo()
        {
            return new DatGrupoSanguineo().ListaGrupoSanguineo();
        }
        public static GRUPO_SANGUINEO RecuperarGrupoSanguineoID(int codigoGrupo)
        {
            return new DatGrupoSanguineo().RecuperarGrupoSanguineoID(codigoGrupo);
        }
        public string RecuperarGS(Int64 pac_codigo)
        {
            string gs = Gs.RecupararGS(pac_codigo);
            return gs;
        }
    }
}
