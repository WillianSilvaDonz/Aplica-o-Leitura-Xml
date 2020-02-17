using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace EmissorNfse.Module.Utils
{
    /// <summary>
    /// Classe com conversões de tipos e dados.
    /// </summary>
    public static class ConvertUtils
    {
        #region Public Static Properties

        /// <summary>
        /// Transforma uma string e System.IO.Stream.
        /// </summary>
        /// <param name="str">string que deve ser convertida.</param>
        /// <returns>System.IO.Stream correspondente a string.</returns>
        public static Stream StringToStream(string str)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// Transforma uma System.IO.Stream em uma string.
        /// </summary>
        /// <param name="stream">stream que deve ser convertida.</param>
        /// <returns>string correspondente a stream.</returns>
        public static String StreamToString(Stream stream)
        {
            return new StreamReader(stream).ReadToEnd();
        }

        /// <summary>
        /// Converte um objecto em byte[].
        /// </summary>
        /// <param name="obj">Objeto a ser convertido.</param>
        /// <returns>Array de bytes.</returns>
        public static byte[] ObjectToByteArray(Object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Converte uma string em hash base64.
        /// </summary>
        /// <param name="str">String que deve ser convertida.</param>
        /// <returns>String com Hash base64.</returns>
        public static string StringToBase64String(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// Converte um hash base64 em string.
        /// </summary>
        /// <param name="str">String contendo o Hash.</param>
        /// <returns>String convertida.</returns>
        public static string Base64StringToString(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        #endregion
    }
}
