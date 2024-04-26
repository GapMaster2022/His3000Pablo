using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatTipoTratamiento
    {
        public List<TIPO_TRATAMIENTO> RecuperaTipoTratamiento()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista =(from t in contexto.TIPO_TRATAMIENTO
                        where t.TIA_CODIGO > 0
                        orderby t.TIA_DESCRIPCION
                        select t).ToList();
                //TIPO_TRATAMIENTO x = new TIPO_TRATAMIENTO();
                //x.TIA_CODIGO = 5;
                //x.TIA_DESCRIPCION = "UCI";
                //lista.Add(x);
                return lista;
                //return (from t in contexto.TIPO_TRATAMIENTO
                //        orderby t.TIA_DESCRIPCION
                //        select t).ToList();
            }
        }

        public TIPO_TRATAMIENTO recuperaTipoTratamiento(short codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_TRATAMIENTO
                        where t.TIA_CODIGO == codigo
                        select t).FirstOrDefault();
            }
        }
    }
}
