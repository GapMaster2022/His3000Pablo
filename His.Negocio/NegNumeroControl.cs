using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;
using Core.Entidades;
using System.Data;

namespace His.Negocio
{
    public class NegNumeroControl
    {
        public static int RecuperaMaximoNumeroControl()
        {
            return new DatNumeroControl().RecuperaMaximoNumeroControl();
        }
        public static NUMERO_CONTROL RecuperarNumeroControlID(int codigoNumeroControl)
        {
            return new DatNumeroControl().RecuperaNumeroControlID(codigoNumeroControl);
        }
        public static List<NUMERO_CONTROL> RecuperaNumeroControl()
        {
            return new DatNumeroControl().RecuperaNumeroControl();
        }
        public static DataTable RecuperaNumeroControlPablo()
        {
            return new DatNumeroControl().RecuperaNumeroControlPablo();
        }               

        public static void CrearNumeroControl(NUMERO_CONTROL numerocontrol)
        {
            new DatNumeroControl().CrearNumeroControl(numerocontrol);
        }
        public static bool CreaControlConsulta(CONTROL_CONSULTA obj)
            {
            return new DatNumeroControl().CreaControlConsulta(obj);
        }
        public static void GrabarNumeroControl(NUMERO_CONTROL numerocontrolModificada, NUMERO_CONTROL numerocontrolOriginal)
        {
            new DatNumeroControl().GrabarNumeroControl(numerocontrolModificada, numerocontrolOriginal);
        }
        public static void EliminarNumeroControl(NUMERO_CONTROL numerocontrol)
        {
            new DatNumeroControl().EliminarNumeroControl(numerocontrol);
        }
        /// <summary>
        /// Consulta si el numero de control es automatico
        /// </summary>
        /// <param name="codigonumerocontrol">codigo del numero de control</param>
        /// <returns>bool</returns>
        public static bool NumerodeControlAutomatico(int codigonumerocontrol)
        {
            bool tipocontrol;
            NUMERO_CONTROL control = new DatNumeroControl().RecuperaNumeroControl().Where(cod => cod.CODCON == codigonumerocontrol).FirstOrDefault();
            if (control.TIPCON == "A")
                tipocontrol = true;
            else
                tipocontrol = false;
            return tipocontrol;
        }
        /// <summary>
        /// Optine el numero de control con el que trabajara y le mantiene ocupado
        /// </summary>
        /// <param name="codigonumerocontrol">codigo de numero de control</param>
        /// <returns>numero de control</returns>
        public static Int16 NumeroControlOptine(int codigonumerocontrol)
        {
            int contador = 1;
        revision:
            
            NUMERO_CONTROL control = new DatNumeroControl().RecuperaNumeroControl().Where(cod => cod.CODCON == codigonumerocontrol).FirstOrDefault();
            NUMERO_CONTROL controlOriginal = control.ClonarEntidad();
            if (control.OCUPADO == true)
            {
                if (contador < 100)
                {
                    contador++;
                    goto revision;
                }
                else
                    throw null;
            }
            else
            {
                control.OCUPADO = true;
                new DatNumeroControl().GrabarNumeroControl(control, controlOriginal);
                return Int16.Parse(control.NUMCON);
            }
        }
        /// <summary>
        /// Aumenta el numero de control y le libera
        /// </summary>
        /// <param name="codigonumerocontrol">codigo del numero de control</param>
        public static void LiberaNumeroControl(int codigonumerocontrol)
        {
            NUMERO_CONTROL control = new DatNumeroControl().RecuperaNumeroControl().Where(cod => cod.CODCON == codigonumerocontrol).FirstOrDefault();
            NUMERO_CONTROL controlOriginal = control.ClonarEntidad();
            control.OCUPADO = false;
            control.NUMCON = (Int64.Parse(control.NUMCON) + 1).ToString();
            new DatNumeroControl().GrabarNumeroControl(control, controlOriginal);
        }

        /// <summary>
        /// Optine el numero de control con el que trabajara y le mantiene ocupado
        /// </summary>
        /// <param name="codigonumerocontrol">codigo de numero de control</param>
        /// <returns>numero de control</returns>
        public static string NumeroControlOp(int codigonumerocontrol)
        {
        rev:
            NUMERO_CONTROL control = new DatNumeroControl().RecuperaNumeroControlID(codigonumerocontrol);
            NUMERO_CONTROL controlOriginal = control.ClonarEntidad();
            if (control.OCUPADO == true)
                goto rev;
            else
            {
                control.OCUPADO = true;
                new DatNumeroControl().GrabarNumeroControl(control, controlOriginal);
                return control.NUMCON;
            }

        }
        public static NUMERO_CONTROL OcuparNControl(int codcon)
        {
            return new DatNumeroControl().OcuparNControl(codcon);
        }
        public static bool LiberaNControl(int codcon)
        {
            return new DatNumeroControl().LiberoNControl(codcon);
        }

    }
}
