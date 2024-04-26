using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using His.Entidades.Clases;
using Core.Entidades;

namespace His.Admision
{
    public partial class frm_Habitaciones : Form
    {
        
        #region Variables
        List<HABITACIONES> habitaciones = new List<HABITACIONES>();
        List<NIVEL_PISO> niveles = new List<NIVEL_PISO>();
        HABITACIONES habitacion = new HABITACIONES();
        public int codPaciente;
        public bool MuevePaciente;
        public bool AsignaPaciente;
        string observacionMovimiento;
        Int16 codhabitacionOcupada;
        TextBox campoPadre = null;
        #endregion

        #region Constructor
        public frm_Habitaciones()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_Habitaciones_Load(object sender, EventArgs e)
        {
            try
            {
                if (AsignaPaciente == true)
                {
                    mnAsignaHabitaciones.Visible = true;
                    mnuCambioEstado.Visible = false;
                    mnuColocaPaciente.Visible = false;
                    mnuMuevePaciente.Visible = false;
                    chk_encargado.Visible = true;
                }
                else
                {
                    mnAsignaHabitaciones.Visible = false;
                    mnuCambioEstado.Visible = true;
                    mnuColocaPaciente.Visible = true;
                    mnuMuevePaciente.Visible = true;
                    chk_encargado.Visible = false;

                }
                

                llenogrid();
                int k = 0;
                var datosgrafico = NegHabitaciones.ListaEstadosdeHabitacion().ToList();
                grid_informativo.Columns.Add("DESCRIPCION", "ESTADO");
                DataGridViewImageColumn imgColumna = new DataGridViewImageColumn();
                imgColumna.Name = "estado";
                imgColumna.HeaderText = " ";
                imgColumna.Image = imageList1.Images[0];
                grid_informativo.Columns.Insert(1, imgColumna);
                foreach (var dato in datosgrafico)
                {
                    grid_informativo.Rows.Add();
                    grid_informativo.Rows[k].Cells[0].Value = dato.HES_NOMBRE;
                    grid_informativo.Rows[k].Cells[1].Value = imageList1.Images[dato.HES_CODIGO];
                    k = k + 1;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
            
            
            
        }
        //private void grid_habitaciones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    //cargo datos de informacion de Habitacion

        //    DataGridViewCell cell = this.grid_habitaciones.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    if (e.Value != null && e.ColumnIndex != 0 && e.ColumnIndex != 1 && Validacolumnas(e.ColumnIndex))
        //    {
        //        if (recuperaNumerodeDetalleHabitacion(e.Value.ToString()) != 0)
        //        {

        //            var datoshabitacion = NegHabitaciones.RecuperarDetallesHabitacion(null, null, null, null, null, recuperaNumerodeDetalleHabitacion(e.Value.ToString()).ToString()).FirstOrDefault();
        //            if (datoshabitacion != null)
        //            {

        //                cell.ToolTipText = ("No Habitacion: " + datoshabitacion.hab_Numero + "\n"
        //                    + "Fecha Ingreso: " + datoshabitacion.HAD_FECHA_INGRESO.ToString() + "\n"
        //                    + "Fecha Alta: " + datoshabitacion.HAD_FECHA_ALTA_MEDICO.ToString() + "\n"
        //                    + "Fecha Disponibilidad: " + datoshabitacion.HAD_FECHA_DISPONIBILIDAD.ToString() + "\n"
        //                    + "Fecha Facturación: " + datoshabitacion.HAD_FECHA_FACTURACION + "\n"
        //                    + "No. Historia Clinica: " + datoshabitacion.PAC_HISTORIA_CLINICA + "\n"
        //                    + "Nombre Paciente: " + datoshabitacion.PACIENTE + "\n"
        //                    + "No. Atención: " + datoshabitacion.ATE_CODIGO + "\n"
        //                    + "Médico Tratante: " + datoshabitacion.MED_NOMBRE + "\n"
        //                    + "Especialidad: " + datoshabitacion.ESP_NOMBRE);
        //            }

        //        }
        //        else
        //        {
        //            cell.ToolTipText = ("No Habitacion: " + e.Value.ToString() + "\n"
        //                + "Fecha Ingreso: " + "\n"
        //                + "Fecha Alta: " + "\n"
        //                + "Fecha Disponibilidad: " + "\n"
        //                + "Fecha Facturación: " + "\n"
        //                + "No. Historia Clinica: " + "\n"
        //                + "Nombre Paciente: " + "\n"
        //                + "No. Atención: " + "\n"
        //                + "Médico Tratante: " + "\n"
        //                + "Especialidad: ");
        //        }

        //    }

        //    //}

        //}
        private void mnAsignaHabitaciones_Click(object sender, EventArgs e)
        {
            var habitacion = NegHabitaciones.listaHabitaciones().Where(hab => hab.hab_Numero == grid_habitaciones.Rows[grid_habitaciones.CurrentRow.Index].Cells[grid_habitaciones.CurrentCell.ColumnIndex].Value.ToString()).FirstOrDefault();
            if (habitacion != null)
            {
                Sesion.codHabitacion = habitacion.hab_Codigo;
                this.Close();
            }
        }
        private void grid_habitaciones_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                grid_habitaciones.CurrentCell = grid_habitaciones.Rows[e.RowIndex].Cells[e.ColumnIndex]; 

                if (grid_habitaciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && e.ColumnIndex != 0 && e.ColumnIndex != 1 && Validacolumnas(e.ColumnIndex))
                {
                    habitacion =  NegHabitaciones.listaHabitaciones().Where(hab => hab.hab_Numero == grid_habitaciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()).FirstOrDefault();
                    if (AsignaPaciente == true)
                    {

                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 6) // habilito el menu de asignacion de habitacion
                            mnAsignaHabitaciones.Enabled = true;
                        else
                            mnAsignaHabitaciones.Enabled = false;
                    }
                    else
                    {
                        if (MuevePaciente == true) // habilito los menues para cambio de habitacion al paciente
                        {
                            if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 6)
                            {
                                mnuMuevePaciente.Enabled = false;
                                mnuColocaPaciente.Enabled = true;
                            }
                            else
                            {
                                mnuMuevePaciente.Enabled = false;
                                mnuColocaPaciente.Enabled = false;
                            }
                        }
                        else
                        {
                            if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 1)
                            {
                                mnuMuevePaciente.Enabled = true;
                                mnuColocaPaciente.Enabled = false;
                                //chk_encargado.Visible = true;
                            }
                            else
                            {
                                mnuMuevePaciente.Enabled = false;
                                mnuColocaPaciente.Enabled = false;
                                //chk_encargado.Visible = false;
                            }
                        }
                        if (habitacion.HABITACIONES_ESTADO.HES_CODIGO == 2 || habitacion.HABITACIONES_ESTADO.HES_CODIGO == 6)
                        {
                            mnuCambioEstado.Enabled = false;
                        }
                        else
                            mnuCambioEstado.Enabled = true;

                    }

