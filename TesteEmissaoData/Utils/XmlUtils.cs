using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace EmissorNfse.Module.Utils
{
    /// <summary>
    /// Classe auxiliar para Xmls.
    /// </summary>
    public static class XmlUtils
    {
        #region Public Static Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="resourceSchemas"></param>
        public static void ValidadeSchema(string xml, params string[] resourceSchemas)
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();

            foreach (string resourceSchema in resourceSchemas)
                xmlReaderSettings.Schemas.Add(null, XmlReader.Create(ConvertUtils.StringToStream(resourceSchema)));

            xmlReaderSettings.ValidationType = ValidationType.Schema;
            xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ReportValidationWarnings;
            xmlReaderSettings.ValidationEventHandler += new ValidationEventHandler((s, e) =>
            {
                var sender = ((XmlReader)s);
                string mensagem = string.Format("Falha na validação do Xml: Tag:{0}, Valor:{1}, Tipo:{2}", sender.Name, sender.Value, sender.ValueType.Name);

                //throw new Exception(mensagem, new Exception(e.Message, e.Exception));
            });

            XmlReader xmlReader = XmlReader.Create(ConvertUtils.StringToStream(xml), xmlReaderSettings);

            while (xmlReader.Read()) { }
        }

        /// <summary>
        /// Serializa um objeto em xml.
        /// </summary>
        /// <param name="obj">Objeto a ser serializado.</param>
        /// <returns>String no formato xml.</returns>
        public static string Serializar(object obj, bool changeEncodingToUtf8, XmlSerializerNamespaces namespaces = null, bool removeDeclation = false)
        {
            StringWriter stringWriter = new StringWriter();
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            string xmlNamespace = GetXmlNamespace(obj);
            if (xmlNamespace != null)
            {
                XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", xmlNamespace);
                serializer.Serialize(stringWriter, obj, (namespaces == null) ? xmlSerializerNamespaces : namespaces);
            }
            else
                serializer.Serialize(stringWriter, obj);

            var xml = GetXmlDocument(stringWriter.ToString());
            if (removeDeclation && xml.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                xml.RemoveChild(xml.FirstChild);
            return changeEncodingToUtf8 ? xml.OuterXml.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"") : xml.OuterXml;
        }

        /// <summary>
        /// Verifica se o objeto informado possui um atributo do tipo XmlTypeAttribute e retorna sua propriedade Namespace.
        /// </summary>
        /// <param name="obj">Objeto que possui ou não o atributo XmlTypeAttribute.</param>
        /// <returns>string com namespace do atributo ou null se não encontrar o atributo.</returns>
        public static string GetXmlNamespace(object obj)
        {
            XmlTypeAttribute[] xmlTypeAttributes = (XmlTypeAttribute[])obj.GetType().GetCustomAttributes(typeof(XmlTypeAttribute), false);

            if (xmlTypeAttributes.Length > 0)
                return xmlTypeAttributes[0].Namespace;
            else
                return null;
        }

        /// <summary>
        /// Assina um elemento Xml pela tag de identificação.
        /// </summary>
        /// <param name="xml">Xml que dese ser assinado.</param>
        /// <param name="idReference">Conteudo da tag de referencia para assinatura.</param>
        /// <param name="certificate">Certificado usado para assinatura.</param>
        /// <returns>Elemento Xml contendo a assinatura.</returns>
        public static string GetSignature(XmlDocument xml, string idReference, X509Certificate2 certificate)
        {
            SignedXml signedXml = new SignedXml(xml);
            signedXml.Signature.Id = "Ass_" + idReference;
            signedXml.SigningKey = certificate.PrivateKey;

            idReference = !string.IsNullOrEmpty(idReference) ? "#" + idReference : "";
            Reference reference = new Reference(idReference);
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigC14NTransform());

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificate));

            signedXml.AddReference(reference);
            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();

            return signedXml.GetXml().OuterXml;
        }

        /// <summary>
        /// Assina string através de certificado: RSA-SHA1
        /// </summary>
        /// <param name="str"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static string GetSignData(this string str, X509Certificate2 certificate)
        {
            var byteConverter = new ASCIIEncoding();
            var asciiBytes = byteConverter.GetBytes(str);

            var rsa = certificate.GetRSAPrivateKey();

            return Convert.ToBase64String(rsa.SignData(asciiBytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1));
        }

        /// <summary>
        /// Transforma um xml em objeto.
        /// </summary>
        /// <param name="xml">string xml.</param>
        /// <param name="tipo">tipo do objeto que deve ser gerado.</param>
        /// <returns>Objeto do tipo informado.</returns>
        public static object Deserializar(string xml, Type tipo)
        {
            StringReader reader = new StringReader(xml);
            XmlSerializer serializer = new XmlSerializer(tipo);

            return serializer.Deserialize(reader);
        }

        /// <summary>
        /// Transforma um xml na classe correspondente.
        /// </summary>
        /// <typeparam name="T">Tipo da classe correspondente.</typeparam>
        /// <param name="xml">Xml que deve ser transformado.</param>
        /// <returns>Instancia de T</returns>
        public static T Deserializar<T>(string xml)
        {
            return (T)Deserializar(xml, typeof(T));
        }

        /// <summary>
        /// Assina um elemento Xml pela tag de identificação.
        /// </summary>
        /// <typeparam name="T">Tipo da assinatura para retorno.</typeparam>
        /// <param name="obj">Objeto que deve ser transformado em xml.</param>
        /// <param name="idReference">Conteudo da tag de referencia para assinatura.</param>
        /// <param name="certificate">Certificado usado para assinatura.</param>
        /// <returns></returns>
        public static T GetSignature<T>(object obj, string idReference, X509Certificate2 certificate)
        {
            return Deserializar<T>(GetSignature(SerializarToXmlDocument(obj, true), idReference, certificate));
        }

        /// <summary>
        /// Gera um XmlDocument de um objeto serializado.
        /// </summary>
        /// <param name="obj">Objeto a ser serializado.</param>
        /// <param name="changeEncodingToUtf8">Usar enconding UTF-8.</param>
        /// <returns>String no formato xml.</returns>
        public static XmlDocument SerializarToXmlDocument(object obj, bool changeEncodingToUtf8)
        {
            return GetXmlDocument(Serializar(obj, changeEncodingToUtf8));
        }

        /// <summary>
        /// Cria um XmlDocument a partir de um xml.
        /// </summary>
        /// <param name="xml">Xml que será convertido em XmlDocument.</param>
        /// <returns>Instancia de XmlDocument.</returns>
        public static XmlDocument GetXmlDocument(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            return xmlDocument;
        }
        #endregion
    }
}
