using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
//using Recursos;

namespace His.Parametros
{
    public static class GeneralPAR
    {
        #region Variables

        //Shortcuts, tclas de funciones
        private static Keys teclaAyuda = Keys.F1;
        private static Keys teclaNuevo = Keys.F2;
        private static Keys teclaEditar = Keys.F3;
        private static Keys teclaEliminar = Keys.F4;
        private static Keys teclaGuardar = Keys.F5;
        private static Keys teclaCancelar = Keys.F6;
        private static Keys teclaTabular = Keys.Enter;
        private static int tamNumAtencion = 7;
        private static int tamHCL = 7;
        //Codigos de tablas generales
        private static Int16 codigoTipoEmpresaAseguradora = 1;
        private static Int16 codigoTipoEmpresaInstitucion = 2;
        private static Int16 codigoAreaFarmacia = 1;
        //Formatos
        private static string formatoDate = "yyyy-MM-dd";
        private static string formatoFecha = "dd-MM-yyyy";
        private static string formatoDateTime = "yyyy-MM-dd HH:mm:ss";
        //Directorio de Formularios
        private static string directorioMatrizFormularios = "FormulariosHCU";
        private static string directorioPacientesFormularios = "Pacientes";
        private static string directorioMicrofilms = "Microfilms";
        private static string directorioTemporales = "Temp";
        private static string directorioPedidos = "Pedidos/Activos";
        //Datos del servidor FTP
        private static string servidorFTP = "10.10.1.103";
        private static string usuarioFTP = "gap";
        private static string claveFTP = "Gapgr2011";

        #endregion

        #region Metodos get y set

        ///<summary>
        /// Retorna e Ingresa el directorio de archivos temporales del sistema His3000
        /// </summary>
        public static string DirectorioPedidos
        {
            get { return directorioPedidos; }
            set { directorioPedidos = value; }
        }
        ///<summary>
        /// Retorna el directorio de archivos temporales del sistema His3000
        /// </summary>
        public static string getDirectorioTemporales()
        {
            return directorioTemporales;
        }

        /// <summary>
        /// Retorna el directorio del servidor ftp donde se guardan los formularios de Historias Clinicas
        /// </summary>
        public static string getDirectorioMatrizFormularios()
        {
            return directorioMatrizFormularios;
        }
        /// <summary>
        /// Retorna el directorio del servidor ftp donde se guardan los formularios de Historias Clinicas por Paciente
        /// </summary>
        public static string getDirectorioPacientesFormularios()
        {
            return directorioPacientesFormularios;
        }
        /// <summary>
        /// Retorna e Ingresa el directorio del servidor ftp donde se guardan los microfilms
        /// </summary>
        public static string DirectorioMicrofilms
        {
            get { return directorioMicrofilms; }
            set { directorioMicrofilms = value; }
        }


        public static string getServidorFTP()
        {
            return servidorFTP;
        }
        public static string getUsuarioFTP()
        {
            return usuarioFTP;
        }
        public static string getClaveFTP()
        {
            return claveFTP;
        }

        /// <summary>
        /// Ingresa el directorio donde se guardaran los formularios de Historias Clinicas
        /// </summary>
        /// <param name="directorio">directorio del servidor ftp donde se ubican los formularios</param>
        public static void setDirectorioMatrizFormulario(string directorio)
        {
            directorioMatrizFormularios = directorio;
        }
        /// <summary>
        /// Ingresa el directorio donde se guardaran los formularios de Historias Clinicas por Pacientes
        /// </summary>
        /// <param name="directorio">directorio del servidor ftp donde se ubican los formularios</param>
        public static void setDirectorioPacientesFormularios(string directorio)
        {
            directorioPacientesFormularios = directorio;
        }

        //parametros servidor ftp
        public static void getServidorFTP(string nameServidor)
        {
            servidorFTP = nameServidor;
        }
        public static void getUsuarioFTP(string usrServidor)
        {
            usuarioFTP = usrServidor;
        }
        public static void getClaveFTP(string pwdServidor)
        {
            claveFTP = pwdServidor;
        }

        public static Int16 CodigoTipoEmpresaAseguradora
        {
            get { return codigoTipoEmpresaAseguradora; }
            set { codigoTipoEmpresaAseguradora = value; }
        }

        public static Int16 CodigoTipoEmpresaInstitucion
        {
            get { return codigoTipoEmpresaInstitucion; }
            set { codigoTipoEmpresaInstitucion = value; }
        }

        public static Keys TeclaAyuda
        {
            get { return teclaAyuda; }
            set { teclaAyuda = value; }
        }

        public static Keys TeclaNuevo
        {
            get { return teclaNuevo; }
            set { teclaNuevo = value; }
        }

        public static Keys TeclaEditar
        {
            get { return teclaEditar; }
            set { teclaEditar = value; }
        }

        public static Keys TeclaEliminar
        {
            get { return teclaEliminar; }
            set { teclaEliminar = value; }
        }

        public static Keys TeclaGuardar
        {
            get { return teclaGuardar; }
            set { teclaGuardar = value; }
        }

        public static Keys TeclaCancelar
        {
            get { return teclaCancelar; }
            set { teclaCancelar = value; }
        }

        public static Keys TeclaTabular
        {
            get { return teclaTabular; }
            set { teclaTabular = value; }
        }

        public static int TamNumAtencion
        {
            get { return tamNumAtencion; }
            set { tamNumAtencion = value; }
        }
        public static int TamHCL
        {
            get { return tamHCL; }
            set { tamHCL = value; }
        }
        public static string FormatoDate
        {
            get { return formatoDate; }
            set { formatoDate = value; }
        }
        public static string FormatoDateTime
        {
            get { return formatoDateTime; }
            set { formatoDateTime = value; }
        }
        public static Int16 CodigoAreaFarmacia
        {
            get { return codigoAreaFarmacia; }
            set { codigoAreaFarmacia = value; }
        }
        #endregion
    }
}
