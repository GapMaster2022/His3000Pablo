using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using His.Entidades.Clases;
using His.Parametros;
using MessageBox = System.Windows.Forms.MessageBox;


namespace His.Admision
{
    public partial class FrmRevertirSalida : Form
    {
        public bool estado;
        public string habitacion;
        public ATENCIONES Atencion { get; set; }

        public FrmRevertirSalida()
        {
            InitializeComponent();
            estado = false;
        }

        private void frm_MoverPacienteHabitacion_Load(object sender, EventArgs e)
        {
            CargarCombos();
        }


        private void CargarCombos()
        {
            cbm_Piso.DataSource = NegHabitaciones.listaNivelesPiso().OrderBy(n => n.NIV_NOMBRE).ToList(); ;
            cbm_Piso.ValueMember = "NIV_CODIGO";
            cbm_Piso.DisplayMember = "NIV_NOMBRE";
        }

        private void CmbPisoTabIndexChanged(object sender, EventArgs e)
        {
            //    try
            //    {
            //        if (cmb_Piso1.SelectedRow!= null)
            //        {
            //            short codNivel = Convert.ToInt16(cmb_Piso1.ValueMember);
            //            var listaHabitaciones = NegHabitaciones.listaHabitaciones(codNivel, Parametros.AdmisionParametros.getEstadoHabitacionDisponible());
            //            cmb_HabitacionCambio.DataSource = listaHabitaciones;
            //            cmb_HabitacionCambio.DisplayMember = "hab_Numero";
            //        }
            //    }
            //    catch (Exception err)
            //    {
            //        MessageBox.Show(err.Message);
            //    }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ultraTextEditor1.Text != "" && cmb_HabitacionCambio.Text != "")
            {
                MessageBoxResult resultado = System.Windows.MessageBox.Show("Desea guardar los cambios", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    try
                    {
                        var habitacionSelecionada = (HABITACIONES)cmb_HabitacionCambio.SelectedItem;
                        if (NegHabitaciones.CambiarEstadoHabitacion(habitacionSelecionada))
                        {
                            ////creo el nuevo detalle
                            int had_codigo;
                            var habitacionDetalle = new HABITACIONES_DETALLE
                            {
                                HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1,
                                ATE_CODIGO = this.Atencion.ATE_CODIGO,
                                HABITACIONESReference = { EntityKey = habitacionSelecionada.EntityKey },
                                ID_USUARIO = Sesion.codUsuario,
                                HAD_FECHA_INGRESO = DateTime.Now,
                                HAD_OBSERVACION = ultraTextEditor1.Text
                            };
                            had_codigo = habitacionDetalle.HAD_CODIGO;

                            //Cambios Edgar 20210305
                            //Creo la historia de habitaciones
                            int hah_codigo;
                            HABITACIONES_HISTORIAL habitacionHistorial = new HABITACIONES_HISTORIAL();
                            habitacionHistorial.HAH_CODIGO = NegHabitacionesHistorial.RecuperaMaximoHabitacionHistorial();
                            habitacionHistorial.ATE_CODIGO = this.Atencion.ATE_CODIGO;
                            habitacionHistorial.HAB_CODIGO = habitacionSelecionada.hab_Codigo;
                            habitacionHistorial.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                            habitacionHistorial.HAH_FECHA_INGRESO = DateTime.Now;
                            habitacionHistorial.HAD_OBSERVACION = ultraTextEditor1.Text;
                            habitacionHistorial.HAH_ESTADO = 1;
                            hah_codigo = habitacionHistorial.HAH_CODIGO;

                            NegHabitaciones.CrearHabitacionDetalle(habitacionDetalle);

                            //SE PONE FECHA ALTA EN HISTORIAL DE HABITACIONES PARA CALCULO DE 
                            //VALORES AUTOMATICOS DE UCI
                            //PABLURAS 31/08/2021
                            NegHabitacionesHistorial.FechaAltaHistorial(this.Atencion.ATE_CODIGO);
                            NegHabitacionesHistorial.CrearHabitacionHistorial(habitacionHistorial);
                            //Consultar que tipo de Ingreso es el original Cambios 20210505 Edgar 
                            NegHabitacionesHistorial.HabHistorialArea(Atencion.ATE_CODIGO, hah_codigo, had_codigo);

                            //--------------------------------------------------------

                            //actualizo la atencion
                            this.Atencion.ATE_FECHA_ALTA = null;
                            this.Atencion.ATE_ESTADO = true;
                            this.Atencion.HABITACIONESReference.EntityKey = habitacionSelecionada.EntityKey;
                            NegHabitaciones.CambiaEstadoHabitacion(this.Atencion.ATE_CODIGO, habitacionSelecionada.hab_Codigo);
                            //NegAtenciones.EditarAtencionAdmision(this.Atencion, 0);
                            //actualizo el estado de la habitacion
                            habitacionSelecionada.HABITACIONES_ESTADOReference.EntityKey = NegHabitaciones.RecuperarEstadoHabitacion(AdmisionParametros.getEstadoHabitacionOcupado()).EntityKey;

                            estado = true;
                            habitacion = cmb_HabitacionCambio.Text;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("La habitacion ya ha sido ocupada.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                    }
                    catch (Exception err)
                    {
                        System.Windows.MessageBox.Show(err.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese Todos los Campos", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmb_Piso_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cmb_Piso1.SelectedRow.Index >=0)
            //    {
            //        short codNivel = Convert.ToInt16(cmb_Piso1.ValueMember);
            //        var listaHabitaciones = NegHabitaciones.listaHabitaciones(codNivel, Parametros.AdmisionParametros.getEstadoHabitacionDisponible());
            //        cmb_HabitacionCambio.DataSource = listaHabitaciones;
            //        cmb_HabitacionCambio.DisplayMember = "hab_Numero";
            //    }
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}
        }

        private void cmb_Piso_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbm_Piso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmb_HabitacionCambio.DataSource = null;
                if (cbm_Piso.SelectedItem != null)
                {
                    string s = cbm_Piso.ValueMember;
                    short codNivel = Convert.ToInt16(((NIVEL_PISO)(cbm_Piso.SelectedItem)).NIV_CODIGO);
                    var listaHabitaciones = NegHabitaciones.listaHabitaciones(codNivel, Parametros.AdmisionParametros.getEstadoHabitacionDisponible());
                    cmb_HabitacionCambio.DataSource = listaHabitaciones.OrderBy(c => c.hab_Numero).ToList();
                    cmb_HabitacionCambio.DisplayMember = "hab_Numero";
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }

        private void cbm_Piso_TabIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cbm_Piso.SelectedItem != null)
            //    {
            //        short codNivel = Convert.ToInt16(cbm_Piso.ValueMember);
            //        var listaHabitaciones = NegHabitaciones.listaHabitaciones(codNivel, Parametros.AdmisionParametros.getEstadoHabitacionDisponible());
            //        cmb_HabitacionCambio.DataSource = listaHabitaciones;
            //        cmb_HabitacionCambio.DisplayMember = "hab_Numero";
            //    }
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}


        }

        private void cbm_Piso_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
