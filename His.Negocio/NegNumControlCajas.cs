using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegNumControlCajas
    {
        public static Int16 RecuperarMaximoNumControlCajas()
        {
            return new DatNumeroControlCajas().RecuperaMaximoNumeroControlCajas();  
        }

        public static List<NUMERO_CONTROL_CAJAS> RecuperarNumControlCajas()
        {
            return new DatNumeroControlCajas().ListaNumeroControlCajas();  
        }
        public static void CrearNumControlCajas(NUMERO_CONTROL_CAJAS numcontrolCajas)
        {
            new DatNumeroControlCajas().CrearNumControlCaja(numcontrolCajas);   
        }
        public static void GrabarNumControlCajas(NUMERO_CONTROL_CAJAS nccModificada, NUMERO_CONTROL_CAJAS nccOriginal)
        {
            new DatNumeroControlCajas().GrabarNumControlCaja(nccModificada, nccOriginal);    
        }
        public static void EliminarNumControlCajas(NUMERO_CONTROL_CAJAS numcontrolCajas)
        {
            new DatNumeroControlCajas().EliminarNumControlCaja(numcontrolCajas);
        }
        public static List<DtoNumeroControlCajas> RecuperaNumControlCajas()
        {
            return new DatNumeroControlCajas().RecuperaNumControlCajas();  
        }
        public static List<DtoNumeroControlCajas> RecuperaNumControlCajas(int codCaja)
        {
            return new DatNumeroControlCajas().RecuperaNumControlCajas(codCaja);
        }

        public static NUMERO_CONTROL_CAJAS RecuperarNumeroControlCajaDoc(int codCaja)
        {
            return new DatNumeroControlCajas().RecuperarNumeroControlCajaDoc(codCaja);
        }

        public static NUMERO_CONTROL_CAJAS RecuperarNumeroControlCajaID(int codNumControlCaja)
        {
            return new DatNumeroControlCajas().RecuperarNumeroControlCajaID(codNumControlCaja);
        }
        public static void EditarNumControlCaja(NUMERO_CONTROL_CAJAS nccModificada)
        {
            new DatNumeroControlCajas().EditarNumControlCaja(nccModificada);
        }
    }
}
