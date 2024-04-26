using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using His.Entidades;

namespace His.Datos
{
    public class DtoPreadmision
    {
        public bool crearPreadmision(PREADMISION cabecera, List<PREADMISION_DETALLE> detalle)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                DbTransaction transaction;
                ConexionEntidades.ConexionEDM.Open();
                transaction = ConexionEntidades.ConexionEDM.BeginTransaction();

                try
                {
                    db.AddToPREADMISION(cabecera);
                    db.SaveChanges();

                    var x = db.PREADMISION
                        .OrderByDescending(a => a.PRE_CODIGO)
                        .First().PRE_CODIGO;

                    foreach (var item in detalle)
                    {
                        item.PRE_CODIGO = x;
                        db.AddToPREADMISION_DETALLE(item);

                        db.SaveChanges();
                    }
                    transaction.Commit();
                    ConexionEntidades.ConexionEDM.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                    ConexionEntidades.ConexionEDM.Close();
                    return false;
                }
            }
        }
        public PREADMISION recuperaPreAdmision(string codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                PREADMISION pre = (from p in db.PREADMISION
                                   where p.PRE_IDENTIFICACION == codigo
                                   && p.PRE_ESTADO == true
                                   select p).FirstOrDefault();
                return pre;

            }
        }
        public PREADMISION recuperaPreAdmisionAte(Int64 ateNumero)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {

                return (from p in db.PREADMISION
                        where p.ATENCION == ateNumero
                        && p.PRE_ESTADO == true
                        select p).FirstOrDefault();

            }
        }
        public List<PREADMISION_DETALLE> recuperaPreAdmisionDetalle(Int64 codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PREADMISION_DETALLE> pd = new List<PREADMISION_DETALLE>();

                pd = (from p in db.PREADMISION_DETALLE
                      where p.PRE_CODIGO == codigo
                      select p).ToList();
                return pd;
            }
        }

        public List<DtoPreAdmision> listarPreadmision()
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<PREADMISION> q = (from p in db.PREADMISION
                                       where p.PRE_ESTADO == true
                                       select p).ToList();
                List<PREADMISION> pre = new List<PREADMISION>();
                foreach (var item in q)
                {
                    var q1 = (from p in db.PREADMISION
                              join a in db.ATENCIONES on p.ATENCION equals a.ATE_CODIGO
                              where a.ESC_CODIGO == 1 && a.ATE_CODIGO == item.ATENCION && a.TIPO_INGRESO.TIP_CODIGO == 1
                              orderby p.PRE_FECHA descending
                              select p).FirstOrDefault();
                    if (q1 != null)
                        pre.Add(q1);
                }
                List<DtoPreAdmision> obj = new List<DtoPreAdmision>();
                foreach (var item in pre)
                {
                    DtoPreAdmision admi = new DtoPreAdmision();
                    admi.CODIGO = item.PRE_CODIGO;
                    admi.NOMBRE = item.PRE_APELLIDO1 + " " + item.PRE_APELLIDO2 + " " + item.PRE_NOMBRE1 + " " + item.PRE_NOMBRE2;
                    admi.ID = item.PRE_IDENTIFICACION;
                    admi.F_INGRESO = Convert.ToString(item.PRE_FECHA);
                    admi.TIA_CODIGO = (Int32)item.TIA_CODIGO;
                    switch (item.PRIORIDAD)
                    {
                        case 1:
                            admi.PRIORIDAD = "ALTA";
                            break;
                        case 2:
                            admi.PRIORIDAD = "MEDIA";
                            break;
                        case 3:
                            admi.PRIORIDAD = "BAJA";
                            break;
                        default:
                            admi.PRIORIDAD = "BAJA";
                            break;
                    }
                    obj.Add(admi);
                }
                return obj;
            }
        }
        public void EliminarPreadmision(short codigo)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                PREADMISION paises = contexto.PREADMISION.SingleOrDefault(x => x.PRE_CODIGO == codigo);
                if (paises != null)
                {
                    contexto.DeleteObject(paises);
                    contexto.SaveChanges();
                }
                PREADMISION_DETALLE pread = contexto.PREADMISION_DETALLE.SingleOrDefault(x => x.PRE_CODIGO == codigo);
                if (pread != null)
                {
                    contexto.DeleteObject(pread);
                    contexto.SaveChanges();
                }
            }
        }
        public bool modificacionEstadoPreadmision(Int64 codigo)
        {
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                try
                {
                    PREADMISION x = new PREADMISION();

                    x = (from p in db.PREADMISION
                         where p.PRE_CODIGO == codigo
                         select p).FirstOrDefault();

                    x.PRE_ESTADO = false;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }
        public List<DtoPreAtencion> consultaPreatencion(DateTime desde, DateTime hasta, bool hc, string Phc)
        {
            List<DtoPreAtencion> pre = new List<DtoPreAtencion>();
            using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                if (hc)
                {
                    var c = (from p in db.PREADMISION
                             join m in db.MEDICOS on p.MED_CODIGO equals m.MED_CODIGO
                             join a in db.ATENCIONES on p.ATENCION equals a.ATE_CODIGO
                             join pa in db.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                             join U in db.USUARIOS on p.ID_USUARIO equals U.ID_USUARIO
                             where p.PRE_FECHA > desde && p.PRE_FECHA < hasta && pa.PAC_HISTORIA_CLINICA == Phc && p.PRE_ESTADO == true
                             select new { p, m, a, pa,U }).ToList();
                    foreach (var item in c)
                    {
                        DtoPreAtencion pr = new DtoPreAtencion();
                        pr.HC = item.pa.PAC_HISTORIA_CLINICA;
                        pr.ATENCION = item.a.ATE_NUMERO_ATENCION;
                        pr.PACIENTE = item.pa.PAC_APELLIDO_PATERNO + " " + item.pa.PAC_APELLIDO_MATERNO + " " + item.pa.PAC_NOMBRE1 + " " + item.pa.PAC_NOMBRE2;
                        pr.MEDICO = item.m.MED_APELLIDO_PATERNO + " " + item.m.MED_APELLIDO_MATERNO + " " + item.m.MED_NOMBRE1 + " " + item.m.MED_NOMBRE2;
                        pr.IDENTIFICACION = item.pa.PAC_IDENTIFICACION;
                        pr.EMAIL = item.pa.PAC_EMAIL;
                        pr.FECHA_PREINGRESO = Convert.ToString(item.p.PRE_FECHA);
                        pr.FECHA_CAMBIO_ATENCION = Convert.ToString(item.p.PRE_FECHA_ESTADO);
                        TimeSpan Duracion;
                        DateTime horainicio = (DateTime)item.p.PRE_FECHA;
                        DateTime horafin = DateTime.Now;
                        if (item.p.PRE_FECHA_ESTADO != null)
                            horafin = (DateTime)item.p.PRE_FECHA_ESTADO;
                        Duracion = horafin - horainicio;
                        //Duracion = horafin.Subtract(horainicio);
                        pr.TIEMPO_CAMBIO_ATENCION = Convert.ToString(Duracion.Days + " Dias " + Duracion.Hours + ":" + Duracion.Minutes + ":" + Duracion.Seconds);
                        pr.DIRECCION = item.p.PRE_DIRECCION;
                        pr.CELULAR = item.p.PRE_CELULAR;
                        switch (item.p.PRIORIDAD)
                        {
                            case 1:
                                pr.PRIORIDAD = "ALTA";
                                break;
                            case 2:
                                pr.PRIORIDAD = "MEDIA";
                                break;
                            case 3:
                                pr.PRIORIDAD = "BAJA";
                                break;
                            default:
                                pr.PRIORIDAD = "BAJA";
                                break;
                        }
                        pr.USUARIO = item.U.APELLIDOS + " " + item.U.NOMBRES;
                        pr.ESTADO = (bool)item.p.PRE_ESTADO;
                        switch (item.p.TIR_CODIGO)
                        {
                            case 0:
                                pr.TIPO_REFERIDO = "HOSPITALARIO";
                                break;
                            case 1:
                                pr.TIPO_REFERIDO = "PRIBADO";
                                break;
                            default:
                                pr.TIPO_REFERIDO = "N/A";
                                break;
                        }
                        switch (item.p.TIR_CODIGO)
                        {
                            case 0:
                                pr.TIPO_TRATAMIENTO = "CLINICO";
                                break;
                            case 1:
                                pr.TIPO_TRATAMIENTO = "OBSTETRICO";
                                break;
                            case 2:
                                pr.TIPO_TRATAMIENTO = "OTROS";
                                break;
                            case 3:
                                pr.TIPO_TRATAMIENTO = "QUIRURGICO";
                                break;
                            case 4:
                                pr.TIPO_TRATAMIENTO = "UCI";
                                break;
                            default:
                                pr.TIPO_TRATAMIENTO = "N/A";
                                break;
                        }
                        pr.SEGURO_MEDICO = item.p.SEGURO_MEDICO;
                        pr.PROCEDIMIENTO = item.p.PROCEDIMIENTO;
                        pre.Add(pr);
                    }
                }
                else
                {
                    var c = (from p in db.PREADMISION
                             join m in db.MEDICOS on p.MED_CODIGO equals m.MED_CODIGO
                             join a in db.ATENCIONES on p.ATENCION equals a.ATE_CODIGO
                             join pa in db.PACIENTES on a.PACIENTES.PAC_CODIGO equals pa.PAC_CODIGO
                             join U in db.USUARIOS on p.ID_USUARIO equals U.ID_USUARIO
                             where p.PRE_FECHA > desde && p.PRE_FECHA < hasta && p.PRE_ESTADO == true
                             select new { p, m, a, pa,U }).ToList();
                    foreach (var item in c)
                    {
                        DtoPreAtencion pr = new DtoPreAtencion();
                        pr.HC = item.pa.PAC_HISTORIA_CLINICA;
                        pr.ATENCION = item.a.ATE_NUMERO_ATENCION;
                        pr.PACIENTE = item.pa.PAC_APELLIDO_PATERNO + " " + item.pa.PAC_APELLIDO_MATERNO + " " + item.pa.PAC_NOMBRE1 + " " + item.pa.PAC_NOMBRE2;
                        pr.MEDICO = item.m.MED_APELLIDO_PATERNO + " " + item.m.MED_APELLIDO_MATERNO + " " + item.m.MED_NOMBRE1 + " " + item.m.MED_NOMBRE2;
                        pr.IDENTIFICACION = item.pa.PAC_IDENTIFICACION;
                        pr.EMAIL = item.pa.PAC_EMAIL;
                        pr.FECHA_PREINGRESO = Convert.ToString(item.p.PRE_FECHA);
                        pr.FECHA_CAMBIO_ATENCION = Convert.ToString(item.p.PRE_FECHA_ESTADO);
                        TimeSpan Duracion;
                        DateTime horainicio = (DateTime)item.p.PRE_FECHA;
                        DateTime horafin = DateTime.Now;
                        if (item.p.PRE_FECHA_ESTADO != null)
                            horafin = (DateTime)item.p.PRE_FECHA_ESTADO;
                        Duracion = horafin - horainicio;
                        //Duracion = horafin.Subtract(horainicio);
                        pr.TIEMPO_CAMBIO_ATENCION = Convert.ToString(Duracion.Days + " Dias " + Duracion.Hours + ":" + Duracion.Minutes + ":" + Duracion.Seconds);
                        pr.DIRECCION = item.p.PRE_DIRECCION;
                        pr.CELULAR = item.p.PRE_CELULAR;
                        switch (item.p.PRIORIDAD)
                        {
                            case 1:
                                pr.PRIORIDAD = "ALTA";
                                break;
                            case 2:
                                pr.PRIORIDAD = "MEDIA";
                                break;
                            case 3:
                                pr.PRIORIDAD = "BAJA";
                                break;
                            default:
                                pr.PRIORIDAD = "BAJA";
                                break;
                        }
                        pr.USUARIO = item.U.APELLIDOS + " " + item.U.NOMBRES;
                        pr.ESTADO = (bool)item.p.PRE_ESTADO;
                        switch (item.p.TIR_CODIGO)
                        {
                            case 0:
                                pr.TIPO_REFERIDO = "HOSPITALARIO";
                                break;
                            case 1:
                                pr.TIPO_REFERIDO = "PRIBADO";
                                break;
                            default:
                                pr.TIPO_REFERIDO = "N/A";
                                break;
                        }
                        switch (item.p.TIR_CODIGO)
                        {
                            case 0:
                                pr.TIPO_TRATAMIENTO = "CLINICO";
                                break;
                            case 1:
                                pr.TIPO_TRATAMIENTO = "OBSTETRICO";
                                break;
                            case 2:
                                pr.TIPO_TRATAMIENTO = "OTROS";
                                break;
                            case 3:
                                pr.TIPO_TRATAMIENTO = "QUIRURGICO";
                                break;
                            case 4:
                                pr.TIPO_TRATAMIENTO = "UCI";
                                break;
                            default:
                                pr.TIPO_TRATAMIENTO = "N/A";
                                break;
                        }
                        pr.SEGURO_MEDICO = item.p.SEGURO_MEDICO;
                        pr.PROCEDIMIENTO = item.p.PROCEDIMIENTO;
                        pre.Add(pr);
                    }
                }
                return pre;
            }
        }
    }
}
