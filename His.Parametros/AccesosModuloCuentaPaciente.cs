using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Parametros
{
    public class AccesosModuloCuentaPaciente
    {
        #region Metodos
        public static bool facturacion = false;
        public static bool nuevaFactura = false;
        public static bool divisionCuentas = false;
        public static bool revisionCuentas = false;
        public static bool informe = false;
        public static bool cierreTurno = false;
        public static bool cambioCuenta = false;
        public static bool valoresCuenta = false;
        public static bool expAuditoria = false;
        public static bool garantias = false;
        public static bool nuevaGarantia = false;
        public static bool preAutorizacion = false;
        public static bool reporte = false;
        public static bool auditoria = false;
        public static bool detalleAtencion = false;
        public static bool estadoCuenta = false;
        #endregion
        #region get y set
        public static bool Facturacion
        {
            get { return facturacion; }
            set { facturacion = value; }
        }
        public static bool NuevaFactura
        {
            get { return nuevaFactura; }
            set { nuevaFactura = value; }
        }
        public static bool DivisionCuentas
        {
            get { return divisionCuentas; }
            set { divisionCuentas = value; }
        }
        public static bool RevisionCuentas
        {
            get { return revisionCuentas; }
            set { revisionCuentas = value; }
        }
        public static bool Informe
        {
            get { return informe; }
            set { informe = value; }
        }
        public static bool CierreTurno
        {
            get { return cierreTurno; }
            set { cierreTurno = value; }
        }
        public static bool CambioCuenta
        {
            get { return cambioCuenta; }
            set { cambioCuenta = value; }
        }
        public static bool ValoresCuenta
        {
            get { return valoresCuenta; }
            set { valoresCuenta = value; }
        }
        public static bool ExpAuditoria
        {
            get { return expAuditoria; }
            set { expAuditoria = value; }
        }
        public static bool Garantias
        {
            get { return garantias; }
            set { garantias = value; }
        }
        public static bool NuevaGarantia
        {
            get { return nuevaGarantia; }
            set { nuevaGarantia = value; }
        }
        public static bool PreAutorizacion
        {
            get { return preAutorizacion; }
            set { preAutorizacion = value; }
        }
        public static bool Reporte
        {
            get { return reporte; }
            set { reporte = value; }
        }
        public static bool Auditoria
        {
            get { return auditoria; }
            set { auditoria = value; }
        }
        public static bool DetalleAtencion
        {
            get { return detalleAtencion; }
            set { detalleAtencion = value; }
        }
        public static bool EstadoCuenta
        {
            get { return estadoCuenta; }
            set { estadoCuenta = value; }
        }
        #endregion
    }
}
