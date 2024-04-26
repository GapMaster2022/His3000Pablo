using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using Core.Entidades;
using His.General;
using His.Parametros;
using Recursos;
using His.Entidades.Clases;
using Infragistics.Win.UltraWinGrid;




namespace His.Maintenance
{
    public partial class frm_CatalogoTipo : Form
    {
        #region Variables
        public HC_CATALOGOS tipoprocedimientoOriginal = new HC_CATALOGOS();
        public HC_CATALOGOS catalogoModificado = new HC_CATALOGOS();
        public List<HC_CATALOGOS> procedimiento = new List<HC_CATALOGOS>();
        public int columnabuscada, idTipoCatalogo;
        private HC_CATALOGOS_TIPO tipoCatalogo;
        private HC_CATALOGOS modificarTipoCatalogo;
        int accion = 0;
        ImageList iconImages;
        int codigoCatalogo;
        string nombreCatalogo;

        #endregion

        public frm_CatalogoTipo(HC_CATALOGOS_TIPO parTipoCatalogo, string nombre, int accion)
        {
            InitializeComponent();
            cargarRecursos();
            inicializarControles();
            tipoCatalogo = parTipoCatalogo;
            nombreCatalogo = nombre;
            HalitarControles(true, true, false, true);
            this.accion = accion;
            idTipoCatalogo = parTipoCatalogo.HCT_CODIGO;             
            //zonaSeleccionada = procedimiento;
        }

        public frm_CatalogoTipo(HC_CATALOGOS parCatalogo, HC_CATALOGOS_TIPO tipoCatalogo, string nombre, int accion)
        {
            InitializeComponent();
            cargarRecursos();
            inicializarControles();
            modificarTipoCatalogo = parCatalogo;
            this.tipoCatalogo = tipoCatalogo;
            nombreCatalogo = nombre;
            HalitarControles(true, true, false, true);
            this.accion = accion;
            txt_Nombre.Text = parCatalogo.HCC_NOMBRE;
            idTipoCatalogo = tipoCatalogo.HCT_CODIGO;
            txt_Codigo.Text = Convert.ToString(parCatalogo.HCC_CODIGO);
            
        }
        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            btnGuardar.Image = Archivo.imgBtnImprimir32;
            btnSalir.Image = Archivo.imgBtnSalir32;
            
        }
        public void inicializarControles()
        {
            //Inicializo el imagenList
            iconImages = new System.Windows.Forms.ImageList();
            iconImages.ColorDepth = ColorDepth.Depth16Bit;
            iconImages.ImageSize = new Size(16, 16);
            iconImages.Images.Add(Archivo.icono);
            iconImages.Images.Add(Archivo.icoBtnOrganize);           
        }

        private void frm_Procedimientos_Load(object sender, EventArgs e)
        {
            try
            {
                //HalitarControles(false, false, false, false, false, true, false);
                HalitarControles(true, true, false, true);
                //RecuperaProcedimientos();
                cbx_Estado.Items.Add("Activado");
                cbx_Estado.Items.Add("Desactivado");               
                cbx_Estado.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                if(ex.InnerException !=null)
                    MessageBox.Show(ex.InnerException.Message);
                else
                    MessageBox.Show(ex.Message);
            }
        }
     

        #region Metodos Privados
                
        /// <summary>
        /// Encera los componentes del form
        /// </summary>
        private void ResetearControles()
        {
            catalogoModificado = new HC_CATALOGOS();
            tipoprocedimientoOriginal = new HC_CATALOGOS();

            txt_Codigo.Text = string.Empty;
            txt_Nombre.Text = string.Empty;
        }  

        private bool ValidarFormulario()
        {
            bool valido = true;
            if (txt_Nombre.Text == null || txt_Nombre.Text==string.Empty)
            {
                MessageBox.Show("Campo Obligatorio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Nombre.Focus();
                //AgregarError(txt_Nombre);
                valido = false;
            }else
            {
                cbx_Estado.Focus();
                if (cbx_Estado.SelectedIndex.Equals(-1))
                {                    
                }
                else
                {
                    catalogoModificado.HCC_NOMBRE = txt_Nombre.Text;
                    if (cbx_Estado.SelectedItem.ToString() == "Activado")
                    {
                        catalogoModificado.HCC_ESTADO = true;
                    }
                    else
                    {
                        catalogoModificado.HCC_ESTADO = false;
                    }
                }                    
            }          
            return valido;
        }       
    
        private void GrabarDatos()
        {
            DialogResult resultado;
            //gridProcedimiento.Focus();
            try
            {
                if (ValidarFormulario())
                {
                    resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (accion == 1)
                        {
                            if (catalogoModificado.EntityKey == null)
                            {
                                int codigoNuevo = NegCatalogos.RecuperaMaximoCatalogo();
                                codigoNuevo++;
                                catalogoModificado.HCC_CODIGO = codigoNuevo;
                                catalogoModificado.HC_CATALOGOS_TIPOReference.EntityKey = tipoCatalogo.EntityKey;
                                NegCatalogos.CrearCatalogo(catalogoModificado);
                            }                       
                            ResetearControles();                           
                            MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (accion == 2)
                            {
                                if (catalogoModificado.EntityKey == null)
                                {
                                    catalogoModificado.HCC_CODIGO = modificarTipoCatalogo.HCC_CODIGO;
                                    catalogoModificado.HC_CATALOGOS_TIPOReference.EntityKey = tipoCatalogo.EntityKey;
                                    modificarTipoCatalogo.HC_CATALOGOS_TIPOReference.EntityKey = tipoCatalogo.EntityKey;                                    
                                    NegCatalogos.GrabarCatalogo(catalogoModificado);
                                }
                                ResetearControles();
                                MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        //protected virtual void HalitarControles(bool datosPrincipales, bool datosSecundarios, bool Modificar, bool Grabar, bool Eliminar, bool Nuevo, bool Cancelar)
        protected virtual void HalitarControles(bool datosPrincipales, bool Grabar, bool Cancelar, bool Salir)
        {
            btnGuardar.Enabled = Grabar;
            btnCancelar.Enabled = Cancelar;
            btnSalir.Enabled = Salir;
            grpDatosPrincipales.Enabled = datosPrincipales;
            txt_Codigo.Enabled = false;
            grpDatosPrincipales.Text = nombreCatalogo;
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        
        #endregion


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                GrabarDatos();                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();                     
        }     

        private void cbx_Estado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //SendKeys.Send("{TAB}");                
            }
        }

        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cbx_Estado.Focus();               
            }            
        }        
    }
}
