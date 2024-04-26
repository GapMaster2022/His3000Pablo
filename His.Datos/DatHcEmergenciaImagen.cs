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
    public class DatHcEmergenciaImagen
    {
        public int RecuperaMaximoHcEmergenciaImagen()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var id = contexto.HC_EMERGENCIA_IMAGENES.Select(h => h.IMA_CODIGO).Count();
                    if (id > 0)
                        return contexto.HC_EMERGENCIA_IMAGENES.Select(h => h.IMA_CODIGO).Max();
                    else
                        return 0;
                }
            }
            catch (Exception err) { throw err; }

        }

        public void CrearHcEmergenciaImagen(HC_EMERGENCIA_IMAGENES emergenciaImagen)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_IMAGENES(emergenciaImagen);
                contexto.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// Recupera la lista de Imágenes según Emergencia
        /// </summary>
        /// <param name="codigoTipoTratamiento"></param>
        /// <returns></returns>

        public List<HC_EMERGENCIA_IMAGENES> RecuperarHcEmergenciaImagenes(int codigoEmergencia)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_EMERGENCIA_IMAGENES.Where(c => c.IMA_EMERGENCIA == codigoEmergencia).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// Eliminar Imágenes
        /// </summary>
        /// <param name="enferPrevia"></param>
        public void EliminarHcEmergenciaImagenes(HC_EMERGENCIA_IMAGENES imagenen)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_IMAGENES imag = contexto.HC_EMERGENCIA_IMAGENES.Where(i => i.IMA_CODIGO == imagenen.IMA_CODIGO).FirstOrDefault();
                    contexto.DeleteObject(imag);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ActualizarHcImagen(HC_EMERGENCIA_IMAGENES imagen)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    HC_EMERGENCIA_IMAGENES imag = contexto.HC_EMERGENCIA_IMAGENES.FirstOrDefault(i => i.IMA_CODIGO == imagen.IMA_CODIGO);
                    imag.IMA_HC_CATALOGOS = imagen.IMA_HC_CATALOGOS;
                    imag.IMA_COMUNICADO = imagen.IMA_COMUNICADO;
                    imag.IMA_RESULTADO = imagen.IMA_RESULTADO;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
