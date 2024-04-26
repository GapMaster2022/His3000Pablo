using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using His.Entidades.Servicios;

namespace His.Negocio
{
    public class NegIngresoSalas
    {
        /// <summary>
        /// Método que crea un nuevo registro del intervención medica
        /// </summary>
        /// <param name="Ingreso Sala">Objeto Ingreso SalaS_PACIENTES que se guardara en la base de datos</param>
        public static void CrearIngresoSala(INTERVENCION_MEDICA1  ingresoSala)
        {
            try
            {
                new DatIngresoSalas().Crear(ingresoSala);
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Método que actualiza un registro de intervención medica
        /// </summary>
        /// <param name="ingresoSala">Objeto INTERVENCION_MEDICA1 que se actualizara en la base de datos</param>
        public static void Modificar(INTERVENCION_MEDICA1 ingresoSala)
        {
            try
            {
                new DatIngresoSalas().Modificar(ingresoSala);
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Método que elimina un registro de intervención medica
        /// </summary>
        /// <param name="ingresoSala">Objeto INTERVENCION_MEDICA1 que se eliminara de la base de datos</param>
        public static void Eliminar(Int64 codIngresoSala)
        {
            try
            {
                new DatIngresoSalas().Eliminar(codIngresoSala);
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Método que recupera un listado de intervenciónes medicas por atención
        /// </summary>
        /// <param name="codAtencion"></param>
        public static List<DtoLstIngresoSala> ListarPorAtencion(int codAtencion)
        {
            try
            {
                return new DatIngresoSalas().ListarPorAtencion(codAtencion);
            }
            catch (Exception err) { throw err; }
        }
    }
}
