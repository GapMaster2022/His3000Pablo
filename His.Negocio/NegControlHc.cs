using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegControlHc
    {
        DatControlHc Datos = new DatControlHc();
        public DataTable PorFecha(DateTime fechainicio, DateTime fechafin)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorFecha(fechainicio, fechafin);
            return Tabla;
        }
        public DataTable PorFechaAlta(DateTime fechainicio, DateTime fechafin)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorFechaAlta(fechainicio, fechafin);
            return Tabla;
        }
        public DataTable PorHc(string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorHc(hc);
            return Tabla;
        }
        public DataTable Documentos()
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.MostrarDocumentos();
            return Tabla;
        }
        public DataTable PorAtencionControl(string atencion, string codigo)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.PorAtencionControl(atencion, Convert.ToInt32(codigo));
            return Tabla;
        }
        public DataTable PorEstado(string estado)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorEstado(estado);
            return Tabla;
        }
        public DataTable PorFechayHc(DateTime fechainicio, DateTime fechafin, string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorFechayHc(fechainicio, fechafin, hc);
            return Tabla;
        }
        public DataTable PorFechaAltayHc(DateTime fechainicio, DateTime fechafin, string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorFechaAltayHc(fechainicio, fechafin, hc);
            return Tabla;
        }
        public DataTable PorFechayEstado(DateTime fechainicio, DateTime fechafin, string estado)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorFechayEstado(fechainicio, fechafin, estado);
            return Tabla;
        }
        public DataTable PorFechaAltayEstado(DateTime fechainicio, DateTime fechafin, string estado)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorFechaAltayEstado(fechainicio, fechafin, estado);
            return Tabla;
        }
        public DataTable PorHCyEstado(string hc, string estado)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorHcyEstado(hc, estado);
            return Tabla;
        }
        public DataTable PorFechaHCEstado(DateTime fechainicio, DateTime fechafin, string hc, string estado)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorFechaHCEstado(fechainicio, fechafin, hc, estado);
            return Tabla;
        }
        public DataTable PorFechaAltaHCEstado(DateTime fechainicio, DateTime fechafin, string hc, string estado)
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.ControlPorFechaAltaHCEstado(fechainicio, fechafin, hc, estado);
            return Tabla;
        }
        public void ControlInsert(string estado, string fecha, string usuario, string paciente, 
            string control, string atencion, string estatus)
        {
            Datos.ControlInsertar(estado, fecha, usuario, Convert.ToInt32(paciente), Convert.ToInt32(control),
                Convert.ToInt32(atencion), estatus);
        }
        public string Cantidad()
        {
            string valor = null;
            valor = Datos.Cantidad();
            return valor;
        }
        public void ControlActualizar(string atencion, string control)
        {
            Datos.ControlActualizar(Convert.ToInt32(atencion),
                Convert.ToInt32(control));
        }
        public DataTable TablaControl()
        {
            DataTable Tabla = new DataTable();
            Tabla = Datos.TablaControlHc();
            return Tabla;
        }
        public void InsertarActualizar(string codigo, string descripcion)
        {
            Datos.ControlActualizaInserta(Convert.ToInt32(codigo), descripcion);
        }
        public string Ultimo()
        {
            string valor;
            valor = Datos.UltimoNumero();
            return valor;
        }
        public void ControlEliminar(string codigo)
        {
            Datos.ControlEliminar(Convert.ToInt32(codigo));
        }
        public void ControlCerrar(Int64 atencion, string estatus)
        {
            Datos.ControlCerrar(atencion, estatus);
        }
        public void ControlAbrir(Int64 atencion, string estatus)
        {
            Datos.ControlAbrir(atencion, estatus);
        }
    }
}
