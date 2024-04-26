using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DatHcEmergenciaFDetalle
    {
        public void crearHcEmergenciaDetalle(HC_EMERGENCIA_FORM_EXAMENES detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_FORM_EXAMENES(detalle);
                contexto.SaveChanges();
            }
        }

        public List<HC_EMERGENCIA_FORM_EXAMENES> listaDetalleHcEmergencia(int codHcEmer, string codPadre)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HC_EMERGENCIA_FORM_EXAMENES.Where(n => n.HC_EMERGENCIA_FORM.EMER_CODIGO == codHcEmer && n.EADE_PADRE == codPadre).OrderBy(x => x.HCC_CODIGO).ToList();
            }
        }



        public void actualizarDetalle(HC_EMERGENCIA_FORM_EXAMENES detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_EXAMENES detalleDestino = contexto.HC_EMERGENCIA_FORM_EXAMENES.FirstOrDefault(d => d.EADE_CODIGO == detalle.EADE_CODIGO);
                detalleDestino.EADE_DESCRIPCION = detalle.EADE_DESCRIPCION;
                detalleDestino.EADE_PADRE = detalle.EADE_PADRE;
                detalleDestino.HC_EMERGENCIA_FORMReference.EntityKey = detalle.HC_EMERGENCIA_FORMReference.EntityKey;
                detalleDestino.HCC_CODIGO = detalle.HCC_CODIGO;
                detalleDestino.ID_USUARIO = detalle.ID_USUARIO;
                contexto.SaveChanges();
            }
        }

        
        public int ultimoCodigo()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EMERGENCIA_FORM_EXAMENES
                             select d.EADE_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }




#region EXAMEN FISICO Y DIAGNOSTICO
        public void crearHcEmergenciaEFD(HC_EMERGENCIA_FORM_EXAMENFISICOD detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_FORM_EXAMENFISICOD(detalle);
                contexto.SaveChanges();
            }
        }

        public List<HC_EMERGENCIA_FORM_EXAMENFISICOD> listaEFDHcEmergencia(int codHcEmer)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.HC_EMERGENCIA_FORM_EXAMENFISICOD.Where(n => n.HC_EMERGENCIA_FORM.EMER_CODIGO == codHcEmer).ToList();
            }
        }



        public void actualizarEFD(HC_EMERGENCIA_FORM_EXAMENFISICOD detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_EXAMENFISICOD detalleDestino = contexto.HC_EMERGENCIA_FORM_EXAMENFISICOD.FirstOrDefault(d => d.EEFD_CODIGO == detalle.EEFD_CODIGO);
                detalleDestino.EEFD_DESCRIPCION = detalle.EEFD_DESCRIPCION;
                detalleDestino.HC_EMERGENCIA_FORMReference.EntityKey = detalle.HC_EMERGENCIA_FORMReference.EntityKey;
                detalleDestino.EEFD_TIPO = detalle.EEFD_TIPO;
                detalleDestino.HCC_CODIGO = detalle.HCC_CODIGO;
                detalleDestino.ID_USUARIO = detalle.ID_USUARIO;
                contexto.SaveChanges();
            }
        }



        public int ultimoCodigoEFD()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EMERGENCIA_FORM_EXAMENFISICOD
                             select d.EEFD_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void eliminarEFisico(int codigoEFD)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_EXAMENFISICOD detalle = contexto.HC_EMERGENCIA_FORM_EXAMENFISICOD.FirstOrDefault(h => h.EEFD_CODIGO == codigoEFD);
                contexto.DeleteObject(detalle);
                contexto.SaveChanges();
            }
        }

        public void eliminarEFD(int codigoEFD)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_EXAMENES detalle = contexto.HC_EMERGENCIA_FORM_EXAMENES.FirstOrDefault(h => h.EADE_CODIGO == codigoEFD);
                contexto.DeleteObject(detalle);
                contexto.SaveChanges();
            }
        }
        
#endregion

