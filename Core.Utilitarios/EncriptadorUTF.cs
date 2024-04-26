using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Core.Utilitarios
{
    /// <summary>
    /// Clase encriptamiento UTF
    /// </summary>
    public class EncriptadorUTF
    {
        /// <summary>
        /// Varaiable para cifrado
        /// </summary>
        private static SymmetricAlgorithm mCSP;


        #region Constructores
        /// <summary>
        /// Constructor
        /// </summary>
        static EncriptadorUTF()
        {

            mCSP = new TripleDESCryptoServiceProvider();
            //Seteo de clave y Vector de inicio, no modificar estas claves por favor
            mCSP.Key = new Byte[] { Convert.ToByte("71"), Convert.ToByte("24"), Convert.ToByte("103"), Convert.ToByte("58"), Convert.ToByte("162"), Convert.ToByte("235"), Convert.ToByte("211"), Convert.ToByte("130"), Convert.ToByte("134"), Convert.ToByte("212"), Convert.ToByte("56"), Convert.ToByte("119"), Convert.ToByte("70"), Convert.ToByte("108"), Convert.ToByte("91"), Convert.ToByte("113"), Convert.ToByte("189"), Convert.ToByte("247"), Convert.ToByte("9"), Convert.ToByte("17"), Convert.ToByte("157"), Convert.ToByte("9"), Convert.ToByte("65"), Convert.ToByte("35") };
            mCSP.IV = new Byte[] { Convert.ToByte("230"), Convert.ToByte("128"), Convert.ToByte("180"), Convert.ToByte("179"), Convert.ToByte("98"), Convert.ToByte("247"), Convert.ToByte("139"), Convert.ToByte("137") };

        }
        #endregion

        /// <summary>
        /// Traducir de texto encriptado a texto entendible
        /// </summary>
        /// <param name="prmValor">Texto encriptado</param>
        /// <returns>Texto entendible</returns>
        public static string Desencriptar(string prmValor)
        {

            ICryptoTransform ictEncriptado;
            MemoryStream mstMemoria;
            CryptoStream cytFlujo;
            byte[] bytArreglo;

            ictEncriptado = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

            bytArreglo = Convert.FromBase64String(prmValor);

            mstMemoria = new MemoryStream();
            cytFlujo = new CryptoStream(mstMemoria, ictEncriptado, CryptoStreamMode.Write);
            cytFlujo.Write(bytArreglo, 0, bytArreglo.Length);
            cytFlujo.FlushFinalBlock();

            cytFlujo.Close(); cytFlujo = null;
            ictEncriptado.Dispose(); ictEncriptado = null;

            return Encoding.UTF8.GetString(mstMemoria.ToArray());

        }

        /// <summary>
        /// Traducir de texto entendible a texto encriptado
        /// </summary>
        /// <param name="prmValor">Texto entendible</param>
        /// <returns>Texto Encriptado</returns>
        public static string Encriptar(string prmValor)
        {

            ICryptoTransform ictEncriptado;
            MemoryStream mstMemoria;
            CryptoStream cytFlujo;
            byte[] bytArreglo;

            ictEncriptado = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);

            bytArreglo = Encoding.UTF8.GetBytes(prmValor);

            mstMemoria = new MemoryStream();
            cytFlujo = new CryptoStream(mstMemoria, ictEncriptado, CryptoStreamMode.Write);
            cytFlujo.Write(bytArreglo, 0, bytArreglo.Length);
            cytFlujo.FlushFinalBlock();

            cytFlujo.Close(); cytFlujo = null;
            ictEncriptado.Dispose(); ictEncriptado = null;

            return Convert.ToBase64String(mstMemoria.ToArray());


        }

    }
}
