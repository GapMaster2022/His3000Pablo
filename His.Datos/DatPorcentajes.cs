using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatPorcentajes
    {
        public List<PORCENTAJES> RecuperaPorcentajes()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.PORCENTAJES.ToList();
            }
        }
    }
}
