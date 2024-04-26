using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatPreAtencion
    {
        public int recuperaMaximoPreAtencion()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PREATENCIONES> listpreAt = contexto.PREATENCIONES.ToList();
                if (listpreAt.Count > 0)
                    maxim = contexto.PREATENCIONES.Max(emp => emp.PREA_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }

        public void crearPreAtencion(PREATENCIONES preAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("PREATENCIONES", preAtencion);
            }
        }

        public PREATENCIONES recuperaPreAtencion(int codigoAte)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in contexto.PREATENCIONES
                        where p.PREA_COD_ATENCION == codigoAte 
                        select p).FirstOrDefault();
            }
        }
        

        public void editarPreAtencion(PREATENCIONES preAtencionModificada)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PREATENCIONES preAtencion = contexto.PREATENCIONES.FirstOrDefault(p => p.PREA_CODIGO == preAtencionModificada.PREA_CODIGO);
                preAtencion.PREA_FECHA_PREADMISON = preAtencionModificada.PREA_FECHA_PREADMISON;
                preAtencion.PREA_FEC_INGRESO = preAtencionModificada.PREA_FECHA_ADMISON;
                preAtencion.PREA_ESTADO = preAtencionModificada.PREA_ESTADO;               
                contexto.SaveChanges();
            }
        }
    }
}
