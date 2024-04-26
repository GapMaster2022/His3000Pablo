using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;
using His.Entidades;

namespace His.Formulario
{
    public partial class frm_admisnitracionMedicamentos : Form
    {
        int pocision;
        string Xatecodigo;
        string Xnumeroatencion;
        TextBox codMedicamento = new TextBox();
        TextBox cantidad = new TextBox();
        int PrimerIngreso = 0;
        private static ATENCIONES atencion = null;
        private static PACIENTES pcte = null;
        private Int64 idReserva = 0;
        private USUARIOS usuarioLogado = null;
        USUARIOS userModificar = new USUARIOS();

        public frm_admisnitracionMedicamentos()
        {
            InitializeComponent();

        }
        public frm_admisnitracionMedicamentos(string numero_atencion, USUARIOS _usuarioLogeado)
        {
            InitializeComponent();
            usuarioLogado = _usuarioLogeado;
            userModificar = null;
            ATENCIONES x = NegAtenciones.RecuperarAtencionPorNumero(numero_atencion);
            cmbFrecuencia.SelectedIndex = -1;
            cmbVia.SelectedIndex = -1;
            Xnumeroatencion = numero_atencion;
            Xatecodigo = x.ATE_CODIGO.ToString();
            atencion = NegAtenciones.AtencionID(Convert.ToInt32(Xatecodigo));
            pcte = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(Xatecodigo));
            LlenaCombos();
            DataTable paciente = new DataTable();
            paciente = NegFormulariosHCU.Paciente(atencion.ATE_NUMERO_ATENCION);
            lblPaciente.Text = paciente.Rows[0][0].ToString();
            lblCodPaciente.Text = paciente.Rows[0][1].ToString();
            CargaDatos();
            PrimerIngreso = 1;

        }
        public void CargaDatos()
        {
            DataTable cargaGrid = new DataTable();
            bool admin = false;
            bool noadmin = false;
            cargaGrid = NegFormulariosHCU.RecuperaKardex(atencion.ATE_CODIGO.ToString());
            if (cargaGrid != null)
            {
                //CAMBIOS EDGAR 20210217 
                dtgCabeceraKardex.Rows.Clear();

                for (int i = 0; i < cargaGrid.Rows.Count; i++)
                {
                    if (cargaGrid.Rows[i][15].ToString() == "True")
                    {
                        admin = true;
                    }
                    else
                    {
                        admin = false;
                    }
                    if (cargaGrid.Rows[i][14].ToString() == "True")
                    {
                        noadmin = true;
                    }
                    else
                    {
                        noadmin = false;
                    }

                    dtgCabeceraKardex.Rows.Add(cargaGrid.Rows[i][3].ToString(), cargaGrid.Rows[i][4].ToString(),
                        cargaGrid.Rows[i][0].ToString(), cargaGrid.Rows[i][7].ToString(), cargaGrid.Rows[i][5].ToString(),
                        "", cargaGrid.Rows[i][6].ToString(), cargaGrid.Rows[i][9].ToString(),
                        cargaGrid.Rows[i][8].ToString(), admin, noadmin, cargaGrid.Rows[i][13].ToString(),
                        cargaGrid.Rows[i][0].ToString()); //Agrego uno a uno

                    //dtgCabeceraKardex.Rows.Add();
                    //dtgCabeceraKardex.Rows[i].Cells[0].Value = cargaGrid.Rows[i][3].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[1].Value = cargaGrid.Rows[i][4].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[3].Value = cargaGrid.Rows[i][7].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[4].Value = cargaGrid.Rows[i][5].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[6].Value = cargaGrid.Rows[i][6].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[7].Value = cargaGrid.Rows[i][9].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[8].Value = cargaGrid.Rows[i][8].ToString();
                    //if (cargaGrid.Rows[i][15].ToString() == "True")
                    //{
                    //    dtgCabeceraKardex.Rows[i].Cells[9].Value = true;
                    //    dtgCabeceraKardex.Rows[i].ReadOnly = true;
                    //    dtgCabeceraKardex.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    //}
                    //if (cargaGrid.Rows[i][14].ToString() == "True")
                    //{
                    //    dtgCabeceraKardex.Rows[i].Cells[10].Value = true;
                    //    dtgCabeceraKardex.Rows[i].ReadOnly = true;
                    //    dtgCabeceraKardex.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;
                    //}
                    //dtgCabeceraKardex.Rows[i].Cells[11].Value = cargaGrid.Rows[i][13].ToString();
                    //dtgCabeceraKardex.Rows[i].Cells[12].Value = cargaGrid.Rows[i][0].ToString();
                }


                //Cambios Edgar 20210217
                int valor = 0;
                foreach (DataGridViewRow item in dtgCabeceraKardex.Rows) //pintar las filas con check activos
                {
                    if (Convert.ToBoolean(item.Cells[9].Value) == true)
                    {
                        dtgCabeceraKardex.Rows[valor].ReadOnly = true;
                        dtgCabeceraKardex.Rows[valor].DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    if (Convert.ToBoolean(item.Cells[10].Value) == true)
                    {
                        dtgCabeceraKardex.Rows[valor].ReadOnly = true;
                        dtgCabeceraKardex.Rows[valor].DefaultCellStyle.BackColor = Color.LightPink;
                    }
                    valor++;
                }
            }

        }

        public void LlenaCombos()
        {

            DataTable via = new DataTable();

            List<FRECUENCIA> frecuencia = new List<FRECUENCIA>();
            frecuencia = NegFormulariosHCU.RecuperarFrecuencias();

            via = NegFormulariosHCU.LlenaCombos("VIA");
            cmbVia.DataSource = via;
            cmbVia.DisplayMember = "Detalle";
            cmbVia.ValueMember = "ID_VIA";

            cmbFrecuencia.DataSource = frecuencia;
            cmbFrecuencia.DisplayMember = "Detalle";
            cmbFrecuencia.ValueMember = "ID_FRECUENCIA";

            lblFecha.Text = Convert.ToString(DateTime.Now);
            lblHora.Text = Convert.ToString(DateTime.Now.Hour + ":" + DateTime.Now.Minute);
            lblUsuario.Text = usuarioLogado.NOMBRES.ToString() + " " + usuarioLogado.APELLIDOS.ToString();

        }
        List<KardexCompuesto> lista = new List<KardexCompuesto>();
        private void btnMedicamento_Click(object sender, EventArgs e)
        {
            int check = 0;
            if (checkBox1.Checked)
                check = 1;
            frm_AyudaKardex kardex = new frm_AyudaKardex(atencion.ATE_CODIGO.ToString(), 1, check);
            //kardex.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            DesbloquearControles();

            kardex.ShowDialog();
            if (kardex.lista.Count > 0)
            {
                lista = kardex.lista;
                lblMedicamento.Text = "Producto Compuesto";
                txtCantidadManual.Text = "1";
                txt_dosis_turno.Text = "1";
                txtCantidadManual.Enabled = true;
                txt_reserva.Text = "0";
            }
            else
            {

                lblMedicamento.Text = kardex.medicamento;
                codMedicamento.Text = kardex.cue_codigo;
                //cantidad.Text = kardex.cantidad;
                txtCantidadManual.Text = kardex.cantidad;
                txt_dosis_turno.Text = kardex.cantidad;
                if (lblMedicamento.Text != "" && txt_dosis_turno.Text != "")
                {
                    if (Convert.ToDouble(kardex.cantidad) > 1)
                    {
                        //txtCantidadManual.ReadOnly = true;
                        //txt_dosis_turno.ReadOnly = true;

                        txtDosisUnitaria.Focus();
                    }
                    else
                    {
                        txtCantidadManual.Enabled = true;
                        txt_reserva.Text = "0";
                    }
                }
                else
                {
                    if (kardex.cantidad != "")
                    {
                        gb_dosis.Enabled = true;
                        if (Convert.ToInt16(kardex.cantidad) > 1)
                        {
                            MessageBox.Show("Producto no se puede fraccionar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //txtCantidadManual.Enabled = false;
                            //txt_dosis_turno.Enabled = false;
                            txt_dosis_registra.Text = kardex.cantidad;
                            txt_reserva.Text = "0";
                        }
                    }
                }
            }
            cmbFrecuencia.Enabled = true;
            cmbVia.Enabled = true;
            cmb_medidas.Enabled = true;
            txtDosisUnitaria.Enabled = true;
            cmbVia.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //lblMedicamento.ReadOnly = false;
                lblMedicamento.Text = "";
                //btnMedicamento.Enabled = false;
            }
            else
            {
                lblMedicamento.ReadOnly = true;
                lblMedicamento.Text = "";
                btnMedicamento.Enabled = true;
            }
        }

        public bool Valida()
        {
            erroresPaciente.Clear();
            bool valido = true;
            if (cmb_medidas.SelectedIndex == -1)
            {
                erroresPaciente.SetError(cmb_medidas, "Campo Requerrido");
                valido = false;
            }
            if (cmbVia.SelectedIndex == -1)
            {
                erroresPaciente.SetError(cmbVia, "Campo Requerrido");
                valido = false;
            }
            if (cmbFrecuencia.SelectedIndex == -1)
            {
                erroresPaciente.SetError(cmbFrecuencia, "Campo Requerrido");
                valido = false;
            }
            if (txtDosisUnitaria.Text == "")
            {
                erroresPaciente.SetError(txtDosisUnitaria, "Campo Requerrido");
                valido = false;
            }
            if (txtCantidadManual.Text == "")
            {
                erroresPaciente.SetError(txtCantidadManual, "Campo Requerrido");
                valido = false;
            }
            if (txt_dosis_turno.Text == "")
            {
                erroresPaciente.SetError(txt_dosis_turno, "Campo Requerrido");
                valido = false;
            }
            if (lblMedicamento.Text == "")
            {
                erroresPaciente.SetError(lblMedicamento, "Campo Requerrido");
                valido = false;
            }


            return valido;
            //if (lblMedicamento.Text != "")
            //{
            //    if (txtDosisUnitaria.Text == "")
            //    {
            //        MessageBox.Show("Ingrese la dosis del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        return false;
            //    }
            //    if (chbCantidadManual.Checked && txtCantidadManual.Text == "")
            //    {
            //        MessageBox.Show("Ingrese el numero de cantidades que se va administrar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        return false;
            //    }
            //    return true;
            //}
            //else
            //{
            //    MessageBox.Show("Ingrese Medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return false;
            //}
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                if (chkCompuesto.Checked)
                {
                    if (MessageBox.Show("Esta cargando un medicamento como puesto", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                bool ok = false;
                IngresaKardex objIngresa = new IngresaKardex();
                if (chkCompuesto.Checked)
                {
                    objIngresa.presentacion = lblMedicamento.Text + " (Compuesto)";

                }
                else
                {
                    objIngresa.presentacion = lblMedicamento.Text;

                }
                objIngresa.via = cmbVia.Text;
                objIngresa.frecuencia = cmbFrecuencia.Text;
                objIngresa.dosis = txt_dosis_registra.Text;

                objIngresa.hora = Convert.ToDateTime(cmb_frecuencia_hora.Text);
                objIngresa.fecha = (DateTime.Now);
                objIngresa.ate_codigo = atencion.ATE_NUMERO_ATENCION;
                if (checkBox2.Checked)
                    objIngresa.eventual = true;
                else
                    objIngresa.eventual = false;

                if (checkBox1.Checked)
                {
                    objIngresa.id_kardex = 0;
                    objIngresa.medPropio = true;
                }
                else
                {
                    objIngresa.id_kardex = Convert.ToInt64(codMedicamento.Text);//este es el codigo cue_cuenta para poder verificar que medicamento ya fue puesto en kardex                
                    objIngresa.medPropio = false;
                }
                int hora = 0;
                DateTime fecha = DateTime.Now.Date;
                DateTime xfecha = DateTime.Now.Date;
                int cant = 0;
                if (chbCantidadManual.Checked)
                {
                    cant = Convert.ToInt16(txt_dosis_turno.Text);
                }
                else
                {
                    cant = Convert.ToInt16(cantidad.Text);
                }
                for (int i = 0; i < cant; i++)
                {
                    if (i == 0)
                    {
                        string[] ho = cmb_frecuencia_hora.Text.Split(':');
                        hora = Convert.ToInt16(ho[0]);
                    }
                    else
                    {
                        string frace = cmbFrecuencia.Text;
                        string[] spli = frace.Split(' ');
                        if (spli.Length > 1)
                        {
                            hora += Convert.ToInt16(spli[1]);
                        }
                        if (hora >= 24)
                        {
                            hora -= 24;
                            fecha = DateTime.Today.AddDays(1);
                            //string mananatDate = manana.ToString("yyyy-MM-dd");
                            xfecha = xfecha.AddDays(1);

                        }
                    }
                    //if (hora > 22 && hora < 00)
                    //{
                    //    hora = 22;
                    //}
                    //if (hora > 00 && hora < 06)
                    //{
                    //    hora = 06;
                    //}
                    objIngresa.hora = Convert.ToDateTime(hora + ":00");
                    objIngresa.fecha = xfecha;
                    ok = NegFormulariosHCU.IngresaKardex(objIngresa, usuarioLogado.ID_USUARIO);
                    if (!ok)
                    {
                        MessageBox.Show("Kardex No Se Actualizo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    //SE REALIZA CAMBIOS PARA ARREGLAR LA FUNCIONALIDAD PABLO ROCHA 10/11/2022 CODIGO REMPLAZADO POR EL DE ARRIBA
                    //if (ok)
                    //{

                    //    dtgCabeceraKardex.Rows.Add(new string[]{
                    //    Convert.ToString(codMedicamento.Text),
                    //    Convert.ToString(lblMedicamento.Text),
                    //    Convert.ToString(cmbVia.SelectedIndex+1),
                    //    Convert.ToString(cmbVia.Text),
                    //    Convert.ToString(txtDosisUnitaria.Text),
                    //    Convert.ToString(cmbFrecuencia.SelectedIndex+1),
                    //    Convert.ToString(cmbFrecuencia.Text),
                    //    Convert.ToString(fecha),
                    //    Convert.ToString(hora)
                    //    });
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Kardex No Se Actualizo", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //    return;
                    //}
                }

                PrimerIngreso = 0;
                CargaDatos();
                PrimerIngreso = 1;

                //SE GUARDAN LAS RESERVAS EN UNA TABLA DIFERENTE PARA PODER RESCATARLAS
                if (Convert.ToInt32(txt_reserva.Text) > 0)
                {
                    string[] dosisUni = txt_dosis_registra.Text.Split(' ');

                    RESERVA_KARDEX_MEDICAMENTO obj = new RESERVA_KARDEX_MEDICAMENTO();
                    obj.id_usuario = Sesion.codUsuario;
                    obj.ate_codigo = atencion.ATE_CODIGO;
                    obj.cue_codigo = objIngresa.id_kardex;
                    obj.presentacion = lblMedicamento.Text;
                    obj.frecuencia = Convert.ToInt32(cmbFrecuencia.SelectedIndex.ToString());
                    obj.via_administracion = Convert.ToInt32(cmbVia.SelectedIndex.ToString());
                    obj.dosis = Convert.ToDecimal(dosisUni[0]);
                    obj.medida = Convert.ToInt32(cmb_medidas.SelectedIndex.ToString());
                    obj.reserva = Convert.ToInt32(txt_reserva.Text);
                    obj.estado = true;
                    obj.fecha_registro = DateTime.Now;
                    NegFormulariosHCU.GuardaReservas(obj);
                }
                if (idReserva != 0)
                {
                    NegFormulariosHCU.ModificaReservas(idReserva);
                }

                btnMedicamento.Enabled = true;
                BloquearControles();
                MessageBox.Show("Kardex Actualizado Con Exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void BloquearControles()
        {
            lblCodMedicamento.Text = "";
            codMedicamento.Text = "";
            txtDosisUnitaria.Text = "";
            lblMedicamento.Text = "";
            txtCantidadManual.Text = "";
            txtDosisUnitaria.Text = "";
            txt_dosis_turno.Text = "";
            txt_dosis_registra.Text = "";
            txt_reserva.Text = "";
            cmbFrecuencia.SelectedIndex = -1;
            cmb_medidas.SelectedIndex = -1;
            cmbVia.SelectedIndex = -1;
            gb_dosis.Enabled = false;
        }
        public void DesbloquearControles()
        {
            lblCodMedicamento.Text = "";
            codMedicamento.Text = "";
            txtDosisUnitaria.Text = "";
            lblMedicamento.Text = "";
            txtCantidadManual.Text = "";
            txtDosisUnitaria.Text = "";
            txt_dosis_turno.Text = "";
            txt_dosis_registra.Text = "";
            txt_reserva.Text = "";
            cmbFrecuencia.SelectedIndex = -1;
            cmb_medidas.SelectedIndex = -1;
            cmbVia.SelectedIndex = -1;
            gb_dosis.Enabled = true;
            txtCantidadManual.ReadOnly = false;
            txt_dosis_turno.ReadOnly = false;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Si Continua Perdera La Información Sin Cargar", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            this.Close();

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            if (dtgCabeceraKardex.Rows.Count > 0)
            {
                DataTable xDatos = NegDietetica.getDataTable("KardexMedicamentos", Xatecodigo);


                DSkardexmedicamentos ds = new DSkardexmedicamentos();

                //DataTable codMedicamentos = NegDietetica.getDataTable("Form022_Medicamentos", Xatecodigo);
                //for (int i = 0; i < codMedicamentos.Rows.Count; i++)
                //{
                //    DataTable fechasXmed = NegDietetica.getDataTable("Form022_Medicamentos", Xatecodigo,Convert.ToInt32(codMedicamentos.Rows[0][i].ToString()));

                //}

                DataTable xFechas = NegDietetica.getDataTable("Form022_Fechas", Xatecodigo);
                DataTable xMedicamentos = NegDietetica.getDataTable("Form022_Medicamentos", Xatecodigo);
                int aux_i = 0;
                foreach (DataRow imed in xMedicamentos.Rows)
                {

                    foreach (DataRow ifec in xFechas.Rows)
                    {
                        string[] values = new string[] {
                                    ifec["FechaAdministración"].ToString(),
                                    imed["Presentacion"].ToString(),
                                    imed["dosis"].ToString(),
                                    imed["via"].ToString(),
                                    imed["Frecuencia"].ToString(),
                                    imed["CueCodigo"].ToString()
                                    }; ;

                        DataTable xDosis = NegDietetica.getDataTable("Form022_Registros", Xatecodigo, "0", values);

                        string xUDosis = "";
                        int count = 0;
                        foreach (DataRow item in xDosis.Rows)
                        {
                            Console.WriteLine(item["NoAdministrado"].ToString());
                            if (item["NoAdministrado"].ToString() == "True")
                            {
                                xUDosis += "(" + item["hora"].ToString().Substring(0, 2) + ") "
                                + item["nombres"].ToString().Substring(0, 1)
                                + item["apellidos"].ToString().Substring(0, 1) + " "
                                + item["DEP_NOMBRE"].ToString().Substring(0, 3) + " ["
                                + item["Observacion"].ToString().Trim() + "]"
                                + Environment.NewLine;
                            }
                            if (item["Administrado"].ToString() == "True")
                            {
                                xUDosis += item["hora"].ToString().Substring(0, 2)
                                + "    " + item["nombres"].ToString().Substring(0, 1)
                                + item["apellidos"].ToString().Substring(0, 1) + "    "
                                + item["DEP_NOMBRE"].ToString().Substring(0, 3) + Environment.NewLine;
                            }

                            count++;
                        }
                        for (int i = count; i < 6; i++)
                        {
                            if (xUDosis != "")
                                xUDosis += "." + Environment.NewLine;
                        }
                        NegCertificadoMedico neg = new NegCertificadoMedico();

                        if (xUDosis != "")
                        {
                            DataRow dr = ds.Tables["dtKardexMED"].NewRow();
                            if (imed["dosis"].ToString() == "(COMPUESTO)")
                            {
                                List<MEDICAMENTO_COMPUESTO_DETALLE> medLista = new List<MEDICAMENTO_COMPUESTO_DETALLE>();
                                medLista = NegFormulariosHCU.RecuperaMedCompuestoDetalle(Convert.ToInt64(imed["CueCodigo"].ToString()));
                                string medicamentoGlobal = "";
                                foreach (var item in medLista)
                                {
                                    medicamentoGlobal += "\r\n" + item.cue_detalle + " " + item.dosis + " (" + item.cantidad + ")"; 
                                }
                                dr["MEDICAMENTO"] = imed["Presentacion"].ToString() + "," + imed["via"].ToString() + "," + imed["dosis"].ToString() + medicamentoGlobal + "," + imed["Frecuencia"].ToString();
                            }
                            else
                            {
                                dr["MEDICAMENTO"] = imed["Presentacion"].ToString() + "," + imed["via"].ToString() + "," + imed["dosis"].ToString() + "," + imed["Frecuencia"].ToString();
                            }
                            //dr["EMP_NOMBRE"] = row["EMP_NOMBRE"].ToString();
                            dr["PAC_NOMBRE1"] = pcte.PAC_NOMBRE1;
                            dr["PAC_NOMBRE2"] = pcte.PAC_NOMBRE2;
                            dr["PAC_APELLIDO_PATERNO"] = pcte.PAC_APELLIDO_PATERNO;
                            dr["PAC_APELLIDO_MATERNO"] = pcte.PAC_APELLIDO_MATERNO;
                            dr["PAC_GENERO"] = pcte.PAC_GENERO;
                            dr["PAC_HISTORIA_CLINICA"] = pcte.PAC_HISTORIA_CLINICA;
                            //dr["MEDICAMENTO"] = imed["Presentacion"].ToString() + "," + imed["via"].ToString() + "," + imed["dosis"].ToString() + "," + imed["Frecuencia"].ToString();
                            dr["DIAyMES"] = "          " + ifec["FechaAdministración"].ToString().Substring(0, 5) + "          HORA  INI  FUN";
                            dr["DIAyMES"] = "          " + ifec["FechaAdministración"].ToString().Substring(0, 5) + "          HORA  INI  FUN";
                            dr["EMP_NOMBRE"] = Sesion.nomEmpresa;
                            dr["Logo"] = neg.path();
                            dr["HORA"] = xUDosis;
                            //dr["INI"] = (row["NOMBRES"].ToString()).Substring(0,1) + (row["APELLIDOS"].ToString()).Substring(0, 1);
                            //dr["FUNCION"] = (row["DEP_NOMBRE"].ToString()).Substring(0, 3);
                            // string strdosis = string.Format("{1} {2} {3}", Environment.NewLine, row["Hora"].ToString(), (row["NOMBRES"].ToString()).Substring(0, 1) + (row["APELLIDOS"].ToString()).Substring(0, 1), (row["DEP_NOMBRE"].ToString()).Substring(0, 3));
                            //  dr["HORA"] = row["Hora"].ToString();

                            ds.Tables["dtKardexMED"].Rows.Add(dr);
                        }

                    }
                    aux_i++;
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
                cabecera["edad"] = NegUtilitarios.EdadCalculada(Convert.ToDateTime(pcte.PAC_FECHA_NACIMIENTO),(DateTime)atencion.ATE_FECHA_INGRESO);
                ds.Tables["Encabezado"].Rows.Add(cabecera);

                His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "FORM022", ds);
                myreport.Show();
            }
            else
                MessageBox.Show("No hay datos para generar el reporte", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            ///********************************************///
            //frmReportes rep = new frmReportes();
            //rep.campo1 = ateCodigo;
            //rep.reporte = "KardexMedicamento";
            //rep.ShowDialog();
            //rep.Dispose();
        }

        private void frm_admisnitracionMedicamentos_Load(object sender, EventArgs e)
        {
            //foreach (DataGridViewColumn c in dtgCabeceraKardex.Columns)
            //{
            //    if (c.Name != "horaAdministracion")
            //        c.ReadOnly = true;
            //    //if (c.Name != "noAdministrado")
            //    //    c.ReadOnly = true;
            //    //if (c.Name != "Administrado")
            //    //    c.ReadOnly = true;
            //    //if (c.Name != "observacion")
            //    //    c.ReadOnly = true;
            //}
        }

        private void dtgCabeceraKardex_SelectionChanged(object sender, EventArgs e)
        {
            //dtgCabeceraKardex.CurrentCell = dtgCabeceraKardex.CurrentRow.Cells["horaAdministracion"];
            //dtgCabeceraKardex.BeginEdit(true);

        }

        private void dtgCabeceraKardex_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.CurrentCell.ColumnIndex == 11)
            {
                TextBox tb = ((TextBox)e.Control);
                tb.CharacterCasing = CharacterCasing.Upper;
            }
            if (dgv.CurrentCell.ColumnIndex == 8)
            {

                TextBox txt = e.Control as TextBox;
                if (txt != null)
                {
                    txt.KeyPress += new KeyPressEventHandler(dtgCabeceraKardex_KeyPress);
                }
            }
        }

        private void dtgCabeceraKardex_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                pocision = dtgCabeceraKardex.CurrentRow.Index;
            }
            catch
            {
            }
        }

        private void dtgCabeceraKardex_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (dtgCabeceraKardex.CurrentCell.ColumnIndex == 8)
            //{
            //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
        }

        private void dtgCabeceraKardex_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

            dtgCabeceraKardex.CommitEdit(DataGridViewDataErrorContexts.Commit);


            if (PrimerIngreso == 1)
            {

                try
                {
                    bool IsCheck = false;
                    bool IsCheck2 = false;
                    bool actualizado = false;
                    int hora;
                    #region Administrado                    


                    if (e.ColumnIndex == 9)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dtgCabeceraKardex.CurrentRow.Cells["administrado"];
                        if (userModificar == null)
                        {
                            IsCheck = dtgCabeceraKardex.Rows.OfType<DataGridViewRow>().Any(x => Convert.ToBoolean(x.Cells["administrado"].Value));

                            dtgCabeceraKardex.BeginEdit(true);
                        }
                        else
                        {
                            IsCheck = true;
                        }
                        if (IsCheck)
                        {
                            if (MessageBox.Show("Si Continua Se Almacenara el Medicamento como Administrado", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes && IsCheck)
                            {
                                dtgCabeceraKardex.Rows[pocision].ReadOnly = true;
                                if (userModificar == null)
                                {
                                    actualizado = NegFormulariosHCU.ActualizaMedicamento(Convert.ToDateTime(dtgCabeceraKardex.Rows[pocision].Cells[8].Value), IsCheck, "", Convert.ToInt64(dtgCabeceraKardex.Rows[pocision].Cells[12].Value));
                                }
                                else
                                {
                                    actualizado = NegFormulariosHCU.EditarKardexMedicamento(userModificar.ID_USUARIO, Convert.ToInt64(dtgCabeceraKardex.Rows[pocision].Cells[12].Value), true, dtgCabeceraKardex.Rows[pocision].Cells[3].Value.ToString(), 1, dtgCabeceraKardex.Rows[pocision].Cells[6].Value.ToString());
                                    //dtgCabeceraKardex.Rows[pocision].Cells[10].Value = false;
                                }
                                if (actualizado)
                                {
                                    MessageBox.Show("Medicamento Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Cambios(true);
                                    CargaDatos();
                                }
                                else
                                {
                                    MessageBox.Show("Medicamento No Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dtgCabeceraKardex.Rows[pocision].Cells[9].Value = false;
                                }
                            }
                            else
                            {
                                chk.Value = 0;
                                dtgCabeceraKardex.EndEdit();
                            }

                        }
                    }

                    #endregion




                    #region No Administrado


                    else if (e.ColumnIndex == 10)
                    {

                        if (userModificar == null)
                        {
                            IsCheck2 = Convert.ToBoolean(dtgCabeceraKardex.Rows[pocision].Cells[10].Value);
                            dtgCabeceraKardex.BeginEdit(true);

                        }
                        else
                        {
                            IsCheck2 = true;
                        }
                        DataGridViewCheckBoxCell chk2 = (DataGridViewCheckBoxCell)dtgCabeceraKardex.CurrentRow.Cells["noAdministrado"];

                        if (IsCheck2)
                        {
                            if (dtgCabeceraKardex.Rows[pocision].Cells[11].Value != null && dtgCabeceraKardex.Rows[pocision].Cells[11].Value.ToString() != "")
                            {
                                if (MessageBox.Show("Si Continua Se Almacenara el Medicamento como No Administrado", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {

                                    dtgCabeceraKardex.Rows[pocision].ReadOnly = true;
                                    if (userModificar == null)
                                    {
                                        actualizado = NegFormulariosHCU.ActualizaMedicamento(Convert.ToDateTime(dtgCabeceraKardex.Rows[pocision].Cells[8].Value), IsCheck2, dtgCabeceraKardex.Rows[pocision].Cells[11].Value.ToString(), Convert.ToInt64(dtgCabeceraKardex.Rows[pocision].Cells[12].Value));
                                    }
                                    else
                                    {
                                        actualizado = NegFormulariosHCU.EditarKardexMedicamento(userModificar.ID_USUARIO, Convert.ToInt64(dtgCabeceraKardex.Rows[pocision].Cells[12].Value), false, dtgCabeceraKardex.Rows[pocision].Cells[3].Value.ToString(), 1, dtgCabeceraKardex.Rows[pocision].Cells[6].Value.ToString(), dtgCabeceraKardex.Rows[pocision].Cells[11].Value.ToString());
                                        //dtgCabeceraKardex.Rows[pocision].Cells[9].Value = false;
                                    }

                                    if (actualizado)
                                    {
                                        MessageBox.Show("Medicamento Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Cambios(true);
                                        CargaDatos();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Medicamento No Actualizado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //dtgCabeceraKardex.Rows[pocision].Cells[10].Value = false;
                                    }
                                }
                            }
                            else
                            {
                                chk2.Value = 0;
                                //dtgCabeceraKardex.EndEdit();
                                MessageBox.Show("Si Va Marcar Medicamento Como No Administrado Debe Incluir La Razon En El Campo Observación", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }

                    }
                    #endregion
                    //Cambios(true);
                }

                catch (Exception ex)
                { Console.WriteLine(ex); }
            }
            else
            {
                Cambios(true);
            }
        }

        public void Cambios(bool estado)
        {
            int valor = 0;
            foreach (DataGridViewRow item in dtgCabeceraKardex.Rows) //pintar las filas con check activos
            {
                if (Convert.ToBoolean(item.Cells[9].Value) == true)
                {
                    dtgCabeceraKardex.Rows[valor].ReadOnly = estado;
                    dtgCabeceraKardex.Rows[valor].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                if (Convert.ToBoolean(item.Cells[10].Value) == true)
                {
                    dtgCabeceraKardex.Rows[valor].ReadOnly = estado;
                    dtgCabeceraKardex.Rows[valor].DefaultCellStyle.BackColor = Color.LightPink;
                }
                valor++;
            }
        }

        private void dtgCabeceraKardex_KeyDown(object sender, KeyEventArgs e)
        {
            //if(dtgCabeceraKardex.SelectedRows.Count > 0)
            //{
            //    if(e.KeyCode == Keys.Delete)
            //    {
            //        string producto = dtgCabeceraKardex.CurrentRow.Cells[1].Value.ToString();
            //        int id_kardexmedicamento = Convert.ToInt32(dtgCabeceraKardex.CurrentRow.Cells[2].Value.ToString());
            //        if(MessageBox.Show("¿Está seguro de eliminar: " + producto + "?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation
            //            ) == DialogResult.Yes)
            //        {
            //            //Elimina el producto
            //            try
            //            {
            //                NegFormulariosHCU.EliminarProdKardexMed(id_kardexmedicamento);
            //                MessageBox.Show("El producto se ha eliminado correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                CargaDatos();
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show(ex.Message);
            //            }
            //        }
            //    }

        }

        private void dtgCabeceraKardex_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            dtgCabeceraKardex.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dtgCabeceraKardex_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            #region fecha

            try
            {
                pocision = dtgCabeceraKardex.CurrentRow.Index;
            }
            catch
            {
            }

            if (e.ColumnIndex == 8)
            {
                try
                {
                    string xhora = dtgCabeceraKardex.CurrentRow.Cells[8].Value.ToString().Substring(0, 2) + ":" + dtgCabeceraKardex.Rows[pocision].Cells[8].Value.ToString().Substring(2, 2);
                    string xid = dtgCabeceraKardex.CurrentRow.Cells[12].Value.ToString();
                    string[] x = new string[]
                    {
                                xid,xhora
                    };
                    NegDietetica.setROW("HoraKardexMedicamentos", x, xid);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            else if (e.ColumnIndex == 3)
            {
                if (userModificar != null)
                {
                    bool actualizado = NegFormulariosHCU.EditarKardexMedicamento(userModificar.ID_USUARIO, Convert.ToInt64(dtgCabeceraKardex.Rows[pocision].Cells[12].Value), true, dtgCabeceraKardex.Rows[pocision].Cells[3].Value.ToString(), 2, dtgCabeceraKardex.Rows[pocision].Cells[6].Value.ToString());

                    if (actualizado)
                    {
                        MessageBox.Show("Cambio exitoso de Vía de administración", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Para realizar cambios debe ingresar usuario y clave", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (e.ColumnIndex == 6)
            {
                if (userModificar != null)
                {
                    bool actualizado = NegFormulariosHCU.EditarKardexMedicamento(userModificar.ID_USUARIO, Convert.ToInt64(dtgCabeceraKardex.Rows[pocision].Cells[12].Value), true, dtgCabeceraKardex.Rows[pocision].Cells[3].Value.ToString(), 2, dtgCabeceraKardex.Rows[pocision].Cells[6].Value.ToString());

                    if (actualizado)
                    {
                        MessageBox.Show("Cambio exitoso de la Frecuencia de administración", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Para realizar cambios debe ingresar usuario y clave", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            #endregion
        }

        private void chbCantidadManual_CheckedChanged(object sender, EventArgs e)
        {
            //if (lblMedicamento.Text != "" && Convert.ToInt16(cantidad.Text) <= 1)
            //{
            //    if (chbCantidadManual.Checked)
            //    {
            //        lblCantidad.Visible = true;
            //        txtCantidadManual.Visible = true;
            //        txtCantidadManual.Focus();
            //    }
            //    else
            //    {
            //        lblCantidad.Visible = false;
            //        txtCantidadManual.Visible = false;
            //        txtCantidadManual.Text = "";
            //    }
            //}
            //else
            //{
            //    if (cantidad.Text != "")
            //    {
            //        if (Convert.ToInt16(cantidad.Text) > 1)
            //        {
            //            MessageBox.Show("Producto no se puede fraccionar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    chbCantidadManual.Checked = false;
            //}
        }

        private void txtCantidadManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Se necesita que complete la cantidad del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Enfocar otro cuadro de texto
                txt_dosis_turno.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
        }

        private void txtDosisUnitaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Se necesita que complete la cantidad total del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Enfocar otro cuadro de texto
                cmb_medidas.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
        }

        private void txt_dosis_turno_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumberDecimal(e, false);
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Se necesita que complete la cantidad total del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Enfocar otro cuadro de texto
                btnCargar.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
            if (txt_dosis_turno.Text != "" && txtCantidadManual.Text != "")
            {
                if (Convert.ToDouble(txt_dosis_turno.Text) > Convert.ToDouble(txtCantidadManual.Text))
                {
                    MessageBox.Show("La cantidad de dosis no puede ser mayor a las dosis divididas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_dosis_turno.Text = "";
                    txt_dosis_turno.Focus();
                }
            }
        }

        private void txtCantidadManual_Leave(object sender, EventArgs e)
        {
            if (txtDosisUnitaria.Text != "")
            {
                if (txtCantidadManual.Text != "")
                {
                    double totalProducto = Convert.ToDouble(txtDosisUnitaria.Text);
                    double dosis = Convert.ToDouble(txtCantidadManual.Text);
                    double aplicar = Convert.ToDouble(txt_dosis_turno.Text);
                    if (totalProducto >= dosis)
                    {
                        if (txtCantidadManual.ReadOnly)
                        {
                            txt_dosis_registra.Text = txtDosisUnitaria.Text + " " + cmb_medidas.Text;
                        }
                        else
                        {
                            double registra = totalProducto / dosis;
                            txt_dosis_registra.Text = (Convert.ToInt32(registra)).ToString() + " " + cmb_medidas.Text;
                            double reserva = dosis - aplicar;
                            txt_reserva.Text = reserva.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Division no puede proceder revise las cantidades.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDosisUnitaria.Text = totalProducto.ToString();
                        txtCantidadManual.Text = "";
                        txtCantidadManual.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Se necesita que complete la cantidad de la dosis del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void cmb_medidas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                if (txtDosisUnitaria.Text == "")
                {
                    MessageBox.Show("Se necesita que complete la cantidad total del medicamento", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Enfocar otro cuadro de texto
                txtCantidadManual.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
        }

        private void txt_dosis_turno_Leave(object sender, EventArgs e)
        {
            if (txt_dosis_turno.Text != "" && txtCantidadManual.Text != "")
            {
                double dosis = Convert.ToDouble(txtCantidadManual.Text);
                double registra = Convert.ToDouble(txt_dosis_turno.Text);
                double reserva = dosis - registra;
                txt_reserva.Text = reserva.ToString();
            }
        }

        private void btn_reservas_Click(object sender, EventArgs e)
        {
            frm_AyudaKardex kardex = new frm_AyudaKardex(atencion.ATE_CODIGO, true);
            BloquearControles();
            kardex.ShowDialog();
            if (kardex.medicamento != "")
            {
                gb_dosis.Enabled = true;
                lblMedicamento.Text = kardex.medicamento;
                codMedicamento.Text = kardex.cue_codigo;
                txtCantidadManual.Text = kardex.cantidad;
                txt_dosis_turno.Text = kardex.cantidad;
                cmb_medidas.SelectedIndex = kardex.medida;
                cmbVia.SelectedIndex = kardex.via;
                cmbFrecuencia.SelectedIndex = kardex.frecuencia;
                idReserva = kardex.idReseerva;
                txt_dosis_registra.Text = (Convert.ToInt32(kardex.dosis)).ToString() + " " + cmb_medidas.Text;
                txtDosisUnitaria.Text = (Convert.ToInt32(kardex.dosis)).ToString();
                txt_reserva.Text = "0";
                btnMedicamento.Enabled = false;
                txt_dosis_turno.Enabled = true;
                txtDosisUnitaria.Enabled = false;
                txtCantidadManual.Enabled = false;
                cmb_medidas.Enabled = false;
                cmbVia.Enabled = false;
                cmbFrecuencia.Enabled = false;
            }
        }

        private void btnModificarValores_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin(true);
            frm.ShowDialog();
            userModificar = frm.user;
            Cambios(false);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbVia_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                cmbFrecuencia.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
        }

        private void cmbFrecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Tab || e.KeyChar == (char)Keys.Enter)
            {
                txtDosisUnitaria.Focus();
                e.Handled = true; // Para evitar que se ingrese un salto de línea en el cuadro de texto actual
            }
        }

        private void cmbFrecuencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            int combo = cmbFrecuencia.SelectedIndex + 1;
            List<FRECUENCIA_HORAS> obj = new List<FRECUENCIA_HORAS>();
            obj = NegFormulariosHCU.RecuperarFrecuenciasHoras(combo);
            cmb_frecuencia_hora.DataSource = obj;
            cmb_frecuencia_hora.DisplayMember = "detalle";
            cmb_frecuencia_hora.ValueMember = "id_hora_frecuencia";
        }

        private void btnCompuesto_Click(object sender, EventArgs e)
        {
            frm_MedicamentoCompuesto frm = new frm_MedicamentoCompuesto(atencion, usuarioLogado);
            frm.ShowDialog();
            CargaDatos();
        }
    }
}
