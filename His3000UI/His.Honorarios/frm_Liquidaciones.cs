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
using System.Threading;
using System.Windows.Forms;

namespace His.Honorarios
{
    public partial class frm_Liquidaciones : Form
    {
        public frm_Liquidaciones()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void CargarLiquidaciones()
        {
            dsHonorarios1 = NegLiquidacion.honorariosCxE(dtpDesde.Value, dtpHasta.Value);

            ultraGridLiquidacion.DataSource = dsHonorarios1;
            foreach (var item in ultraGridLiquidacion.Rows)
            {
                item.Cells["Medico"].Column.CellActivation = Activation.NoEdit;
                item.Cells["Codigo"].Column.CellActivation = Activation.NoEdit;
                item.Cells["Cantidad"].Column.CellActivation = Activation.NoEdit;
                item.Cells["Total"].Column.CellActivation = Activation.NoEdit;
                item.Band.Columns["Medico"].Width = 300;
                item.Cells["Seleccionar"].Column.Hidden = true;
                foreach (UltraGridChildBand x in item.ChildBands)
                {
                    RowsCollection bandchild = x.Rows;
                    foreach (var y in bandchild)
                    {
                        y.Cells["Paciente"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Porfuera"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Fecha"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Documento"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Valor"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Comision"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Aporte"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Retencion"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Pagar"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Usuario"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Seguro"].Column.CellActivation = Activation.NoEdit;
                        y.Cells["Honorario"].Column.CellActivation = Activation.NoEdit;

                        y.Band.Columns["Usuario"].Width = 200;
                    }
                }
            }
        }
        private void frm_Liquidaciones_Load(object sender, EventArgs e)
        {
            #region ASIGNACION DE FECHA
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            dtpDesde.Value = oPrimerDiaDelMes;
            dtpHasta.Value = oUltimoDiaDelMes;
            #endregion
            CargarLiquidaciones();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dtpDesde.Value <= dtpHasta.Value)
            {
                CargarLiquidaciones();
            }
            else
                MessageBox.Show("La fecha \"Desde\" no puedo ser mayor que fecha \"Hasta\"", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ultraGridLiquidacion_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            this.ultraGridLiquidacion.DisplayLayout.Bands[1].Columns["forpag"].Hidden = true;
            this.ultraGridLiquidacion.DisplayLayout.Bands[1].Columns["FormaPago"].Hidden = true;
            try
            {
                UltraGridBand sumatoria = ultraGridLiquidacion.DisplayLayout.Bands[1];
                SummarySettings sumarTotal = sumatoria.Summaries.Add("Pagar", SummaryType.Sum, sumatoria.Columns["Pagar"]);
                SummarySettings sumarRetencion = sumatoria.Summaries.Add("Retencion", SummaryType.Sum, sumatoria.Columns["Retencion"]);
                this.ultraGridLiquidacion.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
            }
            catch (Exception)
            {
                MessageBox.Show("No tiene valores a liquidar","HIS3000",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        public void ejecutaLiquidar()
        {
            List<LIQUIDACION> Liquidar = new List<LIQUIDACION>();
            DateTime fechaLiquida = DateTime.Now;

            if (ultraGridLiquidacion.Rows.Count > 0)
            {
                NUMERO_CONTROL numdoc = NegNumeroControl.OcuparNControl(10); //el 10 debe ser liquidacion para honorarios cxe
                if (numdoc.NUMCON != null)
                {
                    foreach (UltraGridRow item in ultraGridLiquidacion.Rows)
                    {
                        foreach (UltraGridChildBand childBand in item.ChildBands)
                        {
                            RowsCollection bandchild = childBand.Rows;
                            foreach (var x in bandchild)
                            {
                                if (x.Cells["Seleccion"].Text == "True")
                                {
                                    LIQUIDACION xliquida = new LIQUIDACION();
                                    xliquida.ID_USUARIO = Sesion.codUsuario;
                                    xliquida.MED_CODIGO = Convert.ToInt32(item.Cells["Codigo"].Value.ToString());
                                    xliquida.HOM_CODIGO = Convert.ToInt64(x.Cells["Honorario"].Value.ToString());
                                    xliquida.LIQ_FECHA = fechaLiquida;
                                    xliquida.LIQ_NUMDOC = Convert.ToInt64(numdoc.NUMCON.Trim());
                                    Liquidar.Add(xliquida);
                                }
                            }
                        }
                    }
                    if (NegLiquidacion.guardarLiquidacion(Liquidar))
                    {
                        if (!NegNumeroControl.LiberaNControl(10))
                            MessageBox.Show("No se ha podido liberar numero de control. Consulte con el administrador", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        MessageBox.Show("Liquidacion realizada con éxito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //bloqueo los honorarios generados la liquidacion
                        if (!NegLiquidacion.Bloquearhonorario(Liquidar))
                            MessageBox.Show("Honorarios no se bloquearon", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Imprimir(Convert.ToInt64(numdoc.NUMCON));
                        CargarLiquidaciones();
                    }
                    else
                        MessageBox.Show("No se puedo liquidar. Intente más tarde.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (contador <= 3) //TRES INTENTOS DE ESPERA HASTA QUE SE LIBERE EL NUMERO DE CONTROL
                    {
                        contador++;
                        Thread.Sleep(2000); //ESPERO 2 SEGUNDOS
                        ejecutaLiquidar();
                    }
                    else
                    {
                        MessageBox.Show("Numero de control ocupado. Intente más tarde.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
                MessageBox.Show("No hay registros a liquidar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public int contador = 0;
        private void btnLiquidar_Click(object sender, EventArgs e)
        {
            contador = 1;
            ejecutaLiquidar();
        }

        public void Imprimir(Int64 numdoc)
        {
            List<LIQUIDACION> x = new List<LIQUIDACION>(); //cargo  la liquidacion realizada consultando en la base
            x = NegLiquidacion.recuperarLiquidacion(numdoc);
            EMPRESA e = NegEmpresa.RecuperaEmpresa();
            if (x.Count > 0)
            {
                DsLiquidacion liquidacion = new DsLiquidacion();
                DataRow dr;

                Int64 numero = 0;
                foreach (var item in x)
                {
                    MEDICOS m = NegMedicos.recuperarMedico((int)item.MED_CODIGO);
                    HONORARIOS_MEDICOS h = NegLiquidacion.recuperarHonorario((Int64)item.HOM_CODIGO);
                    ATENCIONES a = NegAtenciones.RecuperarAtencionID((Int64)h.ATE_CODIGO);
                    PACIENTES p = NegPacientes.recuperarPacientePorAtencion((int)h.ATE_CODIGO);

                    dr = liquidacion.Tables["Liquidacion"].NewRow();
                    dr["Medico"] = m.MED_APELLIDO_PATERNO + " " + m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1 + " " + m.MED_NOMBRE2;
                    dr["Paciente"] = p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO + " " + p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2;
                    dr["Factura"] = a.ATE_FACTURA_PACIENTE;
                    dr["Atencion"] = a.ATE_FECHA_INGRESO;
                    dr["Hc"] = p.PAC_HISTORIA_CLINICA;
                    dr["Fecha"] = item.LIQ_FECHA;
                    dr["Liquidacion"] = item.LIQ_NUMDOC;
                    numero = (Int64)item.LIQ_NUMDOC;
                    dr["ValorNeto"] = h.HOM_VALOR_NETO;
                    liquidacion.Tables["Liquidacion"].Rows.Add(dr);
                }

                dr = liquidacion.Tables["Principal"].NewRow();
                dr["Logo"] = NegUtilitarios.RutaLogo("General");
                dr["Empresa"] = e.EMP_NOMBRE;
                dr["Telefono"] = e.EMP_TELEFONO;
                dr["Direccion"] = e.EMP_DIRECCION;
                dr["Liquidacion"] = numero;
                liquidacion.Tables["Principal"].Rows.Add(dr);

                frmReportes view = new frmReportes("Liquidacion", liquidacion);
                view.ShowDialog();
            }
            else
                MessageBox.Show("No tiene liquidacion realizadas.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarLiquidaciones();
        }

        private void ultraGridLiquidacion_AfterCellUpdate(object sender, CellEventArgs e)
        {
            //if (e.Cell.Column.Key == "Seleccionar") //valido cuando el true se elijan todos los childrows
            //{
            //    if (e.Cell.Value.ToString() == "True")
            //    {
            //        UltraGridRow fila = ultraGridLiquidacion.ActiveRow;
            //        foreach (var item in ultraGridLiquidacion.Rows)
            //        {
            //            foreach (UltraGridChildBand x in item.ChildBands)
            //            {
            //                RowsCollection bandchild = x.Rows;
            //                foreach (var y in bandchild)
            //                {
            //                    y.Cells["Seleccion"].Value = true;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        UltraGridRow fila = ultraGridLiquidacion.ActiveRow;
            //        foreach (var item in ultraGridLiquidacion.Rows)
            //        {
            //            foreach (UltraGridChildBand x in item.ChildBands)
            //            {
            //                RowsCollection bandchild = x.Rows;
            //                foreach (var y in bandchild)
            //                {
            //                    y.Cells["Seleccion"].Value = false;
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}
