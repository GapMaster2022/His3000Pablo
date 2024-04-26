using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegTipoDocumentos
    {
        public static Int16 RecuperaMaximoTipoDocumento()
        {
            return new DatTipoDocumentos().RecuperaMaximoTipoDocumento();
        }
        public static List<TIPO_DOCUMENTO> RecuperaTipoDocumentos()
        {
            return new DatTipoDocumentos().RecuperaTipoDocumentos();
        }
        public static void CrearTipoDocumento(TIPO_DOCUMENTO tipodocumento)
        {
            new DatTipoDocumentos().CrearTipoDocumento(tipodocumento);
        }
        public static void GrabarTipoDocumento(TIPO_DOCUMENTO tipodocumentoModificada, TIPO_DOCUMENTO tipodocumentoOriginal)
        {
            new DatTipoDocumentos().GrabarTipoDocumento(tipodocumentoModificada, tipodocumentoOriginal);
        }
        public static void EliminarTipoDocumento(TIPO_DOCUMENTO tipodocumento)
        {
            new DatTipoDocumentos().EliminarTipoDocumento(tipodocumento);
        }
        public static TIPO_DOCUMENTO RecuperarTipoDocumentoID(int codTipoDoc)
        {
            return new DatTipoDocumentos().RecuperarTipoDocumentoID(codTipoDoc);
        }
    }
}
