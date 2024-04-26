using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;
using His.Parametros;
using System.IO;
using System.Diagnostics;

namespace CuentaPaciente
{
    public partial class frmConsolidarCuentas : Form
    {        
        private List<ESTADOS_CUENTA> listaEstadosCuenta;
        private List<ESTADOS_CUENTA> listaEstadosCuentaMenu;
        int esto=0;
        private int codAtencion;
        private string historia;
        //Atencion atencion = new His.Honorarios.Datos.Atencion();
        DataTable datos = new DataTable();
        public frmConsolidarCuentas(int codAtencion,string hc,string nombre ,string ci,
            string habitacion,string fechIngreso,string  fechaAlta)
        {
            InitializeComponent();
            this.codAtencion = codAtencion;
            this.historia = hc;
            lblNombre.Text = nombre;
            lblCedula.Text = ci;
            lblHc.Text = hc;
            lbAtencion.Text = Convert.ToString(codAtencion);
            lblHabitacion.Text = habitacion;
            lblIngreso.Text = fechIngreso.Substring(0,10);
            lblAlta.Text = fechaAlta.Substring(0,10);
            cargarGrid();
   
            
        }
        public void cargarGrid()
        {
            
            DataTable datos = NegCuentasPacientes.AtencionesConsolidar(historia.Trim());
            ugrdCuenta.DataSource = datos;
            if (datos.Rows.Count == 0)
                ultraTabControl1.Enabled = false;
            else
                ultraTabControl1.Enabled = true;
        }
      

        private void frmEstadoCuenta_Load(object sender, EventArgs e)
            {
                

        }

        private void toolStripbtnNuevo_Click(object sender, EventArgs e)
        {          
            //CargarCuentas();          
        }
      
      

        private void ultraCuentas_InitializeLayout_1(object sender, InitializeLayoutEventArgs e)
        {
            
                
        }

        private void tsbDespacho_Click(object sender, EventArgs e)
        {
           
            if (MessageBox.Show("Esta seguro que desea  cambiar  de  estado  " , "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Int16 contador = 0;
                    ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["NOMBRE"];
                    foreach (var item in ugrdCuenta.Rows)
                    {
                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
                        {
                            NegCuentasPacientes.actualizarCuentasPaciente(Convert.ToInt32(codAtencion), Convert.ToInt32(item.Cells["ATE_CODIGO"].Value));
                            NegCuentasPacientes.actualizarCuenta(Convert.ToInt32(codAtencion), Convert.ToInt32(item.Cells["ATE_CODIGO"].Value));
                                                       
                            contador++;
                        }
                    }
                    if (contador > 0)
                    {
                        MessageBox.Show("Atenciones Consolidadas con exito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    }
                    else
                    {
                        MessageBox.Show("Seleccione una  cuenta para el  cambio de estado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    cargarGrid();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
                  
        }
      

        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

    
        
        
        private void toolStripButton3_Click(object sender, EventArgs e)
        {           

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
     
            ////grid.Rows[currentRow + 1].Cells[grid.ActiveCell].Activate();
            //ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["ATENCION"];
            //string cadenaSql = "";
            //try
            //{
            //    Int16 contador = 0;
            //    foreach (var item in ugrdCuenta.Rows)
            //    {
            //        if ((bool)item.Cells["CHECKTRANS"].Value == true)
            //        {
            //             cadenaSql = Convert.ToString(item.Cells["ATENCION"].Value).Trim()+","+cadenaSql;
            //            contador++;
            //        }
            //    }
            //    if (contador > 0)
            //    {
            //        frmExportarExel frm = new frmExportarExel(cadenaSql, contador);
            //        frm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Seleccione una  cuenta para exportar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
              

            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void uGridCuentas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
            bandUno.Columns["ATE_CODIGO"].Hidden = true;
            bandUno.Columns["NOMBRE"].Width = 250;
            
            if (!ugrdCuenta.DisplayLayout.Bands[0].Columns.Exists("CHECKTRANS"))
            {
                ugrdCuenta.DisplayLayout.Bands[0].Columns.Add("CHECKTRANS", "");
                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DataType = typeof(bool);
                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                ugrdCuenta.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;

            }
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Header.VisiblePosition = 0;
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].CellActivation = Activation.AllowEdit;
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DefaultCellValue = true;

           

        }
      


   
        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["NOMBRE"];
            string cadenaSql = "";
            try
            {
                Int16 contador = 0;
                foreach (var item in ugrdCuenta.Rows)
                {
                    if ((bool)item.Cells["CHECKTRANS"].Value == true)
                    {
                        cadenaSql = Convert.ToString(item.Cells["ATE_CODIGO"].Value).Trim();
                        contador++;
                    }
                }
                if (contador == 1)
                {
                    frmDetalleCuenta frm = new frmDetalleCuenta(Convert.ToInt32(cadenaSql));
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Seleccione un solo  Item para ver  detalle ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ugrdCuenta_InitializeRow(object sender, InitializeRowEventArgs e)
        {

            if (Convert.ToString(e.Row.Cells["ATE_CODIGO"].Value) == Convert.ToString(codAtencion))
            {


                e.Row.Hidden = true;

            }
            if (Convert.ToString(e.Row.Cells["ESTADO"].Value) == "CONSOLIDADA")
            {

           // e.Row.Appearance.BackColor = Color.Red;
            e.Row.Appearance.BackColor2 = Color.White;
            e.Row.Appearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            e.Row.Selected = false;
                //Me.ultraGrid1.Rows(0).Activation = Activation.Disabled  
                    e.Row.Activation=Activation.Disabled;
            }
            //UltraGridBand oBand = e.Layout.Bands[0];


            //foreach (UltraGridColumn oCol in oBand.Columns)
            //{
            //    oCol.CellActivation = Activation.NoEdit;
            //}
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("Esta seguro que desea  consolidar ", "AVISO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Int16 contador = 0;
                    ugrdCuenta.ActiveCell = ugrdCuenta.Rows[0].Cells["NOMBRE"];
                    foreach (var item in ugrdCuenta.Rows)
                    {
                        if ((bool)item.Cells["CHECKTRANS"].Value == true)
                        {
                            NegCuentasPacientes.actualizarCuentasPaciente(Convert.ToInt32(codAtencion), Convert.ToInt32(item.Cells["ATE_CODIGO"].Value));
                            NegCuentasPacientes.actualizarCuenta(Convert.ToInt32(codAtencion), Convert.ToInt32(item.Cells["ATE_CODIGO"].Value));

                            contador++;
                        }
                    }
                    if (contador > 0)
                    {
                        MessageBox.Show("Atenciones Consolidadas con exito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                      //  this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una  cuenta para el  cambio de estado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    cargarGrid();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
