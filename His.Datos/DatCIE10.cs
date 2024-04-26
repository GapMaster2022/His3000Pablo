using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;


namespace His.Datos
{
    public class DatCIE10
    {
        public Int16 RecuperaMaximoCIE10()
        {
            return 0;
        }
        public CIE10 RecuperarCIE10(string codigoCIE10)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from g in contexto.CIE10
                            where g.CIE_CODIGO == codigoCIE10
                            select g).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }           
        }
        public List<CIE10> RecuperarCie10Formularios(Int64 ate_codigo)
        {
            using(var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<CIE10> Cie10s = new List<CIE10>();

                //Emergencia
                HC_EMERGENCIA_FORM emer = db.HC_EMERGENCIA_FORM.FirstOrDefault(x => x.ATENCIONES.ATE_CODIGO == ate_codigo);

                if(emer != null)
                {
                    List<HC_EMERGENCIA_FORM_DIAGNOSTICOS> lEmer = (from e in db.HC_EMERGENCIA_FORM_DIAGNOSTICOS
                                                                   where e.HC_EMERGENCIA_FORM.EMER_CODIGO == emer.EMER_CODIGO
                                                                   select e).ToList();
                    foreach (var item in lEmer)
                    {
                        CIE10 c = db.CIE10.FirstOrDefault(x => x.CIE_CODIGO == item.CIE_CODIGO);
                        Cie10s.Add(c);
                    }
                }

                //Consulta Externa
                string AteCodigo = ate_codigo.ToString();
                Form002MSP CxE = db.Form002MSP.FirstOrDefault(x => x.AteCodigo == AteCodigo);

                if(CxE != null)
                {
                    List<CIE10> CCxE = new List<CIE10>();
                    CIE10 xCie = new CIE10();
                    if(CxE.diagnostico1cie != "")
                    {
                        xCie = db.CIE10.FirstOrDefault(x => x.CIE_CODIGO == CxE.diagnostico1cie);
                        CCxE.Add(xCie);
                    }
                    if (CxE.diagnostico2cie != "")
                    {
                        xCie = db.CIE10.FirstOrDefault(x => x.CIE_CODIGO == CxE.diagnostico2cie);
                        CCxE.Add(xCie);
                    }
                    if (CxE.diagnostico3cie != "")
                    {
                        xCie = db.CIE10.FirstOrDefault(x => x.CIE_CODIGO == CxE.diagnostico3cie);
                        CCxE.Add(xCie);
                    }
                    if (CxE.diagnostico4cie != "")
                    {
                        xCie = db.CIE10.FirstOrDefault(x => x.CIE_CODIGO == CxE.diagnostico4cie);
                        CCxE.Add(xCie);
                    }


                    if (Cie10s.Count > 0) //entra a validar los cie10 para no repetirlos
                    {
                        foreach (var item in CCxE)
                        {
                            bool existe = false;
                            foreach (var i in Cie10s)
                            {
                                if (item.CIE_CODIGO == i.CIE_CODIGO)
                                    existe = true;
                            }
                            if(!existe)
                                Cie10s.Add(item);
                        }
                    }
                    else
                    {
                        Cie10s = CCxE;
                    }
                }
                return Cie10s = Cie10s.Distinct().ToList();
            }
        }
    }
}
