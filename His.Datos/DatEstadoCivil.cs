using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatEstadoCivil
    {
        public List<ESTADO_CIVIL> RecuperaEstadoCivil()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from e in contexto.ESTADO_CIVIL
                        orderby e.ESC_NOMBRE
                        select e).ToList();
            }
        }

        public ESTADO_CIVIL RecuperaEstadoCivilID(int codigoEstadoCivil)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from e in contexto.ESTADO_CIVIL
                        where e.ESC_CODIGO == codigoEstadoCivil
                        select e).FirstOrDefault();
            }
        }
    }
}
