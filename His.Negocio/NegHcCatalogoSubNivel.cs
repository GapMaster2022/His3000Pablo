using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegHcCatalogoSubNivel
    {
        public static int RecuperaMaximoHcCatalogoSubNivel()
        {
            return new DatHcCatalogoSubNivel().RecuperaMaximoHcCatalogoSubNivel();
        }
        //public static List<HC_CATALOGO_SUBNIVEL> RecuperarHcCatalogoSubNivel(int codTipo)
        //{
        //    //return new DatHcCatalogoSubNivel().RecuperarHcCatalogoSubNivel(codTipo);
        //}
        public static List<HC_CATALOGO_SUBNIVEL> RecuperarHcCatalogosSubNivel(int codTipo)
        {
            return new DatHcCatalogoSubNivel().RecuperarHcCatalogosSubNivel(codTipo);
        }

        public static HC_CATALOGO_SUBNIVEL RecuperarHCCatalogosSubnivel(int codTipo)
        {
            return new DatHcCatalogoSubNivel().RecuperarHCCatalogosSubnivel(codTipo);
        }

        public static HC_CATALOGO_SUBNIVEL RecuperarHCCatalogosSNivel(int codTipo)
        {
            return new DatHcCatalogoSubNivel().RecuperarHCCatalogosSNivel(codTipo);
        }

       
        public static List<HC_CATALOGO_SUBNIVEL> RecuperarSubNivel(int codTipo)
        {
            return new DatHcCatalogoSubNivel().RecuperarSubNivel(codTipo);
        }

        public static HC_CATALOGO_SUBNIVEL RecuperarSubNivelCatalogo(int codCatalogo, int codSubNivel)
        {
            return new DatHcCatalogoSubNivel().RecuperarSubNivelCatalogo(codCatalogo, codSubNivel);
        }
    }
}
