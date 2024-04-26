using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using His.Entidades.General;
using System.Data;

namespace His.Negocio
{
    public class NegPacienteGarantia
    {
        DatosGarantias garantia = new DatosGarantias();
        public DataTable CargarGarantias()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.Garantias();
            return Tabla;
        }
        public DataTable CargarGarantiasTodo()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.GarantiasTodo();
            return Tabla;
        }
        public DataTable TipoGarantia()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.TipoGarantia();
            return Tabla;
        }
        public DataTable TipoBanco()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.Banco();
            return Tabla;
        }
        public DataTable TipoTarjeta()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.TipoTarjeta();
            return Tabla;
        }
        public void ModificarGarantia(string tipogarantia, string beneficiario, string documento,
            string valor, string fecha, string banco, string tipotarjeta, string ccv, string dias, string lote,
            string autorizacion, string persona, string fechaau, string establecimiento, string codigo, 
            string numtarjeta, string usuario, string identificacion, string telefono, string caducidad)
        {
            garantia.ModificarGarantia(Convert.ToInt32(tipogarantia), beneficiario, documento, Convert.ToDouble(valor),
                fecha, banco, tipotarjeta, ccv, Convert.ToInt32(dias), lote, autorizacion, persona, fechaau, establecimiento, Convert.ToInt32(codigo),
                numtarjeta, usuario, identificacion, telefono, caducidad);
        }
        public void InsertarGarantia(string tipogarantia, string atencion, string beneficiario, string documento,
            string valor, string fecha, string banco, string tipotarjeta, string ccv, string dias, string lote,
            string autorizacion, string persona, string fechaau, string establecimiento, string numtarjeta, 
            string usuario, string identificacion, string telefono, string caducidad)
        {
            garantia.InsertarGarantia(Convert.ToInt32(tipogarantia), Convert.ToInt32(atencion), beneficiario, documento, Convert.ToDouble(valor),
                fecha, banco, tipotarjeta, ccv, Convert.ToInt32(dias), lote, autorizacion, persona, fechaau, establecimiento, numtarjeta, 
                usuario, identificacion, telefono, caducidad);
        }
        public string Aseguradora(string codigo)
        {
            string seguro = null;
            seguro = garantia.Aseguradora(codigo);
            return seguro;
        }
        public DataTable CargarGarantiaFechas(string fechainicio, string fechafin, string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarGarantiaFechas(fechainicio, fechafin, hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarGarantiaFechasCaduca(string fechainicio, string fechafin, string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarGarantiaFechasCaduca(fechainicio, fechafin, hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarGarantiaFechasCancelado(string fechainicio, string fechafin, string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarGarantiaFechasCancelado(fechainicio, fechafin, hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarGarantiaFechasVigente(string fechainicio, string fechafin, string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarGarantiaFechasVigente(fechainicio, fechafin, hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarGarantiaHC(string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarGarantiaHC(hc);
            return Tabla;
        }
        public void Anular(string fechaanula, string usuarioanula, string codigo, string observacion)
        {
            garantia.InsertarAnulacion(fechaanula, usuarioanula, Convert.ToInt32(codigo), observacion);
        }
        public void Caduca(string codigo, string fecha)
        {
            garantia.CaducaPreAutorizacion(Convert.ToInt32(codigo), fecha);
        }
        public DataTable CargarPacienteGarantiaTodo(string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarPacienteGarantiaTodo(hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarPacienteGarantiaCaducada(string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarPacienteGarantiaCaduca(hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarPacienteGarantiaCancelada(string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarPacienteGarantiaCancelada(hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarPacienteGarantiaVigente(string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.CargarPacienteGarantiaVigente(hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        //Desde Aqui
        public DataTable CargarPorFechas(DateTime fechainicio, DateTime fechafin)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechas(fechainicio, fechafin);
            return Tabla;
        }
        public DataTable CargarPorFechasHc(string fechainicio, string fechafin, string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechasHc(fechainicio, fechafin, hc);
            return Tabla;
        }
        public DataTable CargarPorFechasHcCaducada(string fechainicio, string fechafin, string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechasHcCaducada(fechainicio, fechafin, hc);
            return Tabla;
        }
        public DataTable CargarPorFechasHcCancelada(string fechainicio, string fechafin, string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechasHcCancelada(fechainicio, fechafin, hc);
            return Tabla;
        }
        public DataTable CargarPorFechasHcVigente(string fechainicio, string fechafin, string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechasHcVigente(fechainicio, fechafin, hc);
            return Tabla;
        }
        public DataTable CargarPorFechasHcTipo(string fechainicio, string fechafin, string hc, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechasHcTipo(fechainicio, fechafin, hc, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarPorHc(string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorHc(hc);
            return Tabla;
        }
        public DataTable CargarPorHcCaducada(string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorHcCaducada(hc);
            return Tabla;
        }
        public DataTable CargarPorHcCancelada(string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorHcCancelada(hc);
            return Tabla;
        }
        public DataTable CargarPorHcVigente(string hc)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorHcCancelada(hc);
            return Tabla;
        }
        public DataTable CargarPorTodo()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorTodo();
            return Tabla;
        }
        public DataTable CargarPorCaducada()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorCaducada();
            return Tabla;
        }
        public DataTable CargarPorCancelada()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorCancelada();
            return Tabla;
        }
        public DataTable CargarPorVigente()
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorVigente();
            return Tabla;
        }
        public DataTable CargarPorTipo(string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorTipo(Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarPorFechayTipo(string fechainicio, string fechafin, string tipo)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechayTipo(fechainicio, fechafin, Convert.ToInt32(tipo));
            return Tabla;
        }
        public DataTable CargarPorFechayCaducada(string fechainicio, string fechafin)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechayCaducada(fechainicio, fechafin);
            return Tabla;
        }
        public DataTable CargarPorFechayCancelada(string fechainicio, string fechafin)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechayCancelada(fechainicio, fechafin);
            return Tabla;
        }
        public DataTable CargarPorFechayVigente(string fechainicio, string fechafin)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechayVigente(fechainicio, fechafin);
            return Tabla;
        }
        public void Aprobacion(string fecha, string usuario, string codigo, string observacion)
        {
            garantia.InsertarAprobacion(fecha, usuario, Convert.ToInt32(codigo), observacion);
        }
        public DataTable FechaTodo(string fechainicio, string fechafin)
        {
            DataTable Tabla = new DataTable();
            Tabla = garantia.PorFechasTodo(Convert.ToDateTime(fechainicio), Convert.ToDateTime(fechafin));
            return Tabla;
        }
    }
}
