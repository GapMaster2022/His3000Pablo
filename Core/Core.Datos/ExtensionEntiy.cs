using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Reflection;
using Core.Entidades;
using System.Data;
using System.Data.EntityClient;
namespace Core.Datos
{
    /// <summary>
    /// Clase extension de entidades
    /// </summary>
    public static class ExtensionEntiy
    {
        /// <summary>
        ///  Permite crear los registros en base a una entidad
        /// </summary>
        /// <param name="contexto">Nombre del contexto</param>
        /// <param name="nombreContenedor">Nombre del contenedor</param>
        /// <param name="entidad">Entidad enviada a crear</param>
        public static int Crear(this ObjectContext contexto, string nombreContenedor, EntityObject entidad)
        {
            contexto.AddObject(nombreContenedor, entidad);
            return contexto.SaveChanges();
        }
        public static int EditarHM(this ObjectContext contexto, string Tabla, EntityObject entidad)
        {
            contexto.AttachUpdated(entidad);
            return contexto.SaveChanges();
        }
        /// <summary>
        ///  Permite eliminar una sola entidad
        /// </summary>
        /// <param name="contexto">Contexto sobre el que se va a trabajar</param>
        /// <param name="entidad"> Entidad que se envia para eliminar</param>
        public static void Eliminar(this ObjectContext contexto, EntityObject entidad)
        {
            contexto.Attach(entidad);
            contexto.DeleteObject(entidad);
            contexto.SaveChanges();
        }
        /// <summary>
        ///  Permite grabar una lista
        /// </summary>
        /// <typeparam name="T">Tipo de entidad</typeparam>
        /// <param name="contexto">contexto sobre el que se trabaja</param>
        /// <param name="listaEntidad">Lista de entidades a grabar</param>
        public static int GrabarLista<T>(this ObjectContext contexto, string nombreContenedor, List<T> listaEntidad) where T : EntityObject
        {
            foreach (EntityObject entidad in listaEntidad)
            {
                if (entidad.EntityKey == null)
                {
                 
                    contexto.AddObject(nombreContenedor, entidad);
                }
                else
                    ModificarDatos(contexto, entidad);

            }
            return contexto.SaveChanges();
        }
        public static void ModificarDatos(ObjectContext contexto, EntityObject entidad)
        {

            contexto.AttachTo(ExtensionEntidades.GetFullEntitySetName(entidad.EntityKey), entidad);
            entidad.SetAllModified(contexto);
        }
        /// <summary>
        /// Modifica la entidad que se envia como extensión al contexto
        /// </summary>
        /// <typeparam name="T">Tipo de Entidad</typeparam>
        /// <param name="entity">Instancia de la Entidad</param>
        /// <param name="context">Contexto del Módulo</param>
        public static void SetAllModified<T>(this T entity, ObjectContext context) where T : IEntityWithKey
        {
            var stateEntry = context.ObjectStateManager.GetObjectStateEntry(entity.EntityKey);
            var propertyNameList = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata.Select(pn => pn.FieldType.Name);

            foreach (var propName in propertyNameList)
                stateEntry.SetModifiedProperty(propName);

        }
        public static void AttachUpdated(this ObjectContext obj, EntityObject objectDetached)
        {
            if (objectDetached.EntityState == EntityState.Detached)
            {
                object original = null;
                if (obj.TryGetObjectByKey(objectDetached.EntityKey, out original))
                    obj.ApplyPropertyChanges(objectDetached.EntityKey.EntitySetName, objectDetached);
                else
                    throw new ObjectNotFoundException();
            }
        }

        /// <summary>
        ///  Permite eliminar una sola entidad
        /// </summary>
        /// <param name="contexto">Contexto sobre el que se va a trabajar</param>
        /// <param name="unaListaEntidad"> Lista de Entidades a eliminar</param>
        public static void EliminarLista<T>(this ObjectContext contexto, List<T> unaListaEntidad) where T : EntityObject
        {
            foreach (var entidadAEliminar in unaListaEntidad)
            {
                contexto.Attach(entidadAEliminar);
                contexto.DeleteObject(entidadAEliminar);
            }
            contexto.SaveChanges();
        }
        /// <summary>
        ///  Permite crear la lista de registros
        /// </summary>
        /// <param name="contexto">Nombre del contexto</param>
        /// <param name="nombreContenedor">Nombre del contenedor</param>
        /// <param name="unaListaEntidad">Entidad enviada a crear</param>
        public static int CrearLista<T>(this ObjectContext contexto, string nombreContenedor, List<T> unaListaEntidad) where T : EntityObject
        {

            foreach (var item in unaListaEntidad)
       
                contexto.AddObject(nombreContenedor, item);
            return contexto.SaveChanges();

        }
        /// <summary>
        /// Permite guardar de manera generica los datos maestro - detalle
        /// </summary>
        /// <param name="contexto">Contexto donde se realiza la accion</param>
        /// <param name="entidadModificada">Entidad Modificada</param>
        /// <param name="entidadOriginal">Entidad Original</param>
        public static int Grabar(this ObjectContext contexto, EntityObject entidadModificada, EntityObject entidadOriginal)
        {

            ModificarDatos(contexto, entidadModificada, entidadOriginal);
            return contexto.SaveChanges();

        }

