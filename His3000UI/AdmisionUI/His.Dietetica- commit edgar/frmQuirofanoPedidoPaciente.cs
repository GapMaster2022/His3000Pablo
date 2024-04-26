using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Dietetica
{
    public partial class frmQuirofanoPedidoPaciente : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string cie_codigo; //Recibe el codigo del procedimiento desde el explorador
        internal static string pac_codigo; //Recibe el codigo del paciente desde el explorador
        internal static string ate_codigo; //Recibe el codigo de atencion desde el explorador
        internal static string cie_descripcion; //Recibe la descripcion del procedimiento desde el explorador
        internal static string nombrepaciente; //Recibe el nombre del paciente desde el explorador
        internal static bool modotablet;
        private static string orden;
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
            lblprocedimiento.Text = cie_descripcion;
            lblmedico.Text = medico;
            lblseguro.Text = seguro;
            lbltipo.Text = tipo;
            lblgenero.Text = genero;
            lblreferido.Text = referido;
            CargarProcedimientosPaciente();
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
        }
        public void AlternarColores(DataGridView dgv)
        {
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(238, 245, 253);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
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
                if(item.Cells[5].Value.ToString() != "")
                {
                    if(Convert.ToInt32(item.Cells[5].Value.ToString()) <= Convert.ToInt32(item.Cells[3].Value.ToString()))
                    {
                        Despachar = true;
                    }
                    else
                    {
                        Despachar = false;
                        break;
                    }
                }
                else
                {
                    Despachar = false;
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
            TablaProcedimientos.Columns[6].ReadOnly = true;
            TablaProcedimientos.Columns[7].Visible = false;
            SumarCantidades();
            ValidarStock();
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
            modotablet = false;
        }
        public void RecuperarUsuario()
        {
            usuario = His.Entidades.Clases.Sesion.nomUsuario;
        }
        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            int contador = 0;
            int valor = Convert.ToInt32(Quirofano.Cantidad(ate_codigo));
            bool Error = false;
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
                    if(Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[5].Value.ToString()) <= 
                        Convert.ToInt32(TablaProcedimientos.Rows[i].Cells[3].Value.ToString())){
                        Error = false;
                    }
                    else
                    {
                        Error = true;
                        MessageBox.Show("¡No Hay Suficiente Stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Error = false;
                        break;
                    }
                    if(Error == false)
                    {
                        if (contador == valor)
                        {
                            LimpiarVariables();
                            codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                            cantidad = TablaProcedimientos.Rows[i].Cells[4].Value.ToString();
                            cant_usada = TablaProcedimientos.Rows[i].Cells[5].Value.ToString();
                            orden = TablaProcedimientos.Rows[i].Cells[7].Value.ToString();
                            Quirofano.ModificarProcedimientoPaciente(orden, cie_codigo, codpro, cantidad, pac_codigo, ate_codigo, cant_usada, usuario);
                        }
                        else
                        {
                            LimpiarVariables();
                            codpro = TablaProcedimientos.Rows[i].Cells[0].Value.ToString();
                            cantidad = TablaProcedimientos.Rows[i].Cells[4].Value.ToString();
                            cant_usada = TablaProcedimientos.Rows[i].Cells[5].Value.ToString();
                            orden = TablaProcedimientos.Rows[i].Cells[7].Value.ToString();
                            Quirofano.AgregarProcedimientoPaciente(orden, cie_codigo, codpro, cantidad, pac_codigo, ate_codigo, cant_usada, usuario);
                        }
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
        }

        private void TablaProcedimientos_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
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
            }
        }

        private void btnenter_Click(object sender, EventArgs e)
        {
            if (fila <= TablaCantidad)
            {
                TablaProcedimientos.Rows[fila].Cells[5].Value = pantalla;
                pantalla = null;
                fila += 1;
            }
        }

        private void TablaProcedimientos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            fila = TablaProcedimientos.CurrentRow.Index;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "0";
                pantalla += num;
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "8";
                pantalla += num;
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "9";
                pantalla += num;
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "4";
                pantalla += num;
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "5";
                pantalla += num;
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "6";
                pantalla += num;
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "1";
                pantalla += num;
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "2";
                pantalla += num;
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (TablaProcedimientos.Enabled == true)
            {
                string num = "3";
                pantalla += num;
            }
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            pantalla = pantalla.Remove(pantalla.Length - 1);
        }

        private void toolStripDespachar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Está Seguro de Generar el Pedido?", "Warning", MessageBoxButtons.YesNo
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
        }
    }
}
