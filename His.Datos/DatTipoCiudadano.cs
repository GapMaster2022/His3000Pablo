using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatTipoCiudadano
    {
        public TIPO_CIUDADANO RecuperarTipoCiudadanoID(int codigoTipoCiudadano)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_CIUDADANO
                        where t.TC_CODIGO == codigoTipoCiudadano
                        select t).FirstOrDefault();
            }
        }

        public List<TIPO_CIUDADANO> listaTiposCiudadano()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_CIUDADANO
                        orderby t.TC_DESCRIPCION
                                  select t).ToList();
            }
        }
    }
}
