using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatTipoReferido
    {
        public List<TIPO_REFERIDO> listaTipoReferido()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from r in contexto.TIPO_REFERIDO
                        orderby r.TIR_DESCRIPCION
                        select r).ToList();
            }

        }
        public TIPO_REFERIDO RecuperarTipoReferido(int codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in db.TIPO_REFERIDO
                        where t.TIR_CODIGO == codigo
                        select t).FirstOrDefault();
            }
        }
    }
}
