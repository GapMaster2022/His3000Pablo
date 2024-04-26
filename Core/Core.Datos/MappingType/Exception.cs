using System;
using System.Runtime.Serialization;


namespace Core.Datos.MappingType
{
    /// <summary>
    /// Control de Excepción en Mapeo
    /// </summary>
    [Serializable]
    public class CSException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CSException()
        {
        }

        /// <summary>
        /// Mensaje del Incidente
        /// </summary>
        /// <param name="message"></param>
        public CSException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CSException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public CSException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CSObjectNotFoundException : CSException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        public CSObjectNotFoundException(Type type, object key)
            : base(String.Format("Object with key [{0}] of type [{1}] does not exist", key, type.Name))
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CSOptimisticLockException : CSException
    {
        /// <summary>
        /// 
        /// </summary>
        public CSOptimisticLockException()
            : base("Optimistic lock error")
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CSValidationException : CSException
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public class CSExpressionException : CSException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public CSExpressionException(string message)
            : base(message)
        {
        }
    }
}
