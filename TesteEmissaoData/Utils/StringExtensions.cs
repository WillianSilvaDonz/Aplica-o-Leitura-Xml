using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace EmissorNfse.SoapModules.Utils
{
	/// <summary>
	/// Classe com operações de strings.
	/// </summary>
	public static class StringExtensions
	{
        #region Private Static Methods

        /// <summary>
        /// Percorre os elementos e remove namespaces de atributos
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns>Retorna um XElement limpo de namespace</returns>
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                var xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }

            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Preenche a string com zeros a esquerda.
        /// </summary>
        /// <param name="valor">valor que deve receber os zeros.</param>
        /// <param name="tamanhoValor">Tamanho da string de retorno.</param>
        /// <returns>string formatada com zeros a esquerda.</returns>
        public static string SetZerosEsquerda(this string valor, int tamanhoValor)
		{
			string strRetorno = "";
			for(int i = 0; i < (tamanhoValor - valor.Length); i++)
				strRetorno += "0";

			return strRetorno + valor;
		}

		/// <summary>
		/// Formata um um array de string usando a função 'string.Format'.
		/// </summary>
		/// <param name="strings">Array de string a ser formatado.</param>
		/// <param name="args">Argumentos de formatação.</param>
		/// <returns>Array de string formatado.</returns>
		public static string[] Format(this string[] strings, params object[] args)
		{
			for(int i = 0; i < strings.Length; i++)
				strings[i] = string.Format(strings[i], args);

			return strings;
		}

		/// <summary>
		/// Extrai os dígitos de uma string
		/// </summary>
		/// <param name="valor">string cujos dígitos serão extraídos.</param>
		/// <returns>string com os dígitos da string recebida</returns>
		public static string GetNumber(this object obj)
		{
			string numbers = string.Empty;
			foreach(char @char in obj.ToString().ToCharArray())
				if("0123456789".Contains(@char.ToString()))
					numbers += @char.ToString();

			return numbers;
		}

		/// <summary>
		/// Extrai os dígitos de uma string e completa com zeros a esquerda
		/// </summary>
		/// <param name="valor">string cujos dígitos serão extraídos.</param>
		/// <param name="tamanhoValor">quantidade de zeros à esquerda.</param>
		/// <returns>string com os dígitos da string recebida</returns>       
		public static string GetNumber(this object obj, int tamanhoValor)
		{
			return obj.GetNumber().SetZerosEsquerda(tamanhoValor);
		}

		/// <summary>
		/// Remove o cabeçalho padrão (com encoding 'utf-8')de um texto XML
		/// </summary>
		/// <param name="valor">string do XML.</param>
		/// <returns>XML sem o cabeçalho</returns>
		public static string RemoveXmlHeader(this string xml)
		{
			return xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
		}

		/// <summary>
		/// Converte um texto para XML (&amp;lt; e &amp;gt; para &lt; e &gt;)
		/// </summary>
		/// <param name="valor">string a ser convertida.</param>
		/// <returns>XML convertido</returns>
		public static string DecodeToXml(this string xml)
		{
			if(xml.Trim().StartsWith("&lt;"))
			{
				var xmlDoc = new XmlDocument();
				xmlDoc.LoadXml("<raiz>" + xml + "</raiz>");

				return xmlDoc.DocumentElement.InnerText;
			}
			else
				return xml;
		}

		/// <summary>
		/// Capitaliza uma string (primeiro caractere em maiúsculo e o restante em minúsculo)
		/// </summary>
		/// <param name="valor">string a ser capitalizada.</param>
		/// <returns>string capitalizada</returns>
		public static string Capitalize(this string text)
		{
			return string.IsNullOrEmpty(text) ? "" : (text.Substring(0, 1).ToUpper()) + (text.Substring(1).ToLower());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		public static string Truncate(this string text, int size)
		{
			return text.Length > size ? text.Substring(0, size) : text;
		}

		/// <summary>
		/// Tenta converter uma string em inteiro, se não for bem sucedido retorna 0.
		/// </summary>
		/// <param name="valor">string a ser convertida.</param>
		/// <returns>valor inteiro</returns>
		public static int GetInteger(this object obj)
		{
			var number = 0;
			int.TryParse(GetNumber(obj), out number);

			return number;
		}

        /// <summary>
		/// Tenta converter uma string em long, se não for bem sucedido retorna 0.
		/// </summary>
		/// <param name="valor">string a ser convertida.</param>
		/// <returns>valor long</returns>
        public static long GetLong(this object obj)
        {
            long number = 0;
            long.TryParse(GetNumber(obj), out number);

            return number;
        }

        /// <summary>
        /// Remove os caracteres de uma string. [Não gera exceção se ela for vazia].
        /// </summary>
        /// <param name="str">String que tera os caracteres removidos.</param>
        /// <param name="startIndex">Ponto inicial da remoção.</param>
        /// <param name="count">Quantidade de caracteres removidos.</param>
        /// <returns>String</returns>
        public static string StrRemove(this string str, int startIndex, int count)
		{
			return string.IsNullOrWhiteSpace(str) ? str : str.Remove(startIndex, count);
		}

        /// <summary>
        /// Remove namespaces de elementos Root da string
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns>Retorna uma string limpa de namespaces</returns>
        public static string RemoveAllNamespaces(this string xmlDocument)
        {
            var xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        /// <summary>
        /// Substring que verifica se o tamanho final da string realmente existe
        /// </summary>
        /// <param name="stringTarget"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns>Nova String</returns>
        public static string DecreaseString(this string stringTarget, int index, int length)
        {
            if (!string.IsNullOrEmpty(stringTarget))
            {
                if (stringTarget.Length > length)
                {
                    return stringTarget.Substring(index, length);
                }
            }

            return stringTarget;
        }

        /// <summary>
        /// Repete determinado caracter 'n' vezes
        /// </summary>
        /// <param name="stringTarget"></param>
        /// <param name="length"></param>
        /// <returns>String com caracter 'n' vezes</returns>
        public static string StringRepeat(this string stringTarget, int length)
        {
            return new StringBuilder(stringTarget.Length * length).Insert(0, stringTarget, length).ToString();
        }

        /// <summary>
        /// Preenche determinada string com espaçoes de acordo com tamanho informado
        /// </summary>
        /// <param name="stringTarget"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns>String com tamanho informado</returns>
        public static string FillWithSpace(this string stringTarget, int index, int length)
        {
            return string.Format("{" + index + ", " + length + "}", stringTarget);
        }

        /// <summary>
        /// Retorna uma string formatada contendo o valor. Ex de retorno 10,50
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public static string FormatPrice(this decimal price)
        {
            return string.Format("{0:0,0.00}", price);
        }

        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        #endregion
    }
}