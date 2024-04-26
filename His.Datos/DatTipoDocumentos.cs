using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatTipoDocumentos
    {
        public Int16 RecuperaMaximoTipoDocumento()
        {
            Int16 maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<TIPO_DOCUMENTO> tidocumento = contexto.TIPO_DOCUMENTO.ToList();
                if (tidocumento.Count > 0)
                    maxim = contexto.TIPO_DOCUMENTO.Max(emp => emp.TID_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }
        public List<TIPO_DOCUMENTO> RecuperaTipoDocumentos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_DOCUMENTO
                        select t).ToList();
            }
        }
        public void CrearTipoDocumento(TIPO_DOCUMENTO tipodocumento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Crear("TIPO_DOCUMENTO", tipodocumento);

            }
        }
        public void GrabarTipoDocumento(TIPO_DOCUMENTO tipodocumentoModificada, TIPO_DOCUMENTO tipodocumentoOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Grabar(tipodocumentoModificada, tipodocumentoOriginal);
            }
        }
        public void EliminarTipoDocumento(TIPO_DOCUMENTO tipodocumento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(tipodocumento);
            }
        }

        public TIPO_DOCUMENTO RecuperarTipoDocumentoID(int codTipoDocumento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from t in contexto.TIPO_DOCUMENTO
                        where t.TID_CODIGO == codTipoDocumento
                        select t).FirstOrDefault();
            }
        }
    }
}
