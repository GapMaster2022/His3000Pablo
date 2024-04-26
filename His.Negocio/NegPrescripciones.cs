using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;


namespace His.Negocio
{
    public class NegPrescripciones
    {
        public static void crearPrescripcion(HC_PRESCRIPCIONES nPrescripcion)
        {
            new DatPrescripciones().crearPrescripcion(nPrescripcion);
        }

        public static List<HC_PRESCRIPCIONES> listaPrescripciones(Int64 codNotaEvolucion)
        {
            return new DatPrescripciones().listaPrescripciones(codNotaEvolucion);
        }

        public static int ultimoCodigo()
        {
            return new DatPrescripciones().ultimoCodigo();
        }

        public static void editarPrescripcion(HC_PRESCRIPCIONES ePrescripcion)
        {
            new DatPrescripciones().editarPrescripcion(ePrescripcion);
        }

        public void EditarIndicaciones(string pres_codigo, string indicaciones)
        {
            DatPrescripciones pres = new DatPrescripciones();
            pres.EditarIndicaciones(Convert.ToInt32(pres_codigo), indicaciones);
        }
        public static HC_PRESCRIPCIONES recuperarPrescripcionID(int codPres)
        {
            return new DatPrescripciones().recuperarPrescripcionID(codPres);
        }

        public static void IngresaImagen(Int64 Codigo, byte[] Imagen)
        {
            new DatPrescripciones().IngresaImagen(Codigo, Imagen);
        }

        public static DataTable RegresaImagen(Int64 codProducto)
        {
            return new DatPrescripciones().RegresaImagen(codProducto);
        }

    }
}
