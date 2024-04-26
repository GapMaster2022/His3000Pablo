using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using His.Datos;

namespace His.Negocio
{
    public class NegHonorarioExplorador
    {
        DatHonorario Honorario = new DatHonorario();
        public DataTable VerPacientes()
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.VerPacientes();
            return Tabla;
        }
        public DataTable FiltroFecha(DateTime fechadesde, DateTime fechahasta)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha(fechadesde, fechahasta);
            return Tabla;
        }
        public DataTable FiltroHc(string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroHc(hc);
            return Tabla;
        }
        public DataTable FiltroMedico(string med_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroMedico(Convert.ToInt32(med_codigo));
            return Tabla;
        }
        public DataTable FiltroVale(string numvale)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroVale(numvale);
            return Tabla;
        }
        public DataTable FiltroFactura(string numFac)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFactura(numFac);
            return Tabla;
        }
        public DataTable FiltroFecha_Hc(string fechadesde, string fechahasta, string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Hc(fechadesde, fechahasta, hc);
            return Tabla;
        }
        public DataTable FiltroFecha_Medico(string fechadesde, string fechahasta, string med_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Medico(fechadesde, fechahasta, Convert.ToInt32(med_codigo));
            return Tabla;
        }
        public DataTable FiltroFecha_Vale(string fechadesde, string fechahasta, string numvale)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Vale(fechadesde, fechahasta, numvale);
            return Tabla;
        }
        public DataTable FiltroFecha_Factura(string fechadesde, string fechahasta, string numFac)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Factura(fechadesde, fechahasta, numFac);
            return Tabla;
        }
        public DataTable FiltroHc_Medico(string hc, string med_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroHc_Medico(hc, Convert.ToInt32(med_codigo));
            return Tabla;
        }
        public DataTable FiltroHc_Vale(string hc, string numvale)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroHc_Vale(hc, numvale);
            return Tabla;
        }
        public DataTable FiltroHc_Factura(string hc, string numFac)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroHc_Factura(hc, numFac);
            return Tabla;
        }
        public DataTable FiltroMedico_Vale(string med_codigo, string numvale)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroMedico_Vale(Convert.ToInt32(med_codigo), numvale);
            return Tabla;
        }
        public DataTable FiltroMedico_Factura(string med_codigo, string numFac)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroMedico_Factura(Convert.ToInt32(med_codigo), numFac);
            return Tabla;
        }
        public DataTable FiltroVale_Factura(string numvale, string numfac)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroVale_Factura(numvale, numfac);
            return Tabla;
        }
        public DataTable FiltroFecha_Hc_Medico(string fechadesde, string fechahasta, string hc, string med_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Hc_Medico(fechadesde, fechahasta, hc, Convert.ToInt32(med_codigo));
            return Tabla;
        }
        public DataTable FiltroFecha_Hc_Vale(string fechadesde, string fechahasta, string hc, string numvale)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Hc_Vale(fechadesde, fechahasta, hc, numvale);
            return Tabla;
        }
        public DataTable FiltroFecha_Hc_Factura(string fechadesde, string fechahasta, string hc, string numFac)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Hc_Factura(fechadesde, fechahasta, hc, numFac);
            return Tabla;
        }
        public DataTable FiltroFecha_Medico_Vale(string fechadesde, string fechahasta, string med_codigo, string numvale)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Medico_Vale(fechadesde, fechahasta, Convert.ToInt32(med_codigo), numvale);
            return Tabla;
        }
        public DataTable FiltroFecha_Medico_Factura(string fechadesde, string fechahasta, string med_codigo, string numFac)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Medico_Factura(fechadesde, fechahasta, Convert.ToInt32(med_codigo), numFac);
            return Tabla;
        }
        public DataTable FiltroFecha_Vale_Factura(string fechadesde, string fechahasta, string numvale, string numFac)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroFecha_Vale_Factura(fechadesde, fechahasta, numvale, numFac);
            return Tabla;
        }
        public DataTable FiltroHc_Medico_Vale(string hc, string med_codigo, string numvale)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.FiltroHc_Medico_Vale(hc, Convert.ToInt32(med_codigo), numvale);
            return Tabla;
        }
        public DataTable RecuperarPedido(string cue_codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Honorario.RecuperarPedido(Convert.ToInt64(cue_codigo));
            return Tabla;
        }
        public void GuardarPedidoDevolucion(string cue_codigo, string usuario)
        {
            Honorario.GuardarPedidoDevolucion(Convert.ToInt64(cue_codigo), Convert.ToInt32(usuario));
        }
        public void GuardarPedidoDevolucionDetalle(string pro_codigo, string pro_descripcion, string cantidad, string valor, string iva, string pdd_codigo)
        {
            Honorario.GuardarPedidoDevolucionDetalle(Convert.ToInt32(pro_codigo), pro_descripcion, Convert.ToInt32(cantidad),
                Convert.ToDouble(valor), Convert.ToDouble(iva), Convert.ToInt64(pdd_codigo));
        }
        public void Eliminar(string cue_codigo)
        {
            Honorario.Eliminar(Convert.ToInt64(cue_codigo));
        }
    }
}
