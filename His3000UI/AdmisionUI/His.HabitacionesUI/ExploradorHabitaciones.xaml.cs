using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using His.General;
using His.Negocio;
using System.Windows.Controls.Primitives;
using His.Entidades;
using His.Parametros;
using System.Windows.Media.Effects;
using His.Formulario;
using His.Entidades.General;
using Infragistics.Documents.Excel;
using Infragistics.Windows.DataPresenter.WordWriter;
using Infragistics.Windows.DataPresenter.ExcelExporter;
using Infragistics.Windows.Reporting;
using His.Entidades.Pedidos;
using His.Entidades.Servicios;
using His.Entidades.Clases;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Data.OleDb;
using His.Entidades.Reportes;
using System.Net;
using His.Admision;
using His.DatosReportes;
using System.Drawing.Printing;
using System.Threading;
using His.Emergencia;



//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;


namespace His.HabitacionesUI
{
    /// <summary>
    /// Lógica de interacción para ExploradorHonorarios.xaml
    /// </summary>
    public partial class ExploradorHabitaciones
    {

        #region Variables 
        public event EventHandler Click;
        //estado
        bool _controlesCargados;
        int colorControl = 0;
        //gradientes
        LinearGradientBrush _gradienteVentana;
        LinearGradientBrush _gradienteBotonA;
        LinearGradientBrush _gradienteBotonAover;
        LinearGradientBrush _gradienteBotonB;
        LinearGradientBrush _gradienteBotonBover;
        List<HABITACIONES> _habitacionesLista;
        List<DtoPacientesAtencionesActivas> _pacientes;
        List<DtoPacientesAtencionesActivas> _paciente;
        List<DtoPacientesImagen> _pacientes_imagen;
        List<DtoCatalogosLista> _lstMedicos;
        List<DtoCatalogosLista> _lstSalas;
        List<DtoLstIngresoSala> _listaIntervenciones;
        HC_EVOLUCION evolucion = null;
        HC_EVOLUCION evoluciones = null;
        int priemera = 1;
        int control = 1;
        int superior = 100;
        int inferior = 0;
        //infragsitics
        private Infragistics.Windows.DataPresenter.GridView _xamGridView;
        private Infragistics.Windows.DataPresenter.CarouselView _xamCarouselView;
        private Infragistics.Windows.DataPresenter.CardView _xamCardView;
        //
        ATENCIONES _atencionActiva;
        //declaro un objeto Timer para el reloj
        System.Timers.Timer _timer;
        Int32 Piso = 0;
        DateTime FechaNacimientoPaciente;
        int a = 0;
        public List<DtoPedidoDevolucionDetalle> PedidosDetalle = new List<DtoPedidoDevolucionDetalle>();
        /*datos convenios*/

