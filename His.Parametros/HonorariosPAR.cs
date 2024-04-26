using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public static class HonorariosPAR
    {
        #region Variables
        //Directorio de Pacientes
        private static string directorioPacientesFormularios = "Pacientes";

        //Directorio Microfilms
        private static string directorioMicrofilms = "Microfilms";

        //Codigo en la tabla tipo forma de pago para las tarjetas de credito
        static int[] codigoTarjetasCredito =new int[9] {5,6,7,8,10,11,12,13,14};
        static int codigoFormaPagoCheque = 2;
        static Int16 codigoFormaPagoHonorarioDirecto = 126;
        static Int16 codigoTipoHonorarioNoCobraComision = 4;
        static string codigoQuito = "1701";
        static Int16 tamanoHCL = 7;

        //Codigo de medico de la clinica
        static int codigoMedicoClinica = 9999;
        
        //Estados de los honorarios medicos

        private static Int16 honorarioCreado = 0;
        private static Int16 honorarioParcialmenteRecuperado = 1;
        private static Int16 honorarioRecuperado = 2;
        private static Int16 honorarioParcialmentoCancelado = 3;
        private static Int16 honorarioCancelado = 4;

        //Tipo de movimientos
        public static Int16 codigoTipoMovimientoHonorariosMedicos = 201;
        public static Int16 codigoTipoMovimientoHonorariosDirectos = 202;
        public static Int16 codigoTipoMovimientoNotaCredito = 101;
        public static Int16 codigoTipoMovimientoNotaDebito = 102;
        public static Int16 codigoTipoMovimientoNotaCreditoInterna = 105;
        public static Int16 codigoTipoMovimientoNotaDebitoInterna = 106;


        //Tipo de Documentos
        public static Int16 codigoTipoDocumentoFactura = 1;
        public static Int16 codigoTipoDocumentoNotaCredito = 2;
        public static Int16 codigoTipoDocumentoNotaDebito = 3;
        public static Int16 codigoTipoDocumentoRetencion = 4;
        public static Int16 codigoTipoDocumentoNotaDebitoInterna = 5;
        public static Int16 codigoTipoDocumentoNotaCreditoInterna = 6;
        public static Int16 codigoTipoDocumentoNdComisionesReferidos = 7;
        public static Int16 codigoTipoDocumentoNdValoresNoCubiertos = 8;


        //Tipo de Notas
        public static Int16 codigoTipoNotaCredito = 101;
        public static Int16 codigoTipoNotaDebito = 102;
        public static Int16 codigoTipoNotaDebitoComisionesYreferidos = 103;
        public static Int16 codigoTipoNotaDebitoValoresNoCubiertos = 104;
        public static Int16 codigoTipoNotaCreditoInterna = 105;
        public static Int16 codigoTipoNotaDebitoInterna = 106;


        //direccion del servidor SMTP
        private static string smtpHost ="mail.hcp.com.ec";
        /// <summary>
        /// ingressa y obtiene la direccion del servidor SMTP
        /// </summary>
        /// 

        //puerto del servidor SMTP
        static int smtpPuerto = 25;
        /// <summary>
        /// ingressa y obtiene el puerto del servidor SMTP
        /// </summary>
        /// 

        //si el servidor de Correo SMTP requiere autentificacion
        static bool smtpCredencial = false;
        /// <summary>
        /// se ingresa y obtiene el valor del estado de autentificacion del servidor SMTP
        /// </summary>
        /// 

        //cuenta de correo para envio de correos automaticos
        static string smtpCorreo = "direccionmedica@hcp.com.ec";
        /// <summary>
        /// ingressa y obtiene la cuenta de correo electronico para el envio de correos automaticos
        /// </summary>
        /// 

        //usuario de la  cuenta de correo para envio de correos automaticos
        static string smtpCorreoUsr;
        /// <summary>
        /// ingressa y obtiene la usuario de la cuenta de correo para envio automatico de correos
        /// </summary>

        //clave de la  cuenta de correo para envio de correos automaticos
        static string smtpCorreoPwd;
        /// <summary>
        /// ingressa y obtiene la clave de la cuenta de correo
        /// </summary>
        /// 

        #endregion

        #region Metodos get y set
        public static string SmtpDireccionHost{
            get { return smtpHost; }
            set { smtpHost = value; }
        }
        public static int SmtpPuerto {
            get {return smtpPuerto ;}
            set { smtpPuerto=value;} 
        }
        public static bool SmtpCredencial
        {
            get { return smtpCredencial; }
            set { smtpCredencial = value; }
        }
        public static string SmtpCorreo
        {
            get { return smtpCorreo; }
            set { smtpCorreo = value; }
        }
        
        public static string SmtpCorreoUsr
        {
            get { return smtpCorreoUsr; }
            set { smtpCorreoUsr = value; }
        }
        
        public static string SmtpCorreoPwd
        {
            get { return smtpCorreoPwd; }
            set { smtpCorreoPwd = value; }
        }
        public static int[] getCodigoTarjetasCredito()
        {
           return codigoTarjetasCredito; 
        }
        public static int getCodigoFormaPagoCheque()
        {
            return codigoFormaPagoCheque;
        }
        public static int getCodigoFPHonorarioDirecto()
        {
            return codigoFormaPagoHonorarioDirecto; 
        }
        public static int getCodigoTipoHonorarioNoCobraComision()
        {
            return codigoTipoHonorarioNoCobraComision;
        }
        public static string DirectorioFomulariosPacientes
        {
            get { return directorioPacientesFormularios; }
            set { directorioPacientesFormularios = value; }
        }
        public static string DirectorioMicrofilms
        {
            get { return directorioMicrofilms; }
            set { directorioMicrofilms = value; }
        }
        public static string CodigoQuito
        {
            get { return codigoQuito; }
            set { codigoQuito = value; }
        }
        public static Int16 TamanoHCL
        {
            get { return tamanoHCL; }
            set { tamanoHCL = value; }
        }

        public static Int16 HonorarioCreado
        {
            get { return honorarioCreado; }
            set { honorarioCreado = value; }
        }
        public static Int16 HonorarioParcialmenteRecuperado
        {
            get { return honorarioParcialmenteRecuperado; }
            set { honorarioParcialmenteRecuperado = value; }
        }
        public static Int16 HonorarioRecuperado
        {
            get { return honorarioRecuperado; }
            set { honorarioRecuperado = value; }
        }
        public static Int16 HonorarioParcialmenteCancelado
        {
            get { return HonorarioParcialmenteCancelado; }
            set { HonorarioParcialmenteCancelado = value; }
        }
        public static Int16 HonorarioCancelado
        {
            get { return honorarioCancelado; }
            set { HonorarioCancelado = value; }
        }
        public static Int32 CodigoMedicoClinica
        {
            get { return codigoMedicoClinica; }
            set { codigoMedicoClinica = value; }
        }
        #endregion
    }
}
