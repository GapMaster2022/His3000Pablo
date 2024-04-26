using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using His.Negocio;
using System.Windows.Forms;
using Core.Datos;
using His.Entidades;
using His.DatosReportes;

namespace His.Formulario
{
    public partial class frm_Referencia : Form
    {
        int _ate_codigo;
        int _id_usuario;
        ATENCIONES atencion = null;
        HC_EPICRISIS epicrisis = null;
        HC_ANAMNESIS anamnesis = new HC_ANAMNESIS();
        List<HC_ANAMNESIS_DETALLE> listaDetalleEF = new List<HC_ANAMNESIS_DETALLE>();
        List<HC_CATALOGOS> listaCatalogo = new List<HC_CATALOGOS>();
        HC_CATALOGOS catalogo = new HC_CATALOGOS();
        HC_ANAMNESIS_DETALLE detalle = new HC_ANAMNESIS_DETALLE();
        public frm_Referencia(int ATE_CODIGO)
        {
            _ate_codigo = ATE_CODIGO;

            InitializeComponent();
            // USUARIOS u = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            _id_usuario = His.Entidades.Clases.Sesion.codUsuario;
            refrescarSolicitudes();
        }


        public void CargarReferencia()
        {

            DataTable x = NegDietetica.getDataTable("GetReferencia", _ate_codigo.ToString(), _id_usuario.ToString());
            txtReferencia.Text = Convert.ToString(x.Rows[0]["Id"]);
            dtpFecha.Value = Convert.ToDateTime(x.Rows[0]["FECHA"]);
            txtEstablecimiento.Text = Convert.ToString(x.Rows[0]["ESTABLECIMIENTO"]);
            txtMedicoCOD.Text = Convert.ToString(x.Rows[0]["MED_CODIGO"]);
            if (txtMedicoCOD.Text != string.Empty)
            {
                cargarMedico(Convert.ToInt32(x.Rows[0]["MED_CODIGO"]), txtMedico, txtMedicoCOD);
            }
            txtServicio.Text = Convert.ToString(x.Rows[0]["SERVICIO"]);
            txtMotivo.Text = Convert.ToString(x.Rows[0]["MOTIVO"]);
            txtResumen.Text = Convert.ToString(x.Rows[0]["RESUMEN"]);
            txtHallazgos.Text = Convert.ToString(x.Rows[0]["HALLAZGOS"]);
            txtTratamiento.Text = Convert.ToString(x.Rows[0]["TRATAMIENTO"]);
            gridDiagnosticos.Rows.Clear();

            DataTable diagnosticos = NegDietetica.getDataTable("GetDxReferencia", Convert.ToString(x.Rows[0]["Id"]).Trim());

            foreach (DataRow row in diagnosticos.Rows)
            {
                string aux;
                if ((row[2].ToString()) == "True")
                    aux = "DEFINITIVO";
                else
                    aux = "PRESUNTIVO";

                this.gridDiagnosticos.Rows.Add(row[0].ToString(), row[1].ToString(), aux);
            }
            //CAMBIOS EDGAR 20210115
            atencion = NegAtenciones.RecuperarAtencionID(_ate_codigo);
            epicrisis = NegEpicrisis.recuperarEpicrisisPorAtencion(atencion.ATE_CODIGO);
            DataTable evolucionAnalisis = new DataTable();
            evolucionAnalisis = NegEpicrisis.RecuperaEvolucionAnalisis(atencion.ATE_CODIGO);
            if (epicrisis == null)
            {
                anamnesis = NegAnamnesis.recuperarAnamnesisPorAtencion(atencion.ATE_CODIGO);
                if (anamnesis != null)
                {
                    string cuadroClinico = " ";
                    listaDetalleEF = NegAnamnesisDetalle.listaDetallesAnamnesis(anamnesis.ANE_CODIGO);
                    listaCatalogo = NegCatalogos.RecuperarHcCatalogosPorTipo(Parametros.ReporteFormularios.codigoCatalogo_ExamenFisico);
                    for (int i = 0; i < listaDetalleEF.Count; i++)
                    {
                        detalle = listaDetalleEF.ElementAt(i);
                        for (int j = 0; j < listaCatalogo.Count; j++)
                        {
                            catalogo = listaCatalogo.ElementAt(j);
                            int codigo = (NegCatalogos.listaCatalogos().FirstOrDefault(c => c.EntityKey == detalle.HC_CATALOGOSReference.EntityKey).HCC_CODIGO);
                            if (catalogo.HCC_CODIGO == codigo)
                                cuadroClinico = cuadroClinico + "\n" + catalogo.HCC_NOMBRE + " :" + detalle.ADE_DESCRIPCION;
                        }
                    }
                    txtResumen.Text = anamnesis.ANE_PROBLEMA + "\t\t" + cuadroClinico;

                    txtTratamiento.Text = anamnesis.ANE_PLAN_TRATAMIENTO;
                    List<HC_ANAMNESIS_DIAGNOSTICOS> diag = NegAnamnesisDetalle.recuperarDiagnosticosAnamnesis(anamnesis.ANE_CODIGO);
                    if (diag != null)
                    {
                        foreach (HC_ANAMNESIS_DIAGNOSTICOS item in diag)
                        {
                            DataGridViewRow fila = new DataGridViewRow();
                            DataGridViewTextBoxCell textcell = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell textcell1 = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell textcell2 = new DataGridViewTextBoxCell();
                            DataGridViewCheckBoxCell chkcell = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell chkcell2 = new DataGridViewCheckBoxCell();
                            //textcell.Value = diagnosticos.CDA_CODIGO;
                            textcell1.Value = item.CIE_CODIGO;
                            textcell2.Value = item.CDA_DESCRIPCION;
                            if (item.CDA_ESTADO.Value)
                            {
                                chkcell.Value = true;
                                chkcell2.Value = false;
                            }
                            else
                            {
                                chkcell2.Value = true;
                                chkcell.Value = false;
                            }
                            //fila.Cells.Add(textcell);
                            fila.Cells.Add(textcell2);
                            fila.Cells.Add(textcell1);
                            //fila.Cells.Add(chkcell);
                            //fila.Cells.Add(chkcell2);
                            gridDiagnosticos.Rows.Add(fila);
                        }
                    }
                }
            }
            else
            {
                txtResumen.Text = epicrisis.EPI_CUADRO_CLINICO;
                //txt_evolucion.Text = epicrisis.EPI_EVOLUCION;
                txtHallazgos.Text = epicrisis.EPI_HALLAZGOS;
                txtTratamiento.Text = epicrisis.EPI_TRATAMIENTO;
                //txt_condiciones.Text = epicrisis.EPI_CONDICIONES_EGRESO;
                //txtRealizadoPor.Text = epicrisis.EPI_REALIZADO;
            }

        }

