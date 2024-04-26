using His.Entidades;
using His.Entidades.Clases;
using His.General;
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
    public partial class frm_admisnitracionInsumos : Form
    {
        private static string Xatecodigo, Xnumeroatencion;
        private static ATENCIONES atencion = null;
        private static PACIENTES pcte = null;



        public frm_admisnitracionInsumos(string ate_Codigo)
        {
            InitializeComponent();
            ATENCIONES x = NegAtenciones.RecuperarAtencionPorNumero(ate_Codigo);
            Xnumeroatencion = ate_Codigo;
            Xatecodigo = x.ATE_CODIGO.ToString();
            atencion = NegAtenciones.AtencionID(Convert.ToInt32(Xatecodigo));
            pcte = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(Xatecodigo));

            /*frm_AyudaKardex kardex = new frm_AyudaKardex(Xatecodigo, 27, 1);  *///traeMedicamentos
            //kardex.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            List<KardexEnfermeriaMEdicamentos> lista = NegFormulariosHCU.RecuperaMedicamentos(atencion.ATE_CODIGO.ToString(), 27, 1);
            List<KARDEX_INSUMOS> listaKardex = new List<KARDEX_INSUMOS>();
            foreach (var item in lista)
            {
                KARDEX_INSUMOS obj = new KARDEX_INSUMOS();
               
                if (item.Cantidad == 0)
                {
                    obj.PRO_CODIGO = item.id_producto;
                    obj.PRO_DESCRIPCION = item.Producto;
                    obj.CUE_CODIGO = Convert.ToInt64(item.Id);
                    obj.ID_USUARIO = Sesion.codUsuario;
                    obj.ATE_CODIGO = atencion.ATE_CODIGO;
                    obj.fecha = DateTime.Now;
                    obj.Administrado = false;
                    obj.NoAdministrado = true;
                    obj.observacion = "Existe devulucion";

                    listaKardex.Add(obj);

                }
                else
                {
                    for (int i = 0; i < item.Cantidad; i++)
                    {
                        obj.PRO_CODIGO = item.id_producto;
                        obj.PRO_DESCRIPCION = item.Producto;
                        obj.CUE_CODIGO = Convert.ToInt64(item.Id);
                        obj.ID_USUARIO = Sesion.codUsuario;
                        obj.ATE_CODIGO = atencion.ATE_CODIGO;
                        obj.fecha = DateTime.Now;
                        obj.Administrado = true;
                        obj.NoAdministrado = false;
                        obj.observacion = "";

                        listaKardex.Add(obj);

                    }

                }




            }

            if (NegFormulariosHCU.IngresoKardexInsumo(listaKardex))
            {
                MessageBox.Show("Kardex insumo actualizado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }






            CargarGrid();

            var gridBand = grid.DisplayLayout.Bands[0];
            ///formateando tabla en grid
            if (grid.Rows.Count > 0)
            {
                gridBand.Columns["ID_USUARIO"].Hidden = true;
                gridBand.Columns["CUE_CODIGO"].Hidden = true;
                gridBand.Columns["ATE_CODIGO"].Hidden = true;
                gridBand.Columns["CUE_CODIGO"].Hidden = false;
                gridBand.Columns["PRO_DESCRIPCION"].Width = 300;
                ///all columns read only
                for (int i = 0; i < gridBand.Columns.Count; i++)
                {
                    gridBand.Columns[i].CellActivation = Activation.NoEdit;
                }
                //i activate the check column
                gridBand.Columns["Administrado"].CellActivation = Activation.AllowEdit;
                gridBand.Columns["NoAdministrado"].CellActivation = Activation.AllowEdit;
                //gridBand.Columns["observacion"].CellActivation = Activation.AllowEdit;
                gridBand.Columns["fecha"].CellActivation = Activation.AllowEdit;
                gridBand.Columns["observacion"].CellActivation = Activation.AllowEdit;
            }
            //gridBand.Columns["CUE_CODIGO"].Width = 50;
            // gridBand.Columns["id"].Hidden = true;


            // gridBand.Columns["Administrado"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
        }

        private void CargarGrid()
        {
            #region Cargar DatosPcte
            txtHC.Text = pcte.PAC_HISTORIA_CLINICA;
            txtAteCodigo.Text = atencion.ATE_CODIGO.ToString();
            txtPaciente.Text = (pcte.PAC_APELLIDO_PATERNO + " " + pcte.PAC_APELLIDO_MATERNO + " " + pcte.PAC_NOMBRE1 + " " + pcte.PAC_NOMBRE2).Trim();
            txtEdad.Text = Funciones.CalcularEdad(Convert.ToDateTime(pcte.PAC_FECHA_NACIMIENTO)).ToString() + " años";
            MEDICOS med = NegMedicos.medicoPorAtencion(atencion.ATE_CODIGO);
            txtMedico.Text = med.MED_APELLIDO_PATERNO + " " + med.MED_NOMBRE1 + " " + med.MED_NOMBRE2;
            txtSexo.Text = pcte.PAC_GENERO;
            txtDNI.Text = pcte.PAC_IDENTIFICACION;
            txtIngreso.Text = atencion.ATE_FECHA_INGRESO.ToString();
            txtHabitacion.Text = NegHabitaciones.getNombreHabitacion(atencion.ATE_CODIGO);
            #endregion
            #region Cargar Insumos
            //DataTable x = NegDietetica.getDataTable("KardexInsumos_CuentasProdsPorIngresar", Xatecodigo);
            //foreach (DataRow row in x.Rows)
            //{
            //    int xCant = Convert.ToInt32(row["CANTIDAD"].ToString());
            //    object[] list = new object[] {
            //            Xatecodigo,     //@ate_codigo 
            //            row["CUE_CODIGO"].ToString(),    //,<CUE_CODIGO, bigint,> 
            //            row["PRO_CODIGO"].ToString(),    // ,<PRO_CODIGO, nvarchar(15),> 
            //            row["PRO_DESCRIPCION"].ToString()    // ,<PRO_DESCRIPCION, nvarchar(500),>
            //        };
            //    for (int i = 0; i < xCant; i++)
            //    {
            //        NegDietetica.setROW("UpdateKardexInsumo", list, Xatecodigo);
            //    }
            //}

            //DataTable GRIDS = NegDietetica.getDataTable("KardexGrid", Xatecodigo);
            grid.DataSource = NegDietetica.getDataTable("KardexGrid", Xatecodigo);
            #endregion
            //Pinto
            UltraGridBand band = this.grid.DisplayLayout.Bands[0];
            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                if (Convert.ToBoolean(row.Cells["Administrado"].Value))
                    row.Appearance.BackColor = Color.LimeGreen;
                if (Convert.ToBoolean(row.Cells["NoAdministrado"].Value))
                    row.Appearance.BackColor = Color.IndianRed;

            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            frm_AyudaKardex kardex = new frm_AyudaKardex(Xatecodigo, 27, 1);  //traeMedicamentos
            kardex.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            kardex.ShowDialog();


            //if (kardex.cue_codigo != string.Empty)
            //{
            //    object[] list = new object[] {
            //            Xatecodigo,     //@ate_codigo 
            //            kardex.producto,    //,<CUE_CODIGO, bigint,> 
            //            kardex.cue_codigo,    // ,<PRO_CODIGO, nvarchar(15),> 
            //            kardex.medicamento    // ,<PRO_DESCRIPCION, nvarchar(500),>
            //        };
            //    for (int i = 0; i < Convert.ToInt32(kardex.cantidad); i++)
            //    {
            //        NegDietetica.setROW("KardexMedicamentos_ItemsCuentaPaciente", list, Xatecodigo);
            //    }
            //}

            CargarGrid();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            //Caracteristicas de Filtro en la grilla
            //grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            //grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            //grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            //grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            ////dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            //grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;+UltraGridBand bandUno = ultraGridPerfilesSic.DisplayLayout.Bands[0];

            //ultraGridProcedimiento.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow
            // UltraGridBand bandUno = grid.DisplayLayout.Bands[0];
            // grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            // //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            // grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            // grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            // bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            // bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            // bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            // grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            // grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            // grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            // //Caracteristicas de Filtro en la grilla
            // grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            // grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            //grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            // grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            // grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            // //
            // grid.DisplayLayout.UseFixedHeaders = true;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void grid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
        {
            //  Console.WriteLine("before" +  e.Cell.Value.ToString());
        }

        private void grid_AfterCellUpdate(object sender, CellEventArgs e)
        {
            // Console.WriteLine("after" + e.Cell.Value.ToString());
            if (e.Cell.Column.Header.Caption == "observacion" || e.Cell.Column.Header.Caption == "fecha")
            {
                #region Guardando
                try
                {
                    int ladm = 0;
                    int lnoadm = 0;
                    if (Convert.ToBoolean(grid.Rows[grid.ActiveRow.Index].Cells["Administrado"].Value) == true)
                        ladm = 1;
                    else
                        lnoadm = 1;
                    object[] list = new object[]
                    {
                        ladm, lnoadm,
                        (Convert.ToDateTime(grid.Rows[grid.ActiveRow.Index].Cells["fecha"].Text)).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                        Sesion.codUsuario,
                        grid.Rows[grid.ActiveRow.Index].Cells["observacion"].Text
                    };
                    NegDietetica.setROW("UpdateKardexInsumo", list, grid.Rows[grid.ActiveRow.Index].Cells["id"].Text.ToString());
                }
                catch (Exception)
                {
                    //MessageBox.Show("Test");
                    //throw;
                }
                #endregion
            }
        }
        private void grid_CellChange(object sender, CellEventArgs e)
        {
            Console.WriteLine("cell change --> original - " + e.Cell.OriginalValue.ToString() + ",  now -> " + e.Cell.Text.ToString());
            #region CheckMAnejo ADmin con NoAdmin
            bool IsCheck2 = Convert.ToBoolean(grid.Rows[grid.ActiveRow.Index].Cells["NoAdministrado"].Value);
            string str = Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["observacion"].Value);
            //if (IsCheck2)
            //{
            //    if (Convert.ToString(grid.Rows[grid.ActiveRow.Index].Cells["observacion"].Value) != "")
            //    {

            //    }
            //    else
            //        MessageBox.Show("Si Va Marcar Como No Administrado Debe Incluir La Razon En El Campo Observación", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            try
            {
                int ladm = 0;
                int lnoadm = 0;
                bool Xadministrado;
                if (e.Cell.Column.Header.Caption == "Administrado")
                {


                    if (Convert.ToBoolean(e.Cell.Text) == true)
                        Xadministrado = true;
                    else
                        Xadministrado = false;
                    if (Xadministrado)
                    {
                        grid.Rows[grid.ActiveRow.Index].Appearance.BackColor = Color.Green;
                        grid.Rows[grid.ActiveRow.Index].Cells["NoAdministrado"].Value = false;
                        grid.Rows[grid.ActiveRow.Index].Cells["ID_USUARIO"].Value = Sesion.codUsuario.ToString();
                        grid.Rows[grid.ActiveRow.Index].Cells["USUARIO"].Value = Sesion.nomUsuario.ToString();
                        grid.Rows[grid.ActiveRow.Index].Cells["fecha"].Value = DateTime.Now;
                    }
                    else
                    {
                        grid.Rows[grid.ActiveRow.Index].Cells["ID_USUARIO"].Value = 0;
                        grid.Rows[grid.ActiveRow.Index].Cells["USUARIO"].Value = "";
                        grid.Rows[grid.ActiveRow.Index].Cells["fecha"].Value = DateTime.Now;
                        grid.Rows[grid.ActiveRow.Index].Appearance.BackColor = Color.Transparent;
                    }

                    if (Xadministrado)
                        ladm = 1;
                    else
                        lnoadm = 1;

                }
                if (e.Cell.Column.Header.Caption == "NoAdministrado")
                {
                    grid.Rows[grid.ActiveRow.Index].Cells["ID_USUARIO"].Value = Sesion.codUsuario.ToString();
                    grid.Rows[grid.ActiveRow.Index].Cells["USUARIO"].Value = Sesion.nomUsuario.ToString();
                    grid.Rows[grid.ActiveRow.Index].Cells["fecha"].Value = DateTime.Now;

                    if (Convert.ToBoolean(e.Cell.Text) == true)
                        Xadministrado = false;
                    else
                        Xadministrado = true;

                    if (!Xadministrado)
                    {
                        grid.Rows[grid.ActiveRow.Index].Appearance.BackColor = Color.IndianRed;
                        grid.Rows[grid.ActiveRow.Index].Cells["Administrado"].Value = false;
                        grid.Rows[grid.ActiveRow.Index].Cells["ID_USUARIO"].Value = Sesion.codUsuario.ToString();
                        grid.Rows[grid.ActiveRow.Index].Cells["USUARIO"].Value = Sesion.nomUsuario.ToString();
                        grid.Rows[grid.ActiveRow.Index].Cells["fecha"].Value = DateTime.Now;
                    }
                    else
                    {
                        grid.Rows[grid.ActiveRow.Index].Cells["ID_USUARIO"].Value = 0;
                        grid.Rows[grid.ActiveRow.Index].Cells["USUARIO"].Value = "";
                        grid.Rows[grid.ActiveRow.Index].Cells["fecha"].Value = DateTime.Now;
                        grid.Rows[grid.ActiveRow.Index].Appearance.BackColor = Color.Transparent;
                    }
                    if (Xadministrado)
                        ladm = 1;
                    else
                        lnoadm = 1;
                }
                #endregion
                if (e.Cell.Column.Header.Caption == "Administrado" || e.Cell.Column.Header.Caption == "NoAdministrado")
                {
                    #region Guardando
                    try
                    {
                        object[] list = new object[]
                        {
                        ladm, lnoadm,
                        (Convert.ToDateTime(grid.Rows[grid.ActiveRow.Index].Cells["fecha"].Text)).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                        Sesion.codUsuario,
                        grid.Rows[grid.ActiveRow.Index].Cells["observacion"].Text
                        };
                        NegDietetica.setROW("UpdateKardexInsumo", list, grid.Rows[grid.ActiveRow.Index].Cells["id"].Text.ToString());
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Test");
                        //throw;
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
            }
        }

        private void grid_ClickCellButton(object sender, CellEventArgs e)
        {

            //  Console.WriteLine("grid_ClickCellButton  -->" + grid.Rows[grid.ActiveRow.Index].Cells["PRO_DESCRIPCION"].Value.ToString());

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            UltraGridBand band = this.grid.DisplayLayout.Bands[0];
            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                if (!row.IsFilteredOut)
                {
                    row.Cells["Administrado"].Value = true;
                    row.Appearance.BackColor = Color.Green;
                    row.Cells["NoAdministrado"].Value = false;
                    row.Cells["ID_USUARIO"].Value = Sesion.codUsuario.ToString();
                    row.Cells["USUARIO"].Value = Sesion.nomUsuario.ToString();
                    row.Cells["fecha"].Value = DateTime.Now;



                    try
                    {

                        string[] list = new string[]
                        {
                        "1", "0",
                        (Convert.ToDateTime(row.Cells["fecha"].Value)).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                        Sesion.codUsuario.ToString(),
                        row.Cells["observacion"].Value.ToString()
                        };
                        NegDietetica.setROW("UpdateKardexInsumo", list, row.Cells["id"].Text.ToString());
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Ha ocurrido un error, favor comuniquese con sistemas. \n " + ex);
                        //throw;
                    }


                    // Console.WriteLine(row.Cells["PRO_DESCRIPCION"].Value);
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            UltraGridBand band = this.grid.DisplayLayout.Bands[0];
            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                if (!row.IsFilteredOut)
                {
                    row.Cells["Administrado"].Value = false;
                    row.Appearance.BackColor = Color.IndianRed;
                    row.Cells["NoAdministrado"].Value = true;
                    row.Cells["ID_USUARIO"].Value = Sesion.codUsuario.ToString();
                    row.Cells["USUARIO"].Value = Sesion.nomUsuario.ToString();
                    row.Cells["fecha"].Value = DateTime.Now;



                    try
                    {


                        string[] list = new string[]
{
                        "0", "1",
                        (Convert.ToDateTime(row.Cells["fecha"].Value)).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                        Sesion.codUsuario.ToString(),
                        row.Cells["observacion"].Value.ToString()
};
                        NegDietetica.setROW("UpdateKardexInsumo", list, row.Cells["id"].Text.ToString());

                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Test");
                        //throw;
                    }
                }
            }
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                DSkardexmedicamentos ds = new DSkardexmedicamentos();


                DataTable xFechas = NegDietetica.getDataTable("Form022_IFechas", Xatecodigo);
                DataTable xInsumos = NegDietetica.getDataTable("Form022_Insumos", Xatecodigo);

                foreach (DataRow imed in xInsumos.Rows)
                {
                    foreach (DataRow ifec in xFechas.Rows)
                    {
                        string[] values = new string[] {
                        ifec["Fecha"].ToString(),
                        imed["PRO_DESCRIPCION"].ToString(),
                        };

                        DataTable xDosis = NegDietetica.getDataTable("Form022_IRegistros", Xatecodigo, "0", values);

                        string xUDosis = "";
                        int count = 0;
                        foreach (DataRow item in xDosis.Rows)
                        {
                            Console.WriteLine(item["NoAdministrado"].ToString());
                            if (item["NoAdministrado"].ToString() == "True")
                            {
                                xUDosis += "   (" + item["CANTIDAD"].ToString() + ")    "
                                + item["NOMBRES"].ToString().Substring(0, 1)
                                + item["APELLIDOS"].ToString().Substring(0, 1) + "   "
                                + item["DEP_NOMBRE"].ToString().Substring(0, 3) + "   " + item["observacion"].ToString()
                                + Environment.NewLine;
                            }
                            else
                            {
                                xUDosis += "    " + item["CANTIDAD"].ToString()
                                + "     " + item["NOMBRES"].ToString().Substring(0, 1)
                                + item["APELLIDOS"].ToString().Substring(0, 1) + "    "
                                + item["DEP_NOMBRE"].ToString().Substring(0, 3) + Environment.NewLine;
                            }

                            count++;
                        }
                        for (int i = count; i < 4; i++)
                        {
                            xUDosis += "." + Environment.NewLine;
                        }
                        DataRow dr = ds.Tables["dtKardexMED"].NewRow();
                        dr["EMP_NOMBRE"] = Sesion.nomEmpresa;
                        dr["PAC_NOMBRE1"] = pcte.PAC_NOMBRE1;
                        dr["PAC_NOMBRE2"] = pcte.PAC_NOMBRE2;
                        dr["PAC_APELLIDO_PATERNO"] = pcte.PAC_APELLIDO_PATERNO;
                        dr["PAC_APELLIDO_MATERNO"] = pcte.PAC_APELLIDO_MATERNO;
                        dr["PAC_GENERO"] = pcte.PAC_GENERO;
                        dr["PAC_HISTORIA_CLINICA"] = pcte.PAC_HISTORIA_CLINICA;
                        dr["MEDICAMENTO"] = imed["PRO_DESCRIPCION"].ToString();
                        dr["DIAyMES"] = "       " + ifec["Fecha"].ToString().Substring(0, 5) + "     CANT  INI  FUN ";


                        dr["HORA"] = xUDosis;
                        //dr["INI"] = (row["NOMBRES"].ToString()).Substring(0,1) + (row["APELLIDOS"].ToString()).Substring(0, 1);
                        //dr["FUNCION"] = (row["DEP_NOMBRE"].ToString()).Substring(0, 3);
                        // string strdosis = string.Format("{1} {2} {3}", Environment.NewLine, row["Hora"].ToString(), (row["NOMBRES"].ToString()).Substring(0, 1) + (row["APELLIDOS"].ToString()).Substring(0, 1), (row["DEP_NOMBRE"].ToString()).Substring(0, 3));
                        //  dr["HORA"] = row["Hora"].ToString();

                        ds.Tables["dtKardexMED"].Rows.Add(dr);

                    }
                }

                ATENCIONES atencion = NegAtenciones.AtencionID(Convert.ToInt64(Xatecodigo));
                DataRow cabecera;
                cabecera = ds.Tables["Encabezado"].NewRow();
                cabecera["institucion"] = His.Entidades.Clases.Sesion.nomEmpresa;
                cabecera["unicodigo"] = "";
                cabecera["establecimiento"] = His.Entidades.Clases.Sesion.nomEmpresa;
                cabecera["hc"] = pcte.PAC_IDENTIFICACION;
                cabecera["narchivo"] = pcte.PAC_HISTORIA_CLINICA + "-" + atencion.ATE_NUMERO_ATENCION;
                cabecera["papellido"] = pcte.PAC_APELLIDO_PATERNO;
                cabecera["sapellido"] = pcte.PAC_APELLIDO_MATERNO;
                cabecera["pnombre"] = pcte.PAC_NOMBRE1;
                cabecera["snombre"] = pcte.PAC_NOMBRE2;
                cabecera["fnacimiento"] = pcte.PAC_FECHA_NACIMIENTO;
                if (pcte.PAC_GENERO == "M")
                    cabecera["sexo"] = "HOMBRE";
                else
                    cabecera["sexo"] = "MUJER";
                cabecera["edad"] = NegUtilitarios.EdadCalculada(Convert.ToDateTime(pcte.PAC_FECHA_NACIMIENTO), (DateTime)atencion.ATE_FECHA_INGRESO);
                ds.Tables["Encabezado"].Rows.Add(cabecera);

                His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "FORM022i", ds);
                myreport.Show();
            }
            else
                MessageBox.Show("No hay datos para generar reporte", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void frm_admisnitracionInsumos_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