#region DIAGNOSTICOS

        public void crearHCEDiagnosticos(HC_EMERGENCIA_FORM_DIAGNOSTICOS nuevoDiagnostico)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    int codigo;
                    if (contexto.HC_EMERGENCIA_FORM_DIAGNOSTICOS.Count() > 0)
                        codigo = contexto.HC_EMERGENCIA_FORM_DIAGNOSTICOS.Max(h => h.ED_CODIGO) + 1;
                    else
                        codigo = 1;

                    nuevoDiagnostico.ED_CODIGO = codigo;
                    contexto.AddToHC_EMERGENCIA_FORM_DIAGNOSTICOS(nuevoDiagnostico);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }

        public void actualizarHcEmergenciaDiagnostico(HC_EMERGENCIA_FORM_DIAGNOSTICOS detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_DIAGNOSTICOS detalleDestino = contexto.HC_EMERGENCIA_FORM_DIAGNOSTICOS.FirstOrDefault(d => d.ED_CODIGO == detalle.ED_CODIGO);
                detalleDestino.CIE_CODIGO = detalle.CIE_CODIGO;
                detalleDestino.ED_ESTADO = detalle.ED_ESTADO;
                detalleDestino.HC_EMERGENCIA_FORMReference.EntityKey = detalle.HC_EMERGENCIA_FORMReference.EntityKey;
                detalleDestino.ED_DESCRIPCION = detalle.ED_DESCRIPCION;
                detalleDestino.ID_USUARIO = detalle.ID_USUARIO;
                contexto.SaveChanges();
            }
        }


        public HC_EMERGENCIA_FORM_DIAGNOSTICOS buscarDiagnostico(string codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.HC_ANAMNESIS_DETALLE.Where(n => n.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis).ToList();
                return (from n in contexto.HC_EMERGENCIA_FORM_DIAGNOSTICOS
                        where n.CIE_CODIGO == codigo
                        select n
                    ).FirstOrDefault();
            }
        }


        public List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> recuperarDiagnosticosHcEmergencia(int codHCEmergencia, string tipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var diagnosticos = (from d in contexto.HC_EMERGENCIA_FORM_DIAGNOSTICOS
                                    where d.HC_EMERGENCIA_FORM.EMER_CODIGO == codHCEmergencia & d.ED_TIPO == tipo
                                    select d).ToList();
                return diagnosticos;
            }
        }

        public List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> RecuperarDiagnosticos(Int64 codHCEmergencia)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var diagnosticos = (from d in contexto.HC_EMERGENCIA_FORM_DIAGNOSTICOS
                                    where d.HC_EMERGENCIA_FORM.EMER_CODIGO == codHCEmergencia
                                    select d).ToList();
                return diagnosticos;
            }
        }


        public int ultimoCodigoADiagnostico()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EMERGENCIA_FORM_DIAGNOSTICOS
                             select d.ED_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void eliminarDiagnosticoDetalle(int codigoDiagnosticoDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_DIAGNOSTICOS diagDetalle = contexto.HC_EMERGENCIA_FORM_DIAGNOSTICOS.FirstOrDefault(h => h.ED_CODIGO == codigoDiagnosticoDetalle);
                contexto.DeleteObject(diagDetalle);
                contexto.SaveChanges();
            }
        }

        

#endregion


        #region EMERGENCIA OBSTETRICA

        public void crearHCEObstetrica(HC_EMERGENCIA_FORM_OBSTETRICA hcEmergencia)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToHC_EMERGENCIA_FORM_OBSTETRICA(hcEmergencia);
                contexto.SaveChanges();

            }
        }

        public HC_EMERGENCIA_FORM_OBSTETRICA recuperarHCEObstetrica(int codHCEmergencia)
        {
            try
            {
                HC_EMERGENCIA_FORM_OBSTETRICA hcEmergencia;
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    hcEmergencia = (from d in contexto.HC_EMERGENCIA_FORM_OBSTETRICA
                                           where d.HC_EMERGENCIA_FORM.EMER_CODIGO == codHCEmergencia
                                           select d).FirstOrDefault();
                    return hcEmergencia;
                }
            }
            catch (Exception err) { throw err; }
        }


        public void actualizarHCEObstetrica(HC_EMERGENCIA_FORM_OBSTETRICA hcEmergencia)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_OBSTETRICA hcEmergenciaDestino = contexto.HC_EMERGENCIA_FORM_OBSTETRICA.FirstOrDefault(a => a.EOBT_CODIGO == hcEmergencia.EOBT_CODIGO);
                hcEmergenciaDestino.EOBT_GESTA = hcEmergencia.EOBT_GESTA;
                hcEmergenciaDestino.EOBT_PARTOS = hcEmergencia.EOBT_PARTOS;
                hcEmergenciaDestino.EOBT_ABORTOS = hcEmergencia.EOBT_ABORTOS;
                hcEmergenciaDestino.EOBT_CESAREAS = hcEmergencia.EOBT_CESAREAS;
                hcEmergenciaDestino.EOBT_FUM = hcEmergencia.EOBT_FUM;
                hcEmergenciaDestino.EOBT_SEM_GESTACION = hcEmergencia.EOBT_SEM_GESTACION;
                hcEmergenciaDestino.EOBT_MOV_FETAL = hcEmergencia.EOBT_MOV_FETAL;
                hcEmergenciaDestino.EOBT_FREC_CFETAL = hcEmergencia.EOBT_FREC_CFETAL;
                hcEmergenciaDestino.EOBT_MEM_SROTAS = hcEmergencia.EOBT_MEM_SROTAS;
                hcEmergenciaDestino.EOBT_TIEMPO = hcEmergencia.EOBT_TIEMPO;
                hcEmergenciaDestino.EOBT_ALT_UTERINA = hcEmergencia.EOBT_ALT_UTERINA;
                hcEmergenciaDestino.EOBT_PRESENTACION = hcEmergencia.EOBT_PRESENTACION;
                hcEmergenciaDestino.EOBT_DILATACION = hcEmergencia.EOBT_DILATACION;
                hcEmergenciaDestino.EOBT_BORRAMIENTO = hcEmergencia.EOBT_BORRAMIENTO;
                hcEmergenciaDestino.EOBT_PLANO = hcEmergencia.EOBT_PLANO;
                hcEmergenciaDestino.EOBT_PELVIS_UTIL = hcEmergencia.EOBT_PELVIS_UTIL;
                hcEmergenciaDestino.EOBT_SANGRADO_VAGINAL = hcEmergencia.EOBT_SANGRADO_VAGINAL;
                hcEmergenciaDestino.EOBT_CONTACCIONES = hcEmergencia.EOBT_CONTACCIONES;
                hcEmergenciaDestino.EOBT_OBSERVACION = hcEmergencia.EOBT_OBSERVACION;
                contexto.SaveChanges();
            }
        }


        public int ultimoCodigoHCEObstetrica()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EMERGENCIA_FORM_OBSTETRICA
                             select d.EOBT_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void eliminarHCEObstetrica(int codigoHCEObstetrica)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_OBSTETRICA obtDetalle = contexto.HC_EMERGENCIA_FORM_OBSTETRICA.FirstOrDefault(o => o.EOBT_CODIGO == codigoHCEObstetrica);
                contexto.DeleteObject(obtDetalle);
                contexto.SaveChanges();
            }
        }
