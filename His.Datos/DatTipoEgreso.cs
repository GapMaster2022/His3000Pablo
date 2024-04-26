using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatTipoEgreso
    {
        public List<TIPO_EGRESO> ListaTipoIngreso()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_EGRESO
                        select t).ToList();
            }
        }

        public TIPO_EGRESO TipoPorId(Int32 codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from h in contexto.TIPO_EGRESO
                        where h.TIE_CODIGO == codigo
                        select h).FirstOrDefault();
            }
        }
    }
}
