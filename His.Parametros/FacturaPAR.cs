using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Core.Datos;

namespace His.Parametros
{
    public class FacturaPAR
    {
        private static Int16 serieFactUno = 001;
        private static Int16 serieFactDos = 001;
        //private static short serieFactAut = 1106391568;
        private static Int16 ivaUno = 0;
        private static Int16 ivaDos = 8;
        private static Int16 ivaTres = 12;

        
        private static Int16 BodegaDefectoFactura = 1;

        public static Int16 serieFacturasUno
        {
            get { return serieFactUno; }
            set { serieFactUno = value; }
        }

        public static Int16 serieFacturasDos
        {
            get { return serieFactDos; }
            set { serieFactDos = value; }
        }

        //public static Int16 serieFacturasAutorizacion
        //{
        //    get { return serieFactAut; }
        //    set { serieFactAut = value; }
        //}

        public static Int16 ivaValorUno
        {
            get { return ivaUno; }
            set { ivaUno = value; }
        }

        public static Int16 ivaValorDos
        {
            get { return ivaDos; }
            set { ivaDos = value; }
        }

        public static Int16 ivaValorTres
        {
            get { return ivaTres; }
            set { ivaTres = value; }
        }

        public static Int16 BodegaPorDefecto
        {
            get { return BodegaDefectoFactura; }
            set { BodegaDefectoFactura = value; }
        }

    }
}
