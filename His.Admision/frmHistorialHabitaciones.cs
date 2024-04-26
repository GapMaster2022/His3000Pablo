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
using His.Admision;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;
using His.Parametros;
using System.IO;
using System.Diagnostics;

namespace His.Admision
{
    public partial class frmHistorialHabitaciones : Form
    {
        private string fechaAltaIni;
        private string fechaAltaFin;
        //bool pintarCeldas;
        int contador = 0;
        //byte ranColor;
       // Color[] pColor;
        string valorGridAnt;
        string[] conjuntoAtenciones;
        public frmHistorialHabitaciones()
        {
            //pColor = mColor();
            InitializeComponent();
            cargarGrid();

        }
    

        private void frmHistorialHabitaciones_Load(object sender, EventArgs e)
        {
            //toolStripbtnNuevo.Image = Archivo.ButtonRefresh;
            //pColor = mColor();
            //btnExportar.Image = Archivo.imgOfficeExcel;
        }
        ////private Color[] mColor()
        //{
        //    Color[] color = new Color[]{
        //    Color.Aqua,
        //    Color.AliceBlue,
        //    Color.AntiqueWhite,
        //    Color.CadetBlue,
        //    Color.Aquamarine,
        //    Color.Azure,
        //    Color.BurlyWood,
        //    Color.DarkGray,
        //    Color.Beige,
        //    Color.DarkKhaki,
        //    Color.Bisque,
        //    Color.Gainsboro,
        //    Color.GhostWhite,
        //    Color.Khaki,
        //    Color.Lavender,
        //    Color.LavenderBlush,
        //    Color.LightCyan,
        //    Color.LightGoldenrodYellow,
        //    Color.LightGray,
        //    Color.LightGreen,
        //    Color.LightPink,
        //    Color.LightSalmon,
        //    Color.LightSeaGreen,
        //    Color.LightSkyBlue,
        //    Color.LightSlateGray,
        //    Color.LightSteelBlue,
        //    Color.LightYellow,
        //    Color.MintCream,
        //    Color.MistyRose,
        //    Color.Moccasin,
        //    Color.PaleGreen,
        //    Color.PaleTurquoise,
        //    Color.PaleVioletRed,
        //    Color.PapayaWhip,
        //    Color.PeachPuff,
        //    Color.Peru,
        //    Color.Pink,
        //    Color.Plum,
        //    Color.PowderBlue,
        //    Color.Salmon,
        //    Color.SandyBrown,
        //    Color.SeaGreen,
        //    Color.SeaShell,
        //    Color.Sienna,
        //    Color.Silver,
        //    Color.SkyBlue,
        //    Color.SlateBlue,
        //    Color.SlateGray,
        //    Color.Snow,
        //    Color.BlanchedAlmond,
        //    Color.Blue,
        //    Color.BlueViolet,
        //    Color.Brown,
        //    Color.Chartreuse,
        //    Color.Chocolate,
        //    Color.Coral,
        //    Color.CornflowerBlue,
        //    Color.Cornsilk,
        //    Color.Crimson,
        //    Color.Cyan,
        //    Color.DarkBlue,
        //    Color.DarkCyan,
        //    Color.DarkGoldenrod,
        //    Color.DarkGreen,
        //    Color.DarkMagenta,
        //    Color.DarkOliveGreen,
        //    Color.DarkOrange,
        //    Color.DarkOrchid,
        //    Color.DarkRed,
        //    Color.DarkSalmon,
        //    Color.DarkSeaGreen,
        //    Color.DarkSlateBlue,
        //    Color.DarkSlateGray,
        //    Color.DarkTurquoise,
        //    Color.DarkViolet,
        //    Color.DeepPink,
        //    Color.DeepSkyBlue,
        //    Color.DimGray,
        //    Color.DodgerBlue,
        //    Color.Firebrick,
        //    Color.FloralWhite,
        //    Color.ForestGreen,
        //    Color.Fuchsia,
        //    Color.Gold,
        //    Color.Goldenrod,
        //    Color.Gray,
        //    Color.Green,
        //    Color.GreenYellow,
        //    Color.Honeydew,
        //    Color.HotPink,
        //    Color.IndianRed,
        //    Color.Indigo,
        //    Color.Ivory,
        //    Color.LawnGreen,
        //    Color.LemonChiffon,
        //    Color.LightBlue,
        //    Color.LightCoral,
        //    Color.Lime,
        //    Color.LimeGreen,
        //    Color.Linen,
        //    Color.Magenta,
        //    Color.Maroon,
        //    Color.MediumAquamarine,
        //    Color.MediumBlue,
        //    Color.MediumOrchid,
        //    Color.MediumPurple,
        //    Color.MediumSeaGreen,
        //    Color.MediumSlateBlue,
        //    Color.MediumSpringGreen,
        //    Color.MediumTurquoise,
        //    Color.MediumVioletRed,
        //    Color.MidnightBlue,
        //    Color.NavajoWhite,
        //    Color.Navy,
        //    Color.OldLace,
        //    Color.Olive,
        //    Color.OliveDrab,
        //    Color.Orange,
        //    Color.OrangeRed,
        //    Color.Orchid,
        //    Color.PaleGoldenrod,
        //    Color.Purple,
        //    Color.Red,
        //    Color.RosyBrown,
        //    Color.RoyalBlue,
        //    Color.SaddleBrown,
        //    Color.SpringGreen,
        //    Color.SteelBlue,
        //    Color.Tan,
        //    Color.Teal,
        //    Color.Thistle,
        //    Color.Tomato,
        //    Color.Transparent,
        //    Color.Turquoise,
        //    Color.Violet,
        //    Color.Wheat,
        //    Color.White,
        //    Color.WhiteSmoke,
        //    Color.Yellow,
        //    Color.YellowGreen,
        //    Color.Black
        //    };
        //    return color;
        //}
        public void cargarGrid()
        {
            if (chk_Fecha.Checked)
            {
                //conjuntoAtenciones = new string[100000]; 
                DateTime fechaAltaIni1 = dateTimePicker1.Value;
                DateTime fechaAltaFin1 = dtpFiltroHasta.Value;
                //int count = 0;

                DataTable Datos1 = NegHabitaciones.HistorialHabitacionesFecha(fechaAltaIni1, fechaAltaFin1);
                ugrdHistorial.DataSource = Datos1;
                //var x = (from r in Datos1.AsEnumerable()
                //         select r["ATENCION"]).ToList();
                //foreach (var name in x)
                //{


                //    conjuntoAtenciones[count] = Convert.ToString(name);
                //    count++;
            }

            //}
            else
            {
                DataTable Datos = NegHabitaciones.HistorialHabitaciones(Convert.ToInt64(txtHistoria.Text));
                ugrdHistorial.DataSource = Datos;
            }
            conjuntoAtenciones = null;
           
        }

