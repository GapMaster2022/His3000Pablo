using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using System.Data.Objects;
using System.Linq;

namespace TarifariosUI
{
    public partial class frmAdministrarTipoEmpresa : GeneralApp.FrmAdministracion
    {
        public frmAdministrarTipoEmpresa()
        {
            InitializeComponent();
            
        }

        private void frmAdministrarTipoEmpresa_Load(object sender, EventArgs e)
        {
            inicializarControles();
        }

        private void inicializarControles()
        {
            try
            {
                //
                txtCodigo.Text = "";
                txtNombre.Text = ""; 
                //cargo la lista de tipo de empresas
                var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                var tipoEmpresasLista = contexto.TIPO_EMPRESA.Where(t => t.TE_ESTADO == true);
                listaGridview.DataSource = tipoEmpresasLista.ToList();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
        protected override bool CargarItem()
        {
            try
            {
                if (listaGridview.ActiveRow != null)
                { 
                    TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA )listaGridview.ActiveRow.ListObject;
                    txtCodigo.Text = tipoEmpresa.TE_CODIGO.ToString() ;
                    txtNombre.Text = tipoEmpresa.TE_DESCRIPCION;   
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        protected override bool GuardarItem()
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);   
                if (txtCodigo.Text.Trim() != "")
                {
                    Int16 codigo =Convert.ToInt16(txtCodigo.Text.ToString()) ;
                    TIPO_EMPRESA tipoEmpresa = contexto.TIPO_EMPRESA.First(e => e.TE_CODIGO == codigo);
                    tipoEmpresa.TE_DESCRIPCION = txtNombre.Text;
                    contexto.SaveChanges();  
                }
                else
                {
                    Int16 codigo;
                    TIPO_EMPRESA tipoEmpresa = new TIPO_EMPRESA();
                    var nuevoCodigo = (from t in contexto.TIPO_EMPRESA
                                       select t.TE_CODIGO).Max();
                    if (nuevoCodigo == null)
                        codigo = 0;
                    else
                        codigo = Convert.ToInt16(nuevoCodigo);
                    codigo++; 
                    //
                    tipoEmpresa.TE_CODIGO = codigo;
                    tipoEmpresa.TE_DESCRIPCION = txtNombre.Text;
                    tipoEmpresa.TE_ESTADO = true;
                    contexto.AddToTIPO_EMPRESA(tipoEmpresa);
                    contexto.SaveChanges();  
                }
                inicializarControles();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        protected override bool EliminarItem()
        {
            try
            {
                if (listaGridview.ActiveRow != null)
                {
                    DialogResult confirmacion = MessageBox.Show("Esta seguro de elimar el tipo de empresa?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (confirmacion == DialogResult.OK)
                    {
                        HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                        TIPO_EMPRESA tipoEmpresaOri = (TIPO_EMPRESA)listaGridview.ActiveRow.ListObject;
                        TIPO_EMPRESA tipoEmpresa = contexto.TIPO_EMPRESA.FirstOrDefault(t => t.TE_CODIGO == tipoEmpresaOri.TE_CODIGO);
                        contexto.DeleteObject(tipoEmpresa);
                        contexto.SaveChanges();
                    }
                    else {
                        return false; 
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione el tipo de empresa", "", MessageBoxButtons.OK, MessageBoxIcon.Error);       
                }
                MessageBox.Show("El tipo de empresa fue eliminado exitosamente de la base de datos", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                inicializarControles();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true; 
        }

        private void listaGridview_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void listaGridview_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            AdminMenu("actualizar");
            CargarItem();
        }

       

    }
}
