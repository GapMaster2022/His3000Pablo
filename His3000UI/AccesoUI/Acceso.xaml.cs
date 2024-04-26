using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Utilitarios;
//using GeneralApp;
using His.Admision;
using His.Entidades;
using His.HabitacionesUI;
using His.Honorarios;
//using His.Maintenance;
using His.Maintenance;
using His.Negocio;
using TarifariosUI;
using CuentaPaciente;
using His.Parametros;
using His.Dietetica;
using System.Diagnostics;

namespace AccesoUI
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml 
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnHonorarios_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
                btnHonorarios.Background = new SolidColorBrush(Colors.DarkGray);
                btnHonorarios.Foreground = new SolidColorBrush(Colors.Black);
                txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
 
        }

        private void btnHonorarios_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnHonorarios.Background = miGradiente;  
            //btnHonorarios.Background = new SolidColorBrush(Colors.Black);
            //btnHonorarios.Foreground = new SolidColorBrush(Colors.White);   
            txtResumen.Text = "";

        }

        private void btnHonorarios_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //AbrirFormulario<MDI_Honorarios>();
                Form honorarios = new MDI_Honorarios(); //CAMBIOS EDGAR RAMOS
                honorarios.Show();
                //System.Diagnostics.Process.Start("C:\\Archivos de programa\\GapSystem\\His3000\\Honorarios\\" + "His.Honorarios.exe");
                //this.Close();

            }
            catch (Exception err)
            {

                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //CAMBIOS EDGAR RAMOS... 
        #region Meotodo Formulario
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            formulario = System.Windows.Forms.Application.OpenForms.Cast<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
                                                                                     //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.Show();
                formulario.BringToFront();
            }
            //si el formulario/instancia existe
            else
            {
                formulario.BringToFront();
            }
        }
        #endregion
        private void btnCerrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Está seguro de SALIR?\r\nSe perdera informacion de las ventanas abiertas.\r\n¿Desea Continuar?", "HIS3000", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void btnTarifario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcesses())
                //{
                //    if (proc.ProcessName == "TarifariosUI.exe")
                //        System.Diagnostics.Process.Start("taskkill", "C:\\Archivos de programa\\GapSystem\\His3000\\Tarifario" + "TarifariosUI.exe");
                //}
                //System.Diagnostics.Process.Start("C:\\Archivos de programa\\GapSystem\\His3000\\Tarifario\\" + "TarifariosUI.exe");
                //this.Close();
                //AbrirFormulario<frmMainTarifario>();
                Form tarifario = new frmMainTarifario(); //CAMBIO EDGAR RAMOS
                tarifario.Show();

            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMantenimiento_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.Maintenance.MDI_Maintenance>();
                //System.Diagnostics.Process.Start("C:\\Archivos de programa\\GapSystem\\His3000\\Mantenimiento\\" + "His.Mantenimiento.exe");
                //this.Close();
                Form mantanimiento = new MDI_Maintenance();
                mantanimiento.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMantenimiento_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
                btnMantenimiento.Background = new SolidColorBrush(Colors.DarkGray);
                btnMantenimiento.Foreground = new SolidColorBrush(Colors.Black);
                txtResumen.Text = "En el Modulo de Mantenimiento podra encontrar todo \n lo referente \n";
        }

        private void btnMantenimiento_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnMantenimiento.Background = miGradiente;  
            //btnMantenimiento.Background = new SolidColorBrush(Colors.Black);
            //btnMantenimiento.Foreground = new SolidColorBrush(Colors.White);
            txtResumen.Text = ""; 
        }

        private void btnTarifario_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnTarifario.Background = new SolidColorBrush(Colors.DarkGray);
            btnTarifario.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n"; 
        }

        private void btnTarifario_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133,133,129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnTarifario.Background = miGradiente;  
            //btnTarifario.Background = new SolidColorBrush(Colors.Black);
            //btnTarifario.Foreground = new SolidColorBrush(Colors.White);
            txtResumen.Text = ""; 
        }

        private void btnAdmision_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //AbrirFormulario<MDI_Admision>();
                Form admision = new MDI_Admision(); //CAMBIOS EDGAR RAMOS
                admision.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdmision_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAdmision.Background = new SolidColorBrush(Colors.DarkGray);
            btnAdmision.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n"; 
        }

        private void btnAdmision_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnAdmision.Background = miGradiente;  
            //btnAdmision.Background = new SolidColorBrush(Colors.Black);
            //btnAdmision.Foreground = new SolidColorBrush(Colors.White);
            txtResumen.Text = ""; 
        }

        private void btnHabitaciones_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExploradorHabitaciones habitaciones = new ExploradorHabitaciones();
                habitaciones.Show(); 
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnHabitaciones_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnHabitaciones.Background = miGradiente;  
            txtResumen.Text = ""; 
        }

        private void btnHabitaciones_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnHabitaciones.Background = new SolidColorBrush(Colors.DarkGray);
            btnHabitaciones.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n"; 
        }

        private void btnEmergencia_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.Emergencia.MDI_Emergencia>();
                His.Emergencia.MDI_Emergencia emergencia = new His.Emergencia.MDI_Emergencia(); //CAMBIOS EDGAR RAMOS
                emergencia.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEmergencia_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        	btnEmergencia_Copy1.Background = new SolidColorBrush(Colors.DarkGray);
            btnEmergencia_Copy1.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n"; 
        }

        private void btnEmergencia_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
        	LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnEmergencia_Copy1.Background = miGradiente;  
            txtResumen.Text = ""; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
   
            AccesosModuloAcceso.setModuloMantenimiento(false);
            AccesosModuloAcceso.setModuloMantenimiento(false);
            AccesosModuloAcceso.setModuloHonorarios(false);
            AccesosModuloAcceso.setModuloTarifario(false);
            AccesosModuloAcceso.setModuloAdmision(false);
            AccesosModuloAcceso.setModuloHabitaciones(false);
            AccesosModuloAcceso.setModuloEmergencias(false);
            AccesosModuloAcceso.setModuloPedidos(false);
            AccesosModuloAcceso.setModuloCuentaPaciente(false);
            //Cambios Edgar 2020-12-09
            AccesosModuloAcceso.ModuloMedicos = false;
            AccesosModuloAcceso.ModuloPedidosEspeciales = false;
            AccesosModuloAcceso.ModuloConsultaExterna = false;
            AccesosModuloAcceso.ModuloEmergencia = false;
                                                                     
            if (His.Entidades.Clases.Sesion.codUsuario != 0)
            {
                USUARIOS usuario = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario);
                List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(usuario.ID_USUARIO);

                if (perfilUsuario != null)
                {
                    foreach (var Perfil in perfilUsuario)
                    {
                        if (perfilUsuario != null)
                            NegAccesoOpciones.asignarAccesosPorPerfil(Perfil.ID_PERFIL);
                        else
                        {
                            System.Windows.MessageBox.Show("No se asignado un perfil al usuario", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                            this.Close();
                        }
                    }
                }
                //valido los accesos de acuerdo al perfil del usuario
                btnAdmision.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloAdmision();
                img_Admision.IsEnabled = AccesosModuloAcceso.getModuloAdmision();
                btnHabitaciones.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloHabitaciones();
                img_Habitacion.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloHabitaciones();
                btnHonorarios.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloHonorarios();
                img_Honorarios.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloHonorarios();
                btnMantenimiento.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloMantenimiento();
                img_Mantenimiento.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloMantenimiento();
                btnTarifario.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloTarifario();
                img_Tarifario.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloTarifario();
                btnEmergencia.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloEmergencias();
                img_Emergencia.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloEmergencias();
                btnPedidosPaciente.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloPedidos();
                img_Pedidos.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloPedidos();
                btnCuentaPaciente.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloCuentaPaciente();
                img_Cuenta.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloCuentaPaciente();
                //Cambios Edgar 2020-12-09     AQUI SE PUEDE AGREGAR SEGURIDAD A LOS MODULO DEPENDIENDO DEL TIPO DE ACCESO TENGA EL USUARIO
                btnCuentaPaciente_Copy1.IsEnabled = AccesosModuloAcceso.ModuloPedidosEspeciales;
                img_PE.IsEnabled = AccesosModuloAcceso.ModuloPedidosEspeciales;
                btnConsultaExterna.IsEnabled = AccesosModuloAcceso.ModuloConsultaExterna;
                img_CE.IsEnabled = AccesosModuloAcceso.ModuloConsultaExterna;
                btnMedicos.IsEnabled = AccesosModuloAcceso.ModuloMedicos;
                img_Medico.IsEnabled = AccesosModuloAcceso.ModuloMedicos;
                btnEmergencia_Copy1.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloEmergencias();
                img_Emergencia.IsEnabled = His.Parametros.AccesosModuloAcceso.getModuloEmergencias();

                //
                lblUsuario.Content = His.Entidades.Clases.Sesion.nomUsuario;
                lblDepartamento.Content = His.Entidades.Clases.Sesion.nomDepartamento;
                ImageSource imgSource = new BitmapImage(new Uri("imagenes/male.png", UriKind.RelativeOrAbsolute));
                imgUsuario.Source = imgSource;

                //ImageSource imgLogo = new BitmapImage(new Uri("imagenes/Empresa.png", UriKind.RelativeOrAbsolute));
                //pasteur_gap_3_png.Source = imgLogo;
            }
            else
            {
                System.Windows.MessageBox.Show("Usuario Incorrecto", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                contenedor.IsEnabled = false;
            }
        }

        private void btnCuentaPaciente_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			btnCuentaPaciente.Background = new SolidColorBrush(Colors.DarkGray);
            btnCuentaPaciente.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n"; 
        }

        private void btnCuentaPaciente_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnCuentaPaciente.Background = miGradiente;  
            txtResumen.Text = ""; 
        }

        private void btnCuentaPaciente_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //AbrirFormulario<CuentaPaciente.MDICuentaPaciente>();
                CuentaPaciente.MDICuentaPaciente cuentaPaciente = new CuentaPaciente.MDICuentaPaciente(); //CAMBIOS EDGAR RAMOS
                cuentaPaciente.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPedidosPaciente_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	try
            {
                ////AbrirFormulario<His.Pedidos.MDIPedidos>();
                His.Pedidos.MDIPedidos pedidos = new His.Pedidos.MDIPedidos(); //CAMBIOS EDGAR
                pedidos.Show();
                //string path = AppDomain.CurrentDomain.BaseDirectory;

                //Process p = new Process();
                //p.StartInfo.FileName = path + @"His.Pedido.exe";
                //p.Start();
                ////psi.Arguments = "/c \"first.exe -a -b -c | second.exe\"";
                //MDI_Pedido x = new MDI_Pedido();
                //x.Show();
            }
            catch (Exception err)


            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPedidosPaciente_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
			btnPedidosPaciente.Background = new SolidColorBrush(Colors.DarkGray);
            btnPedidosPaciente.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n"; 
        }

        private void btnPedidosPaciente_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
        	LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnPedidosPaciente.Background = miGradiente;  
            txtResumen.Text = ""; 
        }

        private void btnCerrarSesion_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            try
            {
                //Cambios edgar 20210324 No se puede cerrar habitacion por no ser un form nativo de microsoft
                if(System.Windows.Forms.MessageBox.Show("¿Esta seguro de cerrar sesión?\r\nSe perdera informacion de las ventanas abiertas.\r\n¿Desea Continuar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Stop)
                    == System.Windows.Forms.DialogResult.Yes)
                {
                    CerrarFormulario();
                    this.Hide();


                    login ventana = new login();
                    ventana.Show();
                    this.Close();
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show("Debe HABITACIONES para poder continuar. \r\nMás detalles: " + err.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //System.Windows.MessageBox.Show(err.Message);
            }
        }

        public void CerrarFormulario()
        {
            bool abierto = false;
            FormCollection formulariosApp = System.Windows.Forms.Application.OpenForms;

            foreach (Form f in formulariosApp)
            {

                if (f.Name != "login")
                {
                    abierto = true;
                    f.Close();
                    break;
                }
            }
            if(abierto == true)
            {
                CerrarFormulario();
            }
        }

        private void CerrarForm(string form)
        {
            Form Formulario;
            Formulario = System.Windows.Forms.Application.OpenForms[form];
            if(Formulario != null)
            {
                Formulario.Close();
            }
        }
        //private void CerrarFormulario<MiForm>() where MiForm : Form, new()
        //{
        //    Form formulario;
        //    formulario = System.Windows.Forms.Application.OpenForms.Cast<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
        //    string form = MiForm.ToString();
        //    Form prueba;
        //    prueba = System.Windows.Forms.Application.OpenForms["MDI_Admision"];
        //    //si el formulario / instancia no existe
        //    if (formulario == null)
        //    {
        //        formulario = new MiForm();
        //        formulario.Show();
        //        formulario.BringToFront();
        //    }
        //    ////si el formulario / instancia existe
        //    if (formulario != null)
        //    {
        //        formulario.Close();
        //    }
        //    if (prueba != null)
        //    {
        //        prueba.Close();
        //    }
        //}
        private void btnCuentaPaciente_Copy1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Form mantanimiento = new MDI_PE();
                mantanimiento.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCuentaPaciente_Copy1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnCuentaPaciente_Copy1.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void btnCuentaPaciente_Copy1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnCuentaPaciente_Copy1.Background = new SolidColorBrush(Colors.DarkGray);
            btnCuentaPaciente_Copy1.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void btnConsultaExterna_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                His.ConsultaExterna.mdiConsultaExterna consulta = new His.ConsultaExterna.mdiConsultaExterna();
                consulta.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void btnMedicos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                His.Formulario.MDI_Medicos medicos = new His.Formulario.MDI_Medicos();
                medicos.Show();

            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnMedicos_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnMedicos.Background = new SolidColorBrush(Colors.DarkGray);
            btnMedicos.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void btnMedicos_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnMedicos.Background = miGradiente;
            //btnAdmision.Background = new SolidColorBrush(Colors.Black);
            //btnAdmision.Foreground = new SolidColorBrush(Colors.White);
            txtResumen.Text = "";
        }

        private void image_Copy1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnEmergencia_Copy1.Background = new SolidColorBrush(Colors.DarkGray);
            btnEmergencia_Copy1.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_Copy1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnEmergencia_Copy1.Background = miGradiente;
            txtResumen.Text = "";
        }


        private void image_Copy1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.Emergencia.MDI_Emergencia>();
                His.Emergencia.MDI_Emergencia emergencia = new His.Emergencia.MDI_Emergencia(); //CAMBIOS EDGAR RAMOS
                emergencia.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.Pedidos.MDIPedidos>();
                His.Pedidos.MDIPedidos pedidos = new His.Pedidos.MDIPedidos(); //CAMBIOS EDGAR
                pedidos.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnPedidosPaciente.Background = new SolidColorBrush(Colors.DarkGray);
            btnPedidosPaciente.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnPedidosPaciente.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void image_Copy_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<CuentaPaciente.MDICuentaPaciente>();
                CuentaPaciente.MDICuentaPaciente cuentaPaciente = new CuentaPaciente.MDICuentaPaciente(); //CAMBIOS EDGAR RAMOS
                cuentaPaciente.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // TODO: Add event handler implementation here.
            btnCuentaPaciente.Background = new SolidColorBrush(Colors.DarkGray);
            btnCuentaPaciente.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_Copy_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnCuentaPaciente.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void btnMedicos_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnMedicos.Background = new SolidColorBrush(Colors.DarkGray);
            btnMedicos.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void btnMedicos_MouseLeave_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnMedicos.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void btnMedicos_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.Formulario.MDI_Medicos>();
                //His.Formulario.MDI_Medicos x = new His.Formulario.MDI_Medicos();
                //x.Show();
                His.Honorarios.MDI_Medicos x = new MDI_Medicos();
                x.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.Formulario.MDI_Medicos>();
                His.Formulario.MDI_Medicos x = new His.Formulario.MDI_Medicos();
                x.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnMedicos.Background = new SolidColorBrush(Colors.DarkGray);
            btnMedicos.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_Copy2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnMedicos.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void btnConsultaExterna_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.ConsultaExterna.mdiConsultaExterna>();
                His.ConsultaExterna.mdiConsultaExterna x = new His.ConsultaExterna.mdiConsultaExterna();
                x.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnConsultaExterna_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnConsultaExterna.Background = new SolidColorBrush(Colors.DarkGray);
            btnConsultaExterna.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void btnConsultaExterna_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnConsultaExterna.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void image_Copy3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnConsultaExterna.Background = new SolidColorBrush(Colors.DarkGray);
            btnConsultaExterna.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_Copy3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnConsultaExterna.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void btnCuentaPaciente_Copy1_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.Dietetica.MDI_PE>();
                His.Dietetica.MDI_PE x = new MDI_PE();
                x.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCuentaPaciente_Copy1_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnCuentaPaciente_Copy1.Background = new SolidColorBrush(Colors.DarkGray);
            btnCuentaPaciente_Copy1.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void btnCuentaPaciente_Copy1_MouseLeave_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnCuentaPaciente_Copy1.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void image_Copy4_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.Dietetica.MDI_PE>();
                His.Dietetica.MDI_PE x = new MDI_PE();
                x.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy4_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnCuentaPaciente_Copy1.Background = new SolidColorBrush(Colors.DarkGray);
            btnCuentaPaciente_Copy1.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_Copy4_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnCuentaPaciente_Copy1.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void image_Copy5_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ExploradorHabitaciones habitaciones = new ExploradorHabitaciones();
                habitaciones.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy5_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnHabitaciones.Background = new SolidColorBrush(Colors.DarkGray);
            btnHabitaciones.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";

        }

        private void image_Copy5_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnHabitaciones.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void btnTarifario_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //AbrirFormulario<TarifariosUI.frmMainTarifario>();
                TarifariosUI.frmMainTarifario x = new frmMainTarifario();
                x.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnTarifario_MouseEnter_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnTarifario.Background = new SolidColorBrush(Colors.DarkGray);
            btnTarifario.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void btnTarifario_MouseLeave_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnTarifario.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void image_Copy6_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<TarifariosUI.frmMainTarifario>();
                TarifariosUI.frmMainTarifario x = new frmMainTarifario();
                x.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy6_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnTarifario.Background = new SolidColorBrush(Colors.DarkGray);
            btnTarifario.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_Copy6_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);

            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnTarifario.Background = miGradiente;
            txtResumen.Text = "";
        }

        private void image_Copy7_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<MDI_Admision>();
                Form admision = new MDI_Admision(); //CAMBIOS EDGAR RAMOS
                admision.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy7_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAdmision.Background = new SolidColorBrush(Colors.DarkGray);
            btnAdmision.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_Copy7_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnAdmision.Background = miGradiente;
            //btnAdmision.Background = new SolidColorBrush(Colors.Black);
            //btnAdmision.Foreground = new SolidColorBrush(Colors.White);
            txtResumen.Text = "";
        }

        private void image_Copy9_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<MDI_Honorarios>();
                Form honorarios = new MDI_Honorarios(); //CAMBIOS EDGAR RAMOS
                honorarios.Show();
                //System.Diagnostics.Process.Start("C:\\Archivos de programa\\GapSystem\\His3000\\Honorarios\\" + "His.Honorarios.exe");
                //this.Close();

            }
            catch (Exception err)
            {

                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy9_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnHonorarios.Background = new SolidColorBrush(Colors.DarkGray);
            btnHonorarios.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "Este modulo muestra \n Todo lo referente \n";
        }

        private void image_Copy9_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnHonorarios.Background = miGradiente;
            //btnHonorarios.Background = new SolidColorBrush(Colors.Black);
            //btnHonorarios.Foreground = new SolidColorBrush(Colors.White);   
            txtResumen.Text = "";
        }

        private void image_Copy8_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                AbrirFormulario<His.Maintenance.MDI_Maintenance>();
                //System.Diagnostics.Process.Start("C:\\Archivos de programa\\GapSystem\\His3000\\Mantenimiento\\" + "His.Mantenimiento.exe");
                //this.Close();
                //Form mantanimiento = new MDI_Maintenance(); 
                //mantanimiento.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void image_Copy8_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnMantenimiento.Background = new SolidColorBrush(Colors.DarkGray);
            btnMantenimiento.Foreground = new SolidColorBrush(Colors.Black);
            txtResumen.Text = "En el Modulo de Mantenimiento podra encontrar todo \n lo referente \n";
        }

        private void image_Copy8_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            LinearGradientBrush miGradiente = new LinearGradientBrush();
            miGradiente.StartPoint = new Point(0, 0);
            miGradiente.EndPoint = new Point(0, 1);
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(133, 133, 129), 1.0));
            miGradiente.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.0));
            btnMantenimiento.Background = miGradiente;
            //btnMantenimiento.Background = new SolidColorBrush(Colors.Black);
            //btnMantenimiento.Foreground = new SolidColorBrush(Colors.White);
            txtResumen.Text = "";
        }

        private void image_Copy3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //AbrirFormulario<His.ConsultaExterna.mdiConsultaExterna>();
                His.ConsultaExterna.mdiConsultaExterna x = new His.ConsultaExterna.mdiConsultaExterna();
                x.Show();
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void contenedor_Initialized(object sender, EventArgs e)
        {

        }
    }
}
