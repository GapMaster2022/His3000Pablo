using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using System.IO;

namespace His.Admision
{
    public partial class frmExploradorControlAyuda : Form
    {
        NegControlHc Negocio = new NegControlHc();
        internal static string paciente;
        internal static string atencion;
        internal static string codigoA;
        private static string usuario;
        internal static string codigop;
        private static int contador;
        internal static string numestado;
        public frmExploradorControlAyuda()
        {
            InitializeComponent();
        }

        private void frmExploradorControlAyuda_Load(object sender, EventArgs e)
        {
            RecuperarUsuario();
            lblpaciente.Text = paciente;
            lblatencion.Text = atencion;
            CargarDocumentos();
            Bloquear();
            ValidarEstado();
        }
        public void ValidarEstado()
        {
            if (numestado == "1")
            {
                btnCerrar.Enabled = true;
                btnAbrir.Enabled = false;
            }
            else
            {
                BloquearTodo();
            }
        }
        public void Bloquear()
        {
            TablaControl.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btneditar.Enabled = true;
            btnAbrir.Enabled = true;
            toolStripButtonActualizar.Enabled = true;
        }
        public void CargarDocumentos()
        {
            TablaControl.Rows.Clear();
            string cod;
            DataTable Tabla = new DataTable();
            DataTable Tabla1 = new DataTable();
            Tabla = Negocio.Documentos();
            Tabla1 = Negocio.PorAtencionControl(lblatencion.Text, codigoA);
            foreach (DataRow item in Tabla.Rows)
            {
                TablaControl.Rows.Add(item[0], item[1]);
            }
            for (int i = 0; i < TablaControl.RowCount; i++)
            {
                foreach (DataRow item in Tabla1.Rows)
                {
                    cod = item[0].ToString();
                    if (Convert.ToString(TablaControl.Rows[i].Cells["cod"].Value) == cod)
                    {
                        TablaControl.Rows[i].Cells["est"].Value = true;
                        TablaControl.Rows[i].Cells["fecha"].Value = item[2];
                        TablaControl.Rows[i].Cells["user"].Value = item[3];
                    }
                }
                
            }
        }
        public void RecuperarUsuario()
        {
            usuario = His.Entidades.Clases.Sesion.nomUsuario;
        }
        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CargarDocumentos();
            Bloquear();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (TablaControl.Rows.Count == 0)
            {
                MessageBox.Show("No hay Registros para Mostrar.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ExportarAExcel();
            }
        }
        private void ExportarAExcel()
        {
            this.CopiarGrilla();

            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object valor = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(valor);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            MessageBox.Show("Exportación Finalizada", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void CopiarGrilla()
        {
            TablaControl.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            TablaControl.MultiSelect = true;
            TablaControl.SelectAll();
            DataObject dataObj = TablaControl.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);

            TablaControl.MultiSelect = false;
            TablaControl.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;

        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            CargarDocumentos();
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            TablaControl.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btneditar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            lblpaciente.Focus();//Se agrego este focus, para poder guardar los checks, ya que al poner un check este no guarda porque necesita un evento que autorice el cambio
            AceptarCambios();
            Bloquear();
        }
        public void AceptarCambios()
        {
            string control; //Codigo de la tabla Control_His_Clin en la BD
            DateTime fecha = DateTime.Now; 
            int cantidad = 0; //Variable que contiene el numero maximo de registro dentro de la tabla Control_His_Clin en la BD
            cantidad = Convert.ToInt32(Negocio.Cantidad());
            for (int i = 0; i < TablaControl.Rows.Count; i++)
            {
                //Aqui se controlara el numero de checks que tiene y si ha completado los documentos, esto hara que 
                // pase todo a COMPLETO
                if (Convert.ToBoolean(this.TablaControl.Rows[i].Cells["est"].Value) == true)
                {
                    contador += 1;
                }
            }
            for (int i = 0; i < TablaControl.Rows.Count; i++)
            {
                control = null;
                control = TablaControl.Rows[i].Cells["cod"].Value.ToString();
                try
                {
                    if (cantidad == contador)
                    {
                        Negocio.ControlInsert("COMPLETO", Convert.ToString(fecha), usuario, codigop, control, codigoA, "1");
                        btnAbrir.Enabled = true;
                        btnCerrar.Enabled = false;
                    }
                    else
                    {
                        if(TablaControl.Rows[i].Cells["est"].Value != null)
                        {
                        
                            if (TablaControl.Rows[i].Cells["est"].Value.ToString() == "True")
                            {
                                Negocio.ControlInsert("INCOMPLETO", Convert.ToString(fecha), usuario, codigop, control, codigoA, "1");
                                btnAbrir.Enabled = false;
                                btnCerrar.Enabled = true;
                            }
                            else
                            {
                                Negocio.ControlActualizar(codigoA, control);
                            }
                        }              
                    }
                }
                catch (Exception ex)
                {
                    TablaControl.Rows[i].Cells["est"].Value = "false";
                    //MessageBox.Show(ex.Message);
                }
            }
            MessageBox.Show("Los Cambios se han Guardado con Exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void BloquearTodo()
        {
            TablaControl.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btneditar.Enabled = false;
            btnCerrar.Enabled = false;
            toolStripButtonActualizar.Enabled = false;

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("¿Esta Seguro de Cerrar Los Documentos para este Paciente?","Warning", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Negocio.ControlCerrar(Convert.ToInt64(codigoA), "0");
                    MessageBox.Show("Se ha Cerrado Exitosamente!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BloquearTodo();
                    btnAbrir.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Esta Seguro de Volver Abrir Los Documentos para este Paciente?", "Warning", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    Negocio.ControlAbrir(Convert.ToInt64(codigoA), "1");
                    MessageBox.Show("Se ha Abierto Exitosamente!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Bloquear();
                    btnCerrar.Enabled = true;
                    btnAbrir.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblpaciente_Click(object sender, EventArgs e)
        {
        }
    }
}
