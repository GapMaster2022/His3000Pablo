using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using His.Parametros;
using Recursos;

namespace His.Admision
{
    /// <summary>
    /// Lógica de interacción para frmAyudaHabitaciones.xaml
    /// </summary>
    public partial class frmAyudaHabitaciones : UserControl
    {
        public static frmControlesWPF formPadre; 
        public frmAyudaHabitaciones(frmControlesWPF padre, int x = 0)
        {
            InitializeComponent();
            List<HABITACIONES> habitacionesLista;
            if (x==1)
            {
                habitacionesLista = NegHabitaciones.listaHabitacionesEmergenciaLX();
            }
            else if(x == 2) //mushuñan
            {
                habitacionesLista = NegHabitaciones.listaHabitacionesMushuñan();
            }
            else if(x == 3) //consulta externa
            {
                habitacionesLista = NegHabitaciones.listaHabitacionesConsultorios();
            }
            else if (x == 4)
            {
                habitacionesLista = NegHabitaciones.listaHabitacionesBrigadaMedica();
            }
            else
            {
                habitacionesLista = NegHabitaciones.listaHabitaciones();
            }
            inicializarControles();
            if (Sesion.TipoBusquedaHabitacion == 1)
            {
                cargarhabitaciones(habitacionesLista, "Endoscopia");
            }
            else
            {
                cargarhabitaciones(habitacionesLista, "general");
            }
            formPadre = padre;
            formPadre.Text = "Ayuda";
        }
        private void inicializarControles()
        {
            habitaciones.Width = this.Width;
            habitaciones.Height = this.Height;
        }
        private void cargarhabitaciones(List<HABITACIONES> habitacionesLista, string tipo)
        {
            try
            {
                if (tipo == "general")
                {
                    var listaFiltradaHabitaciones = from h in habitacionesLista
                                                    where h.hab_Activo == true && h.HABITACIONES_ESTADO.HES_CODIGO == Parametros.AdmisionParametros.getEstadoHabitacionDisponible() 
                                                    group h by h.NIVEL_PISO.NIV_NOMBRE into grupos
                                                    select new { Categoria = grupos.Key,detalle=  grupos.Select(hb => new ListaHabitaciones() { codigo=hb.hab_Codigo,numero= hb.hab_Numero, estado=hb.HABITACIONES_ESTADO.HES_NOMBRE }) };
                    xamDataGridHabitaciones.DataSource = listaFiltradaHabitaciones; 
                }

                if (tipo == "Endoscopia")
                {
                    var listaFiltradaHabitaciones = from h in habitacionesLista
                                                    where ((h.hab_Activo == true) && ((Convert.ToInt32(h.NIVEL_PISOReference.EntityKey.EntityKeyValues[0].Value) == 9) || (Convert.ToInt32(h.NIVEL_PISOReference.EntityKey.EntityKeyValues[0].Value) == 14)) && (h.HABITACIONES_ESTADO.HES_CODIGO == Parametros.AdmisionParametros.getEstadoHabitacionDisponible()))
                                                    group h by h.NIVEL_PISO.NIV_NOMBRE into grupos
                                                    select new { Categoria = grupos.Key, detalle = grupos.Select(hb => new ListaHabitaciones() { codigo = hb.hab_Codigo, numero = hb.hab_Numero, estado = hb.HABITACIONES_ESTADO.HES_NOMBRE }) };
                    xamDataGridHabitaciones.DataSource = listaFiltradaHabitaciones;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void moverItem1(MenuItem item)
        {
            MessageBox.Show("Mover la habitacion " + item.Tag);
        }

        private void enviarHabitacion(HABITACIONES habitacion)
        {
            try
            {
                if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == Parametros.AdmisionParametros.getEstadoHabitacionDisponible())
                {
                    Sesion.codHabitacion = habitacion.hab_Codigo;
                    Sesion.numHabitacion = habitacion.hab_Numero;
                    formPadre.Close();
                }
                else
                {
                    MessageBox.Show("Por favor seleccione una habitacion disponible", "", MessageBoxButton.OK, MessageBoxImage.Information);     
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);     
            }
        }
        private void mostrarInfHabitacion(HABITACIONES habitacion)
        {
            if ((habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionOcupado()) || (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionAlta()) || (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionCancelada()))
            {
                var detalleHabitacion = NegHabitaciones.RecuperarDetallesHabitacion(null, null, habitacion.hab_Codigo.ToString(), null, null, null, null, null, "false").OrderByDescending(h => h.HAD_FECHA_INGRESO).FirstOrDefault();
                infHabitacion.Visibility = Visibility.Visible;
                Point posicionActual = Mouse.GetPosition(habitaciones);
                if ((posicionActual.X + infHabitacion.Width) > habitaciones.ActualWidth)
                {
                    infHabitacion.Margin = new Thickness((habitaciones.ActualWidth - infHabitacion.Width), (posicionActual.Y), 0, 0);
                }
                else
                {
                    infHabitacion.Margin = new Thickness((posicionActual.X), (posicionActual.Y), 0, 0);
                }
                if (detalleHabitacion != null)
                {
                    Label info = new Label();
                    string strInfo = "";
                    if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionOcupado())
                    {
                        strInfo = "Paciente: " + detalleHabitacion.PACIENTE + "\n" +
                                    "HC: " + detalleHabitacion.PAC_HISTORIA_CLINICA + "\n" +
                                    "# Atención: " + detalleHabitacion.ATE_CODIGO + "\n" +
                                    "Fec. Ingreso: " + detalleHabitacion.HAD_FECHA_INGRESO + "\n" +
                                    "Medico: " + detalleHabitacion.MED_NOMBRE + "\n" +
                                    "Especialidad: " + detalleHabitacion.ESP_NOMBRE;
                    }
                    else if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionAlta())
                    {
                        strInfo = "Paciente: " + detalleHabitacion.PACIENTE + "\n" +
                                    "HC: " + detalleHabitacion.PAC_HISTORIA_CLINICA + "\n" +
                                    "# Atención: " + detalleHabitacion.ATE_CODIGO + "\n" +
                                    "Fec. Ingreso: " + detalleHabitacion.HAD_FECHA_INGRESO + "\n" +
                                    "Medico: " + detalleHabitacion.MED_NOMBRE + "\n" +
                                    "Diagnostico: " + detalleHabitacion.ATE_DIAGNOSTICO_FINAL;
                    }
                    else if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionCancelada())
                    {
                        strInfo = "Paciente: " + detalleHabitacion.PACIENTE + "\n" +
                                    "HC: " + detalleHabitacion.PAC_HISTORIA_CLINICA + "\n" +
                                    "# Atención: " + detalleHabitacion.ATE_CODIGO + "\n" +
                                    "Fec. Ingreso: " + detalleHabitacion.HAD_FECHA_INGRESO + "\n" +
                                    "Medico: " + detalleHabitacion.MED_NOMBRE + "\n" +
                                    "Fec Facturación: " + detalleHabitacion.HAD_FECHA_FACTURACION +
                                    "Diagnostico: " + detalleHabitacion.ATE_DIAGNOSTICO_FINAL;
                    }
                    info.Content = strInfo;
                    stackPanel1.Children.Add(info);
                    Label historia = new Label();
                }
            }

