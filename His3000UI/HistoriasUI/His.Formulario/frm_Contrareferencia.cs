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
    public partial class frm_Contrareferencia : Form
    {
        int _ate_codigo;
        int _id_usuario;

        public frm_Contrareferencia(int ATE_CODIGO)
        {
            _ate_codigo = ATE_CODIGO;
          
            InitializeComponent();
            // USUARIOS u = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            _id_usuario = His.Entidades.Clases.Sesion.codUsuario;
            refrescarSolicitudes();

            //CAmbios Edgar 20210303 Requerimientos de la pasteur por Alex
            if (His.Entidades.Clases.Sesion.codDepartamento == 6)
            {
                HabilitarBotones(false, false, false, false, false, false);
            }
        }


        public void CargarReferencia()
        {
            
            DataTable x = NegDietetica.getDataTable("GetContrareferencia", _ate_codigo.ToString(), _id_usuario.ToString());
            txtReferencia.Text = Convert.ToString(x.Rows[0]["Id"]);
            dtpFecha.Value = Convert.ToDateTime(x.Rows[0]["FECHA"]);
            txtEstablecimiento.Text = Convert.ToString(x.Rows[0]["ESTABLECIMIENTO"]);
            txtMedicoCOD.Text = Convert.ToString(x.Rows[0]["MED_CODIGO"]);
            if (txtMedicoCOD.Text != string.Empty)
            {
                cargarMedico(Convert.ToInt32(x.Rows[0]["MED_CODIGO"]), txtMedico, txtMedicoCOD);
            }
            txtServicio.Text = Convert.ToString(x.Rows[0]["SERVICIO"]);
            txtResumen.Text = Convert.ToString(x.Rows[0]["RESUMEN"]);
            txtHallazgos.Text = Convert.ToString(x.Rows[0]["HALLAZGOS"]);
            txtTratamientoRealizado.Text = Convert.ToString(x.Rows[0]["TRATAMIENTO_REALIZADO"]);
            txtTratamientoRecomendado.Text = Convert.ToString(x.Rows[0]["TRATAMIENTO_RECOMENDADO"]);
            gridDiagnosticos.Rows.Clear();

            DataTable diagnosticos = NegDietetica.getDataTable("GetDxContrareferencia", txtReferencia.Text.Trim());

            foreach (DataRow row in diagnosticos.Rows)
            {
                string aux;
                if ((row[2].ToString()) == "True")
                    aux = "DEFINITIVO";
                else
                    aux = "PRESUNTIVO";

                this.gridDiagnosticos.Rows.Add(row[0].ToString(), row[1].ToString(), aux);
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
                MEDICOS med = NegMedicos.MedicoID(codMedico);
                if (!med.MED_ESTADO)
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
                HabilitarBotones(true, false, true, true, false, false);
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
                txtResumen.Text,                                             //5
                txtHallazgos.Text,                                            //6
                txtTratamientoRealizado.Text,                                          //7
                txtTratamientoRecomendado.Text                                         //8
            };



                NegDietetica.setROW("DxContrareferencia", x);



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


                NegDietetica.saveDxContrareferencia(ListDiagnosticos, Convert.ToInt32(txtReferencia.Text));
                MessageBox.Show("Datos Almacenados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
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
            if (txtResumen.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese el resumen del cuadro clínico.");
                return false;
            }
            if (txtHallazgos.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese 'Hallazgos relevantes de exámenes y procedimientos diagnósticos'.");
                return false;
            } 
            if (txtTratamientoRealizado.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese 'Tratamiento y procedimientos terapéuticos realizados'.");
                return false;
            }
            if (txtTratamientoRecomendado.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Por favor ingrese 'Plan de tratamiento realizado'.");
                return false;
            }
            if (gridDiagnosticos.RowCount == 0)
            {
                MessageBox.Show("Por favor ingrese un diagnóstico");
                return false;
            }
          return true;    
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(true, false, false, true, false, false);
            CargarReferencia();
        }
        public void HabilitarBotones(bool nuevo, bool guardar, bool modificar, bool imprimir, bool cancelar, bool paneles)
        {
            btnnuevo.Enabled = nuevo;
            btnModificar.Enabled = modificar;
            btnGuardar.Enabled = guardar;
            btnCancelar.Enabled = cancelar;
            btnImprimir.Enabled = imprimir;
            gr1.Enabled = paneles;
            gr2.Enabled = paneles;
            gr3.Enabled = paneles;
        }
        private void btnModificar_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, false, false, true, true);
        }

        private void btnAddDiagnotico_Click(object sender, EventArgs e)
        {
            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("DIAGNOSTICOS");
            ayuda.ShowDialog();
            if (ayuda.codigo != string.Empty)
            {
                if (!BuscarItem(ayuda.codigo, gridDiagnosticos))
                    this.gridDiagnosticos.Rows.Add(ayuda.codigo, ayuda.producto, "PRESUNTIVO");
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
                    DataTable X = NegDietetica.getDataTable("ContrareferenciaEncabezado",_ate_codigo.ToString());
                    ReportesHistoriaClinica EN = new ReportesHistoriaClinica();
                    EN.saveReferenciaEncabezado(X);

                    DataTable Y = NegDietetica.getDataTable("GetDxContrareferencia", txtReferencia.Text.Trim());

                  

                    for (int i = 0; i < Y.Rows.Count; i++)
                    {
                        string definitivo = "";
                        string presuntivo = "";
                        if (Convert.ToString(Y.Rows[i]["DEFINITIVO"])==("True"))
                            definitivo = "X";
                        else
                            presuntivo = "X";
                        PedidoImagen_reporteDiagnosticos dx = new PedidoImagen_reporteDiagnosticos();
                        dx.diagnostico = Y.Rows[i]["CIE_DESCRIPCION"].ToString();
                        dx.CIE = Y.Rows[i]["CIE_CODIGO"].ToString();
                        dx.presuntivo = presuntivo;
                        dx.definitivo = definitivo;
                        dx.id_imagenologia = "0";

                        if (i<3)
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
                    frmReportes ventana = new frmReportes(1, "Contrareferencia");
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

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(false, true, false, false, true, true);
        }

        private void gridSol_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Desea cargar la ContraReferencia?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                //limpiarCampos();
                //hin_codigo = Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["ID"].Value.ToString());
                //CargarUltimaInterconsulta();
                //ValidarCerrado();
                ////controles_prepararedicion();
                ////cargarPedido(Convert.ToInt32(gridSol.Rows[gridSol.CurrentRow.Index].Cells["id"].Value));
                ////controles_visualizaPedido();
                //NegAtenciones atenciones = new NegAtenciones();
                //string estado = atenciones.EstadoCuenta(Convert.ToString(ate_codigo));
                //if (estado != "1")
                //{
                //    Bloquear();
                //}
                //ValidarEnfermeria();
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
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
