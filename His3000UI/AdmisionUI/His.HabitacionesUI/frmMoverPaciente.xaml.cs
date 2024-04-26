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
using System.Windows.Shapes;
using FeserWard.Controls;
using His.Entidades;
using His.Negocio;
using His.Entidades.Clases;
using His.Parametros;
using System.Data;

namespace His.HabitacionesUI
{
	/// <summary>
	/// Interaction logic for frmMoverPaciente.xaml
	/// </summary>
	public partial class frmMoverPaciente : Window
	{

        bool estado;
        HABITACIONES parHabitacion;
        ATENCIONES parAtencion;
        public string habitacion;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
		public frmMoverPaciente(HABITACIONES habitacion,ATENCIONES atencion)
		{
			this.InitializeComponent();
            parAtencion = atencion;
            parHabitacion = habitacion;
            estado = false;
            
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cargarCombos();
        }
        private void cargarCombos()
        {
            try
            {
                xamCboPiso.ItemsSource =  NegHabitaciones.listaNivelesPiso();
                xamCboPiso.DisplayMemberPath = "NIV_NOMBRE";
                xamCboPiso.SelectedIndex = 0;

                DataTable tabla = new DataTable();
                tabla = NegParametros.ParametroHabitacionDefault();

                if(tabla.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(tabla.Rows[0][1].ToString()))
                    {
                        xamCboPiso.SelectedText = tabla.Rows[0][0].ToString();
                        xamCboPiso.IsEnabled = false;
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void xamCboPiso_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (xamCboPiso.SelectedItem != null)
                {
                    NIVEL_PISO item = (NIVEL_PISO)xamCboPiso.SelectedItem;
                    List<HABITACIONES> listaHabitaciones = NegHabitaciones.listaHabitaciones(item.NIV_CODIGO,Parametros.AdmisionParametros.getEstadoHabitacionDisponible());
                    xamCboHabitaciones.ItemsSource = listaHabitaciones;
                    xamCboHabitaciones.DisplayMemberPath = "hab_Numero";
                    xamCboHabitaciones.SelectedIndex = 0;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }

        }

        public void RefrescarHabitacion()
        {
            if (xamCboPiso.SelectedItem != null)
            {
                NIVEL_PISO item = (NIVEL_PISO)xamCboPiso.SelectedItem;
                List<HABITACIONES> listaHabitaciones = NegHabitaciones.listaHabitaciones(item.NIV_CODIGO, Parametros.AdmisionParametros.getEstadoHabitacionDisponible());
                xamCboHabitaciones.ItemsSource = listaHabitaciones;
                xamCboHabitaciones.DisplayMemberPath = "hab_Numero";
                xamCboHabitaciones.SelectedIndex = 0;
            }
        }

        public bool getEstado()
        {
            return estado;
        }

        private void btnAceptar_Click_1(object sender, RoutedEventArgs e)
        {

            MessageBoxResult resultado = MessageBox.Show("Desea guardar los cambios", "HIS3000", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultado == MessageBoxResult.Yes)
            {
                #region Codigo Anterior
                //try
                //{
                //    HABITACIONES habitacionSelecionada = (HABITACIONES)xamCboHabitaciones.SelectedItem;
                //    //recupero el detalle actual
                //    HABITACIONES_DETALLE habitacionDetalleOld = NegHabitaciones.RecuperarDetalleHabitacion(parAtencion);
                //    habitacionDetalleOld.HAD_FECHA_DISPONIBILIDAD = DateTime.Now;
                //    habitacionDetalleOld.HAD_OBSERVACION = "(cambio habitación origen) " + txtObservacion.Text;
                //    NegHabitaciones.ActualizarDetallehabitacion(habitacionDetalleOld);
                //    //creo el nuevo detalle
                //    HABITACIONES_DETALLE habitacionDetalle = new HABITACIONES_DETALLE();
                //    habitacionDetalle.HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1;
                //    habitacionDetalle.ATE_CODIGO = parAtencion.ATE_CODIGO;
                //    habitacionDetalle.HABITACIONESReference.EntityKey = habitacionSelecionada.EntityKey;
                //    habitacionDetalle.HAD_ESTADO = Convert.ToString(parHabitacion.HABITACIONES_ESTADO.HES_CODIGO);
                //    habitacionDetalle.ID_USUARIO = Sesion.codUsuario;
                //    habitacionDetalle.HAD_FECHA_INGRESO = DateTime.Now;
                //    habitacionDetalle.HAD_OBSERVACION = "cambio habitación destino";
                //    habitacionDetalle.HAD_REGISTRO_ANTERIOR = (short)habitacionDetalleOld.HAD_CODIGO;
                //    NegHabitaciones.CrearHabitacionDetalle(habitacionDetalle);
                //   //crear habitaciones detalle
                //    HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
                //    habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
                //    habitacionHistorial.ATE_CODIGO = parAtencion.ATE_CODIGO;

                //    habitacionHistorial.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                //    habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
                //    habitacionHistorial.HAD_OBSERVACION = "Se mueve de  habitacion";
                //    habitacionHistorial.HAH_REGISTRO_ANTERIOR = (short)habitacionDetalleOld.HAD_CODIGO;
                //    habitacionHistorial.HAH_ESTADO = Convert.ToInt16(parHabitacion.HABITACIONES_ESTADO.HES_CODIGO);


                //    //actualizo la atencion
                //    parAtencion.HABITACIONESReference.EntityKey = habitacionSelecionada.EntityKey;
                //    NegAtenciones.EditarAtencionAdmision(parAtencion,1);
                //    //actualizo el estado de la habitacion
                //    parHabitacion.HABITACIONES_ESTADOReference.EntityKey = NegHabitaciones.RecuperarEstadoHabitacion(AdmisionParametros.getEstadoHabitacionDisponible()).EntityKey;
                //    NegHabitaciones.CambiarEstadoHabitacion(parHabitacion);
                //    habitacionSelecionada.HABITACIONES_ESTADOReference.EntityKey = NegHabitaciones.RecuperarEstadoHabitacion(AdmisionParametros.getEstadoHabitacionOcupado()).EntityKey;
                //    habitacionHistorial.HAB_CODIGO = (habitacionSelecionada.hab_Codigo);
                //    NegHabitaciones.CambiarEstadoHabitacion(habitacionSelecionada);
                //    estado = true;
                //    NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);

                //    this.Close();
                //}
                #endregion
                try
                {
                    var habitacionSelecionada = (HABITACIONES)xamCboHabitaciones.SelectedItem;
                    if(NegHabitaciones.HabDisponible(habitacionSelecionada.hab_Codigo) != 5)
                    {
                        MessageBox.Show("La habitacion: " + habitacionSelecionada.hab_Numero + " ya ha sido ocupada.", "HIS30000", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        RefrescarHabitacion();
                        return;
                    }
                    ////creo el nuevo detalle
                    int had_codigo;
                    var habitacionDetalle = new HABITACIONES_DETALLE
                    {
                        HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1,
                        ATE_CODIGO = parAtencion.ATE_CODIGO,
                        HABITACIONESReference = {EntityKey = habitacionSelecionada.EntityKey},
                        ID_USUARIO = Sesion.codUsuario,
                        HAD_FECHA_INGRESO = DateTime.Now,
                        HAD_OBSERVACION = txtObservacion.Text
                    };

                    //--------------------------------------------------------

                    //actualizo la atencion
                    parAtencion.ATE_FECHA_ALTA = null;
                    parAtencion.ATE_ESTADO = true;
                    parAtencion.HABITACIONESReference.EntityKey = habitacionSelecionada.EntityKey;
                    had_codigo = habitacionDetalle.HAD_CODIGO;

                    if(NegHabitaciones.CambiaEstadoHabitacion(parAtencion.ATE_CODIGO, habitacionSelecionada.hab_Codigo))
                    {
                        NegHabitaciones.CrearHabitacionDetalle(habitacionDetalle);

                        //SE PONE FECHA ALTA EN HISTORIAL DE HABITACIONES PARA CALCULO DE 
                        //VALORES AUTOMATICOS DE UCI
                        //PABLURAS 31/08/2021
                        NegHabitacionesHistorial.FechaAltaHistorial(parAtencion.ATE_CODIGO);

                        //Cambios Edgar 20210305
                        //Creo la historia de habitaciones
                        int hah_codigo;
                        HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
                        habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
                        habitacionHistorial.ATE_CODIGO = parAtencion.ATE_CODIGO;
                        habitacionHistorial.HAB_CODIGO = habitacionSelecionada.hab_Codigo;
                        habitacionHistorial.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                        habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
                        habitacionHistorial.HAD_OBSERVACION = txtObservacion.Text;
                        habitacionHistorial.HAH_ESTADO = 1;
                        hah_codigo = habitacionHistorial.HAH_CODIGO;
                        NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);
                        //Consultar que tipo de Ingreso es el original Cambios 20210505 Edgar 
                        NegHabitacionesHistorial.HabHistorialArea(parAtencion.ATE_CODIGO, hah_codigo, had_codigo);


                        //NegAtenciones.EditarAtencionAdmision(parAtencion, 0);
                        //actualizo el estado de la habitacion
                        habitacionSelecionada.HABITACIONES_ESTADOReference.EntityKey = NegHabitaciones.RecuperarEstadoHabitacion(AdmisionParametros.getEstadoHabitacionOcupado()).EntityKey;

                        NegHabitaciones.CambiarEstadoHabitacion(habitacionSelecionada);
                        estado = true;
                        habitacion = xamCboHabitaciones.Text;
                        this.Close();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Error de conectividad, vuela a intentar el cambio de habitación", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    
                }
                catch 
                {
                    MessageBox.Show("Error de conectividad, vuela a intentar el cambio de habitación", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
	}
	
}