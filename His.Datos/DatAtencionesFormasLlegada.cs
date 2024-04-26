using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
namespace His.Datos
{
    public class DatAtencionesFormasLlegada
    {
        /// <summary>
        /// Metodo que crea una nueva AtencionesFormasLlegada
        /// </summary>
        /// <param name="atencionFormaLLegada">envio el objeto ATENCION_FORMAS_LLEGADA </param>
        public void crear(ATENCION_FORMAS_LLEGADA atencionFormaLLegada)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.AddToATENCION_FORMAS_LLEGADA(atencionFormaLLegada);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; } 
        }

        public void actualizar(ATENCION_FORMAS_LLEGADA atencionFormaLLegada)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCION_FORMAS_LLEGADA atencionF = contexto.ATENCION_FORMAS_LLEGADA.Where(a => a.AFL_CODIGO==atencionFormaLLegada.AFL_CODIGO).FirstOrDefault();
                    atencionF.AFL_DESCRIPCION = atencionFormaLLegada.AFL_DESCRIPCION;
                    atencionF.AFL_ESTADO = atencionFormaLLegada.AFL_ESTADO;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; } 


        }

        /// <summary>
        /// Método que elimina un objeto ATENCION_FORMAS_LLEGADA
        /// </summary>
        /// <param name="atencionFormaLLegada">recibe un objeto ATENCION_FORMAS_LLEGADA</param>
        public void eliminar(ATENCION_FORMAS_LLEGADA atencionFormaLLegada)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.DeleteObject(atencionFormaLLegada);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; } 
        }

        /// <summary>
        /// Método que permite recuperar una lista de Atenciones de Formas de Llegada
        /// </summary>
        /// <returns>ATENCION_FORMAS_LLEGADA></returns>
        public List<ATENCION_FORMAS_LLEGADA> listaAtencionesFormasLlegada()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from f in contexto.ATENCION_FORMAS_LLEGADA
                        orderby f.AFL_DESCRIPCION
                        select f).ToList();
            }
        }

        /// <summary>
        /// Metodo que devuelve una forma de llegada enviando como parametro el codigo
        /// </summary>
        /// <param name="codigoFormaLlegada">codigo de la forma de llegada</param>
        /// <returns>devuelve un objeto de ATENCION_FORMAS_LLEGADA</returns>
        public ATENCION_FORMAS_LLEGADA atencionesFormasLlegadaPorID(Int16 codigoFormaLlegada)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from f in contexto.ATENCION_FORMAS_LLEGADA
                            where f.AFL_CODIGO == codigoFormaLlegada
                            select f).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; } 
        }

        /// <summary>
        /// Metodo que devuelve una forma de llegada enviando como parametro el codigo de la atencion
        /// </summary>
        /// <param name="codigoAtencion">codigo de la forma de llegada</param>
        /// <returns>devuelve un objeto de ATENCION_FORMAS_LLEGADA</returns>
        public ATENCION_FORMAS_LLEGADA atencionesFormasLlegadaPorAtencion(int codigoAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from f in contexto.ATENCION_FORMAS_LLEGADA
                            join a in contexto.ATENCIONES on f.AFL_CODIGO equals a.ATENCION_FORMAS_LLEGADA.AFL_CODIGO 
                            where a.ATE_CODIGO == codigoAtencion 
                            select f).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }
    }
}
