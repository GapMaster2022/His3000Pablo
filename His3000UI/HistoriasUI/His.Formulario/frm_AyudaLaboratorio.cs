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

namespace His.Formulario
{
    public partial class frm_AyudaLaboratorio : Form
    {
        public Int64 tipo;
        public string[] array = new string[20];
        public List<DtoLaboratorioVarios> parte1 = new List<DtoLaboratorioVarios>();
        public List<DtoLaboratorioVarios> parte2 = new List<DtoLaboratorioVarios>();
        public List<DtoLaboratorioVarios> parteMas = new List<DtoLaboratorioVarios>();
        public List<DtoLaboratorioVarios> parteRes1 = new List<DtoLaboratorioVarios>();
        public List<DtoLaboratorioVarios> parteRes2 = new List<DtoLaboratorioVarios>();
        public frm_AyudaLaboratorio()
        {
            InitializeComponent();
        }
        public frm_AyudaLaboratorio(int _tipo)
        {
            InitializeComponent();
            this.tipo = _tipo;
            //cargarGrid();
        }
        public void cargarGrid()
        {
            DataTable cosulta = new DataTable();
            int mitad = 0;
            switch (tipo)
            {
                case 1:
                    cosulta = NegLaboratorio.listarProductoDt(6);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        parte1.Add(lab);
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        parte2.Add(lab);
                    }
                    ultraGridP1.DataSource = parte1;
                    ultraGridP2.DataSource = parte2;
                    break;
                case 2:
                    cosulta = NegLaboratorio.listarProductoDt(7);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        parte1.Add(lab);
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        parte2.Add(lab);
                    }
                    ultraGridP1.DataSource = parte1;
                    ultraGridP2.DataSource = parte2;
                    break;
                case 3:
                    cosulta = NegLaboratorio.listarProductoDt(8);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        parte1.Add(lab);
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        parte2.Add(lab);
                    }
                    ultraGridP1.DataSource = parte1;
                    ultraGridP2.DataSource = parte2;
                    break;
                case 4:
                    cosulta = NegLaboratorio.listarProductoDt(9);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        parte1.Add(lab);
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        parte2.Add(lab);
                    }
                    ultraGridP1.DataSource = parte1;
                    ultraGridP2.DataSource = parte2;
                    break;
                case 5:
                    cosulta = NegLaboratorio.listarProductoDt(10);
                    for (int i = 0; i < cosulta.Rows.Count; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        parte1.Add(lab);
                    }
                    ultraGridP1.DataSource = parte1;
                    break;
                case 6:
                    cosulta = NegLaboratorio.listarProductoDt(11);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        parte1.Add(lab);
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        parte2.Add(lab);
                    }
                    ultraGridP1.DataSource = parte1;
                    ultraGridP2.DataSource = parte2;
                    break;
                case 7:
                    cosulta = NegLaboratorio.listarProductoDt(12);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        parte1.Add(lab);
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                break;
                            }
                            else
                            {
                                lab.V = false;
                            }
                        }
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        parte2.Add(lab);
                    }
                    ultraGridP1.DataSource = parte1;
                    ultraGridP2.DataSource = parte2;
                    break;
                default:
                    break;
            }

        }
        public void CargagridRes()
        {
            DataTable cosulta = new DataTable();
            int mitad = 0;
            switch (tipo)
            {
                case 1:
                    cosulta = NegLaboratorio.listarProductoDt(6);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes1.Add(lab);
                                break;
                            }
                        }
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes2.Add(lab);
                                break;
                            }
                        }
                    }
                    break;
                case 2:
                    cosulta = NegLaboratorio.listarProductoDt(7);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes1.Add(lab);
                                break;
                            }
                        }
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes2.Add(lab);
                                break;
                            }
                        }
                    }
                    break;
                case 3:
                    cosulta = NegLaboratorio.listarProductoDt(8);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes1.Add(lab);
                                break;
                            }
                        }
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes2.Add(lab);
                                break;
                            }
                        }
                    }
                    break;
                case 4:
                    cosulta = NegLaboratorio.listarProductoDt(9);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes1.Add(lab);
                                break;
                            }
                        }
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes2.Add(lab);
                                break;
                            }
                        }
                    }
                    break;
                case 5:
                    cosulta = NegLaboratorio.listarProductoDt(10);
                    for (int i = 0; i < cosulta.Rows.Count; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes1.Add(lab);
                                break;
                            }
                        }
                    }
                    ultraGridP1.DataSource = parte1;
                    break;
                case 6:
                    cosulta = NegLaboratorio.listarProductoDt(11);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes1.Add(lab);
                                break;
                            }
                        }
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes2.Add(lab);
                                break;
                            }
                        }
                    }
                    break;
                case 7:
                    cosulta = NegLaboratorio.listarProductoDt(12);
                    mitad = cosulta.Rows.Count / 2;
                    for (int i = 0; i < mitad; i++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[i][3].ToString();
                        lab.EXAMEN = cosulta.Rows[i][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[i][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes1.Add(lab);
                                break;
                            }
                        }
                    }
                    for (int y = mitad; y < cosulta.Rows.Count; y++)
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = cosulta.Rows[y][3].ToString();
                        lab.EXAMEN = cosulta.Rows[y][4].ToString();
                        foreach (var item in parteMas)
                        {
                            if (cosulta.Rows[y][3].ToString() == item.CODIGO)
                            {
                                lab.V = true;
                                parteRes2.Add(lab);
                                break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private void ultraGridP1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void ultraGridP2_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            guardaInformacion();
        }
        private void guardaInformacion()
        {
            parte1 = new List<DtoLaboratorioVarios>();
            parte2 = new List<DtoLaboratorioVarios>();
            foreach (UltraGridRow item in ultraGridP1.Rows)
            {
                if (Convert.ToBoolean(item.Cells["V"].Value.ToString()) == true)
                {
                    try
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = item.Cells["CODIGO"].Value.ToString();
                        lab.V = true;
                        lab.EXAMEN = item.Cells["EXAMEN"].Value.ToString();
                        parte1.Add(lab);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //throw;
                    }
                }
            }
            foreach (UltraGridRow item in ultraGridP2.Rows)
            {
                if (Convert.ToBoolean(item.Cells["V"].Value.ToString()) == true)
                {
                    try
                    {
                        DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                        lab.CODIGO = item.Cells["CODIGO"].Value.ToString();
                        lab.V = true;
                        lab.EXAMEN = item.Cells["EXAMEN"].Value.ToString();
                        parte2.Add(lab);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //throw;
                    }
                }
            }
            this.Close();
        }
        private void limpiarGrid()
        {
            parte1 = new List<DtoLaboratorioVarios>();
            parte2 = new List<DtoLaboratorioVarios>();
            parteMas = new List<DtoLaboratorioVarios>();
            parteRes1 = new List<DtoLaboratorioVarios>();
            parteRes2 = new List<DtoLaboratorioVarios>();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ultraGridP1.DataSource = null;
            ultraGridP2.DataSource = null;
            limpiarGrid();
            cargarGrid();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            guardaInformacion();
        }

        private void ultraGridP1_CellChange(object sender, CellEventArgs e)
        {
            foreach (var item in ultraGridP1.Rows)
            {
                if (item.Cells["CODIGO"].Value.ToString() == e.Cell.Row.Cells["CODIGO"].Value.ToString())
                {
                    foreach (var list in parteRes1)
                    {
                        if (item.Cells["CODIGO"].Value.ToString() == list.CODIGO)
                        {
                            DtoLaboratorioVarios lab1 = new DtoLaboratorioVarios();
                            lab1.CODIGO = item.Cells["CODIGO"].Value.ToString();
                            lab1.V = true;
                            lab1.EXAMEN = item.Cells["EXAMEN"].Value.ToString();
                            parteRes1.RemoveAll(r => r.CODIGO ==list.CODIGO );
                            e.Cell.Row.Appearance.ForeColor = Color.Black;
                            return;
                        }
                    }
                    DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                    lab.CODIGO = item.Cells["CODIGO"].Value.ToString();
                    lab.V = true;
                    lab.EXAMEN = item.Cells["EXAMEN"].Value.ToString();
                    parteRes1.Add(lab);
                    e.Cell.Row.Appearance.ForeColor = Color.Red;
                }
            }
            //foreach (var item in ultraGridP1.Rows)
            //{
            //    if (Convert.ToBoolean(item.Cells["V"].Value.ToString()) == Convert.ToBoolean(e.Cell.Row.Cells["V"].Value.ToString()))
            //    {
            //        if (!Convert.ToBoolean(item.Cells["V"].Value.ToString()))
            //            e.Cell.Row.Appearance.ForeColor = Color.Red;
            //        else
            //            e.Cell.Row.Appearance.ForeColor = Color.Black;
            //    }
            //}
        }

        private void ultraGridP2_CellChange(object sender, CellEventArgs e)
        {
            foreach (var item in ultraGridP2.Rows)
            {
                if (item.Cells["CODIGO"].Value.ToString() == e.Cell.Row.Cells["CODIGO"].Value.ToString())
                {
                    foreach (var list in parteRes2)
                    {
                        if (item.Cells["CODIGO"].Value.ToString() == list.CODIGO)
                        {
                            DtoLaboratorioVarios lab1 = new DtoLaboratorioVarios();
                            lab1.CODIGO = item.Cells["CODIGO"].Value.ToString();
                            lab1.V = true;
                            lab1.EXAMEN = item.Cells["EXAMEN"].Value.ToString();
                            parteRes2.RemoveAll(r => r.CODIGO == list.CODIGO);
                            e.Cell.Row.Appearance.ForeColor = Color.Black;
                            return;
                        }
                    }
                    DtoLaboratorioVarios lab = new DtoLaboratorioVarios();
                    lab.CODIGO = item.Cells["CODIGO"].Value.ToString();
                    lab.V = true;
                    lab.EXAMEN = item.Cells["EXAMEN"].Value.ToString();
                    parteRes2.Add(lab);
                    e.Cell.Row.Appearance.ForeColor = Color.Red;
                }
            }
            //foreach (var item in ultraGridP2.Rows)
            //{
            //    if (Convert.ToBoolean(item.Cells["V"].Value.ToString()) == Convert.ToBoolean(e.Cell.Row.Cells["V"].Value.ToString()))
            //    {
            //        if (!Convert.ToBoolean(item.Cells["V"].Value.ToString()))
            //            e.Cell.Row.Appearance.ForeColor = Color.Red;
            //        else
            //            e.Cell.Row.Appearance.ForeColor = Color.Black;
            //    }
            //}
        }

        private void frm_AyudaLaboratorio_Load(object sender, EventArgs e)
        {
            cargarGrid();
            CargagridRes();
        }
    }
}
