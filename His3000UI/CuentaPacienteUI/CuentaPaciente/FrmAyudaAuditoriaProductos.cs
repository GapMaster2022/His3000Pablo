using His.Entidades;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CuentaPaciente
{
    public partial class FrmAyudaAuditoriaProductos : Form
    {
        public RUBROS rubro = new RUBROS();
        private PEDIDOS_AREAS areas;
        private List<PRODUCTO> listaProductosDisponibles;
        public List<PRODUCTO> listaProductosSolicitados;
        public string descripcion;
        public string iva;
        private Int16 codigoArea;
        DataTable Productos = new DataTable();
        public PEDIDOS_DETALLE PedidosDetalleItem = null;
        public List<PEDIDOS_DETALLE> PedidosDetalle = new List<PEDIDOS_DETALLE>();
        int Resultado = 0;
        bool listartodos = false;
        RUBROS _Rubro = new RUBROS();
        Int32 _CodigoEmpresa = 0;
        Int32 _CodigoConvenio = 0;
        public string codpro = "";
        public string division = "";
        public FrmAyudaAuditoriaProductos()
        {
            InitializeComponent();
        }

        public FrmAyudaAuditoriaProductos(PEDIDOS_AREAS areas, RUBROS Rubro, Boolean todos/*, Int32 CodigoEmpresa*/, Int32 CodigoConvenio)
        {
            listartodos = todos;
            InitializeComponent();
            this.areas = areas;
            _Rubro = Rubro;
            /*_CodigoEmpresa = CodigoEmpresa;*/
            _CodigoConvenio = CodigoConvenio;
            cargarProductos();
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                int cantidad = Convert.ToInt32(cmb_Mostrar.SelectedValue);
                string textoBusqueda = txt_Nombre.Text.Trim() == "" ? null : txt_Nombre.Text.Trim();
                //listaProductosDisponibles = NegProducto.RecuperarProductosLista(1, cantidad, textoBusqueda, codigoArea);

                if (areas.PEA_CODIGO != 1)
                {

                    if (listartodos && His.Entidades.Clases.Sesion.codDepartamento != 7)
                    {
                        Productos = NegProducto.RecuperarProductosListaSPall(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                    }
                    else if (listartodos && His.Entidades.Clases.Sesion.codDepartamento == 7) //Cambios Edgar 20210203
                    {
                        //aqui se filtraran si es cajero se mostraran unicamente todos los servicios
                        Productos = NegProducto.RecuperarProductosListaServicios(1, txt_Nombre.Text.ToString(), _Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                    }
                    else
                    {
                        Productos = NegProducto.RecuperarProductosListaSP(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                    }


                }
                else
                {

                    Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, txt_Nombre.Text.ToString(), /*areas.PEA_CODIGO*/_Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);

                }

                //gridProductos.DataSource = listaProductosDisponibles;


                gridProductos.DataSource = Productos;

                if (Productos.Rows.Count > 0)
                {
                    gridProductos.Focus();
                }
                else
                {
                    txt_Nombre.Focus();
                }
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message);
            }
        }

        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_Buscar.Focus();
            }
        }
        private void cargarProductos()
        {

            listaProductosSolicitados = new List<PRODUCTO>();
            descripcion = null;
            try
            {
                //cargo la lista de cantidad a paginar
                List<int> listaCantidad = new List<int>() { 10, 20, 50, 100, 500, 1000 };
                cmb_Mostrar.DataSource = listaCantidad;
                cmb_Mostrar.SelectedIndex = 1;
                List<DataRow> List = new List<DataRow>();

                if (His.Parametros.FacturaPAR.BodegaPorDefecto == 1)
                {
                    if (NegAccesoOpciones.ParametroBodega())
                    {
                        His.Parametros.FacturaPAR.BodegaPorDefecto = 10;                        
                    }
                }
                else
                {
                    if (NegAccesoOpciones.ParametroBodega())
                    {
                        His.Parametros.FacturaPAR.BodegaPorDefecto = 10;
                        
                    }
                }
                if (areas.PEA_CODIGO != 1)
                {

                    if (codigoArea > 0)
                    {
                        //listaProductosDisponibles = NegProducto.RecuperarProductosLista(0, 20, null, areas.PEA_CODIGO);

                        if (listartodos)
                        {

                            Productos = NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/ , _Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                        }
                        else
                        {
                            Productos = NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/ , _Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                        }
                    }
                    else
                    {
                        //listaProductosDisponibles = NegProducto.RecuperarProductosLista(0, 20, null);
                        if (listartodos && His.Entidades.Clases.Sesion.codDepartamento != 7)
                        {
                            Productos = NegProducto.RecuperarProductosListaSPall(1, ""/*txtBuscar.Value.ToString()*/, _Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                        }
                        else if (listartodos && His.Entidades.Clases.Sesion.codDepartamento == 7) //Cambios Edgar Ramos
                        {
                            //aqui se filtraran si es cajero se mostraran unicamente todos los servicios
                            Productos = NegProducto.RecuperarProductosListaServicios(1, "", _Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                        }
                        else
                        {
                            Productos = NegProducto.RecuperarProductosListaSP(1, ""/*txtBuscar.Value.ToString()*/, _Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                        }

                    }
                }
                else
                {

                    if (codigoArea > 0)
                    {
                        //listaProductosDisponibles = NegProducto.RecuperarProductosLista(0, 20, null, areas.PEA_CODIGO);
                        Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/ , _Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                    }
                    else
                    {
                        //listaProductosDisponibles = NegProducto.RecuperarProductosLista(0, 20, null);
                        Productos = NegProducto.RecuperarProductosListaSP_Farmacia(1, ""/*txtBuscar.Value.ToString()*/, _Rubro.RUB_CODIGO, His.Parametros.FacturaPAR.BodegaPorDefecto, _CodigoEmpresa, _CodigoConvenio);
                    }

                }
                //foreach (DataRow dr in Productos.Rows)
                //{
                //    List.Add(dr);
                //}

                //gridProductos.DataSource = listaProductosDisponibles;


                gridProductos.DataSource = Productos;
                gridProductos.Columns["DIVISION"].Width = 200;
                gridProductos.Columns["CODIGO"].Width = 65;
                gridProductos.Columns["PRODUCTO"].Width = 250;
                gridProductos.Columns["STOCK"].Width = 90;
                gridProductos.Columns["IVA"].Width = 50;
                gridProductos.Columns["VALOR"].Width = 80;
                gridProductos.Columns["Cantidad"].Width = 0;
                DataGridViewColumn Column = gridProductos.Columns[7];
                Column.Visible = false;



                //cargo los valores por defecto de la grilla
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show(err.Message);
            }

        }

        private void gridProductos_DoubleClick(object sender, EventArgs e)
        {
            if(gridProductos.SelectedRows.Count > 0)
            {
                codpro = gridProductos.CurrentRow.Cells["CODIGO"].Value.ToString();
                division = gridProductos.CurrentRow.Cells["DIVISION"].Value.ToString();
                iva = gridProductos.CurrentRow.Cells["IVA"].Value.ToString();
                this.Close();
            }
        }

        private void FrmAyudaAuditoriaProductos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void FrmAyudaAuditoriaProductos_Load(object sender, EventArgs e)
        {
            
        }
    }
}
