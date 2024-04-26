using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Core.Entidades
{
    /// <summary>
    /// Clase base de una Entidad 
    /// </summary>
    /// 
    [DataContract]
    public class BaseEntidad : IDisposable
    {

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {

        }
    }
}
