using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatTipoGarantias
    {
        public List<TIPO_GARANTIA> listaTipoGarantia()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_GARANTIA
                        orderby t.TG_NOMBRE
                        select t).ToList(); 
            }
        }
    }
}