            //List<HABITACIONES_ATENCION_VISTA> infHabitaciones = NegHabitaciones.RecuperarDetallesHabitacion(null, null, null, null, null, null, AdmisionParametros.getEstadoHabitacionOcupado().ToString() , null,"false").OrderByDescending(h=>h.HAD_FECHA_INGRESO).ToList();
        }

        private void infHabCerrar_Click(object sender, RoutedEventArgs e)
        {
            infHabitacion.Visibility = Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void infHabCerrar_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void infHabCerrar_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void xamDataGridHabitaciones_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void xamDataGridHabitaciones_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (xamDataGridHabitaciones.ActiveRecord != null) // verifico que exista un dato seleccionado / Giovanny Tapia / 27/08/2012
                {
                    if (!xamDataGridHabitaciones.ActiveRecord.HasChildren)
                    {
                        if (xamDataGridHabitaciones.ActiveDataItem != null)
                        {

                            ListaHabitaciones item = (ListaHabitaciones)xamDataGridHabitaciones.ActiveDataItem;

                                Sesion.codHabitacion = item.codigo;
                                Sesion.numHabitacion = item.numero;
                                formPadre.Close();

                        }
                    }

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void xamDataGridHabitaciones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (xamDataGridHabitaciones.ActiveRecord != null)
                {
                    if (!xamDataGridHabitaciones.ActiveRecord.HasChildren)
                    {
                        ListaHabitaciones item = (ListaHabitaciones)xamDataGridHabitaciones.ActiveDataItem;

                        Sesion.codHabitacion = item.codigo;
                        Sesion.numHabitacion = item.numero;
                        formPadre.Close();

                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }

        }

        private void xamDataGridHabitaciones_Initialized(object sender, EventArgs e)
        {
            
        }
    }

    public class ListaHabitaciones
    {
        public int codigo { get; set; }
        public string numero { get; set; }
        public string estado { get; set; }
    }

}
