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
        public frmQuirofanoPedidoPaciente()
        {
            InitializeComponent();
        }

        private void frmQuirofanoPedidoPaciente_Load(object sender, EventArgs e)
        {
            if(modotablet == true)
            {
                panelTecladoNumerico.Visible = true;
            }
            else
            {
                panelTecladoNumerico.Visible = false;
            }
            lblpaciente.Text = nombrepaciente;
            lblmedico.Text = medico;
            lblseguro.Text = seguro;
            lbltipo.Text = tipo;
            lblgenero.Text = genero;
            lblreferido.Text = referido;
            Bloquear();
            if(Despachar == false)
            {
                toolStripDespachar.Enabled = false;
            }
            else
            {
                toolStripDespachar.Enabled = true;
            }
            RecuperarUsuario();
            CargarProcedimientoPaciente();
        }
        public void AlternarColores(DataGridView dgv)
        {
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(238, 245, 253);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
        }
        public void CargarProcedimientoPaciente()
        {
            cmbProcedimiento.DataSource = Quirofano.ProcedimientosPaciente(ate_codigo, pac_codigo);
            cmbProcedimiento.DisplayMember = "CIE_DESCRIPCION";
            cmbProcedimiento.ValueMember = "CIE_CODIGO";
        }
        public void SumarCantidades()
        {
            int sumaoriginal = 0;
            int sumausar = 0;
            foreach (DataGridViewRow item in TablaProcedimientos.Rows)
            {
                int cantoriginal, cantusada;
                cantoriginal = Convert.ToInt32(item.Cells[4].Value.ToString());
                sumaoriginal += cantoriginal;
                if (item.Cells[5].Value.ToString() == "")
                {
                    sumausar += 0;
                }
                else
                {
                    cantusada = Convert.ToInt32(item.Cells[5].Value.ToString());
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
                if(item.Cells[4].Value.ToString() != "")
                {
                    if(Convert.ToInt32(item.Cells[4].Value.ToString()) <= Convert.ToInt32(item.Cells[3].Value.ToString()))
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
        public void CargarProcedimientosPaciente()
        {
            TablaProcedimientos.DataSource = Quirofano.PacienteProcedimiento(cie_codigo, ate_codigo);
            TablaProcedimientos.Columns[0].ReadOnly = true;
            TablaProcedimientos.Columns[1].ReadOnly = true;
            TablaProcedimientos.Columns[2].ReadOnly = true;
            TablaProcedimientos.Columns[3].ReadOnly = true;
            TablaProcedimientos.Columns[4].ReadOnly = true;
            TablaProcedimientos.Columns[5].ReadOnly = true;
            TablaProcedimientos.Columns[6].ReadOnly = false;
            TablaProcedimientos.Columns[8].Visible = false;
            TablaProcedimientos.Columns[7].ReadOnly = true;
            TablaProcedimientos.Columns[9].ReadOnly = true;
            TablaProcedimientos.Columns[10].Visible = false;
            SumarCantidades();
            AlternarColores(TablaProcedimientos);
            if(modotablet == true)
            {
                foreach (DataGridViewRow fila in TablaProcedimientos.Rows)
                {
                    fila.Height = 40;
                }
            }
            else
            {
                foreach (DataGridViewRow fila in TablaProcedimientos.Rows)
                {
                    fila.Height = 20;
                }
            }
            TablaCantidad = TablaProcedimientos.Rows.Count;
            AutomatizarDevolucion();
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
            TablaProcedimientos.Enabled = false;
        }
        public void Desbloquear()
        {
            btnGuardar.Enabled = true;
            TablaProcedimientos.Enabled = true;
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
            int valor = Convert.ToInt32(Quirofano.Cantidad(ate_codigo));
            lblpaciente.Focus();
            lblcantoriginal.Focus();
            try
            {
                for (int i = 0; i < TablaProcedimientos.RowCount; i++)
                {
                    contador += 1;
                }
                for (int i = 0; i < TablaProcedimientos.RowCount; i++)
                {
                    if (contador == valor)
                    {
                        LimpiarVariables();
                        codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                        cant_usada = TablaProcedimientos.Rows[i].Cells[6].Value.ToString();
                        orden = TablaProcedimientos.Rows[i].Cells[8].Value.ToString();
                        Quirofano.ModificarProcedimientoPaciente(orden, cie_codigo, codpro, pac_codigo, ate_codigo, cant_usada, usuario);
                    }
                    else
                    {
                        LimpiarVariables();
                        codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                        pro_descripcion = TablaProcedimientos.Rows[i].Cells[1].Value.ToString();
                        cantidad = TablaProcedimientos.Rows[i].Cells[4].Value.ToString();
                        cant_usada = TablaProcedimientos.Rows[i].Cells[6].Value.ToString();
                        orden = TablaProcedimientos.Rows[i].Cells[8].Value.ToString();
                        Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codpro, cantidad, pac_codigo, ate_codigo, cant_usada, usuario);
                    }
                }
                btnEditar.Enabled = true;
                Bloquear();
                CargarProcedimientosPaciente();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void GenerarTicket()
        {
            
            DatosQuirofanoPedido p = new DatosQuirofanoPedido();
            frmQuirofanoPedidoImprimir x = new frmQuirofanoPedidoImprimir();
            DataTable TablaInfo = new DataTable();
            TablaInfo = Quirofano.RecuperarInfoPaciente(ate_codigo);
            foreach (DataRow item in TablaInfo.Rows)
            {
                x.ped.Clear();
                p.atencion = item[0].ToString();
                p.hc = item[1].ToString(); ;
                p.num_pedido = numpedido;
                p.paciente = item[2].ToString(); ;
                p.fecha = item[3].ToString(); ;
                p.medico = item[4].ToString(); ;
                p.usuario = item[5].ToString(); ;
                x.ped.Add(p);
            }
            DataTable Tabla = new DataTable(); //Almacena los productos del pedido generado recientemente
            Tabla = Quirofano.ProductosPaciente(ate_codigo, numpedido);
            foreach (DataRow item in Tabla.Rows)
            {
                DatosQuirofanoPedido pedidoimp = new DatosQuirofanoPedido
                {
                    cant = item[0].ToString(),
                    codigo = item[1].ToString(),
                    descripcion = item[2].ToString()
                };
                x.ped.Add(pedidoimp);
            }
            x.Show();

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
        }

        private void TablaProcedimientos_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                OnlyNumber(e, false);
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
            if(TablaProcedimientos.Enabled == true)
            {
                string num = "7";
                pantalla += num;
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
            }
        }

        private void btnenter_Click(object sender, EventArgs e)
        {
            if(TablaProcedimientos.Enabled != false)
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
            if(TablaProcedimientos.Enabled != false)
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
                catch (Exception)
                {

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
            if (PedidoAdicional == true && error == false) 
            {
                if (MessageBox.Show("¿Está Seguro de Generar el Pedido Adicional?", "Warning", MessageBoxButtons.YesNo
                , MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < TablaProcedimientos.RowCount; i++)
                        {
                            codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                            cant_usada = TablaProcedimientos.Rows[i].Cells[6].Value.ToString();
                            Quirofano.PedidoAdicional(ate_codigo, pac_codigo, cie_codigo, cant_usada, codpro); //Aqui graba cuando ya se decida hacer el pedido adicional
                            //Es decir que cuando se haga un pedido adicional este solo debera enviar los productos que sean mayor a cero a farmacia, como referencia que diga que es pedido adicional

                            //aqui codigo para cargar productos a cuentas pacientes y demas metodos que lleva la accion

                            //Quirofano.ProductoBodega(codpro, cant_usada);
                        }
                        MessageBox.Show("Pedido Adicional Exitoso", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Bloquear();
                        CargarProcedimientosPaciente();
                        btnEditar.Enabled = true;
                        ValidarAdicional();
                        PedidoAdicional = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            else
            {
                if (MessageBox.Show("¿Está Seguro de Generar el Pedido?", "Warning", MessageBoxButtons.YesNo
                , MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < TablaProcedimientos.RowCount; i++)
                        {
                            codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                            cant_usada = TablaProcedimientos.Rows[i].Cells[5].Value.ToString();
                            //aqui codigo para cargar productos a cuentas pacientes y demas metodos que lleva la accion

                            //Quirofano.ProductoBodega(codpro, cant_usada);
                        }
                        MessageBox.Show("Pedido Exitoso", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Bloquear();
                        CargarProcedimientosPaciente();
                        btnEditar.Enabled = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                ValidarAdicional();
            }
        }

        private void cmbProcedimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            cie_codigo = cmbProcedimiento.SelectedValue.ToString();
            CargarProcedimientosPaciente();
            ValidarStock();
            toolStripRegistro.Visible = true;
            MostrarRegistro();
            //Tabla que verifica si tiene valores dentro del registro
            DataTable Tabla = new DataTable();
            Tabla = Quirofano.RegistroPaciente(ate_codigo, pac_codigo, cie_codigo);
            foreach (DataRow item in Tabla.Rows)
            {
                if(item[0].ToString() == "")
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
        //funcion que almacena las cantidades de la tabla si dentro de la base de datos no existe
        public void GuardarCantidades()
        {
            DateTime fecha = DateTime.Now;
            double costo, total;
            int contador = 0;
            int valor = Convert.ToInt32(Quirofano.Cantidad(ate_codigo));
            try
            {
                //Verifico si tiene los productos del procedimiento
                if(TablaProcedimientos.Rows.Count > 0)
                {
                    for (int i = 0; i < TablaProcedimientos.RowCount; i++)
                    {
                        contador += 1;
                    }
                    if (contador != valor)
                    {
                        Quirofano.AgregarPedidoPaciente(fecha.ToString(), id_usuario, ate_codigo, hab_codigo);
                        numpedido = Quirofano.RecuperarPedidoNum(ate_codigo); //Recupera el ultimo numero de pedido de acuerdo con la atencion
                        foreach (DataGridViewRow item in TablaProcedimientos.Rows)
                        {
                            codpro = item.Cells[0].Value.ToString();
                            pro_descripcion = item.Cells[1].Value.ToString();
                            cantidad = item.Cells[4].Value.ToString();
                            orden = item.Cells[8].Value.ToString();
                            costo = Convert.ToDouble(item.Cells[10].Value.ToString());
                            total = Math.Round((Convert.ToInt32(cantidad) * costo), 2);
                            Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codpro, cantidad, pac_codigo, ate_codigo, "0", usuario);
                            Quirofano.PedidoDetalle(codpro, pro_descripcion, cantidad, valor.ToString(), total.ToString(), numpedido);
                        }
                        GenerarTicket();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Algo ocurrio al ingresar productos del procedimiento al paciente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ValidarProcedimientoCerrado()
        {
            string estado;
            try
            {
                estado = Quirofano.ProcedimientoCerrado(ate_codigo, pac_codigo, cie_codigo);
                if(estado == "1")
                {
                    Bloquear();
                    btnadicional.Enabled = false;
                    toolStripAgregar.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnEditar.Enabled = false;
                    toolStripRegistro.Enabled = true;
                    toolStripDespachar.Enabled = false;
                    toolStripCerrar.Enabled = false;
                    btncancelar.Enabled = false;
                }
                if (estado == "")
                {
                    btnadicional.Enabled = true;
                    toolStripAgregar.Enabled = true;
                    btnGuardar.Enabled = false;
                    btnEditar.Enabled = true;
                    toolStripRegistro.Enabled = true;
                    toolStripDespachar.Enabled = true;
                    toolStripCerrar.Enabled = true;
                    btncancelar.Enabled = true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error con Validar Estado de Procedimiento");
                return;
            }
        }
        public void MostrarRegistro()
        {
            DataTable TablaRegistro = new DataTable();
            TablaRegistro = Quirofano.RegistroPaciente(ate_codigo, pac_codigo, cie_codigo);
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
                lblduracion.Text = item[15].ToString(); // duracion de procedimiento
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
            //TablaProcedimientos.Enabled = true;
            if(TablaProcedimientos.Rows.Count > 0)
            {
                frmquirofanopedidoadicional.ate_codigo = ate_codigo;
                frmquirofanopedidoadicional.pac_codigo = pac_codigo;
                frmquirofanopedidoadicional.cie_codigo = cie_codigo;
                frmquirofanopedidoadicional x = new frmquirofanopedidoadicional();
                x.Show();
                x.FormClosed += X_FormClosed1;
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
            if(checkBox1.Checked == true)
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
            if(TablaProcedimientos.Rows.Count > 0)
            {
                frmQuirofanoRegistro.cie_codigo = cmbProcedimiento.SelectedValue.ToString();
                frmQuirofanoRegistro.cie_descripcion = cmbProcedimiento.SelectedText;
                frmQuirofanoRegistro.ate_codigo = ate_codigo;
                frmQuirofanoRegistro.pac_codigo = pac_codigo;
                frmQuirofanoRegistro.nom_paciente = lblpaciente.Text;
                frmQuirofanoRegistro x = new frmQuirofanoRegistro();
                x.Show();
            }
            else
            {
                MessageBox.Show("Debe Actualizar primero el procedimiento.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripAgregar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Desea Agregar Producto al Procedimiento", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                frmayudaQuirofano.productosPedidos = true;
                frmayudaQuirofano x = new frmayudaQuirofano();
                x.Show();
                x.FormClosed += X_FormClosed;
            }
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(codigo_producto !=null && orden !=null && cant != null)
            {
                try
                {

                    Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codigo_producto, cant, pac_codigo, ate_codigo, null, usuario);
                    MessageBox.Show("El Producto ha sido Agregado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarProcedimientosPaciente();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void toolStripCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta Seguro de Cerrar Procedimiento? \n Una Vez Cerrado no se Volvera Abrir. \n ¿Desea Continuar?",
                "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                try
                {
                    Quirofano.CerrarProcedimiento(ate_codigo, pac_codigo, cie_codigo);
                    MessageBox.Show("Procedimiento Cerrado Correctamente", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {

                    MessageBox.Show("Algo Ocurrio al Cerrar el Procedimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TablaProcedimientos_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //CALCULO AUTOMATICO PARA LA COLUMNA DE DEVOLUCION... AUN NO ESTA DISPONIBLE POR EL CAMBIO DE COLUMNAS SOLO ES UNA PRUEBA 
            //string valor = null;
            //if(TablaProcedimientos.Rows.Count > 1)
            //{
            //    if(fila <= TablaCantidad)
            //    valor = TablaProcedimientos.Rows[fila].Cells[5].Value.ToString();
            //    if (valorcelda != null || valorcelda != "")
            //    {
            //        valor = Convert.ToString(Math.Abs(Convert.ToInt32(valorcelda) - Convert.ToInt32(valor)));
            //        TablaProcedimientos.Rows[fila].Cells[5].Value = valor;
            //        fila += 1;
            //    }
            //}
        }

        //private void guardarPedido(List<PEDIDOS_DETALLE> PedidosDetalle, string _PED_DESCRIPCION, int _ATE_CODIGO)
        //{
        //    ///////////////recibo listado de productos desde el FormDialog
        //    List<PEDIDOS_DETALLE> listaProductosSolicitados = PedidosDetalle;
        //    /////////////////////////////////
        //    //Proceso para guardar
        //    /////////////////////////////////
        //    if (listaProductosSolicitados != null && listaProductosSolicitados.Count > 0)
        //    {


        //        Double totales = 0.00;
        //        Double valores = 0.00;
        //        Double ivaTotal = 0.00;
        //        PEDIDOS pedido = new PEDIDOS();
        //        pedido.PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1;
        //        pedido.PED_FECHA = DateTime.Now; 
        //        pedido.PED_DESCRIPCION = _PED_DESCRIPCION;
        //        pedido.PED_ESTADO = 2;
        //        pedido.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
        //        pedido.ATE_CODIGO = _ATE_CODIGO;
        //        ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
        //        byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
        //        pedido.PEE_CODIGO = codigoEstacion; 
        //        pedido.TIP_PEDIDO = His.Parametros.FarmaciaPAR.PedidoMedicamentos;
        //        pedido.PED_PRIORIDAD = 1;
        //        pedido.MED_CODIGO = 0;
        //        PEDIDOS_AREAS ar = new PEDIDOS_AREAS();
        //            ComboBox cmb_Areas = new ComboBox();
        //            cmb_Areas.DataSource=NegPedidos.recuperarListaAreasTodas().OrderBy(a => a.PEA_NOMBRE).ToList();
        //            ar = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
        //        pedido.PEDIDOS_AREASReference.EntityKey = ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).EntityKey;
        //        if (form.codTransaccion != 0)
        //        {
        //            pedido.PED_TRANSACCION = form.codTransaccion;
        //        }
        //        else
        //        {
        //            pedido.PED_TRANSACCION = pedido.PED_CODIGO;
        //        }
        //        Int32 Pedido = pedido.PED_CODIGO;
        //        /////////CREANDO PEDIDO///////
        //        NegPedidos.crearPedido(pedido);
        //        foreach (var solicitud in listaProductosSolicitados)
        //        {
        //            if (Convert.ToInt32(ar.PEA_CODIGO) != 1)
        //            {
        //                if (Convert.ToInt32(ar.PEA_CODIGO) == 16)
        //                {
        //                    DtoDetalleHonorariosMedicos HonorariosMedico = new DtoDetalleHonorariosMedicos();
        //                    HonorariosMedico = form.DetalleHonorarios.FirstOrDefault(h => h.CODPRO == solicitud.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value.ToString());
        //                    if (HonorariosMedico != null)
        //                    {
        //                        pedido.MED_CODIGO = HonorariosMedico.MED_CODIGO;
        //                    }
        //                }
        //                solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
        //                solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
        //                // --
        //                Int32 codpro = Convert.ToInt32(solicitud.PRO_CODIGO_BARRAS.ToString());
        //                Int32 xcodDiv = 0;
        //                Int16 XRubro = 0;
        //                DataTable auxDT = NegFactura.recuperaCodRubro(codpro);
        //                foreach (DataRow row in auxDT.Rows)
        //                {
        //                    XRubro = Convert.ToInt16(row[0].ToString());
        //                    xcodDiv = Convert.ToInt32(row[1].ToString());
        //                }
        //                NegPedidos.crearDetallePedido(solicitud, pedido, XRubro, xcodDiv, form.NumVale);


        //            }
        //            else
        //            {
        //                string CodigoProducto = "";
        //                decimal ValorIva = 0;
        //                solicitud.PEDIDOSReference.EntityKey = pedido.EntityKey;
        //                solicitud.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
        //                CodigoProducto = Convert.ToString(solicitud.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value).Substring(0, 1);
        //                ValorIva = Convert.ToDecimal(solicitud.PDD_IVA);
        //                if (ValorIva > 0)
        //                {
        //                    NegPedidos.crearDetallePedido(solicitud, pedido, 27/*SUMINISTROS*/ , 1, form.NumVale);
        //                }
        //                else
        //                {
        //                    NegPedidos.crearDetallePedido(solicitud, pedido, 1/*MEDICAMENTOS*/ , 1, form.NumVale);
        //                }
        //            }
        //            /*HONORARIOS MEDICOS*/
        //            DtoDetalleHonorariosMedicos Honorarios = new DtoDetalleHonorariosMedicos();
        //            Honorarios = form.DetalleHonorarios.FirstOrDefault(h => h.CODPRO == solicitud.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value.ToString());
        //            if (Honorarios != null)
        //            {
        //                Honorarios.PED_CODIGO = Pedido;
        //                Honorarios.ID_LINEA = solicitud.PDD_CODIGO;
        //                NegPedidos.GuardaDetalleHonorarios(Honorarios);
        //            }
        //            totales = totales + Convert.ToDouble(solicitud.PDD_TOTAL);
        //            valores = valores + Convert.ToDouble(solicitud.PDD_VALOR);
        //            ivaTotal = ivaTotal + Convert.ToDouble(solicitud.PDD_IVA);
        //        }
        //        List<DtoDetalleCuentaPaciente> listaCuenta = NegDetalleCuenta.recuperarCuentaPaciente(cod_ate);

        //        //cargarDetalleFactura();
        //        MessageBox.Show("Pedido No." + Pedido.ToString() + " fue ingresado correctamente.", "His3000");
        //        /*impresion*/
        //        frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Pedido, ar.PEA_CODIGO, 1, 1);
        //        frmPedidos.Show();
        //    }

        //}
    }
}