        private void toolStripbtnNuevo_Click(object sender, EventArgs e)
        {
            cargarGrid();
        }

        private void ugrdHistorial_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            
            UltraGridBand bandUno = ugrdHistorial.DisplayLayout.Bands[0];
            ugrdHistorial.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ugrdHistorial.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ugrdHistorial.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ugrdHistorial.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ugrdHistorial.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            
            bandUno.Columns["PACIENTE"].Width = 200;
            bandUno.Columns["OBSERVACION"].Width = 200;
        }

        private void ugrdHistorial_InitializeRow(object sender, InitializeRowEventArgs e)
        {      

            try
            {
                         
                    if (e.Row.Cells[0].Value.ToString() != valorGridAnt)
                    {
                        //ranColor++;
                        //e.Row.Appearance.BackColor = pColor[ranColor];
                        valorGridAnt = e.Row.Cells[0].Value.ToString();
                    }
            //        else
            //            e.Row.Appearance.BackColor = pColor[ranColor];
              
            }
            catch (Exception err) { MessageBox.Show(err.Message);
            }
        
        }
            
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                CreateExcel(ugrdHistorial, FindSavePath());
            }
            catch (Exception esx)
            {

            }
        }
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }
        private void CreateExcel(UltraGrid datosGrid, String myFilepath)
        {
            try
            {
                if (myFilepath != null)
                {


                    this.ultraGridExcelExporter1.Export(datosGrid, myFilepath);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtHistoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                frm_AyudaPacientes form = new frm_AyudaPacientes();
                form.campoPadre = txtHistoria;
                form.ShowDialog();
                form.Dispose();
                cargarGrid();
            }
            catch (Exception er) { MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void chk_HC_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_HC.Checked)
            {
                chk_Fecha.Checked = false;
                ayudaPacientes.Enabled = true;
                txtHistoria.Text = "";
                txtHistoria.Enabled = true;
            }
            else
            {
                ayudaPacientes.Enabled = false;
                txtHistoria.Text = "";
                txtHistoria.Enabled = false;
            }
        }

        private void chk_Fecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Fecha.Checked)
                chk_HC.Checked = false;
        }
    }
}
