using His.Entidades;
using His.Entidades.Clases;
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

namespace His.Honorarios
{
    public partial class frm_AsientoHonMed : Form
    {

        private bool xSelection = true;
        public frm_AsientoHonMed()
        {
            InitializeComponent();
            dtpDesde.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpHasta.Value = DateTime.Now;
            dtpDesde2.Value = Convert.ToDateTime(String.Format("{0:g}", (DateTime.Now.Year).ToString() + "/" + DateTime.Now.Month + "/01"));
            dtpHasta2.Value = DateTime.Now;
            refrescar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refrescar()
        {
            if (!ckbFuera.Checked && !ckbSeguros.Checked)
            {
                if (His.Entidades.Clases.Sesion.codDepartamento == 3)
                    grid.DataSource = NegDietetica.getDataTable("HorariosMedicosAsientos", dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"), dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), null, "1");
                else
                    grid.DataSource = NegDietetica.getDataTable("HorariosMedicosAsientos", dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"), dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"), null, "0");

            }

            else
            {
                if (His.Entidades.Clases.Sesion.codDepartamento == 3)
                    grid.DataSource = NegDietetica.HonorariosAsientoContable(Convert.ToDateTime(dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00")), Convert.ToDateTime(dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59")), ckbFuera.Checked, ckbSeguros.Checked, 1);
                else
                    grid.DataSource = NegDietetica.HonorariosAsientoContable(Convert.ToDateTime(dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00")), Convert.ToDateTime(dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59")), ckbFuera.Checked, ckbSeguros.Checked, 0);
            }


            var gridBand = grid.DisplayLayout.Bands[0];


            gridBand.Columns["HC"].Hidden = true;
            gridBand.Columns["ATE_CODIGO"].Hidden = true;
            gridBand.Columns["MED_CODIGO"].Hidden = true;
            gridBand.Columns["HOM_CODIGO"].Hidden = true;
            gridBand.Columns["AUTORIZACION"].Hidden = true;
            gridBand.Columns["CADUCIDAD"].Hidden = true;
            gridBand.Columns["COD_RET"].Hidden = true;
            gridBand.Columns["CTA_RETENCION"].Hidden = true;
            gridBand.Columns["HON_X_FUERA"].Hidden = false;
            gridBand.Columns["ATE_FACTURA_PACIENTE"].Hidden = false;
            gridBand.Columns["CTA_HONORARIOS"].Hidden = true;
            gridBand.Columns["CTA_MEDICO"].Hidden = true;
            gridBand.Columns["CTA_APORTE"].Hidden = true;
            gridBand.Columns["CTA_COMISION"].Hidden = true;
            gridBand.Columns["GENERADO"].Hidden = true;


            gridBand.Columns["PACIENTE"].Width = 300;
            gridBand.Columns["MEDICO"].Width = 300;
            gridBand.Columns["FECHA_FACTURA_MED"].Width = 90;
            gridBand.Columns["FACTURA"].Width = 150;
            gridBand.Columns["COMISION"].Width = 60;
            gridBand.Columns["APORTE"].Width = 60;
            gridBand.Columns["RETENCION"].Width = 60;
            gridBand.Columns["A_PAGAR"].Width = 60;


            ///all columns read only
            for (int i = 0; i < gridBand.Columns.Count; i++)
            {
                gridBand.Columns[i].CellActivation = Activation.NoEdit;
            }
            //i activate the check column
            //gridBand.Columns["Seleccion"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            //gridBand.Columns["Seleccion"].CellActivation = Activation.AllowEdit;
            //gridBand.Columns["Seleccion"].Width = 50;

            grid2.DataSource = NegDietetica.getDataTable("HorariosMedicosAsientos2", dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"), dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"));

