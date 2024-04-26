using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using His.Entidades.General;

namespace His.Datos
{
    public class DatIess
    {
        //Lista para tarifario Iess
        public List<DtoIess> RecuperarTarifario()
        {
            try
            {
                List<DtoIess> Iess = new List<DtoIess>();
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return
                   (from t in contexto.TARIFARIO_IESS
                    orderby t.COD_IESS descending
                    select new DtoIess
                    {
                        COD_IESS = t.COD_IESS,
                        NOM_EXA = t.NOM_EXA,
                        PR_LAB_NV3 = t.PR_LAB_NV3.Value
                    }).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
