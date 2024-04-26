using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegPerfil
    {
        public static Int16 RecuperaMaximoPerfil()
        {
            return new DatPerfiles().RecuperaMaximoPerfil();
        }
        public static List<PERFILES> RecuperaPerfiles()
        {
            return new DatPerfiles().RecuperaPerfiles();
        }
        public static void CrearPerfil(PERFILES perfil)
        {
            new DatPerfiles().CrearPerfil(perfil);

        }
        public static void GrabarPerfil(PERFILES perfilModificada, PERFILES perfilOriginal)
        {
            new DatPerfiles().GrabarPerfil(perfilModificada, perfilOriginal);

        }
        public static void EliminarPerfil(PERFILES perfil)
        {
            new DatPerfiles().EliminarPerfil(perfil);
        }
        /// <summary>
        /// Metodo que retorna el perfil de un usuario
        /// </summary>
        /// <param name="codigoUsuario">Codigo de usuario</param>
        /// <returns>Objeto PERFILES</returns>
        public List<PERFILES> RecuperarPerfil(int codigoUsuario)
        {
            return new DatPerfiles().RecuperarPerfil(codigoUsuario);
        }
        public static PERFILES RecuperaPerfil(Int64 perfil)
        {
            return new DatPerfiles().RecuperaPerfil(perfil);
        }
        public static void borrarPerfiles(Int64 ID_PERFIL, Int64 ID_MODULO)
        {
            new DatPerfiles().borrarPerfiles(ID_PERFIL, ID_MODULO);
        }
        public static void BorraPerfilesSic(Int64 id_perfil, Int64 id_modulo)
        {
            new DatPerfiles().BorraPerfilesSic(id_perfil, id_modulo);
        }
        public static void BorraPerfilesCg(Int64 id_perfil, Int64 id_modulo)
        {
            new DatPerfiles().BorraPerfilesCg(id_perfil, id_modulo);
        }
        public static PERFILES recuperaPerfilesXUsuario(Int64 id_usuario)
        {
            return new DatPerfiles().recuperaPerfilesXUsuario(id_usuario);
        }
    }
}
