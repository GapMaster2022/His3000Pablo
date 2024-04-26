using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatEpicrisis
    {
        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EPICRISIS
                             select d.EPI_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }
                
        public HC_EPICRISIS recuperarEpicrisisPorAtencion(int codigoAtencion)
        {
            HC_EPICRISIS epicrisis;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                epicrisis = (from e in contexto.HC_EPICRISIS
                             where e.ATENCIONES.ATE_CODIGO == codigoAtencion
                             select e).FirstOrDefault();
                    
                return epicrisis;
            }
        }


        public DataTable RecuperaEvolucionAnalisis(Int64 Ate_Codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataSet Dts;
            BaseContextoDatos obj = new BaseContextoDatos();
            Sqlcon = obj.ConectarBd();
            try
            {
                Sqlcon.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Sqlcmd = new SqlCommand("sp_RecuperaEvolucionAnalisis", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;


            Sqlcmd.Parameters.Add("@ATE_CODIGO", SqlDbType.Int);
            Sqlcmd.Parameters["@ATE_CODIGO"].Value = Ate_Codigo;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataSet();
            Sqldap.Fill(Dts, "tabla");

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Dts.Tables["tabla"];
        }

        public int ultimoCodigoDiagnostico()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EPICRISIS_DIAGNOSTICO
                             select d.HED_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }


        public int ultimoCodigoMedico()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EPICRISIS_MEDICOS
                             select d.EPM_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void ActualizarEpicrisis(HC_EPICRISIS epicrisis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EPICRISIS epicrisisDestino = contexto.HC_EPICRISIS.FirstOrDefault(e => e.EPI_CODIGO == epicrisis.EPI_CODIGO);
                epicrisisDestino.EPI_CONDICIONES_EGRESO = epicrisis.EPI_CONDICIONES_EGRESO;
                epicrisisDestino.EPI_CUADRO_CLINICO = epicrisis.EPI_CUADRO_CLINICO;
                epicrisisDestino.EPI_EVOLUCION = epicrisis.EPI_EVOLUCION;
                epicrisisDestino.EPI_FECHA_CREACION = epicrisis.EPI_FECHA_CREACION;
                epicrisisDestino.EPI_FECHA_EGRESO = epicrisis.EPI_FECHA_EGRESO;
                epicrisisDestino.EPI_FECHA_MODIFICACION = epicrisis.EPI_FECHA_MODIFICACION;
                epicrisisDestino.EPI_HALLAZGOS = epicrisis.EPI_HALLAZGOS;
                epicrisisDestino.EPI_TRATAMIENTO = epicrisis.EPI_TRATAMIENTO;
                epicrisisDestino.EPI_REALIZADO = epicrisis.EPI_REALIZADO;
                epicrisisDestino.TIPO_EGRESOReference.EntityKey = epicrisis.TIPO_EGRESOReference.EntityKey;
                contexto.SaveChanges();
            }
        }

        public void ActualizarDiagnosticos(HC_EPICRISIS_DIAGNOSTICO diagnostico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EPICRISIS_DIAGNOSTICO diagnosticoDestino = contexto.HC_EPICRISIS_DIAGNOSTICO.FirstOrDefault(d => d.HED_CODIGO == diagnostico.HED_CODIGO);
                diagnosticoDestino.HED_DESCRIPCION = diagnostico.HED_DESCRIPCION;
                diagnosticoDestino.HED_ESTADO = diagnostico.HED_ESTADO;
                diagnosticoDestino.HED_TIPO = diagnostico.HED_TIPO;
                diagnosticoDestino.ID_USUARIO = diagnostico.ID_USUARIO;
                diagnosticoDestino.CIE_CODIGO = diagnostico.CIE_CODIGO;
                contexto.SaveChanges();
            }
        }

        public List<HC_EPICRISIS_DIAGNOSTICO> recuperarDiagnosticosEpicrisisIngreso(int codEpicrisis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.MEDICOS.Where(m => m.MED_RECIBE_LLAMADA == true).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                //return contexto.HC_EPICRISIS_DIAGNOSTICO.Where(e => e.HC_EPICRISIS.EPI_CODIGO == codEpicrisis).ToList();
                var diagnosticos = (from d in contexto.HC_EPICRISIS_DIAGNOSTICO
                                    where d.HC_EPICRISIS.EPI_CODIGO == codEpicrisis && d.HED_TIPO== "I"
                                    select d).ToList();
                return diagnosticos;
            }
        }

        public List<HC_EPICRISIS_DIAGNOSTICO> recuperarDiagnosticosEpicrisisEgreso(int codEpicrisis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.MEDICOS.Where(m => m.MED_RECIBE_LLAMADA == true).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                //return contexto.HC_EPICRISIS_DIAGNOSTICO.Where(e => e.HC_EPICRISIS.EPI_CODIGO == codEpicrisis).ToList();
                var diagnosticos = (from d in contexto.HC_EPICRISIS_DIAGNOSTICO
                                    where d.HC_EPICRISIS.EPI_CODIGO == codEpicrisis && d.HED_TIPO== "E"
                                    select d).ToList();
                return diagnosticos;
            }
        }

        public List<HC_EPICRISIS_MEDICOS> recuperarMedicosEpicrisis(int codEpicrisis)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var medico = (from d in contexto.HC_EPICRISIS_MEDICOS
                              where d.HC_EPICRISIS.EPI_CODIGO == codEpicrisis
                              select d).ToList();
                return medico;
            }
        }

        public HC_EPICRISIS_MEDICOS recuperarMedicosEpicrisisCodigo(int codEpicrisis) // Recupera el campo EPM_CODIGO de la tabla HC_EPICRISIS_MEDICOS / Giovanny Tapia /18/09/2012
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from u in contexto.HC_EPICRISIS_MEDICOS
                        where u.HC_EPICRISIS.EPI_CODIGO == codEpicrisis
                        select u).FirstOrDefault();
                //return contexto.USUARIOS.FirstOrDefault(usu => usu.ID_USUARIO == codusu);
            }

        }


        public void ActualizarMedicos(HC_EPICRISIS_MEDICOS medico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EPICRISIS_MEDICOS medicoDestino = contexto.HC_EPICRISIS_MEDICOS.FirstOrDefault(d => d.EPM_CODIGO == medico.EPM_CODIGO);
                medicoDestino.MED_NOMBRE = medico.MED_NOMBRE;
                medicoDestino.MED_CODIGO = medico.MED_CODIGO;
                medicoDestino.MED_PERIODO_RESP = medico.MED_PERIODO_RESP;
                contexto.SaveChanges();
            }
        }

        public void EliminarMedicos(int codigoMedico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EPICRISIS_MEDICOS medico = contexto.HC_EPICRISIS_MEDICOS.FirstOrDefault(d => d.EPM_CODIGO == codigoMedico);
                contexto.Eliminar(medico);
            }
        }
    }
}
