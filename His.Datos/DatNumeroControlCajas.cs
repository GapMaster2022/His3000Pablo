using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatNumeroControlCajas
    {
        public Int16 RecuperaMaximoNumeroControlCajas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                var lista = (from n in contexto.NUMERO_CONTROL_CAJAS
                             select n.NCC_CODIGO).ToList();

                if (lista.Count > 0)
                    return lista.Max();

                return 0;
            }
        }

        public List<NUMERO_CONTROL_CAJAS> ListaNumeroControlCajas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from ncc in contexto.NUMERO_CONTROL_CAJAS
                        join c in contexto.CAJAS on ncc.CAJAS.CAJ_CODIGO equals c.CAJ_CODIGO
                        join td in contexto.TIPO_DOCUMENTO on ncc.TIPO_DOCUMENTO.TID_CODIGO equals td.TID_CODIGO    
                        select ncc).ToList();  
            }
        }


        public List<DtoNumeroControlCajas> RecuperaNumControlCajas()
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<DtoNumeroControlCajas> listaNcontrol = new List<DtoNumeroControlCajas>();
                var lista = from c in contexto.CAJAS
                            join ncc in contexto.NUMERO_CONTROL_CAJAS on c.CAJ_CODIGO equals ncc.CAJAS.CAJ_CODIGO
                            join td in contexto.TIPO_DOCUMENTO on ncc.TIPO_DOCUMENTO.TID_CODIGO equals td.TID_CODIGO
                            select new { ncc.NCC_CODIGO, c.CAJ_CODIGO ,c.CAJ_NOMBRE, td.TID_CODIGO,td.TID_DESCRIPCION, ncc.NCC_NUMERO, ncc.NCC_FACTURA_INICIAL, ncc.NCC_FACTURA_FINAL, ncc.NCC_TIPO };     
                            

                if (lista != null)
                {
                    foreach (var acceso in lista)
                    {
                        listaNcontrol.Add(new DtoNumeroControlCajas()
                        {
                            NCC_CODIGO = acceso.NCC_CODIGO,
                            CAJ_NOMBRE = acceso.CAJ_NOMBRE,
                            TID_DESCRIPCION = acceso.TID_DESCRIPCION,
                            NCC_NUMERO = acceso.NCC_NUMERO,
                            NCC_FACTURA_INICIAL = (Int32)acceso.NCC_FACTURA_INICIAL,
                            NCC_FACTURA_FINAL = (Int32)acceso.NCC_FACTURA_FINAL,
                            NCC_TIPO = acceso.NCC_TIPO,
                            CAJ_CODIGO = acceso.CAJ_CODIGO,
                            TID_CODIGO = acceso.TID_CODIGO 
                            //PRE_VALOR = Convert.ToDecimal(acceso.PRE_VALOR)
                        });
                    }
                }
                else
                {
                    listaNcontrol = null;
                }


                return listaNcontrol;

            }
        }

        public List<DtoNumeroControlCajas> RecuperaNumControlCajas(int codCaja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<DtoNumeroControlCajas> listaNcontrol = new List<DtoNumeroControlCajas>();
                var lista = from c in contexto.CAJAS
                            join ncc in contexto.NUMERO_CONTROL_CAJAS on c.CAJ_CODIGO equals ncc.CAJAS.CAJ_CODIGO
                            join td in contexto.TIPO_DOCUMENTO on ncc.TIPO_DOCUMENTO.TID_CODIGO equals td.TID_CODIGO
                            where c.CAJ_CODIGO == codCaja
                            select new { ncc.NCC_CODIGO, c.CAJ_CODIGO, c.CAJ_NOMBRE, td.TID_CODIGO, td.TID_DESCRIPCION, ncc.NCC_NUMERO, ncc.NCC_FACTURA_INICIAL, ncc.NCC_FACTURA_FINAL, ncc.NCC_TIPO };


                if (lista != null)
                {
                    foreach (var acceso in lista)
                    {
                        listaNcontrol.Add(new DtoNumeroControlCajas()
                        {
                            NCC_CODIGO = acceso.NCC_CODIGO,
                            CAJ_NOMBRE = acceso.CAJ_NOMBRE,
                            TID_DESCRIPCION = acceso.TID_DESCRIPCION,
                            NCC_NUMERO = acceso.NCC_NUMERO,
                            NCC_FACTURA_INICIAL = (Int32)acceso.NCC_FACTURA_INICIAL,
                            NCC_FACTURA_FINAL = (Int32)acceso.NCC_FACTURA_FINAL,
                            NCC_TIPO = acceso.NCC_TIPO,
                            CAJ_CODIGO = acceso.CAJ_CODIGO,
                            TID_CODIGO = acceso.TID_CODIGO
                            //PRE_VALOR = Convert.ToDecimal(acceso.PRE_VALOR)
                        });
                    }
                }
                else
                {
                    listaNcontrol = null;
                }

                return listaNcontrol;
            }
        }


        public void CrearNumControlCaja(NUMERO_CONTROL_CAJAS numcontrolCaja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                contexto.AddToNUMERO_CONTROL_CAJAS(numcontrolCaja);
                contexto.SaveChanges();   
            }
        }

        public void GrabarNumControlCaja(NUMERO_CONTROL_CAJAS nccModificada, NUMERO_CONTROL_CAJAS nccOriginal)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                NUMERO_CONTROL_CAJAS ncajas = contexto.NUMERO_CONTROL_CAJAS.FirstOrDefault(c => c.NCC_CODIGO == nccModificada.NCC_CODIGO);
                ncajas.CAJASReference.EntityKey = nccModificada.CAJASReference.EntityKey;
                ncajas.TIPO_DOCUMENTOReference.EntityKey = nccModificada.TIPO_DOCUMENTOReference.EntityKey;
                ncajas.NCC_NUMERO = nccModificada.NCC_NUMERO;               
                contexto.SaveChanges();  
            }
        }

        public void EliminarNumControlCaja(NUMERO_CONTROL_CAJAS numcontrolCajas)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                NUMERO_CONTROL_CAJAS nccajas = contexto.NUMERO_CONTROL_CAJAS.FirstOrDefault(c => c.NCC_CODIGO == numcontrolCajas.NCC_CODIGO);
                contexto.DeleteObject(nccajas);
                contexto.SaveChanges(); 
            }
        }

        public NUMERO_CONTROL_CAJAS RecuperarNumeroControlCajaID(int codNumControlCaja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from n in contexto.NUMERO_CONTROL_CAJAS
                        where n.NCC_CODIGO == codNumControlCaja
                        select n).FirstOrDefault();
            }
        }



        public NUMERO_CONTROL_CAJAS RecuperarNumeroControlCajaDoc(int codCaja)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                return (from nc in contexto.NUMERO_CONTROL_CAJAS
                        join c in contexto.CAJAS on nc.CAJAS.CAJ_CODIGO equals codCaja
                        join t in contexto.TIPO_DOCUMENTO on nc.TIPO_DOCUMENTO.TID_CODIGO equals 1
                        select nc).FirstOrDefault();
            }
        }

        public void EditarNumControlCaja(NUMERO_CONTROL_CAJAS nccModificada)
        {
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                NUMERO_CONTROL_CAJAS nccOriginal = contexto.NUMERO_CONTROL_CAJAS.FirstOrDefault(n=>n.NCC_CODIGO==nccModificada.NCC_CODIGO);
                nccOriginal.NCC_FACTURA_INICIAL = nccModificada.NCC_FACTURA_INICIAL;
                nccOriginal.NCC_FACTURA_FINAL = nccModificada.NCC_FACTURA_FINAL;
                nccOriginal.NCC_NUMERO = nccModificada.NCC_NUMERO;
                nccOriginal.NCC_TIPO = nccModificada.NCC_TIPO;
                contexto.SaveChanges();
            }
        }

    }
}
