using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using His.Datos;
using His.Entidades;

namespace His.Negocio
{
    public class NegImagen
    {
        public static DataTable getSubareas()
        {
            return new DatImagen().getSubareas();
        }
        public static DataTable getForm012(Int64 x)
        {
            return new DatImagen().getForm012(x);
        }
        public static DataTable getForm012Dx(Int64 x)
        {
            return new DatImagen().getForm012Dx(x);
        }
        public static DataTable getForm012Estudios(Int64 x)
        {
            return new DatImagen().getForm012Estudios(x);
        }

        public static DataTable getAgendamientos(string fechas, int x = 0)
        {
            return new DatImagen().getAgendamientos(fechas, x);
        }

        public static DataTable getAgendamientosInformes(string fechas)
        {
            return new DatImagen().getAgendamientosInformes(fechas);
        }

        public static DataTable RegistraCorreoEnvio(string email, Int64 id)
        {
            return new DatImagen().RegistraCorreoEnvio(email, id);
        }

        public static DataTable getAteImagen(int x)
        {
            return new DatImagen().getAteImagen(x);
        }
        
        public static DataTable getAteImagenCount(int x)
        {
            return new DatImagen().getAteImagenCount(x);
        }
        public static DataTable getAgendamiento(string id)
        {
            return new DatImagen().getAgendamiento(id);
        }
        public static HC_IMAGENOLOGIA_AGENDAMIENTOS_INFORME validarInformeHoras(int id)
        {
            return new DatImagen().ValidarEdicionHoras(id);
        }
        public static DataTable getAgendamientoEstudios(string id)
        {
            return new DatImagen().getAgendamientoEstudios(id);
        }

        public static void saveAgendamiento(int id_agendamiento, string[] p, List<PedidoImagen_estudios> e, DataTable dt = null)
        {
            new DatImagen().saveAgendamiento(id_agendamiento, p, e, dt);
        }

        public static void saveInformeAgendamiento(string[] p, List<PedidoImagen_diagnostico> x, Int64 id, int codMed)
        {
            new DatImagen().saveInformeAgendamiento(p, x, id, codMed);
        }

        public static void guardarCambios(string[] p, List<PedidoImagen_diagnostico> x, Int64 id)
        {
            new DatImagen().guardarCambios(p, x, id);
        }
        public static DataTable getReporteEncabezado(int idImagenologia)
        {
            return new DatImagen().getReporteEncabezado(idImagenologia);
        }
        public static DataTable getReporteRubros(int idImagenologia)
        {
            return new DatImagen().getReporteRubros(idImagenologia);
        }

        public static DataTable getPedidoCabecera(int idImagenologia)
        {
            return new DatImagen().getPedidoCabecera(idImagenologia);
        }
        public static DataTable getPedidoEstudios(int idImagenologia)
        {
            return new DatImagen().getPedidoEstudios(idImagenologia);
        }
        public static DataTable getPedidoDiagnosticos(int idImagenologia)
        {
            return new DatImagen().getPedidoDiagnosticos(idImagenologia);
        }

        public static DataTable getSolicitudes(int atecodigo)
        {
            return new DatImagen().getSolicitudes(atecodigo);
        }

        public static DataTable getInterconsultas(Int64 ate_codigo)
        {
            return new DatImagen().getInterconsultas(ate_codigo);
        }
        public static DataTable getHistopatologico(int ate_codigo)
        {
            return new DatImagen().getHistopatologico(ate_codigo);
        }
        public static DataTable getProtocolos(Int64 ate_codigo)
        {
            return new DatImagen().getProtocolos(ate_codigo);
        }
        public static DataTable getLaboratorio(Int64 ate_codigo)
        {
            return new DatImagen().getLaboratorio(ate_codigo);
        }
        public static bool getProtocoloExiste(Int64 adf_codigo)
        {
            return new DatImagen().getProtocoloExiste(adf_codigo);
        }
        public static Int64 getCodigoProtocolo(Int64 ate_codigo)
        {
            return new DatImagen().getCodigoProtocolo(ate_codigo);
        }
        public static DataTable getProductos()
        {
            return new DatImagen().getProductos();
        }

        public static DataTable getCIE10()
        {
            return new DatImagen().getCIE10();
        }

        public static void saveSolicitud(PedidoImagen x)
        {
            new DatImagen().saveSolicitud(x);
        }
        public static void deleteAgendamiento(int x, string y)
        {
            new DatImagen().deleteAgendamiento(x, y);
        }
        
        public DataTable PacientesImagen()
        {
            DatImagen imagen = new DatImagen();
            DataTable Tabla = new DataTable();
            Tabla = imagen.PacientesImagen();
            return Tabla;
        }

        public static DataTable CargarImagen(int id)
        {
            return new DatImagen().CargarInforme(id);
        }
        public static DataTable CargarImagenDx(int id)
        {
            return new DatImagen().CargarInformeDx(id);
        }
        public static void actualiceRadiologo(Int64 id, int medRadiologo)
        {
            new DatImagen().actualizarRadiologo(id, medRadiologo);
        }
    }
}
