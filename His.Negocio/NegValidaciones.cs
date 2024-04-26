using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using His.Entidades.Clases;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace His.Negocio
{
    public class NegValidaciones
    {
        public static  int NUMERO_DE_PROVINCIAS = 24;
        public static  bool esCedulaValida(String ci)
        {
            if (ci.Length == 13)
            {
                int ultimo = Convert.ToInt32(ci.Substring(9, 1));
                int provincia = Convert.ToInt32(ci.Substring(0, 2));
                int tercero = Convert.ToInt32(ci.Substring(2, 1));
                bool valida = true;
                if (provincia <= 24 || provincia == 30)
                {
                    if (tercero >= 0 && tercero <= 5)
                    {
                        //Agrupo todos los pares y los sumo
                        int pares = Convert.ToInt32(ci.Substring(1, 1)) + Convert.ToInt32(ci.Substring(3, 1)) + Convert.ToInt32(ci.Substring(5, 1)) + Convert.ToInt32(ci.Substring(7, 1));
                        //Agrupo los impares, los multiplico por un factor de 2, si la resultante es > que 9 le restamos el 9 a la resultante
                        int numero1 = Convert.ToInt32(ci.Substring(0, 1));
                        numero1 = (numero1 * 2);
                        if (numero1 > 9)
                        {
                            numero1 = (numero1 - 9);
                        }
                        int numero3 = Convert.ToInt32(ci.Substring(2, 1));
                        numero3 = (numero3 * 2);
                        if (numero3 > 9)
                        {
                            numero3 = (numero3 - 9);
                        }

                        int numero5 = Convert.ToInt32(ci.Substring(4, 1));
                        numero5 = (numero5 * 2);
                        if (numero5 > 9)
                        {
                            numero5 = (numero5 - 9);
                        }
                        int numero7 = Convert.ToInt32(ci.Substring(6, 1));
                        numero7 = (numero7 * 2);
                        if (numero7 > 9)
                        {
                            numero7 = (numero7 - 9);
                        }
                        int numero9 = Convert.ToInt32(ci.Substring(8, 1));
                        numero9 = (numero9 * 2);
                        if (numero9 > 9)
                        {
                            numero9 = (numero9 - 9);
                        }

                        int impares = numero1 + numero3 + numero5 + numero7 + numero9;
                        //Suma total
                        int suma_total = (pares + impares);
                        int mo = suma_total % 10;
                        if (mo == 0)
                        {
                            if (ultimo != mo)
                            {
                                valida = false;
                                return valida;
                            }
                        }
                        else
                        {
                            mo = 10 - mo;
                            //MessageBox.Show("el resultado es: " + mo);
                            if (ultimo != mo)
                            {
                                valida = false;
                                return valida;
                            }
                        }
                    }
                    else if (tercero == 6)
                    {
                        //3,2,7,6,5,4,3,2
                        int ante = Convert.ToInt32(ci.Substring(8, 1));
                        int resultado = (Convert.ToInt32(ci.Substring(0, 1)) * 3) + (Convert.ToInt32(ci.Substring(1, 1)) * 2) +
                            (Convert.ToInt32(ci.Substring(2, 1)) * 7) + (Convert.ToInt32(ci.Substring(3, 1)) * 6) +
                            (Convert.ToInt32(ci.Substring(4, 1)) * 5) + (Convert.ToInt32(ci.Substring(5, 1)) * 4) +
                            (Convert.ToInt32(ci.Substring(6, 1)) * 3) + (Convert.ToInt32(ci.Substring(7, 1)) * 2);
                        int mod = resultado % 11;
                        if (mod == 0)
                        {
                            if (mod != ante)
                            {
                                valida = false;
                                return valida;
                            }
                        }
                        else
                        {
                            mod = 11 - mod;
                            if (mod != ante)
                            {
                                valida = false;
                                return valida;
                            }
                        }
                    }
                    else if (tercero == 9)
                    {
                        //4,3,2,7,6,5,4,3,2
                        int resultado = (Convert.ToInt32(ci.Substring(0, 1)) * 4) + (Convert.ToInt32(ci.Substring(1, 1)) * 3) +
                            (Convert.ToInt32(ci.Substring(2, 1)) * 2) + (Convert.ToInt32(ci.Substring(3, 1)) * 7) +
                            (Convert.ToInt32(ci.Substring(4, 1)) * 6) + (Convert.ToInt32(ci.Substring(5, 1)) * 5) +
                            (Convert.ToInt32(ci.Substring(6, 1)) * 4) + (Convert.ToInt32(ci.Substring(7, 1)) * 3) +
                            (Convert.ToInt32(ci.Substring(8, 1)) * 2);
                        int mod = resultado % 11;
                        if (mod == 0)
                        {
                            if (mod != ultimo)
                            {
                                valida = false;
                                return valida;
                            }
                        }
                        else
                        {
                            mod = 11 - mod;
                            if (mod != ultimo)
                            {
                                valida = false;
                                return valida;
                            }
                        }
                    }
                    else
                    {
                        valida = false;
                        return valida;
                    }
                }
                else
                {
                    valida = false;
                    return valida;
                }
                return valida;
            }
            else if (ci.Length == 10)
            {
                try
                {
                    int[] digitos = new int[10];
                    int resultado = 0;
                    string region;
                    int modulo = 10;
                    for (int aux = 0; aux <= 9; aux++)
                    {
                        digitos[aux] = int.Parse(ci[aux].ToString());
                    }
                    region = digitos[0].ToString() + digitos[1].ToString();
                    if (int.Parse(region.ToString()) <= 24 || int.Parse(region.ToString()) == 30)
                    {
                        for (int aux = 0; aux <= 8; aux += 2)
                        {
                            digitos[aux] *= 2;
                            if (digitos[aux] > 9)
                                digitos[aux] -= 9;
                        }
                        for (int aux = 0; aux <= 8; aux++)
                        {
                            resultado = resultado + digitos[aux];
                        }
                        modulo = resultado % 10;
                        if (modulo == 0)
                        {
                            if (modulo == digitos[9])
                                return true;
                            else
                                return false;
                        }
                        else
                        {
                            modulo = 10 - modulo;
                            if (modulo == digitos[9])
                                return true;
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }


        public static bool esTelefonoValido(String telf)
        {
            if (telf.Length == 9 || telf.Length==10)
            {
                if (telf.Substring(0, 1) != "0")
                    return false;
            }
            else
                return false;
            
            return true;
        }

        public static bool esTelefonoValidoCelular(String telf)
        {
            if (telf.Length == 10)
            {
                if (telf.Substring(0, 1) != "0")
                    return false;
            }
            else
                return false;
                       
            return true;
        }

        public static bool localAsignado()
        {
            try
            {
                //StreamReader objReader = new StreamReader("C:\\Archivos de programa\\GapSystem\\His3000\\Control.ini");
                //string sLine = "";
                //string[] conexion = new string[3];
                //ArrayList arrText = new ArrayList();
                //int i = 0;
                //while (sLine != null)
                //{
                //    sLine = objReader.ReadLine();
                //    if (sLine != null)
                //        arrText.Add(sLine);
                //}
                //objReader.Close();

                //foreach (string sOuput in arrText)
                //{
                //    conexion[i] = sOuput;
                //    i = i++;
                //    Console.WriteLine(sOuput);
                //}
                //Console.ReadLine();
                //if (conexion[0] != string.Empty && conexion[0]!=null)
                //{
                //    Sesion.codLocal =Int16.Parse(conexion[0]);
                //    return true;
                //}
                //else
        

                His.Parametros.ArchivoIni archivo = new His.Parametros.ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                if (archivo.IniReadValue("Caja", "codigo") == "0")
                {
                    return false;
                }
                else
                {
                    Sesion.codLocal = Int16.Parse(archivo.IniReadValue("Caja", "codigo"));
                    return true;
                }

            }
             catch(Exception err)
            {
                Console.Write(err.Message);
                return false;
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        public static void alzheimer()
        {
            /*Libera memoria/Giovanny Tapia/16/02/2013*/
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
        }

        //Cambios Edgar 20210529
        public static bool ValidarRuc(string ruc)
        {
            bool valido = true;
            int ultimo = Convert.ToInt32(ruc.Substring(9, 1));
            int provincia = Convert.ToInt32(ruc.Substring(0, 2));
            int tercero = Convert.ToInt32(ruc.Substring(2, 1));
            if (provincia <= 24 || provincia == 30)
            {
                if (tercero >= 0 && tercero <= 5)
                {
                    //Agrupo todos los pares y los sumo
                    int pares = Convert.ToInt32(ruc.Substring(1, 1)) + Convert.ToInt32(ruc.Substring(3, 1)) + Convert.ToInt32(ruc.Substring(5, 1)) + Convert.ToInt32(ruc.Substring(7, 1));
                    //Agrupo los impares, los multiplico por un factor de 2, si la resultante es > que 9 le restamos el 9 a la resultante
                    int numero1 = Convert.ToInt32(ruc.Substring(0, 1));
                    numero1 = (numero1 * 2);
                    if (numero1 > 9)
                    {
                        numero1 = (numero1 - 9);
                    }
                    int numero3 = Convert.ToInt32(ruc.Substring(2, 1));
                    numero3 = (numero3 * 2);
                    if (numero3 > 9)
                    {
                        numero3 = (numero3 - 9);
                    }

                    int numero5 = Convert.ToInt32(ruc.Substring(4, 1));
                    numero5 = (numero5 * 2);
                    if (numero5 > 9)
                    {
                        numero5 = (numero5 - 9);
                    }
                    int numero7 = Convert.ToInt32(ruc.Substring(6, 1));
                    numero7 = (numero7 * 2);
                    if (numero7 > 9)
                    {
                        numero7 = (numero7 - 9);
                    }
                    int numero9 = Convert.ToInt32(ruc.Substring(8, 1));
                    numero9 = (numero9 * 2);
                    if (numero9 > 9)
                    {
                        numero9 = (numero9 - 9);
                    }

                    int impares = numero1 + numero3 + numero5 + numero7 + numero9;
                    //Suma total
                    int suma_total = (pares + impares);
                    int mo = suma_total % 10;
                    if (mo == 0)
                    {
                        if (ultimo != mo)
                        {
                            MessageBox.Show("¡Ruc no Valida!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return valido = false;
                        }
                    }
                    else
                    {
                        mo = 10 - mo;
                        //MessageBox.Show("el resultado es: " + mo);
                        if (ultimo != mo)
                        {
                            MessageBox.Show("¡Ruc no Valida!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return valido = false;
                        }
                    }
                }
                else if (tercero == 6)
                {
                    //3,2,7,6,5,4,3,2
                    int ante = Convert.ToInt32(ruc.Substring(8, 1));
                    int resultado = (Convert.ToInt32(ruc.Substring(0, 1)) * 3) + (Convert.ToInt32(ruc.Substring(1, 1)) * 2) +
                        (Convert.ToInt32(ruc.Substring(2, 1)) * 7) + (Convert.ToInt32(ruc.Substring(3, 1)) * 6) +
                        (Convert.ToInt32(ruc.Substring(4, 1)) * 5) + (Convert.ToInt32(ruc.Substring(5, 1)) * 4) +
                        (Convert.ToInt32(ruc.Substring(6, 1)) * 3) + (Convert.ToInt32(ruc.Substring(7, 1)) * 2);
                    int mod = resultado % 11;
                    if (mod == 0)
                    {
                        if (mod != ante)
                        {
                            MessageBox.Show("¡Ruc no Valido!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return valido = false;
                        }
                    }
                    else
                    {
                        mod = 11 - mod;
                        if (mod != ante)
                        {
                            MessageBox.Show("¡Ruc no Valido!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return valido = false;
                        }
                    }
                }
                else if (tercero == 9)
                {
                    //4,3,2,7,6,5,4,3,2
                    int resultado = (Convert.ToInt32(ruc.Substring(0, 1)) * 4) + (Convert.ToInt32(ruc.Substring(1, 1)) * 3) +
                        (Convert.ToInt32(ruc.Substring(2, 1)) * 2) + (Convert.ToInt32(ruc.Substring(3, 1)) * 7) +
                        (Convert.ToInt32(ruc.Substring(4, 1)) * 6) + (Convert.ToInt32(ruc.Substring(5, 1)) * 5) +
                        (Convert.ToInt32(ruc.Substring(6, 1)) * 4) + (Convert.ToInt32(ruc.Substring(7, 1)) * 3) +
                        (Convert.ToInt32(ruc.Substring(8, 1)) * 2);
                    int mod = resultado % 11;
                    if (mod == 0)
                    {
                        if (mod != ultimo)
                        {
                            MessageBox.Show("¡Ruc no Valido!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return valido = false;
                        }
                    }
                    else
                    {
                        mod = 11 - mod;
                        if (mod != ultimo)
                        {
                            MessageBox.Show("¡Ruc no Valido!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return valido = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("¡Ruc no Valido!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return valido = false;
                }
            }
            else
            {
                MessageBox.Show("El " + provincia + " no existe como provincia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return valido = false;
            }
            return valido;
        }
    }
}