        /// <summary>
        ///  Permite modificar los datos antes de grabar
        /// </summary>
        /// <param name="contexto">Contexto sobre el que se trabaja</param>
        /// <param name="entidadModificada">Entidad modificada</param>
        /// <param name="entidadOriginal">Entidad Original</param>
        private static void ModificarDatos(ObjectContext contexto, EntityObject entidadModificada, EntityObject entidadOriginal)
        {
            
            contexto.Attach(entidadOriginal);
            ExtensionEntiy.SetAllModifiedValues(entidadOriginal, entidadModificada, contexto);
            ExtensionEntiy.ApplyReferencePropertyChanges(contexto, (IEntityWithRelationships)entidadModificada, (IEntityWithRelationships)entidadOriginal);
            SiguienteNivelGuardar(contexto, entidadModificada, entidadOriginal);

        }
        /// <summary>
        /// Baja hacia el siguiente nivel del arbol
        /// </summary>
        /// <param name="contexto">Contexto donde se realiza la accion</param>
        /// <param name="entidadModificada">Entidad Modificada</param>
        /// <param name="entidadOriginal">Entidad Original</param>
        private static void SiguienteNivelGuardar(ObjectContext contexto, EntityObject entidadModificada, EntityObject entidadOriginal)
        {
            IEnumerable<IRelatedEnd> relEndsModificados = ((IEntityWithRelationships)entidadModificada).RelationshipManager.GetAllRelatedEnds();
            IEnumerable<IRelatedEnd> relEndsOriginales = ((IEntityWithRelationships)entidadOriginal).RelationshipManager.GetAllRelatedEnds();

            SiguienteNivelEliminar(contexto, entidadModificada, relEndsModificados, relEndsOriginales);
            SiguienteNivelGuardarEntidades(contexto, entidadOriginal, relEndsModificados, relEndsOriginales);
        }

