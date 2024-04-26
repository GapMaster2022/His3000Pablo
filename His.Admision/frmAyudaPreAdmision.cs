using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Admision
{
    public partial class frmAyudaPreAdmision : Form
    {
        public string iden = "";
        public Int64 codigo = 0;
        public frmAyudaPreAdmision()
        {
            InitializeComponent();
        }

        private void frmAyudaPreAdmision_Load(object sender, EventArgs e)
        {
            cargarPreAdmision();
        }
        //private void cargarPreAdmision()
        //{
        //    List<DtoPreAdmision> lista = new List<DtoPreAdmision>();
        //    lista = NegPreadmision.listarPreadmision();
        //    grid.DataSource = lista
        //    .Select(
        //    p => new
        //    {
        //        CODIGO = p.PRE_CODIGO,
        //        NOMBRE = p.PRE_APELLIDO1 + " " + p.PRE_APELLIDO2 + " " + p.PRE_NOMBRE1 + " " + p.PRE_NOMBRE2,
        //        ID = p.PRE_IDENTIFICACION,
        //        F_INGRESO = p.PRE_FECHA,
        //        TIPO_TRATAMIENTO =
        //    }).ToList();
        //    //para los cambios que solicitan en la pasteur
        //    grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
        //    grid.DisplayLayout.Bands[0].Columns["F_INGRESO"].Width = 150;
        //    grid.DisplayLayout.Bands[0].Columns["TIA_CODIGO"].Hidden = true;
        //}
        private void cargarPreAdmision()
        {
            List<DtoPreAdmision> lista = new List<DtoPreAdmision>();
            lista = NegPreadmision.listarPreadmision();
            grid.DataSource = lista;
            //.Select(
            //p => new
            //{
            //    CODIGO = p.PRE_CODIGO,
            //    NOMBRE = p.PRE_APELLIDO1 + " " + p.PRE_APELLIDO2 + " " + p.PRE_NOMBRE1 + " " + p.PRE_NOMBRE2,
            //    ID = p.PRE_IDENTIFICACION,
            //    F_INGRESO = p.PRE_FECHA,
            //    TIPO_TRATAMIENTO = 
            //}).ToList();
            //para los cambios que solicitan en la pasteur
            grid.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 350;
            grid.DisplayLayout.Bands[0].Columns["F_INGRESO"].Width = 150;
            grid.DisplayLayout.Bands[0].Columns["TIA_CODIGO"].Hidden = true;
            grid.DisplayLayout.Bands[0].Columns["TIPO_TRATAMIENTO"].Hidden = true;
        }
        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
            this.grid.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
        }

        private void grid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if(grid.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = grid.ActiveRow;
                iden = fila.Cells["ID"].Value.ToString();
                this.Close();
            }
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                UltraGridRow fila = grid.ActiveRow;
                if (grid.Selected.Rows.Count > 0)
                {
                    if (MessageBox.Show("¿Si continua deshabilitara el Pre-Ingreso de: " + fila.Cells[1].Value.ToString() + "?", "HIS3000",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        try
                        {
                            if (!NegPreadmision.cambioEstadoPreadmsion(Convert.ToInt64(fila.Cells["CODIGO"].Value.ToString())))
                                MessageBox.Show("No se ha podido deshabilitar la preadmision", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                MessageBox.Show("Deshabilitado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cargarPreAdmision();
                            }
                            //NegPreadmision.EliminarPreadmision(fila.Cells["CODIGO"].Value.ToString());
                            //MessageBox.Show("Eliminado Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //cargarPreAdmision();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void grid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            UltraGridBand band = this.grid.DisplayLayout.Bands[0];
            foreach (UltraGridRow item in band.GetRowEnumerator(GridRowType.DataRow))
            {
                //DateTime valo = Convert.ToDateTime(item.Cells[3].Value.ToString());
                //DateTime valo1 = valo.AddMinutes(20);
                //DateTime valo2 = valo1.AddMinutes(20);

                //if (Convert.ToInt32(item.Cells[5].Value.ToString())==5)
                //{
                //    item.Appearance.BackColor = Color.FromArgb(244, 83, 87);//Rojo
                //}
                //else
                //{
                //    item.Appearance.BackColor = Color.FromArgb(176, 245, 164); // verde
                //}
                //else if (ValidaHora1(valo1))
                //{
                //    item.Appearance.BackColor = Color.FromArgb(255, 159, 64);//naranja
                //}
                //else
                //{
                //    item.Appearance.BackColor = Color.FromArgb(155, 202, 174); // verde
                //}

                //if (ValidaHora2(valo2))
                //{
                //    item.Appearance.BackColor = Color.FromArgb(247, 111, 114);//Rojo
                //}
            }
        }
        public static bool ValidaHora1(DateTime v1)
        {
            if (v1 < DateTime.Now)
            {
                return true;
            }
            else if (v1.Hour > DateTime.Now.Hour)
            {
                return true;
            }
            else if (v1.Minute < DateTime.Now.Minute)
            {
                return true;
            }
            else
                return false;
        }
        public static bool ValidaHora2(DateTime v1)
        {
            if (v1 < DateTime.Now)
            {
                return true;
            }
            else if (v1.Hour > DateTime.Now.Hour)
            {
                return true;
            }
            else if (v1.Minute < DateTime.Now.Minute)
            {
                return true;
            }
            else
                return false;
        }
    }
}
