using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;
using His.Parametros;
using System.IO;
using System.Diagnostics;

namespace CuentaPaciente
{
    public partial class frmExportarExel : Form
    {
        string sqlCadena = "";
        DataTable datosAct = new DataTable();
        int control = 0;
        int cont = 0;
        int contador = 0;
        int numeroRegistros = 0;
        string[] conjuntoAtenciones = new string[1000];
        public frmExportarExel(string cadenaSql, int contador)
        {
            sqlCadena = cadenaSql;
            cont = contador;
            InitializeComponent();
        }

        private void frmExportarExel_Load(object sender, EventArgs e)
        {
            //DataTable datos = NegAseguradoras.CuentasAtenciones(sqlCadena);
            //uGridCuentas.DataSource = datos;
            toolStripbtnNuevo.Image = Archivo.imgOfficeExcel;
            generarAtenciones();
        }
        private DataTable GetAtenciones()
        {
            DataTable dtAtenciones = new DataTable("Atenciones");
            DataRow drRow = dtAtenciones.NewRow();

            dtAtenciones.Columns.Add(new DataColumn("codigo",
                Type.GetType("System.String")));

            string[] cadenaAtenciones = sqlCadena.Split(',');


            for (int i = 0; i < cont; i++)
            {
                drRow = dtAtenciones.NewRow();
                drRow["codigo"] = cadenaAtenciones[i];

                dtAtenciones.Rows.Add(drRow);


            }

            return dtAtenciones;
        }


        public void generarAtenciones()
        {
            int count = 0;

            StringWriter swStringWriter = new StringWriter();
            DataTable dtatenciones = GetAtenciones();
            dtatenciones.WriteXml(swStringWriter);

            string stratenciones = swStringWriter.ToString();
            DataTable datos = NegCuentasPacientes.CuentasAtenciones(stratenciones);
            datosAct = NegCuentasPacientes.CuentasAtenciones(stratenciones);

            var x = (from r in datos.AsEnumerable()
                     select r["PLANILLA"]).Distinct().ToList();
            foreach (var name in x)
            {


                conjuntoAtenciones[count] = Convert.ToString(name);
                count++;


            }
            numeroRegistros = conjuntoAtenciones.Count();
            uGridCuentas.DataSource = datos;

            if (datos.Rows.Count > 0)
            {
                Decimal TotalIva = datos.AsEnumerable().Sum(o => o.Field<Decimal>("iva")); // suma de iva de la cuenta / Giovanny Tapia / 31/08/2012
                Decimal TotalProductos = datos.AsEnumerable().Sum(o => o.Field<Decimal>(17)); // suma de total de la cuenta / Giovanny Tapia / 31/08/2012

                this.lblTotalCuenta.Text = (TotalIva + TotalProductos).ToString(); // Muestra la suma de Iva + Total / Giovanny Tapia / 31/08/2012
            }
        }