        /// <summary>
        /// Permite eliminar los datos en el siguiente nivel
        /// </summary>
        /// <param name="contexto">Contexto donde se realiza la accion</param>
        /// <param name="entidadModificada">Entidad Modificada</param>
        /// <param name="relEndsModificados">Coleccion de modificados</param>
        /// <param name="relEndsOriginales">Coleccion de originales</param>
        private static void SiguienteNivelEliminar(ObjectContext contexto, EntityObject entidadModificada, IEnumerable<IRelatedEnd> relEndsModificados, IEnumerable<IRelatedEnd> relEndsOriginales)
        {
            foreach (IRelatedEnd relEndOriginal in relEndsOriginales.Where(p => p.IsEntityReference() == false).ToList<IRelatedEnd>())
            {
                List<IRelatedEnd> listaModificada = ((IEntityWithRelationships)entidadModificada).RelationshipManager.GetAllRelatedEnds().Where(p => p.IsEntityReference() == false).ToList<IRelatedEnd>();
                bool blnEliminar = true;

                List<EntityObject> listaEliminar = new List<EntityObject>();

                foreach (EntityObject entidadOriginal in relEndOriginal)
                {
                    IRelatedEnd relacionModificada = listaModificada.Where(p => p.SourceRoleName == relEndOriginal.SourceRoleName && p.TargetRoleName == relEndOriginal.TargetRoleName && p.RelationshipName == relEndOriginal.RelationshipName).FirstOrDefault();

                    blnEliminar = true;
                    foreach (EntityObject entidadMod in relacionModificada)
                    {
                        if (entidadMod.EntityKey == entidadOriginal.EntityKey)
                            blnEliminar = false;
                    }

                    if (blnEliminar)
                        listaEliminar.Add(entidadOriginal);
                }

                bool blnEliminoRegistros = false;

                foreach (var item in listaEliminar)
                {
                    contexto.DeleteObject(item);
                    blnEliminoRegistros = true;
                }

                if (blnEliminoRegistros)
                    contexto.SaveChanges();

            }
        }
        /// <summary>
        /// Verifica si existe referencia hacia la entidad
        /// </summary>
        /// <param name="relatedEnd"></param>
        /// <returns></returns>
        public static bool IsEntityReference(this IRelatedEnd relatedEnd)
        {
            Type relationshipType = relatedEnd.GetType();
            return (relationshipType.GetGenericTypeDefinition() == typeof(EntityReference<>));
        }
        /// <summary>
        /// Permite guardar los datos en el siguiente nivel
        /// </summary>
        /// <param name="contexto">Contexto donde se realiza la accion</param>
        /// <param name="entidadOriginal">Entidad Original</param>
        /// <param name="relEndsModificados">Coleccion de modificados</param>
        /// <param name="relEndsOriginales">Coleccion de originales</param>
        private static void SiguienteNivelGuardarEntidades(ObjectContext contexto, EntityObject entidadOriginal, IEnumerable<IRelatedEnd> relEndsModificados, IEnumerable<IRelatedEnd> relEndsOriginales)
        {
            foreach (IRelatedEnd relEndModificado in relEndsModificados.Where(p => p.IsEntityReference() == false).ToList<IRelatedEnd>())
            {
                List<EntityObject> listaSinModificar = new List<EntityObject>();
                List<IRelatedEnd> listaOriginal = ((IEntityWithRelationships)entidadOriginal).RelationshipManager.GetAllRelatedEnds().Where(p => p.IsEntityReference() == false).ToList<IRelatedEnd>();
                //Original Codigo, Modificada valor
                Dictionary<EntityObject, EntityObject> listaRelacional = new Dictionary<EntityObject, EntityObject>();

                foreach (EntityObject entidadModificada in relEndModificado)
                {
                    ///Esto significa que agrego la entidad
                    if (((EntityObject)entidadModificada).EntityKey == null)
                    {
                     
                        listaSinModificar.Add(entidadModificada);
                    }
                    else
                    {
                        IRelatedEnd relacionOriginal = listaOriginal.Where(p => p.SourceRoleName == relEndModificado.SourceRoleName && p.TargetRoleName == relEndModificado.TargetRoleName && p.RelationshipName == relEndModificado.RelationshipName).FirstOrDefault();

                        foreach (EntityObject entidadOrig in relacionOriginal)
                        {
                            if (entidadOrig.EntityKey == entidadModificada.EntityKey)
                                listaRelacional.Add(entidadOrig, entidadModificada);

                        }

                        foreach (var item in listaRelacional)
                        {
                           
                            ExtensionEntiy.SetAllModifiedValues(item.Key, item.Value, contexto);
                            ExtensionEntiy.ApplyReferencePropertyChanges(contexto, (IEntityWithRelationships)item.Value, (IEntityWithRelationships)item.Key);
                        }

                    }

                }

                for (int i = 0; i < listaSinModificar.Count; i++)
                    ((IEntityWithRelationships)entidadOriginal).RelationshipManager.GetAllRelatedEnds().Where(p => p.SourceRoleName == relEndModificado.SourceRoleName && p.TargetRoleName == relEndModificado.TargetRoleName && p.RelationshipName == relEndModificado.RelationshipName).Select(p => p).FirstOrDefault().Add(listaSinModificar[i]);
            }
        }
        /// <summary>
        /// Permite asignar en el contexto las entidades cambiadas con las entidades originales
        /// </summary>
        /// <typeparam name="T">Tipo de Entidad</typeparam>
        /// <param name="entity">Entidad original</param>
        /// <param name="entityCambiada">Entidad Modificada</param>
        /// <param name="context">Contexto</param>
        public static void SetAllModifiedValues<T>(T entity, T entityCambiada, ObjectContext context) where T : IEntityWithKey
        {
            var stateEntry = context.ObjectStateManager.GetObjectStateEntry(entity.EntityKey);
            PropertyInfo[] propiedadesOriginales = entityCambiada.GetType().GetProperties();

            for (int i = 0; i < stateEntry.CurrentValues.FieldCount; i++)
            {
                PropertyInfo propiedad = propiedadesOriginales.ToList().Where(p => p.Name == stateEntry.CurrentValues.GetName(i)).FirstOrDefault();
                if (!stateEntry.CurrentValues.GetValue(i).Equals(propiedad.GetValue(entityCambiada, null)))
                {
                    stateEntry.SetModifiedProperty(propiedad.Name);
                    stateEntry.CurrentValues.SetValue(i, propiedad.GetValue(entityCambiada, null));
                }
            }

        }
        /// <summary>
        /// Este metodo permite aplicar los cambios sobre que se encuentran asociadas o agregadas a la principal
        /// </summary>
        /// <param name="context">Contexto sobre el cual se realizan los cambios</param>
        /// <param name="newEntity">Nueva entidad</param>
        /// <param name="oldEntity">Entidad original</param>
        public static void ApplyReferencePropertyChanges(this ObjectContext context,

            IEntityWithRelationships newEntity,

            IEntityWithRelationships oldEntity)
        {

            foreach (var relatedEnd in oldEntity.RelationshipManager.GetAllRelatedEnds())
            {
                var oldRef = relatedEnd as EntityReference;

                if (oldRef != null)
                {
                    // this related end is a reference not a collection 
                    var newRef = newEntity.RelationshipManager.GetRelatedEnd(oldRef.RelationshipName, oldRef.TargetRoleName) as EntityReference;

                    oldRef.EntityKey = newRef.EntityKey;

                }
            }
        }
    }
}
