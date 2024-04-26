using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using His.Entidades;
using His.Datos;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace His.Negocio
{
    public class NegUtilitarios
    {
        public static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public static void OnlyNumberDecimal(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789." + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        public static void OnlyHora(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789:" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static bool ValidaTemperatura(decimal valor)
        {
            Int64 vminimo = NegParametros.RecuperaValorParSvXcodigo(60);
            Int64 vmaximo = NegParametros.RecuperaValorParSvXcodigo(61);
            if (vmaximo == 0)
                vmaximo = 1000;
            if (valor >= vminimo && valor <= vmaximo)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Temperatura no puede ser menor de "+vminimo+ "° ni mayor de " + vmaximo+"°", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public static bool ValidaPrecion1(double valor)
        {
            Int64 vmaximo = NegParametros.RecuperaValorParSvXcodigo(55);
            if (vmaximo == 0)
                vmaximo = 1000;
            if (valor >= 0 && valor <= vmaximo)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Presión 1 no puede ser mayor de "+vmaximo , "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        public static bool ValidaPrecion2(double valor)
        {
            Int64 vmaximo = NegParametros.RecuperaValorParSvXcodigo(54);
            if (vmaximo == 0)
                vmaximo = 1000;
            if (valor >= 0 && valor <= vmaximo)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Presión 2 no puede ser mayor de " + vmaximo , "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public static bool ValidaFcardiaca(double valor)
        {
            Int64 vminimo = NegParametros.RecuperaValorParSvXcodigo(56);
            Int64 vmaximo = NegParametros.RecuperaValorParSvXcodigo(57);
            if (vmaximo == 0)
                vmaximo = 1000;
            if (valor >= vminimo && valor <= vmaximo)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Frecuencia Cardiaca no puede ser \r\n menor de "+vminimo+ " ni mayor de " + vmaximo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public static bool ValidaFrespiratoria(double valor)
        {
            Int64 vminimo = NegParametros.RecuperaValorParSvXcodigo(58);
            Int64 vmaximo = NegParametros.RecuperaValorParSvXcodigo(59);
            if (vmaximo == 0)
                vmaximo = 1000;
            if (valor >= vminimo && valor <= vmaximo)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Frecuencia Respiratoria no puede ser \r\n menor de "+vminimo+ " ni mayor de " + vmaximo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public static bool ValidaSatOxigeno(double valor)
        {
            Int64 vminimo = NegParametros.RecuperaValorParSvXcodigo(62);
            Int64 vmaximo = NegParametros.RecuperaValorParSvXcodigo(63);
            if (vmaximo == 0)
                vmaximo = 1000;
            if (valor >= vminimo && valor <= vmaximo)
            {
                return true;
            }
            else
            {
                MessageBox.Show("La Saturacion de Oxigeno no puede ser \r\n menor de " + vminimo + " ni mayor de " + vmaximo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public static Int64 validaDiasEpicrisis(double valor)
        {
            Int64 dias = NegParametros.RecuperaValorParSvXcodigo(64);
            return dias;
        }
        public static bool validadorEmail(string correo)
        {
            bool ok = false;
            try
            {
                String expresion;
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(correo, expresion))
                {
                    if (Regex.Replace(correo, expresion, String.Empty).Length == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }
                }
                else
                {
                    ok = false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ok;
        }

        public static List<DtoParametros> RecuperaInformacionCorreo()
        {
            return new DatParametros().RecuperaParametros(20);
        }

        public static string RutaLogo(string tipo)
        {
            return new DatParametros().RutaLogo(tipo);
        }
        public static DataTable Congregaciones(int tipo)
        {
            return new DatParametros().Congregaciones(tipo);
        }
        public static bool ParametroAuditoria()
        {
            return new DatParametros().ParametroAuditoria();
        }

        public static int EdadCalculada(DateTime dtp_fecnac, DateTime fechaAtencion)
        {
            if (dtp_fecnac == DateTime.Now.Date)//valida fecha de naciemiento si tiene fecha de hoy no validará el resto de informacion
                return 0;
            DateTime actual = fechaAtencion;
            DateTime nacido = dtp_fecnac;

            int edadAnos = actual.Year - nacido.Year;
            if (actual.Month < nacido.Month || (actual.Month == nacido.Month && actual.Day < nacido.Day))
                edadAnos--;

            int edadMeses = actual.Month - nacido.Month;
            if (actual.Day < nacido.Day)
                edadMeses--;
            if (edadMeses < 0)
                edadMeses += 12;

            int diaActual = actual.Day;
            int diaCumple = nacido.Day;
            int diasDiferencia = diaActual - diaCumple;
            if (diasDiferencia < 0)
            {
                //edadMeses -= 1;
                diasDiferencia += DateTime.DaysInMonth(actual.Year, actual.Month);
            }
            return edadAnos;
        }

        public static bool IpBodegas()
        {
            string hostName = Dns.GetHostName();
            string ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();

            /***********************************************************************/
            DataTable PisoBodega = new DataTable();
            PisoBodega = NegHabitaciones.RecuperarPisoBodega(ipaddress);
            try
            {
                His.Entidades.Clases.Sesion.bodega = Convert.ToInt32(PisoBodega.Rows[0][3].ToString());
                His.Entidades.Clases.Sesion.ip = ipaddress;
                His.Entidades.Clases.Sesion.bodega_reposicion = Convert.ToInt32(PisoBodega.Rows[0][4].ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static RegistroCivil ObtenerRegistroCivil(string cedula)
        {
            PARAMETROS_DETALLE pathWebServices = NegParametros.RecuperaPorCodigo(49);
            string path = pathWebServices.PAD_VALOR;
            var url = path + $"services/cedulaV2/{cedula}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            RegistroCivil reg = new RegistroCivil();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                reg = JsonConvert.DeserializeObject<RegistroCivil>(responseBody);
                            }
                    }
                }
                return reg;
            }
            catch (WebException ex)
            {
                // Handle error
                return null;
            }

        }
        public static RUC ObtenerRUC(string ruc)
        {
            PARAMETROS_DETALLE pathWebServices = NegParametros.RecuperaPorCodigo(49);
            string path = pathWebServices.PAD_VALOR;
            var url = path + $"services/ruc/{ruc}";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            RUC reg = new RUC();
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader != null)
                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                string responseBody = objReader.ReadToEnd();
                                return JsonConvert.DeserializeObject<RUC>(responseBody);
                            }
                    }
                }
                return null;
                
            }
            catch (WebException ex)
            {
                // Handle error
                return null;
            }

        }

        public static ValidaNube ValidarDocumento(string identificacion)
        {
            try
            {
                PARAMETROS_DETALLE pathWebServices = NegParametros.RecuperaPorCodigo(49);
                string path = pathWebServices.PAD_VALOR;
                var url = "";
                if (identificacion.Length == 10)
                {
                    url = path + $"services/validacedula/{identificacion}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";
                    try
                    {
                        using (WebResponse response = request.GetResponse())
                        {
                            using (Stream strReader = response.GetResponseStream())
                            {
                                if (strReader != null)
                                {
                                    using (StreamReader objReader = new StreamReader(strReader))
                                    {
                                        string responseBody = objReader.ReadToEnd();
                                        return JsonConvert.DeserializeObject<ValidaNube>(responseBody);
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {

                        return null;
                    }
                }
                else 
                {
                    url = path + $"services/validaRuc/{identificacion}";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";
                    try
                    {
                        using (WebResponse response = request.GetResponse())
                        {
                            using (Stream strReader = response.GetResponseStream())
                            {
                                if (strReader != null)
                                {
                                    using (StreamReader objReader = new StreamReader(strReader))
                                    {
                                        string responseBody = objReader.ReadToEnd();
                                        return JsonConvert.DeserializeObject<ValidaNube>(responseBody);
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }

                    }
                    catch (Exception)
                    {

                        return null;
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }
        public static List<ACCESO_OPCIONES> ListaAccesoOpcionesPorPerfil(Int32 codigoPerfil, Int32 codigoModulo)
        {
            return new DatAccesoOpciones().ListaAccesoOpcionesPorPerfil(codigoPerfil, codigoModulo);
        }
    }
}
