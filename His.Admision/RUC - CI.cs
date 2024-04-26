using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;

namespace His.Admision
{
    public partial class RUC___CI : Form
    {
        public RUC___CI()
        {
            InitializeComponent();
        }

        private void btn_consultar_Click(object sender, EventArgs e)
        {
            if (txt_ruc.Text != "" && txt_ruc.Text.Length == 13)
            {
                RUC sri = NegUtilitarios.ObtenerRUC(txt_ruc.Text);
                if (sri.consulta != null)
                {
                    txt_razonSocial.Text = sri.consulta[0].razonSocial;
                    txt_actividadEconomica.Text = sri.consulta[0].actividadEconomicaPrincipal;
                    txt_estado.Text = sri.consulta[0].estadoContribuyenteRuc;
                    txt_obligadoContabilidad.Text = sri.consulta[0].obligadoLlevarContabilidad;
                    txt_tipoContribuyente.Text = sri.consulta[0].tipoContribuyente;
                    txt_regimen.Text = sri.consulta[0].regimen;
                    txt_categoria.Text = sri.consulta[0].categoria;
                    txt_agente.Text = sri.consulta[0].agenteRetencion;
                    txt_contribuyenteEspecial.Text = sri.consulta[0].contribuyenteEspecial;
                    if (sri.consulta[0].representantesLegales != null)
                        txt_representante.Text = sri.consulta[0].representantesLegales[0].nombre;
                    else
                        txt_representante.Text = "";
                    txt_contibuyenteFantasma.Text = sri.consulta[0].contribuyenteFantasma;
                    txt_transaccionesExistentes.Text = sri.consulta[0].transaccionesInexistente;
                    txt_fechaInicio.Text = sri.consulta[0].informacionFechasContribuyente.fechaInicioActividades;
                    txt_fechaCese.Text = sri.consulta[0].informacionFechasContribuyente.fechaCese;
                    txt_fechaActualizacion.Text = sri.consulta[0].informacionFechasContribuyente.fechaActualizacion;
                    txt_fechaReinicio.Text = sri.consulta[0].informacionFechasContribuyente.fechaReinicioActividades;

                    DataTable dtDetalle = new DataTable();
                    UltraGridBand bandUno = gbv_establecimientos.DisplayLayout.Bands[0];
                    gbv_establecimientos.DataSource = dtDetalle;
                    dtDetalle.Columns.Add("Nombre Fantasia Comercial", Type.GetType("System.String"));
                    dtDetalle.Columns.Add("Tipo Establecimiento", Type.GetType("System.String"));
                    dtDetalle.Columns.Add("Dirección Completa", Type.GetType("System.String"));
                    dtDetalle.Columns.Add("Estado", Type.GetType("System.String"));
                    dtDetalle.Columns.Add("Numero Establecimiento", Type.GetType("System.String"));
                    dtDetalle.Columns.Add("Matriz", Type.GetType("System.String"));
                    bandUno.Columns["Nombre Fantasia Comercial"].Width = 200;
                    bandUno.Columns["Tipo Establecimiento"].Width = 150;
                    bandUno.Columns["Dirección Completa"].Width = 650;
                    bandUno.Columns["Estado"].Width = 100;
                    bandUno.Columns["Numero Establecimiento"].Width = 100;
                    bandUno.Columns["Matriz"].Width = 100;

                    txt_razonSocial.Visible = true;
                    txt_actividadEconomica.Visible = true;
                    txt_estado.Visible = true;
                    txt_obligadoContabilidad.Visible = true;
                    txt_tipoContribuyente.Visible = true;
                    txt_regimen.Visible = true;
                    txt_categoria.Visible = true;
                    txt_agente.Visible = true;
                    txt_contribuyenteEspecial.Visible = true;
                    txt_representante.Visible = true;
                    txt_contibuyenteFantasma.Visible = true;
                    txt_transaccionesExistentes.Visible = true;
                    txt_fechaInicio.Visible = true;
                    txt_fechaCese.Visible = true;
                    txt_fechaActualizacion.Visible = true;
                    txt_fechaReinicio.Visible = true;
                    btn_consultar.Visible = false;
                    btn_nueva.Visible = true;
                    txt_actividadEconomica.Visible = true;
                    txt_razonSocial.Visible = true;
                }
                else
                {
                    MessageBox.Show("RUC Consultado no tiene información, verificar que este correcto el RUC", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_ruc.Focus();
                }

            }
            else
            {
                MessageBox.Show("RUC incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ruc.Focus();
            }
        }

        public void Limpiar()
        {
            txt_razonSocial.Visible = false;
            txt_actividadEconomica.Visible = false;
            txt_estado.Visible = false;
            txt_obligadoContabilidad.Visible = false;
            txt_tipoContribuyente.Visible = false;
            txt_regimen.Visible = false;
            txt_categoria.Visible = false;
            txt_agente.Visible = false;
            txt_contribuyenteEspecial.Visible = false;
            txt_representante.Visible = false;
            txt_contibuyenteFantasma.Visible = false;
            txt_transaccionesExistentes.Visible = false;
            txt_miPyme.Visible = false;
            txt_fechaInicio.Visible = false;
            txt_fechaCese.Visible = false;
            txt_fechaActualizacion.Visible = false;
            txt_fechaReinicio.Visible = false;
            gbv_establecimientos.Visible = false;
            btn_consultar.Visible = true;
            btn_nueva.Visible = false;
            txt_ruc.Text = "";
            txt_actividadEconomica.Visible = false;
            txt_razonSocial.Visible = false;
        }

        private void btn_nueva_Click(object sender, EventArgs e)
        {
            Limpiar();

        }
    }
}
