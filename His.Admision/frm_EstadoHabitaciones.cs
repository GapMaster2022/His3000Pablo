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
using Core.Entidades;
using His.Entidades.Clases;

namespace His.Admision
{
    public partial class frm_EstadoHabitaciones : Form
    {
        #region variables
        public string codhabitacion;
        HABITACIONES datHabitacion = new HABITACIONES();
        #endregion

        #region Constructor
        public frm_EstadoHabitaciones()
        {
            InitializeComponent();
        }
        #endregion

        

        #region eventos
        private void frm_EstadoHabitaciones_Load(object sender, EventArgs e)
        {
            try
            {
                int k = 0;
                var datosgrafico = NegHabitaciones.ListaEstadosdeHabitacion().ToList();
                grid_estados.Columns.Add("CODIGO", "CODIGO");
                grid_estados.Columns.Add("DESCRIPCION", "ESTADO");
                DataGridViewImageColumn imgColumna = new DataGridViewImageColumn();
                imgColumna.Name = "estado";
                imgColumna.HeaderText = " ";
                imgColumna.Image = imageList1.Images[0];
                grid_estados.Columns.Insert(2, imgColumna);
                DataGridViewCheckBoxColumn activo = new DataGridViewCheckBoxColumn();
                activo.HeaderText = "ACTIVAR";
                grid_estados.Columns.Insert(3, activo);
                foreach (var dato in datosgrafico)
                {
                    grid_estados.Rows.Add();
                    grid_estados.Rows[k].Cells[0].Value = dato.HES_CODIGO;
                    grid_estados.Rows[k].Cells[1].Value = dato.HES_NOMBRE;
                    grid_estados.Rows[k].Cells[2].Value = imageList1.Images[dato.HES_CODIGO];
                    grid_estados.Rows[k].Cells[3].Value = false;
                    k = k + 1;
                }
                cargadatos();
                grid_estados.Columns[0].Width = 50;
                grid_estados.Columns[1].Width = 180;
                grid_estados.Columns[2].Width = 30;
                grid_estados.Columns[3].Width = 60;

                
                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DialogResult resultado;
            //    resultado = MessageBox.Show("Desea cambier el estado de la Habitación?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (resultado == DialogResult.Yes)
            //    {
            //        for (int i = 0; i <= grid_estados.Rows.Count - 1; i++)
            //        {
            //            if (grid_estados.Rows[i].Cells[3].Value.ToString() == true.ToString())
            //            {
            //                HABITACIONES habitacionO = new HABITACIONES();
            //                HABITACIONES_DETALLE detallehabitacion = new HABITACIONES_DETALLE();
            //                habitacionO = datHabitacion.ClonarEntidad();
            //                HABITACIONES_ESTADO estado = NegHabitaciones.ListaEstadosdeHabitacion().Where(cod => cod.HES_CODIGO == datHabitacion.HABITACIONES_ESTADO.HES_CODIGO + 1).FirstOrDefault();
            //                datHabitacion.HABITACIONES_ESTADOReference.EntityKey = estado.EntityKey;
            //                NegHabitaciones.GrabarHabitaciones(datHabitacion, habitacionO); // cambio de estado a la habitacion

            //                HABITACIONES_DETALLE detalleActual = NegHabitaciones.RecuperarDetalleHabitacion(Int16.Parse(NegHabitaciones.UltimoDetalle(codhabitacion).ToString()));
            //                detallehabitacion.HAD_CODIGO = NegHabitaciones.RecuperaMaximoDetalleHabitacion() + 1;
            //                if (detalleActual != null && estado.HES_CODIGO <= 4)
            //                {
            //                    detallehabitacion.ATE_CODIGO = detalleActual.ATE_CODIGO;

            //                    detallehabitacion.HAD_ENCARGADO = detalleActual.HAD_ENCARGADO;
            //                    if (estado.HES_CODIGO == 2)
            //                        detallehabitacion.HAD_FECHA_ALTA_MEDICO = DateTime.Now;
            //                    else
            //                        detallehabitacion.HAD_FECHA_ALTA_MEDICO = detalleActual.HAD_FECHA_ALTA_MEDICO;
            //                    if (estado.HES_CODIGO == 4)
            //                        detallehabitacion.HAD_FECHA_DISPONIBILIDAD = DateTime.Now;
            //                    else
            //                        detallehabitacion.HAD_FECHA_DISPONIBILIDAD = detalleActual.HAD_FECHA_DISPONIBILIDAD;
            //                    detallehabitacion.HAD_FECHA_FACTURACION = detalleActual.HAD_FECHA_FACTURACION;
            //                    detallehabitacion.HAD_FECHA_INGRESO = detalleActual.HAD_FECHA_INGRESO;

            //                    detallehabitacion.HAD_REGISTRO_ANTERIOR = detalleActual.HAD_REGISTRO_ANTERIOR;

            //                }
            //                else
            //                {
            //                    detallehabitacion.HAD_FECHA_DISPONIBILIDAD = DateTime.Now;
            //                }
            //                detallehabitacion.HAD_OBSERVACION = textBox1.Text;
            //                detallehabitacion.ID_USUARIO = Sesion.codUsuario;
            //                detallehabitacion.HABITACIONESReference.EntityKey = habitacionO.EntityKey;
            //                NegHabitaciones.CrearHabitacionDetalle(detallehabitacion); // grabo el detalle de movimientos de la habitacion
            //                break;
            //            }
            //        }
            //        MessageBox.Show("Cambios Realizados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.Close();
            //    }
            //    else
            //    {
            //        this.Close();
            //    }


            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        #endregion

        #region Metodos privados
        //carga los datos de la habitacion que se va a cambiar el estado
        private void cargadatos()
        {
            //int ultimodetalle = NegHabitaciones.UltimoDetalle(codhabitacion);
                datHabitacion = NegHabitaciones.listaHabitaciones().Where(c => c.hab_Numero == codhabitacion).FirstOrDefault();
                //txt_infestado.Text = datHabitacion.HABITACIONES_ESTADO.HES_NOMBRE;
                
                //grid_estados.Rows[2].ReadOnly = true;
                //grid_estados.Rows[2].Cells[0].Style.BackColor = Color.LightGray;
                //grid_estados.Rows[2].Cells[1].Style.BackColor = Color.LightGray;
                //grid_estados.Rows[2].Cells[2].Style.BackColor = Color.LightGray;
                //grid_estados.Rows[2].Cells[3].Style.BackColor = Color.LightGray;
                //for (int i = 0; i <= datHabitacion.HABITACIONES_ESTADO.HES_CODIGO - 1; i++)
                //{
                //    grid_estados.Rows[i].ReadOnly = true;
                //    for (int j = 0; j <= grid_estados.Columns.Count - 1; j++)
                //    {
                //        grid_estados.Rows[i].Cells[j].Style.BackColor = Color.LightGray;
                //    }
                //}
                //grid_estados.Rows[datHabitacion.HABITACIONES_ESTADO.HES_CODIGO].Cells[3].Value = true;
                //if (ultimodetalle != 0)
                //{
                   
                //    if (datHabitacion.HABITACIONES_ESTADO.HES_CODIGO == 1 || datHabitacion.HABITACIONES_ESTADO.HES_CODIGO == 2 || datHabitacion.HABITACIONES_ESTADO.HES_CODIGO == 3 || datHabitacion.HABITACIONES_ESTADO.HES_CODIGO == 4)
                //    {
                //        var datoshabitacion = NegHabitaciones.RecuperarDetallesHabitacion(null, null, null, null, null, ultimodetalle.ToString(),null,null,null).FirstOrDefault();
                //        if (datoshabitacion != null)
                //        {

                //            txt_infhabitacion.Text = datoshabitacion.hab_Numero;
                //            txt_inffecingreso.Text = datoshabitacion.HAD_FECHA_INGRESO.ToString();
                //            txt_inffecalta.Text = datoshabitacion.HAD_FECHA_ALTA_MEDICO.ToString();
                //            txt_inffecdisponible.Text = datoshabitacion.HAD_FECHA_DISPONIBILIDAD.ToString();
                //            txt_inffecfacturacion.Text = datoshabitacion.HAD_FECHA_FACTURACION.ToString();
                //            txt_infhistoriaclinica.Text = datoshabitacion.PAC_HISTORIA_CLINICA;
                //            txt_infpaciente.Text = datoshabitacion.PACIENTE;
                //            txt_infatencion.Text = datoshabitacion.ATE_CODIGO.ToString();
                //            txt_infmedico.Text = datoshabitacion.MED_NOMBRE;
                //            txt_infespecialidad.Text = datoshabitacion.ESP_NOMBRE;
                //        }
                //    }
                //    else
                //    {
                //        var dethabitacion = NegHabitaciones.DetalleHabitacion().Where(c => c.HAD_CODIGO == ultimodetalle).FirstOrDefault();
                //        txt_infhabitacion.Text = datHabitacion.hab_Numero;
                //        txt_inffecingreso.Text = string.Empty;
                //        txt_inffecalta.Text = string.Empty;
                //        txt_inffecdisponible.Text = dethabitacion.HAD_FECHA_DISPONIBILIDAD.ToString();
                //        txt_inffecfacturacion.Text = string.Empty;
                //        txt_infhistoriaclinica.Text = string.Empty;
                //        txt_infpaciente.Text = string.Empty;
                //        txt_infatencion.Text = string.Empty;
                //        txt_infmedico.Text = string.Empty;
                //        txt_infespecialidad.Text = string.Empty;
                //    }

                //}
                //else
                //{
                //    txt_infhabitacion.Text =  codhabitacion;
                //    txt_inffecingreso.Text = string.Empty;
                //    txt_inffecalta.Text = string.Empty;
                //    txt_inffecdisponible.Text = string.Empty;
                //    txt_inffecfacturacion.Text = string.Empty;
                //    txt_infhistoriaclinica.Text = string.Empty;
                //    txt_infpaciente.Text = string.Empty;
                //    txt_infatencion.Text = string.Empty;
                //    txt_infmedico.Text = string.Empty;
                //    txt_infespecialidad.Text = string.Empty;
                //}
        }
        #endregion

    }
}
