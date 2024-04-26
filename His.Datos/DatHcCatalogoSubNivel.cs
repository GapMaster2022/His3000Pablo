using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using His.Entidades;
using Core.Datos;
using Core.Entidades;

namespace His.Datos
{
    public class DatHcCatalogoSubNivel
    {
        public int RecuperaMaximoHcCatalogoSubNivel()
        {
            int maxim;
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                List<HC_CATALOGO_SUBNIVEL> tipoCatalogoSubNivel = contexto.HC_CATALOGO_SUBNIVEL.ToList();
                if (tipoCatalogoSubNivel.Count > 0)
                    maxim = contexto.HC_CATALOGO_SUBNIVEL.Max(emp => emp.CA_CODIGO);
                else
                    maxim = 0;
                return maxim;
            }
        }

        public List<DtoHcCatalogoSubNivel> RecuperarHcCatalogoSubNivel(int codTipo)
        {
            List<DtoHcCatalogoSubNivel> antecedentesGrid = new List<DtoHcCatalogoSubNivel>();
            using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            {
                //return contexto.CIUDAD.FirstOrDefault(c => c.CODCIUDAD == codigoCiudad);
                List<HC_CATALOGO_SUBNIVEL> antList = new List<HC_CATALOGO_SUBNIVEL>();
                antList = contexto.HC_CATALOGO_SUBNIVEL.Where(c => c.HC_CATALOGOS.HCC_CODIGO == codTipo).ToList();
                foreach (var valor in antList)
                {
                    antecedentesGrid.Add(new DtoHcCatalogoSubNivel()
                    {
                        CA_CODIGO = valor.CA_CODIGO,
                        CA_INF = (bool)valor.CA_INF,
                        CA_DESCRIPCION = valor.CA_DESCRIPCION,
                        HCC_CODIGO = codTipo,
                        ENTITYSETNAME = valor.EntityKey.GetFullEntitySetName(),
                        ENTITYID = valor.EntityKey.EntityKeyValues[0].Key
                    }
                    );
                }
                return antecedentesGrid;
            }
        }

        public List<HC_CATALOGO_SUBNIVEL> RecuperarHcCatalogosSubNivel(int codTipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_CATALOGO_SUBNIVEL.Where(c => c.HC_CATALOGOS.HCC_CODIGO == codTipo).ToList();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public HC_CATALOGO_SUBNIVEL RecuperarHCCatalogosSubnivel(int codTipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return contexto.HC_CATALOGO_SUBNIVEL.Where(c => c.CA_CODIGO == codTipo).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Recupera un elemento HC_CATALOGO_SUBNIVEL con los datos de HC_CATALOGO y HC_CATALOGO_TIPO
        /// </summary>
        /// <param name="codTipo">codigo del HC_CATALOGO_SUBNIVEL</param>
        /// <returns>HC_CATALOGO_SUBNIVEL</returns>

        public HC_CATALOGO_SUBNIVEL RecuperarHCCatalogosSNivel(int codTipo)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from t in contexto.HC_CATALOGOS_TIPO
                                 join c in contexto.HC_CATALOGOS on t.HCT_CODIGO equals c.HC_CATALOGOS_TIPO.HCT_CODIGO
                                 join s in contexto.HC_CATALOGO_SUBNIVEL on c.HCC_CODIGO equals s.HC_CATALOGOS.HCC_CODIGO
                                 where s.CA_CODIGO == codTipo
                                 select s).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<HC_CATALOGO_SUBNIVEL> RecuperarSubNivel(int codTipo)
        {
            try
               {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    var lista = (from t in contexto.HC_CATALOGOS_TIPO
                                 join c in contexto.HC_CATALOGOS on t.HCT_CODIGO equals c.HC_CATALOGOS_TIPO.HCT_CODIGO
                                 join s in contexto.HC_CATALOGO_SUBNIVEL on c.HCC_CODIGO equals s.HC_CATALOGOS.HCC_CODIGO
                                 where t.HCT_CODIGO == codTipo
                                 select s).ToList();
                    return lista;
                    //ulgdbListadoCatSub.DataSource = lista;
                   }
               }
            catch (Exception err)
            {
                throw err;
            }
        }


        public HC_CATALOGO_SUBNIVEL RecuperarSubNivelCatalogo(int codCatalogo, int codSubNivel)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    return (from t in contexto.HC_CATALOGOS_TIPO
                            join c in contexto.HC_CATALOGOS on t.HCT_CODIGO equals c.HC_CATALOGOS_TIPO.HCT_CODIGO
                            join s in contexto.HC_CATALOGO_SUBNIVEL on c.HCC_CODIGO equals s.HC_CATALOGOS.HCC_CODIGO
                            where t.HCT_CODIGO == codCatalogo && s.CA_CODIGO == codSubNivel
                            select s).FirstOrDefault();
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


    }
}
