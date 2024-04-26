using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;
using Recursos;
using His.Parametros;
using His.Negocio;


namespace CuentaPaciente
{
    public partial class frmAyudaProcediemtos : Form
    {
       public  int codTransaccion = 0;
         public  string descripcion = "";
        private List<TARIFARIOS_DETALLE> listaProductosDisponibles;
        public List<TARIFARIOS_DETALLE> listaProductosSolicitados;
        public ListView listaV = null;
        double porcentajeFacturar=0;
        public frmAyudaProcediemtos()
        {
            InitializeComponent();
            limpiarHonorariosList();
            porcentajeFacturar = 100;
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {

           
                if (Convert.ToBoolean(this.optCodigo.Checked.ToString()))
                {
                    cargarDetalleTarifario(this.txt_Nombre.Text, "referencia");
                }
                else if (Convert.ToBoolean(this.optDescripcion.Checked.ToString()))
                {
                    cargarDetalleTarifario(this.txt_Nombre.Text, "descripcion");
                }
          

        }
  
        public void cargarDetalleTarifario(string filtro,string tipo)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);

            int cant = 100;

            if (tipo.Equals("referencia"))
            {

               

                var tarifarioDetalleQuery = (from t in contexto.TARIFARIOS_DETALLE
                                             where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO ==1
                                             && t.TAD_REFERENCIA.Contains(filtro)
                                             orderby t.TAD_CODIGO
                                             select new {NUMERO=t.TAD_CODIGO, CODIGO= t.TAD_REFERENCIA,DESCRIPCION= t.TAD_NOMBRE,VALOR_UVR= t.TAD_UVR,ANESTECIA= t.TAD_ANESTESIA }).Take(cant).ToList();
                try
                {
                    // lleno la lista de tarifarios
                    this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (tipo.Equals("descripcion"))
            {
                var tarifarioDetalleQuery = (from t in contexto.TARIFARIOS_DETALLE
                                             where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == 1
                                             && t.TAD_DESCRIPCION.Contains(filtro)
                                             orderby t.TAD_CODIGO
                                             select new {NUMERO=t.TAD_CODIGO, CODIGO = t.TAD_REFERENCIA, DESCRIPCION = t.TAD_NOMBRE, VALOR_UVR = t.TAD_UVR, ANESTECIA = t.TAD_ANESTESIA }).Take(cant).ToList();
                try
                {

                    this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
              
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
          
         
        }

        private void btn_Anadir_Click(object sender, EventArgs e)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            enviardatos(contexto);
        }


        private void enviardatos(HIS3000BDEntities contexto)
        {
            bool uvr = false;
            //if (Convert.ToBoolean(this.optUvr.Checked.ToString()))
            if (Convert.ToBoolean(this.tpuvr.Checked.ToString()))
                uvr = true;

            DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
            int codigoDetalle = Convert.ToInt32(fila.Cells[0].Value.ToString());
            TARIFARIOS_DETALLE tarifarioDetalle = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(
                                                        t => t.TAD_CODIGO == codigoDetalle);


            Int64 codigo = tarifarioDetalle.ESPECIALIDADES_TARIFARIOS.EST_CODIGO;

            //if (rdb_medico.Checked == true)
            
                addHonorarioDetalle(0, codigo,
                    fila.Cells[1].Value.ToString(),
                    fila.Cells[2].Value.ToString(),
                    Convert.ToInt32(txt_Cantidad1.Text.Trim()),
                    Convert.ToDouble(fila.Cells[3].Value),
                    Convert.ToDouble(fila.Cells[4].Value),
                    uvr);
           
        }
        private void addHonorarioDetalle(int codigoDetalle, Int64 codigoEspecialidad, string referencia, string procedimiento, int cantidad, double uvr, double anes, bool conUvr)
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
              
                //DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
                CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
                                                c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == 13
                                                    && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);

                //realizo los calculos para obtener el costo unitario
                double costoU = 0;
                decimal unidadesUvr = 0;
                decimal unidadesAnes = 0;
                decimal costoT;
                if (conUvr)
                {
                    if (convenios != null)
                    {
                        costoU = (Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr);
                    }
                    else
                    {
                        MessageBox.Show("No existe un Convenio", MessageBoxIcon.Information.ToString());
                        return;
                    }
                    unidadesUvr = Convert.ToDecimal(uvr);
                }
                else
                {
                    if (convenios != null)
                    {
                        costoU = (Convert.ToDouble(convenios.CON_VALOR_ANESTESIA) * anes);
                    }
                    else
                    {
                        MessageBox.Show("No existe un Convenio", MessageBoxIcon.Information.ToString());
                        return;
                    }
                    unidadesAnes = Convert.ToDecimal(anes);
                }
                //redondear valores

                unidadesUvr = Decimal.Round(unidadesUvr, 2);
                unidadesAnes = Decimal.Round(unidadesAnes, 2);

                Double tempTotal = costoU * (porcentajeFacturar / 100);
                costoT = Decimal.Round((Decimal.Round(Convert.ToDecimal(tempTotal), 2) * cantidad), 2);
                //Agrego una fila al ListView
                ListViewItem lista = new ListViewItem();
                lista = lvwHonorarios.Items.Add(referencia.ToString(), 0);
                //lista.SubItems.Add(referencia);
                lista.SubItems.Add(procedimiento);
                lista.SubItems.Add(cantidad.ToString());
                //lista.SubItems.Add(unidadesUvr.ToString());
                //lista.SubItems.Add(unidadesAnes.ToString());
                if (convenios != null)
                {
                    //lista.SubItems.Add(convenios.CON_VALOR_UVR.ToString());
                    //lista.SubItems.Add(convenios.CON_VALOR_ANESTESIA.ToString());
                }
                else
                {
                    lista.SubItems.Add("0");
                    lista.SubItems.Add("0");
                    lista.SubItems.Add("0");
                }

                double auxporcentaje = porcentajeFacturar;
                Decimal total = 0;
                if (txt_valor1.Text == "0")
                {
                    
                    lista.SubItems.Add(costoU.ToString());
                    lista.SubItems.Add(auxporcentaje.ToString());
                    lista.SubItems.Add(costoT.ToString());
                }
                else
                {
                    lista.SubItems.Add(txt_valor1.Text.Trim());
                    decimal totalAx = Decimal.Round((Decimal.Round(Convert.ToDecimal(txt_valor1.Text.Trim()), 2) * cantidad), 2);
                    lista.SubItems.Add(auxporcentaje.ToString());
                    lista.SubItems.Add(totalAx.ToString());
                }
                    
                    //lista.SubItems.Add("0");
                    total = (Convert.ToDecimal(txt_total.Text) + costoT);


                    txt_total.Text = total.ToString();
                txt_valor1.Text="0";
            
                  this.lvwHonorarios.Sort();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void limpiarHonorariosList()
        {
            lvwHonorarios.Items.Clear();
            lvwHonorarios.View = View.Details;
            lvwHonorarios.FullRowSelect = true;
            lvwHonorarios.GridLines = true;
            lvwHonorarios.LabelEdit = false;
            lvwHonorarios.HideSelection = false;
            lvwHonorarios.Columns.Clear();
            //lvwHonorarios.Columns.Add("Cod", 40, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Codigo", 50, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Procedimiento", 300, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Cantidad", 60, HorizontalAlignment.Left);
            //lvwHonorarios.Columns.Add("U. Uvr", 70, HorizontalAlignment.Left);
            //lvwHonorarios.Columns.Add("U. Anestesia", 70, HorizontalAlignment.Left);
            //lvwHonorarios.Columns.Add("Valor Uvr", 80, HorizontalAlignment.Left);
            //lvwHonorarios.Columns.Add("Valor Anestesia", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Valor Unitario", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Porcentaje", 70, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Valor Total", 80, HorizontalAlignment.Left);
           
            //lvwHonorarios.Columns.Add("Ayudantia", 80, HorizontalAlignment.Left);
        }

        private void lvwHonorarios_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Crear una instancia de la clase que realizará la comparación
            // indicando la columna en la que se ha pulsado

            ListViewColumnSort oCompare = new ListViewColumnSort(e.Column);
            /*
            ' La columna en la que se ha pulsado
            ' esto sólo es necesario si no se utiliza el contructor parametrizado
            'oCompare.ColumnIndex = e.Column
            */
            /*
            Asignar el orden de clasificación
            */
            if (lvwHonorarios.Sorting == SortOrder.Ascending)
                oCompare.Sorting = SortOrder.Descending;
            else
                oCompare.Sorting = SortOrder.Ascending;
            lvwHonorarios.Sorting = oCompare.Sorting;
            /*
            El tipo de datos de la columna en la que se ha pulsado
            */
            switch (e.Column)
            {
                case 0:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Cadena;
                    break;
                case 1:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Numero;
                    break;
                case 2:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Fecha;
                    break;
            }
            // Asignar la clase que implementa IComparer
            // y que se usará para realizar la comparación de cada elemento
            lvwHonorarios.ListViewItemSorter = oCompare;
        }

