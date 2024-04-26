using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class CuentasPacientes
    {
        //DEPENDENCIAS SEGURO IESS
        private static int codigoDependencia = 39;
        //TIPO SEGURO IESS
        private static int codigoTipoSeguro = 38;
        //41	TIPO EXAMEN - SEGURO
        private static int codigoTipoExamen = 41;
        //42	MAESTRO DERIVACION
        private static int codigoDerivacion = 42;
        //43	CONTINGENCIA CUBIERTA
        private static int codigoContingencia = 43;
        //40	PARENTESCO
        private static int codigoParentesco = 40;
        //44	TIPO DIAGNOSTICO
        private static int codigoDiagnostico = 44;

        //Código Areas

        private static int codigoFarmacia = 3;
        private static int codigoHospitalarios = 1;
        private static int codigoAlimentacion = 2;
        private static int codigoLaboratorio = 4;
        private static int codigoLaboratorioP = 4;
        private static int codigoImagen = 6;
        private static int codigoQuirofano = 7;
        private static int codigoEmergencia	= 8;
        private static int codigoNeonatologia= 9;
        private static int codigoSalaPartos	= 10;
        private static int codigoTerapiaI = 11;
        private static int codigoTerapiaR = 12;
        private static int codigoServicosI = 13;
        private static int codigoEquipos = 14;
        private static int codigoElectros = 15;
        private static int codigoHonorarios = 16;
        private static int codigoOtrosP = 20;
        public static Boolean ContinuaFacturacion = true;


        //Códigos SUBDIVISION

        private static int codigoLaboratorioClinico = 411;

        //ESTADOS DE CUENTA
        private static int estadoCuentaCancelada = 3;

        //RUBROS
        private static int rubroAnestesia = 23;
        private static int rubroSuministros = 27;
        private static int rubroOtrosP = 29;

        //Suministro
        private static String codigoSuministros = "CS001";

        public static int CodigoDependencia
        {
            get { return codigoDependencia; }
            set { codigoDependencia = value; }
        }
        public static int CodigoTipoSeguro
        {
            get { return codigoTipoSeguro; }
            set { codigoTipoSeguro = value; }
        }
        public static int CodigoTipoExamen
        {
            get { return codigoTipoExamen; }
            set { codigoTipoExamen = value; }
        }
        public static int CodigoDerivacion
        {
            get { return codigoDerivacion; }
            set { codigoDerivacion = value; }
        }
        public static int CodigoContingencia
        {
            get { return codigoContingencia; }
            set { codigoContingencia = value; }
        }

        public static int CodigoParentesco
        {
            get { return codigoParentesco; }
            set { codigoParentesco = value; }
        }

        public static int CodigoDiagnostico
        {
            get { return codigoDiagnostico; }
            set { codigoDiagnostico = value; }
        }


        //AREAS

        public static int CodigoFarmacia
        {
            get { return codigoFarmacia; }
            set { codigoFarmacia = value; }
        }

        public static int CodigoHospitalarios
        {
            get { return codigoHospitalarios; }
            set { codigoHospitalarios = value; }
        }
        public static int CodigoAlimentacion
        {
            get { return codigoAlimentacion; }
            set { codigoAlimentacion = value; }
        }
        public static int CodigoLaboratorio
        {
            get { return codigoLaboratorio; }
            set { codigoLaboratorio = value; }
        }
        public static int CodigoLaboratorioP
        {
            get { return codigoLaboratorioP; }
            set { codigoLaboratorioP = value; }
        }
        public static int CodigoImagen
        {
            get { return codigoImagen; }
            set { codigoImagen = value; }
        }
        public static int CodigoQuirofano
        {
            get { return codigoQuirofano; }
            set { codigoQuirofano = value; }
        }

        public static int CodigoEmergencia
        {
            get { return codigoEmergencia; }
            set { codigoEmergencia = value; }
        }

        public static int CodigoNeonatologia
        {
            get { return codigoNeonatologia; }
            set { codigoNeonatologia = value; }
        }
        public static int CodigoSalaPartos
        {
            get { return codigoSalaPartos; }
            set { codigoSalaPartos = value; }
        }
        public static int CodigoTerapiaI
        {
            get { return codigoTerapiaI; }
            set { codigoTerapiaI = value; }
        }
        public static int CodigoTerapiaR
        {
            get { return codigoTerapiaR; }
            set { codigoTerapiaR = value; }
        }
        public static int CodigoServicosI
        {
            get { return codigoServicosI; }
            set { codigoServicosI = value; }
        }

        public static int CodigoEquipos
        {
            get { return codigoEquipos; }
            set { codigoEquipos = value; }
        }

        public static int CodigoElectros
        {
            get { return codigoElectros; }
            set { codigoElectros = value; }
        }
        public static int CodigoHonorarios
        {
            get { return codigoHonorarios; }
            set { codigoHonorarios = value; }
        }
        public static int CodigoOtrosP
        {
            get { return codigoOtrosP; }
            set { codigoOtrosP = value; }
        }

   
        public static int EstadoCuentaCancelada
        {
            get { return estadoCuentaCancelada; }
            set { estadoCuentaCancelada = value; }
        }


        public static int CodigoLaboratorioClinico
        {
            get { return codigoLaboratorioClinico; }
            set { codigoLaboratorioClinico = value; }
        }


        public static int RubroAnestesia
        {
            get { return rubroAnestesia; }
            set { rubroAnestesia = value; }
        }

        public static int RubroSuministros
        {
            get { return rubroSuministros; }
            set { rubroSuministros = value; }
        }

        public static int RubroOtrosP
        {
            get { return rubroOtrosP; }
            set { rubroOtrosP = value; }
        }


        //private static String codigoSuministros = "CS001";

        public static string CodigoSuministros
        {
            get { return codigoSuministros; }
            set { codigoSuministros = value; }
        }
    }
}