        private void toolStripbtnNuevo_Click(object sender, EventArgs e)
        {
            int control = 0;
            try
            {
                CreateExcel(FindSavePath());
                var x2 = (from r in datosAct.AsEnumerable()
                          select r["ESC_CODIGO"]).Distinct().ToList();

                foreach (var name2 in x2)
                {

                    if (3 != Convert.ToInt16(name2))
                    {
                        control++;

                    }
                }

                if (control == 0)
                {
                    var x1 = (from r in datosAct.AsEnumerable()
                              select r["PLANILLA"]).Distinct().ToList();
                    foreach (var name1 in x1)
                    {
                        // NegAseguradoras.actualizarEstadoFactura(Convert.ToString(name1), 5);

                    }
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void CreateExcel(String myFilepath)
        {
            try
            {
                if (myFilepath != null)
                {

                    this.ultraGridExcelExporter1.Export(uGridCuentas, myFilepath);

                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }

        private void uGridCuentas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;

            e.Layout.Bands[0].Columns["ADS_CODIGO"].Hidden = true;
            e.Layout.Bands[0].Columns["RUB_CODIGO"].Hidden = true;
            uGridCuentas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            uGridCuentas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            uGridCuentas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            uGridCuentas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            uGridCuentas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;


        }

        private void uGridCuentas_InitializeRow(object sender, InitializeRowEventArgs e)
        {


            try
            {



                string codAte = conjuntoAtenciones[contador];


                if (codAte.Trim() == Convert.ToString(e.Row.Cells[1].Value).Trim())
                {
                    e.Row.Cells[1].Value = Convert.ToString(contador);
                }
                else
                {
                    contador++;
                    e.Row.Cells[1].Value = Convert.ToString(contador + 1);
                }





                if (Convert.ToString(e.Row.Cells[34].Value) == "27")
                {


                    e.Row.Cells[10].Value = "";

                }
                if (Convert.ToString(e.Row.Cells[34].Value) == "1")
                {


                    e.Row.Cells[10].Value = "";

                }
                if (Convert.ToString(e.Row.Cells[10].Value) == "99103")
                {


                    e.Row.Cells[10].Value = "";

                }
                if (Convert.ToString(e.Row.Cells[10].Value) == "99104")
                {


                    e.Row.Cells[10].Value = "";

                }
                if (Convert.ToString(e.Row.Cells[10].Value) == "99105")
                {


                    e.Row.Cells[10].Value = "";

                }
                if (Convert.ToString(e.Row.Cells[10].Value) == "109064")
                {


                    e.Row.Cells[10].Value = "";

                }

            }

            catch (Exception ex)
            {
                if (control == 0)
                    e.Row.Cells[1].Value = "1";
                control++;
                e.Row.Cells[1].Value = "1";
            }


        }
    }
}






//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using His.Entidades;
//using His.Entidades.Pedidos;
//using His.Negocio;
//using Infragistics.Win.UltraWinGrid;
//using Recursos;
//using System.Data.Objects;
//using System.Data.Objects.DataClasses;
//using Core.Datos;
//using His.Parametros;
//using System.IO;
//using System.Diagnostics;

//namespace CuentaPaciente
//{
//    public partial class frmExportarExel : Form
//    {
//        string sqlCadena = "";
//        DataTable datosAct = new DataTable();
//        int control = 0;
//        int cont = 0;
//        int contador = 0;
//        int numeroRegistros = 0;
//        string[] conjuntoAtenciones = new string[1000]; 
//        public frmExportarExel(string cadenaSql,int contador)
//        {
//            sqlCadena = cadenaSql+"1";
//            cont = contador;
//            InitializeComponent();
//        }

//        private void frmExportarExel_Load(object sender, EventArgs e)
//        {
//            //DataTable datos = NegAseguradoras.CuentasAtenciones(sqlCadena);
//            //uGridCuentas.DataSource = datos;
//            toolStripbtnNuevo.Image = Archivo.imgOfficeExcel;   
//            generarAtenciones();
//        }
//        private DataTable GetAtenciones()
//        {
//            DataTable dtAtenciones = new DataTable("Atenciones");
//            DataRow drRow = dtAtenciones.NewRow(); 

//            dtAtenciones.Columns.Add(new DataColumn("codigo",
//                Type.GetType("System.String")));

//            string[] cadenaAtenciones = sqlCadena.Split(',');
            
    
//            for (int i = 0; i < cont; i++)
//            {
//                drRow = dtAtenciones.NewRow();
//                drRow["codigo"] = cadenaAtenciones[i];

//                dtAtenciones.Rows.Add(drRow);

               
//            }           
            
//            return dtAtenciones;
//        }


//        public void generarAtenciones()
//        {
//            int count = 0;

//            StringWriter swStringWriter = new StringWriter();
//            DataTable dtatenciones = GetAtenciones();
//            dtatenciones.WriteXml(swStringWriter);
     
//           string stratenciones = swStringWriter.ToString();
//           DataTable datos = NegCuentasPacientes.CuentasAtenciones(stratenciones);
//           datosAct = NegCuentasPacientes.CuentasAtenciones(stratenciones);

//            var x = (from r in datos.AsEnumerable()
//                    select r["PLANILLA"]).Distinct().ToList();
//           foreach (var name in x) 
//           { 
               

//               conjuntoAtenciones[count] =Convert.ToString( name);
//               count++;

           
//           }
//           numeroRegistros = conjuntoAtenciones.Count();
//            uGridCuentas.DataSource = datos;

//            if (datos.Rows.Count > 0)
//            {
//                Decimal TotalIva = datos.AsEnumerable().Sum(o => o.Field<Decimal>("iva")); // suma de iva de la cuenta / Giovanny Tapia / 31/08/2012
//                Decimal TotalProductos = datos.AsEnumerable().Sum(o => o.Field<Decimal>(17)); // suma de total de la cuenta / Giovanny Tapia / 31/08/2012

//                this.lblTotalCuenta.Text = (TotalIva + TotalProductos).ToString(); // Muestra la suma de Iva + Total / Giovanny Tapia / 31/08/2012
//            }
//        }

//        private void toolStripbtnNuevo_Click(object sender, EventArgs e)
//        {
//            int control = 0;
//            try
//            {
//                CreateExcel(FindSavePath());
//                var x2 = (from r in datosAct.AsEnumerable()
//                         select r["ESC_CODIGO"]).Distinct().ToList();

//                foreach (var name2 in x2)
//                {

//                    if(3!=Convert.ToInt16(name2))
//                    {
//                        control++;
                    
//                    }
//                }

//                if (control == 0)
//                {
//                    var x1 = (from r in datosAct.AsEnumerable()
//                              select r["PLANILLA"]).Distinct().ToList();
//                    foreach (var name1 in x1)
//                    {
//                       // NegAseguradoras.actualizarEstadoFactura(Convert.ToString(name1), 5);

//                    }
//                }
                            
//            }
//            catch (Exception ex) { MessageBox.Show(ex.Message); }
//            finally { this.Cursor = Cursors.Default; }
//        }
//        private void CreateExcel(String myFilepath)
//        {
//            try
//            {
//                if (myFilepath != null)
//                {

//                    this.ultraGridExcelExporter1.Export(uGridCuentas, myFilepath);

//                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
//                }
               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private String FindSavePath()
//        {
//            Stream myStream;
//            string myFilepath = null;
//            try
//            {
//                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
//                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
//                saveFileDialog1.FilterIndex = 2;
//                saveFileDialog1.RestoreDirectory = true;
//                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
//                {
//                    if ((myStream = saveFileDialog1.OpenFile()) != null)
//                    {
//                        myFilepath = saveFileDialog1.FileName;
//                        myStream.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            return myFilepath;
//        }

//        private void uGridCuentas_InitializeLayout(object sender, InitializeLayoutEventArgs e)
//        {
//            e.Layout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;
           
//            e.Layout.Bands[0].Columns["ADS_CODIGO"].Hidden = true;
//            e.Layout.Bands[0].Columns["RUB_CODIGO"].Hidden = true;
//            uGridCuentas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
//            uGridCuentas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
//            uGridCuentas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
//            uGridCuentas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
//            uGridCuentas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
           

//        }

//        private void uGridCuentas_InitializeRow(object sender, InitializeRowEventArgs e)
//        {
           

//            try
//            {               



//                string codAte = conjuntoAtenciones[contador];
               

//               if (codAte.Trim() == Convert.ToString(e.Row.Cells[1].Value).Trim())
//               {
//                   e.Row.Cells[1].Value = Convert.ToString(contador+1);
//               }
//               else
//               {
//                   contador++;
//                   e.Row.Cells[1].Value = Convert.ToString(contador+1);
//               }


            
             

//                if (Convert.ToString(e.Row.Cells[34].Value) == "27")
//                {


//                    e.Row.Cells[10].Value = "";

//                }
//                if (Convert.ToString(e.Row.Cells[34].Value) == "1")
//                {


//                    e.Row.Cells[10].Value = "";

//                }
//                if (Convert.ToString(e.Row.Cells[10].Value) == "99103")
//                {


//                    e.Row.Cells[10].Value = "";

//                }
//                if (Convert.ToString(e.Row.Cells[10].Value) == "99104")
//                {


//                    e.Row.Cells[10].Value = "";

//                }
//                if (Convert.ToString(e.Row.Cells[10].Value) == "99105")
//                {


//                    e.Row.Cells[10].Value = "";

//                }
//                if (Convert.ToString(e.Row.Cells[10].Value) == "109064")
//                {


//                    e.Row.Cells[10].Value = "";

//                }
            
//            }
               
//            catch (Exception ex)
//            {
//                if(control==0)
//                e.Row.Cells[1].Value = "1";
//                control++;
             
//            }
        

//        }
//    }
//}
