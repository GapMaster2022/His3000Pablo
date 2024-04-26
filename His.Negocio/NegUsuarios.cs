using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegUsuarios
    {
        #region Consultas
        public static void CrearUsuarioPerfiles(USUARIOS_PERFILES upNuevo)
        {
            new DatUsuarios().CrearUsuarioPerfiles(upNuevo);
        }
        public static void EliminaUsuarioPerfiles(List<USUARIOS_PERFILES> upModificada, List<USUARIOS_PERFILES> upOriginal)
        {
            new DatUsuarios().EliminaUsuarioPerfiles(upModificada, upOriginal);
        }

        public static List<USUARIOS_PERFILES> ListaUsuarioPerfiles()
        {
            return new DatUsuarios().ListaUsuarioPerfiles();
        }
        public static void CrearUsuario(USUARIOS usuario)
        {
            new DatUsuarios().CrearUsuario(usuario);
        }
        public static DataTable BuscaCedula(string cedula)
        {
            return new DatUsuarios().BuscaCedula(cedula);

        }
        public static DataTable BuscaUsuario(string nomusuario)
        {
            return new DatUsuarios().BuscaUsuario(nomusuario);

        }
        public static List<ARE_ASIGNADA> cargarAreaAsignada()
        {
            return new DatUsuarios().cargarAreaAsignada();
        }
        public Int32 insertarUsuario(Usuarios usuarios)
        {
            try
            {
                Int32 resp = 0;

                DatUsuarios usuariosDAL = new DatUsuarios();
                usuariosDAL.insertarUsuario(usuarios);
                return resp;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public Int32 actualizar(Usuarios usuarios)
        {
            try
            {
                Int32 resp = 0;
                DatUsuarios usuariosDAL = new DatUsuarios();
                resp = usuariosDAL.actualizar(usuarios);

                return resp;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public Usuarios buscarporId(Int64 idUsuario)
        {
            Usuarios usuarios = new Usuarios();
            DatUsuarios usuariosDAL = new DatUsuarios();
            usuarios = usuariosDAL.buscarporId(idUsuario);
            return usuarios;

        }
        public static void GrabarUsuario(USUARIOS usuarioModificada, USUARIOS usuarioOriginal)
        {
            new DatUsuarios().GrabarUsuario(usuarioModificada, usuarioOriginal);
        }
        public static void ActualizarUsuario(USUARIOS usuario)
        {
            new DatUsuarios().ActualizarUsuario(usuario);
        }
        public static bool EliminarUsuario(Int64 id_usuario)
        {
            return new DatUsuarios().EliminarUsuario(id_usuario);
        }
        public static Int16 RecuperaMaximoUsuario()
        {
            return new DatUsuarios().RecuperaMaximoUsuario();
        }
        public static List<DtoUsuariosPerfil> ListaConsultaTablasOpciones(Int16 codusuario)
        {
            return new DatUsuarios().ListaConsultaTablasOpciones(codusuario);
        }
        public static List<DtoAccesosSic> BuscaPerfilesSic(Int64 codmod, Int64 id_usu)
        {
            return new DatUsuarios().BuscaPerfilesSic(codmod, id_usu);
        }
        public static List<DtoAccesosSic> BuscaConsidenciasSic(double codusu)
        {
            return new DatUsuarios().BuscaConsidenciasSic(codusu);
        }
        //public static DataTable BuscaPerfilesCg()
        //{
        //    return new DatUsuarios().BuscaPerfilesCG();
        //}
        public static List<DtoAccesosSic> BuscaConsidenciasCG(double codusu)
        {
            return new DatUsuarios().BuscaConsidenciasCG(codusu);
        }
        public static void CrearPerfilSicSP(double codusu, double codopc, string staopc, string sw)
        {
            new DatUsuarios().CrearPerfilSicSP(codusu, codopc, staopc, sw);
        }
        public static bool CrearPerfilSic(Int64 codusu, Int64 codopc, string staopc, string sw)
        {
            return new DatUsuarios().CrearPerfilSic(codusu, codopc, staopc, sw);
        }
        public static bool CrearPerfilSicUsu(Int64 codusu, Int64 codopc, bool staopc)
        {
            return new DatUsuarios().CrearPerfilSicUsu(codusu, codopc, staopc);
        }
        public static bool BorrarPerfilSicUsu(Int64 codusu)
        {
            return new DatUsuarios().BorrarPerfilSicUsu(codusu);
        }
        public static bool ActualizarMedicoUsuario(Int64 codusu,string ruc)
        {
            return new DatUsuarios().ActualizarMedicoUsuario(codusu,ruc);
        }

        public static bool BorrarPerfilCgUsu(Int64 codusu)
        {
            return new DatUsuarios().BorrarPerfilCgUsu(codusu);
        }
        public static bool CrearPerfilCgUsu(Int64 codusu, Int64 codopc, bool staopc)
        {
            return new DatUsuarios().CrearPerfilCgUsu(codusu, codopc, staopc);
        }
        public static bool CrearPerfilCg(Int64 codusu, Int64 codopc, string staopc, string sw)
        {
            return new DatUsuarios().CrearPerfilCg(codusu, codopc, staopc, sw);
        }
        //public static void crearPerfilesCG(double maquina)
        //{
        //    new DatUsuarios().crearPerfilesCG(maquina);
        //}
        public static bool crearPerfilesSic(string grupo)
        {
            return new DatUsuarios().crearPerfilesSic(grupo);
        }
        public static DataTable AreaAsignada(Int16 ID_USUARIO)
        {
            return new DatUsuarios().AreaAsignada(ID_USUARIO);
        }
        public static USUARIOS RecuperaUsuario(Int64 codusu)
        {
            return new DatUsuarios().RecuperaUsuario(codusu);
        }
        public static USUARIOS_FIRMA recuperaFirma(Int64 ID_USUARIO)
        {
            return new DatUsuarios().recuperaFirma(ID_USUARIO);
        }
        public static USUARIOS RecuperaUsuarioCedula(string codusu)
        {
            return new DatUsuarios().RecuperaUsuarioCedula(codusu);
        }
        public static USUARIOS RecuperaUsuarioNombres(string Datos) // Encuentra el codigo de un usuario apartir del nombre / Giovanny Tapia /18/09/2012
        {
            return new DatUsuarios().RecuperaUsuarioNombres(Datos);
        }

        public static List<USUARIOS> RecuperaUsuarios()
        {
            return new DatUsuarios().RecuperaUsuarios();
        }
        public static List<DtoUsuarios> RecuperaUsuariosFormulario()
        {
            return new DatUsuarios().RecuperaUsuariosFormulario();
        }
        public List<DtoAccesosPorPerfil> ListaConsultaTablasOpciones(int idModulo, int idPerfil)
        {

            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            return datCliente.ListaConsultaTablasOpciones(idModulo, idPerfil);
        }

        public DataSet RecuperarClientesDataSet()
        {

            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            return datCliente.RecuperarClientesDataSet();
        }
        //public List<His.Entidades.Usuarios> RecuperarClientesClientes()
        //{
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    return datCliente.RecuperarClientesClientes();
        //}
        //public DataSet RecuperaUsuario(Int16 codusu)
        //{
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    return datCliente.RecuperaUsuario(codusu);
        //}

        public DataSet RecuperaNumeroControl(int a)
        {
            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            return datCliente.RecuperaNumeroControl(a);
        }
        /// <summary>
        /// Verifica si existe un usuario
        /// </summary>
        /// <param name="procedimiento">nombre del procedimeinto</param>
        /// <param name="usuario">datos de usuario</param>
        /// <returns>dataset</returns>
        //public int ExisteUsuario(string procedimiento, Usuarios usuario) {
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    return datCliente.ExisteUsuario(procedimiento, usuario);
        //}
        public DataSet ListaConsultaTablas(string consulta)
        {
            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            return datCliente.ListaConsultaTablas(consulta);
        }

        #endregion
        #region Afectaciones
        ///// <summary>
        ///// Inserta usuarios
        ///// </summary>
        ///// <param name="procedimiento">Nombre del procedimiento</param>
        ///// <param name="usuario">Datos del ususario</param>
        //public void InsertaUsuarios(string procedimietno, Usuarios usuario) 
        //{
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    datCliente.InsertaUsuarios(procedimietno, usuario);
        //}
        ///// <summary>
        ///// Modifica usuarios
        ///// </summary>
        ///// <param name="procedimiento">Nombre del procedimiento</param>
        ///// <param name="usuario">Datos del ususario</param>
        //public void ModificaUsuarios(string procedimietno, Usuarios usuario)
        //{
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    datCliente.ModificaUsuarios(procedimietno, usuario);
        //}
        //public void AumentaNumeroControl(int a) {
        //    His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
        //    datCliente.AumentaNumeroControl(a);
        //}
        /// <summary>
        /// inserta campos en una tabla cualquiera
        /// </summary>
        /// <param name="procedimiento">nombre del procedimiento</param>
        /// <param name="Args">argumentos del sp</param>
        public void InsertaenTabla(string procedimiento, List<object> Args)
        {
            His.Datos.DatDatos datCliente = new His.Datos.DatDatos();
            datCliente.InsertaenTabla(procedimiento, Args);
        }
        #endregion
        /// <summary>
        /// Metodo que valida el login y password  de un usuario
        /// </summary>
        /// <param name="usr">login</param>
        /// <param name="pwd">clave</param>
        /// <returns>Si/No el usuario existe</returns>
        public static USUARIOS ValidarUsuario(string usr, string pwd)
        {
            try
            {
                USUARIOS usuario;
                usuario = new DatUsuarios().ValidarUsuario(usr, pwd);
                return usuario;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public static USUARIOS_PERFILES perfilUsuario(int codUsuario)
        {
            return new DatUsuarios().perfilUsuario(codUsuario);
        }
        public static USUARIOS RecuperarUsuarioID(Int64 codUsuario)
        {
            return new DatUsuarios().RecuperarUsuarioID(codUsuario);
        }

        public static DataTable RecuperaUsuariosCajeros()
        {
            return new DatUsuarios().RecuperaUsuariosCajeros();
        }
        public static DataTable NickName()
        {
            return new DatUsuarios().NickName();
        }
        public static List<DtoUsuarioReposion> UsuarioReposicion(int id_usuario)
        {
            return new DatUsuarios().RecuperarUsuarioActual(id_usuario);
        }
        public static DEPARTAMENTOS ConsultaDepartamento(int dep)
        {
            return new DatUsuarios().ConsultaDepartamento(dep);
        }
        public static ARE_ASIGNADA ConsultaArea(int area)
        {
            return new DatUsuarios().ConsultaArea(area);
        }
        public static int ConsultaUsuario(string cedula)
        {
            return new DatUsuarios().ConsultaUsuario(cedula);
        }

        public static DataTable ConsultaUsuarioDep(string cedula)
        {
            return new DatUsuarios().ConsultaUsuarioDep(cedula);
        }
        public static USUARIOS ConsultaUsuarioDepModelo(string cedula)
        {
            return new DatUsuarios().ConsultaUsuarioDepModelo(cedula);
        }
        public static DataTable ULtimoCodigo()
        {
            return new DatUsuarios().ULtimoCodigo();
        }
        public static bool CrearPerfilHis(int ID_PERFIL, int ID_USUARIO)
        {
            return new DatUsuarios().CrearPerfilHis(ID_PERFIL, ID_USUARIO);
        }
        public static DataTable UsuariosSic()
        {
            return new DatUsuarios().UsuariosSic();
        }
        public static List<DtoModulo> ModuloSic()
        {
            return new DatUsuarios().ModuloSic();
        }
        public static DataTable UsuariosCG()
        {
            return new DatUsuarios().UsuariosCG();
        }
        public static List<DtoModulo> ModuloCG()
        {
            return new DatUsuarios().ModuloCG();
        }
        public static List<DtoAccesosSic> BuscaPerfilesCg(Int64 codmod, Int64 id_usu)
        {
            return new DatUsuarios().BuscaPerfilesCg(codmod, id_usu);
        }
        public static DataTable PerfilesSic()
        {
            return new DatUsuarios().PerfilesSic();
        }
        public static bool crearModuloSic(string modulo)
        {
            return new DatUsuarios().crearModuloSic(modulo);
        }
        public static DataTable PerfilesCg()
        {
            return new DatUsuarios().PerfilesCg();
        }
        public static bool crearPerfilesCg(string grupo)
        {
            return new DatUsuarios().crearPerfilesCg(grupo);
        }
        public static bool editarPerfilCg(Int64 codgru, string desgru)
        {
            return new DatUsuarios().editarPerfilCg(codgru, desgru);
        }
        public static bool eliminarPerfilCg(Int64 codgru)
        {
            return new DatUsuarios().eliminarPerfilCg(codgru);
        }
        public static bool crearModuloCg(string modulo)
        {
            return new DatUsuarios().crearModuloCg(modulo);
        }
        public static bool editarModuloCg(Int64 codmod, string nommod)
        {
            return new DatUsuarios().editarModuloCg(codmod, nommod);
        }
        public static bool eliminarModuloCg(Int64 codmod)
        {
            return new DatUsuarios().eliminarModuloCg(codmod);
        }
        public static List<DtoUsuariosPerfilesSic> ListaSic()
        {
            return new DatUsuarios().ListaSic();
        }
        public static DataTable opcionesXModuloSic(Int64 codmod)
        {
            return new DatUsuarios().opcionesXModuloSic(codmod);
        }
        public static DataTable opcionesXModuloCg(Int64 codmod)
        {
            return new DatUsuarios().opcionesXModuloCg(codmod);
        }
        public static DataTable usuarioXCedulaCg(string cedula)
        {
            return new DatUsuarios().usuarioXCedulaCg(cedula);
        }
        public static DataTable usuarioXCedulaSic(string cedula)
        {
            return new DatUsuarios().usuarioXCedulaSic(cedula);
        }
        public static List<object> RecuperaListaUsuario()
        {
            return new DatUsuarios().RecuperaListaUsuario();
        }
    }

}
