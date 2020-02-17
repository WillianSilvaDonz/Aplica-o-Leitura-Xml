using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace EmissorNfse.SoapModules.Utils
{
    public static class XDocumentUtils
    {
        #region Private Static Methods

        /// <summary>
        /// Decora uma string caso a mesma não for vazia
        /// </summary>
        /// <param name="text">string a ser decorada.</param>
        /// <param name="func">Func&lt;string, string&gt; de decoração.</param>
        /// <returns>System.IO.Stream correspondente a string decorada.</returns>
        private static string Decorate(this string text, Func<string, string> func)
        {
            if (func == null)
            {
                return text;
            }

            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            return func.Invoke(text);
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Constrói uma consulta XPath que recupera todos os elementos com um determinado nome local (independentemente de namespace)
        /// </summary>
        /// <param name="elementName">string do nome do elemento.</param>
        /// <returns>string correspondente ao comando XPath.</returns>
        public static string ToXPathElement(this string elementName, bool absolute = false)
        {
            return (absolute ? "//" : "") + "*[local-name()='" + elementName + "']";

        }

        /// <summary>
        /// Obtém o valor textual de um determinado nó decendente de um elemento XML
        /// </summary>
        /// <param name="xElement">System.Xml.Linq.XElement relativo ao nó raiz da busca.</param>
        /// <param name="funcDecorate">Func&lt;string, string&gt; de decoração do valor textual recuperado (opcional)</param>
        /// <param name="isNullable">bool que define se o valor default será null ou string vazia (opcional)</param>
        /// <returns>string correspondente ao valor textual do elemento XML.</returns>
        public static string GetNodeValue(this XElement xElement, string xPathName, Func<string, string> funcDecorate = null, bool isNullable = false)
        {
            var value = "";
            var res = xElement.XPathSelectElement(xPathName);
            if (res != null)
            {
                var nodes = res.Nodes().ToArray();
                if (nodes.Any() && nodes[0].NodeType == System.Xml.XmlNodeType.Text)
                {
                    value = nodes[0].ToString().Decorate(funcDecorate);
                }
            }
            if ((isNullable) && (string.IsNullOrEmpty(value)))
            {
                value = null;
            }
            return value;
        }

        /// <summary>
        /// Obtém o valor textual de um determinado nó decendente de um elemento XML
        /// </summary>
        /// <param name="xDocument">System.Xml.Linq.XDocument relativo ao documento XML da busca.</param>
        /// <param name="funcDecorate">Func&lt;string, string&gt; de decoração do valor textual recuperado (opcional)</param>
        /// <param name="isNullable">bool que define se o valor default será null ou string vazia (opcional)</param>
        /// <returns>string correspondente ao valor textual do elemento XML.</returns>
        public static string GetNodeValue(this XDocument xDocument, string xPathName, Func<string, string> func = null, bool isNullable = false)
        {
            return GetNodeValue(xDocument.Root, xPathName, func);
        }

        /// <summary>
        /// Obtém o valor textual de um determinado nó decendente de um elemento XML, retornando null como valor default
        /// </summary>
        /// <param name="xDocument">System.Xml.Linq.XDocument relativo ao documento XML da busca.</param>
        /// <param name="funcDecorate">Func&lt;string, string&gt; de decoração do valor textual recuperado (opcional)</param>
        /// <returns>string correspondente ao valor textual do elemento XML.</returns>
        public static string GetNodeValueNullable(this XDocument xDocument, string xPathName, Func<string, string> func = null)
        {
            return GetNodeValue(xDocument.Root, xPathName, func, true);
        }

        /// <summary>
        /// Converte um texto XML em um XDocument, lançando uma exceção com o próprio XML na mensagem em caso de falha de leitura.
        /// </summary>
        /// <param name="xml">object relativo ao texto XML.</param>
        /// <returns>System.Xml.Linq.XDocument contendo o XML recebido.</returns>
        public static XDocument ToXDocument(this object xml)
        {
            try
            {
                return XDocument.Parse(xml.ToString());
            }
            catch (Exception e)
            {
                throw new Exception(xml.ToString());
            }
        }

        #endregion
    }
}
