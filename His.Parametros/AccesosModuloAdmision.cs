using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public static class AccesosModuloAdmision
    {
        //Se cambia por completo la estrucura para manejar seguridades // Mario Valencia // 30/10/2023
        #region Variables

        private static bool archivo = false;
        private static bool check = false;
        private static bool formulario = false;
        private static bool salir = false;
        private static bool admision = false;
        private static bool admEmergenciaM = false;
        private static bool admEmergencia = false;
        private static bool serviciosExternos = false;
        private static bool preIngreso = false;
        private static bool estadistica = false;
        private static bool controlHc = false;
        private static bool explorador = false;
        private static bool pacientes = false;
        private static bool atenciones = false;
        private static bool cuentasFacturar = false;
        private static bool habitaciones = false;
        private static bool hc = false;
        private static bool rubros = false;
        private static bool expProcediminetos = false;
        private static bool expProcRubros = false;
        private static bool reportes = false;
        private static bool garantias = false;
        private static bool inec = false;
        private static bool censoDiario = false;
        private static bool censoSe = false;
        private static bool solicitudHc = false;
        private static bool cierreTurno = false;
        private static bool rangoEdades = false;
        private static bool defunciones = false;
        private static bool rucCi = false;
        private static bool laboratorio = false;
        private static bool tarifario = false;
        #endregion

        #region Metodos get y set

        public static bool Archivo
        {
            get
            {
                return archivo;
            }
            set
            {
                archivo = value;
            }
        }

        public static bool Check
        {
            get
            {
                return check;
            }
            set
            {
                check = value;
            }
        }

        public static bool Formulario
        {
            get
            {
                return formulario;
            }
            set
            {
                formulario = value;
            }
        }

        public static bool Salir
        {
            get
            {
                return salir;
            }
            set
            {
                salir = value;
            }
        }

        public static bool Admision
        {
            get
            {
                return admision;
            }
            set
            {
                admision = value;
            }

        }

        public static bool AdmEmergenciaM
        {
            get
            {
                return admEmergenciaM;
            }
            set
            {
                admEmergenciaM = value;
            }

        }

        public static bool AdmEmergencia
        {
            get
            {
                return admEmergencia;
            }
            set
            {
                admEmergencia = value;
            }

        }

        public static bool ServiciosExtermos
        {
            get
            {
                return serviciosExternos;
            }
            set
            {
                serviciosExternos = value;
            }

        }

        public static bool PreIngreso
        {
            get
            {
                return preIngreso;
            }
            set
            {
                preIngreso = value;
            }
        }

        public static bool Estadistica
        {
            get
            {
                return estadistica;
            }
            set
            {
                estadistica = value;
            }
        }

        public static bool ControlHc
        {
            get
            {
                return controlHc;
            }
            set
            {
                controlHc = value;
            }
        }

        public static bool Explorador
        {
            get
            {
                return explorador;
            }
            set
            {
                explorador = value;
            }
        }

        public static bool Pacientes
        {
            get
            {
                return pacientes;
            }
            set
            {
                pacientes = value;
            }
        }

        public static bool Atenciones
        {
            get
            {
                return atenciones;
            }
            set
            {
                atenciones = value;
            }
        }

        public static bool CuentaFacturada
        {
            get
            {
                return cuentasFacturar;
            }
            set
            {
                cuentasFacturar = value;
            }
        }

        public static bool Habitaciones
        {
            get
            {
                return habitaciones;
            }
            set
            {
                habitaciones = value;
            }
        }

        public static bool Hc
        {
            get
            {
                return hc;
            }
            set
            {
                hc = value;
            }
        }

        public static bool Rubros
        {
            get { return rubros; }
            set { rubros = value; }
        }
        public static bool ExplProcedimientos
        {
            get { return expProcediminetos; }
            set { expProcediminetos = value; }
        }
        public static bool ExpProcRubros
        {
            get { return expProcRubros; }
            set { expProcRubros = value; }
        }
        public static bool Reportes
        {
            get { return reportes; }
            set { reportes = value; }
        }
        public static bool Garantias
        {
            get { return garantias; }
            set { garantias = value; }
        }
        public static bool Inec
        {
            get { return inec; }
            set { inec = value; }
        }
        public static bool CensoDiario
        {
            get { return censoDiario; }
            set { censoDiario = value; }
        }
        public static bool CensoSxe
        {
            get { return censoSe; }
            set { censoSe = value; }
        }
        public static bool SolicitudHc
        {
            get { return solicitudHc; }
            set { solicitudHc = value; }
        }
        public static bool CierreTurno
        {
            get { return cierreTurno; }
            set { cierreTurno = value; }
        }
        public static bool RangoEdades
        {
            get { return rangoEdades; }
            set { rangoEdades = value; }
        }
        public static bool Defunciones
        {
            get { return defunciones; }
            set { defunciones = value; }
        }
        public static bool RucCi
        {
            get { return rucCi; }
            set { rucCi = value; }
        }
        public static bool Laboratorio
        {
            get { return laboratorio; }
            set { laboratorio = value; }
        }
        public static bool Tarifario
        {
            get { return tarifario; }
            set { tarifario = value; }
        }



        #endregion
    }
}