#endregion

#region PLAN DE TRATAMIENTO

        public void crearTratamiento(HC_EMERGENCIA_FORM_TRATAMIENTO nuevoTratamiento)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    int codigo;
                    if (contexto.HC_EMERGENCIA_FORM_TRATAMIENTO.Count() > 0)
                        codigo = contexto.HC_EMERGENCIA_FORM_TRATAMIENTO.Max(h => h.ETRA_CODIGO) + 1;
                    else
                        codigo = 1;

                    nuevoTratamiento.ETRA_CODIGO = codigo;

                    contexto.AddToHC_EMERGENCIA_FORM_TRATAMIENTO(nuevoTratamiento);
                    contexto.SaveChanges();
                }
            }
            catch (Exception err) { throw err; }
        }

        public void actualizarTratameinto(HC_EMERGENCIA_FORM_TRATAMIENTO nuevoTratamiento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_TRATAMIENTO tratamiento = contexto.HC_EMERGENCIA_FORM_TRATAMIENTO.FirstOrDefault(d => d.ETRA_CODIGO == nuevoTratamiento.ETRA_CODIGO);
                tratamiento.ETRA_DESCRIPCION = nuevoTratamiento.ETRA_DESCRIPCION;
                tratamiento.ETRA_TIPO = nuevoTratamiento.ETRA_TIPO;
                //tratamiento.HC_EMERGENCIA_FORMReference.EntityKey = nuevoTratamiento.HC_EMERGENCIA_FORMReference.EntityKey;
                tratamiento.ID_USUARIO = nuevoTratamiento.ID_USUARIO;
                if (nuevoTratamiento.PRO_CODIGO != "")
                {
                    tratamiento.PRO_CODIGO = nuevoTratamiento.PRO_CODIGO;
                }

                if (nuevoTratamiento.EMER_POSOLOGIA != "")
                {
                    tratamiento.EMER_POSOLOGIA = nuevoTratamiento.EMER_POSOLOGIA;
                }
                if (nuevoTratamiento.VIA != "")
                {
                    tratamiento.VIA = nuevoTratamiento.VIA;
                }
                if (nuevoTratamiento.DOSIS != "")
                {
                    tratamiento.DOSIS = nuevoTratamiento.DOSIS;
                }
                if (nuevoTratamiento.DIAS != "")
                {
                    tratamiento.DIAS = nuevoTratamiento.DIAS;
                }

                contexto.SaveChanges();
            }
        }


        public HC_EMERGENCIA_FORM_TRATAMIENTO buscarTratameinto(int codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.HC_ANAMNESIS_DETALLE.Where(n => n.HC_ANAMNESIS.ANE_CODIGO == codAnamnesis).ToList();
                return (from n in contexto.HC_EMERGENCIA_FORM_TRATAMIENTO
                        where n.ETRA_CODIGO == codigo
                        select n
                    ).FirstOrDefault();
            }
        }


        public List<HC_EMERGENCIA_FORM_TRATAMIENTO> recuperarTratameinto(int codHCEmergencia, string tipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var diagnosticos = (from d in contexto.HC_EMERGENCIA_FORM_TRATAMIENTO
                                        where d.HC_EMERGENCIA_FORM.EMER_CODIGO == codHCEmergencia & d.ETRA_TIPO == tipo
                                        select d).ToList();
                    return diagnosticos;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public int ultimoCodigoTratameinto()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.HC_EMERGENCIA_FORM_TRATAMIENTO
                             select d.ETRA_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public void eliminarTratameinto(int codigoTratamiento)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                HC_EMERGENCIA_FORM_TRATAMIENTO traDetalle = contexto.HC_EMERGENCIA_FORM_TRATAMIENTO.FirstOrDefault(h => h.ETRA_CODIGO == codigoTratamiento);
                contexto.DeleteObject(traDetalle);
                contexto.SaveChanges();
            }
        }

#endregion
    }
}
