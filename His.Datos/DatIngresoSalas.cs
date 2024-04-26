using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Entidades.Servicios;

namespace His.Datos
{
    public class DatIngresoSalas
    {
        #region metodos generales

        /// <summary>
        /// Método que crea un nuevo registro de intervención medica
        /// </summary>
        /// <param name="ingresoSala">Objeto INTERVENCION_MEDICA1 que se guardara en la base de datos</param>
        public void Crear(INTERVENCION_MEDICA1 ingresoSala)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    int codIngresoSala; 
                    INTERVENCION_MEDICA1 ingSalaUltimo = contexto.INTERVENCION_MEDICA1Conjunto.OrderByDescending(i=>i.INT_CODIGO).FirstOrDefault();
                    codIngresoSala = ingSalaUltimo != null ? ingSalaUltimo.INT_CODIGO + 1 : 1;
                    ingresoSala.INT_CODIGO = codIngresoSala;
                    contexto.AddToINTERVENCION_MEDICA1Conjunto(ingresoSala);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Método que actualiza un registro de intervención medica
        /// </summary>
        /// <param name="ingresoSala">Objeto INTERVENCION_MEDICA1 que se actualizara en la base de datos</param>
        public void Modificar(INTERVENCION_MEDICA1 ingresoSala)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    INTERVENCION_MEDICA1 ingresoSalaOri = contexto.INTERVENCION_MEDICA1Conjunto.Where(i => i.INT_CODIGO==ingresoSala.INT_CODIGO).FirstOrDefault();
                    ingresoSalaOri.ID_USUARIO = ingresoSala.ID_USUARIO;
                    ingresoSalaOri.INT_ESTADO = ingresoSala.INT_ESTADO;
                    ingresoSalaOri.INT_FECHA_FIN = ingresoSala.INT_FECHA_FIN;
                    ingresoSalaOri.INT_FECHA_INI = ingresoSala.INT_FECHA_INI;
                    ingresoSalaOri.INT_TIPO = ingresoSala.INT_TIPO;
                    ingresoSalaOri.MED_CODIGO = ingresoSala.MED_CODIGO;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Método que elimina un registro de intervención medica
        /// </summary>
        /// <param name="ingresoSala">Objeto INTERVENCION_MEDICA1 que se eliminara de la base de datos</param>
        public void Eliminar(Int64 codIngresoSala)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    INTERVENCION_MEDICA1 ingresoSalaOri = contexto.INTERVENCION_MEDICA1Conjunto.Where(i => i.INT_CODIGO == codIngresoSala).FirstOrDefault();
                    contexto.DeleteObject(ingresoSalaOri);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }
        /// <summary>
        /// Método que recupera un listado de intervenciónes medicas por atención
        /// </summary>
        /// <param name="codAtencion"></param>
        public List<DtoLstIngresoSala> ListarPorAtencion(int codAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var lstIngresos = from i in contexto.INTERVENCION_MEDICA1Conjunto
                                   join u in contexto.USUARIOS on i.ID_USUARIO equals u.ID_USUARIO
                                   join m in contexto.MEDICOS on i.MED_CODIGO equals m.MED_CODIGO
                                   join t in contexto.TIPO_INTER_MEDICA on i.INT_TIPO equals t.TII_CODIGO
                                   where i.ATE_CODIGO == codAtencion && i.INT_ESTADO == true
                                   select new DtoLstIngresoSala
                                   {
                                       INT_CODIGO = i.INT_CODIGO,
                                       INT_FECHA_INI = i.INT_FECHA_INI.Value,
                                       INT_FECHA_FIN = i.INT_FECHA_FIN.Value,
                                       ID_USUARIO = i.ID_USUARIO.Value,
                                       NOM_USUARIO = u.NOMBRES,
                                       MED_CODIGO = i.MED_CODIGO.Value,
                                       NOM_MEDICO = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO +
                                                    m.MED_NOMBRE1 + " " + m.MED_NOMBRE2,
                                       INT_ESTADO = i.INT_ESTADO.Value,
                                       INT_TIPO = i.INT_TIPO.Value,
                                       NOM_TIPO = t.TII_NOMBRE,
                                       ATE_CODIGO = i.ATE_CODIGO.Value
                                   };
                    return lstIngresos.ToList();
                }
            }
            catch (Exception err) { throw err; }
        }

    #endregion
    }
}
