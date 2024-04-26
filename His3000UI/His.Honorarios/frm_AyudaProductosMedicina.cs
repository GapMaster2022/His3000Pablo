using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Honorarios
{
    public partial class frm_AyudaProductosMedicina : Form
    {
        public string paciente;
        public string edad;
        public string hc;
        public string atencion;
        public int codigoArea;
        public int _Rubro;
        public int _CodigoConvenio;
        public int _CodigoEmpresa;
        public string ate_codigo;
        private int Resultado = 0;
        private bool StockNegativo = false;
        Boolean _todos = false;
        public string codpro;
        public string despro;
        public string cant;
        public DataTable dt;
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        public frm_AyudaProductosMedicina()
        {
            InitializeComponent();
        }
        
        private void frm_AyudaProductosMedicina_Load(object sender, EventArgs e)
        {
            lblpaciente.Text = paciente;
            lbledad.Text = edad;
            lblhc.Text = hc;
            lblusuario.Text = His.Entidades.Clases.Sesion.nomUsuario;
            lblatencion.Text = atencion;
            Bloquear();
            CargarHoras();
            CargarDias();
            try
            {
                DataTable verificafactura = new DataTable();
                verificafactura = NegFactura.VerificaFactura(Convert.ToInt64(ate_codigo));
                string factura = verificafactura.Rows[0]["ate_factura_paciente"].ToString();
                string esc_codigo = verificafactura.Rows[0]["esc_codigo"].ToString();
                DataTable dtProductos = new DataTable();
                DataTable dtPermisosUsuario = new DataTable();
                List<DataRow> List = new List<DataRow>();
                Resultado = 0;

                Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "SALDOS NEGATIVOS PEDIDOS");
                if (Resultado == 0)
                {
                    StockNegativo = false;
                }
                else
                {
                    StockNegativo = true;
                }
                cbcantidad.SelectedIndex = 0;

                if (codigoArea != 1)
                {
                    //Valida el tipo de combenio para los rubros que no son medicamento ni insumos
                    if (_CodigoConvenio == 0)
                    {
                        if (codigoArea > 0)
                        {
                            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
                            if (_todos)  ///alexalex
                            {
                                dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                            }
                            else
                            {
                                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                            }
                        }
                        else
                        {
                            if (_todos)  ///alexalex
                            {
                                dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                            }
                            else
                            {
                                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                            }
                        }
                    }
                    else
                    {
                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                        if (dtProductos.Rows.Count == 0)
                        {
                            if (codigoArea > 0)
                                if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                                }
                            else
                                if (_todos)  ///alexalex
                            {
                                dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                            }
                            else
                            {
                                dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                            }
                        }
                    }
                }
                else
                {
                    //if(_CodigoConvenio==0)
                    if (codigoArea > 0)
                        //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);
                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                    else
                        //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
                        dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                    //else
                    //    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPconvenios(1, ""/*txtBuscar.Value.ToString()*/, _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);                
                }

                //foreach (DataRow dr in dtProductos.Rows)
                //{
                //    List.Add(dr);
                //}

                //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
                //xamDataPresenterProductos.DataSource = dtProductos;
                TablaDisponibles.DataSource = dtProductos;
                OcultarColumnas();
                txtfiltro.Focus();
                /*Verificacion de los permisos del usuario segun el perfil*/

                Resultado = 0;
                Resultado = NegPedidos.PermiososUsuario(His.Entidades.Clases.Sesion.codUsuario, "MUESTRA PRECIOS PEDIDOS");

                //if (esc_codigo == "1")
                //{
                //    //cargo la lista de cantidad a paginar  
                    
                //    //cargo los valores por defecto de la grilla
                //}
                //else
                //{
                //    MessageBox.Show("Paciente Fue Dado de Alta No Se Puede Hacer Pedidos, Consulte Con Caja", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    this.Close();
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        private void OcultarColumnas()
        {
            this.TablaDisponibles.Columns[0].Visible = false;
            this.TablaDisponibles.Columns[3].Visible = false;
            this.TablaDisponibles.Columns[4].Visible = false;
            this.TablaDisponibles.Columns[5].Visible = false;
            this.TablaDisponibles.Columns[6].Visible = false;
            this.TablaDisponibles.Columns[7].Visible = false;
        }
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                //cargo la lista de cantidad a paginar
                DataTable dtProductos = new DataTable();

                List<DataRow> List = new List<DataRow>();
                cbcantidad.SelectedIndex = 0;

                if (codigoArea != 1)
                {
                    if (_CodigoConvenio == 0)
                    {
                        if (codigoArea > 0)
                        {
                            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null,codigoArea);

                            if (xamCheckEditor1.Checked == true)
                            {
                                if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(2, txtfiltro.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por el generico / Giovanny Tapia / 18/10/2012
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(2, txtfiltro.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por el generico / Giovanny Tapia / 18/10/2012
                                }
                            }

                            else
                            {
                                if (_todos)  ///alexalex
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSPall(1, txtfiltro.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                                }
                                else
                                {
                                    dtProductos = Negocio.NegProducto.RecuperarProductosListaSP(1, txtfiltro.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (codigoArea > 0)
                    {

                        if (xamCheckEditor1.Checked == true)
                        {
                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(2, txtfiltro.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                        }

                        else
                        {
                            //listaProductosDisponibles = Negocio.NegProducto.RecuperarProductosLista(0, 20, null);
                            dtProductos = Negocio.NegProducto.RecuperarProductosListaSP_Farmacia(1, txtfiltro.Text.ToString(), _Rubro, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio); // Busca por producto / Giovanny Tapia / 18/10/2012
                        }

                    }
                }

                //xamDataPresenterProductos.DataSource = listaProductosDisponibles;
                //xamDataPresenterProductos.DataSource = dtProductos;
                TablaDisponibles.DataSource = dtProductos;
                OcultarColumnas();
                if (TablaDisponibles.Rows.Count > 0)
                {
                    TablaDisponibles.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TablaDisponibles_KeyDown(object sender, KeyEventArgs e)
        {
            if(TablaDisponibles.SelectedRows.Count > 0)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    Desbloquear();
                    codpro = TablaDisponibles.CurrentRow.Cells[1].Value.ToString();
                    despro = TablaDisponibles.CurrentRow.Cells[2].Value.ToString();
                    txtcantidad.Text = "1";
                    txtcantidad.Focus();
                }
            }
            
        }

        private void txtcantidad_KeyDown(object sender, KeyEventArgs e)
        {
            errorProvider1.Clear();
            if(e.KeyCode == Keys.Enter)
            {

                if (txtcantidad.Text != "")
                {
                    if (Convert.ToInt32(txtcantidad.Text) >= 1)
                    {
                        cant = txtcantidad.Text;
                        txtdosis.Focus();
                    }
                    else
                    {
                        errorProvider1.SetError(txtcantidad, "Cantidad no puede ser 0");
                    }
                }
                else
                {
                    errorProvider1.SetError(txtcantidad, "Debe agregar la cantidad.");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Está seguro de Cancelar Pedido de medicamentos?", "HIS3000",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtcantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789," + Convert.ToChar(8);
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            errorProvider1.Clear();
            if(e.KeyCode == Keys.Enter)
            {
                if(txtdosis.Text.Trim() != "")
                    cbhoras.Focus();
                else
                {
                    errorProvider1.SetError(txtdosis, "Debe agregar la indicación");
                    txtdosis.Focus();
                }
            }
        }
        public void Bloquear()
        {
            txtcantidad.Enabled = false;
            txtdias.Enabled = false;
            txtdosis.Enabled = false;
            btnañadir.Enabled = false;
            cbhoras.Enabled = false;
            cbTiempo.Enabled = false;
        }
        public void Desbloquear()
        {
            txtcantidad.Enabled = true;
            txtdias.Enabled = true;
            txtdosis.Enabled = true;
            btnañadir.Enabled = true;
            cbhoras.Enabled = true;
            cbTiempo.Enabled = true;
        }

        private void txtdias_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void cbhoras_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtdias.Text = "1";
                txtdias.Focus();
            }
        }

        private void txtdias_KeyDown(object sender, KeyEventArgs e)
        {
            errorProvider1.Clear();
            if(e.KeyCode == Keys.Enter)
            {
                if(txtdias.Text != "")
                {
                    if (txtdias.Text != "0")
                    {
                        cbTiempo.Focus();
                    }
                    else
                    {
                        errorProvider1.SetError(txtdias, "Día no puede ser 0");
                    }
                } 
                else
                {
                    errorProvider1.SetError(txtdias, "Debe agregar el/los día(s)");
                }
            }
        }

        private void cbTiempo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                TablaSolicitados.Rows.Add(cant, despro, txtdosis.Text + " CADA " + cbhoras.GetItemText(cbhoras.SelectedItem) + " DURANTE " + txtdias.Text + " " + cbTiempo.GetItemText(cbTiempo.SelectedItem));
                Limpiar();
                Bloquear();
                TablaDisponibles.Focus();
            }
        }
        public void Limpiar()
        {
            txtcantidad.Text = "";
            txtdias.Text = "";
            txtdosis.Text = "";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(TablaSolicitados.Rows.Count > 0)
            {
                //Creating DataTable.
                dt = new DataTable();

                foreach (DataGridViewColumn columna in this.TablaSolicitados.Columns)
                {
                    DataColumn col = new DataColumn(columna.HeaderText);
                    dt.Columns.Add(col);
                }
                //Recorrer filas
                foreach (DataGridViewRow fila in this.TablaSolicitados.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = fila.Cells[0].Value.ToString();
                    dr[1] = fila.Cells[1].Value.ToString();
                    dr[2] = fila.Cells[2].Value.ToString();
                    dt.Rows.Add(dr);
                }
                this.Close();
            }
        }
        public void CargarHoras()
        {
            try
            {
                cbhoras.DataSource = Certificado.CargarHoras();
                cbhoras.DisplayMember = "MHO_DESCRIPCION";
                cbhoras.ValueMember = "MHO_CODIGO";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarDias()
        {
            try
            {
                cbTiempo.DataSource = Certificado.CargarDias();
                cbTiempo.DisplayMember = "MDI_DESCRIPCION";
                cbTiempo.ValueMember = "MDI_CODIGO";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnañadir_Click(object sender, EventArgs e)
        {
            TablaSolicitados.Rows.Add(cant, despro, txtdosis.Text + " CADA " + cbhoras.GetItemText(cbhoras.SelectedItem) + " DURANTE " + txtdias.Text + " " + cbTiempo.GetItemText(cbTiempo.SelectedItem));
            Limpiar();
            Bloquear();
            TablaDisponibles.Focus();
        }
    }
}
