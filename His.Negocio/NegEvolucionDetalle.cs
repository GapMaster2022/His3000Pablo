using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using His.Datos;
using System.Data;

namespace His.Negocio
{
    public class NegEvolucionDetalle
    {
        public static void crearEvolucionDetalle(HC_EVOLUCION_DETALLE nEvolucionDetalle)
        {
            new DatEvolucionDetalle().crearEvolucionDetalle(nEvolucionDetalle);
        }

        public static List<HC_EVOLUCION_DETALLE> listaNotasEvolucion(Int64 codEvolucion)
        {
            return new DatEvolucionDetalle().listaNotasEvolucion(codEvolucion);
        }

        public static DataTable RecuperaPrescripciones(Int64 EVD_CODIGO)
        {
            return new DatEvolucionDetalle().RecuperaPrescripciones(EVD_CODIGO);
        }
        public static EVOLUCIONDETALLE ultimaNotaEvolucion(Int64 codEvolucion)
        {
            return new DatEvolucionDetalle().ultimaNotaEvolucion(codEvolucion);
        }

        public static int ultimoCodigo()
        {
            return new DatEvolucionDetalle().ultimoCodigo();
        }

        public static int GuardaEvolucionEnfermeria (int ate_codigo,int pac_codigo, int id_usuario,string nom_usuario)
        {
            return new DatEvolucionDetalle().GuardaEvolucionEnfermeria(ate_codigo, pac_codigo, id_usuario, nom_usuario);
        }
        public static int GrabaEvolucionEnfermeriaPrescripciones(int EVD_CODIGO, string PRES_FARMACOTERAPIA_INDICACIONES, string PRES_FARMACOS_INSUMOS, Boolean PRES_ESTADO, string PRES_FECHA_ADMINISTRACION)
        {
            return new DatEvolucionDetalle().GuardaEvolucionEnfermeriaPrescripciones(EVD_CODIGO, PRES_FARMACOTERAPIA_INDICACIONES, PRES_FARMACOS_INSUMOS, PRES_ESTADO, PRES_FECHA_ADMINISTRACION);
        }
        public static int GuardaEvolucionEnfermeriaDetalle(int EVO_CODIGO, int ID_USUARIO, string NOM_USUARIO, string EVD_DESCRIPCION)
        {
            return new DatEvolucionDetalle().GuardaEvolucionEnfermeriaDetalle(EVO_CODIGO, ID_USUARIO, NOM_USUARIO, EVD_DESCRIPCION);
        }

        public static DataTable RecuperaEvdCodigo(int EVO_CODIGO)
        {
            return new DatEvolucionDetalle().RecuperaEvdCodigo(EVO_CODIGO);
        }
        public static List<HC_EVOLUCION_DETALLE> RecuperoTodasEvolucionDetalle(Int64 EVO_CODIGO)
        {
            return new DatEvolucionDetalle().RecuperoTodasEvolucionDetalle(EVO_CODIGO);
        }

        public static DataTable FechasResponsabilidad(Int64 evoCodigo, Int64 medCodigo)
        {
            return new DatEvolucionDetalle().FechasResponsabilidad(evoCodigo, medCodigo);
        }
        public static DataTable verificaEvolucionEnfermeria(Int64 ATE_CODIGO)
        {
            return new DatEvolucionDetalle().verificaEvolucionEnfermeria(ATE_CODIGO);
        }
        public static void editarNotaEvolucionMedica(string notaModificada, DateTime fechaInicio, DateTime fechaFin, string evd_codigo, string docs, string medicoTratante)
        {
            new DatEvolucionDetalle().editarNotaEvolucionMedica(notaModificada, fechaInicio, fechaFin, Convert.ToInt32(evd_codigo), docs, medicoTratante);
        }
        public static void editarNotaEvolucion(HC_EVOLUCION_DETALLE notaModificada, DateTime fechaInicio, DateTime fechaFin)
        {
            new DatEvolucionDetalle().editarNotaEvolucion(notaModificada, fechaInicio, fechaFin);
        }
        public void EliminarEvolucion(string observacion, int id_usuario, Int64 ate_codigo, int evo_codigo, Int64 evd_codigo)
        {
            DatEvolucionDetalle evolucion = new DatEvolucionDetalle();
            evolucion.EliminarEvolucion(observacion, id_usuario, ate_codigo, evo_codigo, evd_codigo);
        }
        public static List<HC_EVOLUCION_DETALLE> RecuperaEvolucion(int evo_codigo)
        {
            return new DatEvolucionDetalle().RecuperaEvolucion(evo_codigo);
        }
        public static HC_EVOLUCION_DETALLE RecuperaEvolucionDetalle(int evo_codigo)
        {
            return new DatEvolucionDetalle().RecuperaEvolucionDetalle(evo_codigo);
        }
        public static List<HC_PRESCRIPCIONES> RecuperaEvoPrescripciones(int evd_codigo)
        {
            return new DatEvolucionDetalle().RecuperaEvoPrescripciones(evd_codigo);
        }
        public static USUARIOS RecuperarUsuario(int id_usuario)
        {
            return new DatEvolucionDetalle().RecuperarUsuario(id_usuario);
        }
        public DataTable FechaHoraEvolucion(int evd_codigo)
        {
            DatEvolucionDetalle evo = new DatEvolucionDetalle();
            DataTable Tabla = evo.FechaYHora(evd_codigo);
            return Tabla;
        }
    }
}

