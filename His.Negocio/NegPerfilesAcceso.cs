using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
namespace His.Negocio
{
    public class NegPerfilesAcceso
    {
        public List<ACCESO_OPCIONES> AccesosUsuarios(Int16 usuario, Int16 modulo)
        {
            DatPerfilesAcceso datCliente = new DatPerfilesAcceso();
            return datCliente.AccesosUsuarios(usuario, modulo);

        }
        public static List<DtoAccesosPorPerfil> ListaConsultaTablasOpciones(int idModulo, int idPerfil)
        {
            return new DatPerfilesAcceso().ListaConsultaTablasOpciones(idModulo, idPerfil);
        }
        public static List<PERFILES_ACCESOS> ListaPerfilesAccesos()
        {
            return new DatPerfilesAcceso().ListaPerfilesAccesos();
        }
        public static void EliminaListaPerfilesAccesos(List<PERFILES_ACCESOS> acperModificado, List<PERFILES_ACCESOS> acperOriginal)
        {
            new DatPerfilesAcceso().EliminaListaPerfilesAccesos(acperModificado, acperOriginal);
        }
        public static void CrearPerfilesAccesos(PERFILES_ACCESOS perfilacceso)
        {
            new DatPerfilesAcceso().CrearPerfilesAccesos(perfilacceso);
        }
        public static List<PERFILES_ACCESOS> ListaPerfilesAccesosXmodulo(Int32 id_modulo)
        {
            return new DatPerfilesAcceso().ListaPerfilesAccesosXmodulo(id_modulo);
        }
        public static bool EliminarPerfiAcceso(Int64 id_ferfil, Int64 id_acceso)
        {
            return new DatPerfilesAcceso().EliminarPerfiAcceso(id_ferfil, id_acceso);
        }
        public static bool crearPerfilesAccessoXlista(List<ACCESO_OPCIONES> accop, Int32 id_perfil)
        {
            return new DatPerfilesAcceso().crearPerfilesAccessoXlista(accop, id_perfil);
        }
    }
}