            gridBand.Columns["Seleccion"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            gridBand.Columns["Seleccion"].CellActivation = Activation.AllowEdit;
            gridBand.Columns["Seleccion"].Width = 50;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            refrescar();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            refrescar();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea generar los asientos contables?" + "\n [Una vez generado, no hay reverso.]", "His3000", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                UltraGridBand band = this.grid.DisplayLayout.Bands[0];
                foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
                {
                    if (Convert.ToBoolean(row.Cells["Seleccion"].Text))
                    {

                        int AUXILIARLINEA = 1;
                        string numing, tipord, fecha, linea, codcue, columna, valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica, forpag, for_descripcion;
                        DataTable cgcodconcli = NegDietetica.getDataTable("getCgCueCliente", row.Cells["MED_CODIGO"].Value.ToString());
                        try
                        {
                            cuecliente = cgcodconcli.Rows[0][0].ToString();
                        }
                        catch (Exception ex)
                        {
                            cuecliente = "0";
                        }



                        ///GERMANIZA:: "SI TIENES EN 0 NO TE DEBE PERMITIR GENERAR EL ASIENTO"
                        if (cuecliente == "0")
                        {
                            MessageBox.Show("No existe el codigo del medico " + row.Cells["MEDICO"].Value.ToString() + " creado en Contabilidad, no es posible generar el asiento de la factura " + row.Cells["FACTURA"].Value.ToString(), "His3000");
                            return;
                        }

                        if (row.Cells["FACTURA"].Value.ToString().Trim() == string.Empty)
                        {
                            if (!Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                            {
                                MessageBox.Show("Es necesario registre el numero de factura del medico " + row.Cells["MEDICO"].Value.ToString() + " , no es posible generar el asiento.", "His3000");
                                return;
                            }

                        }

                        DataTable dtC = NegDietetica.getDataTable("EscCodigo_byAteCodigo", row.Cells["ATE_CODIGO"].Value.ToString());
                        string xauxx = dtC.Rows[0]["ESC_CODIGO"].ToString();
                        if (!Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                        {
                            if (xauxx.Trim() != "6")
                            {

                                //if (row.Cells["FACTURA"].Value.ToString().Trim() == string.Empty || row.Cells["forpag"].Value.ToString()==)
                                MessageBox.Show("Es necesario tener el Nº facturada, La cuenta para que se generen los asientos, no es posible generar el asiento.", "His3000");
                                return;
                            }
                        }


                        #region ASIENTO AGRUPADO
                        //PROCESO PARA GENERAR UN ASIENTO CUANDO EL MEDICO TIENE VARIAS FORMAS DE PAGO CON EL MISMO #FACTURA
                        //DataTable Agrupados = new DataTable();  //ME DEVUELVE HONORARIOS QUE MANEJAN EL MISMO NUMERO DE FACTURA
                        //Agrupados = NegHonorariosMedicos.Agrupados(Convert.ToInt64(row.Cells["ATE_CODIGO"].Value.ToString()), row.Cells["FACTURA"].Value.ToString());

                        //if(Agrupados.Rows.Count > 1)
                        //{
                        //    DataTable dt2 = NegDietetica.getDataTable("getNumeroControlAsiento", row.Cells["HOM_CODIGO"].Value.ToString());
                        //    numing = dt2.Rows[0][0].ToString();
                        //    foreach (DataRow item in Agrupados.Rows)
                        //    {
                        //        if(item["HOM_CODIGO"].ToString() == row.Cells["HOM_CODIGO"].Value.ToString())
                        //        {
                        //            row.Cells["Seleccion"].Value = false;

                        //            //GENERO EL ASIENTO AGRUPADO
                        //            tipord = "8";
                        //            fecha = (Convert.ToDateTime(item["HOM_FACTURA_FECHA"].ToString())).ToString("yyyy'-'MM'-'dd'T'00':'00':'00");
                        //            codlocal = "1";
                        //            codzona = "1";
                        //            estado = "N";

                        //            forpag = ""; for_descripcion = "";
                        //            forpag = item["forpag"].ToString();
                        //            for_descripcion = item["despag"].ToString(); 
                        //            fecaux = fecha.Substring(0, 10).Replace("-", "");
                        //            //HONORARIOS_MEDICOS honorarioaBorrar = new HONORARIOS_MEDICOS();
                        //            //honorarioaBorrar = new NegHonorariosMedicos().RecuperaHonorariosMedicosID(Convert.ToInt64(row.Cells["HOM_CODIGO"].Value.ToString()));


                        //            //DataTable Tabla = new DataTable();
                        //            //Tabla = NegHonorariosMedicos.HMDatosAdiciones(Convert.ToInt32(row.Cells["HOM_CODIGO"].Value.ToString()));
                        //            //if (row.Cells["FACTURA"].Value.ToString().Trim() == "")
                        //            //    nocomp = honorarioaBorrar.HOM_VALE;
                        //            //else
                        //            //    nocomp = row.Cells["FACTURA"].Value.ToString();
                        //            //numche = "0";


                        //            ////tipmov = "FCS";/////////????????????????
                        //            //codcentrocosto = "0";
                        //            //codrubro = "0";
                        //            //codactividad = "0";
                        //            //autorizacion = row.Cells["AUTORIZACION"].Value.ToString();
                        //            //fechacad = row.Cells["CADUCIDAD"].Value.ToString();
                        //            //observa = "PCTE." + row.Cells["PACIENTE"].Value.ToString();
                        //            //fecpago = (Convert.ToDateTime(row.Cells["FECHA_FACTURA_MED"].Value)).ToString("yyyy'-'MM'-'dd'T'00':'00':'00");
                        //            //if (!Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                        //            //    codretencion = row.Cells["COD_RET"].Value.ToString(); ///TOMO DEL EXISTENTE, ATADO AL MEDICO
                        //            //else
                        //            //    codretencion = "0";
                        //            //cajachica = "3";

                        //            ////asiento de VALOR NETO
                        //            //if (!Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                        //            //{
                        //            //    tipmov = "FSS";
                        //            //    linea = AUXILIARLINEA.ToString();
                        //            //    codcue = row.Cells["CTA_HONORARIOS"].Value.ToString();
                        //            //    columna = "DEBE";
                        //            //    valor = (Convert.ToDouble(row.Cells["VALOR"].Value)).ToString().Replace(",", ".");
                        //            //    comentario = "HONORARIOS MEDICOS";
                        //            //    //if (Convert.ToDouble(row.Cells["VALOR"].Value) > 0)
                        //            //    //{
                        //            //    string[] x1 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica, forpag, for_descripcion };
                        //            //    NegDietetica.setROW("AsientoContable", x1);
                        //            //    AUXILIARLINEA++;
                        //            //    //}
                        //            //}
                        //            //else
                        //            //{
                        //            //    string cuenta = NegMedicos.CuentaContableSic(row.Cells["forpag"].Value.ToString());
                        //            //    tipmov = "0";
                        //            //    linea = AUXILIARLINEA.ToString();
                        //            //    codcue = cuenta;
                        //            //    columna = "DEBE";
                        //            //    valor = (Convert.ToDouble(row.Cells["VALOR"].Value)).ToString().Replace(",", ".");
                        //            //    if (row.Cells["VALOR_CUBIERTO"].Value.ToString() != "")
                        //            //        valor = (Convert.ToDouble(valor) - Convert.ToDouble(row.Cells["VALOR_CUBIERTO"].Value)).ToString();
                        //            //    comentario = for_descripcion;
                        //            //    //if (Convert.ToDouble(row.Cells["VALOR"].Value) > 0)
                        //            //    //{
                        //            //    string[] x1 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica, forpag, for_descripcion };
                        //            //    NegDietetica.setROW("AsientoContable", x1);
                        //            //    AUXILIARLINEA++;
                        //            //    //}
                        //            //}

                        //            ////asiento de APORTE
                        //            //linea = AUXILIARLINEA.ToString();
                        //            //tipmov = "0";
                        //            //codcue = row.Cells["CTA_APORTE"].Value.ToString();
                        //            //columna = "HABER";
                        //            //valor = (Convert.ToDouble(row.Cells["APORTE"].Value)).ToString().Replace(",", ".");
                        //            //if (valor.Trim() == "")
                        //            //    valor = "0";
                        //            //comentario = "APORTE";

                        //            //if (Convert.ToDouble(row.Cells["APORTE"].Value) > 0)
                        //            //{
                        //            //    string[] x2 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica, forpag, for_descripcion };
                        //            //    NegDietetica.setROW("AsientoContable", x2);
                        //            //    AUXILIARLINEA++;
                        //            //}
                        //            ////asiento de COMISION
                        //            //linea = AUXILIARLINEA.ToString();
                        //            //tipmov = "0";
                        //            //codcue = row.Cells["CTA_COMISION"].Value.ToString();
                        //            //columna = "HABER";
                        //            //valor = (Convert.ToDouble(row.Cells["COMISION"].Value)).ToString().Replace(",", ".");
                        //            //if (valor.Trim() == "")
                        //            //    valor = "0";
                        //            //comentario = "COMISION";
                        //            //if (Convert.ToDouble(row.Cells["COMISION"].Value) > 0)
                        //            //{
                        //            //    string[] x3 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica, forpag, for_descripcion };
                        //            //    NegDietetica.setROW("AsientoContable", x3);
                        //            //    AUXILIARLINEA++;
                        //            //}
                        //            ////asiento de RETENCION
                        //            //linea = AUXILIARLINEA.ToString();
                        //            //tipmov = "RFS";
                        //            //codcue = row.Cells["CTA_RETENCION"].Value.ToString();
                        //            //columna = "HABER";
                        //            //valor = (Convert.ToDouble(row.Cells["RETENCION"].Value)).ToString().Replace(",", ".");
                        //            //if (valor.Trim() == "")
                        //            //    valor = "0";
                        //            //comentario = "RETENCION";
                        //            //if (Convert.ToDouble(row.Cells["RETENCION"].Value) > 0)
                        //            //{
                        //            //    string[] x4 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica, forpag, for_descripcion };
                        //            //    NegDietetica.setROW("AsientoContable", x4);
                        //            //    AUXILIARLINEA++;
                        //            //}
                        //            ////asiento de POR PAGAR
                        //            //linea = AUXILIARLINEA.ToString();
                        //            //tipmov = "0";
                        //            //codcue = row.Cells["CTA_MEDICO"].Value.ToString();
                        //            //columna = "HABER";
                        //            //if (Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                        //            //{
                        //            //    valor = (Convert.ToDouble(row.Cells["A_PAGAR"].Value)).ToString().Replace(",", ".");
                        //            //}
                        //            //else
                        //            //    valor = (Convert.ToDouble(row.Cells["A_PAGAR"].Value)).ToString().Replace(",", ".");
                        //            //if (valor.Trim() == "")
                        //            //    valor = "0";
                        //            //comentario = "HON.MED." + (row.Cells["MEDICO"].Value); ////A NOMBRE DE MEDICO

                        //            //if (Convert.ToDouble(row.Cells["A_PAGAR"].Value) > 0)
                        //            //{
                        //            //    string[] x5 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica, forpag, for_descripcion };
                        //            //    NegDietetica.setROW("AsientoContable", x5);
                        //            //    AUXILIARLINEA++;
                        //            //}
                        //        }
                        //    }
                        //}

                        #endregion

                        #region Genero Asiento
                        DataTable dt1 = NegDietetica.getDataTable("getNumeroControlAsiento", row.Cells["HOM_CODIGO"].Value.ToString());
                        numing = dt1.Rows[0][0].ToString();

                        tipord = "8";
                        //fecha = (Convert.ToDateTime(row.Cells["FECHA_FACTURA_MED"].Value)).ToString("yyyy'-'MM'-'dd'T'00':'00':'00");
                        fecha = dtpAsiento.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00");
                        codlocal = "1";
                        codzona = "1";
                        estado = "N";

                        forpag = ""; for_descripcion = "";
                        forpag = row.Cells["forpag"].Value.ToString();
                        for_descripcion = row.Cells["FORMA PAGO"].Value.ToString();
                        fecaux = fecha.Substring(0, 10).Replace("-", "");
                        PACIENTES dtoPacientes = new PACIENTES();
                        ATENCIONES ultimaAtencion = new ATENCIONES();
                        dtoPacientes = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(row.Cells["ATE_CODIGO"].Value.ToString()));
                        ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(row.Cells["ATE_CODIGO"].Value.ToString()));
                        HONORARIOS_MEDICOS honorarioaBorrar = new HONORARIOS_MEDICOS();
                        honorarioaBorrar = new NegHonorariosMedicos().RecuperaHonorariosMedicosID(Convert.ToInt64(row.Cells["HOM_CODIGO"].Value.ToString()));

                        DataTable Tabla = new DataTable();
                        Tabla = NegHonorariosMedicos.HMDatosAdiciones(Convert.ToInt32(row.Cells["HOM_CODIGO"].Value.ToString()));
                        if (row.Cells["FACTURA"].Value.ToString().Trim() == "")
                            nocomp = honorarioaBorrar.HOM_VALE;
                        else
                            nocomp = row.Cells["FACTURA"].Value.ToString();
                        numche = "0";


                        //tipmov = "FCS";/////////????????????????
                        codcentrocosto = "0";
                        codrubro = "0";
                        codactividad = "0";
                        autorizacion = row.Cells["AUTORIZACION"].Value.ToString();
                        fechacad = row.Cells["CADUCIDAD"].Value.ToString();
                        if (honorarioaBorrar.HOM_OBSERVACION != null)
                            observa = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Cells["PACIENTE"].Value.ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Cells["ATE_CODIGO"].Value.ToString() + "]";
                        else
                            observa = "PCTE." + row.Cells["PACIENTE"].Value.ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Cells["ATE_CODIGO"].Value.ToString() + "]";
                        fecpago = dtpAsiento.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00");
                        if (!Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                            codretencion = row.Cells["COD_RET"].Value.ToString(); ///TOMO DEL EXISTENTE, ATADO AL MEDICO
                        else
                            codretencion = "0";
                        cajachica = "3";

                        //asiento de VALOR NETO
                        if (!Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                        {
                            tipmov = "FSS";
                            linea = AUXILIARLINEA.ToString();
                            codcue = row.Cells["CTA_HONORARIOS"].Value.ToString();
                            columna = "DEBE";
                            valor = (Convert.ToDouble(row.Cells["VALOR"].Value)).ToString().Replace(",", ".");
                            valor = Math.Round(Convert.ToDouble(valor), 2).ToString();
                            comentario = "HONORARIOS MEDICOS";
                            //if (Convert.ToDouble(row.Cells["VALOR"].Value) > 0)
                            //{
                            string[] x1 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                            row.Cells["HOM_CODIGO"].Value.ToString(), Sesion.codUsuario.ToString()};
                            NegDietetica.setROW("AsientoContable", x1);
                            AUXILIARLINEA++;
                            //}
                        }
                        else
                        {
                            string cuenta = NegMedicos.CuentaContableSic(row.Cells["forpag"].Value.ToString());
                            string codcueSic = NegMedicos.TipoMoviemientoSic(cuenta);
                            if (Convert.ToInt32(codcueSic) ==4 )
                                tipmov = "DP";
                            else
                                tipmov = "0";

                            linea = AUXILIARLINEA.ToString();
                            codcue = cuenta;
                            columna = "DEBE";
                            valor = (Convert.ToDouble(row.Cells["VALOR"].Value)).ToString().Replace(",", ".");
                            valor = (Math.Round(Convert.ToDouble(valor), 2)).ToString();
                            if (row.Cells["VALOR_CUBIERTO"].Value.ToString() != "")
                            {
                                valor = (Convert.ToDouble(valor) - Convert.ToDouble(row.Cells["VALOR_CUBIERTO"].Value) - Convert.ToDouble(row.Cells["RECORTE"].Value)).ToString();
                                valor = (Math.Round(Convert.ToDouble(valor), 2)).ToString();
                            }
                            else
                            {
                                valor = (Convert.ToDouble(valor) - Convert.ToDouble(row.Cells["RECORTE"].Value)).ToString();
                                valor = (Math.Round(Convert.ToDouble(valor), 2)).ToString();
                            }
                            comentario = for_descripcion;
                            //if (Convert.ToDouble(row.Cells["VALOR"].Value) > 0)
                            //{
                            string[] x1 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor, codlocal,
                                codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto,
                                codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica,
                                forpag, for_descripcion,  row.Cells["HOM_CODIGO"].Value.ToString(), Sesion.codUsuario.ToString() };
                            NegDietetica.setROW("AsientoContable", x1);
                            AUXILIARLINEA++;
                            //}
                        }

                        //asiento de APORTE
                        linea = AUXILIARLINEA.ToString();
                        tipmov = "0";
                        codcue = row.Cells["CTA_APORTE"].Value.ToString();
                        columna = "HABER";
                        valor = (Convert.ToDouble(row.Cells["APORTE"].Value)).ToString().Replace(",", ".");
                        valor = Math.Round(Convert.ToDouble(valor), 2).ToString();
                        if (valor.Trim() == "")
                            valor = "0";
                        comentario = "APORTE";

                        if (Convert.ToDouble(row.Cells["APORTE"].Value) > 0)
                        {
                            string[] x2 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                    observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Cells["HOM_CODIGO"].Value.ToString(), Sesion.codUsuario.ToString()};
                            NegDietetica.setROW("AsientoContable", x2);
                            AUXILIARLINEA++;
                        }
                        else
                        {
                            //APORTE DE APC QUE NO SE TOMO EN CUENTA 
                            if (Tabla.Rows[0]["HON_EXCESO"].ToString() != "" && Tabla.Rows[0]["HON_EXCESO"].ToString() != "0.00")
                            {
                                linea = AUXILIARLINEA.ToString();
                                tipmov = "0";
                                codcue = row.Cells["CTA_APORTE"].Value.ToString();
                                columna = "HABER";
                                valor = Tabla.Rows[0]["HON_EXCESO"].ToString();
                                valor = Math.Round(Convert.ToDouble(valor), 2).ToString();
                                if (valor.Trim() == "")
                                    valor = "0";
                                comentario = "APORTE";

                                string[] x2 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                    observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Cells["HOM_CODIGO"].Value.ToString(), Sesion.codUsuario.ToString()};
                                NegDietetica.setROW("AsientoContable", x2);
                                AUXILIARLINEA++;

                                linea = AUXILIARLINEA.ToString();
                                tipmov = "0";
                                codcue = "210101-005";
                                columna = "DEBE";
                                valor = Tabla.Rows[0]["HON_EXCESO"].ToString();
                                valor = Math.Round(Convert.ToDouble(valor), 2).ToString();
                                if (valor.Trim() == "")
                                    valor = "0";
                                comentario = "HON.MED." + (row.Cells["MEDICO"].Value); //NOMBRE DEL MEDICO

                                string[] x = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa,
                                    fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Cells["HOM_CODIGO"].Value.ToString(), Sesion.codUsuario.ToString()};
                                NegDietetica.setROW("AsientoContable", x);
                                AUXILIARLINEA++;

                            }
                        }
                        //asiento de COMISION
                        linea = AUXILIARLINEA.ToString();
                        tipmov = "0";
                        codcue = row.Cells["CTA_COMISION"].Value.ToString();
                        columna = "HABER";
                        valor = (Convert.ToDouble(row.Cells["COMISION"].Value)).ToString().Replace(",", ".");
                        if (valor.Trim() == "")
                            valor = "0";
                        comentario = "COMISION";
                        if (Convert.ToDouble(row.Cells["COMISION"].Value) > 0)
                        {
                            string[] x3 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                    observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Cells["HOM_CODIGO"].Value.ToString(), Sesion.codUsuario.ToString()};
                            NegDietetica.setROW("AsientoContable", x3);
                            AUXILIARLINEA++;
                        }
                        //asiento de RETENCION
                        linea = AUXILIARLINEA.ToString();
                        tipmov = "RFS";
                        codcue = row.Cells["CTA_RETENCION"].Value.ToString();
                        columna = "HABER";
                        valor = (Convert.ToDouble(row.Cells["RETENCION"].Value)).ToString().Replace(",", ".");
                        if (valor.Trim() == "")
                            valor = "0";
                        comentario = "RETENCION";
                        if (Convert.ToDouble(row.Cells["RETENCION"].Value) > 0)
                        {
                            string[] x4 = new string[] { numing, tipord, fecha, linea, codcue, columna,
                                    valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp,
                                    numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                    observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Cells["HOM_CODIGO"].Value.ToString(), Sesion.codUsuario.ToString()};
                            NegDietetica.setROW("AsientoContable", x4);
                            AUXILIARLINEA++;
                        }
                        //asiento de POR PAGAR
                        linea = AUXILIARLINEA.ToString();
                        tipmov = "0";
                        PARAMETROS_DETALLE pd = NegParametros.RecuperaPorCodigo(53);
                        if ((bool)pd.PAD_ACTIVO)
                        {
                            if (Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                                codcue = pd.PAD_VALOR;
                            else
                                codcue = row.Cells["CTA_MEDICO"].Value.ToString();
                        }
                        else
                            codcue = row.Cells["CTA_MEDICO"].Value.ToString();
                        columna = "HABER";
                        if (Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value.ToString().Trim()))
                        {
                            valor = (Convert.ToDouble(row.Cells["A_PAGAR"].Value)).ToString().Replace(",", ".");
                        }
                        else
                            valor = (Convert.ToDouble(row.Cells["A_PAGAR"].Value)).ToString().Replace(",", ".");
                        if (valor.Trim() == "")
                            valor = "0";
                        comentario = "HON.MED." + (row.Cells["MEDICO"].Value); ////A NOMBRE DE MEDICO

                        if (Convert.ToDouble(row.Cells["A_PAGAR"].Value) > 0)
                        {
                            string[] x5 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa,
                                    fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Cells["HOM_CODIGO"].Value.ToString(), Sesion.codUsuario.ToString()};
                            NegDietetica.setROW("AsientoContable", x5);
                            AUXILIARLINEA++;
                        }


                        //SE CREA LA AUDITORIA DE LOS HONORARIOS
                        NegHonorariosMedicos.CrearHonorarioAuditoria(honorarioaBorrar, Convert.ToBoolean(row.Cells["HON_X_FUERA"].Value), true, "ASIENTO", cajachica, Convert.ToDouble(row.Cells["VALOR_CUBIERTO"].Value), Convert.ToInt64(row.Cells["MED_CODIGO"].Value.ToString()));
                        #endregion

                    }
                }
                MessageBox.Show("Asientos generados con exito!!! :)", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refrescar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UltraGridBand band = this.grid.DisplayLayout.Bands[0];
            foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            {
                row.Cells["Seleccion"].Value = xSelection;
            }
            xSelection = !xSelection;
        }

        private void toolStripButton2_MouseHover(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];
            //grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            //grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            //bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;
            //grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            //grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            //grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            grid.DisplayLayout.UseFixedHeaders = true;
        }

        private void grid2_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = grid2.DisplayLayout.Bands[0];
            //grid2.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid2.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid2.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            //grid2.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            //bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;
            //grid2.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            //grid2.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            //grid2.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;
            //Caracteristicas de Filtro en la grilla
            grid2.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid2.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid2.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid2.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid2.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            grid2.DisplayLayout.UseFixedHeaders = true;
        }

        private void ckbFuera_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbFuera.Checked)
            {

            }
        }

        private void frm_AsientoHonMed_Load(object sender, EventArgs e)
        {
            dtpAsiento.Value = DateTime.Now;
        }
    }
}
