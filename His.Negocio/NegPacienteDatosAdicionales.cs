using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;

namespace His.Negocio
{
    public class NegPacienteDatosAdicionales
    {
        public static void CrearPacienteDatosAdicionales(PACIENTES_DATOS_ADICIONALES datosPaciente, Int64 codigoPaciente)
        {
            new DatPacientesDatosAdicionales().CrearPacienteDatosAdicionales(datosPaciente, codigoPaciente);
        }
        public static void EditarPacienteDatosAdicionales(PACIENTES_DATOS_ADICIONALES datosPaciente)
        {
            new DatPacientesDatosAdicionales().EditarPacienteDatosAdicionales(datosPaciente);
        }
        public static void EditarPacienteDatosAdicionalesHonorarios(PACIENTES_DATOS_ADICIONALES datosPaciente)
        {
            new DatPacientesDatosAdicionales().EditarPacienteDatosAdicionalesHonorarios(datosPaciente);
        }
        public static int ultimoCodigoDatos()
        {
            return new DatPacientesDatosAdicionales().ultimoCodigoDatos();
        }
        public static PACIENTES_DATOS_ADICIONALES RecuperarDatosPacientesID(int codigo)
        {
            return new DatPacientesDatosAdicionales().RecuperarDatosPacienteID(codigo);
        }
        public static void ReestablecerEstados(int codigoPaciente)
        {
            new DatPacientesDatosAdicionales().ReestablecerEstados(codigoPaciente);
        }
        public static List<PACIENTES_DATOS_ADICIONALES> listaDatosAdicionales(int keyPaciente)
        {
            return new DatPacientesDatosAdicionales().listaDatosAdicionales(keyPaciente);
        }
        public static PACIENTES_DATOS_ADICIONALES RecuperarDatosAdicionalesPaciente(Int64 keyPaciente)
        {
            return new DatPacientesDatosAdicionales().RecuperarDatosAdicionalesPaciente(keyPaciente);
        }
        public static List<DtoPacienteDatosAdicionales> listaDatosAdicionalesDto(int keyPaciente)
        {
            return new DatPacientesDatosAdicionales().listaDatosAdicionalesDto(keyPaciente);
        }
        public static PACIENTES_DATOS_ADICIONALES RecuperarDatosAdicionalesPacienteID(int ateCodigo)
        {
            return new DatPacientesDatosAdicionales().RecuperarDatosAdicionalesPacienteID(ateCodigo);
        }
        public static Int16 ultimoNumeroRegistro(int codPaciente)
        {
            return new DatPacientesDatosAdicionales().ultimoNumeroRegistro(codPaciente);
        }
        public static void PDA2_save(DtoPacienteDatosAdicionales2 pda)
        {
            new DatPacientesDatosAdicionales().PDA2_save(pda);
        }
        public static DtoPacienteDatosAdicionales2 PDA2_find(Int64 codigoPaciente)
        {
            return new DatPacientesDatosAdicionales().PDA2_find(codigoPaciente);
        }
        public static void RevertirDefuncion(int pac_codigo)
        {
            new DatPacientesDatosAdicionales().Reversion(pac_codigo);
        }
        public static PACIENTES_DATOS_ADICIONALES2 pacientesdatos2(Int64 pac_codigo)
        {
            return new DatPacientesDatosAdicionales().pacientesdatos2(pac_codigo);
        }
    }
}