        private void frmAyudaProcediemtos_Load(object sender, EventArgs e)
        {
            txt_Nombre.Focus();

            cargarDetalleTarifario("a", "descripcion");
         
          
        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
              descripcion = txt_Observaciones.Text;
            listaV = lvwHonorarios;
            //foreach (ListViewItem items in listaV.Items)
            //{MessageBox.Show((items.SubItems[1].Text));
            //}
            if (txtVale.Text.Trim() == string.Empty)
                codTransaccion = 0;
            else
                codTransaccion = Convert.ToInt32(txtVale.Text);
            this.Close();
        }

        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Convert.ToBoolean(this.optCodigo.Checked.ToString()))
                {
                    cargarDetalleTarifario(this.txt_Nombre.Text, "referencia");
                }
                else if (Convert.ToBoolean(this.optDescripcion.Checked.ToString()))
                {
                    cargarDetalleTarifario(this.txt_Nombre.Text, "descripcion");
                }
            }
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
             
                //txt_valor.Focus();
                tarifariosDetalleGrid.Focus();
            }
        }

        private void lvwHonorarios_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (ListViewItem items in lvwHonorarios.SelectedItems)
                    {
                        lvwHonorarios.Items.Remove(items);
                    }
                    Decimal total = 0;
                    foreach (ListViewItem fila in lvwHonorarios.Items)
                    {
                        total += Convert.ToDecimal(fila.SubItems[5].Text);
                    }
                    txt_total.Text = total.ToString();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void txt_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                enviardatos(contexto);
               
            }
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    txt_Nombre.Focus();
            //}
        }

        private void txt_valor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //   txt_Cantidad.Focus();

            //}
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void tarifariosDetalleGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
             int filas=   tarifariosDetalleGrid.Rows.Count;

             if (filas >1)
             {

                 tarifariosDetalleGrid.CurrentCell = tarifariosDetalleGrid.Rows
                     [tarifariosDetalleGrid.CurrentRow.Index - 1].Cells[0];
                 txt_valor1.Focus();
                 txt_Cantidad1.Text = "1";
             }
             if (filas == 0)
             {
                 txt_Nombre.Focus();
                 txt_Cantidad1.Text = "1";
             }
             if (filas == 1)
             {
                 txt_valor1.Focus();
                 txt_Cantidad1.Text = "1";
             }
    
            
            }
          
        }

        private void tarifariosDetalleGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void lvwHonorarios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_total_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_Observaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void btn_Aceptar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                descripcion = txt_Observaciones.Text;
                listaV = lvwHonorarios;
                if (txtVale.Text.Trim() == string.Empty)
                    codTransaccion = 0;
                else
                    codTransaccion = Convert.ToInt32(txtVale.Text);
                this.Close();
            }

            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void tsmiCienPor_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = true;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiCienPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiSetentaCincoPor_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = true;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiSetentaCincoPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiCincuentaPor_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = true;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiCincuentaPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiVeinteCincoPor_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = true;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiVeinteCincoPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiOtro_Click(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = true;
            porcentajeFacturar = Convert.ToInt16(tsTxtPorcentajeCobrar.Text);
            tsTxtPorcentajeCobrar.Visible = true; 
        }

        private void tsTxtPorcentajeCobrar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (Char)Keys.Back == e.KeyChar))
                e.Handled = true; 
        }

        private void tsTxtPorcentajeCobrar_TextChanged(object sender, EventArgs e)
        {
            if (tsTxtPorcentajeCobrar.Text.Length == 0)
                tsTxtPorcentajeCobrar.Text = "0";

            porcentajeFacturar = Convert.ToInt16(tsTxtPorcentajeCobrar.Text);
        }

        private void txt_valor1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_Cantidad1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                enviardatos(contexto);

            }
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
            //if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            //{
            //    e.Handled = true;
            //    txt_Nombre.Focus();
            //}
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            enviardatos(contexto);
        }

        private void tpuvr_Click(object sender, EventArgs e)
        {
            tpanestesia.Checked = false;
            tpuvr.Checked = true;
        }

        private void tpanestesia_Click(object sender, EventArgs e)
        {
            tpuvr.Checked = false;
            tpanestesia.Checked = true;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
