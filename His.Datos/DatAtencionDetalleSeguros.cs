using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatAtencionDetalleSeguros
    {
        public void Crear(ATENCIONES_DETALLE_SEGUROS nuevoDetalleSeguro)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    contexto.Crear("ATENCIONES_DETALLE_SEGUROS", nuevoDetalleSeguro);
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }




        public int ultimoCodigoDetalleSeguros()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                int maxim = 0;

                if (contexto.ATENCIONES_DETALLE_SEGUROS.ToList().Count > 0)
                    maxim = (int) contexto.ATENCIONES_DETALLE_SEGUROS.Max(ads => ads.ADS_CODIGO);
                return maxim;
            }
        }

        public ATENCIONES_DETALLE_SEGUROS RecuperarDetalleSegurosAtencion(int codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from s in contexto.ATENCIONES_DETALLE_SEGUROS
                        where s.ATENCIONES.ATE_CODIGO == codAtencion
                        select s).FirstOrDefault();
            }
        }


        public void editarDetalleSeguros(ATENCIONES_DETALLE_SEGUROS detalleModif)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES_DETALLE_SEGUROS detalleOrigin =
                        contexto.ATENCIONES_DETALLE_SEGUROS.FirstOrDefault(s => s.ADS_CODIGO == detalleModif.ADS_CODIGO);
                    detalleOrigin.ADS_ASEGURADO_NOMBRE = detalleModif.ADS_ASEGURADO_NOMBRE;
                    detalleOrigin.ADS_ASEGURADO_CEDULA = detalleModif.ADS_ASEGURADO_CEDULA;
                    detalleOrigin.ADS_ASEGURADO_TELEFONO = detalleModif.ADS_ASEGURADO_TELEFONO;
                    detalleOrigin.ADS_ASEGURADO_PARENTESCO = detalleModif.ADS_ASEGURADO_PARENTESCO;
                    detalleOrigin.ADS_ASEGURADO_CIUDAD = detalleModif.ADS_ASEGURADO_CIUDAD;
                    detalleOrigin.ADS_ASEGURADO_DIRECCION = detalleModif.ADS_ASEGURADO_DIRECCION;
                    detalleOrigin.ADA_CODIGO = detalleModif.ADA_CODIGO;
                    detalleOrigin.ADA_NUM_PANTILLA = detalleModif.ADS_CODIGO;
                    detalleOrigin.ADA_CONTINGENCIA = detalleModif.ADA_CONTINGENCIA;
                    detalleOrigin.ADA_TIPO_DIAGNOSTICO = detalleModif.ADA_TIPO_DIAGNOSTICO;
                    detalleOrigin.ADA_TIEMPO_ANESTESIA = detalleModif.ADA_TIEMPO_ANESTESIA;
                    detalleOrigin.ADA_MARCA = detalleModif.ADA_MARCA;
                    detalleOrigin.ADA_TIPO_EXAMEN = detalleModif.ADA_TIPO_EXAMEN;
                    contexto.SaveChanges();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public bool eliminarAtencionDetalleSeguro(int codAtencion)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ATENCIONES_DETALLE_SEGUROS acceso =
                        contexto.ATENCIONES_DETALLE_SEGUROS.FirstOrDefault(s => s.ATENCIONES.ATE_CODIGO == codAtencion);
                    contexto.Eliminar(acceso);
                    return true;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Retorna diagnósticos de las atenciones 
        /// </summary>
        /// <param name="codAtenDetalle"></param>
        /// <returns>Lista de Diagnósticos</returns>
        public List<ATENCIONES_SEGUROS_DIAGNOSTICOS> recuperarDiagnosticosAtencion(int codAtenDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var diagnosticos = (from d in contexto.ATENCIONES_SEGUROS_DIAGNOSTICOS
                                    where d.ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO == codAtenDetalle
                                    select d).ToList();
                return diagnosticos;
            }
        }

        /// <summary>
        /// Método para guadar un objeto ATENCIONES_SEGUROS_DIAGNOSTICOS en la Base de Datos
        /// </summary>
        /// <param name="diagnostico">ATENCIONES_SEGUROS_DIAGNOSTICOS</param>
        public void crearASDiagnostico(ATENCIONES_SEGUROS_DIAGNOSTICOS diagnostico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                int codigo;
                if (contexto.ATENCIONES_SEGUROS_DIAGNOSTICOS.Count() > 0)
                    codigo = contexto.ATENCIONES_SEGUROS_DIAGNOSTICOS.Max(h => h.ASD_CODIGO) + 1;
                else
                    codigo = 1;
                diagnostico.ASD_CODIGO = codigo;
                contexto.AddToATENCIONES_SEGUROS_DIAGNOSTICOS(diagnostico);
                contexto.SaveChanges();
            }
        }

        /// <summary>
        /// Actualizar ATENCIONES_SEGUROS_DIAGNOSTICOS
        /// </summary>
        /// <param name="detalle">ATENCIONES_SEGUROS_DIAGNOSTICOS</param>
        public void actualizarASDiagnostico(ATENCIONES_SEGUROS_DIAGNOSTICOS detalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES_SEGUROS_DIAGNOSTICOS detalleD =
                    contexto.ATENCIONES_SEGUROS_DIAGNOSTICOS.FirstOrDefault(d => d.ASD_CODIGO == detalle.ASD_CODIGO);
                detalleD.CIE_CODIGO = detalle.CIE_CODIGO;
                detalleD.ASD_ESTADO = detalle.ASD_ESTADO;
                detalleD.ATENCIONES_DETALLE_SEGUROSReference.EntityKey =
                    detalle.ATENCIONES_DETALLE_SEGUROSReference.EntityKey;
                detalleD.ASD_DESCRIPCION = detalle.ASD_DESCRIPCION;
                detalleD.ID_USUARIO = detalle.ID_USUARIO;
                detalleD.ASD_INDICE = detalle.ASD_INDICE;
                contexto.SaveChanges();
            }
        }

        public void eliminarASDiagnosticoDetalle(int codigoDiagnosticoDetalle)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                ATENCIONES_SEGUROS_DIAGNOSTICOS diagDetalle = contexto.ATENCIONES_SEGUROS_DIAGNOSTICOS.FirstOrDefault(h => h.ASD_CODIGO == codigoDiagnosticoDetalle);
                contexto.DeleteObject(diagDetalle);
                contexto.SaveChanges();
            }
        }
        #region Explorador de Pacientes

        //public List<DtoCuentaAtencionesIESS> RecuperarAtencionesCuentasPacientes(string fechaAtencionIni, string fechaAtencionFin)
        //{
        //    try
        //    {
        //        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
        //        {
        //            if (fechaAtencionIni != null)
        //            {
        //                DateTime fechaIni = Convert.ToDateTime(fechaAtencionIni + " 00:00:00");
        //                DateTime fechaFin = Convert.ToDateTime(fechaAtencionFin + " 23:59:59");
        //                //Int16 tipoFormaPago = Convert.ToInt16(codTipoFormaPago);

        //                return (from p in contexto.PACIENTES
        //                            join pd in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals pd.PACIENTES.PAC_CODIGO
        //                            join a in contexto.ATENCIONES on p.PAC_CODIGO equals a.PACIENTES.PAC_CODIGO
        //                            join c in contexto.ATENCION_DETALLE_CATEGORIAS on a.ATE_CODIGO equals c.ATENCIONES.ATE_CODIGO
        //                            join ct in contexto.CATEGORIAS_CONVENIOS on c.CATEGORIAS_CONVENIOS.CAT_CODIGO equals ct.CAT_CODIGO
        //                            join d in contexto.ATENCIONES_DETALLE_SEGUROS on p.PAC_CODIGO equals d.ATENCIONES.ATE_CODIGO
        //                            join dg in contexto.ATENCIONES_SEGUROS_DIAGNOSTICOS on d.ADS_CODIGO equals dg.ATENCIONES_DETALLE_SEGUROS.ADS_CODIGO
        //                            where a.ATE_FECHA_INGRESO >= fechaIni && a.ATE_FECHA_INGRESO <= fechaFin && a.ATE_ESTADO == true
        //                            select new
        //                            {
        //                                 PAC_CODIGO = p.PAC_CODIGO,
        //                                 HISTORIA_CLINICA = p.PAC_HISTORIA_CLINICA,
        //                                 PAC_APELLIDOS = p.PAC_APELLIDO_PATERNO + " "+ p.PAC_APELLIDO_MATERNO,
        //                                 PAC_NOMBRES = p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2,
        //                                 PAC_IDENTIFICACION = p.PAC_IDENTIFICACION,
        //                                 PAC_GENERO = p.PAC_GENERO,
        //                                 PAC_FECHA_NACIMIENTO = p.PAC_FECHA_NACIMIENTO,
        //                                 ATE_CODIGO = a.ATE_CODIGO,
        //                                 ATE_FECHA_INGRESO = a.ATE_FECHA_INGRESO,
        //                                 ATE_EDAD_PACIENTE = a.ATE_EDAD_PACIENTE,
        //                                 CAT_CODIGO = ct.CAT_CODIGO,
        //                                 CAT_NOMBRE = ct.CAT_NOMBRE,
        //                                 ADA_AUTORIZACION = c.ADA_AUTORIZACION,
        //                                 HCC_CODIGO_TS = c.HCC_CODIGO_TS,
        //                                 HCC_CODIGO_DE = c.HCC_CODIGO_DE,
        //                                 ADS_CODIGO = d.ADS_CODIGO,
        //                                 ADA_CODIGO = c.ADA_CODIGO,
        //                                 ADS_ASEGURADO_NOMBRE = d.ADS_ASEGURADO_NOMBRE,
        //                                 ADS_ASEGURADO_CEDULA = d.ADS_ASEGURADO_CEDULA,
        //                                 ADS_ASEGURADO_PARENTESCO = d.ADS_ASEGURADO_PARENTESCO,
        //                                 ADA_CONTINGENCIA = d.ADA_CONTINGENCIA,
        //                                 ADA_TIPO_DIAGNOSTICO = d.ADA_TIPO_DIAGNOSTICO,
        //                                 ASD_CODIGO 
        //public string CIE_CODIGO;

        //                              h.HOM_CODIGO,
        //                              m.MED_CODIGO,
        //                              SUJETO_RETENCION = m.MED_APELLIDO_MATERNO + " " + m.MED_NOMBRE1,
        //                              m.MED_RUC,
        //                              h.HOM_FACTURA_MEDICO,
        //                              h.HOM_FACTURA_FECHA,
        //                              h.HOM_VALOR_NETO,
        //                              r.RET_PORCENTAJE,
        //                              VALOR_RETENCION = r.RET_PORCENTAJE * h.HOM_VALOR_NETO,
        //                              CONRETENCION = false,
        //                              h.RET_CODIGO1
        //                          };
                      
                        
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        Console.Write(err.Message);
        //        return null;
        //    }
        //}

        
        #endregion
    }
}
