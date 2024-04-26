using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
//using His.Datos.ConexionSic3000;

namespace His.Negocio
{
    public class NegProducto
    {
        //public static List<PRODUCTO> RecuperarProductosLista(string buscar, int criterio, int cantidad, int codigo)
        //{
        //    return new DatProducto().RecuperarProductosLista(buscar,criterio, cantidad, codigo);
        //}

        public static DataTable RecuperarPedido(int NumeroPedido)
        {
            return new DatProducto().RecuperarPedido(NumeroPedido);
        }

        public static DataTable RecuperarProductosListaSPall(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {
            return new DatProducto().RecuperarProductosListaSPall(Opcion, Filtro, Division, Bodega, CodigoEmpresa, CodigoConvenio);
        }

        public static DataTable RecuperarProductosListaServicios(int Opcion, string Filtro, int Division, int Bodega, int CodigoEmpresa, int CodigoConvenio)
        {
            return new DatProducto().RecuperarProductosListaServicios(Opcion, Filtro, Division, Bodega, CodigoEmpresa, CodigoConvenio);
        }
        public static DataTable RecuperarProductosListaSPPedidosAreas(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio, Int32 PedidosAreas)
        {
            return new DatProducto().RecuperarProductosListaSPPedidosAreas(Opcion, Filtro, Division, Bodega, CodigoEmpresa, CodigoConvenio, PedidosAreas);
        }
        public static DataTable RecuperarProductosListaSP(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {
            return new DatProducto().RecuperarProductosListaSP(Opcion, Filtro, Division, Bodega, CodigoEmpresa, CodigoConvenio);
        }

        public static DataTable RecuperarProductosListaSPconvenios(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {
            return new DatProducto().RecuperarProductosListaSPconvenios(Opcion, Filtro, Division, Bodega, CodigoEmpresa, CodigoConvenio);
        }

        public static DataTable RecuperarProductosListaSP_Farmacia(int Opcion, string Filtro, int Division, int Bodega, Int32 CodigoEmpresa, Int32 CodigoConvenio)
        {
            return new DatProducto().RecuperarProductosListaSP_Farmacia(Opcion, Filtro, Division, Bodega, CodigoEmpresa, CodigoConvenio);
        }

        public static List<PRODUCTO> RecuperarProductosLista()
        {
            return new DatProducto().RecuperarProductosLista();
        }
        /// <summary>
        /// Metodo que retorna la lista paginada de productos
        /// </summary>
        /// <param name="desde">registro desde el cual se empieza la seleccion</param>
        /// <param name="cantidad">cantidad de registros que se seleccionaran</param>
        /// <returns></returns>
        public static List<PRODUCTO> RecuperarProductosLista(int desde, int cantidad, string busqueda)
        {
            return new DatProducto().RecuperarProductosLista(desde, cantidad, busqueda);
        }
        /// <summary>
        /// Metodo que retorna la lista paginada de productos
        /// </summary>
        /// <param name="desde">registro desde el cual se empieza la seleccion</param>
        /// <param name="cantidad">cantidad de registros que se seleccionaran</param>
        /// <param name="codigoArea">Codigo del Area a la que esta relacionada la estructura</param>
        /// <returns></returns>
        public static List<PRODUCTO> RecuperarProductosLista(int desde, int cantidad, string busqueda, Int16 codigoArea)
        {
            return new DatProducto().RecuperarProductosLista(desde, cantidad, busqueda, codigoArea);
        }
        public static PRODUCTO RecuperarProductoID(int codProducto)
        {
            return new DatProducto().RecuperarProductoID(codProducto);
        }

        public static List<PRODUCTO> RecuperarProducto(int codProductoEst)
        {
            return new DatProducto().RecuperarProducto(codProductoEst);
        }

        public static List<PRODUCTO> RecuperarProductoSubDiv(int codSubDivision)
        {
            return new DatProducto().RecuperarProductoSubDiv(codSubDivision);
        }

        //public static List<DtoLaboratorioEstructura> RecuperarProductoSub(Int16 codSub)
        //{
        //    return new DatProducto().RecuperarProductoSub(codSub);
        //}

        #region

        public static DataTable RecuperarProductoSic(string codpro)
        {
            return new His.Datos.ConexionSic3000.DatProducto().RecuperarProductos(codpro);
        }

        #endregion

        public static DataTable DivisionProducto(string codpro, int codigo)
        {
            return new DatProducto().DivisionProducto(codpro, codigo);
        }
        public static Int64 NumeroDocumento()
        {
            return new DatProducto().NumeroDocumento();
        }

        public static DataTable RecuperarProductoIDSIC(Int64 codProducto)
        {
            return new DatProducto().RecuperarProductoIDSIC(codProducto);
        }
        public static DataTable listaproductosXdescripcion(string descripcion)
        {
            return new DatProducto().listaproductosXdescripcion(descripcion);
        }
        public static DataTable recuperaProductoSicXcodpro(string codpro)
        {
            return new DatProducto().recuperaProductoSicXcodpro(codpro);
        }
    }
}
