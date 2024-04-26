using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesoModuloMedicos
    {
        #region Metodos
        public static bool explorador = false;
        public static bool explCertificadoMedico = false;
        public static bool explRecetaMedica = false;
        public static bool explHc = false;
        public static bool explCertificadoAsistencia = false;

        public static bool medicos = false;
        public static bool certificadoGeneral = false;
        public static bool certificadoCovid = false;
        public static bool certificadoAsistencia = false;
        public static bool certificadoRrecetaMedica = false;

        #endregion
        #region get y set
        public static bool Explorador
        {
            get { return explorador; }
            set { explorador = value; }
        }
        public static bool ExpCertificadoMedico
        {
            get { return explCertificadoMedico; }
            set { explCertificadoMedico = value; }
        }
        public static bool ExpRecetaMedica
        {
            get { return explRecetaMedica; }
            set { explRecetaMedica = value; }
        }
        public static bool ExpHc
        {
            get { return explHc; }
            set { explHc = value; }
        }
        public static bool ExpCertificadoAsistencia
        {
            get { return explCertificadoAsistencia; }
            set { explCertificadoAsistencia = value; }
        }

        public static bool Medicos
        {
            get { return medicos; }
            set { medicos = value; }
        }
        public static bool CertGeneral
        {
            get { return certificadoGeneral; }
            set { certificadoGeneral = value; }
        }
        public static bool CertCovid
        {
            get { return certificadoCovid; }
            set { certificadoCovid = value; }
        }
        public static bool CertAsistencia
        {
            get { return certificadoAsistencia; }
            set { certificadoAsistencia = value; }
        }
        public static bool CertRrecetaMedica
        {
            get { return certificadoRrecetaMedica; }
            set { certificadoRrecetaMedica = value; }
        }
        #endregion
    }
}
