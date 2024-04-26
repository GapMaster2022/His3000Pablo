using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;


namespace His.Datos
{
    public class DatRetenciones
    {
        /// <summary>
        /// Metodo que guarda una retencion en la base de datos
        /// </summary>
        /// <param name="retencion">Retencion a guardar</param>
        public void  CrearRetencion(RETENCIONES retencion)
        {
            using(var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM )) 
                {
                    contexto.Crear("RETENCIONES", retencion);   
                }
        }
        public void GrabarRetencion(RETENCIONES retencionModificada, RETENCIONES retencionOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Grabar(retencionModificada, retencionOriginal);
            }
        }
        public List<DtoRetenciones> RecuperaRetenciones()
        {
            List<DtoRetenciones> medicogrid = new List<DtoRetenciones>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<RETENCIONES> medicos = new List<RETENCIONES>();
                medicos = contexto.RETENCIONES.Include("CAJAS").Include("USUARIOS").Include("RETENCIONES_FUENTE").ToList();
                foreach (var acceso in medicos)
                {
                    medicogrid.Add(new DtoRetenciones()
                    {
                        CAJ_CODIGO = acceso.CAJAS.CAJ_CODIGO,
                        ID_USUARIO = acceso.USUARIOS.ID_USUARIO,
                        RET_BASE=acceso.RET_BASE,
                        RET_CODIGO=acceso.RET_CODIGO,
                        RET_DOCUMENTO_AFECTADO=acceso.RET_DOCUMENTO_AFECTADO,
                        RET_DOCUMENTO_TIPO=acceso.RET_DOCUMENTO_TIPO,
                        RET_EJERCICIO_FISCAL=acceso.RET_EJERCICIO_FISCAL==null ? Int16.Parse(DateTime.Now.Year.ToString()): Int16.Parse(acceso.RET_EJERCICIO_FISCAL.ToString()),
                        RET_FECHA=acceso.RET_FECHA,
                        RET_IMPRESA=acceso.RET_IMPRESA,
                        RET_PORCENTAJE=acceso.RET_PORCENTAJE,
                        RET_RUC=acceso.RET_RUC,
                        RET_SUJETO_RETENCION=acceso.RET_SUJETO_RETENCION,
                        RET_VALOR=acceso.RET_VALOR==null? decimal.Parse("0"): decimal.Parse(acceso.RET_VALOR.ToString()),
                        RET_RET_CODIGO= acceso.RETENCIONES_FUENTE.RET_CODIGO,
                        RET_ANULADO=acceso.RET_ANULADO==null? false : bool.Parse(acceso.RET_ANULADO.ToString()),
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key
                    });
                }
                return medicogrid;
            }
        }
    }
}
