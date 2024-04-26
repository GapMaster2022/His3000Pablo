using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using His.Parametros;
using His.Entidades.Pedidos;
using His.Admision;
using His.Formulario;
using His.Entidades.Clases;
using His.HabitacionesUI;

namespace His.Dietetica
{
    public partial class frmQuirofanoPedidoPaciente : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string cie_codigo; //Recibe el codigo del procedimiento desde el explorador
        internal static string pac_codigo; //Recibe el codigo del paciente desde el explorador
        internal static string ate_codigo; //Recibe el codigo de atencion desde el explorador
        internal static string pro_descripcion; //Recibe la descripcion del procedimiento desde el explorador
        internal static string nombrepaciente; //Recibe el nombre del paciente desde el explorador
        internal static bool modotablet;
        internal static string orden;
        private static string codpro; //Variable que contiene el codigo del producto por cada fila del grid dentro del for
        private static string cantidad; //variable que contiene la cantidad del producto por cada fila del grid dentro del for
        private static string cant_usada; // variable que contiene el cambio del usuario dentro de la cantidad 
        internal static string medico; //almacena el nombre del medico tratante
        internal static string tipo; //almacena el tipo de tratamiento del paciente
        internal static string seguro; //almacena el nombre de la aseguradora del paciente
        internal static string genero; //almacena el genero del paciente 
        internal static string referido; //almacena el tipo de referido del paciente
        private static bool Despachar; //variable que verifica la existencia del stock con la cantidad que se va usar dentro del grid
        private static string usuario; //usuario del sistema
        private static string pantalla; //pantalla donde se enviaran los valores del teclado virtual y luego se enviara al grid al presionar enter
        private static int fila; //numero de la fila seleccionada dentro del grid
        private static int TablaCantidad; //contiene el numero de filas dentro del grid
        private static bool PedidoAdicional; //Variable que enviara a despachar el adicion si es verdadero
        internal static string cant; //Variable que almacena la cantidad de producto que se va a utilizar dentro del procedimiento
        internal static string codigo_producto; //Varible global que recoge el codigo del producto desde otro formulario
        private static string valorcelda;
        private static string id_usuario;
        private static string hab_codigo;
        private string numpedido;
        public int bodega = Sesion.bodega; //por defecto la bodega de quirofano.
        public frmQuirofanoPedidoPaciente()
        {
            InitializeComponent();
            datosPaciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
            CargarProcedimientoPaciente();
            CargarProcedimientosPaciente();
        }
        public frmQuirofanoPedidoPaciente(int bodega)
        {
            InitializeComponent();
            datosPaciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
            CargarProcedimientoPaciente();
            CargarProcedimientosPaciente();
            this.bodega = bodega;
        }
        ATENCIONES ultimaAtencion = new ATENCIONES();
        PACIENTES datosPaciente = new PACIENTES();
        HABITACIONES habitacion = new HABITACIONES();
        private void frmQuirofanoPedidoPaciente_Load(object sender, EventArgs e)
        {
            if (modotablet == true)
            {
                panelTecladoNumerico.Visible = true;
            }
            else
            {
                panelTecladoNumerico.Visible = false;
            }


            //datosPaciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));

            ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));

            habitacion = NegHabitaciones.RecuperarHabitacionId(ultimaAtencion.HABITACIONES.hab_Codigo);
            lblpaciente.Text = datosPaciente.PAC_APELLIDO_PATERNO + " " + datosPaciente.PAC_APELLIDO_MATERNO + " " + datosPaciente.PAC_NOMBRE1 + " " + datosPaciente.PAC_NOMBRE2;
            lblmedico.Text = ultimaAtencion.MEDICOS.MED_APELLIDO_PATERNO + " " + ultimaAtencion.MEDICOS.MED_APELLIDO_MATERNO + " " + ultimaAtencion.MEDICOS.MED_NOMBRE1 + " " + ultimaAtencion.MEDICOS.MED_NOMBRE2;
            DataTable TReferido = new DataTable();
            TReferido = NegDetalleCuenta.ReferidoPaciente(ultimaAtencion.ATE_CODIGO);
            DataTable DatosConvenioAtencion = new DataTable();
            DatosConvenioAtencion = NegAtenciones.CodigoConvenio(ultimaAtencion.ATE_CODIGO);
            if (TReferido.Rows.Count > 0)
                lblreferido.Text = TReferido.Rows[0][0].ToString();
            lblseguro.Text = DatosConvenioAtencion.Rows[0]["CAT_NOMBRE"].ToString();
            lbltipo.Text = tipo;
            lblgenero.Text = datosPaciente.PAC_GENERO;
            Bloquear();
            if (Despachar == false)
            {
                toolStripDespachar.Enabled = false;
            }
            else
            {
                toolStripDespachar.Enabled = true;
            }
            RecuperarUsuario();
            CargarProcedimientoPaciente();
            //if (cmbProcedimiento.SelectedIndex == -1)
            //{
            //    BloquearVacio();
            //}
        }
        public void BloquearVacio()
        {
            btnadicional.Enabled = false;
            toolStripAgregar.Enabled = false;
            btnEditar.Enabled = false;
            toolStripCerrar.Enabled = false;
            checkBox1.Enabled = false;
            btnDevolucion.Enabled = false;
        }
        public void AlternarColores(DataGridView dgv)
        {
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(238, 245, 253);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
        }
        public void CargarProcedimientoPaciente()
        {
            DataTable dt = new DataTable();
            dt = Quirofano.ProcedimientosPaciente(ate_codigo, datosPaciente.PAC_CODIGO.ToString(), bodega);
            //cmbProcedimiento.DataSource = null;
            //cmbProcedimiento.DataSource = dt;
            //cmbProcedimiento.DisplayMember = dt.Columns[1].ToString();
            //cmbProcedimiento.ValueMember = dt.Columns[0].ToString();
            cie_codigo = dt.Rows[0][0].ToString();
            cmbProcedimiento.Text = dt.Rows[0][1].ToString();
            CargarProcedimientosPaciente();
            MostrarRegistro();
            //Tabla que verifica si tiene valores dentro del registro
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.RegistroPaciente(ate_codigo, datosPaciente.PAC_CODIGO.ToString(), cie_codigo);
            foreach (DataRow item in Tabla.Rows)
            {
                if (item[0].ToString() == "")
                {
                    toolStripRegistro.Visible = false;
                    groupBox1.Visible = false;
                }
                else
                {
                    toolStripRegistro.Visible = true;
                    groupBox1.Visible = true;
                }
            }
            GuardarCantidades(); //Si no tiene cantidades del procedimiento con paciente se lo ingresera.
            ValidarProcedimientoCerrado();
        }
        public void SumarCantidades()
        {
            double sumaoriginal = 0;
            double sumausar = 0;
            foreach (DataGridViewRow item in TablaProcedimientos.Rows)
            {
                double cantoriginal, cantusada;
                cantoriginal = Convert.ToDouble(item.Cells[4].Value.ToString());
                sumaoriginal += cantoriginal;
                if (item.Cells[5].Value.ToString() == "")
                {
                    sumausar += 0;
                }
                else
                {
                    cantusada = Convert.ToDouble(item.Cells[5].Value.ToString());
                    sumausar += cantusada;
                }

            }
            lblcantoriginal.Text = Convert.ToString(sumaoriginal);
            lblcantusada.Text = Convert.ToString(sumausar);
        }
        public void ValidarStock()
        {
            foreach (DataGridViewRow item in TablaProcedimientos.Rows)
            {
                if (item.Cells[4].Value.ToString() != "")
                {
                    if (Convert.ToInt32(item.Cells[4].Value.ToString()) <= Convert.ToInt32(item.Cells[3].Value.ToString()))
                    {
                        Despachar = true;
                        toolStripDespachar.Enabled = true;
                    }
                    else
                    {
                        Despachar = false;
                        toolStripDespachar.Enabled = false;
                        break;
                    }
                }
                else
                {
                    Despachar = false;
                    toolStripDespachar.Enabled = false;
                }
            }
        }
        public void RefreshTablaProcedimiento()
        {
            TablaProcedimientos.DataSource = "";
            TablaProcedimientos.DataSource = NegQuirofano.PacienteProcedimientoRecuperado(Convert.ToInt64(cie_codigo), Convert.ToInt64(ate_codigo));
            SumarCantidades();
            AlertaStock();
            AlternarColores(TablaProcedimientos);
            //foreach (DataGridViewRow fila in TablaProcedimientos.Rows)
            //{
            //    fila.Height = 20;
            //}
            TablaCantidad = TablaProcedimientos.Rows.Count;
        }
        public void CargarProcedimientosPaciente()
        {
            if (cie_codigo != null)
            {
                List<REPOSICION_QUIROFANO> obj = new List<REPOSICION_QUIROFANO>();
                obj = NegQuirofano.ReposicionQuirofano(Convert.ToInt64(cie_codigo), Convert.ToInt64(ate_codigo));
                if (obj.Count == 0)
                {
                    INTERVENCIONES_REGISTRO_QUIROFANO reg = new INTERVENCIONES_REGISTRO_QUIROFANO();
                    reg = NegQuirofano.RegistroQuirofano(Convert.ToInt64(cie_codigo));
                    if(reg == null)
                    {
                        MessageBox.Show("Procedimiento mal registrado. Por favor volver a registrar el procedimiento para continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ATENCIONES ate = new ATENCIONES();
                        ate = NegAtenciones.RecuepraAtencionNumeroAtencion(Convert.ToInt64(ate_codigo));
                        var pacCodigo = ate.PACIENTESReference.EntityKey.EntityKeyValues[0].Value;
                        PACIENTES pac = new PACIENTES();
                        pac = NegPacientes.RecuperarPacienteID(Convert.ToInt32(pacCodigo));
                        frmQuirofanoRegistro.ate_codigo = ate_codigo;
                        frmQuirofanoRegistro.pac_codigo = Convert.ToString(pac.PAC_CODIGO);
                        frmQuirofanoRegistro.nom_paciente = pac.PAC_APELLIDO_PATERNO + ' ' + pac.PAC_APELLIDO_MATERNO + ' ' + pac.PAC_NOMBRE1 + ' ' + pac.PAC_NOMBRE2;
                        frmQuirofanoRegistro.Editar = true;
                        frmQuirofanoRegistro x = new frmQuirofanoRegistro(bodega);
                        x.ShowDialog();
                        reg = NegQuirofano.RegistroQuirofano(Convert.ToInt64(cie_codigo));
                    }
                    PROCEDIMIENTOS_CIRUGIA pro = new PROCEDIMIENTOS_CIRUGIA();
                    pro = NegQuirofano.Procedimiento(reg.cie_10);
                    if (pro is null)
                    {
                        TablaProcedimientos.DataSource = NegQuirofano.PacienteProcedimientoNew(1, Sesion.bodega);
                    }
                    else
                    {
                        TablaProcedimientos.DataSource = NegQuirofano.PacienteProcedimientoNew(Convert.ToInt32(pro.PCI_CODIGO), Sesion.bodega);
                    }
                    //TablaProcedimientos.DataSource = Quirofano.PacienteProcedimiento(cie_codigo, ate_codigo, bodega);

                }
                else
                {
                    TablaProcedimientos.DataSource = "";
                    TablaProcedimientos.DataSource = NegQuirofano.PacienteProcedimientoRecuperado(Convert.ToInt64(cie_codigo), Convert.ToInt64(ate_codigo));

                }
                TablaProcedimientos.Columns[0].ReadOnly = true;
                TablaProcedimientos.Columns[0].Width = 20;
                TablaProcedimientos.Columns[1].ReadOnly = true;
                TablaProcedimientos.Columns[1].Width = 40;
                TablaProcedimientos.Columns[2].ReadOnly = true;
                TablaProcedimientos.Columns[2].Width = 40;
                TablaProcedimientos.Columns[3].ReadOnly = true;
                TablaProcedimientos.Columns[3].Width = 20;
                TablaProcedimientos.Columns[4].ReadOnly = false;
                TablaProcedimientos.Columns[5].ReadOnly = true;
                TablaProcedimientos.Columns[5].Visible = false;
                TablaProcedimientos.Columns[6].ReadOnly = true;
                TablaProcedimientos.Columns[6].Visible = false;
                TablaProcedimientos.Columns[6].Width = 60;
                TablaProcedimientos.Columns[7].ReadOnly = true;
                TablaProcedimientos.Columns[8].Visible = false;
                TablaProcedimientos.Columns[9].ReadOnly = true;
                TablaProcedimientos.Columns[10].Visible = false;

                SumarCantidades();
                AlternarColores(TablaProcedimientos);
                AlertaStock();

                TablaCantidad = TablaProcedimientos.Rows.Count;

                if (TablaProcedimientos.Rows.Count > 0)
                {
                    foreach (DataGridViewRow r in TablaProcedimientos.Rows)
                    {
                        lblproductos.Text = (r.Index + 1).ToString();
                    }
                    if (obj.Count == 0)
                    {
                        GrabarBoton();
                    }
                }
                else
                {
                    lblproductos.Text = "0";
                }
            }
            //AutomatizarDevolucion();
        }
        public void AlertaStock()
        {
            foreach (DataGridViewRow item in TablaProcedimientos.Rows)
            {
                if (Convert.ToDouble(item.Cells["Stock"].Value) <= 0)
                {
                    TablaProcedimientos.Rows[item.Index].DefaultCellStyle.BackColor = Color.Salmon;
                }
                if (Convert.ToDouble(item.Cells["Cant. Original"].Value) > Convert.ToDouble(item.Cells["Stock"].Value))
                {
                    TablaProcedimientos.Rows[item.Index].DefaultCellStyle.BackColor = Color.Salmon;
                }
            }
        }
        public void AutomatizarDevolucion()
        {
            string adicional;
            foreach (DataGridViewRow item in TablaProcedimientos.Rows)
            {
                if (item.Cells[6].Value.ToString() == "0")
                {
                    adicional = item.Cells[5].Value.ToString();
                    if (adicional == "")
                    {
                        item.Cells[6].Value = item.Cells[4].Value.ToString();
                    }
                    else
                    {
                        if (Convert.ToInt32(item.Cells[4].Value.ToString()) >= Convert.ToInt32(item.Cells[5].Value.ToString()))
                        {
                            item.Cells[6].Value = Convert.ToInt32(item.Cells[4].Value.ToString()) - Convert.ToInt32(item.Cells[5].Value.ToString());
                        }
                        else
                        {
                            item.Cells[6].Value = 0;
                        }
                    }
                }
            }
        }
        public void Bloquear()
        {
            btnGuardar.Enabled = false;
            //TablaProcedimientos.Enabled = true;
            if (TablaProcedimientos.Rows.Count > 0)
                TablaProcedimientos.Columns[4].ReadOnly = true;
        }
        public void Desbloquear()
        {
            btnGuardar.Enabled = true;
            //TablaProcedimientos.Enabled = true;
            //TablaProcedimientos.Columns[4].ReadOnly = false;
        }
        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void RecuperarUsuario()
        {
            usuario = His.Entidades.Clases.Sesion.nomUsuario;
            id_usuario = Convert.ToString(His.Entidades.Clases.Sesion.codUsuario);
            frmquirofanopedidoadicional.id_usuario = Convert.ToString(His.Entidades.Clases.Sesion.codUsuario);
        }
        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            int contador = 0;
            int valor = Convert.ToInt32(NegQuirofano.Cantidad(ate_codigo, Convert.ToInt32(cie_codigo)));
            lblpaciente.Focus();
            lblcantoriginal.Focus();
            toolStripAgregar.Enabled = true;
            try
            {
                List<REPOSICION_QUIROFANO> obj = new List<REPOSICION_QUIROFANO>();
                obj = NegQuirofano.ReposicionQuirofano(Convert.ToInt64(cie_codigo), Convert.ToInt64(ate_codigo));
                if (obj.Count > 0)
                {
                    for (int i = 0; i < TablaProcedimientos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()) > 0)
                        {
                            NegQuirofano.QuirofanoActualizaProductos(TablaProcedimientos.Rows[i].Cells[0].Value.ToString(),
                                Convert.ToInt32(cie_codigo), Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()), Convert.ToInt64(ate_codigo));

                        }
                    }
                }
                else
                {
                    for (int i = 0; i < TablaProcedimientos.Rows.Count; i++)
                    {
                        var repo = new REPOSICION_QUIROFANO();


                        repo.RQ_CANTIDAD = Convert.ToInt16(TablaProcedimientos.Rows[i].Cells[4].Value.ToString());
                        repo.CODPRO = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                        repo.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                        repo.PCI_CODIGO = Convert.ToInt64(cie_codigo);
                        repo.RQ_FECHAPEDIDO = DateTime.Now;
                        repo.RQ_FECHACREACION = DateTime.Now;
                        repo.RQ_FECHAREPOSICION = DateTime.Now;
                        repo.ID_USUARIO = Sesion.codUsuario;
                        repo.RQ_CANTIDADADICIONAL = 0;
                        repo.RQ_CANTIDADDEVOLUCION = 0;

                        obj.Add(repo);

                    }
                    if (NegQuirofano.GuarddaReposicion(obj))
                    {
                        MessageBox.Show("Información Guardad con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        return;
                    }
                }
                MessageBox.Show("Cambios guardado correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnEditar.Enabled = true;
                Bloquear();
                CargarProcedimientosPaciente();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GrabarBoton()
        {
            int contador = 0;
            int valor = Convert.ToInt32(NegQuirofano.Cantidad(ate_codigo, Convert.ToInt32(cie_codigo)));
            lblpaciente.Focus();
            lblcantoriginal.Focus();
            toolStripAgregar.Enabled = true;
            try
            {
                List<REPOSICION_QUIROFANO> obj = new List<REPOSICION_QUIROFANO>();
                obj = NegQuirofano.ReposicionQuirofano(Convert.ToInt64(cie_codigo), Convert.ToInt64(ate_codigo));
                if (obj.Count > 0)
                {
                    for (int i = 0; i < TablaProcedimientos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()) > 0)
                        {
                            NegQuirofano.QuirofanoActualizaProductos(TablaProcedimientos.Rows[i].Cells[0].Value.ToString(),
                                Convert.ToInt32(cie_codigo), Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()), Convert.ToInt64(ate_codigo));

                        }
                    }
                }
                else
                {
                    if (ultimaAtencion.ATE_CODIGO != 0)
                    {
                        for (int i = 0; i < TablaProcedimientos.Rows.Count; i++)
                        {
                            var repo = new REPOSICION_QUIROFANO();


                            repo.RQ_CANTIDAD = Convert.ToInt16(TablaProcedimientos.Rows[i].Cells[4].Value.ToString());
                            repo.CODPRO = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                            repo.ATE_CODIGO = ultimaAtencion.ATE_CODIGO;
                            repo.PCI_CODIGO = Convert.ToInt64(cie_codigo);
                            repo.RQ_FECHAPEDIDO = DateTime.Now;
                            repo.RQ_FECHACREACION = DateTime.Now;
                            repo.RQ_FECHAREPOSICION = DateTime.Now;
                            repo.ID_USUARIO = Sesion.codUsuario;
                            repo.RQ_CANTIDADADICIONAL = 0;
                            repo.RQ_CANTIDADDEVOLUCION = 0;

                            obj.Add(repo);

                        }
                        if (NegQuirofano.GuarddaReposicion(obj))
                        {

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void GenerarTicket()
        {
            DatosImpresion DI = new DatosImpresion();
            DataRow drImpresion;
            DataTable TablaInfo = new DataTable();
            DataTable Tabla = new DataTable(); //Almacena los productos del pedido generado recientemente
            Tabla = Quirofano.ProductosPaciente(ate_codigo, numpedido);
            TablaInfo = Quirofano.RecuperarInfoPaciente(ate_codigo);

            foreach (DataRow item in Tabla.Rows)
            {
                drImpresion = DI.Tables["Pedido"].NewRow();
                if (bodega == NegParametros.ParametroBodegaQuirofano())
                    drImpresion["Departamento"] = "QUIROFANO";
                else
                    drImpresion["Departamento"] = "GASTROENTEROLOGIA";
                drImpresion["Atencion"] = TablaInfo.Rows[0][0].ToString();
                drImpresion["Hc"] = TablaInfo.Rows[0][1].ToString();
                drImpresion["Pedido"] = numpedido;
                drImpresion["Paciente"] = TablaInfo.Rows[0][2].ToString();
                drImpresion["Fecha"] = TablaInfo.Rows[0][3].ToString();
                drImpresion["Medico"] = TablaInfo.Rows[0][4].ToString();
                drImpresion["Usuario"] = TablaInfo.Rows[0][5].ToString();
                drImpresion["Habitacion"] = TablaInfo.Rows[0][6].ToString();
                drImpresion["Cantidad"] = item[0].ToString();
                drImpresion["Codigo"] = item[1].ToString();
                drImpresion["Descripcion"] = item[2].ToString();
                DI.Tables["Pedido"].Rows.Add(drImpresion);
            }

            frmReportes Reporte = new frmReportes(1, "PedidoQuirofano", DI);
            Reporte.Show();
            MessageBox.Show("Pedido Nro: " + numpedido + " Generado Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void LimpiarVariables()
        {
            codpro = null;
            cant_usada = null;
            cantidad = null;
            orden = null;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            Desbloquear();
            btnEditar.Enabled = false;
            checkBox1.Enabled = true;
            //toolStripAgregar.Enabled = false;
        }

        private void TablaProcedimientos_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                NegUtilitarios.OnlyNumberDecimal(e, false);
            }
            catch (Exception)
            {
                MessageBox.Show("¡Debe ingresar solo Números!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
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

        private void btn7_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "7";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btnenter_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled != false)
            {
                if (fila <= TablaCantidad)
                {
                    TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
                    pantalla = null;
                    fila += 1;
                    btnborrar.Enabled = true;
                }
            }

        }

        private void TablaProcedimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fila = TablaProcedimientos.CurrentRow.Index;
            valorcelda = TablaProcedimientos.Rows[fila].Cells[5].Value.ToString();
            btnborrar.Enabled = true;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "0";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "8";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "9";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "4";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "5";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "6";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "1";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "2";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "3";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled != false)
            {
                int lon_pantalla = pantalla.Length;
                try
                {
                    pantalla = pantalla.Remove(lon_pantalla - 1); lon_pantalla -= 1;
                    if (lon_pantalla == 0)
                    {
                        TablaProcedimientos.Rows[fila].Cells[5].Value = 0;
                        btnborrar.Enabled = false;
                    }
                    else
                    {
                        TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void toolStripDespachar_Click(object sender, EventArgs e)
        {
            lblcantoriginal.Focus();
            bool error = false; //Verifica si existe stock disponible para pedido adicional, si es falso hace el pedido caso contrario no lo hara.
            for (int i = 0; i < TablaProcedimientos.Rows.Count; i++)
            {
                if (Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[6].Value.ToString()) <=
                    Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[3].Value.ToString()))
                {
                    error = false;
                }
                else
                {
                    error = true;
                    MessageBox.Show("¡No Hay Suficiente Stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }
            }
            if (error == false) //if (PedidoAdicional == true && error == false)
            {
                if (MessageBox.Show("¿Está seguro de Despachar los productos?", "Warning", MessageBoxButtons.YesNo
                , MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < TablaProcedimientos.RowCount; i++)
                        {
                            double costo, total;

                            codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                            cant_usada = TablaProcedimientos.Rows[i].Cells[6].Value.ToString();
                            Quirofano.PedidoAdicional(ate_codigo, pac_codigo, cie_codigo, cant_usada, codpro); //Aqui graba cuando ya se decida hacer el pedido adicional
                                                                                                               //Es decir que cuando se haga un pedido adicional este solo debera enviar los productos que sean mayor a cero a farmacia, como referencia que diga que es pedido adicional

                            //PEDIDO ADICION -- CUENTA PACIENTE

                            PedidosDetalleItem = new PEDIDOS_DETALLE();
                            codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                            DataTable TablaP = NegProducto.RecuperarProductoSic(codpro);
                            pro_descripcion = TablaProcedimientos.Rows[i].Cells[1].Value.ToString();
                            cantidad = TablaProcedimientos.Rows[i].Cells[4].Value.ToString();
                            orden = TablaProcedimientos.Rows[i].Cells[8].Value.ToString();
                            costo = Convert.ToDouble(TablaProcedimientos.Rows[i].Cells[10].Value.ToString());
                            total = Math.Round((Convert.ToInt32(cantidad) * costo), 2);

                            //Cambios Edgar 20210601

                            PedidosDetalleItem.PDD_CODIGO = 1;
                            PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                            PedidosDetalleItem.PDD_CANTIDAD = Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[4].Value.ToString());
                            PedidosDetalleItem.PRO_DESCRIPCION = TablaProcedimientos.Rows[i].Cells[1].Value.ToString();
                            PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[10].Value.ToString());
                            PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[10].Value.ToString()) * Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                            PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[10].Value.ToString()) * Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[4].Value.ToString())), 2) + Math.Round(((((Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[10].Value.ToString()) * Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                            PedidosDetalleItem.PDD_ESTADO = true;
                            PedidosDetalleItem.PDD_COSTO = 0;
                            PedidosDetalleItem.PDD_FACTURA = null;
                            PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                            PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                            PedidosDetalleItem.PDD_RESULTADO = null;
                            PedidosDetalleItem.PRO_CODIGO_BARRAS = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();

                            PedidosDetalle.Add(PedidosDetalleItem);

                        }
                        if (GuardarPedidoQuirofano())
                        {
                            GenerarTicket();
                            MessageBox.Show("El despacho se realizo con éxito.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Bloquear();
                            CargarProcedimientosPaciente();
                            btnEditar.Enabled = true;
                            ValidarAdicional();
                            PedidoAdicional = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void cmbProcedimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //toolStripRegistro.Visible = true;
            //cmbProcedimiento.Focus();
            //if (cmbProcedimiento.SelectedValue.ToString() != "System.Data.DataRowView")
            //{
            //    cie_codigo = cmbProcedimiento.SelectedValue.ToString();
            //    CargarProcedimientosPaciente();
            //    MostrarRegistro();
            //    //Tabla que verifica si tiene valores dentro del registro
            //    DataTable Tabla = new DataTable();
            //    Tabla = Quirofano.RegistroPaciente(ate_codigo, datosPaciente.PAC_CODIGO.ToString(), cie_codigo);
            //    foreach (DataRow item in Tabla.Rows)
            //    {
            //        if (item[0].ToString() == "")
            //        {
            //            toolStripRegistro.Visible = false;
            //            groupBox1.Visible = false;
            //        }
            //        else
            //        {
            //            toolStripRegistro.Visible = true;
            //            groupBox1.Visible = true;
            //        }
            //    }
            //    GuardarCantidades(); //Si no tiene cantidades del procedimiento con paciente se lo ingresera.
            //    ValidarProcedimientoCerrado();
            //}

        }
        //funcion que almacena las cantidades de la tabla si dentro de la base de datos no existe
        public PEDIDOS_DETALLE PedidosDetalleItem = null;
        PRODUCTO Prod = new PRODUCTO();
        public List<PEDIDOS_DETALLE> PedidosDetalle = new List<PEDIDOS_DETALLE>();
        public void GuardarCantidades()
        {
            //DateTime fecha = DateTime.Now;
            //double costo, total;
            //int contador = 0;
            //int valor = Convert.ToInt32(NegQuirofano.Cantidad(ate_codigo, Convert.ToInt32(cie_codigo)));
            //try
            //{
            //    //Verifico si tiene los productos del procedimiento
            //    if (TablaProcedimientos.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < TablaProcedimientos.RowCount; i++)
            //        {
            //            contador += 1;
            //        }
            //        if (contador != valor)
            //        {

            //            //Quirofano.AgregarPedidoPaciente(fecha.ToString(), id_usuario, ate_codigo, hab_codigo);
            //            //numpedido = Quirofano.RecuperarPedidoNum(ate_codigo); //Recupera el ultimo numero de pedido de acuerdo con la atencion
            //            foreach (DataGridViewRow item in TablaProcedimientos.Rows)
            //            {
            //                PedidosDetalleItem = new PEDIDOS_DETALLE();
            //                codpro = item.Cells[0].Value.ToString();
            //                DataTable TablaP = NegProducto.RecuperarProductoSic(codpro);
            //                pro_descripcion = item.Cells[1].Value.ToString();
            //                cantidad = item.Cells[4].Value.ToString();
            //                orden = item.Cells[8].Value.ToString();
            //                costo = Convert.ToDouble(item.Cells[10].Value.ToString());
            //                total = Math.Round((Convert.ToInt32(cantidad) * costo), 2);


            //                Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codpro, cantidad, pac_codigo, ate_codigo, "0", usuario, 0);
            //                //Quirofano.PedidoDetalle(codpro, pro_descripcion, cantidad, valor.ToString(), total.ToString(), numpedido);

            //                //Cambios Edgar 20210601

            //                PedidosDetalleItem.PDD_CODIGO = 1;
            //                PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
            //                PedidosDetalleItem.PDD_CANTIDAD = Convert.ToInt32(item.Cells[4].Value.ToString());
            //                PedidosDetalleItem.PRO_DESCRIPCION = item.Cells[1].Value.ToString();
            //                PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(item.Cells[10].Value.ToString());
            //                PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(item.Cells[10].Value.ToString()) * Convert.ToDecimal(item.Cells[4].Value.ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
            //                PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(item.Cells[10].Value.ToString()) * Convert.ToDecimal(item.Cells[4].Value.ToString())), 2) + Math.Round(((((Convert.ToDecimal(item.Cells[10].Value.ToString()) * Convert.ToDecimal(item.Cells[4].Value.ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
            //                PedidosDetalleItem.PDD_ESTADO = true;
            //                PedidosDetalleItem.PDD_COSTO = 0;
            //                PedidosDetalleItem.PDD_FACTURA = null;
            //                PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
            //                PedidosDetalleItem.PDD_FECHA_FACTURA = null;
            //                PedidosDetalleItem.PDD_RESULTADO = null;
            //                PedidosDetalleItem.PRO_CODIGO_BARRAS = item.Cells[0].Value.ToString();

            //                PedidosDetalle.Add(PedidosDetalleItem);
            //            }
            //            //SE COMENTA LA AGREGACION DE PEDIDO E IMPRESION DE TICKET
            //            //GuardarPedidoQuirofano();
            //            //GenerarTicket();
            //            CargarProcedimientosPaciente();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Algo ocurrio al ingresar productos del procedimiento al paciente\r\nMás Informacion: " + ex.Message, "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        public void ValidarProcedimientoCerrado()
        {
            string estado;
            try
            {
                estado = Quirofano.ProcedimientoCerrado(ate_codigo, pac_codigo, cie_codigo);
                if (estado == "0")
                {
                    Bloquear();
                    btnadicional.Enabled = true;
                    toolStripAgregar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEditar.Enabled = false;
                    toolStripRegistro.Enabled = true;
                    toolStripDespachar.Enabled = false;
                    toolStripCerrar.Enabled = false;
                    btncancelar.Enabled = false;
                    btnDevolucion.Enabled = false;
                    btnadicional.Visible = true; //AQUI REALIZAN PRODUCTOS EXTRAS CUANDO SE HAYA CERRADO EL PROCEDIMIENTO.
                    TablaProcedimientos.ReadOnly = true;
                }
                if (estado == "1")
                {
                    //btnadicional.Enabled = true;
                    toolStripAgregar.Enabled = true;
                    btnGuardar.Enabled = true;
                    btnEditar.Enabled = true;
                    toolStripRegistro.Enabled = true;
                    toolStripDespachar.Enabled = true;
                    toolStripCerrar.Enabled = true;
                    btncancelar.Enabled = true;
                    btnDevolucion.Enabled = true;
                    TablaProcedimientos.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error con Validar Estado de Procedimiento.\r\nMás detalle: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        public void MostrarRegistro()
        {
            DataTable TablaRegistro = new DataTable();
            TablaRegistro = Quirofano.RegistroPaciente(ate_codigo, datosPaciente.PAC_CODIGO.ToString(), cie_codigo);
            foreach (DataRow item in TablaRegistro.Rows)
            {
                lblcirujano.Text = Convert.ToString(item[1]); //Nombre de Cirujano
                lblayudante.Text = Convert.ToString(item[2]); //Nombre de Ayudante
                lblayudantia.Text = Convert.ToString(item[3]); //nombre de ayudantia
                lbltipoanestesia.Text = item[16].ToString(); //Tipo de Anestesia
                lblanestesiologo.Text = Convert.ToString(item[6]); //Nombre de Anestesiologo
                lblhi.Text = Convert.ToString(item[7]); //Hora de Inicio
                lblcirculante.Text = Convert.ToString(item[8]); // Nombre de Circulante
                lblinstrumentista.Text = Convert.ToString(item[9]); //Nombre de Instrumentista
                lblpatologo.Text = Convert.ToString(item[10]); //Nombre de Patologo
                lblatencion.Text = Convert.ToString(item[11]); //Tipo de atencion
                lblfecha.Text = item[13].ToString(); //Fecha
                lblhf.Text = item[14].ToString(); //hora final
                lblduracion.Text = Convert.ToDateTime(item[15].ToString()).ToLongTimeString(); // duracion de procedimiento
                frmquirofanopedidoadicional.hab_codigo = item[0].ToString();
                hab_codigo = item[0].ToString();
            }
        }
        public void ValidarAdicional()
        {
            foreach (DataGridViewRow item in TablaProcedimientos.Rows)
            {
                if (item.Cells[6].Value.ToString() == "" && item.Cells[5].Value.ToString() != "")
                {
                    btnadicional.Visible = true; //Escondemos el boton, porque si existe valores dentro de adicionales, de acuerdo al ing, solo se pide una vez
                }
                else
                {
                    btnadicional.Visible = false;
                    //Aqui va el codigo que restara a stock desde el adicional, controlando claro que el adicional sea superior al stock
                }
            }
        }

        private void btnadicional_Click(object sender, EventArgs e)
        {
            ATENCIONES nuevaConsulta = new ATENCIONES();
            nuevaConsulta = NegAtenciones.RecuperarAtencionID(ultimaAtencion.ATE_CODIGO);
            if (nuevaConsulta.ESC_CODIGO != 1)
            {
                MessageBox.Show("Paciente ha sido dado de alta.\r\nComuniquese con caja.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //TablaProcedimientos.Enabled = true;
            if (TablaProcedimientos.Rows.Count > 0)
            {
                frmquirofanopedidoadicional.ate_codigo = ate_codigo;
                frmquirofanopedidoadicional.pac_codigo = pac_codigo;
                frmquirofanopedidoadicional.cie_codigo = cie_codigo;
                frmquirofanopedidoadicional x = new frmquirofanopedidoadicional(bodega);
                x.ShowDialog();
                CargarProcedimientosPaciente();
            }
            else
            {
                MessageBox.Show("No Tiene Productos, Vuelva a Elegir el/otro Procedimieto.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void X_FormClosed1(object sender, FormClosedEventArgs e)
        {
            //Aqui se aplicara el cambio dentro de la columna pedido adicional, es decir aqui sumara lo que reciba del formulario pedido adicional

            CargarProcedimientosPaciente();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow item in TablaProcedimientos.Rows)
                {
                    item.Cells[5].Value = item.Cells[4].Value.ToString();
                }
                checkBox1.Enabled = false;
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            checkBox1.Checked = false;
            TablaProcedimientos.DataSource = null;
            Bloquear();
        }

        private void toolStripRegistro_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Rows.Count > 0)
            {
                //frmQuirofanoRegistro.cie_codigo = cmbProcedimiento.SelectedValue.ToString();
                //frmQuirofanoRegistro.cie_descripcion = cmbProcedimiento.GetItemText(cmbProcedimiento.SelectedItem);
                frmQuirofanoRegistro.ate_codigo = ate_codigo;
                frmQuirofanoRegistro.pac_codigo = pac_codigo;
                frmQuirofanoRegistro.nom_paciente = lblpaciente.Text;
                frmQuirofanoRegistro.Editar = true;
                frmQuirofanoRegistro x = new frmQuirofanoRegistro(bodega);
                x.ShowDialog();
                MostrarRegistro();
                CargarProcedimientoPaciente();
            }
            else
            {
                MessageBox.Show("Debe Actualizar primero el procedimiento.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripAgregar_Click(object sender, EventArgs e)
        {
            ///NUEVO FORMULARIO DE PEDIDO DE PRODUCTOS
            if (toolStripCerrar.Enabled == true)
            {
                frm_QuirofanoAgregarVarios x = new frm_QuirofanoAgregarVarios(bodega);
                x.ate_codigo = ate_codigo;
                x.pac_codigo = pac_codigo;
                x.pci_codigo = cie_codigo;
                x.ShowDialog();
                RefreshTablaProcedimiento();
                SumarCantidades();
                //CargarProcedimientosPaciente();
            }
        }

        public void CargarProcedimientoActual()
        {
            AlternarColores(TablaProcedimientos);
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (codigo_producto != null && orden != null && cant != null)
            {
                try
                {
                    PedidosDetalle = new List<PEDIDOS_DETALLE>();
                    DataTable TablaProcedimientos = Quirofano.PacienteProcedimiento(cie_codigo, ate_codigo, bodega);
                    bool existe = false;

                    foreach (DataRow item in TablaProcedimientos.Rows)
                    {
                        if (item["Codigo"].ToString() == codigo_producto)
                        {
                            MessageBox.Show("El producto ya está agregado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            existe = true;
                            break;
                        }
                    }
                    if (!existe)
                    {
                        //Cambios 20210607
                        DateTime fecha = DateTime.Now;
                        DataTable TablaP = NegProducto.RecuperarProductoSic(codigo_producto);

                        //Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codpro, cantidad, pac_codigo, ate_codigo, "0", usuario);
                        //Quirofano.PedidoDetalle(codpro, pro_descripcion, cantidad, valor.ToString(), total.ToString(), numpedido);

                        //Cambios Edgar 20210601
                        string xIVA = "1." + TablaP.Rows[0]["iva"].ToString();
                        double valorsinIVA = Convert.ToInt32(TablaP.Rows[0]["despro"].ToString()) / Convert.ToDouble(xIVA);
                        PedidosDetalleItem = new PEDIDOS_DETALLE();
                        PedidosDetalleItem.PDD_CODIGO = 1;
                        PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                        PedidosDetalleItem.PDD_CANTIDAD = Convert.ToInt32(cant);
                        PedidosDetalleItem.PRO_DESCRIPCION = TablaP.Rows[0]["despro"].ToString();
                        PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(valorsinIVA);
                        PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(cant))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                        PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(cant)), 2) + Math.Round(((((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(cant))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                        PedidosDetalleItem.PDD_ESTADO = true;
                        PedidosDetalleItem.PDD_COSTO = 0;
                        PedidosDetalleItem.PDD_FACTURA = null;
                        PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                        PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                        PedidosDetalleItem.PDD_RESULTADO = null;
                        PedidosDetalleItem.PRO_CODIGO_BARRAS = TablaP.Rows[0]["Codigo"].ToString();

                        PedidosDetalle.Add(PedidosDetalleItem);
                        Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codigo_producto, cant, pac_codigo, ate_codigo, null, Sesion.nomUsuario, 0);
                        MessageBox.Show("El Producto ha sido Agregado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //SE COMENTA PARA QUE EL PEDIDO NO SE AGREGE
                        //GuardarPedidoQuirofano();
                        //GenerarTicket();

                    }
                    codigo_producto = null;
                    orden = null;
                    cant = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void toolStripCerrar_Click(object sender, EventArgs e)
        {
            ATENCIONES nuevaConsulta = new ATENCIONES();
            nuevaConsulta = NegAtenciones.RecuperarAtencionID(ultimaAtencion.ATE_CODIGO);
            //CargarProcedimientosPaciente();
            if (nuevaConsulta.ESC_CODIGO != 1)
            {
                MessageBox.Show("Paciente ha sido dado de alta.\r\nComuniquese con caja.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("¿Esta Seguro de Cerrar Procedimiento? \n Una Vez Cerrado no se Volvera Abrir. \n ¿Desea Continuar?",
                "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                try
                {
                    lblcantoriginal.Focus();
                    DataTable TablaPro = new DataTable();
                    //TablaPro = Quirofano.PacienteProcedimiento(cie_codigo, ate_codigo, bodega);
                    //TablaPro = NegQuirofano.PacienteProcedimientoNew(1, Sesion.bodega);
                    bool error = false; //Verifica si existe stock disponible para pedido adicional, si es falso hace el pedido caso contrario no lo hara.
                                        //foreach (DataRow item in TablaProcedimientos.Rows)
                    for (int i = 0; i < TablaProcedimientos.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(TablaProcedimientos.Rows[i].Cells[3].Value.ToString()) >= Convert.ToDouble(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()))
                        {
                            error = false;
                        }
                        else
                        {
                            error = true;
                            MessageBox.Show("¡No Hay Suficiente Stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        }
                    }
                    if (error == false) //if (PedidoAdicional == true && error == false)
                    {
                        try
                        {
                            PedidosDetalle = new List<PEDIDOS_DETALLE>();
                            for (int i = 0; i < TablaProcedimientos.Rows.Count; i++)
                            {
                                double costo, total;

                                codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();// item[0].ToString();
                                cant_usada = TablaProcedimientos.Rows[i].Cells[4].Value.ToString();// item[4].ToString();
                                Quirofano.PedidoAdicional(ate_codigo, pac_codigo, cie_codigo, cant_usada, codpro); //Aqui graba cuando ya se decida hacer el pedido adicional

                                //PEDIDO ADICION -- CUENTA PACIENTE
                                if (Convert.ToDouble(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()) > 0) //if (Convert.ToDouble(item[4].ToString()) > 0)
                                {
                                    PedidosDetalleItem = new PEDIDOS_DETALLE();
                                    codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();// item[0].ToString();
                                    DataTable TablaP = NegProducto.RecuperarProductoSic(codpro);
                                    Prod = NegProducto.RecuperarProductoID(Convert.ToInt32(codpro));
                                    pro_descripcion = TablaProcedimientos.Rows[i].Cells[1].Value.ToString();// item[1].ToString();
                                    cantidad = TablaProcedimientos.Rows[i].Cells[4].Value.ToString();// item[4].ToString();
                                    orden = TablaProcedimientos.Rows[i].Cells[8].Value.ToString();// item[8].ToString();
                                    costo = Convert.ToDouble(TablaProcedimientos.Rows[i].Cells[10].Value.ToString());//Convert.ToDouble(item[10].ToString());
                                    total = Math.Round((Convert.ToDouble(cantidad) * costo), 2);



                                    //Cambios Edgar 20210601
                                    string xIVA = "1." + TablaP.Rows[0]["iva"].ToString(); //SE REGRESA EL VALOR UNITARIO DEL PRECIO DE VENTA
                                    double valorsinIVA = Math.Round(Convert.ToDouble(TablaProcedimientos.Rows[i].Cells[10].Value.ToString()) / Convert.ToDouble(xIVA), 2);//Math.Round(Convert.ToDouble(item[10].ToString()) / Convert.ToDouble(xIVA), 2);

                                    PedidosDetalleItem.PDD_CODIGO = 1;
                                    PedidosDetalleItem.PRODUCTOReference.EntityKey = Prod.EntityKey;
                                    PedidosDetalleItem.PDD_CANTIDAD = Math.Round(Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()), 2);//Math.Round(Convert.ToDecimal(item[4].ToString()), 2);
                                    PedidosDetalleItem.PRO_DESCRIPCION = TablaProcedimientos.Rows[i].Cells[1].Value.ToString();// item[1].ToString();
                                    PedidosDetalleItem.PDD_VALOR = Convert.ToDecimal(valorsinIVA);
                                    PedidosDetalleItem.PDD_IVA = Math.Round(((((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 4);//Math.Round(((((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(item[4].ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 4);
                                    PedidosDetalleItem.PDD_TOTAL = Math.Round((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[4].Value.ToString())), 2) + Math.Round(((((Convert.ToDecimal(valorsinIVA) * Convert.ToDecimal(TablaProcedimientos.Rows[i].Cells[4].Value.ToString()))) * (Convert.ToDecimal(TablaP.Rows[0]["iva"].ToString()))) / 100), 2);
                                    PedidosDetalleItem.PDD_ESTADO = true;
                                    PedidosDetalleItem.PDD_COSTO = 0;
                                    PedidosDetalleItem.PDD_FACTURA = null;
                                    PedidosDetalleItem.PDD_ESTADO_FACTURA = null;
                                    PedidosDetalleItem.PDD_FECHA_FACTURA = null;
                                    PedidosDetalleItem.PDD_RESULTADO = null;
                                    PedidosDetalleItem.PRO_CODIGO_BARRAS = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();//item[0].ToString();
                                    PedidosDetalleItem.PRO_BODEGA_SIC = bodega;

                                    PedidosDetalle.Add(PedidosDetalleItem);
                                }

                            }
                            if (GuardarPedidoQuirofano())
                            {
                                GenerarTicket();
                                //MessageBox.Show("El despacho se realizo con éxito.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Bloquear();
                                CargarProcedimientosPaciente();
                                btnEditar.Enabled = true;
                                ValidarAdicional();
                                PedidoAdicional = false;

                                Quirofano.CerrarProcedimiento(ate_codigo, pac_codigo, cie_codigo);
                                MessageBox.Show("Procedimiento Cerrado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ValidarProcedimientoCerrado();
                                this.Close();
                            }
                            else
                            {
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Algo Ocurrio al Cerrar el Procedimiento.\r\nMás detalle: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void TablaProcedimientos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
        }
        PACIENTES paciente = new PACIENTES();
        public bool GuardarPedidoQuirofano()
        {
            try
            {
                Int32 Pedido = 0;
                paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
                var archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
                byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
                PEDIDOS_AREAS p_a = NegPedidos.recuperarAreaPorID(100);
                ATENCIONES ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
                QUIROFANO_PROCE_PRODU reg = NegQuirofano.habitacionProcedimiento(ultimaAtencion.ATE_CODIGO, Convert.ToInt64(cie_codigo));
                int codigoArea = 100;
                if (PedidosDetalle.Count() > 0)
                {
                    string descripcion = "PEDIDO GENERADO DESDE ";
                    if (bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
                        descripcion = descripcion + "QUIROFANO.";
                    else
                        descripcion = descripcion + "GASTROENTEROLOGIA.";
                    var pedido = new PEDIDOS
                    {
                        PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1,
                        PED_FECHA = DateTime.Now /*PARA GUARDAR EL PEDIDO SE NECESITA FECHA Y HORA/ GIOVANNY TAPIA / 12/04/2013*/,
                        PED_DESCRIPCION = descripcion,
                        PED_ESTADO = FarmaciaPAR.PedidoPendiente,
                        ID_USUARIO = Sesion.codUsuario,
                        ATE_CODIGO = ultimaAtencion.ATE_CODIGO,
                        PEE_CODIGO = codigoEstacion,
                        TIP_PEDIDO = FarmaciaPAR.PedidoMedicamentos,
                        PED_PRIORIDAD = 1,
                        MED_CODIGO = 0,
                        PEDIDOS_AREASReference = { EntityKey = p_a.EntityKey },
                        HAB_CODIGO = ultimaAtencion.HABITACIONES.hab_Codigo,
                        PED_TRANSACCION = 1,
                        IP_MAQUINA = Sesion.ip
                    };

                    Pedido = pedido.PED_CODIGO;
                    numpedido = Pedido.ToString();
                    //NegPedidos.crearPedido(pedido);

                    Int32 xcodDiv = 0;
                    Int16 XRubro = 0;
                    DataTable auxDT = new DataTable();

                    List<PEDIDOS_DETALLE> pdetalle = new List<PEDIDOS_DETALLE>();
                    List<CUENTAS_PACIENTES> cuentaPaciente = new List<CUENTAS_PACIENTES>();
                    List<REPOSICION_QUIROFANO> reposicion = new List<REPOSICION_QUIROFANO>();
                    DateTime Hoy = DateTime.Now;

                    #region codigo anterior
                    //foreach (var solicitud in PedidosDetalle)
                    //{
                    //    Int32 codpro = Convert.ToInt32(solicitud.PRO_CODIGO_BARRAS.ToString());
                    //    if (codigoArea != 1)
                    //    {
                    //        solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                    //        solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                    //        auxDT = NegFactura.recuperaCodRubro(codpro);
                    //        foreach (DataRow row in auxDT.Rows)
                    //        {
                    //            XRubro = Convert.ToInt16(row[0].ToString());
                    //            xcodDiv = Convert.ToInt32(row[1].ToString());
                    //        }

                    //        #region AntiguoPedido

                    //        //Valida duplicados
                    //        //NegPedidos.crearDetallePedidoQuirofano(solicitud, pedido, XRubro, xcodDiv, descripcion);
                    //        //Quirofano.ProductoBodega(codpro.ToString(), solicitud.PDD_CANTIDAD.ToString(), bodega);
                    //        //DataTable TablaP = NegProducto.RecuperarProductoSic(solicitud.PRO_CODIGO_BARRAS);
                    //        ////ACTUALIZA DENTRO DEL KARDEX EL NUMERO DE LA BODEGA
                    //        //NegQuirofano.ActualizarKardexSic(pedido.PED_CODIGO.ToString(), bodega);

                    //        ////CREA DATOS PARA LA TABLA DE REPOSICION
                    //        //NegQuirofano.DatosReposicion(Convert.ToInt64(ate_codigo), Convert.ToInt32(cie_codigo),
                    //        //    Convert.ToInt32(solicitud.PDD_CANTIDAD), Convert.ToDateTime(TablaProcedimientos.Rows[0].Cells["Fecha"].Value.ToString()),
                    //        //    pedido.PED_CODIGO, solicitud.PRO_CODIGO_BARRAS, Sesion.codUsuario);

                    //        #endregion

                    //        #region NuevoPedido

                    //        //pdetalle.Add(solicitud);

                    //        //#region Cuenta Paciente

                    //        //CUENTAS_PACIENTES cp = new CUENTAS_PACIENTES();
                    //        //cp.ATE_CODIGO = pedido.ATE_CODIGO;
                    //        //cp.PRO_CODIGO = Convert.ToString(solicitud.PRO_CODIGO_BARRAS);
                    //        //cp.CUE_ESTADO = 1;
                    //        //cp.CUE_FECHA = (DateTime)pedido.PED_FECHA;
                    //        //cp.CUE_VALOR_UNITARIO = solicitud.PDD_VALOR;
                    //        //cp.CUE_IVA = solicitud.PDD_IVA;
                    //        //cp.CUE_VALOR = solicitud.PDD_VALOR * solicitud.PDD_CANTIDAD;
                    //        //cp.ID_USUARIO = pedido.ID_USUARIO;
                    //        //cp.PED_CODIGO = xcodDiv;
                    //        //cp.RUB_CODIGO = XRubro;
                    //        //cp.CAT_CODIGO = 0;
                    //        //cp.CUE_CANTIDAD = Convert.ToDecimal(solicitud.PDD_CANTIDAD);
                    //        //cp.CUE_DETALLE = solicitud.PRO_DESCRIPCION;
                    //        //cp.CUE_NUM_FAC = "0";
                    //        //cp.PRO_CODIGO_BARRAS = solicitud.PRO_CODIGO_BARRAS;
                    //        //cp.MED_CODIGO = pedido.MED_CODIGO;
                    //        //cp.Codigo_Pedido = pedido.PED_CODIGO;
                    //        //cp.DivideFactura = "N";
                    //        //cp.FECHA_FACTURA = (DateTime)pedido.PED_FECHA;
                    //        //cp.CUE_OBSERVACION = "PEDIDO GENERADO POR QUIROFANO";

                    //        //cuentaPaciente.Add(cp);

                    //        //#endregion

                    //        //#region Reposicion
                    //        //REPOSICION_QUIROFANO rq = new REPOSICION_QUIROFANO();
                    //        //rq.ATE_CODIGO = pedido.ATE_CODIGO;
                    //        //rq.PCI_CODIGO = Convert.ToInt64(cie_codigo);
                    //        //rq.RQ_CANTIDAD = (int)solicitud.PDD_CANTIDAD;
                    //        //rq.RQ_CANTIDADADICIONAL = 0;
                    //        //rq.RQ_CANTIDADDEVOLUCION = 0;
                    //        //rq.RQ_FECHACREACION = Convert.ToDateTime(TablaProcedimientos.Rows[0].Cells["Fecha"].Value.ToString());
                    //        //rq.PED_CODIGO = pedido.PED_CODIGO;
                    //        //rq.CODPRO = solicitud.PRO_CODIGO_BARRAS;
                    //        //rq.ID_USUARIO = Sesion.codUsuario;

                    //        //reposicion.Add(rq);
                    //        //#endregion


                    //        #endregion

                    //    }
                    //    else
                    //    {
                    //        string CodigoProducto = "";
                    //        decimal ValorIva = 0;
                    //        solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
                    //        solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                    //        CodigoProducto = Convert.ToString(solicitud.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value).Substring(0, 1);
                    //        ValorIva = Convert.ToDecimal(solicitud.PDD_IVA);
                    //        auxDT = NegFactura.recuperaCodRubro(codpro);
                    //        foreach (DataRow row in auxDT.Rows)
                    //        {
                    //            XRubro = Convert.ToInt16(row[0].ToString());
                    //            xcodDiv = Convert.ToInt32(row[1].ToString());
                    //        }
                    //        NegPedidos.crearDetallePedidoQuirofano(solicitud, pedido, XRubro, xcodDiv, descripcion);
                    //        Quirofano.ProductoBodega(CodigoProducto, solicitud.PDD_CANTIDAD.ToString(), bodega);

                    //    }
                    //}
                    #endregion
                    if (!NegPedidos.nuevoPedidoProcedimiento(pedido, PedidosDetalle, bodega, Convert.ToInt32(cie_codigo), descripcion))
                    {

                        MessageBox.Show("Pedido no generado. Intente nuevamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    //CREO EL DESPACHO AUTOMATICO PARA QUIROFANO
                    List<DtoDespachos> despachos = new List<DtoDespachos>();
                    DtoDespachos xdespacho = new DtoDespachos();
                    if (bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
                        xdespacho.observacion = "DESPACHADO DESDE QUIROFANO";
                    else
                        xdespacho.observacion = "DESPACHADO DESDE GASTROENTEROLOGIA";
                    xdespacho.ped_codigo = Convert.ToInt64(numpedido);

                    despachos.Add(xdespacho);

                    if (!NegPedidos.InsertarDespachos(despachos, 0, DateTime.Now))
                    {
                        MessageBox.Show("No se pudo realizar despacho automatico." +
                            "\r\nIntentelo manual desde el modulo de pedidos - Control despachos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return true;

                    #region Nuevo 
                    ////NUEVO PEDIDO CON TRANSACTION
                    //if (NegPedidos.PedidoQuirofano(pedido , cuentaPaciente, reposicion, pdetalle, bodega))
                    //{
                    //    //CREO EL DESPACHO AUTOMATICO PARA QUIROFANO
                    //    List<DtoDespachos> despachos = new List<DtoDespachos>();
                    //    DtoDespachos xdespacho = new DtoDespachos();
                    //    xdespacho.observacion = "DESPACHADO DESDE QUIROFANO";
                    //    xdespacho.ped_codigo = Convert.ToInt64(numpedido);

                    //    despachos.Add(xdespacho);

                    //    if (!NegPedidos.InsertarDespachos(despachos, 0, DateTime.Now))
                    //    {
                    //        MessageBox.Show("No se pudo realizar despacho automatico." +
                    //            "\r\nIntentelo manual desde el modulo de pedidos - Control despachos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    }
                    //    return true;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("No se pudo crear el pedido. Intente nuevamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return false;
                    //}
                    #endregion
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            PACIENTES datospaciente = new PACIENTES();
            ATENCIONES atencion = new ATENCIONES();
            datospaciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));

            atencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));

            frm_ListaPedidosQuirofano x = new frm_ListaPedidosQuirofano();
            x.ate_codigo = Convert.ToInt32(ate_codigo);
            x.ShowDialog();

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
                        if (Sesion.codDepartamento == 14 || Sesion.codDepartamento == 1)
                        {
                            validador = true;
                        }
                    }
                    else
                        validador = true;

                }
                if (validador)
                {
                    Int32 DevolucionNumero = 0;
                    var codigoArea = 100;
                    var codigoArea1 = 100;
                    //DataTable devolviendo = new DataTable();
                    //devolviendo = NegHabitaciones.Devolviendo(Convert.ToInt64(Item.PED_CODIGO.ToString()));
                    if (x.ped_codigo != null)
                    {
                        var solicitudMedicamentos = new frmDevolucionPedido(codigoArea1, lblpaciente.Text, lblmedico.Text, datospaciente.PAC_HISTORIA_CLINICA, atencion.ATE_NUMERO_ATENCION, x.ped_codigo, Convert.ToInt64(ate_codigo));
                        solicitudMedicamentos.ShowDialog();

                        DevolucionNumero = solicitudMedicamentos.DevolucionNumero;

                        if (DevolucionNumero != 0)
                        {

                            HabitacionesUI.frmImpresionPedidos frmPedidos = new HabitacionesUI.frmImpresionPedidos(DevolucionNumero, codigoArea1, 1, 0);
                            frmPedidos.ShowDialog();
                            //ACTUALIZA DENTRO DEL KARDEX EL NUMERO DE LA BODEGA
                            NegQuirofano.ActualizarKardexSic(DevolucionNumero.ToString(), bodega);
                            CargarProcedimientosPaciente();
                        }
                        //else
                        //{
                        //    MessageBox.Show("La devolución no se a guardado. Consulte con el administrador del sistema.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }
                    //else
                    //{
                    //    MessageBox.Show("Seleccione un pedido.", "His3000");
                    //}
                }
                else
                    MessageBox.Show("Solo Se Puede Hacer Devoluciones Desde Farmacia", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Debe Seleccionar Un Producto para la Devolución: " + ex.ToString(), "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TablaProcedimientos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (TablaProcedimientos.ReadOnly == false)
                {

                    if (MessageBox.Show("Al eliminar el registro no se podra volver a recuperar.\r\n¿Desea continuar?",
                        "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                        == DialogResult.Yes)
                    {
                        try
                        {
                            if (TablaProcedimientos.SelectedRows.Count == 1)
                            {
                                NegQuirofano.QuirofanoEliminaRegistro(TablaProcedimientos.CurrentRow.Cells[0].Value.ToString(),
                                    Convert.ToInt32(cie_codigo), Convert.ToInt64(ate_codigo));
                                MessageBox.Show("Registro eliminado correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                                MessageBox.Show("No se pueden eliminar varios registros a la vez", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Algo ocurrio al eliminar el registro. Más detalle: " + ex.Message,
                                "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
            ((DataTable)TablaProcedimientos.DataSource).DefaultView.RowFilter = $"Producto LIKE '%{txtProducto.Text}%'";
        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar productos con cantidad en cero?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                NegQuirofano.QuirofanoEliminaRegistro2(Convert.ToInt32(cie_codigo), Convert.ToInt64(ate_codigo));

                CargarProcedimientoPaciente();
                MessageBox.Show("Productos en cero eliminados correctamente.", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                CargarProcedimientoPaciente();
        }
        public double cantAnterior = 0;
        public int rowAnterior = 0;
        private void TablaProcedimientos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                cantAnterior = Convert.ToDouble(TablaProcedimientos.Rows[e.RowIndex].Cells[4].Value.ToString());
                rowAnterior = e.RowIndex;
            }
        }

        private void TablaProcedimientos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (Convert.ToDouble(TablaProcedimientos.Rows[rowAnterior].Cells[4].Value.ToString()) > Convert.ToDouble(TablaProcedimientos.Rows[rowAnterior].Cells[3].Value.ToString()))
                {
                    MessageBox.Show("La cantidad no puede ser mayor al stock", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TablaProcedimientos.Rows[e.RowIndex].Cells[4].Value = cantAnterior;
                }
                else if (Convert.ToDouble(TablaProcedimientos.Rows[rowAnterior].Cells[4].Value.ToString()) < 0)
                {
                    MessageBox.Show("Cantidad debe ser mayor a 0", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TablaProcedimientos.Rows[e.RowIndex].Cells[4].Value = cantAnterior;
                }
                else
                {
                    string isDecimal = TablaProcedimientos.Rows[rowAnterior].Cells[4].Value.ToString();
                    bool valida = false;
                    for (int i = 0; i < isDecimal.Length; i++)
                    {
                        if (isDecimal.Substring(i, 1) == ".")
                            valida = true;
                    }
                    if (valida)
                    {
                        if (!NegQuirofano.validaDecimales(TablaProcedimientos.Rows[rowAnterior].Cells[0].Value.ToString()))
                        {
                            MessageBox.Show("El producto no permite decimales.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TablaProcedimientos.Rows[e.RowIndex].Cells[4].Value = cantAnterior;
                            return;
                        }
                    }
                    NegQuirofano.QuirofanoActualizaProductos(TablaProcedimientos.Rows[rowAnterior].Cells[0].Value.ToString(),
                        Convert.ToInt32(cie_codigo), Convert.ToDouble(TablaProcedimientos.Rows[rowAnterior].Cells[4].Value.ToString()), Convert.ToInt64(ate_codigo));
                }
            }
        }

        private void TablaProcedimientos_Leave(object sender, EventArgs e)
        {
            TablaProcedimientos.Columns["Stock"].ReadOnly = true;
        }
    }
}
