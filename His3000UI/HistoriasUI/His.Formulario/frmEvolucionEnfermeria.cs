using Core.Datos;
using His.Entidades;
using His.General;
using His.Negocio;
using Infragistics.Win;
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
    public partial class frmEvolucionEnfermeria : Form
    {
        ATENCIONES atencion = null;
        PACIENTES pcte = null;
        public frmEvolucionEnfermeria(int nAtencion)
        {
            InitializeComponent();
            atencion = NegAtenciones.AtencionID(nAtencion);
            pcte = NegPacientes.recuperarPacientePorAtencion(nAtencion);
            actualizarGrid();
            CargarDatosPcte();
            dtpFecha.Value = DateTime.Now;
        }

        private void CargarDatosPcte()
        {
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
        }
        private void bloquearBotones(bool nuevo, bool editar, bool imprimir)
        {
            btnNuevo.Enabled = nuevo;
            btnModificar.Enabled = editar;
            btnImprimir.Enabled = imprimir;
        }

        private void actualizarGrid()
        {
            DataTable cargaGrid = NegDietetica.getDataTable("DetalleEvolucionEnfermeria", atencion.ATE_CODIGO.ToString());
            if (cargaGrid.Rows.Count == 0)
                bloquearBotones(true, false, false);
            gridNotasEvolucion.DataSource = cargaGrid;
            try
            {
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["NOTA"].CellMultiLine = DefaultableBoolean.True;
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["NOTA"].VertScrollBar = true;
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["NOTA"].Width = 400;
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["FECHA_REPORTE"].Width = 120;
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["FECHA_SISTEMA"].Width = 120;
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_CODIGO"].Hidden = true;
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["ID_USUARIO"].Hidden = true;
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["FECHA_REPORTE"].Format = "dd/MM/yyyy HH:mm:ss";
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["FECHA_SISTEMA"].Format = "dd/MM/yyyy HH:mm:ss";
                this.gridNotasEvolucion.DisplayLayout.Bands[0].Columns["URL"].Hidden = true;
            }
            catch (Exception)
            {

                //throw;
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDatos.Visible = true;
            bloquearBotones(false, false, false);

            dtpFecha.Value = DateTime.Now;
            txtNota.Text = "";
            txtEVD.Text = "NUEVO";


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios();
            usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            usuario.ShowDialog();
            if (!usuario.aceptado)
                return;
            USUARIOS nusuario = NegUsuarios.RecuperarUsuarioID(usuario.usuarioActual);

            if (txtNota.Text.Trim() != string.Empty)
            {

                string[] detalleEvo = new string[]{
                    atencion.ATE_CODIGO.ToString(),
                    //Entidades.Clases.Sesion.codUsuario.ToString(),
                    //Entidades.Clases.Sesion.nomUsuario.Trim(),
                    nusuario.ID_USUARIO.ToString(),
                    nusuario.APELLIDOS+" "+nusuario.NOMBRES,
                    dtpFecha.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"),
                    txtNota.Text
                };
                try
                {
                    NegDietetica.setROW("DetalleEvolucionEnfermeria", detalleEvo, txtEVD.Text.Trim());

                    actualizarGrid();

                    grpDatos.Visible = false;
                    bloquearBotones(true, true, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("! " + ex, "HIS-3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void gridNotasEvolucion_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridLayout layout = e.Layout;
            UltraGridOverride ov = layout.Override;
            ov.DefaultRowHeight = 50;
            ov.CellClickAction = CellClickAction.RowSelect;
            ov.RowSelectors = DefaultableBoolean.True;
            ov.RowSizing = RowSizing.Fixed;
            //ov.RowSizing = RowSizing.AutoFree;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            His.Formulario.frm_ClaveFormularios usuario = new frm_ClaveFormularios(true);
            usuario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            usuario.ShowDialog();

            if (!usuario.aceptado)
                return;

            if (Convert.ToString(usuario.usuarioActual) == gridNotasEvolucion.Rows[gridNotasEvolucion.ActiveRow.Index].Cells["ID_USUARIO"].Value.ToString().Trim())
            {
                dtpFecha.Value = Convert.ToDateTime(gridNotasEvolucion.Rows[gridNotasEvolucion.ActiveRow.Index].Cells["FECHA_REPORTE"].Value.ToString());
                txtNota.Text = gridNotasEvolucion.Rows[gridNotasEvolucion.ActiveRow.Index].Cells["NOTA"].Value.ToString();
                txtEVD.Text = gridNotasEvolucion.Rows[gridNotasEvolucion.ActiveRow.Index].Cells["EVD_CODIGO"].Value.ToString();


                grpDatos.Visible = true;
                btnNuevo.Enabled = false;
                btnModificar.Enabled = false;
            }
            else
            {
                MessageBox.Show("La entrada solo puede modificar el usuario que lo creo.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            grpDatos.Visible = false;
            actualizarGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DataTable dtEvolucion = NegDietetica.getDataTable("DetalleEvolucionEnfermeria", atencion.ATE_CODIGO.ToString());


            DSevolEnfermeria ds = new DSevolEnfermeria();
            #region Lleno dataset ds
            DataRow dr = ds.Tables["Paciente"].NewRow();
            if (!NegParametros.ParametroFormularios())
                dr["HC"] = txtHC.Text.Trim();
            else
                dr["HC"] = txtDNI.Text.Trim();
            dr["ATE_CODIGO"] = txtAteCodigo.Text.Trim();
            dr["NOMBRE"] = txtPaciente.Text.Trim();
            dr["EDAD"] = txtEdad.Text.Trim();
            dr["MEDICO"] = txtMedico.Text.Trim();
            dr["SEXO"] = txtSexo.Text.Trim();
            dr["IDENTIFICACION"] = txtDNI.Text.Trim();
            dr["FECHA_INGRESO"] = txtIngreso.Text.Trim();
            dr["HABITACION"] = txtHabitacion.Text.Trim();
            DataTable IO = NegDietetica.getDataTable("EMPRESA");
            dr["LOGO"] = IO.Rows[0]["EMP_PATHIMAGEN"].ToString();
            ds.Tables["Paciente"].Rows.Add(dr);
         
            foreach (DataRow row in dtEvolucion.Rows)
            {
                DataRow D = ds.Tables["Notas"].NewRow();
                D["HC"] = txtHC.Text.Trim();
                DateTime fec = Convert.ToDateTime(row["FECHA_REPORTE"].ToString());
                D["FECHA"] = (fec.Day).ToString().PadLeft(2, '0') + "-" + fec.Month.ToString().PadLeft(2, '0') + "-" + fec.Year;
                D["HORA"] = (Convert.ToDateTime(row["FECHA_REPORTE"].ToString())).ToString("HH:mm");
                D["NOTA"] = row["NOTA"].ToString();
                D["USUARIO"] = row["USUARIO"].ToString();
                D["URL"] = row["URL"].ToString();
                ds.Tables["Notas"].Rows.Add(D);
            }
            #endregion

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
            cabecera["edad"] = NegUtilitarios.EdadCalculada(Convert.ToDateTime(pcte.PAC_FECHA_NACIMIENTO), Convert.ToDateTime(dtEvolucion.Rows[0][4].ToString()));
            ds.Tables["Encabezado"].Rows.Add(cabecera);


        His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "FORM021", ds);
            myreport.Show();

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEvolucionEnfermeria_Load(object sender, EventArgs e)
        {

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFecha.Value > DateTime.Now)
            {
                dtpFecha.Value = DateTime.Now.Date;
            }
        }
    }
}