        int CodigoConvenio = 0;
        int CodigoTipoEmpresa = 0;
        int CodigoAseguradora = 0;
        DataTable validador = new DataTable();
        Int64 ateCodigo;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ExploradorHabitaciones()
        {

            InitializeComponent();
            InicializarVariables();
            if (!NegUtilitarios.IpBodegas())
            {
                MessageBox.Show("Está maquina no tiene asignada una bodega, por lo que no se puede abrir el modulo", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        /// <summary>
        /// Metodo en el que se inicializan los valors iniciales de las variables
        /// </summary>
        private void InicializarVariables()
        {
            // estado 
            _controlesCargados = false;
            //control timer del Reloj
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Enabled = true;

            //inicilizo los gradientes
            _gradienteVentana = new LinearGradientBrush { StartPoint = new Point(0, 1), EndPoint = new Point(1, 1) };
            _gradienteVentana.GradientStops.Add(new GradientStop(Color.FromRgb(88, 88, 88), 0.0));
            _gradienteVentana.GradientStops.Add(new GradientStop(Color.FromRgb(217, 217, 217), 1.0));

            _gradienteBotonA = new LinearGradientBrush { StartPoint = new Point(1, 1), EndPoint = new Point(0, 0) };
            _gradienteBotonA.GradientStops.Add(new GradientStop(Color.FromRgb(160, 160, 160), 0.0));
            _gradienteBotonA.GradientStops.Add(new GradientStop(Color.FromRgb(240, 240, 240), 1.0));

            _gradienteBotonAover = new LinearGradientBrush { StartPoint = new Point(1, 0), EndPoint = new Point(1, 1) };
            _gradienteBotonAover.GradientStops.Add(new GradientStop(Color.FromRgb(210, 210, 210), 0.0));
            _gradienteBotonAover.GradientStops.Add(new GradientStop(Color.FromRgb(55, 55, 55), 1.0));

            _gradienteBotonB = new LinearGradientBrush { StartPoint = new Point(1, 1), EndPoint = new Point(0, 0) };
            _gradienteBotonB.GradientStops.Add(new GradientStop(Color.FromRgb(125, 180, 255), 0.0));
            _gradienteBotonB.GradientStops.Add(new GradientStop(Color.FromRgb(0, 80, 190), 1.0));

            _gradienteBotonBover = new LinearGradientBrush { StartPoint = new Point(1, 0), EndPoint = new Point(1, 1) };
            _gradienteBotonBover.GradientStops.Add(new GradientStop(Color.FromRgb(110, 160, 210), 0.0));
            _gradienteBotonBover.GradientStops.Add(new GradientStop(Color.FromRgb(55, 55, 55), 1.0));

            //ventana de inf paciente
            infHabitacion.Visibility = Visibility.Hidden;
            //otros
            xdtpFechaInigreso.Value = DateTime.Now;
            xdtpFechaEgreso.Value = DateTime.Now;
            _xamGridView = new Infragistics.Windows.DataPresenter.GridView();
            _xamCarouselView = new Infragistics.Windows.DataPresenter.CarouselView();
            _xamCardView = new Infragistics.Windows.DataPresenter.CardView();
            fechaInf.Content = String.Format("{0:MMMM}", DateTime.Now);
            lblTituloPanelMenu.Content = "";
        }

        #endregion

        #region metodos de la clase

        #region Metodos de inicializacion de componentes
        /// <summary>
        /// evento que carga los botones de estado
        /// </summary>
        private void CargarEstadosHabitaciones()
        {

            validador = NegAtenciones.RecuperaPermisos();
            //CargarEstadosHabitaciones();
            //titulo 
            //WindowLoaded(null, null);
            //lblTituloPanelMenu.Content = "Filtro de estados  de habitaciones ";

            categorias.Children.Clear();
            var scroll = new ScrollViewer
            {
                Height = categorias.Height,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };

            var contenedorEstados = new WrapPanel { Width = categorias.Width, Height = categorias.Height };


            scroll.Content = contenedorEstados;
            DockPanel.SetDock(scroll, Dock.Bottom);
            categorias.Children.Add(scroll);

            List<HABITACIONES_ESTADO> listaHabitacionesEstado = NegHabitaciones.ListaEstadosdeHabitacion();
            foreach (var estado in listaHabitacionesEstado)
            {
                var panel = new DockPanel { Height = 20, Width = 110, Margin = new Thickness(2, 2, 2, 2) };
                var imagen = new Image { Height = 16, Width = 16 };
                DockPanel.SetDock(imagen, Dock.Left);
                imagen.Source = estado.HES_IMAGEN != null ? new BitmapImage(new Uri(@"imagenes\" + estado.HES_IMAGEN.Trim(), UriKind.RelativeOrAbsolute)) :
                                                            new BitmapImage(new Uri(@"imagenes\default.png", UriKind.RelativeOrAbsolute));
                panel.Children.Add(imagen);

                //creo el borde que contendra al boton
                var bordePanel = new Border
                {
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1, 2, 1, 2),
                    Background = _gradienteBotonA,
                    Padding = new Thickness(1),
                    CornerRadius = new CornerRadius(15),
                    Margin = new Thickness(2, 4, 2, 2)
                };
                //creo el efecto de resplandor exterior para el borde
                var bordeEfectoGlow = new DropShadowEffect
                {
                    Color = Color.FromRgb(245, 240, 240),
                    Direction = 280,
                    ShadowDepth = 5,
                    BlurRadius = 9,
                    Opacity = 0.3
                };

                bordePanel.Effect = bordeEfectoGlow;
                //añado el efecto de roll over
                bordePanel.Tag = "A";
                bordePanel.MouseEnter += delegate { BotonMouseEnter(bordePanel); };
                bordePanel.MouseLeave += delegate { BotonMouseLeave(bordePanel); };

                DockPanel.SetDock(bordePanel, Dock.Top);

                var labelNombre = new Label
                {
                    Height = 20,
                    Width = 94,
                    MaxWidth = 94,
                    Background = Brushes.Transparent,
                    Content = estado.HES_NOMBRE,
                    Tag = estado.HES_CODIGO,
                    FontSize = 9
                };
                labelNombre.MouseDown += delegate { FiltrarPorEstado(labelNombre); };
                DockPanel.SetDock(labelNombre, Dock.Top);

                panel.Children.Add(labelNombre);

                bordePanel.Child = panel;

                contenedorEstados.Children.Add(bordePanel);
            }

            //{

            //    var panel = new DockPanel { Height = 20, Width = 110, Margin = new Thickness(2, 2, 2, 2) };
            //    var imagen = new Image { Height = 16, Width = 16 };
            //    DockPanel.SetDock(imagen, Dock.Left);
            //    imagen.Source = new BitmapImage(new Uri(@"imagenes\" + "ConEpicrisis.png", UriKind.RelativeOrAbsolute));
            //    panel.Children.Add(imagen);

            //    //creo el borde que contendra al boton
            //    var bordePanel = new Border
            //    {
            //        BorderBrush = Brushes.Gray,
            //        BorderThickness = new Thickness(1, 2, 1, 2),
            //        Background = _gradienteBotonA,
            //        Padding = new Thickness(1),
            //        CornerRadius = new CornerRadius(15),
            //        Margin = new Thickness(2, 4, 2, 2)
            //    };
            //    //creo el efecto de resplandor exterior para el borde
            //    var bordeEfectoGlow = new DropShadowEffect
            //    {
            //        Color = Color.FromRgb(245, 240, 240),
            //        Direction = 280,
            //        ShadowDepth = 5,
            //        BlurRadius = 9,
            //        Opacity = 0.3
            //    };

            //    bordePanel.Effect = bordeEfectoGlow;
            //    //añado el efecto de roll over
            //    bordePanel.Tag = "A";
            //    bordePanel.MouseEnter += delegate { BotonMouseEnter(bordePanel); };
            //    bordePanel.MouseLeave += delegate { BotonMouseLeave(bordePanel); };

            //    DockPanel.SetDock(bordePanel, Dock.Top);

            //    var labelNombre = new Label
            //    {
            //        Height = 20,
            //        Width = 94,
            //        MaxWidth = 94,
            //        Background = Brushes.Transparent,
            //        Content = "Con Epicrisis",
            //        Tag = 99,
            //        FontSize = 9
            //    };
            //    labelNombre.MouseDown += delegate { FiltrarPorEstado(labelNombre); };
            //    DockPanel.SetDock(labelNombre, Dock.Top);

            //    panel.Children.Add(labelNombre);

            //    bordePanel.Child = panel;

            //    contenedorEstados.Children.Add(bordePanel);
            //}
            //{

            //    var panel = new DockPanel { Height = 20, Width = 110, Margin = new Thickness(2, 2, 2, 2) };
            //    var imagen = new Image { Height = 16, Width = 16 };
            //    DockPanel.SetDock(imagen, Dock.Left);
            //    imagen.Source = new BitmapImage(new Uri(@"imagenes\" + "SinEpicrisis.jpg", UriKind.RelativeOrAbsolute));
            //    panel.Children.Add(imagen);

            //    //creo el borde que contendra al boton
            //    var bordePanel = new Border
            //    {
            //        BorderBrush = Brushes.Gray,
            //        BorderThickness = new Thickness(1, 2, 1, 2),
            //        Background = _gradienteBotonA,
            //        Padding = new Thickness(1),
            //        CornerRadius = new CornerRadius(15),
            //        Margin = new Thickness(2, 4, 2, 2)
            //    };
            //    //creo el efecto de resplandor exterior para el borde
            //    var bordeEfectoGlow = new DropShadowEffect
            //    {
            //        Color = Color.FromRgb(245, 240, 240),
            //        Direction = 280,
            //        ShadowDepth = 5,
            //        BlurRadius = 9,
            //        Opacity = 0.3
            //    };

            //    bordePanel.Effect = bordeEfectoGlow;
            //    //añado el efecto de roll over
            //    bordePanel.Tag = "A";
            //    bordePanel.MouseEnter += delegate { BotonMouseEnter(bordePanel); };
            //    bordePanel.MouseLeave += delegate { BotonMouseLeave(bordePanel); };

            //    DockPanel.SetDock(bordePanel, Dock.Top);

            //    var labelNombre = new Label
            //    {
            //        Height = 20,
            //        Width = 94,
            //        MaxWidth = 94,
            //        Background = Brushes.Transparent,
            //        Content = "Sin Epicrisis",
            //        Tag = 100,
            //        FontSize = 9
            //    };
            //    labelNombre.MouseDown += delegate { FiltrarPorEstado(labelNombre); };
            //    DockPanel.SetDock(labelNombre, Dock.Top);

            //    panel.Children.Add(labelNombre);

            //    bordePanel.Child = panel;

            //    contenedorEstados.Children.Add(bordePanel);

            //}
        }

        public DataTable PisoBodega = new DataTable();
        private void CargarArbolHabitaciones()
        {
            var padre = new TreeViewItem { FontSize = 12, Header = "HABITACIONES", Tag = "0" };
            arbolHabitaciones.Items.Add(padre);
            //WindowLoaded(null, null);
            try
            {
                /*RECUPERA LA IP DE LA MAQUINA / GIOVANNY TAPIA / 04/02/2013*/

                string hostName = Dns.GetHostName();
                string ipaddress = Dns.GetHostByName(hostName).AddressList[0].ToString();

                /***********************************************************************/

                Piso = NegHabitaciones.RecuperaCodigoPiso(ipaddress);
                PisoBodega = NegHabitaciones.RecuperarPisoBodega(ipaddress);

                if (Piso == 99)
                {
                    List<NIVEL_PISO> pisos = NegHabitaciones.listaNivelesPiso();
                    foreach (var piso in pisos)
                    {
                        var hijo = new TreeViewItem { FontSize = 12, Header = piso.NIV_NOMBRE, Tag = piso.NIV_CODIGO.ToString() };
                        padre.Items.Add(hijo);
                    }
                }
                else
                {
                    List<NIVEL_PISO> pisos = NegHabitaciones.listaNivelesPiso(Piso);
                    foreach (var piso in pisos)
                    {
                        var hijo = new TreeViewItem { FontSize = 12, Header = piso.NIV_NOMBRE, Tag = piso.NIV_CODIGO.ToString() };
                        padre.Items.Add(hijo);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Metodo que carga la estructura general de habitaciones 
        /// </summary>
        /// <param name="habitacionesLista">Lista de habitaciones que se mostraran</param>
        private void CargarHabitacionesVistaGen(IEnumerable<HABITACIONES> habitacionesLista)
        {
            // WindowLoaded(null, null);
            habitaciones.Children.Clear();
            var scroll = new ScrollViewer
            {
                Height = habitaciones.ActualHeight,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            var contenedor = new WrapPanel { Width = habitaciones.ActualWidth };
            scroll.Content = contenedor;
            habitaciones.Children.Add(scroll);
            //agrego cada una de las habitaciones de la lista

            if (Piso != 99) /*Para filtrar cuando se establece un piso / Giovanny Tapia / 04/02/2013*/
            {
                habitacionesLista = NegHabitaciones.listaHabitaciones(Piso).ToList();
            }

            foreach (var habitacion in habitacionesLista)
            {
                var panel = new DockPanel { Height = 60, Width = 100, Margin = new Thickness(4, 4, 4, 4) };
                var imagen = new Image { Height = 60, Width = 60 };
                DockPanel.SetDock(imagen, Dock.Right);
                //agrego la imagen que identifica al estado
                imagen.Source = habitacion.HABITACIONES_ESTADO.HES_IMAGEN != null ? new BitmapImage(new Uri(@"imagenes\" + habitacion.HABITACIONES_ESTADO.HES_IMAGEN.Trim(), UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"imagenes\default.png", UriKind.RelativeOrAbsolute));
                //agrego el menu contextual
                var mainMenu = new ContextMenu { Background = _gradienteBotonA };
                panel.ContextMenu = mainMenu;

                //menu habitacion alta programada
                var mniAltaProg = new MenuItem { Header = "Alta Programada", Background = _gradienteBotonA };
                //menu habitacion cuenta cancelada
                var mniCuentaCan = new MenuItem { Header = "Cuenta Cancelada", Background = _gradienteBotonA };
                //menu habitacion desocupada
                var mniDesocupada = new MenuItem { Header = "Asignada", Background = _gradienteBotonA };
                // menu habitacion en limpieza 
                var mniLimpieza = new MenuItem { Header = "En limpieza y desinfección", Background = _gradienteBotonA };
                //menu habitacion disponible
                var mniDisponible = new MenuItem { Header = "Disponible", Background = _gradienteBotonA };
                //menu revertir habitacion
                var menuRevertirCambio = new MenuItem
                {
                    Tag = habitacion,
                    Header = "Revertir Estado De Habitación"
                };
                //menu mover paciente
                //var menuCambioHabitacion = new MenuItem { Tag = habitacion, Header = "Mover Paciente" };
                //se activa o desactiva los estados de la habitacion de acuerdo al estado actual de la misma
                if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionAlta())
                    mniAltaProg.IsEnabled = false;
                if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionCancelada())
                    mniCuentaCan.IsEnabled = false;
                if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionDesocupada())
                    mniDesocupada.IsEnabled = false;
                if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionEnLimpieza())
                    mniLimpieza.IsEnabled = false;

                //Menu que muestra la ventana de Detalle de la habitacion
                var mnuInfHabitacion = new Menu
                {
                    Background = _gradienteBotonA,
                    Width = panel.Width,
                    Height = 20
                };
                DockPanel.SetDock(mnuInfHabitacion, Dock.Left);
                var item = new MenuItem { Header = habitacion.hab_Numero, Tag = habitacion };
                item.Click += delegate { MostrarInfHabitacion((HABITACIONES)item.Tag); };
                imagen.MouseEnter += delegate { MostrarNomPaciente((HABITACIONES)item.Tag, imagen); };
                //WindowLoaded(null, null);
                mnuInfHabitacion.Items.Add(item);

                panel.Children.Add(imagen);
                panel.Children.Add(mnuInfHabitacion);

                contenedor.Children.Add(panel);
                //memu Cambio de estado
                var menuCambioEstado = new MenuItem { Tag = habitacion, Header = "Cambio de Estado" };
                if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 7 || Sesion.codDepartamento == 10 || Sesion.codDepartamento == 2 || Sesion.codDepartamento == 4 || Sesion.codDepartamento == 5 || Sesion.codDepartamento == 12)
                {
                    mainMenu.Items.Add(menuCambioEstado);
                    if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionDisponible())
                        menuCambioEstado.IsEnabled = false;
                    mniAltaProg.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionAlta()); };
                    menuCambioEstado.Items.Add(mniAltaProg);
                    mniCuentaCan.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionCancelada()); };
                    menuCambioEstado.Items.Add(mniCuentaCan);
                    mniDesocupada.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionDesocupada()); };
                    menuCambioEstado.Items.Add(mniDesocupada);
                    mniLimpieza.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionEnLimpieza()); };
                    menuCambioEstado.Items.Add(mniLimpieza);
                    mniDisponible.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionDisponible()); };
                    menuCambioEstado.Items.Add(mniDisponible);
                    menuRevertirCambio.Click += delegate { RecertirEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag); };
                    mainMenu.Items.Add(menuRevertirCambio);
                }
                if (Sesion.codDepartamento != 6)
                {
                    //menuCambioHabitacion.Click += delegate { MoverPacienteHabitacion((HABITACIONES)menuCambioEstado.Tag); };
                    //mainMenu.Items.Add(menuCambioHabitacion);
                }
            }
        }
        /// <summary>
        /// Metodo que carga la estructura general de habitaciones que tienen epicrisis 
        /// </summary>
        /// <param name="habitacionesLista">Lista de habitaciones que se mostraran</param>
        /// 
        private void CargaHabitacionesEpicrisis(IEnumerable<HABITACIONES> habitacionesLista)
        {
            bool Resultado = true;
            habitaciones.Children.Clear();
            var scroll = new ScrollViewer
            {
                Height = habitaciones.ActualHeight,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            var contenedor = new WrapPanel { Width = habitaciones.ActualWidth };
            scroll.Content = contenedor;
            habitaciones.Children.Add(scroll);
            //agrego cada una de las habitaciones de la lista

            if (Piso != 99) /*Para filtrar cuando se establece un piso / Giovanny Tapia / 04/02/2013*/
            {
                habitacionesLista = NegHabitaciones.listaHabitaciones(Piso).ToList();
            }

            foreach (var habitacion in habitacionesLista)
            {

                Resultado = NegHabitaciones.VerificaEpicrisis(habitacion.hab_Numero);

                if (Resultado == true)
                {

                    var panel = new DockPanel { Height = 60, Width = 100, Margin = new Thickness(4, 4, 4, 4) };
                    var imagen = new Image { Height = 60, Width = 60 };
                    DockPanel.SetDock(imagen, Dock.Right);
                    //agrego la imagen que identifica al estado
                    imagen.Source = habitacion.HABITACIONES_ESTADO.HES_IMAGEN != null ? new BitmapImage(new Uri(@"imagenes\" + habitacion.HABITACIONES_ESTADO.HES_IMAGEN.Trim(), UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"imagenes\default.png", UriKind.RelativeOrAbsolute));
                    //agrego el menu contextual
                    var mainMenu = new ContextMenu { Background = _gradienteBotonA };
                    panel.ContextMenu = mainMenu;
                    //memu Cambio de estado
                    var menuCambioEstado = new MenuItem { Tag = habitacion, Header = "Cambio de Estado" };
                    if (Sesion.codDepartamento != 6)
                    {
                        mainMenu.Items.Add(menuCambioEstado);
                        //menu habitacion alta programada
                        var mniAltaProg = new MenuItem { Header = "Alta Programada", Background = _gradienteBotonA };
                        mniAltaProg.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionAlta()); };
                        menuCambioEstado.Items.Add(mniAltaProg);
                        //menu habitacion cuenta cancelada
                        var mniCuentaCan = new MenuItem { Header = "Cuenta Cancelada", Background = _gradienteBotonA };
                        mniCuentaCan.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionCancelada()); };
                        menuCambioEstado.Items.Add(mniCuentaCan);
                        //menu habitacion desocupada
                        var mniDesocupada = new MenuItem { Header = "Asignada", Background = _gradienteBotonA };
                        mniDesocupada.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionDesocupada()); };
                        menuCambioEstado.Items.Add(mniDesocupada);
                        // menu habitacion en limpieza 
                        var mniLimpieza = new MenuItem { Header = "En limpieza y desinfección", Background = _gradienteBotonA };
                        mniLimpieza.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionEnLimpieza()); };
                        menuCambioEstado.Items.Add(mniLimpieza);
                        //menu habitacion disponible
                        var mniDisponible = new MenuItem { Header = "Disponible", Background = _gradienteBotonA };
                        mniDisponible.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionDisponible()); };
                        menuCambioEstado.Items.Add(mniDisponible);
                        //menu revertir habitacion
                        //var menuRevertirCambio = new MenuItem
                        //{
                        //    Tag = habitacion,
                        //    Header = "Revertir estado de la habitación"
                        //};
                        //menuRevertirCambio.Click += delegate { RecertirEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag); };
                        //mainMenu.Items.Add(menuRevertirCambio);
                        //menu mover paciente
                        //var menuCambioHabitacion = new MenuItem { Tag = habitacion, Header = "Mover Paciente" };
                        //menuCambioHabitacion.Click += delegate { MoverPacienteHabitacion((HABITACIONES)menuCambioEstado.Tag); };
                        //mainMenu.Items.Add(menuCambioHabitacion);
                        //se activa o desactiva los estados de la habitacion de acuerdo al estado actual de la misma
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionAlta())
                            mniAltaProg.IsEnabled = false;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionCancelada())
                            mniCuentaCan.IsEnabled = false;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionDesocupada())
                            mniDesocupada.IsEnabled = false;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionEnLimpieza())
                            mniLimpieza.IsEnabled = false;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionDisponible())
                            menuCambioEstado.IsEnabled = false;
                        //Menu que muestra la ventana de Detalle de la habitacion
                        var mnuInfHabitacion = new Menu
                        {
                            Background = _gradienteBotonA,
                            Width = panel.Width,
                            Height = 20
                        };
                        DockPanel.SetDock(mnuInfHabitacion, Dock.Left);
                        var item = new MenuItem { Header = habitacion.hab_Numero, Tag = habitacion };
                        item.Click += delegate { MostrarInfHabitacion((HABITACIONES)item.Tag); };
                        imagen.MouseEnter += delegate { MostrarNomPaciente((HABITACIONES)item.Tag, imagen); };
                        //WindowLoaded(null, null);
                        mnuInfHabitacion.Items.Add(item);

                        panel.Children.Add(imagen);
                        panel.Children.Add(mnuInfHabitacion);

                        contenedor.Children.Add(panel);
                    }
                }
            }
        } // -->> Carga las habitaciones que tienen epicrisis / Giovanny Tapia / 12/09/2012
        /// <summary>
        /// Metodo que carga la estructura general de habitaciones que no tienen epicrisis 
        /// </summary>
        /// <param name="habitacionesLista">Lista de habitaciones que se mostraran</param>
        /// 
        private void CargaHabitacionesSinEpicrisis(IEnumerable<HABITACIONES> habitacionesLista)
        {
            bool Resultado = true;
            habitaciones.Children.Clear();
            var scroll = new ScrollViewer
            {
                Height = habitaciones.ActualHeight,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            var contenedor = new WrapPanel { Width = habitaciones.ActualWidth };
            scroll.Content = contenedor;
            habitaciones.Children.Add(scroll);
            //agrego cada una de las habitaciones de la lista

            if (Piso != 99) /*Para filtrar cuando se establece un piso / Giovanny Tapia / 04/02/2013*/
            {
                habitacionesLista = NegHabitaciones.listaHabitaciones(Piso).ToList();
            }

            foreach (var habitacion in habitacionesLista)
            {

                Resultado = NegHabitaciones.VerificaEpicrisis(habitacion.hab_Numero);

                if (Resultado == false)
                {

                    var panel = new DockPanel { Height = 60, Width = 100, Margin = new Thickness(4, 4, 4, 4) };
                    var imagen = new Image { Height = 60, Width = 60 };
                    DockPanel.SetDock(imagen, Dock.Right);
                    //agrego la imagen que identifica al estado
                    imagen.Source = habitacion.HABITACIONES_ESTADO.HES_IMAGEN != null ? new BitmapImage(new Uri(@"imagenes\" + habitacion.HABITACIONES_ESTADO.HES_IMAGEN.Trim(), UriKind.RelativeOrAbsolute)) : new BitmapImage(new Uri(@"imagenes\default.png", UriKind.RelativeOrAbsolute));
                    //agrego el menu contextual
                    var mainMenu = new ContextMenu { Background = _gradienteBotonA };
                    panel.ContextMenu = mainMenu;
                    //memu Cambio de estado
                    var menuCambioEstado = new MenuItem { Tag = habitacion, Header = "Cambio de Estado" };
                    if (Sesion.codDepartamento != 6)
                    {
                        mainMenu.Items.Add(menuCambioEstado);
                        //menu habitacion alta programada
                        var mniAltaProg = new MenuItem { Header = "Alta Programada", Background = _gradienteBotonA };
                        mniAltaProg.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionAlta()); };
                        menuCambioEstado.Items.Add(mniAltaProg);
                        //menu habitacion cuenta cancelada
                        var mniCuentaCan = new MenuItem { Header = "Cuenta Cancelada", Background = _gradienteBotonA };
                        mniCuentaCan.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionCancelada()); };
                        menuCambioEstado.Items.Add(mniCuentaCan);
                        //menu habitacion desocupada
                        var mniDesocupada = new MenuItem { Header = "Asignada", Background = _gradienteBotonA };
                        mniDesocupada.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionDesocupada()); };
                        menuCambioEstado.Items.Add(mniDesocupada);
                        // menu habitacion en limpieza 
                        var mniLimpieza = new MenuItem { Header = "En limpieza y desinfección", Background = _gradienteBotonA };
                        mniLimpieza.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionEnLimpieza()); };
                        menuCambioEstado.Items.Add(mniLimpieza);
                        //menu habitacion disponible
                        var mniDisponible = new MenuItem { Header = "Disponible", Background = _gradienteBotonA };
                        mniDisponible.Click += delegate { CambiarEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag, AdmisionParametros.getEstadoHabitacionDisponible()); };
                        menuCambioEstado.Items.Add(mniDisponible);
                        //menu revertir habitacion
                        //var menuRevertirCambio = new MenuItem
                        //{
                        //    Tag = habitacion,
                        //    Header = "Revertir estado de la habitación"
                        //};
                        //menuRevertirCambio.Click += delegate { RecertirEstadoHabitacion((HABITACIONES)menuCambioEstado.Tag); };
                        //mainMenu.Items.Add(menuRevertirCambio);
                        //menu mover paciente
                        //var menuCambioHabitacion = new MenuItem { Tag = habitacion, Header = "Mover Paciente" };
                        //menuCambioHabitacion.Click += delegate { MoverPacienteHabitacion((HABITACIONES)menuCambioEstado.Tag); };
                        //mainMenu.Items.Add(menuCambioHabitacion);
                        //se activa o desactiva los estados de la habitacion de acuerdo al estado actual de la misma
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionAlta())
                            mniAltaProg.IsEnabled = false;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionCancelada())
                            mniCuentaCan.IsEnabled = false;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionDesocupada())
                            mniDesocupada.IsEnabled = false;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionEnLimpieza())
                            mniLimpieza.IsEnabled = false;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionDisponible())
                            menuCambioEstado.IsEnabled = false;
                        //Menu que muestra la ventana de Detalle de la habitacion
                        var mnuInfHabitacion = new Menu
                        {
                            Background = _gradienteBotonA,
                            Width = panel.Width,
                            Height = 20
                        };
                        DockPanel.SetDock(mnuInfHabitacion, Dock.Left);
                        var item = new MenuItem { Header = habitacion.hab_Numero, Tag = habitacion };
                        item.Click += delegate { MostrarInfHabitacion((HABITACIONES)item.Tag); };
                        imagen.MouseEnter += delegate { MostrarNomPaciente((HABITACIONES)item.Tag, imagen); };
                        //WindowLoaded(null, null);

                        mnuInfHabitacion.Items.Add(item);

                        panel.Children.Add(imagen);
                        panel.Children.Add(mnuInfHabitacion);

                        contenedor.Children.Add(panel);
                    }
                }
            }
        } // -->> Carga las habitaciones que no tienen epicrisis / Giovanny Tapia / 12/09/2012
        /// <summary>
        /// Metodo que carga la estructura detallada de habitaciones
        /// </summary>
        /// <param name="habitacionesLista">Lista de habitaciones que se mostraran</param>
        private void CargarHabitacionesVistaDet(IEnumerable<HABITACIONES> habitacionesLista)
        {
            //WindowLoaded(null, null);
            List<HABITACIONES_ATENCION_VISTA> infHabitaciones = NegHabitaciones.RecuperarDetallesHabitacion(null, null, null, null, null, null, AdmisionParametros.getEstadoHabitacionOcupado().ToString(), null, "false").OrderByDescending(h => h.HAD_FECHA_INGRESO).ToList();
            habitaciones.Children.Clear();
            var scroll = new ScrollViewer
            {
                Height = habitaciones.ActualHeight,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            var contenedor = new WrapPanel { Width = habitaciones.ActualWidth };
            scroll.Content = contenedor;
            habitaciones.Children.Add(scroll);
            foreach (var habitacion in habitacionesLista)
            {
                DockPanel panel = new DockPanel();
                panel.Margin = new Thickness(4, 4, 4, 4);
                panel.Background = _gradienteVentana;
                //
                ContextMenu mainMenu = new ContextMenu();
                panel.ContextMenu = mainMenu;
                MenuItem item1 = new MenuItem();
                item1.Header = "Mover";
                item1.Tag = habitacion.hab_Numero.ToString();
                item1.Foreground = Brushes.Black;
                item1.Background = Brushes.Transparent;
                mainMenu.Items.Add(item1);
                //
                var barra = new StatusBar { Height = 25, Width = panel.Width };
                DockPanel.SetDock(barra, Dock.Bottom);
                barra.Background = _gradienteVentana;
                var imagen = new Image { Height = 23, Width = 30 };
                if (habitacion.HABITACIONES_ESTADO.HES_IMAGEN != null)
                {
                    imagen.Source = new BitmapImage(new Uri(@"imagenes\" + habitacion.HABITACIONES_ESTADO.HES_IMAGEN.Trim(), UriKind.RelativeOrAbsolute));
                }
                else
                {
                    imagen.Source = new BitmapImage(new Uri(@"imagenes\default.png", UriKind.RelativeOrAbsolute));
                }
                barra.Items.Add(imagen);
                panel.Children.Add(barra);

                //
                Menu menu = new Menu();
                menu.Width = panel.Width;
                menu.Height = 20;
                menu.Background = Brushes.DarkGray;
                DockPanel.SetDock(menu, Dock.Top);
                MenuItem item = new MenuItem();
                item.Header = "Habitacion " + habitacion.hab_Numero;
                item.Tag = habitacion;
                item.Click += delegate { MostrarInfHabitacion((HABITACIONES)item.Tag); };
                //Image imagen = new Image(Archivo.imgBtnDocuments_open );
                //item.Items.Add(imagen);  
                menu.Items.Add(item);

                panel.Children.Add(menu);

                //
                TextBlock texto = new TextBlock();
                texto.Margin = new Thickness(2, 2, 2, 2);
                DockPanel.SetDock(texto, Dock.Left);
                panel.Children.Add(texto);
                if ((habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionOcupado()) || (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionAlta()) || (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionCancelada()))
                {
                    var habitacionDetalle = (from i in infHabitaciones
                                             where i.hab_Codigo == habitacion.hab_Codigo
                                             select i).FirstOrDefault();
                    texto.FontSize = 10;
                    if (habitacionDetalle != null)
                    {
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionOcupado())
                        {
                            texto.Text = "Paciente: " + habitacionDetalle.PACIENTE + "\n" +
                                        "HC: " + habitacionDetalle.PAC_HISTORIA_CLINICA + "\n" +
                                        "# Atención: " + habitacionDetalle.ATE_CODIGO + "\n" +
                                        "Fec. Ingreso: " + habitacionDetalle.HAD_FECHA_INGRESO + "\n" +
                                        "Medico: " + habitacionDetalle.MED_NOMBRE + "\n" +
                                        "Especialidad: " + habitacionDetalle.ESP_NOMBRE;
                        }
                        else if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionAlta())
                        {
                            texto.Text = "Paciente: " + habitacionDetalle.PACIENTE + "\n" +
                                        "HC: " + habitacionDetalle.PAC_HISTORIA_CLINICA + "\n" +
                                        "# Atención: " + habitacionDetalle.ATE_CODIGO + "\n" +
                                        "Fec. Ingreso: " + habitacionDetalle.HAD_FECHA_INGRESO + "\n" +
                                        "Medico: " + habitacionDetalle.MED_NOMBRE + "\n" +
                                        "Diagnostico: " + habitacionDetalle.ATE_DIAGNOSTICO_FINAL;
                        }
                        else if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionCancelada())
                        {
                            texto.Text = "Paciente: " + habitacionDetalle.PACIENTE + "\n" +
                                        "HC: " + habitacionDetalle.PAC_HISTORIA_CLINICA + "\n" +
                                        "# Atención: " + habitacionDetalle.ATE_CODIGO + "\n" +
                                        "Fec. Ingreso: " + habitacionDetalle.HAD_FECHA_INGRESO + "\n" +
                                        "Medico: " + habitacionDetalle.MED_NOMBRE + "\n" +
                                        "Fec Facturación: " + habitacionDetalle.HAD_FECHA_FACTURACION +
                                        "Diagnostico: " + habitacionDetalle.ATE_DIAGNOSTICO_FINAL;
                        }
                    }
                    panel.Height = 120;
                    panel.Width = 200;
                }
                else
                {
                    Image porDefecto = new Image();
                    porDefecto.Source = new BitmapImage(new Uri(@"imagenes\porDefecto.png", UriKind.RelativeOrAbsolute));
                    panel.Children.Add(porDefecto);
                    panel.Height = 120;
                    panel.Width = 96;
                }

                contenedor.Children.Add(panel);
            }
        }

        #endregion

        #region Metodos Generales
        private void MostrarNomPaciente(HABITACIONES habitacion, Image imagen)
        {
            try
            {
                if (string.IsNullOrEmpty((string)imagen.ToolTip))
                {
                    var pacientesInf = _pacientes.Where(p => p.codigoHabitacion == habitacion.hab_Codigo).OrderByDescending(p => p.fechaIngreso).Select(p => p.fechaIngreso + " \t " + p.historiaClincia + " \t " + p.nombrePaciente + " \t " + p.aseguradora).FirstOrDefault();
                    string mesTemporal = "Fecha de ingreso     \t Hs. Clinica \t Nombre del Paciente \t                              Aseguradora \n" + pacientesInf;
                    imagen.ToolTip = mesTemporal;
                    var otr0 = _pacientes.Where(p => p.codigoHabitacion == habitacion.hab_Codigo).OrderByDescending(p => p.fechaIngreso).Select(p => p.historiaClincia).FirstOrDefault();

                }

            }
            catch (Exception err) { MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        #endregion

        #region Metodos Informacion Habitacion
        /// <summary>
        /// Muestra la ventana con las opciones y información general de cada habitación
        /// </summary>
        /// <param name="habitacion">Habitacion de la que se mostrara el detalle</param>
        private void MostrarInfHabitacion(HABITACIONES habitacion)
        {
            try
            {
                LimpiarCamposDetInfHabitacion();
                InciaControlesDetInfHabitacion();
                tabgeneral.Focus();

                if ((habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionOcupado()))
                {
                    //var detalleHabitacion1 = NegHabitaciones.RecuperarDetallesHabitacion(null, null, habitacion.hab_Codigo.ToString(), null, null, null, null, null, "false").OrderByDescending(h => h.HAD_FECHA_INGRESO).FirstOrDefault();
                    var detalleHabitacion = _pacientes.Where(p => p.codigoHabitacion == habitacion.hab_Codigo).OrderByDescending(p => p.fechaIngreso).FirstOrDefault();
                    infHabitacion.Visibility = Visibility.Visible;
                    infHabitacion.Margin = new Thickness((Width / 2 - infHabitacion.Width / 2), (100), 0, 0);
                    superior -= 5;
                    if (detalleHabitacion == null)
                    {
                        detalleHabitacion = NegPacientes.RecuperarPacientesAtencionUltimas(1, null, null, habitacion.hab_Codigo).FirstOrDefault();
                        if (detalleHabitacion == null)
                            return;
                    }

                    //recupero la atencion 
                    _atencionActiva = null;
                    _atencionActiva = NegAtenciones.RecuperarAtencionID(detalleHabitacion.codAtencion);
                    string aux = "";
                    DataTable diagnosticoReferido = new DataTable();
                    diagnosticoReferido = NegAtenciones.RecuperaReferidoDiagnostico(detalleHabitacion.codAtencion);
                    DataTable fechaNacimiento = new DataTable();
                    fechaNacimiento = NegAtenciones.RecuperaFechaNacimiento(detalleHabitacion.historiaClincia);
                    //cambio el color si esta en quirofano
                    if (_atencionActiva.ATE_EN_QUIROFANO != null)
                    {
                        btnEnQuirofano.Background = _atencionActiva.ATE_EN_QUIROFANO == true ? Brushes.Red : Brushes.AliceBlue;
                    }
                    else
                        btnEnQuirofano.Background = Brushes.AliceBlue;

                    xamTxtPaciente.Text = detalleHabitacion.nombrePaciente;
                    xamTxtHC.Text = detalleHabitacion.historiaClincia;
                    xamTxtNumAtencion.Text = detalleHabitacion.codAtencion.ToString();
                    xamTxtFecIngreso.Text = detalleHabitacion.fechaIngreso.ToString();
                    xamTxtMedicoTratante.Text = detalleHabitacion.medicoTratante;
                    xamTxtAseguradora.Text = detalleHabitacion.aseguradora;
                    xamTxtHabitacion.Text = detalleHabitacion.numeroHabitacion;
                    xamTxtGenero.Text = detalleHabitacion.sexo;
                    xamTxtTipoTratamiento.Text = detalleHabitacion.tipoTratamiento;
                    xamTxtDiagnosticoFinal.Text = detalleHabitacion.diagnosticoInicial;
                    FechaNacimientoPaciente = Convert.ToDateTime(fechaNacimiento.Rows[0][0].ToString());
                    xamTxtDiagnosticoFinal.Text = diagnosticoReferido.Rows[0]["ATE_DIAGNOSTICO_INICIAL"].ToString();
                    xamTxtTipoReferido.Text = diagnosticoReferido.Rows[0]["TIR_NOMBRE"].ToString();
                    xamTxtEmail.Text = diagnosticoReferido.Rows[0]["PAC_EMAIL"].ToString();
                    xamTxtTipoTratamiento_Copy.Text = diagnosticoReferido.Rows[0]["ATE_OBSERVACIONES"].ToString();
                    //Recupero la información de los ultimos ingresos
                    var pacientesPorHab = _pacientes.Where(p => p.codigoHabitacion == habitacion.hab_Codigo).OrderByDescending(p => p.fechaIngreso).Select(p => new { p.fechaIngreso, p.historiaClincia, p.nombrePaciente }).ToList();
                    grdHistorial.ItemsSource = pacientesPorHab;
                    //Cargo el grid de Ingreso Salas
                    _listaIntervenciones = NegIngresoSalas.ListarPorAtencion(_atencionActiva.ATE_CODIGO);
                    xdpSalas.DataSource = _listaIntervenciones;
                    //Actualizo la información de los formularios de HC
                    ActualizarListaFormularios(detalleHabitacion.codAtencion, 0);
                    CboAreasSelectionChanged(null, null);
                    evolucion = NegEvolucion.recuperarEvolucionPorAtencion(_atencionActiva.ATE_CODIGO);
                    xdpObservaciones.DataSource = null;
                    if (evolucion != null)
                        xdpObservaciones.DataSource = NegEvolucionDetalle.listaNotasEvolucion(evolucion.EVO_CODIGO);
                    DataTable dietetica = new DataTable();
                    dietetica = NegEvolucion.Dietetica(detalleHabitacion.historiaClincia);
                    if (dietetica.Rows.Count > 0)
                    {
                        txtObservacionDieta.Text = dietetica.Rows[0][0].ToString();
                    }
                    List<RecuperaDietetica> _dietetica = new List<RecuperaDietetica>();
                    _dietetica = NegPacientes.RecuperaDietetica(detalleHabitacion.historiaClincia);
                    xamDataPacienteDieta.DataSource = _dietetica.ToList();
                    ateCodigo = _atencionActiva.ATE_CODIGO;
                    priemera = 0;
                }
                else
                {
                    if (priemera == 1)
                    {
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 2)
                        {
                            MessageBox.Show("No se puede modificar cuenta. \r\nEstado: \"Alta Programada\"", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 5)
                            MessageBox.Show("No se puede recuperar información de una habitación desocupada", "Inf", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    else
                    {
                        priemera = 0;
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 2)
                        {
                            MessageBox.Show("No se puede modificar cuenta. \r\nEstado: \"Alta Programada\"", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 5)
                            MessageBox.Show("No se puede recuperar información de una habitación desocupada", "Inf", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }
                if (_atencionActiva != null)
                {
                    cboAreas.Text = "TODAS LAS AREAS";

                }
                //xdpSolicitudMedicamentos.DataSource = NegPedidos.recuperarListaPedidos(atencionActiva.ATE_CODIGO, Parametros.FarmaciaPAR.PedidoMedicamentos);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void InciaControlesDetInfHabitacion()
        {
            //Inicializo parametros
            if (_lstMedicos == null)
            {
                _lstMedicos = NegCatalogos.RecuperarCatalogoListaMedicos(null);
                cboMedicos.ItemsSource = _lstMedicos;
                cboMedicosPedidos.ItemsSource = _lstMedicos;
                cboMedicos.DisplayMemberPath = "nombre";
                cboMedicos.SelectedValuePath = "codigo";
                cboMedicos.SelectedIndex = 0;
                cboMedicosPedidos.DisplayMemberPath = "nombre";
                cboMedicosPedidos.SelectedValuePath = "codigo";
                cboMedicosPedidos.SelectedIndex = 0;
            }
            else
            {
                cboMedicos.SelectedIndex = 0;
                cboMedicosPedidos.SelectedIndex = 0;
            }

            if (_lstSalas == null)
            {
                _lstSalas = NegCatalogos.RecuperarCatalogoListaSalas();
                trvSalas.ItemsSource = _lstSalas;
                trvSalas.DisplayMemberPath = "nombre";
                trvSalas.SelectedValuePath = "codigo";
            }
            else
            {
                trvSalas.Items.MoveCurrentToFirst();
            }
        }
        /// <summary>
        /// Limpia los campos de la pestaña de información general del paciente
        /// </summary>
        private void LimpiarCamposDetInfHabitacion()
        {
            //actualizo campos 
            xamTxtPaciente.Text = "";
            xamTxtHC.Text = "";
            xamTxtNumAtencion.Text = "";
            xamTxtFecIngreso.Text = "";
            xamTxtMedicoTratante.Text = "";
            xamTxtAseguradora.Text = "";
            xamTxtDiagnosticoFinal.Text = "";
            xamTxtHabitacion.Text = "";
            xamTxtTipoTratamiento.Text = "";
            xamTxtGenero.Text = "";
            lblusr.Content = "";
        }
        public bool ValidarFormularios(int codigoAtencion, int formulario, int CodigoFormulario) // Verifica si un formulario tiene datos / Giovanny Tapia / 21/09/2012
        {
            //AQUI SE DEBE VALIDAR TODOS LO QUE SE TENGA QUE ELIMINAR COMO: EVOLUCION, EPICRISIS... ENTRE OTRAS
            bool validar = false;
            if (formulario == His.Parametros.AdmisionParametros.getHcEvolucionPrescripciones())
            {
                HC_EVOLUCION formularioEv = NegEvolucion.recuperarEvolucionPorAtencion(codigoAtencion);
                if (formularioEv != null)
                {
                    List<HC_EVOLUCION_DETALLE> detalle = NegEvolucionDetalle.listaNotasEvolucion(formularioEv.EVO_CODIGO);
                    if (detalle.Count == 0)
                        validar = true;
                }
            }

            if (formulario == His.Parametros.AdmisionParametros.getHcAnamnesisExamenFisico())
            {
                HC_ANAMNESIS detalleA = NegAnamnesis.recuperarAnamnesisPorAtencion(codigoAtencion);

                if (detalleA == null)
                    validar = true;
            }
            if (formulario == His.Parametros.AdmisionParametros.getHcEpicrisis())
            {
                HC_EPICRISIS detalleE = NegEpicrisis.recuperarEpicrisisPorAtencion(codigoAtencion);

                if (detalleE == null)
                    validar = true;
            }

            if (formulario == His.Parametros.AdmisionParametros.getHcInterconsultaSolicitudInforme())
            {
                HC_INTERCONSULTA detalleIn = NegInterconsulta.recuperarInterconsulta(codigoAtencion);

                if (detalleIn == null)
                    validar = true;
            }
            if (formulario == His.Parametros.AdmisionParametros.getHcEmergencia())
            {
                HC_EMERGENCIA detalleEm = NegHcEmergencia.recuperaremergenciaPorAtencion(codigoAtencion);

                if (detalleEm == null)
                    validar = true;
            }

            if (formulario == His.Parametros.AdmisionParametros.getHcLaboratorioClinicoSolicitud())
            {
                try
                {
                    HC_LABORATORIO_CLINICO detallelab = NegLaboratorio.recuperarlaboratorioPorAtencion(codigoAtencion);

                    if (detallelab == null)
                        validar = true;
                }
                catch
                {
                }

            }
            if (formulario == 21) // Protocolos Operatorios / Giovanny Tapia / 21/09/2012
            {
                HC_PROTOCOLO_OPERATORIO detalleope = NegProtocoloOperatorio.recuperarProtocolo(codigoAtencion, CodigoFormulario); // sELECCIONO EL LISTADO DE PROTOCOLOS OPERATORIO / GIOVANNY TAPIA / 21/09/2012

                if (detalleope == null)
                    validar = true;
            }

            return validar;

        }
        private void ActualizarListaFormularios(int codigoAtencion, int codigoPaciente)
        {
            try
            {
                var marco = new Canvas { Width = stackPanelHC.Width - 18, Height = stackPanelHC.Height - 10 };
                stackPanelHC.Children.Clear();
                var scroll = new ScrollViewer
                { Height = marco.Height, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
                var contenedorFormularios = new WrapPanel { Width = marco.Width, Height = marco.Height };
                scroll.Content = contenedorFormularios;
                //marco.Children.Add(scroll );
                stackPanelHC.Children.Add(scroll);
                List<ATENCION_DETALLE_FORMULARIOS_HCU> listaDetalleFormularios = NegAtencionDetalleFormulariosHCU.listaAtencionDetalleFormularios(codigoAtencion).OrderBy(f => f.FORMULARIOS_HCU.FH_NOMBRE).ToList();
                foreach (var item in listaDetalleFormularios)
                {

                    if (ValidarFormularios(codigoAtencion, item.FORMULARIOS_HCU.FH_CODIGO, item.ADF_CODIGO) == false)// Verifica si un formulario tiene datos o no / Giovanny Tapia / 21/09/2012
                    {
                        var bordeBoton = new Border
                        {
                            BorderBrush = Brushes.Gray,
                            BorderThickness = new Thickness(1, 2, 1, 2),
                            Background = _gradienteBotonB,
                            Padding = new Thickness(1),
                            CornerRadius = new CornerRadius(4),
                            Margin = new Thickness(2, 4, 2, 4),
                            Tag = "B",
                            Width = 200
                        };

                        bordeBoton.MouseEnter += delegate { BotonMouseEnter(bordeBoton); };
                        bordeBoton.MouseLeave += delegate { BotonMouseLeave(bordeBoton); };

                        var formulario = new Label { Content = item.FORMULARIOS_HCU.FH_NOMBRE };

                        bordeBoton.Tag = item;
                        bordeBoton.Child = formulario;
                        bordeBoton.MouseDown += delegate { cargarFormulario(bordeBoton); };
                        bordeBoton.MouseEnter += delegate { BotonMouseEnter(bordeBoton); };
                        bordeBoton.MouseLeave += delegate { BotonMouseLeave(bordeBoton); };
                        contenedorFormularios.Children.Add(bordeBoton);
                        colorControl = 1;
                    }

                    //////////aqui

                    if (ValidarFormularios(codigoAtencion, item.FORMULARIOS_HCU.FH_CODIGO, item.ADF_CODIGO) == true)
                    {
                        var bordeBoton = new Border
                        {
                            BorderBrush = Brushes.Red,
                            BorderThickness = new Thickness(1, 2, 1, 2),
                            Background = Brushes.Red,
                            Padding = new Thickness(1),
                            CornerRadius = new CornerRadius(4),
                            Margin = new Thickness(2, 4, 2, 4),
                            Tag = "B",
                            Width = 200
                        };

                        bordeBoton.MouseEnter += delegate { BotonMouseEnter(bordeBoton); };
                        bordeBoton.MouseLeave += delegate { BotonMouseLeave1(bordeBoton); };

                        var formulario = new Label { Content = item.FORMULARIOS_HCU.FH_NOMBRE };

                        bordeBoton.Tag = item;
                        bordeBoton.Child = formulario;
                        bordeBoton.MouseDown += delegate { cargarFormulario(bordeBoton); };
                        bordeBoton.MouseEnter += delegate { BotonMouseEnter(bordeBoton); };
                        bordeBoton.MouseLeave += delegate { BotonMouseLeave1(bordeBoton); };
                        contenedorFormularios.Children.Add(bordeBoton);
                        colorControl = 0;
                    }

                    ////////////
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        #endregion


        #endregion

        //Metodo que me permite revertir el estado de la habitación
        private void RecertirEstadoHabitacion(HABITACIONES habitacion)
        {
            Int16 codigo = 0;
            try
            {
                //if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionDisponible())
                if (habitacion.HABITACIONES_ESTADO.HES_CODIGO != 1)
                {
                    DataTable revertirHabitacion = new DataTable();
                    DataTable InformacionPaciente1 = new DataTable();
                    InformacionPaciente1 = NegHabitaciones.InformacionPaciente(habitacion);
                    DataTable atecodigo = NegPacientes.registropaciente(InformacionPaciente1.Rows[0][0].ToString().Trim(), 2);

                    if (NegHabitaciones.HorasTranscurridas(Convert.ToInt64(atecodigo.Rows[0]["ATE_CODIGO"].ToString())) >= 24)
                    {
                        MessageBox.Show("Paciente ha superado el maximo en horas permitidas para revertir estado de habitacion", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    revertirHabitacion = NegHabitaciones.RevertirMovimientoHabitacion(habitacion);
                    try
                    {
                        if (revertirHabitacion != null)
                        {
                            if (revertirHabitacion.Rows.Count == 0)
                            {
                                MessageBox.Show("Paciente Ya Tiene Factura No Se Puede Revertir", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                            else
                                MessageBox.Show("Reversión De Habitación Exitosa", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);

                            if (arbolHabitaciones.SelectedItem != null)
                            {
                                TreeViewItem itemH = (TreeViewItem)arbolHabitaciones.SelectedItem;
                                codigo = Convert.ToInt16(itemH.Tag);
                            }

                            if (control == 1)
                            {
                                _habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                                CargarHabitacionesVistaGen(_habitacionesLista);
                            }
                            else
                            {
                                _habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.hab_Numero).ToList();
                                CargarHabitacionesVistaGen(_habitacionesLista);
                            }
                            //cargo pacientes
                            //var otr0 = _pacientes.Where(p => p.codigoHabitacion == habitacion.hab_Codigo).OrderByDescending(p => p.fechaIngreso).Select(p => p.historiaClincia).FirstOrDefault();
                            DataTable InformacionPaciente = new DataTable();
                            InformacionPaciente = NegHabitaciones.InformacionPaciente(habitacion);
                            DataTable codAtencion = NegPacientes.registropaciente(InformacionPaciente.Rows[0][0].ToString().Trim(), 2);
                            string codAtencioP = codAtencion.Rows[0]["ATE_CODIGO"].ToString();
                            HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
                            habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
                            habitacionHistorial.ATE_CODIGO = Convert.ToInt32(codAtencioP.Trim());
                            habitacionHistorial.HAB_CODIGO = (habitacion.hab_Codigo);
                            habitacionHistorial.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                            habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
                            habitacionHistorial.HAD_OBSERVACION = "Se revierte  el  estado ";
                            habitacionHistorial.HAH_ESTADO = 1;
                            NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);
                            _pacientes = NegPacientes.RecuperarPacientesAtencionUltimasReporte(0, true, null, null, 0);
                            //_pacientes = NegHabitaciones.sp_HabitacionesCenso(1);
                            xamDataPresenterPacientes.DataSource = _pacientes.ToList();
                            lblMensaje.Content = "Módulo de Habitaciones Actualizado por ultima vez a las: " + DateTime.Now.TimeOfDay.ToString();
                            WindowLoaded(null, null);
                        }
                        else
                            MessageBox.Show("No se puede revertir, paciente ya facturado.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    catch
                    {
                        //MessageBox.Show("aqui es el error al revertir");
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione una habitación disponible", "Advertencia", MessageBoxButton.OK,
                                    MessageBoxImage.Exclamation);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        //Metodo que me permite el cambio de habitacion
        private void MoverPacienteHabitacion(HABITACIONES habitacion)
        {
            int usuarioPuede = 0;


            if (validador.Rows[1][0].ToString() == "True")
            {
                if (Sesion.codDepartamento == 10 || Sesion.codDepartamento == 1 || Sesion.codDepartamento == 23 || Sesion.codDepartamento == 2) //23 = Medicos General
                    usuarioPuede = 1;
            }
            else
                usuarioPuede = 1;
            if (usuarioPuede == 1)
            {
                Int16 codigo = 0;
                try
                {
                    if ((habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionOcupado()) || (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionAlta()) || (habitacion.HABITACIONES_ESTADO.HES_CODIGO == AdmisionParametros.getEstadoHabitacionCancelada()))
                    {
                        var detalleHabitacion = NegHabitaciones.RecuperarDetallesHabitacion(null, null, habitacion.hab_Codigo.ToString(), null, null, null, null, null, "false").OrderByDescending(h => h.HAD_FECHA_INGRESO).FirstOrDefault();
                        //recupero la atencion 
                        _atencionActiva = null;
                        _atencionActiva = NegAtenciones.RecuperarAtencionID((int)detalleHabitacion.ATE_CODIGO);
                        DataTable verificaH008 = new DataTable();
                        DataTable habitacionNombre = new DataTable();
                        bool siguiente = true;
                        DataTable verificador = new DataTable();
                        verificador = NegHabitaciones.VerificaParametroHoja008();

                        DataTable TablaHabitacionxDefault = new DataTable();
                        TablaHabitacionxDefault = NegParametros.ParametroHabitacionDefault();
                        if (verificador.Rows[0][0].ToString() == "True")
                        {
                            //if(TablaHabitacionxDefault.Rows.Count > 0)
                            //{
                            //    if (Convert.ToBoolean(TablaHabitacionxDefault.Rows[0][1].ToString()))
                            //    {
                            //        habitacionNombre = NegHabitaciones.HabitacionNombre(Convert.ToInt16(habitacion.hab_Codigo.ToString()));
                            //        if (habitacionNombre.Rows[0][0].ToString() == TablaHabitacionxDefault.Rows[0][0].ToString())
                            //        {
                            //            verificaH008 = NegHabitaciones.VerificaH008(Convert.ToInt64(detalleHabitacion.ATE_CODIGO));
                            //            if (verificaH008.Rows.Count == 0)
                            //                siguiente = false;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        habitacionNombre = NegHabitaciones.HabitacionNombre(Convert.ToInt16(habitacion.hab_Codigo.ToString()));
                            //        if (habitacionNombre.Rows[0][0].ToString() == "EMERGENCIA")
                            //        {
                            //            verificaH008 = NegHabitaciones.VerificaH008(Convert.ToInt64(detalleHabitacion.ATE_CODIGO));
                            //            if (verificaH008.Rows.Count == 0)
                            //                siguiente = false;
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            habitacionNombre = NegHabitaciones.HabitacionNombre(Convert.ToInt16(habitacion.hab_Codigo.ToString()));
                            if (habitacionNombre.Rows[0][0].ToString() == "EMERGENCIA")
                            {
                                verificaH008 = NegHabitaciones.VerificaH008(Convert.ToInt64(detalleHabitacion.ATE_CODIGO));
                                if (verificaH008.Rows.Count == 0)
                                    siguiente = false;
                            }
                            //}
                        }
                        if (siguiente)
                        {
                            var moverHabitacion = new frmMoverPaciente(habitacion, _atencionActiva);
                            moverHabitacion.ShowDialog();
                            if (moverHabitacion.getEstado())
                            {
                                if (arbolHabitaciones.SelectedItem != null)
                                {
                                    TreeViewItem itemH = (TreeViewItem)arbolHabitaciones.SelectedItem;
                                    codigo = Convert.ToInt16(itemH.Tag);
                                }

                                if (control == 1)
                                {
                                    _habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                                    CargarHabitacionesVistaGen(_habitacionesLista);
                                }
                                else
                                {
                                    _habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.hab_Numero).ToList();
                                    CargarHabitacionesVistaGen(_habitacionesLista);

                                }

                                //_habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                                //CargarHabitacionesVistaGen(_habitacionesLista);
                                //cargo pacientes
                                //_pacientes = NegPacientes.RecuperarPacientesAtencionUltimas(600, true, null, null);
                                _pacientes = NegPacientes.RecuperarPacientesAtencionUltimasReporte(0, true, _atencionActiva.ATE_CODIGO, null, 0);
                                xamDataPresenterPacientes.DataSource = _pacientes.ToList();
                            }
                        }
                        else
                            MessageBox.Show("El Paciente Debe Tener Realizado La Hoja 008 de Emergencia", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                MessageBox.Show("Esta Opción solo lo pueden realizar usuarios del departamento de ADMISIONES", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void CambiarEstadoHabitacion(HABITACIONES item, Int16 codigoEstado)
        {
            if (Sesion.codDepartamento != 6)
                try
                {
                    #region Validacion de cambio de estado Habitaciones 20211207 0937
                    //VALIDACION CONTRA ATENCION
                    HABITACIONES newHab = item;
                    HABITACIONES_ESTADO estado1;
                    estado1 = NegHabitaciones.RecuperarEstadoHabitacion(codigoEstado);
                    newHab.HABITACIONES_ESTADOReference.EntityKey = estado1.EntityKey;

                    HABITACIONES_DETALLE habOld = NegHabitaciones.RecuperarDetalleHabitacion(newHab);

                    ATENCIONES validaAtencion = NegAtenciones.RecuperarAtencionID((int)habOld.ATE_CODIGO);
                    #endregion

                    if (validaAtencion.ESC_CODIGO == 2 || validaAtencion.ESC_CODIGO == 3 || validaAtencion.ESC_CODIGO == 6) //si esta dado de alta puedo cambiar a disponible
                    {
                        Int16 codigo = 0;
                        var resultado = MessageBox.Show("Esta seguro de cambiar el estado de la habitación", "Cambio estado", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultado.ToString() == "Yes")
                        {
                            HABITACIONES uHabitacion = item;
                            HABITACIONES_ESTADO estado;
                            estado = NegHabitaciones.RecuperarEstadoHabitacion(codigoEstado);

                            uHabitacion.HABITACIONES_ESTADOReference.EntityKey = estado.EntityKey;
                            NegHabitaciones.CambiarEstadoHabitacion(uHabitacion);
                            //si desocupo la habitacion cierro la atencion
                            if (codigoEstado == Parametros.AdmisionParametros.getEstadoHabitacionDisponible() || codigoEstado == Parametros.AdmisionParametros.getEstadoHabitacionEnLimpieza() || codigoEstado == AdmisionParametros.getEstadoHabitacionAlta())
                            {
                                //var detalleHabitacion = NegHabitaciones.RecuperarDetalleHabitacion(item);
                                //Modificado para actualizar al paciente que correponde 
                                //var detalleHabitacion = NegHabitaciones.RecuperarDetallesHabitacion(null, null, item.hab_Codigo.ToString(), null, null, null, null, null, "false").OrderByDescending(h => h.HAD_FECHA_INGRESO).FirstOrDefault();
                                //recupero el detalle actual
                                try
                                {
                                    HABITACIONES_DETALLE habitacionDetalleOld = NegHabitaciones.RecuperarDetalleHabitacion(uHabitacion);
                                    habitacionDetalleOld.HAD_FECHA_DISPONIBILIDAD = DateTime.Now;
                                    NegHabitaciones.ActualizarDetallehabitacion(habitacionDetalleOld);

                                    //recupero la atencion 
                                    _atencionActiva = null;
                                    _atencionActiva = NegAtenciones.RecuperarAtencionID((int)habitacionDetalleOld.ATE_CODIGO);
                                }
                                catch (Exception ex)
                                {
                                    //WindowLoaded(null, null);
                                }
                                //if (Convert.ToInt32(_atencionActiva.TIPO_INGRESOReference.EntityKey.EntityKeyValues[0].Value) != 1)
                                //{

                                //if (codigoEstado == AdmisionParametros.getEstadoHabitacionAlta())
                                //{
                                //    _atencionActiva.ATE_ESTADO = true;
                                //    _atencionActiva.ESC_CODIGO = 2;
                                //    _atencionActiva.ATE_FECHA_ALTA = null;
                                //}

                                //else
                                //{
                                //    _atencionActiva.ATE_ESTADO = false;
                                //    _atencionActiva.ESC_CODIGO = 2;
                                //    _atencionActiva.ATE_FECHA_ALTA = DateTime.Now;
                                //}

                                //}                        
                                //NegAtenciones.EditarAtencionAdmision(_atencionActiva, 0);
                            }
                            //_habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                            //CargarHabitacionesVistaGen(_habitacionesLista);

                            //Cambios Edgar 20210330 se quiere enviar a cuenta paciente el estado de alta programada
                            //if(codigoEstado == 2)
                            //    {
                            //        HABITACIONES_DETALLE habitacionDetalleOld = NegHabitaciones.RecuperarDetalleHabitacion(uHabitacion);
                            //        habitacionDetalleOld.HAD_FECHA_DISPONIBILIDAD = DateTime.Now;
                            //        NegHabitaciones.ActualizarDetallehabitacion(habitacionDetalleOld);

                            //        //recupero la atencion 
                            //        _atencionActiva = null;
                            //        _atencionActiva = NegAtenciones.RecuperarAtencionID((int)habitacionDetalleOld.ATE_CODIGO);
                            //        _atencionActiva.ESC_CODIGO = 2;
                            //        NegAtenciones.EditarAtencionAdmision(_atencionActiva, 0);
                            //    }

                            if (arbolHabitaciones.SelectedItem != null)
                            {
                                TreeViewItem itemH = (TreeViewItem)arbolHabitaciones.SelectedItem;
                                codigo = Convert.ToInt16(itemH.Tag);
                            }

                            if (control == 1)
                            {
                                _habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                                CargarHabitacionesVistaGen(_habitacionesLista);
                            }
                            else
                            {
                                _habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.hab_Numero).ToList();
                                CargarHabitacionesVistaGen(_habitacionesLista);

                            }

                            DataTable InformacionPaciente = new DataTable();
                            InformacionPaciente = NegHabitaciones.InformacionPaciente(uHabitacion);
                            DataTable codAtencion = NegPacientes.registropaciente(InformacionPaciente.Rows[0][0].ToString().Trim(), 2);
                            string codAtencioP = codAtencion.Rows[0]["ATE_CODIGO"].ToString();

                            HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
                            habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
                            habitacionHistorial.ATE_CODIGO = Convert.ToInt32(codAtencioP.Trim());
                            habitacionHistorial.HAB_CODIGO = (uHabitacion.hab_Codigo);
                            habitacionHistorial.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                            habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
                            habitacionHistorial.HAD_OBSERVACION = "Se cambio de estado";
                            habitacionHistorial.HAH_ESTADO = codigoEstado;
                            NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);
                            lblMensaje.Content = "Módulo de Habitaciones Actualizado por ultima vez a las: " + DateTime.Now.TimeOfDay.ToString();
                            WindowLoaded(null, null);
                        }

                    }
                    else
                    {
                        if (codigoEstado == 2)
                        {
                            MessageBox.Show("Paciente aun no tiene hoja de alta.\r\nGenere hoja de alta para cambiar a alta programada.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                        else
                        {
                            MessageBox.Show("Paciente aun no ha sido dado de alta.\r\nComuniquese con caja.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);

                        }
                        _habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                        CargarHabitacionesVistaGen(_habitacionesLista);
                        return;
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                    if (err.InnerException != null)
                        Console.Write(err.InnerException.Message);
                }
        }
        private void FiltrarPorEstado(Label boton)
        {
            try
            {
                String tipo;
                Int16 codigo = 0;
                Int16 codigoEstado = Convert.ToInt16(boton.Tag);
                List<HABITACIONES> habitacionesLista;
                if (arbolHabitaciones.SelectedItem != null)
                {
                    TreeViewItem item = (TreeViewItem)arbolHabitaciones.SelectedItem;
                    codigo = Convert.ToInt16(item.Tag);
                }
                //else
                //{
                //    codigo = 0;
                //}

                //if (codigo > 0)
                //{
                //    habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.HABITACIONES_ESTADO.HES_CODIGO).ToList();
                //    habitacionesLista = habitacionesLista.Where(h => h.HABITACIONES_ESTADO.HES_CODIGO == codigoEstado).ToList() ;
                //    CargarHabitacionesVistaDet(habitacionesLista);
                //}
                //else
                //{
                if (control == 1)
                {
                    if (codigoEstado == 99)// Verifica si se desea filtrar las habitaciones con epicrisis / Giovanny Tapia / 12/09/2012
                    {

                        habitacionesLista = NegHabitaciones.listaHabitaciones().ToList();
                        habitacionesLista = habitacionesLista.Where(h => h.HABITACIONES_ESTADO.HES_CODIGO == 1).ToList(); // selecciono las habitaciones ocupadas / / Giovanny Tapia / 12/09/2012
                        CargaHabitacionesEpicrisis(habitacionesLista);

                    }

                    if (codigoEstado == 100) // Verifica si se desea filtrar las habitaciones con sin epicrisis / Giovanny Tapia / 12/09/2012
                    {

                        habitacionesLista = NegHabitaciones.listaHabitaciones().ToList();
                        habitacionesLista = habitacionesLista.Where(h => h.HABITACIONES_ESTADO.HES_CODIGO == 1).ToList(); // selecciono las habitaciones ocupadas / / Giovanny Tapia / 12/09/2012
                        CargaHabitacionesSinEpicrisis(habitacionesLista);

                    }

                    if (codigoEstado != 99 && codigoEstado != 100)
                    {
                        habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                        habitacionesLista = habitacionesLista.Where(h => h.HABITACIONES_ESTADO.HES_CODIGO == codigoEstado).ToList();
                        CargarHabitacionesVistaGen(habitacionesLista);
                    }
                }
                else
                {
                    habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.hab_Numero).ToList();
                    habitacionesLista = habitacionesLista.Where(h => h.HABITACIONES_ESTADO.HES_CODIGO == codigoEstado).ToList();
                    CargarHabitacionesVistaGen(habitacionesLista);
                }
                //}
                boton.Focus();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void HojaAdmision(Int64 ateCodigo)
        {
            DSAdmision Admision = new DSAdmision();
            ATENCIONES ultimaAtencion = NegAtenciones.AtencionID((int)ateCodigo);
            DataTable Registro1 = NegAtenciones.RegistroAdmision1(Convert.ToInt32(ultimaAtencion.PACIENTESReference.EntityKey.EntityKeyValues[0].Value));
            DataTable Registro2 = NegAtenciones.RegistroAdmision2(ateCodigo);
            DataTable Cambios1 = NegAtenciones.CambiosAdmision(Convert.ToInt32(ultimaAtencion.PACIENTESReference.EntityKey.EntityKeyValues[0].Value));

            TIPO_REFERIDO referido = NegTipoReferido.RecuperarReferido(Convert.ToInt32(ultimaAtencion.TIPO_REFERIDOReference.EntityKey.EntityKeyValues[0].Value));
            CATEGORIAS_CONVENIOS cc = NegAtenciones.tipoSeguro(ultimaAtencion.ATE_CODIGO);
            TIPO_TRATAMIENTO tratamiento = NegTipoTratamiento.recuperaTipoTratamiento(Convert.ToInt16(ultimaAtencion.TIPO_TRATAMIENTOReference.EntityKey.EntityKeyValues[0].Value));
            PACIENTES_DATOS_ADICIONALES datosPacienteActual = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPacienteID(ultimaAtencion.ATE_CODIGO);
            PACIENTES pacienteActual = NegPacientes.recuperarPacientePorAtencion(ultimaAtencion.ATE_CODIGO);
            DIVISION_POLITICA dp = new DIVISION_POLITICA();
            //GRUPO_SANGUINEO gs = NegGrupoSanguineo.RecuperarGrupoSanguineoID(Convert.ToInt32(pacienteActual.GRUPO_SANGUINEOReference.EntityKey.EntityKeyValues[0].Value));
            ETNIA etnia = new ETNIA();
            etnia = NegEtnias.RecuperarEtniaID(Convert.ToInt32(pacienteActual.ETNIAReference.EntityKey.EntityKeyValues[0].Value));
            DataRow drAdmision;
            DataRow drCambios;
            DataRow drRegistro;

            drAdmision = Admision.Tables["Admision"].NewRow();
            drAdmision["Empresa"] = Sesion.nomEmpresa;
            drAdmision["Logo"] = NegUtilitarios.RutaLogo("General");
            drAdmision["Unidad"] = tratamiento.TIA_DESCRIPCION;
            drAdmision["Cod"] = His.Entidades.Clases.Sesion.unicodigo;
            drAdmision["c_Parroquia"] = datosPacienteActual.COD_PARROQUIA;
            drAdmision["c_Canton"] = datosPacienteActual.COD_CANTON;
            drAdmision["c_Provincia"] = datosPacienteActual.COD_PROVINCIA;
            //if (!NegParametros.ParametroFormularios())
            //    drAdmision["Hc"] = pacienteActual.PAC_HISTORIA_CLINICA;
            //else
            //    drAdmision["Hc"] = pacienteActual.PAC_IDENTIFICACION;
            drAdmision["Hc"] = pacienteActual.PAC_HISTORIA_CLINICA;
            drAdmision["Apellido1"] = pacienteActual.PAC_APELLIDO_PATERNO.Trim();
            drAdmision["Apellido2"] = pacienteActual.PAC_APELLIDO_MATERNO.Trim();
            drAdmision["Nombre1"] = pacienteActual.PAC_NOMBRE1.Trim();
            drAdmision["Nombre2"] = pacienteActual.PAC_NOMBRE2.Trim();
            drAdmision["Identificacion"] = pacienteActual.PAC_IDENTIFICACION.Trim();
            drAdmision["Direccion"] = datosPacienteActual.DAP_DIRECCION_DOMICILIO.Trim();
            if (datosPacienteActual.COD_SECTOR != null)
            {
                dp = NegDivisionPolitica.DivisionPolitica(datosPacienteActual.COD_SECTOR);
                drAdmision["Barrio"] = dp.DIPO_NOMBRE;
            }
            if (datosPacienteActual.COD_PARROQUIA != null)
            {
                dp = NegDivisionPolitica.DivisionPolitica(datosPacienteActual.COD_PARROQUIA);
                drAdmision["Parroquia"] = dp.DIPO_NOMBRE;
            }
            if (datosPacienteActual.COD_CANTON != null)
            {
                dp = NegDivisionPolitica.DivisionPolitica(datosPacienteActual.COD_CANTON);
                drAdmision["Canton"] = dp.DIPO_NOMBRE;
            }
            if (datosPacienteActual.COD_PROVINCIA != null)
            {
                dp = NegDivisionPolitica.DivisionPolitica(datosPacienteActual.COD_PROVINCIA);
                drAdmision["Provincia"] = dp.DIPO_NOMBRE;
            }
            drAdmision["Zona"] = "N/A";
            if (datosPacienteActual.DAP_TELEFONO1 == null || datosPacienteActual.DAP_TELEFONO1 == "")
                drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO2;
            else if (datosPacienteActual.DAP_TELEFONO2 == null || datosPacienteActual.DAP_TELEFONO2 == "")
                drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO1;
            else
                drAdmision["Telefono"] = datosPacienteActual.DAP_TELEFONO1 + " / " + datosPacienteActual.DAP_TELEFONO2;
            drAdmision["F_Nacimiento"] = Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO).ToShortDateString();
            dp = NegDivisionPolitica.DivisionPolitica(pacienteActual.DIPO_CODIINEC);
            drAdmision["L_Nacimiento"] = dp.DIPO_NOMBRE;
            drAdmision["Nacionalidad"] = pacienteActual.PAC_NACIONALIDAD;
            drAdmision["Etnia"] = etnia.E_NOMBRE;
            drAdmision["Edad"] = NegUtilitarios.EdadCalculada(Convert.ToDateTime(pacienteActual.PAC_FECHA_NACIMIENTO), (DateTime)ultimaAtencion.ATE_FECHA_INGRESO);

            if (pacienteActual.PAC_GENERO == "M")
                drAdmision["Masculino"] = "X";
            else
                drAdmision["Femenino"] = "X";

            if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 1)
                drAdmision["Soltero"] = "X";
            else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 2)
                drAdmision["Casado"] = "X";
            else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 3)
                drAdmision["Divorsiado"] = "X";
            else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 4)
                drAdmision["Union"] = "X";
            else if (Convert.ToInt32(datosPacienteActual.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value) == 5)
                drAdmision["Viudo"] = "X";

            drAdmision["Instruccion"] = datosPacienteActual.DAP_INSTRUCCION;
            drAdmision["c_Emergencia"] = pacienteActual.PAC_REFERENTE_NOMBRE.Trim();
            drAdmision["Parentesco"] = pacienteActual.PAC_REFERENTE_PARENTESCO.Trim();
            drAdmision["c_Direccion"] = pacienteActual.PAC_REFERENTE_DIRECCION.Trim();
            drAdmision["c_Telefono"] = pacienteActual.PAC_REFERENTE_TELEFONO.Trim();
            drAdmision["Admisionista"] = ultimaAtencion.USUARIOSReference.EntityKey.EntityKeyValues[0].Value;
            drAdmision["F_Admision"] = ultimaAtencion.ATE_FECHA_INGRESO;
            drAdmision["Ocupacion"] = datosPacienteActual.DAP_OCUPACION;
            drAdmision["p_Empresa"] = datosPacienteActual.DAP_EMP_NOMBRE;
            drAdmision["T_Seguro"] = cc.CAT_NOMBRE;
            drAdmision["Referido"] = referido.TIR_NOMBRE;
            drAdmision["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;

            Admision.Tables["Admision"].Rows.Add(drAdmision);

            int aux = 0;
            int contador = Registro1.Rows.Count - 10;
            for (int i = Registro1.Rows.Count - 1; i >= 0; i--)
            {
                if (aux < 10)
                {
                    drRegistro = Admision.Tables["Registro"].NewRow();

                    drRegistro["Numero"] = Registro1.Rows[i]["N_ORDEN"].ToString();
                    drRegistro["Fecha"] = Registro1.Rows[i]["FECHA_INGRESO"].ToString();
                    drRegistro["Edad"] = Registro1.Rows[i]["EDAD"].ToString();
                    drRegistro["Referido"] = Registro1.Rows[i]["REFERIDO_DE"].ToString();
                    if (Registro1.Rows[i]["N_ORDEN"].ToString() == "1")
                        drRegistro["Primera"] = "X";
                    else
                        drRegistro["Sub"] = "X";
                    drRegistro["Admisionista"] = Registro1.Rows[i]["COD_ADMISIONISTA"].ToString();

                    if (contador >= 1)
                    {
                        contador--;
                        drRegistro["Numero1"] = Registro1.Rows[contador]["N_ORDEN"].ToString();
                        drRegistro["Fecha1"] = Registro1.Rows[contador]["FECHA_INGRESO"].ToString();
                        drRegistro["Edad1"] = Registro1.Rows[contador]["EDAD"].ToString();
                        drRegistro["Referido1"] = Registro1.Rows[contador]["REFERIDO_DE"].ToString();
                        drRegistro["Sub1"] = Registro1.Rows[contador]["SUBSEC"].ToString();
                        drRegistro["Admisionista1"] = Registro1.Rows[contador]["COD_ADMISIONISTA"].ToString();
                    }

                    aux++;
                    drRegistro["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;

                    Admision.Tables["Registro"].Rows.Add(drRegistro);
                }
            }

            foreach (DataRow item in Cambios1.Rows)
            {
                drCambios = Admision.Tables["Cambios"].NewRow();
                drCambios["Numero"] = item[0].ToString();
                drCambios["Fecha"] = item[2].ToString();
                drCambios["Estado_Civil"] = item[3].ToString();
                drCambios["Instruccion"] = item[4].ToString();
                drCambios["Ocupacion"] = item[5].ToString();
                drCambios["Empresa"] = item[6].ToString();
                drCambios["T_Seguro"] = item[7].ToString();
                drCambios["Direccion"] = item[8].ToString();
                drCambios["Barrio"] = item[9].ToString();
                drCambios["Zona"] = item[10].ToString();
                drCambios["Parroquia"] = item[11].ToString();
                drCambios["Canton"] = item[12].ToString();
                drCambios["Provincia"] = item[13].ToString();
                drCambios["Telefono"] = item[14].ToString();
                drCambios["ATE_CODIGO"] = ultimaAtencion.ATE_CODIGO;

                Admision.Tables["Cambios"].Rows.Add(drCambios);
            }
            Formulario.frmReportes x = new Formulario.frmReportes(1, "Admision", Admision);
            x.ShowDialog();
        }
        private void cargarFormulario(Border boton)
        {
            try
            {
                //cargo los formularios de las Historias Clinicas 

                var atencionDetalle = (ATENCION_DETALLE_FORMULARIOS_HCU)boton.Tag;
                if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcAdmisionAltaEgreso()) //FALTA DE CREAR EL FORMULARIO ALTA EGRESO, SOLO SE MOSTRARA LA ADMISION
                {
                    HojaAdmision(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    //var frm = new frmReportes();
                    //frm.parametro = atencionDetalle.ATENCIONES.ATE_CODIGO;
                    //frm.reporte = "admision";
                    //frm.ShowDialog();

                    //frmReportes frm = new frmReportes();
                    //frm.campo1 = Convert.ToString(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    //frm.reporte = "rAdmision";
                    //frm.ShowDialog();
                    //frm.Dispose();

                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcAnamnesisExamenFisico())
                {
                    HC_ANAMNESIS anamnesis = NegAnamnesis.recuperarAnamnesisPorAtencion(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    FORMULARIOS_HCU parametroFecha = NegParametros.RecuperaFechaPorCodigo(3);
                    ATENCIONES ate = NegAtenciones.RecuperarAtencionID(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    try
                    {
                        if (anamnesis != null) //Valida si la anamnesis ya existe Andres C 20240322
                        {
                            if (Convert.ToDateTime(parametroFecha.fecha) <= anamnesis.ANE_FECHA) //Compara con la fecha del parametro para que cargue la anamnesis nueva o antigua dependiendo de la fecha de creacion de la anamnesis Andres C 20240322
                            {
                                frmAnamnesisNew anamnew = new frmAnamnesisNew(atencionDetalle.ATENCIONES.ATE_CODIGO, false);                                
                                anamnew.ShowDialog();
                            }
                            else
                            {
                                frm_Anemnesis x = new frm_Anemnesis(atencionDetalle.ATENCIONES.ATE_CODIGO);
                                x.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                                x.ShowDialog();
                            }
                        }
                        else
                        {

                            frmAnamnesisNew anamnew = new frmAnamnesisNew(atencionDetalle.ATENCIONES.ATE_CODIGO, false);
                            anamnew.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                            anamnew.ShowDialog();
                        }
                    }
                    catch (Exception e)
                    {

                       MessageBox.Show(e.Message); 
                    }
                    


                    //var anemeasis = new frmAnamnesisNew(atencionDetalle.ATENCIONES.ATE_CODIGO, false); //CAMBIO PARA ABRIR ANAMNESIS NUEVA  Andres Cabrera 2024/03/04 16H55
                    //var anemeasis = new frm_Anemnesis();
                    //anemeasis.codigoAtencion = atencionDetalle.ATENCIONES.ATE_CODIGO;  
                    //anemeasis.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcEpicrisis())
                {
                    var epicrisis = new frm_Epicrisis(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    epicrisis.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcEvolucionPrescripciones())
                {
                    var evolucion = new frm_Evolucion(atencionDetalle.ATENCIONES.ATE_CODIGO, true);
                    evolucion.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcInterconsultaSolicitudInforme())
                {
                    var interconsulta = new frm_Interconsulta(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    interconsulta.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 21)
                {
                    var protocolo = new frm_Protocolo(atencionDetalle.ATENCIONES.ATE_CODIGO, atencionDetalle.ADF_CODIGO);
                    protocolo.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcLaboratorioClinicoSolicitud())
                {
                    var laboratorio = new frm_LaboratorioClinico(atencionDetalle.ATENCIONES.ATE_CODIGO, true);
                    laboratorio.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcImagenologia())
                {
                    frm_Imagen imagen = new frm_Imagen(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    imagen.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    imagen.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcEmergencia())
                {

                   


                        FORMULARIOS_HCU parametroFecha = NegParametros.RecuperaFechaPorCodigo(7);
                    ATENCIONES ate = NegAtenciones.RecuperarAtencionID(atencionDetalle.ATENCIONES.ATE_CODIGO);


                    if (Convert.ToDateTime(parametroFecha.fecha) <= ate.ATE_FECHA_INGRESO)

                    {
                        frm_EmergenciaNew x = new frm_EmergenciaNew(atencionDetalle.ATENCIONES.ATE_CODIGO);
                        x.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        x.ShowDialog();
                    }
                        else
                        {
                        frm_Emergencia x = new frm_Emergencia(atencionDetalle.ATENCIONES.ATE_CODIGO);
                        x.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        x.ShowDialog();
                    }




                   

                }
                //agregacion Edgar 20210115 -----------------------------------------
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcReferenciaContrarreferencia())
                {
                    frm_Referencia referecia = new frm_Referencia(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    referecia.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 23)
                {
                    frm_Contrareferencia referecia = new frm_Contrareferencia(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    referecia.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 10)
                {
                    frm_Histopatologico013A referecia = new frm_Histopatologico013A(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    referecia.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 25)
                {
                    frm_Contrareferencia referecia = new frm_Contrareferencia(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    referecia.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 26)
                {
                    frm_InterconsultaB referecia = new frm_InterconsultaB(atencionDetalle.ATENCIONES.ATE_CODIGO);
                    referecia.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == AdmisionParametros.getHcConsultaExternaAnamnesisExamenFisico())
                {
                    //Aqui debe ir el formulario de consulta externa. no se puede agregar la referencia por que ya existe en desde consulta externa a habitaciones


                    ConsultaExterna.Consulta x = new ConsultaExterna.Consulta(Convert.ToString(atencionDetalle.ATENCIONES.ATE_CODIGO));
                    x.ShowDialog();
                }
                //Formulario 002 Anestesia
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 22)
                {

                }
                //Formulario 024 Consentimiento informado
                else if (
                    atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 13)
                {
                    frm_Form024 x = new frm_Form024(atencionDetalle.ATENCIONES.ATE_CODIGO, xamTxtHC.Text);
                    x.ShowDialog();
                }
                //Formulario 020 Signos Vitales
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 11)
                {
                    frm_Form020 x = new frm_Form020(atencionDetalle.ATENCIONES.ATE_CODIGO, xamTxtHC.Text);
                    x.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    x.ShowDialog();
                }
                else if (atencionDetalle.FORMULARIOS_HCU.FH_CODIGO == 24)
                {
                    frmIngestaEliminacion x = new frmIngestaEliminacion(atencionDetalle.ATENCIONES.ATE_CODIGO, xamTxtHC.Text);
                    x.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    x.ShowDialog();
                }
                //-------------------------------------------------------------------
            }
            catch (Exception err) { MessageBox.Show(err.Message); }

        }


        #region Eventos

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (!NegUtilitarios.IpBodegas())
            {
                this.Close();
                return;
            }
            try
            {
                lblMensaje.Content = "Módulo de Habitaciones Actualizado por ultima vez a las: " + DateTime.Now.TimeOfDay.ToString();
                lblUsuario.Content = Entidades.Clases.Sesion.nomUsuario;

                //cargo el arbol de pisos y las habitaciones
                if (a == 0)
                {
                    CargarArbolHabitaciones();
                    a++;
                }
                if (Piso == 99)
                {
                    _habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                }
                else
                {
                    _habitacionesLista = NegHabitaciones.listaHabitaciones(Piso).OrderBy(h => h.hab_Numero).ToList();
                }

                CargarHabitacionesVistaGen(_habitacionesLista);
                CargarEstadosHabitaciones();
                //cargo pacientes
                //_pacientes = NegPacientes.RecuperarPacientesAtencionUltimas(600, true,null,null);
                _pacientes = NegPacientes.RecuperarPacientesAtencionUltimasReporte(0, true, null, null, 0);

                DataTable Tabla = new DataTable(); //Recogera los pacientes con imagen
                _pacientes_imagen = NegPacientes.RecuperarPacientesImagen();
                if (_pacientes_imagen != null)
                {
                    xamDataPresenterPacientes_Pedidos.DataSource = _pacientes_imagen.ToList();
                }
                //_pacientes_imagen = NegPacientes.RecupararPacientesImagen();
                xamDataPresenterPacientes.DataSource = _pacientes.ToList();
                //Carga Combo para poder filtrar habitaciones
                //cmbPisos.ItemsSource = NegPacientes.recuperarListaPisos(); 
                //cmbPisos.DisplayMemberPath = "NIV_NOMBRE";
                //cmbPisos.SelectedValuePath = "NIV_CODIGO";
                //cmbPisos.SelectedIndex = 0;
                //Cambio Mario 2022.09.14 para solo mostrar paceintes Hospitalizados y Emergencia
                var listaTipo1 = new List<string> { "HOSPITALIZADO", "EMERGENCIA", "OTROS", "TODOS" };
                //var listaTipo1 = new List<string> { "HOSPITALIZADO", "EMERGENCIA"};
                cmbHabitacionesFINAL.ItemsProvider.ItemsSource = listaTipo1;
                cmbHabitacionesFINAL.SelectedIndex = 3;
                //cargo combo tipo de vista del listado de pacientes, cargo la lista de cantidad a paginar
                var listaTipo = new List<string> { "Grilla", "Carrusel", "Ficha" };
                xceTipoVistaPacientes.ItemsProvider.ItemsSource = listaTipo;
                xceTipoVistaPacientes.SelectedIndex = 0;
                //Filtros Historias Clinicas
                //cargo filtro de epicrisis
                var listaEstados = new List<string> { "Todos", "Sin Anamnesis", "Sin Epicrisis", "Sin Form008", "Sin Protocolo" };
                xceEstadoEpicrisis.ItemsProvider.ItemsSource = listaEstados;
                xceEstadoEpicrisis.SelectedIndex = 0;
                //posicion inicial del control Inf Paciente
                infHabitacion.Margin = new Thickness(0, 0, 0, 0);
                //cargo la lista de Areas
                cboAreas.ItemsSource = NegPedidos.recuperarListaAreasTodas();
                cboAreas.DisplayMemberPath = "PEA_NOMBRE";
                cboAreas.SelectedValuePath = "PEA_CODIGO";
                cboAreas.SelectedIndex = 1;
                //cargo la lista de prioridades
                cboPrioridades.ItemsSource = NegCatalogos.RecuperarCatalogoListaGen("PEDIDOS", "PED_PRIORIDAD").OrderBy(p => p.nombre);
                cboPrioridades.DisplayMemberPath = "nombre";
                cboPrioridades.SelectedValuePath = "codigo";
                cboPrioridades.SelectedIndex = 0;
                //termino el proceso de carga
                _controlesCargados = true;
                //inicializo la ventana pop up
                PopUpVentana.ShowPopUp(150, 300, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "imagenes//ventanaPopUp.png")), " Pacientes con Epicrisis: 0 \n Habitaciones Ocupadas: 1 \n No existen examenes pendientes", new Thickness(20));

                NegValidaciones.alzheimer();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void BtnAgregarClick(object sender, RoutedEventArgs e)
        {
            var ventana = new frmListaFormularios(_atencionActiva);
            ventana.ShowDialog();
            //infHabitacion.Visibility = Visibility.Collapsed;
            //infHabitacion.Margin = new Thickness(0, 0, 0, 0);
            infHabitacion.Visibility = Visibility.Visible;
            infHabitacion.Margin = new Thickness((Width / 2 - infHabitacion.Width / 2), (100), 0, inferior);
            ActualizarListaFormularios(_atencionActiva.ATE_CODIGO, 0);
            superior -= 5;
        }

        private void BtnBuscarPacientesClick(object sender, RoutedEventArgs e)
        {
            DataTable datos = new DataTable();
            DataTable codAtencion = new DataTable();
            DataTable porHistoria = new DataTable();
            try
            {


                //codAtencion=NegPacientes.registropaciente(txtBusquedaPacientes.Text.Trim(),1);
                string codAtencioP = codAtencion.Rows[0]["ATE_CODIGO"].ToString();
                _paciente = NegPacientes.RecuperarPacientesAtencionUltimas(600, true, Convert.ToInt32(codAtencioP.Trim()), null);
                var paciente = _paciente.OrderByDescending(p => p.fechaIngreso).FirstOrDefault();
                if (_paciente.Count != 0)
                {
                    HABITACIONES habitacion = NegHabitaciones.RecuperarHabitacionId(paciente.codigoHabitacion);
                    MostrarInfHabitacion(habitacion);
                }
                else
                {
                    //porHistoria = NegPacientes.registropaciente(txtBusquedaPacientes.Text.Trim(), 2);
                    string codHistoria = porHistoria.Rows[0]["codAtencion"].ToString();
                    _paciente = NegPacientes.RecuperarPacientesAtencionUltimas(600, true, Convert.ToInt32(codHistoria.Trim()), null);
                    var pacienteHis = _paciente.OrderByDescending(p => p.fechaIngreso).FirstOrDefault();
                    if (_paciente.Count != 0)
                    {
                        HABITACIONES habitacion = NegHabitaciones.RecuperarHabitacionId(pacienteHis.codigoHabitacion);
                        MostrarInfHabitacion(habitacion);
                    }
                    else
                    {
                        try
                        {
                            //datos = NegPacientes.registropaciente(txtBusquedaPacientes.Text.Trim(), 2);
                            string nombre = datos.Rows[0]["nombrePaciente"].ToString();
                            PopUpVentana.ShowError(100, 250, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "imagenes//ventanaPopUp.png")), "El paciente " + nombre + "  no se encuentra hospitalizado", new Thickness(20));
                        }
                        catch (Exception errores)
                        {
                            //datos = NegPacientes.registropaciente(txtBusquedaPacientes.Text.Trim(), 1);
                            string nombre = datos.Rows[0]["NOMBRE"].ToString();
                            PopUpVentana.ShowError(100, 250, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "imagenes//ventanaPopUp.png")), "El paciente " + nombre + "  no se encuentra hospitalizado", new Thickness(20));


                        }

                    }
                }

            }
            catch (Exception es)
            {

                try
                {
                    //datos = NegPacientes.registropaciente(txtBusquedaPacientes.Text.Trim(), 2);
                    string nombre = datos.Rows[0]["nombrePaciente"].ToString();
                    PopUpVentana.ShowError(100, 250, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "imagenes//ventanaPopUp.png")), "El paciente " + nombre + "  no se encuentra hospitalizado", new Thickness(20));
                }
                catch (Exception errores)
                {
                    //datos = NegPacientes.registropaciente(txtBusquedaPacientes.Text.Trim(), 1);
                    if (datos.Rows.Count != 0)
                    {
                        string nombre = datos.Rows[0]["NOMBRE"].ToString();
                        PopUpVentana.ShowError(100, 250, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "imagenes//ventanaPopUp.png")), "El paciente " + nombre + "  no se encuentra hospitalizado", new Thickness(20));
                    }
                    else
                        PopUpVentana.ShowError(100, 250, new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "imagenes//ventanaPopUp.png")), "No existe  paciente", new Thickness(20));

                }
            }


        }

        //metodo actualiza la hora del reloj
        void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                relojInf.Content = DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                secondHand.Angle = DateTime.Now.Second * 6;
                minuteHand.Angle = DateTime.Now.Minute * 6;
                hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5);
            }));
        }

        /// <summary>
        /// evento que cambia el color de fondo del boton en el cual se a situado el mouse
        /// </summary>
        private void BotonMouseEnter(Border panel)
        {
            if (panel.Tag.Equals("A"))
            {
                panel.Background = _gradienteBotonAover;
                panel.CornerRadius = new CornerRadius(10);
            }
            else if (panel.Tag.Equals("B"))
            {
                panel.Background = _gradienteBotonBover;
            }
            else
            {
                panel.Background = _gradienteBotonBover;
            }
        }
        /// <summary>
        /// evento que retorna el color del boton luego que el mouse deja de estar sobre el control
        /// </summary>
        private void BotonMouseLeave(Border panel)
        {
            if (panel.Tag.Equals("A"))
            {
                panel.Background = _gradienteBotonA;
                panel.CornerRadius = new CornerRadius(15);
            }
            else if (panel.Tag.Equals("B"))
            {
                panel.Background = _gradienteBotonB;
            }
            else
            {

                panel.Background = _gradienteBotonB;

            }
        }
        private void BotonMouseLeave1(Border panel)
        {

            panel.Background = Brushes.Red;

        }

        private void ArbolHabitacionesSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            try
            {
                String tipo;
                var item = (TreeViewItem)arbolHabitaciones.SelectedItem;
                Int16 codigo = Convert.ToInt16(item.Tag);
                List<HABITACIONES> habitacionesLista;
                if (codigo > 0)
                {
                    //habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.HABITACIONES_ESTADO.HES_CODIGO).ToList();
                    habitacionesLista = _habitacionesLista.Where(h => h.NIVEL_PISO.NIV_CODIGO == codigo).ToList();
                    CargarHabitacionesVistaDet(habitacionesLista);
                }
                else
                {

                    habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                    CargarHabitacionesVistaGen(_habitacionesLista);
                }
                control = 0;
            }

            catch (Exception err)
            {
                // MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Evento que se da al cerrar el panel de inf del paciente
        private void InfHabCerrarClick(object sender, RoutedEventArgs e)
        {
            tabgeneral.Focus();
            infHabitacion.Visibility = Visibility.Collapsed;
            infHabitacion.Margin = new Thickness(0, 0, 0, 0);
            tabgeneral.Focus();
            lblusr.Content = "Usuario:";
            txtCodigoMedico.Text = "";
            txtDatosMedico.Text = "";
            NegHabitaciones.HabitacionesEnOrden();

            NegValidaciones.alzheimer();
            lblMensaje.Content = "Módulo de Habitaciones Actualizado por ultima vez a las: " + DateTime.Now.TimeOfDay.ToString();
            WindowLoaded(null, null);

        }


        private void EstructuraClick(object sender, RoutedEventArgs e)
        {
            var ventana = new EstructurasPisos();
            ventana.ShowDialog();
        }

        private void SalirClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AcercaClick(object sender, RoutedEventArgs e)
        {
            var ventana = new frmAcerca();
            ventana.ShowDialog();
        }

        private void DietasClick(object sender, RoutedEventArgs e)
        {
            var ventana = new frmDietas();
            ventana.ShowDialog();
        }

        private void EstadosClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var ventana = new EstadosHabitaciones();
            ventana.ShowDialog();
        }

        private void RecetasClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var ventana = new frmRecetas();
            ventana.ShowDialog();
        }

        private void HabitacionesClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var ventana = new frmHabitaciones();
            ventana.ShowDialog();
        }
        NegQuirofano Quirofano = new NegQuirofano();
        private void BtnSolicitarClick(object sender, RoutedEventArgs e)
        {
            DataTable validadepartamento = new DataTable();
            validadepartamento = NegEvolucion.VerificaDepartamento(Sesion.codUsuario);
            //if (validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "ENFERMERIA" || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "SISTEMAS")
            //{
            try
            {
                //valido la estacion
                Int32 Pedido = 0;
                var archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
                if (codigoEstacion <= 0)
                {
                    MessageBox.Show("No se encuentra asignada una estación", "Inf", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                if (cboPrioridades.SelectedIndex < 0)
                {
                    MessageBox.Show("Por favor seleccione la prioridad de sus pedido", "Inf", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    cboPrioridades.Focus();
                    return;
                }

                //if (Convert.ToInt32(txtCodigoMedico.Text) < 0 || txtCodigoMedico.Text == "")// cambio para validar nulos David Mantilla 19/08/2013
                if (string.IsNullOrEmpty(txtCodigoMedico.Text) || Convert.ToInt32(txtCodigoMedico.Text) < 0)
                {
                    MessageBox.Show("Por favor seleccione el medico solicitante", "Inf", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    cboMedicosPedidos.Focus();
                    return;
                }

                byte prioridad = Convert.ToByte(cboPrioridades.SelectedValue);

                PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
                RUBROS _Rubro = new RUBROS();

                ar = (PEDIDOS_AREAS)cboAreas.SelectedItem;
                _Rubro = (RUBROS)this.cmb_Rubros.SelectedItem;

                /************************ CARGA LOS DATOS DE LA EMPRESA Y CONVENIO ****************************************/

                DataTable DatosConvenioAtencion = new DataTable();
                DatosConvenioAtencion = NegAtenciones.CodigoConvenio(_atencionActiva.ATE_CODIGO);

                CodigoConvenio = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["TE_CODIGO"].ToString());
                CodigoTipoEmpresa = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["CAT_CODIGO"].ToString()); /* CONVENIO*/
                CodigoAseguradora = Convert.ToInt32(DatosConvenioAtencion.Rows[0]["ASE_CODIGO"].ToString()); /* EMPRESA */

                /**********************************************************************************************************/

                var codigoArea = (Int16)ar.PEA_CODIGO;
                var CodigoRubro = (Int16)_Rubro.RUB_CODIGO;
                var Rubro = (Int16)ar.RUB_CODIGO;
                var pedidoArea = (PEDIDOS_AREAS)cboAreas.SelectedItem;
                var Edad = Funciones.CalcularEdad(Convert.ToDateTime(FechaNacimientoPaciente)).ToString() + " años";
                Boolean todos = false;
                if (cboAreas.Text == "TODAS LAS AREAS")
                {
                    todos = true;
                }
                /*alex*/
                if (His.Parametros.FacturaPAR.BodegaPorDefecto == 1)
                {
                    if (NegAccesoOpciones.ParametroBodega())
                    {
                        if (NegAccesoOpciones.ParametroBodega())
                        {
                            if (PisoBodega.Rows.Count > 0)
                                if (PisoBodega.Rows[0]["NIV_BODEGA"].ToString() != "")
                                    Parametros.FacturaPAR.BodegaPorDefecto = Convert.ToInt16(PisoBodega.Rows[0]["NIV_BODEGA"].ToString());
                                else
                                    His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
                            else
                                His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
                        }
                    }

                }
                else
                {
                    if (NegAccesoOpciones.ParametroBodega())
                    {
                        if (NegAccesoOpciones.ParametroBodega())
                        {
                            if (PisoBodega.Rows.Count > 0)
                                if (PisoBodega.Rows[0]["NIV_BODEGA"].ToString() != "")
                                    Parametros.FacturaPAR.BodegaPorDefecto = Convert.ToInt16(PisoBodega.Rows[0]["NIV_BODEGA"].ToString());
                                else
                                    His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
                            else
                                His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
                        }
                    }
                }
                var solicitudMedicamentos = new frmProductosAyuda(codigoArea, xamTxtPaciente.Text, txtDatosMedico.Text, xamTxtHC.Text.ToString(), xamTxtNumAtencion.Text.ToString(), Edad, xamTxtAseguradora.Text, CodigoRubro, CodigoAseguradora, CodigoTipoEmpresa, todos);
                solicitudMedicamentos.ShowDialog();
                //var usuario = new frm_ClavePedido();
                //if (solicitudMedicamentos.descripcion != null)
                //{
                //    if (validador.Rows[0][0].ToString() == "True")
                //    {
                //        usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                //        usuario.ShowDialog();
                //    }
                //    else
                //    {
                //        usuario.usuarioActual = Entidades.Clases.Sesion.codUsuario;
                //        usuario.validador = true; //Solo para la alianza
                //    }

                //}
                //else
                //    usuario.usuarioActual = 0;

                //if (Entidades.Clases.Sesion.codUsuario == usuario.usuarioActual)
                //{
                if (solicitudMedicamentos.PedidosDetalle != null && solicitudMedicamentos._validador != false)
                {
                    if (solicitudMedicamentos.PedidosDetalle.Count() > 0)
                    {

                        var pedido = new PEDIDOS
                        {
                            PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1,
                            PED_FECHA = DateTime.Now /*PARA GUARDAR EL PEDIDO SE NECESITA FECHA Y HORA/ GIOVANNY TAPIA / 12/04/2013*/,
                            PED_DESCRIPCION = solicitudMedicamentos.descripcion,
                            PED_ESTADO = FarmaciaPAR.PedidoPendiente,
                            ID_USUARIO = Convert.ToInt16(solicitudMedicamentos._usuarioActual),
                            //ID_USUARIO = Entidades.Clases.Sesion.codUsuario,
                            ATE_CODIGO = _atencionActiva.ATE_CODIGO,
                            PEE_CODIGO = codigoEstacion,
                            TIP_PEDIDO = FarmaciaPAR.PedidoMedicamentos,
                            PED_PRIORIDAD = prioridad,
                            MED_CODIGO = Convert.ToInt32(txtCodigoMedico.Text),
                            PEDIDOS_AREASReference = { EntityKey = pedidoArea.EntityKey },
                            HAB_CODIGO = _atencionActiva.HABITACIONES.hab_Codigo,
                            IP_MAQUINA = Sesion.ip
                        };

                        Pedido = pedido.PED_CODIGO;

                        //NegPedidos.crearPedido(pedido);

                        Int32 xcodDiv = 0;
                        Int16 XRubro = 0;
                        DataTable auxDT = new DataTable();
                        List<PEDIDOS_DETALLE> listapedidos = new List<PEDIDOS_DETALLE>();
                        foreach (var solicitud in solicitudMedicamentos.PedidosDetalle)
                        {

                            //solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                            //solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                            //auxDT = NegFactura.recuperaCodRubro(Convert.ToInt32(solicitud.PRO_CODIGO_BARRAS));
                            //foreach (DataRow row in auxDT.Rows)
                            //{
                            //    XRubro = Convert.ToInt16(row[0].ToString());
                            //    xcodDiv = Convert.ToInt32(row[1].ToString());
                            //}
                            listapedidos.Add(solicitud);

                        }
                        if (NegPedidos.crearDetallePedido(listapedidos, pedido, ""))
                        {
                            //foreach (var item in listapedidos)
                            //{
                            //    Quirofano.ProductoBodega(item.PRO_CODIGO_BARRAS, item.PDD_CANTIDAD.ToString(), FacturaPAR.BodegaPorDefecto);
                            //}

                            //CREO EL DESPACHO AUTOMATICO PARA BODEGA MUSHUÑAN
                            if (PisoBodega.Rows.Count > 0)
                            {

                                if (PisoBodega.Rows[0]["NIV_BODEGA"].ToString() == "61")
                                {
                                    List<DtoDespachos> despachos = new List<DtoDespachos>();
                                    DtoDespachos xdespacho = new DtoDespachos();
                                    xdespacho.observacion = "DESPACHADO DESDE MUSHUÑAN";
                                    xdespacho.ped_codigo = Convert.ToInt64(pedido.PED_CODIGO);

                                    despachos.Add(xdespacho);

                                    if (!NegPedidos.InsertarDespachos(despachos, 0, (DateTime)pedido.PED_FECHA))
                                    {
                                        MessageBox.Show("No se pudo realizar despacho automatico." +
                                            "\r\nIntentelo manual desde el modulo de pedidos - Control despachos.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Este pedido no se guardara porque la red esta inestable consulte con el administrador y vuelva a intentar", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }
                        // ActualizarBodega(Pedido.ToString()); // se retira el Update de la bodega en el Kardex, por cambio a manejar IP-Bodega
                        //
                        CboAreasSelectionChanged(null, null);

                        //if (MessageBox.Show("¿Desea Imprimir Directamente a impresora por defecto?",
                        //    "HIS3000", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                        //{
                        //    if (Sesion.codDepartamento == 32 || Sesion.codDepartamento == 9)//Ticket para que vean precios por departamento CARDIOLOGIA E IMAGEN
                        //    {
                        //        //cargarDetalleFactura();
                        //        MessageBox.Show("Pedido No." + Pedido.ToString() + " fue ingresado correctamente.", "His3000");
                        //        /*impresion*/
                        //        frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Pedido, codigoArea, 1, 3);
                        //        frmPedidos.Show();
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Pedido No." + Pedido.ToString() + " fue ingresado correctamente.", "His3000");

                        //        frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Pedido, codigoArea, 0, 1);
                        //        frmPedidos.Show();
                        //    }
                        //}
                        //else
                        //{
                        Imprimir(codigoArea, Pedido, 1);
                        //}



                        //Cambios Edgar 20210330  ////queda pendiente ya que da error. se trata de actualizar el grid de dieta
                        //_paciente = NegPacientes.RecuperarPacientesAtencionUltimas(600, true, Convert.ToInt32(ateCodigo), null);
                        //var paciente = _paciente.OrderByDescending(p => p.fechaIngreso).FirstOrDefault();

                        //HABITACIONES habitacion = NegHabitaciones.RecuperarHabitacionId(paciente.codigoHabitacion);

                        //var detalleHabitacion = _pacientes.Where(p => p.codigoHabitacion == habitacion.hab_Codigo).OrderByDescending(p => p.fechaIngreso).FirstOrDefault();
                        //DataTable dietetica = new DataTable();
                        //dietetica = NegEvolucion.Dietetica(detalleHabitacion.historiaClincia);
                        //if (dietetica.Rows.Count > 0)
                        //{
                        //    txtObservacionDieta.Text = dietetica.Rows[0][0].ToString();
                        //}
                        //List<RecuperaDietetica> _dietetica = new List<RecuperaDietetica>();
                        //_dietetica = NegPacientes.RecuperaDietetica(detalleHabitacion.historiaClincia);
                        //xamDataPacienteDieta.DataSource = _dietetica.ToList();

                        ///fin

                        //xdpSolicitudMedicamentos.DataSource = NegPedidos.recuperarListaPedidos(atencionActiva.ATE_CODIGO, Parametros.FarmaciaPAR.PedidoMedicamentos);
                    }
                }
                //}
                //else
                //{
                //    if (usuario.usuarioActual != 0)
                //        MessageBox.Show("Usuario no es correcto el Pedidos se anulara", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
            }
            catch (System.FormatException fe)
            {
                MessageBox.Show(fe.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //}
            //else
            //{
            //    if (Sesion.codDepartamento == 30)
            //        MessageBox.Show("Personal de Dietetica, debe hacer pedidos desde el módulo PEDIDOS ESPECIALES.", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    else
            //        MessageBox.Show("Opción Solo Para Enfermeria", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
            //}

        }
        public void Imprimir(int pea_codigo, int pedido, int tipo)
        {
            string NombreImpresora = "";//Donde guardare el nombre de la impresora por defecto

            //Busco la impresora por defecto
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                PrinterSettings a = new PrinterSettings();
                a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                if (a.IsDefaultPrinter)
                {
                    NombreImpresora = PrinterSettings.InstalledPrinters[i].ToString();
                }
            }
            if (tipo == 1)
            {
                if (Sesion.codDepartamento == 32 || Sesion.codDepartamento == 9)//Ticket para que vean precios por departamento CARDIOLOGIA E IMAGEN
                {
                    //cargarDetalleFactura();
                    MessageBox.Show("Pedido No." + pedido.ToString() + " fue ingresado correctamente.", "His3000");
                    /*impresion*/
                    frmImpresionPedidos frmPedidos = new frmImpresionPedidos(pedido, pea_codigo, NombreImpresora, 3);
                    frmPedidos.Show();
                }
                else
                {
                    MessageBox.Show("Pedido No." + pedido.ToString() + " fue ingresado correctamente.", "His3000");

                    frmImpresionPedidos frmPedidos = new frmImpresionPedidos(pedido, pea_codigo, NombreImpresora, 1);
                    frmPedidos.Show();
                }
            }
            else
            {
                frmImpresionPedidos frmPedidos = new frmImpresionPedidos(pedido, pea_codigo, NombreImpresora, 0);
                frmPedidos.ShowDialog();
            }

        }
        int conteo = 1; //valida solo 3 intentos 
        public void ActualizarBodega(string numdoc)
        {
            if (NegPedidos.ActualizarBodega(numdoc, Parametros.FacturaPAR.BodegaPorDefecto))
            {
                conteo = 1; //reinicio el conteo
            }
            else
            {
                if (conteo <= 3)
                {
                    Thread.Sleep(2000); //Espero 2 segundos
                    ActualizarBodega(numdoc);
                    conteo++;
                }
                else
                    MessageBox.Show("No se ha podido actualizar el kardex", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnEnQuirofano_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_atencionActiva.ATE_EN_QUIROFANO != null)
            {
                if (_atencionActiva.ATE_EN_QUIROFANO.Value)
                {
                    _atencionActiva.ATE_EN_QUIROFANO = false;
                    btnEnQuirofano.Background = Brushes.AliceBlue;
                }
                else
                {
                    _atencionActiva.ATE_EN_QUIROFANO = true;
                    btnEnQuirofano.Background = Brushes.Red;
                }
            }
            else
            {
                _atencionActiva.ATE_EN_QUIROFANO = true;
                btnEnQuirofano.Background = Brushes.Red;
            }
            NegAtenciones.EditarAtencionAdmision(_atencionActiva, 0);
        }

        private void BtnImprimirClick(object sender, RoutedEventArgs e)
        {
            try
            {
                frm_CensoDiario censo = new frm_CensoDiario();
                censo.ShowDialog();
                //var rptObject = new Report();
                //rptObject.ReportSettings.PageOrientation = PageOrientation.Landscape;
                //var section = new EmbeddedVisualReportSection(xamDataPresenterPacientes);
                //rptObject.Sections.Add(section);
                //xamRepPreReportes.GeneratePreview(rptObject, true, true);
                //tabItemReportes.Focus();
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void XceTipoVistaPacientesSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                switch (e.NewValue.ToString())
                {
                    case "Carrusel":
                        xamDataPresenterPacientes.View = _xamCarouselView;
                        xamDataPresenterPacientes_Pedidos.View = _xamCarouselView;
                        break;
                    case "Ficha":
                        xamDataPresenterPacientes.View = _xamCardView;
                        xamDataPresenterPacientes_Pedidos.View = _xamCardView;
                        break;
                    case "Grilla":
                        xamDataPresenterPacientes.View = _xamGridView;
                        xamDataPresenterPacientes_Pedidos.View = _xamGridView;
                        break;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void XceEstadoEpicrisisValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (_controlesCargados)
                {
                    //Cambios Edgar 20210330
                    //Todos
                    if (xceEstadoEpicrisis.SelectedIndex == 0)
                        _pacientes = NegPacientes.Todos();

                    //Sin Anamnesis
                    else if (xceEstadoEpicrisis.SelectedIndex == 1)
                        _pacientes = NegPacientes.SinAnamnesis();
                    //_pacientes = NegPacientes.RecuperarPacientesAtencionActiva("si");

                    //Sin Epicrisis
                    else if (xceEstadoEpicrisis.SelectedIndex == 2)
                        _pacientes = NegPacientes.SinEpicrisis();
                    //_pacientes = NegPacientes.RecuperarPacientesAtencionActiva("no");

                    //Sin Form008
                    else if (xceEstadoEpicrisis.SelectedIndex == 3)
                        _pacientes = NegPacientes.SinEmergencia();

                    //Sin Protocolo
                    else if (xceEstadoEpicrisis.SelectedIndex == 4)
                        _pacientes = NegPacientes.SinProtocolo();

                    //fin---------------------------------------------------------------




                    xamDataPresenterPacientes.DataSource = _pacientes.ToList();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void XamDataPresenterPacientesMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (xamDataPresenterPacientes.ActiveDataItem != null)
                {
                    var paciente = (DtoPacientesAtencionesActivas)xamDataPresenterPacientes.ActiveDataItem;
                    HABITACIONES habitacion = NegHabitaciones.RecuperarHabitacionId(paciente.codigoHabitacion);
                    MostrarInfHabitacion(habitacion);
                }
                else
                {
                    //  MessageBox.Show("Por favor seleccione un paciente de la lista", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception err)
            {
                //  MessageBox.Show("Por favor seleccione un paciente de la lista", err.Message, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void CboAreasSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboAreas.SelectedItem != null)
                {

                    PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cboAreas.SelectedItem;
                    List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
                    if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
                        cmb_Rubros.ItemsSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
                    else
                        cmb_Rubros.ItemsSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

                    cmb_Rubros.DisplayMemberPath = "RUB_NOMBRE".Trim();
                    cmb_Rubros.SelectedValuePath = "RUB_CODIGO";
                    cmb_Rubros.SelectedIndex = 0;


                    //cargarProductos();
                }

                if (infHabitacion.IsVisible)
                {
                    if (_atencionActiva != null)
                    {
                        int Resultado = 0;
                        Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "MUESTRA PRECIOS PEDIDOS");
                        int codigoArea = Convert.ToInt16(cmb_Rubros.SelectedValue);
                        if (codigoArea != 0)
                        {
                            if (codigoArea == 100)//Cambiar Edgar 20210216
                            {
                                List<DtoPedidos> listaPedidos = NegPedidos.ListaPedidosTodosRubros(_atencionActiva.ATE_CODIGO);

                                xdpSolicitudMedicamentos.DataSource = listaPedidos;
                            }
                            else
                            {
                                List<DtoPedidos> lista = NegPedidos.ListaPedidos(codigoArea, _atencionActiva.ATE_CODIGO);
                                xdpSolicitudMedicamentos.DataSource = lista;
                            }

                        }
                        if (Resultado == 0)
                        {
                            xdpSolicitudMedicamentos.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "HIS3000", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            //    try
            //    {
            //        if (infHabitacion.IsVisible)
            //        {
            //            if (_atencionActiva != null)
            //            {
            //                int Resultado = 0;
            //                Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario,"MUESTRA PRECIOS PEDIDOS");
            //                    Int16 codigoArea = Convert.ToInt16(cboAreas.SelectedValue);
            //                    List<DtoPedidos> lista = NegPedidos.recuperarListaPedidosVistaPendientesPorArea(codigoArea, _atencionActiva.ATE_CODIGO);
            //                    xdpSolicitudMedicamentos.DataSource = lista;

            //                if (Resultado == 0)
            //                {
            //                    xdpSolicitudMedicamentos.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
            //                }
            //            }
            //        }

            //        if (cboAreas.SelectedItem != null)
            //        {

            //            PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cboAreas.SelectedItem;
            //            List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
            //            if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
            //                cmb_Rubros.ItemsSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
            //            else
            //                cmb_Rubros.ItemsSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();

            //            cmb_Rubros.DisplayMemberPath = "RUB_NOMBRE".Trim();
            //            cmb_Rubros.SelectedValuePath = "RUB_CODIGO";
            //            cmb_Rubros.SelectedIndex = 0;


            //        //cargarProductos();
            //        }
            //}
            //    catch(Exception err)
            //    {
            //        MessageBox.Show("", err.Message, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //    }
        }

        private void XamTabControlInfSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private void BtnIngresoSalasClick(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //valido servicio
                if (trvSalas.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione un servicio", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                //valido medico
                if (Convert.ToInt32(cboMedicos.SelectedValue) <= 0)
                {
                    MessageBox.Show("Seleccione un médico", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                var ingresoSala = new INTERVENCION_MEDICA1();
                ingresoSala.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                ingresoSala.INT_ESTADO = true;
                ingresoSala.INT_FECHA_FIN = (DateTime)xdtpFechaEgreso.Value;
                ingresoSala.INT_FECHA_INI = (DateTime)xdtpFechaInigreso.Value;
                ingresoSala.INT_TIPO = Convert.ToByte(trvSalas.SelectedValue);
                ingresoSala.MED_CODIGO = Convert.ToInt32(cboMedicos.SelectedValue);
                ingresoSala.ATE_CODIGO = _atencionActiva.ATE_CODIGO;
                NegIngresoSalas.CrearIngresoSala(ingresoSala);
                //recupero la lista de ingresos
                _listaIntervenciones = NegIngresoSalas.ListarPorAtencion(_atencionActiva.ATE_CODIGO); ;
                xdpSalas.DataSource = _listaIntervenciones;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void XdpSalasKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Delete)
                {
                    if (xdpSalas.ActiveDataItem != null)
                    {
                        var ingreso = (DtoLstIngresoSala)xdpSalas.ActiveDataItem;
                        NegIngresoSalas.Eliminar(ingreso.INT_CODIGO);
                        _listaIntervenciones.Remove(ingreso);
                        xdpSalas.DataSource = null;
                        xdpSalas.DataSource = _listaIntervenciones;
                    }
                    else
                        MessageBox.Show("Seleccione el ingreso a eliminar", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdjuntosClick(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ATENCIONES ate = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(xamTxtNumAtencion.Text));
                PACIENTES pac = NegPacientes.RecuperarPacienteID(ate.PACIENTES.PAC_CODIGO);
                string paciente = pac.PAC_NOMBRE1 + " " + pac.PAC_NOMBRE2;
                string apellido = pac.PAC_APELLIDO_PATERNO + " " + pac.PAC_APELLIDO_MATERNO;
                string numeroAtencion = ate.ATE_NUMERO_ATENCION;
                string numeroHistoria = pac.PAC_HISTORIA_CLINICA;
                int codPaciente = pac.PAC_CODIGO;
                int codAtencion = ate.ATE_CODIGO;

                DataTable x = NegDietetica.getDataTable("PathLocalHC");

                string strDirectorio = numeroHistoria + "//" + codAtencion + "//";
                if (x.Rows.Count > 0)
                {
                    string strPathGeneral = x.Rows[0][0].ToString();

                    var expArchivos = new GeneralApp.ControlesWinForms.ExploradorLocal(strDirectorio, strPathGeneral, paciente, codAtencion.ToString());
                    expArchivos.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No se encontro datos para cargar microfilm.", "HIS3000");
                    return;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnComentariosClick(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void BtnExpExcelClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            { DefaultExt = ".xlsx", Filter = "Archivos de Excel (.xlsx)|*.xlsx" };
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                var writer = new DataPresenterExcelExporter();
                writer.ExportAsync(xamDataPresenterPacientes, dlg.FileName, WorkbookFormat.Excel2007);
            }
        }

        private void BtnExpWordClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog
            { DefaultExt = ".docx", Filter = "Archivos de Excel (.docx)|*.docx" };
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                var writer = new DataPresenterWordWriter();
                writer.ExportAsync(xamDataPresenterPacientes, dlg.FileName);
            }
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                tabItemExHabitaciones.Focus();
                List<HABITACIONES> habitacionesLista;

                if (Piso == 99)
                {
                    habitacionesLista = NegHabitaciones.listaHabitaciones().OrderBy(h => h.hab_Numero).ToList();
                }
                else
                {
                    habitacionesLista = NegHabitaciones.listaHabitaciones(Piso).OrderBy(h => h.hab_Numero).ToList();
                }

                CargarHabitacionesVistaGen(habitacionesLista);
                control = 1;

            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void arbolHabitaciones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            try
            {
                String tipo;
                var item = (TreeViewItem)arbolHabitaciones.SelectedItem;
                Int16 codigo = Convert.ToInt16(item.Tag);
                List<HABITACIONES> habitacionesLista;


                //habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.HABITACIONES_ESTADO.HES_CODIGO).ToList();
                habitacionesLista = NegHabitaciones.listaHabitaciones(codigo).OrderBy(h => h.hab_Numero).ToList();
                CargarHabitacionesVistaGen(habitacionesLista);
                control = 0;


            }
            catch (Exception err)
            {
                // MessageBox.Show(err.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // Se crea por que cuando un paciente no tenia creado el formulario emitia error al crear la Evolucion. // Mario //29-09-2023
                #region Crear Evolucion 
                List<FORMULARIOS_HCU> formularios = NegFormulariosHCU.RecuperarFormulariosHCU();
                List<ATENCION_DETALLE_FORMULARIOS_HCU> formulariosAtencion = NegAtencionDetalleFormulariosHCU.listaFormulariosAtencion(_atencionActiva.ATE_CODIGO);
                PACIENTES paciente = NegPacientes.RecuperarPacienteID(_atencionActiva.PACIENTES.PAC_CODIGO);
                FORMULARIOS_HCU formul = formularios.FirstOrDefault(f => f.FH_CODIGO == 4);
                ATENCION_DETALLE_FORMULARIOS_HCU detalle = formulariosAtencion.FirstOrDefault(d => d.FORMULARIOS_HCUReference.EntityKey == formul.EntityKey);
                if (detalle == null)
                {
                    detalle = new ATENCION_DETALLE_FORMULARIOS_HCU();
                    detalle.ADF_CODIGO = NegAtencionDetalleFormulariosHCU.MaxCodigo() + 1;
                    detalle.FORMULARIOS_HCUReference.EntityKey = formul.EntityKey;
                    detalle.ATENCIONESReference.EntityKey = _atencionActiva.EntityKey;
                    detalle.ADF_FECHA_INGRESO = DateTime.Now;
                    detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    
                    HC_EVOLUCION evolucion = new HC_EVOLUCION();
                    evolucion.EVO_CODIGO = NegEvolucion.ultimoCodigo() + 1;
                    evolucion.ATENCIONESReference.EntityKey = _atencionActiva.EntityKey;
                    evolucion.EVO_FECHA_CREACION = DateTime.Now;
                    evolucion.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                    evolucion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
                    evolucion.PAC_CODIGO = paciente.PAC_CODIGO;
                    evolucion.EVO_ESTADO = true;
                    NegEvolucion.crearEvolucion(evolucion);
                    detalle.REF_CODIGO = evolucion.EVO_CODIGO;
                    NegAtencionDetalleFormulariosHCU.Crear(detalle);
                }
                #endregion
                DataTable validadepartamento = new DataTable();
                validadepartamento = NegEvolucion.VerificaDepartamento(Sesion.codUsuario);
                if (validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "DIRECCION MEDICA"
                    || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "SISTEMAS"
                    || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "MEDICOS"
                    || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "MEDICO TRATANTE"
                    || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "MEDICOS GENERAL")
                {
                    var evolucion = new frm_Evolucion(_atencionActiva.ATE_CODIGO, true);
                    evolucion.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Módulo Solo Para Médicos", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                evoluciones = NegEvolucion.recuperarEvolucionPorAtencion(_atencionActiva.ATE_CODIGO);
                if (evoluciones != null)
                    xdpObservaciones.DataSource = NegEvolucionDetalle.listaNotasEvolucion(evoluciones.EVO_CODIGO);
            }
            catch (ExecutionEngineException error)
            {

            }

        }

        private void lblUsuario_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void xdpObservaciones_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e) // Boton para navegar entre las habitaciones ocupadas / Giovanny Tapia / 13/09/2012
        {
            //

            //MostrarInfHabitacion(habitacion)
            int i = 0;
            int Pestana;
            Pestana = xamTabControlPac.SelectedIndex;


            foreach (var habitacion in _habitacionesLista)
            {
                // MessageBox.Show("HOLA");

                if (xamTxtHabitacion.Text == habitacion.hab_Numero)
                {
                    i++;
                }
                else
                {
                    if (i > 0)
                    {
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 1)//|| habitacion.HABITACIONES_ESTADO.HES_CODIGO == 2)
                        {
                            MostrarInfHabitacion(habitacion);
                            xamTabControlPac.SelectedIndex = Pestana;
                            return;
                        }

                    }
                }
            }

        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e) // Boton para navegar entre las habitaciones ocupadas / Giovanny Tapia / 13/09/2012
        {
            HABITACIONES H = new HABITACIONES();

            //MostrarInfHabitacion(habitacion)
            int i = 0;
            int x = 0;

            int Pestana;
            Pestana = xamTabControlPac.SelectedIndex;

            for (x = (_habitacionesLista.Count() - 1); x >= 0; x--)
            {
                H = _habitacionesLista[x];

                if (xamTxtHabitacion.Text == H.hab_Numero)
                {
                    i++;
                }
                else
                {
                    if (i > 0)
                    {
                        if (H.HABITACIONES_ESTADO.HES_CODIGO == 1)//|| H.HABITACIONES_ESTADO.HES_CODIGO == 2)
                        {
                            MostrarInfHabitacion(H);
                            xamTabControlPac.SelectedIndex = Pestana;
                            return;
                        }

                    }
                }
            }


        }

        private void btnExamenSangre_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Reportes\\His3000Reportes.mdb";
            //string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Reportes\\bdFormulario003.mdb";
            try
            {
                OleDbConnection database;
                string queryDeleteString;

                int i = 0;

                database = new OleDbConnection(connectionString);
                database.Open();

                queryDeleteString = " DELETE * FROM rptExamenSangre ";
                OleDbCommand sqlDelete = new OleDbCommand();
                sqlDelete.CommandText = queryDeleteString;
                sqlDelete.Connection = database;
                sqlDelete.ExecuteNonQuery();

                List<ATENCION_DETALLE_CATEGORIAS> ATDC = new List<ATENCION_DETALLE_CATEGORIAS>();
                EMPRESA Emp = new EMPRESA();
                PACIENTES Pac = new PACIENTES();
                HC_ANAMNESIS AN = new HC_ANAMNESIS();
                HC_ANAMNESIS_DIAGNOSTICOS AND = new HC_ANAMNESIS_DIAGNOSTICOS();
                ATENCIONES_DETALLE_SEGUROS ADS = new ATENCIONES_DETALLE_SEGUROS();
                ANEXOS_IESS AI = new ANEXOS_IESS();
                ANEXOS_IESS AI1 = new ANEXOS_IESS();
                List<ATENCION_DETALLE_CATEGORIAS> ADC = new List<ATENCION_DETALLE_CATEGORIAS>();

                ATDC = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(_atencionActiva.ATE_CODIGO);

                if (ATDC == null)
                {
                    MessageBox.Show("El paciente no tiene ingresado un seguro");
                    return;
                }

                foreach (var atencion in ATDC)
                {
                    if (Convert.ToInt64(atencion.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 21)
                    {
                        i = 1;
                    }
                }

                if (i == 0)
                {
                    MessageBox.Show("El paciente no tiene seguro IESS", "His3000");
                    return;
                }
                Pac = NegPacientes.RecuperarPacienteID(_atencionActiva.PACIENTES.PAC_CODIGO);
                Emp = NegEmpresa.RecuperaEmpresa();
                AN = NegAnamnesis.recuperarAnamnesisPorAtencion(_atencionActiva.ATE_CODIGO);
                if (AN == null)
                {
                    MessageBox.Show("El paciente no tiene Anamnesis");
                    return;
                }
                AND = NegAnamnesis.recuperarAnamnesisDiagnostico(AN.ANE_CODIGO);
                if (AND == null)
                {
                    MessageBox.Show("El paciente no tiene CI10");
                    return;
                }
                ADS = NegAtencionDetalleSeguros.RecuAtencionesDetalleSeguros(_atencionActiva.ATE_CODIGO);
                if (ADS == null)
                {
                    MessageBox.Show("No se a definido el titular del seguro.");
                    return;
                }
                ADC = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(_atencionActiva.ATE_CODIGO);
                if (ADC == null)
                {
                    MessageBox.Show("El paciente no especificado el tipo de seguro que posee.");
                    return;
                }
                AI1 = NegAnexos.RecuperarAnexos(Convert.ToInt32(ADS.ADS_ASEGURADO_PARENTESCO));
                if (AI1 == null)
                {
                    MessageBox.Show("No se a especificado el parentesco");
                    return;
                }
                AI = NegAnexos.RecuperarAnexos(Convert.ToInt32(ADC[0].HCC_CODIGO_TS));
                if (AI == null)
                {
                    MessageBox.Show("No se a especificado el tipo de seguro.");
                    return;
                }

                if (AI1.ANI_DESCRIPCION == "TITULAR")
                {
                    queryDeleteString = " insert into rptExamenSangre values('" + Emp.EMP_NOMBRE + "','" + Emp.EMP_DIRECCION + "','" + Pac.PAC_APELLIDO_MATERNO + "','" + Pac.PAC_APELLIDO_PATERNO + "','" + Pac.PAC_NOMBRE1 + "','" + Pac.PAC_NOMBRE2 + "','" + Pac.PAC_GENERO + "','" + Pac.PAC_FECHA_NACIMIENTO + "','" + Pac.PAC_IDENTIFICACION + "','" + AND.CDA_DESCRIPCION + "','" + AND.CIE_CODIGO + "','" + AI1.ANI_DESCRIPCION + "','" + Pac.PAC_APELLIDO_PATERNO + "','" + Pac.PAC_APELLIDO_MATERNO + "','" + Pac.PAC_NOMBRE1 + "','" + Pac.PAC_NOMBRE2 + "','" + ADS.ADS_ASEGURADO_DIRECCION + "','" + ADS.ADS_ASEGURADO_CEDULA + "','" + AI.ANI_CODIGO + "')";
                }
                else
                {
                    queryDeleteString = " insert into rptExamenSangre values('" + Emp.EMP_NOMBRE + "','" + Emp.EMP_DIRECCION + "','" + Pac.PAC_APELLIDO_MATERNO + "','" + Pac.PAC_APELLIDO_PATERNO + "','" + Pac.PAC_NOMBRE1 + "','" + Pac.PAC_NOMBRE2 + "','" + Pac.PAC_GENERO + "','" + Pac.PAC_FECHA_NACIMIENTO + "','" + Pac.PAC_IDENTIFICACION + "','" + AND.CDA_DESCRIPCION + "','" + AND.CIE_CODIGO + "','" + AI1.ANI_DESCRIPCION + "','" + ADS.ADS_ASEGURADO_NOMBRE + "','" + ADS.ADS_ASEGURADO_NOMBRE + "','" + ADS.ADS_ASEGURADO_NOMBRE + "','" + ADS.ADS_ASEGURADO_NOMBRE + "','" + ADS.ADS_ASEGURADO_DIRECCION + "','" + ADS.ADS_ASEGURADO_CEDULA + "','" + AI.ANI_CODIGO + "')";
                }

                OleDbCommand sqlInsert = new OleDbCommand();
                sqlInsert.CommandText = queryDeleteString;
                sqlInsert.Connection = database;
                sqlInsert.ExecuteNonQuery();

                frmReportes FRM = new frmReportes(1, "ExamenSangre");
                FRM.Show();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //private void CargarReporte()
        //{
        //    DataTable ds1 = new DataTable();
        //    dtsImpresionPedido ds2 = new dtsImpresionPedido();                
        //    DataRow dr2;

        //    ds1 = NegPedidos.DatosPedido(85);

        //    //ds1 = ds2.Tables.Add("Pedidos");

        //    if (ds1 != null && ds1.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr1 in ds1.Rows)
        //        {
        //            dr2 = ds2.Tables["Pedido"].NewRow();

        //            dr2["PACIENTE"] = dr1["PACIENTE"];
        //            dr2["IDENTIFICACION"] = dr1["IDENTIFICACION"];
        //            dr2["FECHA"] = dr1["FECHA"];
        //            dr2["PEDIDO"] = dr1["PEDIDO"];
        //            dr2["CODIGO_USUARIO"] = dr1["CODIGO_USUARIO"];
        //            dr2["USUARIO"] = dr1["USUARIO"];
        //            dr2["MEDICO"] = dr1["MEDICO"];
        //            dr2["PRO_CODIGO"] = dr1["PRO_CODIGO"];
        //            dr2["PRODUCTO"] = dr1["PRODUCTO"];
        //            dr2["PDD_CANTIDAD"] = dr1["PDD_CANTIDAD"];
        //            dr2["PDD_VALOR"] = dr1["PDD_VALOR"];
        //            dr2["PDD_IVA"] = dr1["PDD_IVA"];
        //            dr2["PDD_TOTAL"] = dr1["PDD_TOTAL"];

        //            ds2.Tables["Medida"].Rows.Add(dr2);
        //        }

        //        //ReportDocument reporte = new ReportDocument();
        //        //Reporte = reporte;
        //        //Reporte.Load(Server.MapPath("..\\Crystals\\crpListadoMedidas2.rpt"));
        //        //Reporte.Database.Tables["Medida"].SetDataSource(ds2.Tables["Medida"]);
        //        //crvReporte.ReportSource = Reporte;
        //        //crvReporte.DataBind();

        //        CrystalReport objRpt = new CrystalReport ();
        //        objRpt.SetDataSource (ds.Tables [1]);
        //        crystalReportViewer1.ReportSource objRpt =;
        //        crystalReportViewer1.Refresh ();

        //    }
        //    else
        //    {
        //        //MostarMensaje("No existen registros para esta Matriz.");
        //    }
        //}


        private void btnDevolucion_Click(object sender, RoutedEventArgs e)
        {
            DataTable validadepartamento = new DataTable();
            validadepartamento = NegEvolucion.VerificaDepartamento(Sesion.codUsuario);

            try
            {
                DataTable validadorHabitaciones = new DataTable();
                validadorHabitaciones = NegHabitaciones.ValidadorHabitaciones();
                bool validador = false;
                if (validadorHabitaciones.Rows.Count > 0)
                {
                    if (validadorHabitaciones.Rows[0][0].ToString() == "True")
                    {
                        validador = true;
                    }
                    else
                        validador = true;

                }
                if (validador)
                {
                    Int32 DevolucionNumero = 0;
                    DtoPedidos Item;
                    Item = (DtoPedidos)xdpSolicitudMedicamentos.ActiveDataItem; // Selecciono la fila que esta selecionada en el grid de articulos / Giovanny Tapia / 04/10/2012       
                    PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
                    ar = (PEDIDOS_AREAS)cboAreas.SelectedItem;
                    var codigoArea = (Int16)ar.DIV_CODIGO;
                    var codigoArea1 = (Int16)ar.PEA_CODIGO;
                    if (Item != null)
                    {
                        var solicitudMedicamentos = new frmDevolucionPedido(codigoArea1, xamTxtPaciente.Text, Item.MEDICO.ToString(), xamTxtHC.Text.ToString(), xamTxtNumAtencion.Text.ToString(), Item.PED_CODIGO.ToString(), Convert.ToInt64(_atencionActiva.ATE_CODIGO));
                        solicitudMedicamentos.ShowDialog();

                        DevolucionNumero = solicitudMedicamentos.DevolucionNumero;

                        if (DevolucionNumero != 0)
                        {
                            if (MessageBox.Show("¿Desea Imprimir Directamente a impresora por defecto?",
                            "HIS3000", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.No)
                            {
                                frmImpresionPedidos frmPedidos = new frmImpresionPedidos(DevolucionNumero, codigoArea1, 1, 0);
                                frmPedidos.ShowDialog();
                            }
                            else
                            {
                                Imprimir(codigoArea1, DevolucionNumero, 0);
                            }
                            CboAreasSelectionChanged(null, null);
                        }
                        else
                        {
                            MessageBox.Show("La devolución no se a guardado. Consulte con el administrador del sistema.", "His3000");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un pedido.", "His3000");
                    }
                }
                else
                    MessageBox.Show("Solo Se Puede Hacer Devoluciones Desde Farmacia", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe Seleccionar Un Producto para la Devolución: " + ex.ToString(), "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void BtnImprimeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DtoPedidos Item;
                if (Sesion.codDepartamento == 9 || Sesion.codDepartamento == 32 || Sesion.codDepartamento == 1)
                {

                    Item = (DtoPedidos)xdpSolicitudMedicamentos.ActiveDataItem;
                    if (Item != null)
                    {
                        PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
                        ar = (PEDIDOS_AREAS)cboAreas.SelectedItem;
                        var codigoArea = (Int16)ar.PEA_CODIGO;
                        //DataTable obtieneCodPedido = new DataTable();
                        //obtieneCodPedido = NegHabitaciones.ObtieneCodPedido(Convert.ToInt64(Item.PED_CODIGO.ToString()));

                        frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Convert.ToInt32(Item.PED_CODIGO.ToString()), codigoArea, 1, 3);
                        frmImpresionPedidos.reimpresion = true;
                        frmPedidos.Show();
                    }

                    //frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Convert.ToInt32(fila.Cells["Codigo_Pedido"].Value.ToString()), codigoArea, 1, 3);
                    //frmImpresionPedidos.reimpresion = true;
                    //frmPedidos.Show();
                }
                else
                {
                    Item = (DtoPedidos)xdpSolicitudMedicamentos.ActiveDataItem;
                    if (Item != null)
                    {
                        PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
                        ar = (PEDIDOS_AREAS)cboAreas.SelectedItem;
                        var codigoArea = (Int16)ar.PEA_CODIGO;
                        //DataTable obtieneCodPedido = new DataTable();
                        //obtieneCodPedido = NegHabitaciones.ObtieneCodPedido(Convert.ToInt64(Item.PED_CODIGO.ToString()));

                        frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Convert.ToInt32(Item.PED_CODIGO.ToString()), codigoArea, 1, 1);
                        frmImpresionPedidos.reimpresion = true;
                        frmPedidos.Show();
                    }
                }
            }
            catch (Exception err) { MessageBox.Show("Seleccione Un Item Para Reimprimir: " + err.Message, "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void btnActualizaHabitacion_Click(object sender, RoutedEventArgs e)
        {
            //NegHabitaciones.RecuperaNombrePacientes();
            NegHabitaciones.HabitacionesEnOrden();
            lblMensaje.Content = "Módulo de Habitaciones Actualizado por ultima vez a las: " + DateTime.Now.TimeOfDay.ToString();
            WindowLoaded(null, null);
        }

        private void txtCodigoMedico_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCodigoMedico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

                frm_AyudaMedicos Forma = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
                Forma.ShowDialog();

                txtCodigoMedico.Text = Forma.campoPadre.Text;

                if (Forma.campoPadre.Text != string.Empty)
                    CargarMedico(Convert.ToInt32(Forma.campoPadre.Text.ToString()));

            }
        }

        private void CargarMedico(int codMedico)
        {

            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                DataTable med = NegMedicos.MedicoIDValida(codMedico);
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000");
                    txtCodigoMedico.Text = "";
                    txtDatosMedico.Text = "";
                    return;
                }
                txtDatosMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            }
            else
                txtDatosMedico.Text = string.Empty;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            NegValidaciones.alzheimer();
        }

        private void btnEvoEnfermeria_StylusEnter(object sender, StylusEventArgs e)
        {

        }

        private void btnEvoEnfermeria_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable validadepartamento = new DataTable();
                validadepartamento = NegEvolucion.VerificaDepartamento(Sesion.codUsuario);
                if (validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "ENFERMERIA" || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "SISTEMAS")
                {
                    //var evolucionenfermeras = new frm_EvolucionEnfermeras(_atencionActiva.ATE_CODIGO, true);
                    var evolucionenfermeras = new frmEvolucionEnfermeria(_atencionActiva.ATE_CODIGO);
                    evolucionenfermeras.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Módulo Solo Para Dartamento De Enfermeria", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //evoluciones = NegEvolucion.recuperarEvolucionPorAtencion(_atencionActiva.ATE_CODIGO);
                //if (evolucionenfermeras != null)
                //    xdpObservaciones.DataSource = NegEvolucionDetalle.listaNotasEvolucion(evoluciones.EVO_CODIGO);
            }
            catch (ExecutionEngineException error)
            {

            }

        }

        private void cmbPisosSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbHabitacionesFINALSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            if (cmbHabitacionesFINAL.Text == "TODOS")
            {
                _pacientes = NegPacientes.Todos();
                xamDataPresenterPacientes.DataSource = _pacientes.ToList();
            }
            else if (cmbHabitacionesFINAL.Text == "HOSPITALIZADO")
            {
                //_pacientes = NegPacientes.RecuperarPacientesAtencionUltimasReporte(1, true, null, null, Piso);
                _pacientes = NegPacientes.TipoHospitalizacion();
                xamDataPresenterPacientes.DataSource = _pacientes.ToList();
            }
            else if (cmbHabitacionesFINAL.Text == "EMERGENCIA")
            {
                _pacientes = NegPacientes.TipoEmergencia();
                //_pacientes = NegPacientes.RecuperarPacientesAtencionUltimasReporte(2, true, null, null, Piso);
                xamDataPresenterPacientes.DataSource = _pacientes.ToList();
            }
            else if (cmbHabitacionesFINAL.Text == "OTROS")
            {
                //_pacientes = NegPacientes.RecuperarPacientesAtencionUltimasReporte(3, true, null, null, Piso);
                _pacientes = NegPacientes.TipoOtros();
                xamDataPresenterPacientes.DataSource = _pacientes.ToList();
            }
        }

        private void cmb_Rubros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (infHabitacion.IsVisible)
            {
                if (_atencionActiva != null)
                {
                    int Resultado = 0;
                    Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "MUESTRA PRECIOS PEDIDOS");
                    Int16 codigoArea = Convert.ToInt16(cmb_Rubros.SelectedValue);
                    //List<DtoPedidos> lista = NegPedidos.recuperarListaPedidosVistaPendientesPorArea(codigoArea, _atencionActiva.ATE_CODIGO);
                    if (codigoArea != 0)
                    {
                        if (codigoArea == 100) //Cambios Edgar 20210216 Todos los rubros
                        {
                            List<DtoPedidos> listaPedidos = NegPedidos.ListaPedidosTodosRubros(_atencionActiva.ATE_CODIGO);

                            xdpSolicitudMedicamentos.DataSource = listaPedidos;
                        }
                        else
                        {
                            List<DtoPedidos> listaPedidos = NegPedidos.ListaPedidos(codigoArea, _atencionActiva.ATE_CODIGO);

                            xdpSolicitudMedicamentos.DataSource = listaPedidos;
                        }

                    }


                    if (Resultado == 0)
                    {
                        xdpSolicitudMedicamentos.FieldLayouts[0].Fields[5].Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void btnMedicamento_Click(object sender, RoutedEventArgs e)
        {

            DataTable validadepartamento = new DataTable();
            validadepartamento = NegEvolucion.VerificaDepartamento(Sesion.codUsuario);
            if (validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "ENFERMERIA" || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "SISTEMAS")
            {
                frmLogin frm = new frmLogin(true);
                frm.ShowDialog();
                USUARIOS user = frm.user;
                if (user.USR != "")
                {
                    frm_admisnitracionMedicamentos kardexMedicamento = new frm_admisnitracionMedicamentos(xamTxtNumAtencion.Text, user);
                    kardexMedicamento.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    kardexMedicamento.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Debe Ingresar Login para abrir este formulario", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Módulo Solo Para Dartamento De Enfermeria", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btnKardexInsumo_Click(object sender, RoutedEventArgs e)
        {
            DataTable validadepartamento = new DataTable();
            validadepartamento = NegEvolucion.VerificaDepartamento(Sesion.codUsuario);
            if (validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "ENFERMERIA" || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "SISTEMAS")
            {
                //frm_descargoInsumos insumos = new frm_descargoInsumos(xamTxtNumAtencion.Text);
                frm_admisnitracionInsumos insumos = new frm_admisnitracionInsumos(xamTxtNumAtencion.Text);
                insumos.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                insumos.ShowDialog();
            }
            else
            {
                MessageBox.Show("Módulo Solo Para Dartamento De Enfermeria", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btnHojaAlta_Click(object sender, RoutedEventArgs e)
        {
            //Egreso2020(Convert.ToString(ateCodigo));
            if (!NegCertificadoMedico.ExisteReceta(Convert.ToInt64(ateCodigo)))
            {
                if (MessageBox.Show("Paciente no tiene receta médica. Validar con el médico.\r\n ¿Continuar de todas formas?",
                    "HIS3000", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    frm_Egreso_preview form = new frm_Egreso_preview(Convert.ToInt32(ateCodigo), xamTxtHC.Text);
                    form.ShowDialog();
                }
            }
            else
            {
                frm_Egreso_preview form = new frm_Egreso_preview(Convert.ToInt32(ateCodigo), xamTxtHC.Text);
                form.ShowDialog();
            }
        }

        public void Egreso2020(string codigo)
        {
            //DataTable DatosPcte = NegDietetica.getDataTable("GetEgreso_DatosPaciente", codigo);
            //DataTable Medicos = NegDietetica.getDataTable("GetEgreso_MedicosEvolucion", codigo);
            //DataTable Garantias = NegDietetica.getDataTable("GetEgreso_Garantias", codigo);
            //DataTable HistorialHabitacion = NegDietetica.getDataTable("GetEgreso_HistorialHabitacion", codigo);
            //DataTable ConvenioSeguro = NegDietetica.getDataTable("GetEgreso_ConvenioSeguro", codigo);
            DateTime fechaNacimiento = NegHabitaciones.RecuperaFechaNacimiento(xamTxtHC.Text);
            try
            {
                #region//limpiar tablas
                ReportesHistoriaClinica r1 = new ReportesHistoriaClinica(); r1.DeleteTable("rptEgreso_DatosPcte");
                ReportesHistoriaClinica r2 = new ReportesHistoriaClinica(); r2.DeleteTable("rptEgreso_Medicos");
                ReportesHistoriaClinica r3 = new ReportesHistoriaClinica(); r3.DeleteTable("rptEgreso_Garantias");
                ReportesHistoriaClinica r4 = new ReportesHistoriaClinica(); r4.DeleteTable("rptEgreso_HabitacionHistorial");
                ReportesHistoriaClinica r5 = new ReportesHistoriaClinica(); r5.DeleteTable("rptEgreso_ConvenioSeguro");
                #endregion
                #region //empaquetar y guardar en tablas access
                //foreach (DataRow row in DatosPcte.Rows)
                //{
                //    ///edad
                //    var now = DateTime.Now;
                //    var birthday = fechaNacimiento;
                //    var yearsOld = now - birthday;

                //    int years = (int)(yearsOld.TotalDays / 365.25);
                //    string[] x = new string[] {
                //            row["PAC_HISTORIA_CLINICA"].ToString(),
                //            row["PACIENTE"].ToString(),
                //            row["PAC_IDENTIFICACION"].ToString(),
                //            row["PAC_FECHA_NACIMIENTO"].ToString(),
                //            row["hab_Numero"].ToString(),
                //            row["ATE_FECHA_INGRESO"].ToString(),
                //            row["ATE_CODIGO"].ToString(),
                //            row["ATE_NUMERO_ATENCION"].ToString(),
                //            row["MEDICO"].ToString(),
                //            row["TIP_DESCRIPCION"].ToString(),
                //            row["TIA_DESCRIPCION"].ToString(),
                //            row["ATE_DIAGNOSTICO_INICIAL"].ToString(),
                //            row["ATE_DIAGNOSTICO_FINAL"].ToString(),
                //            row["USUARIO"].ToString(),
                //            row["REFERIDO"].ToString(),
                //            row["FECHA_ALTA"].ToString(),
                //            years.ToString()
                //    };
                //    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                //    AUXma.InsertTable("rptEgreso_DatosPcte", x);
                //}

                //foreach (DataRow row in Medicos.Rows)
                //{
                //    string[] x = new string[] {
                //            row["NOM_USUARIO"].ToString()
                //    };
                //    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                //    AUXma.InsertTable("rptEgreso_Medicos", x);
                //}


                //foreach (DataRow row in Garantias.Rows)
                //{
                //    string[] x = new string[] {
                //            row["ADG_FECHA"].ToString(),
                //            row["TG_NOMBRE"].ToString(),
                //            row["ADG_VALOR"].ToString(),
                //            row["TITULAR"].ToString(),
                //            row["ADG_BANCO"].ToString(),
                //            row["ADG_DOCUMENTO"].ToString(),
                //            row["ADG_TIPOTARJETA"].ToString(),
                //            row["ADG_ESTATUS"].ToString(),
                //            row["ADG_OBSERVACION"].ToString()
                //    };
                //    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                //    AUXma.InsertTable("rptEgreso_Garantias", x);
                //}


                //foreach (DataRow row in HistorialHabitacion.Rows)
                //{
                //    string[] x = new string[] {
                //            row["OBSERVACION"].ToString(),
                //            row["FECHA_MOVIMIENTO"].ToString(),
                //            row["HORA"].ToString(),
                //            row["HABITACION"].ToString(),
                //            row["ANTERIOR"].ToString(),
                //            row["ESTADO"].ToString(),
                //            row["USUARIO"].ToString()
                //    };
                //    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                //    AUXma.InsertTable("rptEgreso_HabitacionHistorial", x);
                //}

                //foreach (DataRow row in ConvenioSeguro.Rows)
                //{
                //    string[] x = new string[] {
                //            row["CAT_NOMBRE"].ToString(),
                //            row["ADA_FECHA_INICIO"].ToString(),
                //            row["ADA_FECHA_FIN"].ToString(),
                //            row["ADA_MONTO_COBERTURA"].ToString()
                //    };
                //    ReportesHistoriaClinica AUXma = new ReportesHistoriaClinica();
                //    AUXma.InsertTable("rptEgreso_ConvenioSeguro", x);
                //}
                #endregion
                //llamo al reporte
                //frmReportes form = new frmReportes();
                //form.reporte = "EGRESO_LX";
                //form.Show();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

        }

        private void BtnBuscaMedico(object sender, RoutedEventArgs e)
        {
            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            frm_AyudaMedicos Forma = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            Forma.ShowDialog();
            if (Forma.campoPadre.Text != "")
                txtCodigoMedico.Text = Forma.campoPadre.Text;

            if (Forma.campoPadre.Text != string.Empty)
                CargarMedico(Convert.ToInt32(Forma.campoPadre.Text.ToString()));
        }

        private void btnImpresionDev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DtoPedidos Item;
                Item = (DtoPedidos)xdpSolicitudMedicamentos.ActiveDataItem;
                if (Item != null)
                {
                    PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
                    ar = (PEDIDOS_AREAS)cboAreas.SelectedItem;
                    var codigoArea = (Int16)ar.PEA_CODIGO;


                    //Busco el devCodigo con el pedcodigo en pedido_devolucion
                    DataTable DevCodigo = NegPedidos.RetornarDevCodigo(Item.PED_CODIGO);
                    if (DevCodigo.Rows.Count > 0)
                    {
                        foreach (DataRow item in DevCodigo.Rows)
                        {
                            if (Convert.ToInt64(item[0].ToString()) > 0)
                            {
                                frmImpresionPedidos frmPedidosDevolucion = new frmImpresionPedidos(Convert.ToInt32(item[0].ToString()), codigoArea, 1, 0);
                                frmImpresionPedidos.reimpresion = true;
                                frmPedidosDevolucion.Show();
                            }
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron Devoluciones con ese Nro. de pedido: " + Item.PED_CODIGO + ".", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    ////DataTable obtieneCodPedido = new DataTable();
                    ////obtieneCodPedido = NegHabitaciones.ObtieneCodPedido(Convert.ToInt64(Item.PED_CODIGO.ToString()));

                    ////if (DevolucionNumero != 0)
                    ////{

                    ////    frmImpresionPedidos frmPedidos = new frmImpresionPedidos(DevolucionNumero, codigoArea1, 1, 0);
                    ////    frmPedidos.Show();

                    ////}
                    ////else
                    ////{
                    ////    MessageBox.Show("La devolución no se a guardado. Consulte con el administrador del sistema.", "His3000");
                    ////}



                    //frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Convert.ToInt32(Item.PED_CODIGO.ToString()), codigoArea, 1, 1);
                    //frmPedidos.Show();
                }
            }
            catch (Exception err) { MessageBox.Show("Seleccione Un Item Para Reimprimir Devolución: " + err.Message, "HIS3000", MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void btnSignosVitales_Click(object sender, RoutedEventArgs e)
        {
            DataTable validadepartamento = new DataTable();
            validadepartamento = NegEvolucion.VerificaDepartamento(Sesion.codUsuario);
            if (validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "ENFERMERIA" || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "SISTEMAS")
            {
                frm_Form020 x = new frm_Form020(Convert.ToInt64(xamTxtNumAtencion.Text), xamTxtHC.Text);
                x.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("Módulo Solo Para Dartamento De Enfermeria", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void btnIngesta_Click(object sender, RoutedEventArgs e)
        {
            DataTable validadepartamento = new DataTable();
            validadepartamento = NegEvolucion.VerificaDepartamento(Sesion.codUsuario);
            if (validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "ENFERMERIA" || validadepartamento.Rows[0]["DEPARTAMENTO"].ToString() == "SISTEMAS")
            {
                frmIngestaEliminacion x = new frmIngestaEliminacion(Convert.ToInt64(xamTxtNumAtencion.Text), xamTxtHC.Text);
                x.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("Módulo Solo Para Dartamento De Enfermeria", "HIS3000", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }

}

