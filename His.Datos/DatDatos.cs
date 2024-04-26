using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Core.Datos;
using His.Entidades;

namespace His.Datos
{
    public class DatDatos
    {
        #region Consultas



        public DataSet RecuperarClientesDataSet()
        {
            DataSet datosc = new DataSet();
            FuenteDatos conexion = new FuenteDatos();
            List<object> parametros = new List<object>();
            parametros.Add(null);
            datosc = conexion.EjecutarDataSet("RecuperarUsuarios", parametros.ToArray());
            return datosc;

        }

        //public List<His.Entidades.Usuarios> RecuperarClientesClientes()
        //{
        //    List<His.Entidades.Usuarios> clientes = new List<His.Entidades.Usuarios>();
        //    FuenteDatos conexion = new FuenteDatos();
        //    clientes = conexion.EjecutarProceso<His.Entidades.Usuarios>("RecuperarUsuarios");
        //    return clientes;
        //}
       
       
        public DataSet RecuperaNumeroControl(int a)
        {
            DataSet datos = new DataSet();
            FuenteDatos conexion = new FuenteDatos();
            datos = conexion.EjecutarSql("Select * from numero_control where codcon=" + a + " ");
            return datos;
        }
        ///// <summary>
        ///// Verifica la existencia de un usuario
        ///// </summary>
        ///// <param name="procedimiento">Nombre del procedimiento</param>
        ///// <param name="usuario">datos del usuario</param>
        ///// <returns>dataset</returns>
        //public int ExisteUsuario(string procedimiento, Usuarios usuario)
        //{
        //    List<DtoUsuarios> codigosUsuario = new List<DtoUsuarios>();
        //    DataSet datosc = new DataSet();
        //    FuenteDatos conexion = new FuenteDatos();
        //    //llenamos los arguemtnos con los datos del usuario
        //    List<object> parametros = new List<object>();
        //    parametros.Add(usuario.USR);
        //    parametros.Add(usuario.PWD);

        //    codigosUsuario = conexion.EjecutarProceso<DtoUsuarios>(procedimiento, parametros.ToArray()).ToList();
        //    if (codigosUsuario.Count > 0)
        //        return codigosUsuario.FirstOrDefault().ID_USUARIO;
        //    else
        //        return 0;
        //}
        public DataSet ListaConsultaTablas(string consulta)
        {
            DataSet datos = new DataSet();
            FuenteDatos conexion = new FuenteDatos();
            datos = conexion.EjecutarSql(consulta);
            return datos;
        }
        public List<DtoAccesosPorPerfil> ListaConsultaTablasOpciones(int idModulo, int idPerfil)
        {
            List<DtoAccesoOpciones> datos = new List<DtoAccesoOpciones>();
            List<DtoAccesoOpciones> accesoPerfiles = new List<DtoAccesoOpciones>();
            FuenteDatos conexion = new FuenteDatos();
            List<object> parametros = new List<object>();
            parametros.Add(Int16.Parse(idModulo.ToString()));
         
            datos = conexion.EjecutarProceso<DtoAccesoOpciones>("sp_AccesoOpcionesConsulta", parametros.ToArray());
            parametros.Clear();
            parametros.Add(idPerfil);
           
            List<DtoAccesosPorPerfil> accesosPorPerfiles = new List<DtoAccesosPorPerfil>();
            accesoPerfiles = conexion.EjecutarProceso<DtoAccesoOpciones>("sp_AccesoPerfilesConsulta", parametros.ToArray());
            foreach (var opcion in datos)
            {
                DtoAccesosPorPerfil opcionPerfil = new DtoAccesosPorPerfil();
                opcionPerfil.ID_ACCESO = opcion.ID_ACCESO;
                opcionPerfil.DESCRIPCION = opcion.DESCRIPCION;
                if (accesoPerfiles.Where(perfil => perfil.ID_ACCESO == opcion.ID_ACCESO).FirstOrDefault() != null)
                    opcionPerfil.TIENEACCESO = true;
                else
                    opcionPerfil.TIENEACCESO = false;
                accesosPorPerfiles.Add(opcionPerfil);
            }
            return accesosPorPerfiles;
        }
       
        #endregion
        #region Afectaciones
        ///// <summary>
        ///// Inserta usuarios
        ///// </summary>
        ///// <param name="procedimiento">Nombre del procedimiento</param>
        ///// <param name="usuario">Datos del ususario</param>
        //public void InsertaUsuarios(string procedimiento, Usuarios usuario)
        //{
        //    FuenteDatos conexion = new FuenteDatos();
        //    //llenamos los argumentos con los datos del usuario
        //    List<object> parametros = new List<object>();
        //    parametros.Add(usuario.ID_USUARIO);
        //    parametros.Add(usuario.DEP_CODIGO);
        //    parametros.Add(usuario.NOMBRES);
        //    parametros.Add(usuario.APELLIDOS);
        //    parametros.Add(usuario.IDENTIFICACION);
        //    parametros.Add(usuario.FECHA_INGRESO);
        //    parametros.Add(usuario.FECHA_NACIMIENTO);
        //    parametros.Add(usuario.GENERO);
        //    parametros.Add(usuario.ESTADO_CIVIL);
        //    parametros.Add(usuario.DIRECCION);
        //    parametros.Add(usuario.ESTADO);
        //    parametros.Add(usuario.USR);
        //    parametros.Add(usuario.PWD);
        //    parametros.Add(usuario.LOGEADO);
        //    conexion.Ejecutar(procedimiento, parametros.ToArray());
        //}
        /// <summary>
        /// Modificar Usuarios
        /// </summary>
        /// <param name="procedimiento">Nombre del procedimiento</param>
        /// <param name="Args">Datos del usuario</param>
        //public void ModificaUsuarios(string procedimiento, Usuarios usuario)
        //{
        //    FuenteDatos conexion = new FuenteDatos();
        //    //llenamos los argumentos con los datos del usuario
        //    List<object> parametros = new List<object>();
        //    parametros.Add(usuario.ID_USUARIO);
        //    parametros.Add(usuario.DEP_CODIGO);
        //    parametros.Add(usuario.NOMBRES);
        //    parametros.Add(usuario.APELLIDOS);
        //    parametros.Add(usuario.IDENTIFICACION);
        //    parametros.Add(usuario.FECHA_NACIMIENTO);
        //    parametros.Add(usuario.GENERO);
        //    parametros.Add(usuario.ESTADO_CIVIL);
        //    parametros.Add(usuario.DIRECCION);
        //    parametros.Add(usuario.ESTADO);
        //    parametros.Add(usuario.USR);
        //    parametros.Add(usuario.PWD);
            
           

        //    conexion.Ejecutar(procedimiento, parametros.ToArray());
        //}
        /// <summary>
        /// Aumenta el numero de control
        /// </summary>
        /// <param name="a">codigo del numero de control</param>
        public void AumentaNumeroControl(int a)
        {
            FuenteDatos conexion = new FuenteDatos();
            conexion.Ejecutar("sp_AumentaNumeroControl", a);
        }
        /// <summary>
        /// inserta campos en una tabla cualquiera
        /// </summary>
        /// <param name="procedimiento">nombre del procedimiento</param>
        /// <param name="Args">argumentos del sp</param>
        public void InsertaenTabla(string procedimiento, List<object> Args)
        {
            FuenteDatos conexion = new FuenteDatos();
            conexion.Ejecutar(procedimiento, Args.ToArray());
        }

        #endregion
    }
}
