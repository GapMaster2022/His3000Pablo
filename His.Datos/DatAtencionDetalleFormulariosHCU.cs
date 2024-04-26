using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
namespace His.Datos
{
    public class DatAtencionDetalleFormulariosHCU
    {
        public void Crear(ATENCION_DETALLE_FORMULARIOS_HCU formularioNuevo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToATENCION_DETALLE_FORMULARIOS_HCU(formularioNuevo);
                contexto.SaveChanges();
            }
        }

        public List<ATENCION_DETALLE_FORMULARIOS_HCU> listaFormulariosAtencion(int codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from l in contexto.ATENCION_DETALLE_FORMULARIOS_HCU
                                 join a in contexto.ATENCIONES on l.ATENCIONES.ATE_CODIGO equals a.ATE_CODIGO
                                 join f in contexto.FORMULARIOS_HCU on l.FORMULARIOS_HCU.FH_CODIGO equals f.FH_CODIGO
                                 where a.ATE_CODIGO==codAtencion
                                 select l).ToList();

            }
        }

        public int maxCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from l in contexto.ATENCION_DETALLE_FORMULARIOS_HCU
                             select l.ADF_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }
        /// <summary>
        /// Metodo que devuelve el detalle de los formularios pertenecientes a una atención
        /// </summary>
        /// <param name="codAtencion">Codigo de la atención</param>
        /// <returns>Retorna una lista de ATENCION_DETALLE_FORMULARIOS_HCU</returns>
        public List<ATENCION_DETALLE_FORMULARIOS_HCU> listaAtencionDetalleFormularios(int codAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var dbQuery = from d in contexto.ATENCION_DETALLE_FORMULARIOS_HCU
                                  where d.ATENCIONES.ATE_CODIGO == codAtencion
                                  select new { atencionFormulariosHCU = d, formularios = d.FORMULARIOS_HCU,atenciones = d.ATENCIONES };
                    var listaAtencionFormulariosHCU = dbQuery.AsEnumerable().Select(d => d.atencionFormulariosHCU).OrderBy(d => d.ADF_CODIGO);

                    return (List<ATENCION_DETALLE_FORMULARIOS_HCU>)listaAtencionFormulariosHCU.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
