using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Data;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Core.Entidades
{
    /// <summary>
    /// Clase que extiende la funcionalidad de las Entidades
    /// </summary>
    public static class ExtensionEntidades
    {
        /// Caragamos un enumerado en una Lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> RecuperarEnumeradoLista<T>()
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("La lista deberá ser un enumerado");

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }
            return enumValList;
        }
        /// <summary>
        /// Genera una copia en memoria de una Entidad
        /// </summary>
        /// <typeparam name="T">Tipo de la Entidad</typeparam>
        /// <param name="p">Instancia a ClonarEntidad</param>
        /// <returns>Entidad clonada</returns>
        public static T ClonarEntidad<T>(this T p)
        {
            T clon;
            var serializador = new DataContractSerializer(typeof(T));
            var memoryStream = new MemoryStream();
            serializador.WriteObject(memoryStream, p);
            memoryStream.Position = 0;
            clon = (T)serializador.ReadObject(memoryStream);
            return clon;

        }

        /// <summary>
        /// Obtiene el EntityKey
        /// </summary>
        /// <param name="relatedEnd"></param>
        /// <returns></returns>
        public static EntityKey ObtenerEntityKey(this IRelatedEnd relatedEnd)
        {
            Debug.Assert(relatedEnd.EsEntityReference());
            Type relationshipType = relatedEnd.GetType();
            PropertyInfo pi = relationshipType.GetProperty("EntityKey");
            return (EntityKey)pi.GetValue(relatedEnd, null);
        }

        #region 
        /// <summary>
        /// Quita el value de la entidad de refencia y mantiene solo entityKey de EntityReference
        /// Usar para cuando quiere cambiar de catalogos entidades referenciadas
        /// Trabaja solo en el primer nivel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entidades">Lista de entidades</param>
        public static void EstablecerReference<T>(List<T> entidades) where T : EntityObject
        {
            if (entidades != null)
                entidades.ForEach(EstablecerReference);

        }
        /// <summary>
        /// Quita el value de la entidad de refencia y mantiene solo entityKey de EntityReference
        /// Usar para cuando quiere cambiar de catalogos entidades referenciadas
        /// Trabaja solo en el primer nivel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entidad"></param>
        public static void EstablecerReference<T>(T entidad) where T : EntityObject
        {
            IEnumerable<IRelatedEnd> relacionesModificadas = ((IEntityWithRelationships)entidad).RelationshipManager.GetAllRelatedEnds().Where(r => r.EsEntityReference());
            foreach (var relacion in relacionesModificadas)
            {
                var entidades = ObtenerColeccionEntidades(relacion);
                if (entidades != null && entidades.Count > 0)
                    QuitarRelacionEntidades(relacion, entidades[0]);
            }
        }
        /// <summary>
        /// Verifica si una lista de entidades tiene o no cambios en cualquiera de sus niveles
        /// Es necesario que tanto modificado como original reflejen los mismos niveles
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listaModificados"></param>
        /// <param name="listaOriginales"></param>
        /// <returns></returns>
        public static bool ExistenCambiosEntidades<T>(List<T> listaModificados, List<T> listaOriginales) where T : EntityObject
        {
            bool hayCambios = false;

            var nuevos = listaModificados.Where(p => p.EntityKey == null).FirstOrDefault(); //Nuevos
            if (nuevos != null)
                return true;

            if (listaModificados.Count != listaOriginales.Count)  //Eliminados
                return true;

            if (listaModificados != null && listaOriginales != null)
            {
                foreach (var entidadOriginal in listaOriginales)
                {
                    var entidadModificada = listaModificados.Where(p => p.EntityKey == entidadOriginal.EntityKey).FirstOrDefault();

                    if (entidadModificada != null)
                    {
                        var huboCambios = ObtenerCambiosEntidades(entidadModificada, entidadOriginal, true);
                        hayCambios = hayCambios || huboCambios;

                        if (huboCambios)
                            return true;

                    }
                }
            }

            return hayCambios;
        }
        /// <summary>
        /// Verifica si una entidad tiene o no cambios en cualquiera de sus niveles
        /// Es necesario que tanto modificado como original reflejen los mismos niveles
        /// </summary>
        /// <param name="modificado">Entidad Modificada</param>
        /// <param name="original">Entidad Original</param>
        /// <returns></returns>
        public static bool ExistenCambiosEntidades(EntityObject modificado, EntityObject original)
        {
            if (modificado != null && original != null)
                return ObtenerCambiosEntidades(modificado, original, true);
            if (modificado != null && original == null)
                return true;
            return false;
        }


        /// <summary>
        /// Verifica si una entidad tiene o no cambios en cualquiera de sus niveles
        /// Es necesario que tanto modificado como original reflejen los mismos niveles
        /// </summary>
        /// <param name="modificado">Entidad Modificada</param>
        /// <param name="original">Entidad Original</param>
        /// <returns></returns>
        public static bool ExistenCambiosMensaje(BaseEntidad modificado, BaseEntidad original)
        {
            if (modificado != null && original != null)
                return checkIfModified(modificado, original);
            if (modificado != null && original == null)
                return true;
            return false;
        }

        /// <summary>
        ///  Obtiene cambio de una lista de Dto
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="listaOriginal">Lista original</param>
        /// <param name="listaModificada">Lista modioficada</param>
        /// <param name="identificador">Identificador</param>
        public static void ObtenerCambiosMensaje<T>(List<T> listaOriginal, List<T> listaModificada, string identificador) where T : BaseEntidad
        {
            if (listaModificada != null && listaOriginal != null)
            {
                List<T> entidadesAQuitar = new List<T>();
                foreach (var original in listaOriginal)
                {

                    var modificado = listaModificada.Where(lm => lm.GetType().GetProperty(identificador).GetValue(lm, null) == original.GetType().GetProperty(identificador).GetValue(original, null)).FirstOrDefault();
                    if (modificado != null)//existe
                    {
                        if (!checkIfModified(modificado, original))
                        {
                            entidadesAQuitar.Add(modificado);
                        }
                    }
                }
                foreach (var aQuitar in entidadesAQuitar)//quitar en los modificados como en los originales
                {
                    int indice = listaModificada.Select((lm, i) => new { Indice = i, Valor = lm }).ToList()
                        .Where(m => m.GetType().GetProperty(identificador).GetValue(m, null) == aQuitar.GetType().GetProperty(identificador).GetValue(aQuitar, null))
                        .Select(p => p.Indice).FirstOrDefault();
                    listaModificada.RemoveAt(indice);

                    indice = listaOriginal.Select((lo, i) => new { Indice = i, Valor = lo }).ToList()
                        .Where(m => m.GetType().GetProperty(identificador).GetValue(m, null) == aQuitar.GetType().GetProperty(identificador).GetValue(aQuitar, null))
                        .Select(p => p.Indice).FirstOrDefault();
                    listaOriginal.RemoveAt(indice);
                }
            }
        }

        /// <summary>
        /// Verifica los cambios entre 2 listas de Dto
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="listaOriginal">Lista Original</param>
        /// <param name="listaModificada">Lista Modificada</param>
        /// <param name="identificador">Identificador de clave unica</param>
        /// <returns>Retorna un booleano</returns>
        public static bool ExistenCambiosMensaje<T>(List<T> listaOriginal, List<T> listaModificada, string identificador) where T : BaseEntidad
        {
            if (listaModificada != null && listaOriginal == null)
                return true;

            if (listaModificada != null && listaOriginal != null)
            {
                if (listaOriginal.Count != listaModificada.Count)
                    return true;

                object propertyValueM;

                foreach (T objM in listaModificada)
                {
                    if (objM.GetType().GetProperty(identificador) != null)
                    {
                        propertyValueM = objM.GetType().GetProperty(identificador).GetValue(objM, null);

                        T objO = listaOriginal.Where(ori => ori.GetType().GetProperty(identificador).Name.ToString() == identificador && ori.GetType().GetProperty(identificador).GetValue(ori, null).ToString() == propertyValueM.ToString()).FirstOrDefault();

                        if (objO == null)
                            return true;
                        else
                        {
                            if (checkIfModified(objM, objO))
                                return true;
                        }
                    }
                    else
                        return true;
                }
            }
            else
                return true;


            return false;
        }



        /// <summary>
        /// Obtiene solo entidades modificadas y sus correspondiente originales tanto exteriormente como internamente en su jerarquía
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listaModificados">Lista modificada no nula</param>
        /// <param name="listaOriginales">Lista original no nula</param>
        public static void ObtenerCambiosEntidades<T>(List<T> listaModificados, List<T> listaOriginales) where T : EntityObject
        {
            if (listaModificados != null && listaOriginales != null)
            {
                List<T> entidadesAQuitar = new List<T>();
                foreach (var original in listaOriginales)
                {
                    var modificado = listaModificados.Where(lm => lm.EntityKey == original.EntityKey).FirstOrDefault();
                    if (modificado != null)//existe
                    {
                        if (!ObtenerCambiosEntidades(modificado, original, false))
                        {
                            entidadesAQuitar.Add(modificado);
                        }
                    }
                }
                foreach (var aQuitar in entidadesAQuitar)//quitar en los modificados como en los originales
                {
                    int indice = listaModificados.Select((lm, i) => new { Indice = i, Valor = lm }).ToList()
                        .Where(m => m.Valor.EntityKey == aQuitar.EntityKey)
                        .Select(p => p.Indice).FirstOrDefault();
                    listaModificados.RemoveAt(indice);
                    indice = listaOriginales.Select((lo, i) => new { Indice = i, Valor = lo }).ToList()
                        .Where(m => m.Valor.EntityKey == aQuitar.EntityKey)
                        .Select(p => p.Indice).FirstOrDefault();
                    listaOriginales.RemoveAt(indice);
                }
            }
        }
        /// <summary>
        /// Método recursivo que verifica si una entidad tiene algún cambio en su nivel y los que no existan cambio  fuera del padre los quita, 
        /// de igual forma pasa con los niveles hijos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modificado"></param>
        /// <param name="original"></param>                
        public static void ObtenerCambiosEntidades<T>(T modificado, T original) where T : EntityObject
        {
            ObtenerCambiosEntidades(modificado, original, false);
        }



        /// <summary>
        /// Método que verifica si una entidad tiene algún cambio en su nivel y los que no existan cambio  fuera del padre los quita
        /// a métodos de asociacion si existe la entidad verifica si hubo cambios 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modificado"></param>
        /// <param name="original"></param>
        /// <param name="soloLectura">Si es de solo lectura o tambien modifica las entidades</param>
        /// <returns>Si hubo o no algun tipo de cambio en la entidad donde: false no hubo cambios, true hubo cambios</returns>
        private static bool ObtenerCambiosEntidades<T>(T modificado, T original, bool soloLectura) where T : EntityObject
        {
            if (modificado != null && original != null)
            {
                var nivelPadre = checkIfModified(modificado, original);//existen cambios en el nivel raíz
                List<string> padres = new List<string>();
                padres.Add(original.GetType().Name);
                var nivelHijos = ObtenerCambiosHijos(modificado, original, padres, soloLectura);//revisa cambios en cada hijo .  y si nivel es hijo y no hubo cambios 
                //lo quita de la lista o coleccion
                //verificar no nivel hijos sino nivel catalogo
                return nivelPadre || nivelHijos;
            } return false;
        }


        /// <summary>
        /// Copia las propiedades de la entidad original a la entidad modificada
        /// </summary>
        /// <param name="m">Entidad Modificada</param>
        /// <param name="o">Entidad Original</param>
        public static void CopiarPropiedadesOriginalAModificado(EntityObject m, EntityObject o)
        {
            foreach (PropertyInfo property in m.GetType().GetProperties())
            {
                if (property.PropertyType.Namespace.Equals("System"))
                {
                    object propertyValueO = o.GetType().GetProperty(property.Name).GetValue(o, null);
                    property.SetValue(m, propertyValueO, null);
                }
            }
        }


        private static bool ObtenerCambiosHijos(EntityObject modificado, EntityObject original, List<string> padres, bool soloLectura)
        {
            bool retorno = false;
            IEnumerable<IRelatedEnd> relacionesModificadas = ((IEntityWithRelationships)modificado).RelationshipManager.GetAllRelatedEnds();
            IEnumerable<IRelatedEnd> relacionesOriginales = ((IEntityWithRelationships)original).RelationshipManager.GetAllRelatedEnds();
            var padresClonado = padres.ClonarEntidad();
            //Filtrar para cuando sea de otro tipo de realcion es IsEntityReference
            //Controlar el envio de diferentes padres pues todos se integran en uno solo
            foreach (var relacionOriginal in relacionesOriginales)
            {
                List<EntityObject> tempQuitarEntidadesSinCambiosModificado = new List<EntityObject>();
                List<EntityObject> tempQuitarEntidadesSinCambiosOriginal = new List<EntityObject>();
                var relacionModificada = relacionesModificadas.Where(rm => rm.RelationshipName == relacionOriginal.RelationshipName).FirstOrDefault();
                var numeroRecurrenciasPadre = padresClonado.Where(p => p == relacionOriginal.TargetRoleName).Count();
                if (numeroRecurrenciasPadre == 0)
                {
                    List<EntityObject> entidadesinternaModificadas = ObtenerColeccionEntidades(relacionModificada);
                    List<EntityObject> entidadesinternaOriginales = ObtenerColeccionEntidades(relacionOriginal);
                    foreach (var entidadOriginal in entidadesinternaOriginales)
                    {
                        //Debe existir el modificado para un original sino enviar mensaje de error
                        var entidadModificada = entidadesinternaModificadas.Where(em => em.EntityKey == entidadOriginal.EntityKey).FirstOrDefault();
                        if (entidadModificada != null)
                        {
                            padresClonado.Add(entidadOriginal.GetType().Name);
                            var nivelPadre = checkIfModified(entidadModificada, entidadOriginal);
                            var nivelHijos = ObtenerCambiosHijos(entidadModificada, entidadOriginal, padresClonado, soloLectura);
                            if ((!nivelPadre && !nivelHijos) && !soloLectura)
                            {
                                tempQuitarEntidadesSinCambiosOriginal.Add(entidadOriginal);//entida que no hubo cambios Original
                                tempQuitarEntidadesSinCambiosModificado.Add(entidadModificada);//entida que no hubo cambios Modificada
                            }
                            retorno = retorno || nivelPadre || nivelHijos;//Retorno cada item lo comparo para ver si en alguno hubo algun cambio
                        }
                        else//No se hallo la entidad Modificada.
                        //Puede ser nueva , o se borro
                        //O Podria ser de una relacion EntityReference
                        //entonces se debe dar un tratamiento especial. 
                        //Quitando la Relación y manteniendo el EntityKey.. de ambos casos modificado y original
                        {
                            retorno = retorno || true;
                            if (relacionModificada.EsEntityReference() && entidadesinternaModificadas != null && entidadesinternaModificadas.Count == 1 && !soloLectura)
                            {
                                QuitarRelacionEntidades(relacionModificada, entidadesinternaModificadas[0]);
                                QuitarRelacionEntidades(relacionOriginal, entidadOriginal);
                            }
                        }
                    }

                    //PM Codigo para establecer todas las referencias de las entidades  que son de tipo asociasiones
                    List<IRelatedEnd> listaModificadosConReferencia = ((IEntityWithRelationships)modificado).RelationshipManager.GetAllRelatedEnds().Where(p => p.EsEntityReference() == true).ToList<IRelatedEnd>();
                    List<IRelatedEnd> listaOriginalesConReferencia = ((IEntityWithRelationships)original).RelationshipManager.GetAllRelatedEnds().Where(p => p.EsEntityReference() == true).ToList<IRelatedEnd>();
                    //Me barro todas las referencias para determinar exactamente cualse cambiaron
                    foreach (EntityReference referenciaModificada in listaModificadosConReferencia)
                    {
                        foreach (EntityReference referenciaOriginal in listaOriginalesConReferencia)
                        {
                            if (referenciaOriginal.SourceRoleName == referenciaModificada.SourceRoleName && referenciaOriginal.TargetRoleName == referenciaModificada.TargetRoleName && referenciaOriginal.RelationshipName == referenciaModificada.RelationshipName)
                            {
                                if (referenciaOriginal.EntityKey != referenciaModificada.EntityKey)
                                    retorno = true;
                            }
                        }
                    }

                    //Verifico si se ingresaron entidades nuevas
                    if (entidadesinternaModificadas.Where(em => em.EntityKey == null).Count() > 0)
                    {
                        retorno = retorno || true;
                    }

                    //Quitar entidades sin cambios
                    QuitarRelacionEntidades(relacionModificada, tempQuitarEntidadesSinCambiosModificado);
                    QuitarRelacionEntidades(relacionOriginal, tempQuitarEntidadesSinCambiosOriginal);

                }

            }
            return retorno;
        }
        private static void QuitarRelacionEntidades(IRelatedEnd relacion, List<EntityObject> entidades)
        {
            if (relacion.EsEntityReference() && entidades != null && entidades.Count > 0)
            {
                QuitarRelacionEntidades(relacion, entidades[0]);
            }
            else//Refencias tipo entityCollection
                foreach (EntityObject entidad in entidades)
                    relacion.Remove(entidad);//
        }
        private static void QuitarRelacionEntidades(IRelatedEnd relacion, EntityObject entidad)
        {
            var refEntityKey = relacion.ObtenerEntityKey().ClonarEntidad();//Si por si acaso mantuvo relacion con la entidad
            if (refEntityKey == null && entidad.EntityKey != null)
                refEntityKey = entidad.EntityKey.ClonarEntidad();
            // al momento de remover el entity reference.EntityKey se pierde            
            if (refEntityKey != null)
            {
                relacion.Remove(entidad);
                ((System.Data.Objects.DataClasses.EntityReference)(relacion)).EntityKey = refEntityKey;
            }
        }
        private static List<EntityObject> ObtenerColeccionEntidades(IRelatedEnd relacion)
        {
            List<EntityObject> entidadesinternaModificadas = new List<EntityObject>();
            foreach (EntityObject entidad in relacion)
            {
                entidadesinternaModificadas.Add(entidad);
            }
            return entidadesinternaModificadas;
        }
        #endregion


        /// <summary>
        /// Verifica si el objeto ha sido modificado o no. en primer nivel
        /// </summary>
        /// <param name="m"></param>
        /// <param name="o"></param>
        /// <returns></returns>        
        private static bool checkIfModified(object m, object o)
        {

            bool isModified = false;
            foreach (PropertyInfo property in m.GetType().GetProperties())
            {
                if (property.PropertyType.Namespace.Equals("System"))
                {
                    object propertyValueM = property.GetValue(m, null);
                    object propertyValueO = o.GetType().GetProperty(property.Name).GetValue(o, null);

                    if (propertyValueM != null)
                    {
                        if (propertyValueM.GetType().Name == typeof(byte[]).Name)//Compara arreglo de bytes
                        {
                            if (ByteEsDiferente(propertyValueM, propertyValueO))
                                isModified = true;
                        }
                        else
                            if (!propertyValueM.Equals(propertyValueO))
                            {
                                isModified = true;
                                break;
                            }
                    }
                    else
                    {
                        if (propertyValueO != null)
                        {
                            isModified = true;
                            break;
                        }
                    }

                }

            }

            return isModified;
        }
        private static bool ByteEsDiferente(object propertyValueM, object propertyValueO)
        {
            var propiedadModificada = (byte[])propertyValueM;
            var propiedadOriginal = (byte[])propertyValueO;
            if (propiedadModificada != null && propiedadOriginal != null)
            {
                for (int i = 0; i < propiedadModificada.Length; i++)
                    if (!propiedadModificada[i].Equals(propiedadOriginal[i]))
                        return true;
            }
            else
            {
                if (propiedadModificada != null || propiedadOriginal != null)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Verifica si existe referencia hacia la entidad
        /// </summary>
        /// <param name="relatedEnd"></param>
        /// <returns></returns>
        public static bool EsEntityReference(this IRelatedEnd relatedEnd)
        {
            Type relationshipType = relatedEnd.GetType();
            return (relationshipType.GetGenericTypeDefinition() == typeof(EntityReference<>));
        }

        /// <summary>
        /// Reemplazar una Entidad (detached) dentro de una Lista Genérica
        /// </summary>
        /// <typeparam name="Entidad"></typeparam>
        /// <param name="lista"></param>
        /// <param name="itemCambiado"></param>
        /// <returns></returns>
        public static IList<Entidad> ReemplazarItem<Entidad>(this IList<Entidad> lista, Entidad itemCambiado) where Entidad : EntityObject
        {
            int posicion = lista
                .Select((prod, pos) => new { Item = prod, Posicion = pos })
                .Single(dupla => dupla.Item.EntityKey == itemCambiado.EntityKey)
                .Posicion;

            lista[posicion] = itemCambiado;
            return lista;
        }

        /// <summary>
        /// Obtiene todo el nombre de la entidad con el contenedor
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetFullEntitySetName(this EntityKey key)
        {
            return key.EntityContainerName + "." + key.EntitySetName;
        }

        /// <summary>
        /// Obtiene una copia de una lista original
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalTypeList"></param>
        /// <returns></returns>
        public static List<T> GetShallowCopy<T>(List<T> originalTypeList)
        {

            List<T> cloneList = new List<T>();
            foreach (T item in originalTypeList)
            {
                T cloneItem = item.ClonarEntidad();
                cloneList.Add((T)cloneItem);
            }

            return cloneList;
        }

        /// <summary>
        /// Para comparar una propiedad con una lista de valores 
        /// simula a la expresion where campo in listaValores
        /// ejemplo:
        /// BuildContainsExpression « Estructura, int » (es =» es.IdEstructura, idsEstructuraPadre)
        /// </summary>
        /// <typeparam name="TElement">Entidad</typeparam>
        /// <typeparam name="TValue">Tipo de dato</typeparam>
        /// <param name="valueSelector">propiedad</param>
        /// <param name="values">Lista de Valores</param>
        /// <returns>expresion</returns>
        public static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(

                 Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {

            if (null == valueSelector) { throw new ArgumentNullException("valueSelector"); }

            if (null == values) { throw new ArgumentNullException("values"); }

            ParameterExpression p = valueSelector.Parameters.Single();

            // p => valueSelector(p) == values[0] || valueSelector(p) == ...

            if (!values.Any())
            {

                return e => false;

            }

            var equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));

            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);

        }

    }
}
