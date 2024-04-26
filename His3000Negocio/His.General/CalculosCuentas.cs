using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.General
{
   public class CalculosCuentas
    {
        public static Double CalcularIVA(string ivaTotal) 
        {
            Double totalIva = Convert.ToDouble(ivaTotal);
            if (totalIva > 0)
            {
                string total = Convert.ToString(ivaTotal.Trim());
                int tamaño = total.Length;
                int posicion = total.IndexOf(".");
                Double numD1 = Math.Round(totalIva, 2);
                totalIva = Convert.ToDouble(numD1);
                //Double numD = Convert.ToDouble(ivaTotal.Substring(0, posicion + 3));
                //string cadena = total.Substring(posicion + 1, (total.Length - (posicion + 1)));
                //if (posicion > 0 && cadena.Length > 2)
                //{
                //    int v = Convert.ToInt32(ivaTotal.Substring(posicion + 3, 1));
                //    if (Convert.ToInt32(ivaTotal.Substring(posicion + 3, 1)) >5)
                //    {
                //        //string cadena = numD.ToString();
                //        int v1 = 0;
                //        int v2 = 0;
                //        int v3 = 0;
                //        for (int i = cadena.Length; i > 2; i--)
                //        {
                //            v1 = Convert.ToInt32(cadena.Substring(i - 1, 1)) + v2;
                //            if (v1 > 4)
                //            {
                //                v2 = 1;
                //            }
                //            else
                //            {
                //                v2 = 0;
                //            }
                //            v1 = 0;
                //        }
                //        if ((v2 > 0))
                //        {
                //            totalIva = Convert.ToDouble(numD) + 0.01;
                //        }
                //        else
                //        {
                //            totalIva = Convert.ToDouble(numD);
                //        }
                //    }
                //    totalIva = Convert.ToDouble(numD);
                //}
            }        
            return totalIva;
        }


        public static Double CalcularRedondeo(string totalV)
                {
            Double totalIva = Convert.ToDouble(totalV);
            if (totalIva > 0)
            {
                string total = Convert.ToString(totalV.Trim());
                int tamaño = total.Length;
                int posicion = total.IndexOf(".");
                Double numD1 = Math.Round(totalIva, 3);
                totalIva = Convert.ToDouble(numD1);                
                string cadena = total.Substring(posicion + 1, (total.Length - (posicion + 1)));
                if (posicion > 0 && cadena.Length > 3)
                {
                    Double numD = Convert.ToDouble(totalV.Substring(0, posicion + 4));
                    int v = Convert.ToInt32(totalV.Substring(posicion + 4, 1)); // TOMA EL ULTIMO VALOR DE LA CADENA
                    if (Convert.ToInt32(totalV.Substring(posicion + 4, 1)) > 4) // VERIFICA EL ULTIMO NUMERO DE LA CADENA SI ES MAYOR A 4
                        {
                        //string cadena = numD.ToString();
                        int v1 = 0;
                        int v2 = 0;
                        int v3 = 0;
                        for (int i = cadena.Length; i > 3; i--) // VERIFICA QUE LA LONGITUD DE LOS DECIMALES DE UN NUMERO SEA MAYOR A 3
                        {
                            v1 = Convert.ToInt32(cadena.Substring(i - 1, 1)) + v2;
                            if (v1 > 4)
                            {
                                v2 = 1;
                            }
                            else
                            {
                                v2 = 0;
                            }
                            v1 = 0;
                        }
                            if ((v2 > 0))
                        {
                            totalIva = Convert.ToDouble(numD) + 0.001;
                        }
                        else
                        {
                            totalIva = Convert.ToDouble(numD);
                        }
                    }
                    else
                        totalIva = Convert.ToDouble(numD);
                }
            }
            return totalIva;
        }

    }
}
