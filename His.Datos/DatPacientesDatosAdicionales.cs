using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace His.Datos
{
    public class DatPacientesDatosAdicionales
    {
        public void PDA2_save(DtoPacienteDatosAdicionales2 pda)
        {
            int x = 0;
            if (pda.FALLECIDO)
                x = 1;
            string cadena_sql =
                "IF not EXISTS(SELECT PAC_CODIGO FROM PACIENTES_DATOS_ADICIONALES2 WHERE PAC_CODIGO = '" + pda.COD_PACIENTE + "') "
                    + "INSERT INTO PACIENTES_DATOS_ADICIONALES2(PACIENTE_REFERENCIA_TELEFONO2, FALLECIDO, FECHA_FALLECIDO, PAC_CODIGO, FOLIO, EMAIL_ACOMPAÑANTE,NACIONA_ETNICA,PUEBLO,PAC_ESTADO_NIVEDU,PAC_TIP_EMPRESA,PAC_SEG_SALUD,PAC_TIP_BONO) "
                                                + "VALUES('" + pda.REF_TELEFONO_2 + "', " + x + ", '" + pda.FEC_FALLECIDO + "', '" + pda.COD_PACIENTE + "', '" + pda.FOLIO + "', '" + pda.email + "','" + pda.nac_etnica + "','" + pda.pueblo + "','" + pda.neducacion + "','" + pda.tempresa + "','" + pda.ssalud + "','" + pda.tbono + "' ) "
                + "ELSE "
                    + "UPDATE PACIENTES_DATOS_ADICIONALES2 SET PACIENTE_REFERENCIA_TELEFONO2 = '" + pda.REF_TELEFONO_2 + "', "
                    + "FALLECIDO = " + x + ", "
                    + "FECHA_FALLECIDO = '" + pda.FEC_FALLECIDO + "', "
                    + "FOLIO = '" + pda.FOLIO + "', "
                    + "EMAIL_ACOMPAÑANTE = '" + pda.email + "', "
                    + "MOTIVO = '" + pda.motivo + "', "
                    + "DIAGNOSTICO = '" + pda.diagnostico + "', "
                    + "ID_PERSONA_TRAMITA = '" + pda.id_persona_tramita + "', "
                    + "NOMBRE_APELLIDO_TRAMITA = '" + pda.nombre_apellido_tramita + "',"
                    + "TELF_CONVENCIONAL = '" + pda.telf_convencional + "', "
                    + "FECHA_ENTREGA_DOCUMENTO = '" + DateTime.Now + "', "
                    + "NACIONA_ETNICA = '" + pda.nac_etnica + "', "
                    + "PUEBLO = '" + pda.pueblo + "', "
                    + "PAC_ESTADO_NIVEDU = '" + pda.neducacion + "', "
                    + "PAC_TIP_EMPRESA = '" + pda.tempresa + "', "
                    + "PAC_SEG_SALUD = '" + pda.ssalud + "', "
                    + "PAC_TIP_BONO = '" + pda.tbono + "', "
                    + "ID_USUARIO = " + pda.id_usuario + " "
                    + "WHERE PAC_CODIGO = '" + pda.COD_PACIENTE + "'"
                ;

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

        public DtoPacienteDatosAdicionales2 PDA2_find(Int64 codigoPaciente)
        {
            DtoPacienteDatosAdicionales2 pda = null;
            int r;
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

            Sqlcmd = new SqlCommand("SELECT  count(DAP2_CODIGO)  FROM [His3000].[dbo].[PACIENTES_DATOS_ADICIONALES2] where PAC_CODIGO=" + codigoPaciente + " ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqldap = new SqlDataAdapter();
            Sqlcmd.CommandTimeout = 180;
            Sqldap.SelectCommand = Sqlcmd;
            Sqldap.Fill(Dts);
            r = Convert.ToInt16(Dts.Rows[0][0]);

            if (r == 1)
            {
                DataTable Dts2 = new DataTable();
                Sqlcmd = new SqlCommand("SELECT  *  FROM [His3000].[dbo].[PACIENTES_DATOS_ADICIONALES2] where PAC_CODIGO=" + codigoPaciente + " ", Sqlcon);
                Sqlcmd.CommandType = CommandType.Text;
                Sqldap = new SqlDataAdapter();
                Sqlcmd.CommandTimeout = 180;
                Sqldap.SelectCommand = Sqlcmd;
                Sqldap.Fill(Dts2);

                pda = new DtoPacienteDatosAdicionales2();

                if ((Dts2.Rows[0]["FALLECIDO"].ToString().Trim()) == "True" || (Dts2.Rows[0]["FALLECIDO"].ToString().Trim()) == "1")
                    pda.FALLECIDO = true;
                else
                    pda.FALLECIDO = false;
                pda.FEC_FALLECIDO = Convert.ToString(Dts2.Rows[0]["FECHA_FALLECIDO"]);
                pda.FOLIO = Convert.ToString(Dts2.Rows[0]["FOLIO"]);
                pda.REF_TELEFONO_2 = Convert.ToString(Dts2.Rows[0]["PACIENTE_REFERENCIA_TELEFONO2"]);
                pda.email = Dts2.Rows[0]["EMAIL_ACOMPAÑANTE"].ToString();
                pda.motivo = Dts2.Rows[0]["MOTIVO"].ToString();
                pda.diagnostico = Dts2.Rows[0]["DIAGNOSTICO"].ToString();
                pda.id_persona_tramita = Dts2.Rows[0]["ID_PERSONA_TRAMITA"].ToString();
                pda.nombre_apellido_tramita = Dts2.Rows[0]["NOMBRE_APELLIDO_TRAMITA"].ToString();
                pda.telf_convencional = Dts2.Rows[0]["TELF_CONVENCIONAL"].ToString();
                if (Dts2.Rows[0]["FECHA_ENTREGA_DOCUMENTO"].ToString() != "")
                {
                    pda.fecha_entrega_documento = Convert.ToDateTime(Dts2.Rows[0]["FECHA_ENTREGA_DOCUMENTO"].ToString());
                }
                pda.COD_PACIENTE = codigoPaciente;
                pda.nac_etnica = Dts2.Rows[0]["NACIONA_ETNICA"].ToString();
                pda.pueblo = Dts2.Rows[0]["PUEBLO"].ToString();
                pda.neducacion = Dts2.Rows[0]["PAC_ESTADO_NIVEDU"].ToString();
                pda.tempresa = Dts2.Rows[0]["PAC_TIP_EMPRESA"].ToString();
                pda.ssalud = Dts2.Rows[0]["PAC_SEG_SALUD"].ToString();
                pda.tbono = Dts2.Rows[0]["PAC_TIP_BONO"].ToString();
            }

            try
            {
                Sqlcon.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pda;



        }
        public void CrearPacienteDatosAdicionales(PACIENTES_DATOS_ADICIONALES datosPaciente, Int64 codigoPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                List<PACIENTES_DATOS_ADICIONALES> datos = new List<PACIENTES_DATOS_ADICIONALES>();
                datos = (from d in contexto.PACIENTES_DATOS_ADICIONALES
                         join p in contexto.PACIENTES on d.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                         where d.PACIENTES.PAC_CODIGO == codigoPaciente
                         select d).ToList();

                foreach (PACIENTES_DATOS_ADICIONALES dat in datos)
                {
                    dat.DAP_ESTADO = false;
                }

                contexto.AddToPACIENTES_DATOS_ADICIONALES(datosPaciente);
                contexto.SaveChanges();
            }
        }

        public int ultimoCodigoDatos()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from d in contexto.PACIENTES_DATOS_ADICIONALES
                             select d.DAP_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }

        }

        public void EditarPacienteDatosAdicionales(PACIENTES_DATOS_ADICIONALES datosPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PACIENTES_DATOS_ADICIONALES datos = contexto.PACIENTES_DATOS_ADICIONALES.FirstOrDefault(d => d.DAP_CODIGO == datosPaciente.DAP_CODIGO);
                datos.DAP_DIRECCION_DOMICILIO = datosPaciente.DAP_DIRECCION_DOMICILIO;
                datos.COD_PAIS = datosPaciente.COD_PAIS;
                datos.COD_PROVINCIA = datosPaciente.COD_PROVINCIA;
                datos.COD_CANTON = datosPaciente.COD_CANTON;
                datos.COD_PARROQUIA = datosPaciente.COD_PARROQUIA;
                datos.COD_SECTOR = datosPaciente.COD_SECTOR;
                datos.DAP_EMP_NOMBRE = datosPaciente.DAP_EMP_NOMBRE;
                datos.DAP_EMP_DIRECCION = datosPaciente.DAP_EMP_DIRECCION;
                datos.DAP_EMP_CIUDAD = datosPaciente.DAP_EMP_CIUDAD;
                datos.DAP_EMP_TELEFONO = datosPaciente.DAP_EMP_TELEFONO;
                datos.DAP_INSTRUCCION = datosPaciente.DAP_INSTRUCCION;
                datos.DAP_OCUPACION = datosPaciente.DAP_OCUPACION;
                datos.DAP_TELEFONO1 = datosPaciente.DAP_TELEFONO1;
                datos.DAP_TELEFONO2 = datosPaciente.DAP_TELEFONO2;
                //datos.DAP_ZONA_URBANA = datosPaciente.DAP_ZONA_URBANA;
                datos.ESTADO_CIVILReference.EntityKey = datosPaciente.ESTADO_CIVILReference.EntityKey;
                datos.TIPO_CIUDADANOReference.EntityKey = datosPaciente.TIPO_CIUDADANOReference.EntityKey;
                if (datosPaciente.EMP_CODIGO != null)
                    datos.EMP_CODIGO = datosPaciente.EMP_CODIGO;
                contexto.SaveChanges();
            }
        }
        //Edgar 20210228 Reversiond de estado de defuncion solo por departamento autorizado
        public void Reversion(int pac_codigo)
        {
            SqlConnection Sqlcon;
            SqlCommand Sqlcmd;
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

            Sqlcmd = new SqlCommand("UPDATE PACIENTES_DATOS_ADICIONALES2 SET FALLECIDO = 0, FECHA_FALLECIDO = NULL, FOLIO = NULL, MOTIVO = NULL, DIAGNOSTICO = NULL, ID_PERSONA_TRAMITA = NULL, NOMBRE_APELLIDO_TRAMITA = NULL, TELF_CONVENCIONAL = NULL, FECHA_ENTREGA_DOCUMENTO = NULL, ID_USUARIO_REVIERTE = " + Sesion.codUsuario + " where PAC_CODIGO=" + pac_codigo + " ", Sqlcon);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 180;
            Sqlcmd.ExecuteNonQuery();
            Sqlcon.Close();
        }
        public void EditarPacienteDatosAdicionalesHonorarios(PACIENTES_DATOS_ADICIONALES datosPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PACIENTES_DATOS_ADICIONALES datos = contexto.PACIENTES_DATOS_ADICIONALES.FirstOrDefault(d => d.DAP_CODIGO == datosPaciente.DAP_CODIGO);
                datos.DAP_DIRECCION_DOMICILIO = datosPaciente.DAP_DIRECCION_DOMICILIO;
                datos.DAP_TELEFONO1 = datosPaciente.DAP_TELEFONO1;
                contexto.SaveChanges();
            }
        }

        public PACIENTES_DATOS_ADICIONALES RecuperarDatosPacienteID(int codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from d in contexto.PACIENTES_DATOS_ADICIONALES
                        where d.PACIENTES.PAC_CODIGO == codigo
                        orderby d.DAP_CODIGO descending
                        select d).FirstOrDefault();
            }
        }

        public PACIENTES_DATOS_ADICIONALES RecuperarDatosAdicionalesPacienteID(int ateCodigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PACIENTES_DATOS_ADICIONALES pacienteDatos = (from d in contexto.PACIENTES_DATOS_ADICIONALES
                                                             join p in contexto.PACIENTES on d.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                                                             join ate in contexto.ATENCIONES on p.PAC_CODIGO equals ate.PACIENTES.PAC_CODIGO
                                                             where ate.ATE_CODIGO == ateCodigo
                                                             select d).OrderByDescending(x => x.DAP_CODIGO).FirstOrDefault();
                return pacienteDatos;
            }
        }

        public PACIENTES_DATOS_ADICIONALES RecuperarDatosAdicionalesPaciente(Int64 keyPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from d in contexto.PACIENTES_DATOS_ADICIONALES
                        join p in contexto.PACIENTES on d.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                        join e in contexto.ESTADO_CIVIL on d.ESTADO_CIVIL.ESC_CODIGO equals e.ESC_CODIGO
                        join c in contexto.TIPO_CIUDADANO on d.TIPO_CIUDADANO.TC_CODIGO equals c.TC_CODIGO
                        where d.DAP_ESTADO == true && p.PAC_CODIGO == keyPaciente
                        select d).FirstOrDefault();

            }
        }

        public void ReestablecerEstados(int codigoPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PACIENTES_DATOS_ADICIONALES> datos = new List<PACIENTES_DATOS_ADICIONALES>();
                datos = contexto.PACIENTES_DATOS_ADICIONALES.Include("PACIENTES").Where(d => d.PACIENTES.PAC_CODIGO == codigoPaciente).ToList();
                foreach (PACIENTES_DATOS_ADICIONALES dat in datos)
                {
                    dat.DAP_ESTADO = false;
                }
                contexto.SaveChanges();

            }
        }

        public List<PACIENTES_DATOS_ADICIONALES> listaDatosAdicionales(int keyPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PACIENTES_DATOS_ADICIONALES> datos = new List<PACIENTES_DATOS_ADICIONALES>();

                //datos = contexto.PACIENTES_DATOS_ADICIONALES.Include("PACIENTES").Where(d=>d.PACIENTES.PAC_CODIGO==codigoPaciente).ToList();
                datos = (from d in contexto.PACIENTES_DATOS_ADICIONALES
                         join p in contexto.PACIENTES on d.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                         join e in contexto.ESTADO_CIVIL on d.ESTADO_CIVIL.ESC_CODIGO equals e.ESC_CODIGO
                         where d.PACIENTES.PAC_CODIGO == keyPaciente && d.DAP_ESTADO == false
                         select d).ToList();

                return datos;
            }
        }

        public List<DtoPacienteDatosAdicionales> listaDatosAdicionalesDto(int keyPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<DtoPacienteDatosAdicionales> listaDatos = new List<DtoPacienteDatosAdicionales>();

                var datos = (from d in contexto.PACIENTES_DATOS_ADICIONALES
                             join p in contexto.PACIENTES on d.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                             join e in contexto.ESTADO_CIVIL on d.ESTADO_CIVIL.ESC_CODIGO equals e.ESC_CODIGO
                             join t in contexto.TIPO_CIUDADANO on d.TIPO_CIUDADANO.TC_CODIGO equals t.TC_CODIGO
                             where d.PACIENTES.PAC_CODIGO == keyPaciente && d.DAP_ESTADO == false
                             select new
                             {
                                 d.DAP_CODIGO,
                                 d.COD_PAIS,
                                 d.COD_PROVINCIA,
                                 d.COD_CANTON,
                                 d.COD_PARROQUIA,
                                 d.COD_SECTOR,
                                 d.DAP_DIRECCION_DOMICILIO,
                                 d.DAP_TELEFONO1,
                                 d.DAP_TELEFONO2,
                                 d.DAP_OCUPACION,
                                 d.DAP_INSTRUCCION,
                                 d.DAP_EMP_NOMBRE,
                                 e.ESC_NOMBRE,
                                 t.TC_DESCRIPCION
                             }
                             ).ToList();

                if (datos != null)
                {
                    foreach (var acceso in datos)
                    {
                        listaDatos.Add(new DtoPacienteDatosAdicionales()
                        {
                            CODIGO = acceso.DAP_CODIGO,
                            PAIS = acceso.COD_PAIS == null ? string.Empty : new DatDivisionPolitica().DivisionPolitica(acceso.COD_PAIS).DIPO_NOMBRE,
                            PROVINCIA = acceso.COD_PROVINCIA == null ? string.Empty : new DatDivisionPolitica().DivisionPolitica(acceso.COD_PROVINCIA).DIPO_NOMBRE,
                            CANTON = acceso.COD_CANTON == null ? string.Empty : new DatDivisionPolitica().DivisionPolitica(acceso.COD_CANTON).DIPO_NOMBRE,
                            PARROQUIA = acceso.COD_PARROQUIA == null ? string.Empty : new DatDivisionPolitica().DivisionPolitica(acceso.COD_PARROQUIA).DIPO_NOMBRE,
                            SECTOR = acceso.COD_SECTOR == null ? string.Empty : new DatDivisionPolitica().DivisionPolitica(acceso.COD_SECTOR).DIPO_NOMBRE,
                            DIRECCION = acceso.DAP_DIRECCION_DOMICILIO,
                            TELEFONO_1 = acceso.DAP_TELEFONO1,
                            TELEFONO_2 = acceso.DAP_TELEFONO2,
                            OCUPACION = acceso.DAP_OCUPACION,
                            INSTRUCCION = acceso.DAP_INSTRUCCION,
                            //NOMBRE_EMPRESA = acceso.DAP_EMP_NOMBRE,
                            ESTADO_CIVIL = acceso.ESC_NOMBRE,
                            TIPO_CIUDADANO = acceso.TC_DESCRIPCION

                        });
                    }
                }
                else
                {
                    listaDatos = null;
                }

                return listaDatos;
            }
        }

        public Int16 ultimoNumeroRegistro(int codPaciente)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var maximo = (from e in contexto.PACIENTES_DATOS_ADICIONALES
                              join p in contexto.PACIENTES on e.PACIENTES.PAC_CODIGO equals p.PAC_CODIGO
                              where e.PACIENTES.PAC_CODIGO == codPaciente
                              orderby e.DAP_NUMERO_REGISTRO descending
                              select e.DAP_NUMERO_REGISTRO).FirstOrDefault();
                return Convert.ToInt16(maximo);
            }
        }
        public PACIENTES_DATOS_ADICIONALES2 pacientesdatos2(Int64 pac_codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from p in db.PACIENTES_DATOS_ADICIONALES2
                        where p.PACIENTES.PAC_CODIGO == pac_codigo
                        select p).FirstOrDefault();
            }
        }
    }
}
