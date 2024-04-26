using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data;
using System.Data.SqlClient;

namespace His.Datos
{
    public class DatMedicos
    {
        public List<MEDICOS> listarMedicosAnestecilogos()
        {
            try
            {
                using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<MEDICOS> medicoAnest = new List<MEDICOS>();
                    medicoAnest = db.MEDICOS.Include("TIPO_MEDICO").Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_ESTADO == true && m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == 102).ToList();
                    return medicoAnest;
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        public List<DtoMedicos> MedicosdeAtenciones()
        {
            List<HONORARIOS_MEDICOS> usuarios = new List<HONORARIOS_MEDICOS>();
            List<MEDICOS> med = new List<MEDICOS>();
            List<DtoMedicos> dtousu = new List<DtoMedicos>();

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                usuarios = contexto.HONORARIOS_MEDICOS.Include("MEDICOS").ToList();
                med = contexto.MEDICOS.Include("ESPECIALIDADES_MEDICAS").ToList();

                foreach (var acceso in med)
                {
                    //bool valor = true;
                    if (usuarios.Where(per => per.MEDICOS.MED_CODIGO == acceso.MED_CODIGO).FirstOrDefault() != null)
                        dtousu.Add(new DtoMedicos()
                        {
                            MED_CODIGO = acceso.MED_CODIGO,
                            MED_APELLIDO_MATERNO = acceso.MED_APELLIDO_MATERNO,
                            MED_APELLIDO_PATERNO = acceso.MED_APELLIDO_PATERNO,
                            MED_RUC = acceso.MED_RUC,
                            ESP_NOMBRE = acceso.ESPECIALIDADES_MEDICAS.ESP_NOMBRE,
                            MED_NOMBRE1 = acceso.MED_NOMBRE1,
                            MED_NOMBRE2 = acceso.MED_NOMBRE2
                        });
                }

            }
            return dtousu;
        }
        public int RecuperaMaximoMedicos()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<MEDICOS> medicos = contexto.MEDICOS.ToList();
                if (medicos.Count > 0)
                    maxim = contexto.MEDICOS.Max(emp => emp.MED_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }

        }
        public List<DtoUsuarios> ListaUsuariosNoMedicos()
        {
            List<USUARIOS> usuarios = new List<USUARIOS>();
            List<USUARIOS> med = new List<USUARIOS>();
            List<DtoUsuarios> dtousu = new List<DtoUsuarios>();

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                usuarios = contexto.USUARIOS.ToList();
                med = contexto.USUARIOS
                   .Join(contexto.MEDICOS, b => b.ID_USUARIO, (MEDICOS a) => a.USUARIOS.ID_USUARIO, (b, a) => b)
                   .ToList();

                foreach (var acceso in usuarios)
                {
                    //bool valor = true;
                    if (med.Where(per => per.ID_USUARIO == acceso.ID_USUARIO).FirstOrDefault() == null)
                        dtousu.Add(new DtoUsuarios() { ID_USUARIO = acceso.ID_USUARIO, NOMBRES = acceso.NOMBRES, APELLIDOS = acceso.APELLIDOS });
                }

            }
            return dtousu;
        }
        public List<DtoMedicosConsulta> MedicosConsulta()
        {
            List<DtoMedicosConsulta> medicoc = new List<DtoMedicosConsulta>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<MEDICOS> medicos = new List<MEDICOS>();
                medicos = contexto.MEDICOS.Include("ESPECIALIDADES_MEDICAS").ToList();
                foreach (var acceso in medicos)
                {
                    medicoc.Add(new DtoMedicosConsulta()
                    {
                        ENTITYSETNAME = acceso.EntityKey.GetFullEntitySetName(),
                        ENTITYID = acceso.EntityKey.EntityKeyValues[0].Key,
                        ESP_NOMBRE = acceso.ESPECIALIDADES_MEDICAS.ESP_NOMBRE,
                        MED_CODIGO = acceso.MED_CODIGO,
                        MED_DIRECCION = acceso.MED_DIRECCION,
                        MED_DIRECCION_CONSULTORIO = acceso.MED_DIRECCION_CONSULTORIO,
                        MED_EMAIL = acceso.MED_EMAIL,
                        MED_NOMBRE = acceso.MED_APELLIDO_PATERNO + " " + acceso.MED_NOMBRE1,
                        MED_RUC = acceso.MED_RUC,
                        MED_TELEFONO_CASA = acceso.MED_TELEFONO_CASA,
                        MED_TELEFONO_CELULAR = acceso.MED_TELEFONO_CELULAR,
                        MED_TELEFONO_CONSULTORIO = acceso.MED_TELEFONO_CONSULTORIO,
                        MED_ESTADO = acceso.MED_ESTADO
                    });
                }
            }
            return medicoc;
        }
        public List<DtoMedicos> RecuperaMedicosFormulario()
        {
            List<DtoMedicos> medicogrid = new List<DtoMedicos>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //List<MEDICOS> medicos = new List<MEDICOS>();
                //medicos = contexto.MEDICOS.Include("ESPECIALIDADES_MEDICAS").Include("USUARIOS").Include("RETENCIONES_FUENTE").Include("BANCOS").Include("TIPO_MEDICO").Include("ESTADO_CIVIL").Include("TIPO_HONORARIO").Where(p=>p.MED_ESTADO==true).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                var medicos = (from m in contexto.MEDICOS
                               join e in contexto.ESPECIALIDADES_MEDICAS on m.ESPECIALIDADES_MEDICAS.ESP_CODIGO equals e.ESP_CODIGO
                               join u in contexto.USUARIOS on m.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                               join r in contexto.RETENCIONES_FUENTE on m.RETENCIONES_FUENTE.RET_CODIGO equals r.RET_CODIGO
                               join b in contexto.BANCOS on m.BANCOS.BAN_CODIGO equals b.BAN_CODIGO
                               join t in contexto.TIPO_MEDICO on m.TIPO_MEDICO.TIM_CODIGO equals t.TIM_CODIGO
                               join c in contexto.ESTADO_CIVIL on m.ESTADO_CIVIL.ESC_CODIGO equals c.ESC_CODIGO
                               join th in contexto.TIPO_HONORARIO on m.TIPO_HONORARIO.TIH_CODIGO equals th.TIH_CODIGO
                               // where m.MED_ESTADO ==true 
                               select new
                               {
                                   e.ESP_CODIGO,
                                   m.MED_AUTORIZACION_SRI,
                                   m.MED_APELLIDO_MATERNO,
                                   m.MED_APELLIDO_PATERNO
                              ,
                                   m.MED_CODIGO,
                                   m.MED_CUENTA_CONTABLE,
                                   m.MED_DIRECCION,
                                   m.MED_ESTADO,
                                   m.MED_FACTURA_FINAL,
                                   m.MED_FACTURA_INICIAL,
                                   m.MED_FECHA,
                                   m.MED_FECHA_NACIMIENTO,
                                   m.MED_FECHA_MODIFICACION,
                                   m.MED_EMAIL,
                                   m.MED_GENERO,
                                   m.MED_NOMBRE1,
                                   m.MED_NOMBRE2,
                                   m.MED_NUM_CUENTA,
                                   r.RET_CODIGO,
                                   r.RET_DESCRIPCION,
                                   r.RET_PORCENTAJE,
                                   m.MED_RUC,
                                   m.MED_TELEFONO_CASA,
                                   m.MED_TELEFONO_CELULAR,
                                   m.MED_TELEFONO_CONSULTORIO,
                                   m.MED_TIPO_CUENTA,
                                   m.MED_VALIDEZ_AUTORIZACION,
                                   m.MED_DIRECCION_CONSULTORIO,
                                   b.BAN_CODIGO,
                                   u.ID_USUARIO,
                                   t.TIM_CODIGO,
                                   e.ESP_NOMBRE,
                                   c.ESC_CODIGO,
                                   th.TIH_CODIGO,
                                   m.MED_CON_TRANSFERENCIA,
                                   m.MED_RECIBE_LLAMADA,
                                   m.MED_CODIGO_MEDICO,
                                   m.MED_CODIGO_LIBRO,
                                   m.MED_CODIGO_FOLIO


                               }).ToList();
                foreach (var acceso in medicos)
                {
                    medicogrid.Add(new DtoMedicos()
                    {
                        ENTITYSETNAME = "HIS3000BDEntities.MEDICOS",
                        ENTITYID = "MED_CODIGO",
                        ESP_CODIGO = acceso.ESP_CODIGO,
                        MED_AUTORIZACION_SRI = acceso.MED_AUTORIZACION_SRI,
                        MED_APELLIDO_MATERNO = acceso.MED_APELLIDO_MATERNO,
                        MED_APELLIDO_PATERNO = acceso.MED_APELLIDO_PATERNO,
                        MED_CODIGO = acceso.MED_CODIGO,
                        MED_CUENTA_CONTABLE = acceso.MED_CUENTA_CONTABLE,
                        MED_DIRECCION = acceso.MED_DIRECCION,
                        MED_ESTADO = acceso.MED_ESTADO == null ? false : bool.Parse(acceso.MED_ESTADO.ToString()),
                        MED_FACTURA_FINAL = acceso.MED_FACTURA_FINAL,
                        MED_FACTURA_INICIAL = acceso.MED_FACTURA_INICIAL,
                        MED_FECHA = acceso.MED_FECHA == null ? DateTime.Parse("01/01/2010") : DateTime.Parse(acceso.MED_FECHA.ToString()),
                        MED_FECHA_NACIMIENTO = acceso.MED_FECHA_NACIMIENTO == null ? DateTime.Parse("01/01/0001") : DateTime.Parse(acceso.MED_FECHA_NACIMIENTO.ToString()),
                        MED_FECHA_MODIFICACION = acceso.MED_FECHA_MODIFICACION == null ? DateTime.Parse("01/01/0001") : DateTime.Parse(acceso.MED_FECHA_MODIFICACION.ToString()),
                        MED_EMAIL = acceso.MED_EMAIL,
                        MED_GENERO = acceso.MED_GENERO,
                        MED_NOMBRE1 = acceso.MED_NOMBRE1,
                        MED_NOMBRE2 = acceso.MED_NOMBRE2,
                        MED_NUM_CUENTA = acceso.MED_NUM_CUENTA,
                        RET_CODIGO = acceso.RET_CODIGO,
                        RET_DESCRIPCION = acceso.RET_DESCRIPCION,
                        RET_PORCENTAJE = acceso.RET_PORCENTAJE,
                        MED_RUC = acceso.MED_RUC,
                        MED_TELEFONO_CASA = acceso.MED_TELEFONO_CASA,
                        MED_TELEFONO_CELULAR = acceso.MED_TELEFONO_CELULAR,
                        MED_TELEFONO_CONSULTORIO = acceso.MED_TELEFONO_CONSULTORIO,
                        MED_TIPO_CUENTA = acceso.MED_TIPO_CUENTA,
                        MED_VALIDEZ_AUTORIZACION = acceso.MED_VALIDEZ_AUTORIZACION,
                        MED_DIRECCION_CONSULTORIO = acceso.MED_DIRECCION_CONSULTORIO,
                        BAN_CODIGO = acceso.BAN_CODIGO,
                        ID_USUARIO = acceso.ID_USUARIO,
                        TIM_CODIGO = acceso.TIM_CODIGO,
                        ESP_NOMBRE = acceso.ESP_NOMBRE,
                        ESC_CODIGO = acceso.ESC_CODIGO,
                        TIH_CODIGO = acceso.TIH_CODIGO,
                        MED_CON_TRANSFERENCIA = acceso.MED_CON_TRANSFERENCIA == null ? false : bool.Parse(acceso.MED_CON_TRANSFERENCIA.ToString()),
                        MED_RECIBE_LLAMADA = acceso.MED_RECIBE_LLAMADA == null ? false : bool.Parse(acceso.MED_RECIBE_LLAMADA.ToString()),
                        MED_CODIGO_MEDICO = acceso.MED_CODIGO_MEDICO,
                        MED_CODIGO_LIBRO = acceso.MED_CODIGO_LIBRO,
                        MED_CODIGO_FOLIO = acceso.MED_CODIGO_FOLIO

                    });
                }
                return medicogrid;
            }
        }

        public DtoMedicos RecuperaDtoMedicoFormulario(int codMedico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var acceso = (from m in contexto.MEDICOS
                              join e in contexto.ESPECIALIDADES_MEDICAS on m.ESPECIALIDADES_MEDICAS.ESP_CODIGO equals e.ESP_CODIGO
                              join u in contexto.USUARIOS on m.USUARIOS.ID_USUARIO equals u.ID_USUARIO
                              join r in contexto.RETENCIONES_FUENTE on m.RETENCIONES_FUENTE.RET_CODIGO equals r.RET_CODIGO
                              join b in contexto.BANCOS on m.BANCOS.BAN_CODIGO equals b.BAN_CODIGO
                              join t in contexto.TIPO_MEDICO on m.TIPO_MEDICO.TIM_CODIGO equals t.TIM_CODIGO
                              join c in contexto.ESTADO_CIVIL on m.ESTADO_CIVIL.ESC_CODIGO equals c.ESC_CODIGO
                              join th in contexto.TIPO_HONORARIO on m.TIPO_HONORARIO.TIH_CODIGO equals th.TIH_CODIGO
                              where m.MED_ESTADO == true && m.MED_CODIGO == codMedico
                              select new
                              {
                                  e.ESP_CODIGO,
                                  m.MED_AUTORIZACION_SRI,
                                  m.MED_APELLIDO_MATERNO,
                                  m.MED_APELLIDO_PATERNO,
                                  m.MED_CODIGO,
                                  m.MED_CUENTA_CONTABLE,
                                  m.MED_DIRECCION,
                                  m.MED_ESTADO,
                                  m.MED_FACTURA_FINAL,
                                  m.MED_FACTURA_INICIAL,
                                  m.MED_FECHA,
                                  m.MED_FECHA_NACIMIENTO,
                                  m.MED_FECHA_MODIFICACION,
                                  m.MED_EMAIL,
                                  m.MED_GENERO,
                                  m.MED_NOMBRE1,
                                  m.MED_NOMBRE2,
                                  m.MED_NUM_CUENTA,
                                  r.RET_CODIGO,
                                  r.RET_DESCRIPCION,
                                  r.RET_PORCENTAJE,
                                  m.MED_RUC,
                                  m.MED_TELEFONO_CASA,
                                  m.MED_TELEFONO_CELULAR,
                                  m.MED_TELEFONO_CONSULTORIO,
                                  m.MED_TIPO_CUENTA,
                                  m.MED_VALIDEZ_AUTORIZACION,
                                  m.MED_DIRECCION_CONSULTORIO,
                                  b.BAN_CODIGO,
                                  u.ID_USUARIO,
                                  t.TIM_CODIGO,
                                  e.ESP_NOMBRE,
                                  c.ESC_CODIGO,
                                  th.TIH_CODIGO,
                                  m.MED_CON_TRANSFERENCIA,
                                  m.MED_RECIBE_LLAMADA
                              }).FirstOrDefault();

                DtoMedicos dtoMedico = new DtoMedicos()
                {
                    ENTITYSETNAME = "HIS3000BDEntities.MEDICOS",
                    ENTITYID = "MED_CODIGO",
                    ESP_CODIGO = acceso.ESP_CODIGO,
                    MED_AUTORIZACION_SRI = acceso.MED_AUTORIZACION_SRI,
                    MED_APELLIDO_MATERNO = acceso.MED_APELLIDO_MATERNO,
                    MED_APELLIDO_PATERNO = acceso.MED_APELLIDO_PATERNO,
                    MED_CODIGO = acceso.MED_CODIGO,
                    MED_CUENTA_CONTABLE = acceso.MED_CUENTA_CONTABLE,
                    MED_DIRECCION = acceso.MED_DIRECCION,
                    MED_ESTADO = acceso.MED_ESTADO == null ? false : bool.Parse(acceso.MED_ESTADO.ToString()),
                    MED_FACTURA_FINAL = acceso.MED_FACTURA_FINAL,
                    MED_FACTURA_INICIAL = acceso.MED_FACTURA_INICIAL,
                    MED_FECHA = acceso.MED_FECHA == null ? DateTime.Parse("01/01/2010") : DateTime.Parse(acceso.MED_FECHA.ToString()),
                    MED_FECHA_NACIMIENTO = acceso.MED_FECHA_NACIMIENTO == null ? DateTime.Parse("01/01/0001") : DateTime.Parse(acceso.MED_FECHA_NACIMIENTO.ToString()),
                    MED_FECHA_MODIFICACION = acceso.MED_FECHA_MODIFICACION == null ? DateTime.Parse("01/01/0001") : DateTime.Parse(acceso.MED_FECHA_MODIFICACION.ToString()),
                    MED_EMAIL = acceso.MED_EMAIL,
                    MED_GENERO = acceso.MED_GENERO,
                    MED_NOMBRE1 = acceso.MED_NOMBRE1,
                    MED_NOMBRE2 = acceso.MED_NOMBRE2,
                    MED_NUM_CUENTA = acceso.MED_NUM_CUENTA,
                    RET_CODIGO = acceso.RET_CODIGO,
                    RET_DESCRIPCION = acceso.RET_DESCRIPCION,
                    RET_PORCENTAJE = acceso.RET_PORCENTAJE,
                    MED_RUC = acceso.MED_RUC,
                    MED_TELEFONO_CASA = acceso.MED_TELEFONO_CASA,
                    MED_TELEFONO_CELULAR = acceso.MED_TELEFONO_CELULAR,
                    MED_TELEFONO_CONSULTORIO = acceso.MED_TELEFONO_CONSULTORIO,
                    MED_TIPO_CUENTA = acceso.MED_TIPO_CUENTA,
                    MED_VALIDEZ_AUTORIZACION = acceso.MED_VALIDEZ_AUTORIZACION,
                    MED_DIRECCION_CONSULTORIO = acceso.MED_DIRECCION_CONSULTORIO,
                    BAN_CODIGO = acceso.BAN_CODIGO,
                    ID_USUARIO = acceso.ID_USUARIO,
                    TIM_CODIGO = acceso.TIM_CODIGO,
                    ESP_NOMBRE = acceso.ESP_NOMBRE,
                    ESC_CODIGO = acceso.ESC_CODIGO,
                    TIH_CODIGO = acceso.TIH_CODIGO,
                    MED_CON_TRANSFERENCIA = acceso.MED_CON_TRANSFERENCIA == null ? false : bool.Parse(acceso.MED_CON_TRANSFERENCIA.ToString()),
                    MED_RECIBE_LLAMADA = acceso.MED_RECIBE_LLAMADA == null ? false : bool.Parse(acceso.MED_RECIBE_LLAMADA.ToString())
                };

                return dtoMedico;
            }
        }

        public void CrearMedico(MEDICOS medico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                contexto.Crear("MEDICOS", medico);
            }
        }

        public void NuevoMedico(MEDICOS medico, int esp_codigo, int tim_codigo, int tih_codigo, int ret_codigo)
        {
            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_CrearMedicos", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@med_codigo", medico.MED_CODIGO);
            command.Parameters.AddWithValue("@esp_codigo", esp_codigo);
            command.Parameters.AddWithValue("@med_nombre1", medico.MED_NOMBRE1);
            command.Parameters.AddWithValue("@med_nombre2", medico.MED_NOMBRE2);
            command.Parameters.AddWithValue("@med_apellido1", medico.MED_APELLIDO_PATERNO);
            command.Parameters.AddWithValue("@med_apellido2", medico.MED_APELLIDO_MATERNO);
            command.Parameters.AddWithValue("@fechanacimiento", medico.MED_FECHA_NACIMIENTO);
            command.Parameters.AddWithValue("@med_direccion", medico.MED_DIRECCION);
            command.Parameters.AddWithValue("@med_direccionC", medico.MED_DIRECCION_CONSULTORIO);
            command.Parameters.AddWithValue("@med_ruc", medico.MED_RUC);
            command.Parameters.AddWithValue("@med_email", medico.MED_EMAIL);
            command.Parameters.AddWithValue("@med_genero", medico.MED_GENERO);
            command.Parameters.AddWithValue("@med_cuenta_contable", medico.MED_CUENTA_CONTABLE);
            command.Parameters.AddWithValue("@telefono_casa", medico.MED_TELEFONO_CASA);
            command.Parameters.AddWithValue("@telefono_consu", medico.MED_TELEFONO_CONSULTORIO);
            command.Parameters.AddWithValue("@celular", medico.MED_TELEFONO_CELULAR);
            command.Parameters.AddWithValue("@transferencia", medico.MED_CON_TRANSFERENCIA);
            command.Parameters.AddWithValue("@tim_codigo", tim_codigo);
            command.Parameters.AddWithValue("@tih_codigo", tih_codigo);
            command.Parameters.AddWithValue("@ret_codigo", ret_codigo);
            command.Parameters.AddWithValue("@llamada", medico.MED_RECIBE_LLAMADA);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public void GrabarMedico(MEDICOS medicoModificada, MEDICOS medicoOriginal, int esp_codigo, int tim_codigo, int tih_codigo)
        {
            var medicos = new MEDICOS();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                medicos = contexto.MEDICOS.Where(a => a.MED_CODIGO == medicoModificada.MED_CODIGO)
                    .SingleOrDefault();

                if (medicos != null)
                {
                    medicos.MED_NOMBRE1 = medicoModificada.MED_NOMBRE1.Trim();
                    medicos.MED_NOMBRE2 = medicoModificada.MED_NOMBRE2.Trim();
                    medicos.MED_APELLIDO_PATERNO = medicoModificada.MED_APELLIDO_PATERNO;
                    medicos.MED_APELLIDO_MATERNO = medicoModificada.MED_APELLIDO_MATERNO;
                    medicos.MED_FECHA = medicoModificada.MED_FECHA;
                    medicos.MED_FECHA_NACIMIENTO = medicoModificada.MED_FECHA_NACIMIENTO;
                    medicos.MED_DIRECCION = medicoModificada.MED_DIRECCION.Trim();
                    medicos.MED_DIRECCION_CONSULTORIO = medicoModificada.MED_DIRECCION_CONSULTORIO;
                    medicos.MED_RUC = medicoModificada.MED_RUC;
                    medicos.MED_GENERO = medicoModificada.MED_GENERO;
                    medicos.MED_EMAIL = medicoModificada.MED_EMAIL.Trim();
                    medicos.MED_NUM_CUENTA = medicoModificada.MED_NUM_CUENTA;
                    medicos.MED_TIPO_CUENTA = medicoModificada.MED_TIPO_CUENTA;
                    medicos.MED_CUENTA_CONTABLE = medicoModificada.MED_CUENTA_CONTABLE;
                    medicos.MED_TELEFONO_CASA = medicoModificada.MED_TELEFONO_CASA;
                    medicos.MED_TELEFONO_CONSULTORIO = medicoModificada.MED_TELEFONO_CONSULTORIO;
                    medicos.MED_TELEFONO_CELULAR = medicoModificada.MED_TELEFONO_CELULAR;
                    medicos.MED_AUTORIZACION_SRI = medicoModificada.MED_AUTORIZACION_SRI;
                    medicos.MED_VALIDEZ_AUTORIZACION = medicoModificada.MED_VALIDEZ_AUTORIZACION;
                    medicos.MED_FACTURA_INICIAL = medicoModificada.MED_FACTURA_INICIAL;
                    medicos.MED_FACTURA_FINAL = medicoModificada.MED_FACTURA_FINAL;
                    medicos.MED_ESTADO = medicoModificada.MED_ESTADO;
                    medicos.MED_FECHA_MODIFICACION = medicoModificada.MED_FECHA_MODIFICACION;
                    medicos.MED_RECIBE_LLAMADA = medicoModificada.MED_RECIBE_LLAMADA;
                    medicos.MED_CODIGO_LIBRO = medicoModificada.MED_CODIGO_LIBRO;
                    medicos.MED_CODIGO_MEDICO = medicoModificada.MED_CODIGO_MEDICO;
                    medicos.MED_RECIBE_LLAMADA = medicoModificada.MED_RECIBE_LLAMADA;
                }

                contexto.SaveChanges();

                //contexto.Grabar(medicoModificada, medicoOriginal);
            }

            SqlCommand command;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("UPDATE MEDICOS SET ESP_CODIGO = @esp_codigo, TIM_CODIGO = @tim_codigo, TIH_CODIGO = @tih_codigo, ESC_CODIGO = "+medicoModificada.ESTADO_CIVILReference.EntityKey.EntityKeyValues[0].Value.ToString() +" WHERE MED_CODIGO = @med_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@esp_codigo", esp_codigo);
            command.Parameters.AddWithValue("@tim_codigo", tim_codigo);
            command.Parameters.AddWithValue("@tih_codigo", tih_codigo);
            command.Parameters.AddWithValue("@med_codigo", medicos.MED_CODIGO);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public void EliminarMedico(MEDICOS medico)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.Eliminar(medico);
            }
        }

        public List<MEDICOS> medicosLista()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return (from m in contexto.MEDICOS
                //        orderby m.MED_APELLIDO_PATERNO
                //        select m).ToList();

                return contexto.MEDICOS.Include("ESPECIALIDADES_MEDICAS").OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
            }
        }

        public MEDICOS recuperarMedico(int codigoMedico)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.MEDICOS.Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_CODIGO == codigoMedico).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }
        public MEDICOS MedicoNombreApellido(string[] medico)
        {
            string apellido1 = medico[0].ToString();
            string apellido2 = medico[1].ToString();
            string nombre1 = medico[2].ToString();
            string nombre2 = medico[3].ToString();

            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.MEDICOS.Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_APELLIDO_PATERNO == apellido1 && m.MED_APELLIDO_MATERNO == apellido2 && m.MED_NOMBRE1 == nombre1 && m.MED_NOMBRE2 == nombre2).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }
        public MEDICOS recuperarMedicoRUC(string medRuc)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.MEDICOS.Where(m => m.MED_RUC == medRuc).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }
        public MEDICOS recuperarMedicoID_Usuario(int idUsuario)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.MEDICOS.Include("ESPECIALIDADES_MEDICAS").Where(m => m.USUARIOS.ID_USUARIO == idUsuario).FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }

        public Int32 RecuperaMedicoHorario()
        {
            // Recupera el codigo de medico que esta de turno en emergencia // Giovanny Tapia // 22/02/2013
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("sp_HorarioMedicos", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            if (Dts.Rows.Count > 0)
            {
                return Convert.ToInt32(Dts.Rows[0][1]);
            }
            else
            {
                return 0;
            }

        }

        public DataTable RecuperaMedicoAtencion(string CodigoHistoria, Int32 CodigoAtencion)
        {
            // Recupera el codigo de medico que esta de turno en emergencia // Giovanny Tapia // 22/02/2013
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("sp_RecuperaMedico", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@CodigoHistoria", SqlDbType.NChar);
            Sqlcmd.Parameters["@CodigoHistoria"].Value = CodigoHistoria;

            Sqlcmd.Parameters.Add("@CodigoAtencion", SqlDbType.Int);
            Sqlcmd.Parameters["@CodigoAtencion"].Value = CodigoAtencion;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);

            return Dts;

        }

        public List<MEDICOS> listaMedicos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<MEDICOS> medicos = new List<MEDICOS>();
                medicos = contexto.MEDICOS
                    .Include("RETENCIONES_FUENTE")
                    .Where(x => x.MED_ESTADO == true)
                    .OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                return medicos;
            }
        }
        /// <summary>
        /// lista de medicos incluido el Tipo de honorario
        /// </summary>
        /// <returns>lista de medicos</returns>
        public List<MEDICOS> listaMedicosIncTipoHonorario()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<MEDICOS> medicos;
                    medicos = contexto.MEDICOS.Include("TIPO_HONORARIO").Where(m => m.MED_ESTADO == true).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                    //medicos = (from m in contexto.MEDICOS.Include("TIPO_HONORARIO")
                    //           join h in contexto.HONORARIOS_MEDICOS on m.MED_CODIGO equals h.MEDICOS.MED_CODIGO
                    //           where m.MED_ESTADO == true
                    //           select m).Distinct().ToList();
                    return medicos;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// lista de medicos incluido la especialidad medica
        /// </summary>
        /// <returns>lista de medicos</returns>
        public List<MEDICOS> listaMedicosIncEspecialidadesMedicas()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<MEDICOS> medicos = new List<MEDICOS>();
                    medicos = contexto.MEDICOS.Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_ESTADO == true).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                    return medicos;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// lista de medicos incluido el tipo medico
        /// </summary>
        /// <returns>lista de medicos</returns>
        public List<MEDICOS> listaMedicosIncTipoMedico()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<MEDICOS> medicos = new List<MEDICOS>();
                    medicos = contexto.MEDICOS.Include("TIPO_MEDICO").Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_ESTADO == true).ToList();
                    return medicos;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        /// <summary>
        /// lista de medicos incluido el tipo medico
        /// </summary>
        /// <returns>lista de medicos</returns>
        public List<MEDICOS> listaMedicosIncTipoMedico(Int16 codTipoMedico)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<MEDICOS> medicos = new List<MEDICOS>();
                    medicos = contexto.MEDICOS.Include("TIPO_MEDICO").Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_ESTADO == true && m.TIPO_MEDICO.TIM_CODIGO == codTipoMedico).ToList();

                    return medicos;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        ///<summary >
        ///Recupera el listado de medicos por un tipo especifico
        ///</summary>
        public List<MEDICOS> listaMedicos(int codigo, string tipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                switch (tipo)
                {
                    case "tipo_honorario":
                        return contexto.MEDICOS.Where(m => m.TIPO_HONORARIO.TIH_CODIGO == codigo).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                    case "tipo_especialidad":
                        return contexto.MEDICOS.Where(m => m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == codigo).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                    case "tipo_medico":
                        return contexto.MEDICOS.Where(m => m.TIPO_MEDICO.TIM_CODIGO == codigo).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                    case "tipo_llamada":
                        if (codigo == 0)
                        {
                            return contexto.MEDICOS.Where(m => m.MED_RECIBE_LLAMADA == false).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                        }
                        else
                        {
                            return contexto.MEDICOS.Where(m => m.MED_RECIBE_LLAMADA == true).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                        }
                    default:
                        return contexto.MEDICOS.ToList();
                }
            }
        }
        ///<summary >
        ///Recupera el listado de medicos por un tipo especifico
        ///</summary>
        public List<MEDICOS> listaMedicosConHonorarios(int codigo, string tipo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                switch (tipo)
                {
                    case "tipo_honorario":
                        return (List<MEDICOS>)(from m in contexto.MEDICOS
                                               join a in contexto.ATENCIONES on m.MED_CODIGO equals a.MEDICOS.MED_CODIGO
                                               where m.TIPO_HONORARIO.TIH_CODIGO == codigo
                                               orderby m.MED_APELLIDO_PATERNO
                                               select m).Distinct().ToList();
                    //return contexto.MEDICOS.Where(m => m.TIPO_HONORARIO.TIH_CODIGO == codigo).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                    case "tipo_especialidad":
                        return (List<MEDICOS>)(from m in contexto.MEDICOS
                                               join a in contexto.ATENCIONES on m.MED_CODIGO equals a.MEDICOS.MED_CODIGO
                                               where m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == codigo
                                               orderby m.MED_APELLIDO_PATERNO
                                               select m).Distinct().ToList();
                    //return contexto.MEDICOS.Where(m => m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == codigo).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                    case "tipo_medico":
                        return (List<MEDICOS>)(from m in contexto.MEDICOS
                                               join a in contexto.ATENCIONES on m.MED_CODIGO equals a.MEDICOS.MED_CODIGO
                                               where m.TIPO_MEDICO.TIM_CODIGO == codigo
                                               orderby m.MED_APELLIDO_PATERNO
                                               select m).Distinct().ToList();
                    //return contexto.MEDICOS.Where(m => m.TIPO_MEDICO.TIM_CODIGO == codigo).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                    case "tipo_llamada":
                        if (codigo == 0)
                        {
                            return (List<MEDICOS>)(from m in contexto.MEDICOS
                                                   join a in contexto.ATENCIONES on m.MED_CODIGO equals a.MEDICOS.MED_CODIGO
                                                   where m.MED_RECIBE_LLAMADA == false
                                                   orderby m.MED_APELLIDO_PATERNO
                                                   select m).Distinct().ToList();
                            //return contexto.MEDICOS.Where(m => m.MED_RECIBE_LLAMADA == false).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                        }
                        else
                        {
                            return (List<MEDICOS>)(from m in contexto.MEDICOS
                                                   join a in contexto.ATENCIONES on m.MED_CODIGO equals a.MEDICOS.MED_CODIGO
                                                   where m.MED_RECIBE_LLAMADA == true
                                                   orderby m.MED_APELLIDO_PATERNO
                                                   select m).Distinct().ToList();
                            //return contexto.MEDICOS.Where(m => m.MED_RECIBE_LLAMADA == true).OrderBy(m => m.MED_APELLIDO_PATERNO).ToList();
                        }
                    default:
                        return (List<MEDICOS>)contexto.MEDICOS.ToList();
                }

            }
        }

        //.Recupera la informaciòn del medico por id
        public MEDICOS RecuperaMedicoId(int codigo)
        {
            MEDICOS medico;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                medico = contexto.MEDICOS.Include("RETENCIONES_FUENTE").Include("TIPO_HONORARIO").FirstOrDefault(m => m.MED_CODIGO == codigo);
                /*medico = (from m in contexto.MEDICOS
                          join r in contexto.RETENCIONES_FUENTE on m.RETENCIONES_FUENTE.RET_CODIGO equals r.RET_CODIGO
                          join t in contexto.TIPO_HONORARIO on m.TIPO_HONORARIO.TIH_CODIGO equals t.TIH_CODIGO
                          where m.MED_CODIGO == codigo
                          select m).FirstOrDefault();*/
                return medico;
            }
        }

        public MEDICOS MedicoID(int codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from m in contexto.MEDICOS
                        where m.MED_CODIGO == codigo
                        select m).FirstOrDefault();
            }
        }
        public DataTable MedicoIDValida(int MedCodigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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

            Sqlcmd = new SqlCommand("SELECT TIM_CODIGO FROM MEDICOS WHERE MED_CODIGO = " + MedCodigo, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        public DataTable RecuperaEspecialidadMed(int MedCodigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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

            Sqlcmd = new SqlCommand("select e.ESP_NOMBRE from MEDICOS m \n" +
            "inner join ESPECIALIDADES_MEDICAS e on m.ESP_CODIGO = e.ESP_CODIGO\n" +
            "where m.MED_CODIGO = " + MedCodigo, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }
        //Ingresa la informacion de Medicos a la tabla auditamedico
        public MEDICOS AuditaMedico(int IdMedico, int IdUsuario, DateTime fecha)
        {
            // PABLO ROCHA / 28/05/2013

            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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

            Sqlcmd = new SqlCommand("SP_AuditaMedico", Sqlcon);
            Sqlcmd.CommandType = CommandType.StoredProcedure;

            Sqlcmd.Parameters.Add("@IdMedico", SqlDbType.Int);
            Sqlcmd.Parameters["@IdMedico"].Value = IdMedico;

            Sqlcmd.Parameters.Add("@IdUsuario", SqlDbType.Int);
            Sqlcmd.Parameters["@IdUsuario"].Value = IdUsuario;

            Sqlcmd.Parameters.Add("@Fecha", SqlDbType.DateTime);
            Sqlcmd.Parameters["@Fecha"].Value = fecha;

            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180; //ESTABLECE EL TIEMPO MAXIMO DE ESPERA A UNA CONSULTA EN EL SERVIDOR EN SEGUNDOS/ GIOVANNY TAPIA /03/07/2012
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            MEDICOS medi = null;
            return medi;
        }

        //.Recupera la informaciòn del medico por codigo de usuario
        public MEDICOS RecuperaMedicoIdUsuario(int codigoUsuario)
        {
            MEDICOS medico;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                medico = contexto.MEDICOS.FirstOrDefault(m => m.USUARIOS.ID_USUARIO == codigoUsuario);
                return medico;
            }
        }
        ///<summary>
        ///.Recupera el Tipo de Honorario de los medicos
        ///</summary> 
        public List<TIPO_HONORARIO> RecuperarTipoHonorario()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.TIPO_HONORARIO.Where(t => t.TIH_ESTADO == true).OrderBy(t => t.TIH_NOMBRE).ToList();
            }
        }
        ///<summary>
        ///. Recupera el Tipo de Especilidad de los medicos
        ///</summary> 
        public List<ESPECIALIDADES_MEDICAS> RecuperarTipoEspecialidad()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.ESPECIALIDADES_MEDICAS.Where(e => e.ESP_ESTADO == true).OrderBy(e => e.ESP_NOMBRE).ToList();
            }
        }
        ///<sumary>
        ///. Recupera el tipo de medico
        ///</sumary>
        public List<TIPO_MEDICO> RecuperarTipoMedico()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return contexto.TIPO_MEDICO.Where(t => t.TIM_ESTADO == true).OrderBy(t => t.TIM_NOMBRE).ToList();
            }
        }

        public List<MEDICOS> listaMedicosTratantes()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from m in contexto.MEDICOS
                            //join a in contexto.ATENCIONES on m.MED_CODIGO equals a.MEDICOS.MED_CODIGO 
                        where m.MED_ESTADO == true
                        select m).ToList();

                //return contexto.MEDICOS.OrderBy(m=>m.MED_APELLIDO_PATERNO).ToList();
            }
        }

        /// <summary>
        /// Metodo que devuelve el listado de medicos por especialidad
        /// </summary>
        /// <param name="codigoEspecialidad">Codigo de la especialidad</param>
        /// <returns>Lista de objetos MEDICOS</returns>
        public List<MEDICOS> listaMedicosPorEspecialidad(Int16 codigoEspecialidad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<MEDICOS> medicos = (from m in contexto.MEDICOS
                                         where m.MED_ESTADO == true && m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == codigoEspecialidad
                                         select m).ToList();
                return medicos;
            }
        }

        /// <summary>
        /// Metodo que devuelve el listado de medicos y sus correo 
        /// </summary>
        /// <param name="codigoEspecialidad">Codigo de la especialidad</param>
        /// <returns>Lista de objetos MEDICOS</returns>
        public List<MEDICOS> listaCorreosMedicosPorEspecialidad(string codigoEspecialidad)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<MEDICOS> medicos;
                if (codigoEspecialidad != null)
                {
                    Int16 espCodigo = Convert.ToInt16(codigoEspecialidad);
                    medicos = (from m in contexto.MEDICOS
                               where m.MED_ESTADO == true && !m.MED_EMAIL.Equals("") && m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == espCodigo
                               select m).ToList();
                }
                else
                {
                    medicos = (from m in contexto.MEDICOS
                               where m.MED_ESTADO == true && !m.MED_EMAIL.Equals("")
                               select m).ToList();
                }
                return medicos;

            }
        }

        public MEDICOS medicoPorAtencion(int codAtencion)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from m in contexto.MEDICOS
                        join a in contexto.ATENCIONES on m.MED_CODIGO equals a.MEDICOS.MED_CODIGO
                        where a.ATE_CODIGO == codAtencion
                        select m).FirstOrDefault();
            }
        }


        public string getMedicoVendedor(string codMed)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts;
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

            Sqlcmd = new SqlCommand("SELECT count(*) as registros FROM dbo.medico_vendedor where dbo.medico_vendedor.cod_medico = " + codMed, Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Dts = new DataTable();
            Sqldap.Fill(Dts);
            int rows = Convert.ToInt32(Dts.Rows[0][0].ToString());

            string medi = "0";
            if (rows > 0) //si existe
            {
                Sqlcmd = new SqlCommand("SELECT        concat (dbo.vendedores.codigo , '_' , dbo.vendedores.nombre) as medico \n" +
                        "FROM            dbo.medico_vendedor INNER JOIN \n" +
                         "dbo.vendedores ON dbo.medico_vendedor.cod_vendedor = dbo.vendedores.codigo \n" +
                         "WHERE(dbo.medico_vendedor.cod_medico = " + codMed + ")", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Dts = new DataTable();
                Sqldap.Fill(Dts);
                medi = Dts.Rows[0][0].ToString();
            }

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

            return medi;
        }

        public void saveMedicoVendedor(string codMed, string codVendedor, bool nuevo)
        {
            string cadena_sql;
            if (codVendedor.Trim() != string.Empty)
            {
                cadena_sql = "delete from [dbo].[medico_vendedor] where cod_medico =" + codMed + " \n" +
                    "INSERT INTO [dbo].[medico_vendedor] ([cod_medico],[cod_vendedor]) VALUES " + "(" + codMed + "," + codVendedor + ")";
            }
            else
            {
                cadena_sql = "delete from [dbo].[medico_vendedor] where cod_medico =" + codMed;
            }
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlConnection Sqlcon = obj.ConectarBd();
            Sqlcon.Open();
            SqlCommand Sqlcmd = new SqlCommand(cadena_sql, Sqlcon);
            try
            {
                Sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DataTable getMedicosGrid()
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
            SqlDataAdapter Sqldap;
            DataTable Dts = new DataTable();
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
            Sqlcmd = new SqlCommand("SELECT dbo.MEDICOS.MED_CODIGO, concat(dbo.MEDICOS.MED_APELLIDO_PATERNO,' ', dbo.MEDICOS.MED_APELLIDO_MATERNO, ' ',dbo.MEDICOS.MED_NOMBRE1, ' ',dbo.MEDICOS.MED_NOMBRE2) as NOMBRE, \n" +
                                 "dbo.ESPECIALIDADES_MEDICAS.ESP_NOMBRE AS ESPECIALIDAD, dbo.MEDICOS.MED_DIRECCION AS DIRECCION, dbo.MEDICOS.MED_DIRECCION_CONSULTORIO AS OBSERVACION, dbo.MEDICOS.MED_RUC AS RUC, \n" +
                                  "dbo.MEDICOS.MED_TELEFONO_CASA AS TLF_CASA, dbo.MEDICOS.MED_TELEFONO_CONSULTORIO AS TLF_CONSULTORIO, dbo.MEDICOS.MED_TELEFONO_CELULAR AS TLF_CELULAR, dbo.MEDICOS.MED_ESTADO AS ACTIVO, dbo.vendedores.nombre AS VENDEDOR, dbo.vendedores.nro_identificacion AS ID_VENDEDOR, dbo.MEDICOS.MED_EMAIL AS CORREO\n" +
                            "FROM dbo.vendedores INNER JOIN dbo.medico_vendedor ON dbo.vendedores.codigo = dbo.medico_vendedor.cod_vendedor RIGHT OUTER JOIN \n" +
                                 "dbo.MEDICOS INNER JOIN dbo.ESPECIALIDADES_MEDICAS ON dbo.MEDICOS.ESP_CODIGO = dbo.ESPECIALIDADES_MEDICAS.ESP_CODIGO ON dbo.medico_vendedor.cod_medico = dbo.MEDICOS.MED_CODIGO", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Dts;
        }

        #region Implementacion Controles
        public IEnumerable<object> DoSearch(string searchTerm, int maxResults, object extraInfo)
        {

            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var l2eIsNotFun =
                    contexto.MEDICOS.Where(
                        p => p.MED_APELLIDO_PATERNO.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase)).Cast
                        <object>();
                return l2eIsNotFun;
            }
        }
        #endregion

        //EDGAR RAMOS 20201127 SE AGREGA EL CIE10 PARA EL PROTOCOLO
        public DataTable CargarCie10()
        {
            SqlConnection conexion;
            SqlCommand command;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            command = new SqlCommand("SELECT DISTINCT CIE_DESCRIPCION AS PROCEDIMIENTO FROM CIE10 ORDER BY CIE_DESCRIPCION ASC", conexion);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Tabla;
        }


        public DataTable CargarPersonal()
        {
            SqlConnection conexion;
            SqlCommand command;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            command = new SqlCommand("select U.IDENTIFICACION,  U.APELLIDOS + ' ' + U.NOMBRES AS NOMBRE, D.DEP_NOMBRE  from USUARIOS u inner join DEPARTAMENTOS d on u.DEP_CODIGO = d.DEP_CODIGO WHERE D.DEP_CODIGO = 6 OR D.DEP_CODIGO = 9 OR D.DEP_CODIGO = 13 AND U.ESTADO = 1 ORDER BY 2 ASC", conexion);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Tabla;
        }

        public DataTable CargarPacienteEmergencia()
        {
            SqlConnection conexion;
            SqlCommand command;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            command = new SqlCommand("SELECT P.PAC_HISTORIA_CLINICA AS HC, A.ATE_NUMERO_ATENCION AS Atencion, P.PAC_IDENTIFICACION AS Identificacion,  P.PAC_APELLIDO_PATERNO + ' ' + P.PAC_APELLIDO_MATERNO + ' ' + P.PAC_NOMBRE1 + ' ' + P.PAC_NOMBRE2 AS Paciente,  A.ATE_FECHA_INGRESO AS 'F. Ingreso', A.ATE_CODIGO, A.ATE_FECHA_ALTA, M.MED_EMAIL, M.MED_RUC,  M.MED_APELLIDO_PATERNO + ' ' + M.MED_APELLIDO_MATERNO + ' ' + M.MED_NOMBRE1 + ' ' + M.MED_NOMBRE2 AS Medico  FROM PACIENTES P INNER JOIN ATENCIONES A ON P.PAC_CODIGO = A.PAC_CODIGO INNER JOIN MEDICOS M ON A.MED_CODIGO = M.MED_CODIGO  INNER JOIN USUARIOS U ON M.ID_USUARIO = U.ID_USUARIO  WHERE A.ESC_CODIGO = 1 AND A.TIP_CODIGO = 1 ORDER BY A.ATE_FECHA_INGRESO DESC", conexion);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Tabla;
        }
        public DataTable CargarTarifario()
        {
            SqlConnection conexion;
            SqlCommand command;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            command = new SqlCommand("SELECT  DISTINCT LTRIM(TAD_DESCRIPCION) as Tarifario, TAD_CODIGO AS Codigo FROM tarifario_detalle ORDER BY LTRIM(TAD_DESCRIPCION) ASC", conexion);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Tabla;
        }
        public DataTable RecuperarNuevosCampos(int med_codigo)
        {
            SqlConnection conexion;
            SqlCommand command;
            SqlDataReader reader;
            DataTable Tabla = new DataTable();
            BaseContextoDatos obj = new BaseContextoDatos();

            conexion = obj.ConectarBd();
            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            command = new SqlCommand("SELECT * FROM MEDICOS WHERE MED_CODIGO = @med_codigo", conexion);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Tabla;
        }
        public void ActualizarCampos(int med_codigo, bool aporte, int ret_codigo)
        {
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("UPDATE MEDICOS SET MED_CON_TRANSFERENCIA = @aporte, RET_CODIGO = @ret_codigo WHERE MED_CODIGO = @med_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@aporte", aporte);
            command.Parameters.AddWithValue("@med_codigo", med_codigo);
            command.Parameters.AddWithValue("@ret_codigo", ret_codigo);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public void ActualizarRucMedico(MEDICOS med, string idUsuario)
        {
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();

            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("UPDATE MEDICOS SET ID_USUARIO = @idUsuario WHERE MED_CODIGO = @med_codigo", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@idUsuario", idUsuario);
            command.Parameters.AddWithValue("@med_codigo", med.MED_CODIGO);
            command.CommandTimeout = 180;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
            connection.Close();
        }
        public string CuentaContableSic(string forpag)
        {
            string cuenta = "";
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("SELECT codcue FROM Sic3000..Forma_Pago where forpag = @forpag", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@forpag", forpag);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                cuenta = reader["codcue"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return cuenta;
        }
        public DataTable VerMedicos()
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            DataTable Tabla = new DataTable();
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_Medico", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 180;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            return Tabla;
        }
        public List<MEDICOS> MedicosCitaMedica(int esp_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<MEDICOS> medicos = new List<MEDICOS>();
                if (esp_codigo == 999)
                    medicos = db.MEDICOS.Include("TIPO_MEDICO").Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_ESTADO == true && m.TIPO_MEDICO.TIM_CODIGO != 7).ToList();
                else
                    medicos = db.MEDICOS.Include("TIPO_MEDICO").Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_ESTADO == true && m.TIPO_MEDICO.TIM_CODIGO != 7 && m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == esp_codigo).ToList();
                return medicos;
            }
        }

        public List<ESPECIALIDADES_MEDICAS> EspecialidadesCita()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<ESPECIALIDADES_MEDICAS> especialidad = (from e in db.ESPECIALIDADES_MEDICAS
                                                             where e.ESP_ESTADO == true
                                                             select e).ToList();

                ESPECIALIDADES_MEDICAS x = new ESPECIALIDADES_MEDICAS();
                x.ESP_CODIGO = 999;
                x.ESP_NOMBRE = "TODOS";
                x.ESP_DESCRIPCION = "TODOS";
                x.ESP_ESTADO = true;
                x.ESP_PADRE = 0;

                especialidad.Add(x);
                return especialidad;
            }
        }
        public bool existeMedico(string identificacion)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                MEDICOS x = new MEDICOS();

                x = db.MEDICOS.FirstOrDefault(a => a.MED_RUC == identificacion);

                if (x != null)
                    return true;
                else
                    return false;
            }
        }
        public string cuentaContable()
        {
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;
            BaseContextoDatos obj = new BaseContextoDatos();
            string cuenta = "";
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("sp_MedicoCuentaContable", connection);
            command.CommandType = CommandType.StoredProcedure;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                cuenta = reader["PAD_VALOR"].ToString();
            }
            reader.Close();
            connection.Close();
            return cuenta;
        }
        public string MED_CODIGO_MEDICO_CG(string med_ruc)
        {
            string cuenta = "";
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select codigo_c from Cg3000..Cgcodcon where campo4 = @med_ruc", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@med_ruc", med_ruc);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                cuenta = reader["codigo_c"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return cuenta;
        }
        public MEDICOS Medicos()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.MEDICOS.FirstOrDefault();
                }
            }
            catch (Exception err) { throw err; }
        }
        public List<MEDICOS> listMedicos()
        {
            List<MEDICOS> med = new List<MEDICOS>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return med = (from m in db.MEDICOS select m).ToList();
            }
        }
        public DtoMedicoCuentaContable MEDICO_CG(string codigo_c)
        {
            DataTable Tabla = new DataTable();
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select campo1,campo2,campo3,campo4,codigo_c from Cg3000..Cgcodcon where codigo_c = @codigo_c ", connection);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@codigo_c", codigo_c);
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            DtoMedicoCuentaContable x = new DtoMedicoCuentaContable();
            try
            {
                foreach (DataRow row in Tabla.Rows)
                {
                    string nombre = row["campo1"].ToString();
                    string[] array = nombre.Split(' ');
                    x.MED_CODIGO_C = row["codigo_c"].ToString();
                    x.MED_NOMBRE1 = array[2];
                    x.MED_NOMBRE2 = array[3];
                    x.MED_APELLIDOS_PATERNO = array[0];
                    x.MED_APELLIDOS_MATERNO = array[1];
                    x.MED_DIRECCION = row["campo2"].ToString();
                    x.MED_TELEFONO = row["campo3"].ToString();
                    x.MED_RUC = row["campo4"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return x;
        }
        public List<DtoMedicoCuentaContable> List_MEDICO_CG()
        {
            List<DtoMedicoCuentaContable> medicoCuentacontable = new List<DtoMedicoCuentaContable>();
            DataTable Tabla = new DataTable();
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select campo1,campo2,codigo_c from Cg3000..Cgcodcon where tipcod = 5 ", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            Tabla.Load(reader);
            reader.Close();
            connection.Close();
            try
            {
                foreach (DataRow row in Tabla.Rows)
                {
                    DtoMedicoCuentaContable x = new DtoMedicoCuentaContable();
                    string nombre = row["campo1"].ToString();
                    string[] array = nombre.Split(' ');
                    x.MED_CODIGO_C = row["codigo_c"].ToString();
                    try
                    {
                        x.MED_NOMBRE1 = array[2];
                    }
                    catch (Exception ex)
                    {
                        x.MED_NOMBRE1 = "";
                        //throw;
                    }
                    try
                    {
                        x.MED_NOMBRE2 = array[3];
                    }
                    catch (Exception ex)
                    {
                        x.MED_NOMBRE2 = "";
                        //throw;
                    }
                    x.MED_APELLIDOS_PATERNO = array[0];
                    try
                    {
                        x.MED_APELLIDOS_MATERNO = array[1];
                    }
                    catch (Exception ex)
                    {
                        x.MED_APELLIDOS_MATERNO = "";
                        //throw;
                    }
                    x.MED_DIRECCION = row["campo2"].ToString();
                    medicoCuentacontable.Add(x);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return medicoCuentacontable;
        }
        public string TipoMoviemientoSic(string codcue)
        {
            string cuenta = "";
            SqlConnection connection;
            SqlCommand command;
            BaseContextoDatos obj = new BaseContextoDatos();
            SqlDataReader reader;
            connection = obj.ConectarBd();
            connection.Open();

            command = new SqlCommand("select codcta_pc from Cg3000..Cgplacue where codpre_pc = '" + codcue + "'", connection);
            command.CommandType = CommandType.Text;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                cuenta = reader["codcta_pc"].ToString();
            }
            reader.Close();
            command.Parameters.Clear();
            connection.Close();
            return cuenta;
        }
        public List<MEDICOS> listaMedicosIncTipoMedicoXEsp(Int64 esp_codigo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    List<MEDICOS> medicos = new List<MEDICOS>();
                    medicos = contexto.MEDICOS.Include("TIPO_MEDICO").Include("ESPECIALIDADES_MEDICAS").Where(m => m.MED_ESTADO == true && m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == esp_codigo).ToList();
                    return medicos;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