        private void frm_Referencia_Load(object sender, EventArgs e)
        {
            CargarReferencia();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddMedico_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtMedico, txtMedicoCOD);
        }
        private void cargarMedico(int codMedico, TextBox txtNombre, TextBox txtCOD)
        {
            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                //MEDICOS med = NegMedicos.MedicoID(codMedico);
                //if (medico.TIPO_MEDICO.TIM_CODIGO == 7)
                //{
                //    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                DataTable med = NegMedicos.MedicoIDValida(codMedico);
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtNombre.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                txtCOD.Text = Convert.ToString(medico.MED_CODIGO);
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                guardar();
                btnModificar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                gr1.Enabled = false;
                gr3.Enabled = false;
                txtResumen.Enabled = false;
                txtTratamiento.Enabled = false;
                btnImprimir.Enabled = true;
            }
        }

        private void guardar()
        {
            try
            {
                string[] x = new string[] {
                txtReferencia.Text,       //0
                dtpFecha.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),  //1
                txtEstablecimiento.Text,                                    //2
                txtServicio.Text,                                           //3
                txtMedicoCOD.Text,                                          //4
                txtMotivo.Text,                                             //5
                txtResumen.Text,                                            //6
                txtHallazgos.Text,                                          //7
                txtTratamiento.Text                                         //8
            };



                NegDietetica.setROW("DxReferencia", x);



                List<PedidoImagen_diagnostico> ListDiagnosticos = new List<PedidoImagen_diagnostico>();
                foreach (DataGridViewRow row in gridDiagnosticos.Rows)
                {
                    PedidoImagen_diagnostico estudio = new PedidoImagen_diagnostico();
                    estudio.CIE_CODIGO = Convert.ToString(row.Cells["CIE10"].Value);
                    if (Convert.ToString(row.Cells["TIPO"].Value) == "DEFINITIVO")
                        estudio.DEFINITIVO = 1;
                    else
                        estudio.DEFINITIVO = 0;
                    ListDiagnosticos.Add(estudio);
                }


                NegDietetica.saveDxReferencia(ListDiagnosticos, Convert.ToInt32(txtReferencia.Text));
                MessageBox.Show("Datos Almacenados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool validar()
        {
            if (txtEstablecimiento.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el establecimiento al que se envía la referencia.");
                return false;
            }
            if (txtServicio.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el servicio que refiere.");
                return false;
            }
            if (txtMedicoCOD.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor seleccione el médico.");
                return false;
            }
            if (txtMotivo.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el motivo de la referencia.");
                return false;
            }
            if (txtResumen.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el resumen del cuadro clínico .");
                return false;
            }
            if (txtHallazgos.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese 'Hallazgos relevantes de exámenes y procedimientos diagnósticos'.");
                return false;
            }
            if (txtTratamiento.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese 'Plan de tratamiento realizado'.");
                return false;
            }
            if (gridDiagnosticos.RowCount == 0)
            {
                MessageBox.Show("Por favor ingrese un diagnostico");
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            gr1.Enabled = false;
            gr3.Enabled = false;
            txtResumen.Enabled = false;
            txtTratamiento.Enabled = false;
            CargarReferencia();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            gr1.Enabled = true;
            gr3.Enabled = true;
            txtResumen.Enabled = true;
            txtTratamiento.Enabled = true;
        }

        private void btnAddDiagnotico_Click(object sender, EventArgs e)
        {
            //frm_ImagenAyuda ayuda = new frm_ImagenAyuda("DIAGNOSTICOS");
            //ayuda.ShowDialog();
            frm_BusquedaCIE10 busqueda = new frm_BusquedaCIE10();
            busqueda.ShowDialog();
            if (busqueda.codigo != string.Empty)
            {
                if (!BuscarItem(busqueda.codigo, gridDiagnosticos))
                    this.gridDiagnosticos.Rows.Add(busqueda.codigo, busqueda.resultado, "PRESUNTIVO");
                else
                    MessageBox.Show("El item ya fue ingresado.");
            }
        }

        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                try
                {
                    //limpiar talas
                    ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                    imagenL.DeleteTable("Referencia");
                    ReportesHistoriaClinica imagend = new ReportesHistoriaClinica();
                    imagend.DeleteTable("ReferenciaDx");
                    ReportesHistoriaClinica imagent = new ReportesHistoriaClinica();
                    imagent.limpiarImagenologiaDiagnostico();
                    //empaquetar y guardar en tablas access
                    DataTable X = NegDietetica.getDataTable("ReferenciaEncabezado", _ate_codigo.ToString());
                    ReportesHistoriaClinica EN = new ReportesHistoriaClinica();
                    EN.saveReferenciaEncabezado(X);

                    DataTable Y = NegDietetica.getDataTable("GetDxReferencia", txtReferencia.Text.Trim());



                    for (int i = 0; i < Y.Rows.Count; i++)
                    {
                        string definitivo = "";
                        string presuntivo = "";
                        if (Convert.ToString(Y.Rows[i]["DEFINITIVO"]) == ("True"))
                            definitivo = "X";
                        else
                            presuntivo = "X";
                        PedidoImagen_reporteDiagnosticos dx = new PedidoImagen_reporteDiagnosticos();
                        dx.diagnostico = Y.Rows[i]["CIE_DESCRIPCION"].ToString();
                        dx.CIE = Y.Rows[i]["CIE_CODIGO"].ToString();
                        dx.presuntivo = presuntivo;
                        dx.definitivo = definitivo;
                        dx.id_imagenologia = "0";

                        if (i < 3)
                        {
                            ReportesHistoriaClinica xx = new ReportesHistoriaClinica();
                            xx.ingresarReferenciaDx(dx);
                        }
                        else
                        {
                            ReportesHistoriaClinica xx = new ReportesHistoriaClinica();
                            xx.ingresarImagenologiaDiagnostico(dx);
                        }
                    }

                    //mostrar reporte
                    frmReportes ventana = new frmReportes(1, "Referencia");
                    ventana.Show();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private List<PedidoImagen_reporteDiagnosticos> empaquetarReporteDx(int idImagenologia)
        {
            List<PedidoImagen_reporteDiagnosticos> ListDx = new List<PedidoImagen_reporteDiagnosticos>();
            DataTable diagnosticos = NegImagen.getPedidoDiagnosticos(idImagenologia);
            foreach (DataRow row in diagnosticos.Rows)
            {
                PedidoImagen_reporteDiagnosticos dx = new PedidoImagen_reporteDiagnosticos();
                dx.diagnostico = row[1].ToString();
                dx.CIE = row[0].ToString();
                dx.id_imagenologia = idImagenologia.ToString();
                if ((row[2].ToString()) == "True")
                    dx.definitivo = "X";
                else
                    dx.presuntivo = "X";
                ListDx.Add(dx);
            }

            return ListDx;
        }
        private void refrescarSolicitudes()
        {
            gridSol.DataSource = NegDietetica.getReferencias(_ate_codigo);
            gridSol.Columns["ID"].Visible = false;
            gridSol.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            if (gridSol.RowCount > 0)
                btnImprimir.Enabled = true;
            else
                btnImprimir.Enabled = false;
        }

    }
}
