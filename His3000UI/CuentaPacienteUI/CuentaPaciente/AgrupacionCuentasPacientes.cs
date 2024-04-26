using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace CuentaPaciente
{
    public partial class AgrupacionCuentasPacientes : Form
    {
        #region VARIABLES GLOBALES

        Int64[] guardaAteCodigo = new Int64[1000];
        int vector = 0;
        public TextBox AteCodigoAgrupado = new TextBox();
        Int64 ate_codigo = 0;
        int usuario = 0;
        #endregion
        public AgrupacionCuentasPacientes(Int64 _ate_codigo, int _usuario)
        {
            InitializeComponent();
            this.ControlBox = false;
            ate_codigo = _ate_codigo;
            usuario = _usuario;
            Buscar();
        }

        #region OBJETOS PARA SER UTILIZADOS

        private void Buscar()
        {
            DataTable busca = new DataTable();
            busca = NegFactura.BuscaPaciente(txtHistoria.Text, txtNombre.Text, txtIdentificacion.Text);
            gridAgrupa.DataSource = busca;
            gridAgrupa.Columns[0].Width = 50;
            gridAgrupa.Columns[1].Width = 65;
            gridAgrupa.Columns[2].Width = 300;
            gridAgrupa.Columns[3].Width = 100;
            gridAgrupa.Columns[4].Width = 120;
        }

        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void gridAgrupa_DoubleClick(object sender, EventArgs e)
        {
            int verifica = 0;
            if (gridAñadidos.Rows.Count > 1)
            {
                Int64 agrupado = Convert.ToInt64(gridAgrupa[0, gridAgrupa.CurrentRow.Index].Value);
                if(agrupado == ate_codigo)
                {
                    MessageBox.Show("No se puede agrupar la cuenta madre", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                for (int i = 0; i < gridAñadidos.Rows.Count; i++)
                {
                    Int64 añadido = Convert.ToInt64(gridAñadidos.Rows[i].Cells[0].Value);
                    
                    if (añadido == agrupado)
                    {
                        verifica = 1;
                        i = gridAñadidos.Rows.Count;
                    }
                }
                if (verifica == 0)
                {
                    gridAñadidos.Rows.Add(new string[]
                    {
                        Convert.ToString(gridAgrupa[0,gridAgrupa.CurrentRow.Index].Value),
                        Convert.ToString(gridAgrupa[1,gridAgrupa.CurrentRow.Index].Value),
                        Convert.ToString(gridAgrupa[2,gridAgrupa.CurrentRow.Index].Value),
                        Convert.ToString(gridAgrupa[3,gridAgrupa.CurrentRow.Index].Value),
                        Convert.ToString(gridAgrupa[4,gridAgrupa.CurrentRow.Index].Value),
                    });
                    guardaAteCodigo[vector] = Convert.ToInt64(gridAgrupa[0, gridAgrupa.CurrentRow.Index].Value);
                    vector++;
                }
                else
                {
                    MessageBox.Show("Paceinte ya fue Añadido", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    verifica = 0;
                }
            }
            else
            {
                Int64 agrupado = Convert.ToInt64(gridAgrupa[0, gridAgrupa.CurrentRow.Index].Value);
                if (agrupado == ate_codigo)
                {
                    MessageBox.Show("No se puede agrupar la cuenta madre", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                btnCancelar.Visible = true;
                gridAñadidos.Rows.Add(new string[]
                {
                    Convert.ToString(gridAgrupa[0,gridAgrupa.CurrentRow.Index].Value),
                    Convert.ToString(gridAgrupa[1,gridAgrupa.CurrentRow.Index].Value),
                    Convert.ToString(gridAgrupa[2,gridAgrupa.CurrentRow.Index].Value),
                    Convert.ToString(gridAgrupa[3,gridAgrupa.CurrentRow.Index].Value),
                    Convert.ToString(gridAgrupa[4,gridAgrupa.CurrentRow.Index].Value),
                });
                guardaAteCodigo[vector] = Convert.ToInt64(gridAgrupa[0, gridAgrupa.CurrentRow.Index].Value);
                vector++;
            }
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void txtHistoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan
                e.Handled = true;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }
        private void btnTerminar_Click(object sender, EventArgs e)
        {

            if (gridAñadidos.RowCount > 1)
            {
                if (MessageBox.Show("¿Está Seguro De Haber Terminado De Agrupar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow item in gridAñadidos.Rows)
                    {
                        if (item.Cells[0].Value != null)
                            NegFactura.GuardaAuxAgrupacion(ate_codigo, Convert.ToInt64(item.Cells[0].Value.ToString()),  usuario);
                    }
                    MessageBox.Show("Agrupacion terminada con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                if (MessageBox.Show("Usted No Agrupo Ninguna Cuenta, ¿Desea Salir De Todas Maneras?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AteCodigoAgrupado.Text = "0";
                    this.Close();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta Seguro De Cancelar Proceso?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AteCodigoAgrupado.Text = "0";
                NegFactura.EliminaAuxAgrupa();
                this.Close();
            }
        }
    }
}
