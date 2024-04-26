using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics;
using His.General;
using System.Reflection;
using His.Parametros;
using His.DatosReportes;

namespace His.Admision.Datos
{
    public class CuentaPaciente
    {
        public static void AgregarCuentaPacientesT(ATENCIONES atencionNueva )
        {
            try
            {
                CUENTAS_PACIENTES_TOTALES cuentaPT = new CUENTAS_PACIENTES_TOTALES();
                cuentaPT.ATE_CODIGO = atencionNueva.ATE_CODIGO;
                cuentaPT.CPT_FECHA = atencionNueva.ATE_FECHA_INGRESO;
                cuentaPT.PAC_CODIGO = Convert.ToInt32(atencionNueva.PACIENTESReference.EntityKey.EntityKeyValues[0].Value);
                cuentaPT.CPT_TOTAL = 0;
                cuentaPT.CPT_IVA = 0;
                cuentaPT.ID_USUARIO = Convert.ToInt16(atencionNueva.USUARIOSReference.EntityKey.EntityKeyValues[0].Value);
                cuentaPT.ESC_CODIGO = 1;
                cuentaPT.CPT_ESTADO = true;
                NegCuentasPacientes.CrearCuentaPT(cuentaPT);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
