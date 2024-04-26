using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegFormulariosHCU
    {
        public static void Crear(FORMULARIOS_HCU formulario)
        {
            new DatFormulariosHCU().Crear(formulario);
        }
        
        public static bool CrearHistopatologico(HC_HISTOPATOLOGICO formulario, List<HC_HISTOPATOLOGICO_DIAGNOSTICOS> lista)
        {
            return new DatFormulariosHCU().CrearHistopatologico(formulario, lista);
        }
        public static bool IngresoKardexInsumo(List<KARDEX_INSUMOS> lista)
        {
            return new DatFormulariosHCU().IngresoKardexInsumo( lista);
        }
        public static bool CrearHistopatologicoB(HC_HISTOPATOLOGICO_B formulario, List<HC_HISTOPATOLOGIA_B_DETALLE> lista)
        {
            return new DatFormulariosHCU().CrearHistopatologicoB(formulario, lista);
        }

        public static List<FORMULARIOS_HCU> RecuperarFormulariosHCU()
        {
            return new DatFormulariosHCU().RecuperarFormulariosHCU();
        }
        public static List<FRECUENCIA> RecuperarFrecuencias()
        {
            return new DatFormulariosHCU().RecuperarFrecuencias();
        }
        public static List<LISTADO_COMPUESTOS> RecuperaListaCompuestso()
        {
            return new DatFormulariosHCU().RecuperaListaCompuestso();
        }
        public static List<FRECUENCIA_HORAS> RecuperarFrecuenciasHoras(int codigo)
        {
            return new DatFormulariosHCU().RecuperarFrecuenciasHoras(codigo);
        }
        public static HC_HISTOPATOLOGICO RecuperaHistopatologico(Int64 id)
        {
            return new DatFormulariosHCU().RecuperaHistopatologico(id);
        }
        public static CIE10 RecuperaCieCodigo(string codigo)
        {
            return new DatFormulariosHCU().RecuperaCieCodigo(codigo);
        }

        public static FORMULARIOS_HCU RecuperarFormularioID(int codigo)
        {
            return new DatFormulariosHCU().RecupararFormularioID(codigo);
        }
        public static int ultimoCodigoFormularios()
        {
            return new DatFormulariosHCU().ultimoCodigoFormulario();
        }
        public static void Editar(FORMULARIOS_HCU formulario)
        {
            new DatFormulariosHCU().Editar(formulario);
        }
        public static void Eliminar(FORMULARIOS_HCU formulario)
        {
            new DatFormulariosHCU().Eliminar(formulario);
        }
        public static List<KeyValuePair<int, string>> RecuperarFormulariosLista()
        {
            return new DatFormulariosHCU().RecuperarFormulariosLista();
        }

        public static DataTable LlenaCombos(string tipo)
        {
            return new DatFormulariosHCU().LlenaCombos(tipo);
        }

        public static DataTable Paciente(string ate_codigo)
        {
            return new DatFormulariosHCU().Paciente(ate_codigo);
        }

        public static List<KardexEnfermeriaMEdicamentos> RecuperaMedicamentos(string ate_codigo, int rubro, int check)
        {
            return new DatFormulariosHCU().RecuperaMedicamentos(ate_codigo, rubro, check);
        }
        public static List<RESERVA_KARDEX_MEDICAMENTO> RecuperaReservas(Int64 ate_codigo)
        {
            return new DatFormulariosHCU().RecuperaReservas(ate_codigo);
        }
        public static MEDICAMENTO_COMPUESTO_ENCABEZADO RecuperaMedCompuestoEncabezado(Int64 ate_codigo)
        {
            return new DatFormulariosHCU().RecuperaMedCompuestoEncabezado(ate_codigo);
        }
        public static List<MEDICAMENTO_COMPUESTO_DETALLE> RecuperaMedCompuestoDetalle(Int64 id_encabezado)
        {
            return new DatFormulariosHCU().RecuperaMedCompuestoDetalle(id_encabezado);
        }
        public static bool ModificaReservas(Int64 idReservas)
        {
            return new DatFormulariosHCU().ModificaReservas(idReservas);
        }

        public static bool IngresaKardex(IngresaKardex ingresa, int usuario)
        {
            return new DatFormulariosHCU().IngresaKardex(ingresa, usuario);
        }
        public static bool GuardaReservas(RESERVA_KARDEX_MEDICAMENTO obj)
        {
            return new DatFormulariosHCU().GuardaReservas(obj);
        }
        public static bool GuardaMedCompuestoEncabezado(MEDICAMENTO_COMPUESTO_ENCABEZADO obj)
        {
            return new DatFormulariosHCU().GuardaMedCompuestoEncabezado(obj);
        }
        public static bool GuardaMedCompuesatoDetalle(List<MEDICAMENTO_COMPUESTO_DETALLE> obj)
        {
            return new DatFormulariosHCU().GuardaMedCompuesatoDetalle(obj);
        }

        public static bool IngresaKardexInsumo(IngresaKardex ingresa, int usuario)
        {
            return new DatFormulariosHCU().IngresaKardexInsumo(ingresa, usuario);
        }

        public static DataTable ReporteDatos(string ate_codigo)
        {
            return new DatFormulariosHCU().ReporteDatos(ate_codigo);
        }

        public static DataTable ReporteDatosInsumos(string ate_codigo)
        {
            return new DatFormulariosHCU().ReporteDatosInsumos(ate_codigo);
        }

        public static DataTable RecuperaKardex(string ate_codigo)
        {
            return new DatFormulariosHCU().RecuperaKardex(ate_codigo);
        }

        public static bool ActualizaMedicamento(DateTime hora, bool check, string observacion, Int64 codigo)
        {
            return new DatFormulariosHCU().ActualizaMedicamento(hora, check,observacion, codigo);
        }
        public static bool EditarKardexMedicamento(int user, Int64 codigo, bool valor, string via, int aux = 1, string frecuencia="", string observacion = "")
        {
            return new DatFormulariosHCU().EditarKardexMedicamento(user, codigo, valor, via, aux, frecuencia, observacion);
        }
        public static void EliminarProdKardexMed(int codigo)
        {
            new DatFormulariosHCU().EliminarProductoKardexM(codigo);
        }

        public static DataTable RecuperaPrefacturaRubros(Int32 ateCodigo)
        {
            return new DatFormulariosHCU().RecuperaPrefacturaRubros(ateCodigo);
        }

        public static DataTable RecuperaPrefacturaDatos(Int32 ateCodigo)
        {
            return new DatFormulariosHCU().RecuperaPrefacturaDatos(ateCodigo);
        }
    }
}