                    mnuContextHab.Show(grid_habitaciones, new Point(MousePosition.X - grid_habitaciones.Left, e.Y));
                }
            }
        }
        private void mnuMuevePaciente_Click(object sender, EventArgs e)
        {
            //int ultimodetalle = NegHabitaciones.UltimoDetalle(grid_habitaciones.Rows[grid_habitaciones.CurrentRow.Index].Cells[grid_habitaciones.CurrentCell.ColumnIndex].Value.ToString());
            //if (ultimodetalle != 0)
            //{
            //    string value = "";
            //    if (InputBox("Habitaciones", "Observación de Cambio de Habitación: ", ref value) == DialogResult.OK)
            //    {
            //        observacionMovimiento = value.ToUpper();
            //    }
            //    // busco el paciente que esta asignado a esa habitacion

            //    var datoshabitacion = NegHabitaciones.RecuperarDetallesHabitacion(null, null, null, null, null,ultimodetalle.ToString(),null,null,null).FirstOrDefault();
            //    if (datoshabitacion != null)
            //    {
            //        codPaciente = int.Parse(datoshabitacion.PAC_HISTORIA_CLINICA);
            //        codhabitacionOcupada = datoshabitacion.hab_Codigo;
            //        MuevePaciente = true;
            //    }
            //    chk_encargado.Visible = true;
            //}
        }
        private void mnuColocaPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado;
                resultado = MessageBox.Show("Desea Colocar en esta habitación al Paciente ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    var paciente = NegPacientes.RecuperarPacientesLista().Where(cod => cod.PAC_HISTORIA_CLINICA.Replace(" ", "") == codPaciente.ToString()).FirstOrDefault();
                    ATENCIONES pacienteAtencionOrig = new ATENCIONES();

                    ATENCIONES pacienteAtencion = NegAtenciones.listaAtenciones().Where(c => c.ATE_CODIGO == recuperaUltimaAtencionPaciente(paciente.PAC_CODIGO.ToString())).FirstOrDefault();
                    pacienteAtencionOrig = pacienteAtencion.ClonarEntidad();
                    HABITACIONES hab = NegHabitaciones.listaHabitaciones().Where(c => c.hab_Numero == grid_habitaciones.Rows[grid_habitaciones.CurrentRow.Index].Cells[grid_habitaciones.CurrentCell.ColumnIndex].Value.ToString()).FirstOrDefault();
                    pacienteAtencion.HABITACIONESReference.EntityKey = hab.EntityKey;
                    //pacienteAtencion.HAB_CODIGO  = hab.hab_Codigo;
                    NegAtenciones.GrabarAtencion(pacienteAtencion, pacienteAtencionOrig); // grabo el cambio de habitacion en la atencion

                    HABITACIONES habantigua = NegHabitaciones.listaHabitaciones().Where(c => c.hab_Codigo == codhabitacionOcupada).FirstOrDefault();

                    HABITACIONES_DETALLE detHabitacion = new HABITACIONES_DETALLE();
                    detHabitacion.HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1;
                    detHabitacion.HABITACIONESReference.EntityKey = habantigua.EntityKey;
                    detHabitacion.ID_USUARIO = Sesion.codUsuario;
                    detHabitacion.ATE_CODIGO = pacienteAtencion.ATE_CODIGO;
                    detHabitacion.HAD_FECHA_DISPONIBILIDAD = DateTime.Now;
                    detHabitacion.HAD_OBSERVACION = observacionMovimiento;
                    NegHabitaciones.CrearHabitacionDetalle(detHabitacion); // grabo detalle de habitacion a desocupada

                    detHabitacion = new HABITACIONES_DETALLE();
                    detHabitacion.HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1;
                    detHabitacion.HABITACIONESReference.EntityKey = hab.EntityKey;
                    detHabitacion.ID_USUARIO = Sesion.codUsuario;
                    detHabitacion.ATE_CODIGO = pacienteAtencion.ATE_CODIGO;
                    detHabitacion.HAD_FECHA_INGRESO = DateTime.Now;
                    detHabitacion.HAD_REGISTRO_ANTERIOR = codhabitacionOcupada;
                    detHabitacion.HAD_ENCARGADO = chk_encargado.Checked;
                    detHabitacion.HAD_OBSERVACION = observacionMovimiento;
                    NegHabitaciones.CrearHabitacionDetalle(detHabitacion); // grabo detalle de habitacion a ocupar


                    NIVEL_PISO nivel = NegHabitaciones.listaNivelesPiso().Where(c => c.NIV_CODIGO == habantigua.NIVEL_PISO.NIV_CODIGO).FirstOrDefault();


                    HABITACIONES habAnt = new HABITACIONES();
                    HABITACIONES habAntnuevoEstado = new HABITACIONES();
                    HABITACIONES habNue = new HABITACIONES();
                    HABITACIONES habNueEstado = new HABITACIONES();
                    HABITACIONES_ESTADO estadoOrig = new HABITACIONES_ESTADO();
                    HABITACIONES_ESTADO estadoModif = new HABITACIONES_ESTADO();

                    habAnt.hab_Codigo = habantigua.hab_Codigo;
                    habAnt.NIVEL_PISOReference.EntityKey = nivel.EntityKey;
                    estadoOrig = NegHabitaciones.ListaEstadosdeHabitacion().Where(c => c.HES_CODIGO == habantigua.HABITACIONES_ESTADO.HES_CODIGO).FirstOrDefault();
                    habAnt.HABITACIONES_ESTADOReference.EntityKey = estadoOrig.EntityKey; //habantigua.HABITACIONES_ESTADO.HES_CODIGO;
                    habAnt.hab_Numero = habantigua.hab_Numero;
                    habAnt.hab_Activo = habantigua.hab_Activo;
                    habAnt.hab_Padre = habantigua.hab_Padre;
                    habAnt.EntityKey = habantigua.EntityKey;
                    habAntnuevoEstado = habAnt.ClonarEntidad();
                    estadoModif = NegHabitaciones.ListaEstadosdeHabitacion().Where(c => c.HES_CODIGO == 4).FirstOrDefault();
                    habAntnuevoEstado.HABITACIONES_ESTADOReference.EntityKey=estadoModif.EntityKey;
                    NegHabitaciones.GrabarHabitaciones(habAntnuevoEstado, habAnt); // cambio de estado a la habitacion desocupada


                    NIVEL_PISO nivelAsig = NegHabitaciones.listaNivelesPiso().Where(c => c.NIV_CODIGO == hab.NIVEL_PISO.NIV_CODIGO).FirstOrDefault();

                    habNue.hab_Codigo = hab.hab_Codigo;
                    habNue.hab_Activo = hab.hab_Activo;
                    estadoOrig = NegHabitaciones.ListaEstadosdeHabitacion().Where(c => c.HES_CODIGO == hab.HABITACIONES_ESTADO.HES_CODIGO).FirstOrDefault();
                    habNue.HABITACIONES_ESTADOReference.EntityKey =estadoOrig.EntityKey;
                    habNue.hab_Numero = hab.hab_Numero;
                    habNue.hab_Padre = hab.hab_Padre;
                    habNue.NIVEL_PISOReference.EntityKey = nivelAsig.EntityKey;
                    habNue.EntityKey = hab.EntityKey;
                    habNueEstado = habNue.ClonarEntidad();
                    estadoModif = NegHabitaciones.ListaEstadosdeHabitacion().Where(c => c.HES_CODIGO ==1).FirstOrDefault();
                    habNueEstado.HABITACIONES_ESTADOReference.EntityKey= estadoModif.EntityKey;

                    NegHabitaciones.GrabarHabitaciones(habNueEstado, habNue); // cambio estado a la habitacion asignada al paciente

                    llenogrid();
                    MuevePaciente = false;
                    chk_encargado.Checked = false;
                    chk_encargado.Visible = false;
                    MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void grid_habitaciones_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            llenainformacion();
        }
        private void mnuCambioEstado_Click(object sender, EventArgs e)
        {

            frm_EstadoHabitaciones frm = new frm_EstadoHabitaciones();
            int fila = grid_habitaciones.CurrentRow.Index;
            int columna = grid_habitaciones.CurrentCell.ColumnIndex;
            frm.codhabitacion = grid_habitaciones.Rows[fila].Cells[columna].Value.ToString();
            frm.ShowDialog();
            llenogrid();
            grid_habitaciones.CurrentCell = grid_habitaciones.Rows[fila].Cells[columna];
            llenainformacion();

        }
        #endregion

        #region Metodos privados
        //lleno grid con los datos de las habitaciones
        private void llenogrid()
        {
            grid_habitaciones.Rows.Clear();
            grid_habitaciones.Columns.Clear();
            int i = 0;
            int j = 0;
            ////HMergedCell pCell;
            int nOffset;
            //agrego columna al grid

            DataGridViewColumn a = new DataGridViewColumn();
            a.Name = "NIV_NUMERO_PISO";
            a.HeaderText = "PISO";
            grid_habitaciones.Columns.Add(a.Name, a.HeaderText);
            grid_habitaciones.Columns[0].Width = 50;
            a.Name = "NIV_NOMBRE";
            a.HeaderText = "NIVEL";
            grid_habitaciones.Columns.Add(a.Name, a.HeaderText);
            niveles = NegHabitaciones.listaNivelesPiso();

            habitaciones = NegHabitaciones.listaHabitaciones().ToList();
            //grid_habitaciones.Rows.Add();
            //grid_habitaciones.Rows[0].Cells[0].Value = "PISO";
            //grid_habitaciones.Rows[0].Cells[1].Value = "NIVEL";
            nOffset = 2;
            //i = i + 1;
            foreach (var acceso in niveles) //cargo los niveles
            {


                grid_habitaciones.Rows.Add();
                grid_habitaciones.Rows[i].Cells[0].Value = acceso.NIV_NUMERO_PISO.ToString();

                grid_habitaciones.Rows[i].Cells[1].Value = acceso.NIV_NOMBRE.ToString();

                var habitacionespiso = habitaciones.Where(cod => cod.NIVEL_PISO.NIV_CODIGO == acceso.NIV_CODIGO).ToList();
                j = 0;
                foreach (var hab in habitacionespiso) // cargo las habitaciones por nivel
                {

                    if (grid_habitaciones.Columns.Count <= j + 2)//agrego columnas al grid
                    {
                        a.Name = "habitaciones";
                        a.HeaderText = "HABITACIONES";
                        grid_habitaciones.Columns.Add(a.Name, a.HeaderText);
                        grid_habitaciones.Columns[j + 2].Width = 35;
                        //grid_habitaciones.Rows[0].Cells[j + 2].Value = "HABITACIONES";
                        DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();


                        iconColumn.Name = "estado";
                        iconColumn.HeaderText = "HABITACIONES";
                        iconColumn.Image = imageList1.Images[0];
                        grid_habitaciones.Columns.Insert(j + 3, iconColumn);
                        grid_habitaciones.Columns[j + 3].Width = 20;
                        //grid_habitaciones.Rows[0].Cells[j + 3].Value = "HABITACIONES";

                    }
                    grid_habitaciones.Rows[i].Cells[j + 2].Value = hab.hab_Numero.ToString();
                    //if (hab.hab_Estado == "L")
                    //    grid_habitaciones.Rows[i].Cells[j + 3].Value = imageList1.Images[2];
                    ////iconColumn.Image = imageList1.Images[0]; //treeIcon.ToBitmap();
                    //else if (hab.hab_Estado == "A")
                    //    grid_habitaciones.Rows[i].Cells[j + 3].Value = imageList1.Images[1];
                    //else
                        grid_habitaciones.Rows[i].Cells[j + 3].Value = imageList1.Images[hab.HABITACIONES_ESTADO.HES_CODIGO];

                    j = j + 2;
                }

                i = i + 1;


            }

            
        }
        // llena informacion
        private void llenainformacion()
        {
            //DataGridViewCell cell = this.grid_habitaciones.Rows[grid_habitaciones.CurrentRow.Index].Cells[grid_habitaciones.CurrentCell.ColumnIndex];
            //if (grid_habitaciones.CurrentCell.Value != null && grid_habitaciones.CurrentCell.ColumnIndex != 0 && grid_habitaciones.CurrentCell.ColumnIndex != 1 && Validacolumnas(grid_habitaciones.CurrentCell.ColumnIndex))
            //{
            //    int ultimodetalle = NegHabitaciones.UltimoDetalle(grid_habitaciones.CurrentCell.Value.ToString());
            //    var datHabitacion = NegHabitaciones.listaHabitaciones().Where(c => c.hab_Numero == grid_habitaciones.CurrentCell.Value.ToString()).FirstOrDefault();
            //    if (ultimodetalle != 0)
            //    {

            //        if (datHabitacion.HABITACIONES_ESTADO.HES_CODIGO == 1 || datHabitacion.HABITACIONES_ESTADO.HES_CODIGO == 2 || datHabitacion.HABITACIONES_ESTADO.HES_CODIGO == 3 || datHabitacion.HABITACIONES_ESTADO.HES_CODIGO == 4)
            //        {
            //            var datoshabitacion = NegHabitaciones.RecuperarDetallesHabitacion(null, null, null, null, null, ultimodetalle.ToString(),null,null,null).FirstOrDefault();
            //            if (datoshabitacion != null)
            //            {

            //                txt_infhabitacion.Text = datoshabitacion.hab_Numero;
            //                txt_inffecingreso.Text = datoshabitacion.HAD_FECHA_INGRESO.ToString();
            //                txt_inffecalta.Text = datoshabitacion.HAD_FECHA_ALTA_MEDICO.ToString();
            //                txt_inffecdisponible.Text = datoshabitacion.HAD_FECHA_DISPONIBILIDAD.ToString();
            //                txt_inffecfacturacion.Text = datoshabitacion.HAD_FECHA_FACTURACION.ToString();
            //                txt_infhistoriaclinica.Text = datoshabitacion.PAC_HISTORIA_CLINICA;
            //                txt_infpaciente.Text = datoshabitacion.PACIENTE;
            //                txt_infatencion.Text = datoshabitacion.ATE_CODIGO.ToString();
            //                txt_infmedico.Text = datoshabitacion.MED_NOMBRE;
            //                txt_infespecialidad.Text = datoshabitacion.ESP_NOMBRE;
            //                var dethabitacion = NegHabitaciones.DetalleHabitacion().Where(c => c.HAD_CODIGO == datoshabitacion.HAD_CODIGO).FirstOrDefault();
            //                chk_infencargado.Checked = dethabitacion.HAD_ENCARGADO==null?false:bool.Parse(dethabitacion.HAD_ENCARGADO.ToString());
            //            }
            //        }
            //        else
            //        {
            //            var dethabitacion = NegHabitaciones.DetalleHabitacion().Where(c => c.HAD_CODIGO == ultimodetalle).FirstOrDefault();
            //            txt_infhabitacion.Text = datHabitacion.hab_Numero;
            //            txt_inffecingreso.Text = string.Empty;
            //            txt_inffecalta.Text = string.Empty;
            //            txt_inffecdisponible.Text = dethabitacion.HAD_FECHA_DISPONIBILIDAD.ToString();
            //            txt_inffecfacturacion.Text = string.Empty;
            //            txt_infhistoriaclinica.Text = string.Empty;
            //            txt_infpaciente.Text = string.Empty;
            //            txt_infatencion.Text = string.Empty;
            //            txt_infmedico.Text = string.Empty;
            //            txt_infespecialidad.Text = string.Empty;
            //            chk_infencargado.Checked = false;
            //        }

            //    }
            //    else
            //    {
            //        txt_infhabitacion.Text =  grid_habitaciones.CurrentCell.Value.ToString();
            //        txt_inffecingreso.Text = string.Empty;
            //        txt_inffecalta.Text = string.Empty;
            //        txt_inffecdisponible.Text = string.Empty;
            //        txt_inffecfacturacion.Text = string.Empty;
            //        txt_infhistoriaclinica.Text = string.Empty;
            //        txt_infpaciente.Text = string.Empty;
            //        txt_infatencion.Text = string.Empty;
            //        txt_infmedico.Text = string.Empty;
            //        txt_infespecialidad.Text = string.Empty;
            //        chk_infencargado.Checked = false;
            //    }

            //}
        }
        //recupero la ultima atencion del paciente
        public int recuperaUltimaAtencionPaciente(string codpaciente)
        {
            var detallehabitacion = NegHabitaciones.RecuperarDetallesHabitacion(null, null, null, codpaciente, null, null,null,null,null).ToList();
            if (detallehabitacion.Count != 0)
                return (int)NegHabitaciones.RecuperarDetallesHabitacion(null, null, null, codpaciente, null, null,null,null,null).Max(a => a.ATE_CODIGO);
            else
                return 0;
        }
        // valido fila con grafico o numero de habitacion
        public bool Validacolumnas(int columna)
        {
            double colum = columna;
            double resultado=0;
            double residuo = 0;
            resultado = colum / 2;
            residuo = resultado - double.Parse(resultado.ToString("###0"));
            if (residuo==0)
                return true;
            else
                return false;
        }
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        public class GroupByGrid : DataGridView
        {
            protected override void OnCellFormatting(

               DataGridViewCellFormattingEventArgs args)
            {

                // Call home to base

                base.OnCellFormatting(args);



                // First row always displays

                if (args.RowIndex == 0)

                    return;





                if (IsRepeatedCellValue(args.RowIndex, args.ColumnIndex))
                {

                    args.Value = string.Empty;

                    args.FormattingApplied = true;

                }

            }



            private bool IsRepeatedCellValue(int rowIndex, int colIndex)
            {

                DataGridViewCell currCell =

                   Rows[rowIndex].Cells[colIndex];

                DataGridViewCell prevCell =

                   Rows[rowIndex - 1].Cells[colIndex];



                if ((currCell.Value == prevCell.Value) ||

                   (currCell.Value != null && prevCell.Value != null &&

                   currCell.Value.ToString() == prevCell.Value.ToString()))
                {

                    return true;

                }

                else
                {

                    return false;

                }

            }



            protected override void OnCellPainting(

               DataGridViewCellPaintingEventArgs args)
            {

                base.OnCellPainting(args);



                args.AdvancedBorderStyle.Bottom =

                   DataGridViewAdvancedCellBorderStyle.None;



                // Ignore column and row headers and first row

                if (args.RowIndex < 1 || args.ColumnIndex < 0)

                    return;



                if (IsRepeatedCellValue(args.RowIndex, args.ColumnIndex))
                {

                    args.AdvancedBorderStyle.Top =

                       DataGridViewAdvancedCellBorderStyle.None;

                }

                else
                {

                    args.AdvancedBorderStyle.Top = AdvancedCellBorderStyle.Top;

                }

            }

        }
        #endregion

        private void grid_habitaciones_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string valorFila = grid_habitaciones.CurrentCell.Value.ToString();

            MessageBox.Show(valorFila);
        }

        

       

        
        

       
        

        

    }
}
