using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegIngestaEliminacion
    {
        public static HC_INGESTA_ELIMINACION recuperaIngestaXcodigo(Int64 IE_CODIGO)
        {
            return new DatIngestaEliminacion().recuperaIngestaXcodigo(IE_CODIGO);
        }
        public static void GrabarIngElm(int ATE_CODIGO, int PAC_CODIGO, DateTime IE_FECHA)
        {
            new DatIngestaEliminacion().GrabarIngElm(ATE_CODIGO, PAC_CODIGO, IE_FECHA);
        }

        public static DataTable ExistenciaIngElm(int ATE_CODIGO, int PAC_CODIGO)
        {
            return new DatIngestaEliminacion().ExistenciaIngElm(ATE_CODIGO, PAC_CODIGO);
        }
        public static DataTable getIngElm(Int64 ate_codigo)
        {
            return new DatIngestaEliminacion().getIngElm(ate_codigo);
        }
        public static bool GrabarDetalleIngElm(HC_INGESTA_ELIMINACION_DETALLE ied, string MODO)
        {
           return new DatIngestaEliminacion().GrabarDetalleIngElm(ied, MODO);
        }
        public static DataTable cargaGrid(string IED_TIPO, Int32 IE_CODIGO)
        {
            return new DatIngestaEliminacion().cargaGrid(IED_TIPO, IE_CODIGO);
        }
        public static DataTable cargarNoche(string IED_TIPO, Int32 IE_CODIGO, String Fecha)
        {
            return new DatIngestaEliminacion().cargarNoche(IED_TIPO, IE_CODIGO, Fecha);
        }
        public static DataTable UltimoRegistro()
        {
            return new DatIngestaEliminacion().UltimoRegistro();
        }
        public static bool eliminarIngElm(Int32 IED_CODIGO)
        {
            return new DatIngestaEliminacion().eliminarIngElm(IED_CODIGO);
        }
        public static Int32 SumaTotales(Int32 IE_CODIGO, string par)
        {
            return new DatIngestaEliminacion().SumaTotales(IE_CODIGO, par);
        }
        public static Int32 ExistenciaParaSignosVitales(Int64 ATE_CODIGO, DateTime IE_FECHA)
        {
            return new DatIngestaEliminacion().ExistenciaParaSignosVitales(ATE_CODIGO, IE_FECHA);
        }
        public static Int32 sumaIngesta(Int32 IE_CODIGO, string par)
        {
            return new DatIngestaEliminacion().sumaIngesta(IE_CODIGO,par);
        }
        public static Int32 sumaOrinaDeposiciones(Int32 IE_CODIGO, string par)
        {
            return new DatIngestaEliminacion().sumaOrinaDeposiciones(IE_CODIGO, par);
        }
        public static HC_INGESTA_ELIMINACION_DETALLE recuperaDetalleUsuario(Int32 IED_CODIGO)
        {
            return new DatIngestaEliminacion().recuperaDetalleUsuario(IED_CODIGO);
        }
        public static Int32 idUsuario(Int32 IE_CODIGO, string par)
        {
            return new DatIngestaEliminacion().idUsuario(IE_CODIGO, par);
        }
        public static List<HC_INGESTA_ELIMINACION> recuperaIngesta(Int64 ATE_CODIGO)
        {
            return new DatIngestaEliminacion().recuperaIngesta(ATE_CODIGO);
        }
        public static DataTable ingestaXfecha(Int64 ate_codigo, DateTime fecha)
        {
            return new DatIngestaEliminacion().ingestaXfecha(ate_codigo, fecha);
        }
        public static DSAyudaIngElm cargaKardexCompuesto(Int64 ATE_CODIGO)
        {
            return new DatIngestaEliminacion().cargaKardexCompuesto(ATE_CODIGO);
        }
        public static string compuestoKardex(Int64 ID)
        {
            return new DatIngestaEliminacion().compuestoKardex(ID);
        }
        public static List<ABREVIACIONES> listadoAbrevioaciones()
        {
            return new DatIngestaEliminacion().listadoAbrevioaciones();
        }
        public static bool desacticaKardex(Int64 ID)
        {
            return new DatIngestaEliminacion().desacticaKardex(ID);
        }
        public static List<TIPO_DRENAJE> cargaDrenaje()
        {
            return new DatIngestaEliminacion().cargaDrenaje();
        }
    }
}
